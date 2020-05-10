using SensorConnector.Common;
using SensorConnector.Common.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using static SensorConnector.Common.AppSettings;
using static SensorConnector.Common.AppSettings.SensorListener;

namespace SensorListener.CommandLineArgsParser
{
    public static class CommandLineArgsParser
    {
        private static readonly ParamParserHelper _paramParserHelper = new ParamParserHelper(InputParamsPattern);

        public static ParsedInputParams ParseInputParams(string[] inputParams)
        {
            int testId;
            int executionTime;
            List<Sensor> sensors = new List<Sensor>();

            var i = 0;

            if (inputParams == null || !inputParams.Any())
            {
                throw new FormatException(
                    $"No executing params were provided. " +
                    $"Use the following pattern for the program executing: \r\n \'{InputParamsPattern}\'");
            }

            _paramParserHelper.CheckParamPassed(inputParams, i, testIdParamName);

            i++;

            _paramParserHelper.CheckParamValuePassed(inputParams, i, testIdParamName);


            if (!int.TryParse(inputParams[1], out testId))
            {
                throw new FormatException(
                    $"Provided value \'{inputParams[1]}\' is not a valid TestId, because it can not be parsed to int.");
            }

            testId = Math.Abs(testId);

            i++;

            _paramParserHelper.CheckParamPassed(inputParams, i, executionTimeParamName);

            i++;

            _paramParserHelper.CheckParamValuePassed(inputParams, i, executionTimeParamName);

            if (!int.TryParse(inputParams[3], out executionTime))
            {
                throw new FormatException(
                    $"Provided value \'{inputParams[3]}\' is not a valid execution time, because it can not be parsed to int.");
            }

            executionTime = Math.Abs(executionTime);

            if (executionTime > maxExecutionTime)
            {
                throw new FormatException(
                    $"Provided value {executionTime}sec. for execution time is too large. " +
                    $"Max allowed value is {maxExecutionTime}sec. (1 hour).");
            }

            i++;

            _paramParserHelper.CheckParamPassed(inputParams, i, sensorsParamName);

            i++;

            while (i < inputParams.Length)
            {
                _paramParserHelper.CheckParamValuePassed(inputParams, i, sensorsParamName);
                var parsedSensor = _paramParserHelper.ParseSensorFromInput(inputParams[i]);

                sensors.Add(parsedSensor);

                i++;
            }

            return new ParsedInputParams()
            {
                TestId = testId,
                ProgramExecutionTime = executionTime,
                Sensors = sensors
            };
        }
    }
}
