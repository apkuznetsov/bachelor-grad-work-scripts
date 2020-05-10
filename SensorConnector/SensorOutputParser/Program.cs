using SensorConnector.Common.Entities;
using SensorOutputParser.CommandLineArgsParser;
using SensorOutputParser.Exporting;
using SensorOutputParser.Queries;
using System;
using System.Text;
using System.Threading.Tasks;
using Vibrant.InfluxDB.Client;
using static SensorConnector.Common.AppSettings;
using static SensorConnector.Common.AppSettings.SensorOutputParser;
using static SensorOutputParser.CommandLineArgsParser.CommandLineArgsParser;
using static SensorOutputParser.SensorOutputParser.SensorOutputParser;

namespace SensorOutputParser
{
    public class Program
    {
        private static ParsedInputParams _parsedInputParams;

        private static InfluxClient _client;

        static int Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task<int> MainAsync(string[] args)
        {
            _client = new InfluxClient(new Uri(InfluxHost));

            await _client.CreateDatabaseAsync(DatabaseName); // creates db if not exist

            try
            {
                #region Test

                // WARNING: Remove or comment out this part for production.
                var testArgs = ExecutionParamsStringExample.Split(' ');
                args = testArgs;

                #endregion Test

                _parsedInputParams = ParseInputParams(args);
            }
            catch (Exception ex)
            {
                var errorMessage = "ERROR: execution params parsing failed.\r\n" + ex.Message;
                Console.WriteLine(errorMessage);

                return 1;
            }

            foreach (var testSensorsInfo in _parsedInputParams.TestSensorsInfo)
            {
                var query = QueryMaker.GetSensorOutputsForTest(
                    testSensorsInfo.TestId,
                    _parsedInputParams.LeftTimeBorder,
                    _parsedInputParams.RightTimeBorder,
                    testSensorsInfo.Sensors);

                var resultSet = await _client.ReadAsync<SensorOutput>(DatabaseName, query);

                var results = resultSet.Results[0];
                var series = results.Series;

                if (series.Count < 1)
                {
                    StringBuilder sensorsToMessageString = new StringBuilder();

                    foreach (var sensor in testSensorsInfo.Sensors)
                    {
                        sensorsToMessageString.Append(sensor + " ");
                    }

                    Console.WriteLine(
                        $"WARNING: For given time borders No outputs were found in Test-{testSensorsInfo.TestId} with sensors: " +
                        $"{sensorsToMessageString}");

                    continue;
                }

                var retrievedOutputs = series[0].Rows;

                try
                {
                    await ParseSensorsDatatypeAsync(testSensorsInfo.Sensors);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                var outputsForExport = ParseRetrievedData(retrievedOutputs);

                var jsonExport = new JsonExport(
                    testSensorsInfo.TestId,
                    _parsedInputParams.LeftTimeBorder,
                    _parsedInputParams.RightTimeBorder,
                    outputsForExport);

                var exportedFile = jsonExport.GetSensorOutputsFile();

                var directoryPath = DefaultDirectoryPath;

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

            return 0;
        }
    }
}