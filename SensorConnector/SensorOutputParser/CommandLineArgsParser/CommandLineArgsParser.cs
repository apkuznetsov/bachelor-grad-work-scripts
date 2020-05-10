using SensorConnector.Common;
using SensorConnector.Common.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using static SensorConnector.Common.AppSettings;
using static SensorConnector.Common.AppSettings.SensorOutputParser;

namespace SensorOutputParser.CommandLineArgsParser
{
    public static class CommandLineArgsParser
    {
        private static readonly ParamParserHelper _paramParserHelper = new ParamParserHelper(InputParamsPattern);
        public static ParsedInputParams ParseInputParams(string[] inputParams)
        {
            ParsedInputParams ParsedInputParams = new ParsedInputParams
            {
                TestSensorsInfo = new List<ParsedTestSensorsInfo>()
            };

            var i = 0;

            if (inputParams == null || !inputParams.Any())
            {
                throw new FormatException(
                    $"No executing params were provided. " +
                    $"Use the following pattern for the program executing: \r\n \'{InputParamsPattern}\'");
            }

            _paramParserHelper.CheckParamPassed(inputParams, i, directoryPathParamName);

            i++;

            _paramParserHelper.CheckParamValuePassed(inputParams, i, directoryPathParamName);

            ParsedInputParams.DirectoryPath = inputParams[i];

            i++;

            _paramParserHelper.CheckParamPassed(inputParams, i, leftTimeBorderParamName);

            i++;

            if (!DateTime.TryParse(inputParams[i], out var leftTimeBorder))
            {
                throw new FormatException(
                    $"Provided value \'{inputParams[i]}\' is not a valid DateTime value.\r\n" +
                    $"Provide time borders in ISO-8601 format. Example: 2020-05-10T07:32:29Z");
            }

            ParsedInputParams.LeftTimeBorder = leftTimeBorder;

            i++;

            _paramParserHelper.CheckParamPassed(inputParams, i, rightTimeBorderParamName);

            i++;

            _paramParserHelper.CheckParamValuePassed(inputParams, i, rightTimeBorderParamName);

            if (!DateTime.TryParse(inputParams[i], out var rightTimeBorder))
            {
                throw new FormatException(
                    $"Provided value \'{inputParams[i]}\' is not a valid DateTime value.\r\n" +
                    $"Provide time borders in ISO-8601 format. Example: 2020-05-10T07:32:29Z");
            }


            ParsedInputParams.RightTimeBorder = rightTimeBorder;

            i++;

            var testSensorsInfoIndex = 0;
            do
            {
                try
                {
                    _paramParserHelper.CheckParamPassed(inputParams, i, testIdParamName);
                }
                catch (FormatException e)
                {
                    if (testSensorsInfoIndex == 0)
                    {
                        throw e;
                    }

                    throw new FormatException(
                        "Expected either  \'{sensorIpAddress}:{sensorPort}\' sensor info or \r\n" +
                        $"\'{testIdParamName}\' parameter, but found {inputParams[i]}");
                }

                i++;

                _paramParserHelper.CheckParamValuePassed(inputParams, i, testIdParamName);

                if (!int.TryParse(inputParams[i], out var testId))
                {
                    throw new FormatException(
                        $"Provided value \'{inputParams[i]}\' is not a valid TestId, because it can not be parsed to int.");
                }

                ParsedInputParams.TestSensorsInfo.Add(
                    new ParsedTestSensorsInfo()
                    {
                        TestId = Math.Abs(testId)
                    });

                i++;

                _paramParserHelper.CheckParamPassed(inputParams, i, sensorsParamName);

                i++;

                var isFirstSensorInput = true;
                do
                {

                    try
                    {
                        _paramParserHelper.CheckParamValuePassed(inputParams, i, sensorsParamName);
                    }
                    catch (FormatException e)
                    {
                        if (isFirstSensorInput)
                        {
                            throw e;
                        }

                        break;
                    }

                    Sensor parsedSensor = null;

                    try
                    {
                        parsedSensor = _paramParserHelper.ParseSensorFromInput(inputParams[i]);
                    }
                    catch (FormatException e)
                    {
                        if (isFirstSensorInput)
                        {
                            throw e;
                        }

                        break;
                    }

                    ParsedInputParams.TestSensorsInfo[testSensorsInfoIndex].Sensors.Add(parsedSensor);

                    i++;
                    isFirstSensorInput = false;

                } while (i < inputParams.Length);

                testSensorsInfoIndex++;

            } while (i < inputParams.Length);

            return ParsedInputParams;
        }
    }
}
