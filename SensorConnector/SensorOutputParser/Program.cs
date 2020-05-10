using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SensorConnector.Common;
using SensorConnector.Common.Entities;
using SensorConnector.Persistence;
using SensorOutputParser.CommandLineArgsParser;
using SensorOutputParser.Exporting;
using SensorOutputParser.Queries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;
using static SensorConnector.Common.AppSettings;
using Sensor = SensorConnector.Common.CommonClasses.Sensor;

namespace SensorOutputParser
{
    public class Program
    {
        private static List<ParsedInputParams> _parsedInputParamsList = new List<ParsedInputParams>();
        private static List<SensorWithParsedDatatype> _sensorWithParsedDatatypes = new List<SensorWithParsedDatatype>();

        private static InfluxClient _client;

        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            _client = new InfluxClient(new Uri(InfluxHost));

            // var databases = await _client.ShowDatabasesAsync();

            await _client.CreateDatabaseAsync(DatabaseName); // creates db if not exist

            #region Test

          _parsedInputParamsList.Add(new ParsedInputParams()
          {
              TestId = 444,
              LeftTimeBorder = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(-1), DateTimeKind.Utc),
              RightTimeBorder = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-2), DateTimeKind.Utc),
              Sensors = new List<Sensor>()
              {
                  new Sensor("127.0.0.1", 1111),
                  new Sensor("127.0.0.1", 3333)
              }
          });

            #endregion Test

            foreach (var item in _parsedInputParamsList)
            {
                await ParseSensorsDatatypeAsync(item.Sensors);
            }


            var jsonString = "{\r\n    \"Temrerature\": \"double\",\r\n    \"Moisture\": \"int\",\r\n    \"Comment\": \"string\"\r\n}";

            var parsedJson = JObject.Parse(jsonString);

            var sensorFieldTypeNameDictionary = new Dictionary<string, string>();

            var currentField = parsedJson.First;

            while (currentField != null)
            {
                var splitCurrentField = currentField
                    .ToString()
                    .Replace("\"", "")
                    .Replace(" ", "")
                    .Split(':');

                var fieldName = splitCurrentField[0];
                var fieldTypeName = splitCurrentField[1];

                sensorFieldTypeNameDictionary.Add(fieldName, fieldTypeName);

                currentField = currentField.Next;
            }
            
            var query = QueryMaker.GetSensorOutputsForTest(
                _parsedInputParamsList[0].TestId,
                _parsedInputParamsList[0].LeftTimeBorder,
                _parsedInputParamsList[0].RightTimeBorder,
                _parsedInputParamsList[0].Sensors);

            var resultSet = await _client.ReadAsync<SensorOutput>(DatabaseName, query);

            // resultSet will contain 1 result in the Results collection (or multiple if you execute multiple queries at once)
            var result = resultSet.Results[0];

            // result will contain 1 series in the Series collection (or potentially multiple if you specify a GROUP BY clause)
            var series = result.Series[0];

            var outputs = series.Rows;

            var outputsForExport = ParseRetrievedData(outputs);

            var jsonExport = new JsonExport(
                _parsedInputParamsList[0].TestId,
                _parsedInputParamsList[0].LeftTimeBorder,
                _parsedInputParamsList[0].RightTimeBorder,
                outputsForExport);

            var exportedFile = jsonExport.GetSensorOutputsFile();

            var directoryPath = AppDomain.CurrentDomain.BaseDirectory + $"parsed-files";

            /*
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var path = $"{directoryPath}\\{exportedFile.FileName}";

            File.WriteAllBytes(path, exportedFile.FileContents);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            */
        }

        private static async Task ParseSensorsDatatypeAsync(List<Sensor> sensors)
        {
            foreach (var sensor in sensors)
            {
                if (_sensorWithParsedDatatypes
                    .Any(x => x.IpAddress == sensor.IpAddress && x.Port == sensor.Port))
                {
                    continue;
                }

                await using var context = new DmsDbContext();

                var sensorWithDatatype = await context.Sensors
                    .Include(x => x.DataType)
                    .Include(x => x.CommunicationProtocol)
                    .FirstOrDefaultAsync(x => x.IpAddress == sensor.IpAddress &&
                                              x.Port == sensor.Port);

                var parsedJson = JObject.Parse(sensorWithDatatype.DataType.Schema);

                var sensorFieldDescriptions = new List<SensorFieldDescription>();

                foreach (var property in parsedJson.Properties())
                {
                    var fieldName = property.Name;
                    var fieldTypeName = property.Value.ToString();

                    sensorFieldDescriptions.Add(
                        new SensorFieldDescription(fieldName, fieldTypeName));
                }

                _sensorWithParsedDatatypes.Add(
                    new SensorWithParsedDatatype()
                    {
                        SensorId = sensorWithDatatype.SensorId,
                        IpAddress = sensorWithDatatype.IpAddress,
                        Port = sensorWithDatatype.Port,
                        FieldDescriptions = sensorFieldDescriptions
                    });
            }
        }

        private static List<SensorOutputForExport> ParseRetrievedData(List<SensorOutput> sensorOutputs)
        {
            var result = new List<SensorOutputForExport>();

            foreach (var sensorOutput in sensorOutputs)
            {
                var sensorFieldValues = new List<SensorFieldValue>();

                var relatedSensor = _sensorWithParsedDatatypes
                    .FirstOrDefault(x =>
                        x.IpAddress.Equals(sensorOutput.SensorIpAddress) && x.Port.Equals(sensorOutput.SensorPort));

                var rawData = sensorOutput.Data;
                var splitRawData = rawData.Split(',');

                if (splitRawData.Length != relatedSensor.FieldDescriptions.Count)
                {
                    continue;
                }

                for (var i = 0; i < splitRawData.Length; i++)
                {
                   var fieldDescription = relatedSensor.FieldDescriptions[i];

                   var fieldValue = new SensorFieldValue()
                   {
                       FieldName = fieldDescription.FieldName
                   };

                   switch (fieldDescription.FieldTypeName)
                   {
                       case AppSettings.SensorOutputParser.AllowedTypeNames.Int:
                       {
                           if (int.TryParse(splitRawData[i], out var intValue))
                           {
                               fieldValue.FieldValue = intValue as int?;
                           }

                           break;
                       }

                       case AppSettings.SensorOutputParser.AllowedTypeNames.Double:
                       {
                           if (double.TryParse(
                               splitRawData[i],
                               NumberStyles.Any,
                               CultureInfo.InvariantCulture, 
                               out var doubleValue))
                           {
                               fieldValue.FieldValue = doubleValue as double?;
                           }

                           break;
                       }

                       case AppSettings.SensorOutputParser.AllowedTypeNames.String:
                       {
                           fieldValue.FieldValue = splitRawData[i] as string;

                           break;
                       }

                       case AppSettings.SensorOutputParser.AllowedTypeNames.Bool:
                       {
                           if (int.TryParse(splitRawData[i], out var boolAsIntValue))
                           {
                               fieldValue.FieldValue = Convert.ToBoolean(boolAsIntValue);

                               break;

                           }

                           if (bool.TryParse(splitRawData[i], out var boolValue))
                           {
                               fieldValue.FieldValue = boolValue as bool?;
                           }

                           break;
                       }
                   }

                   sensorFieldValues.Add(fieldValue);
                }

                var outputForExport = new SensorOutputForExport()
                {
                    CollectedAt = sensorOutput.Timestamp,
                    SensorId = relatedSensor.SensorId,
                    IpAddress = sensorOutput.SensorIpAddress,
                    Port = sensorOutput.SensorPort,
                    ParsedData = sensorFieldValues
                };

                result.Add(outputForExport);
            }

            return result;
        }
    }
}