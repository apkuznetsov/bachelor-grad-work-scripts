using SensorConnector.Common.CommonClasses;
using SensorConnector.Common.Entities;
using SensorOutputParser.CommandLineArgsParser;
using SensorOutputParser.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;
using static SensorConnector.Common.AppSettings;

namespace SensorOutputParser
{
    public class Program
    {
        private static List<ParsedInputParams> _parsedInputParamsList = new List<ParsedInputParams>();

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
              TestId = 132,
              LeftTimeBorder = 1588985973804227400,
              RightTimeBorder = 1589024162229517700,
              Sensors = new List<Sensor>()
              {
                  new Sensor("127.0.0.1", 1111),
                  new Sensor("127.0.0.1", 3333)
              }
          });

            #endregion Test


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


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}