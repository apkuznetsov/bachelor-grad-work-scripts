using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SensorConnector.CommandLineArgsParser
{
    public static class CommandLineArgsParser
    {
        private static readonly string _testIdParamName = "-testId";
        private static readonly string _executionTimeParamName = "-executionTime";
        private static readonly string _sensorsParamName = "-sensors";

        private const int MAX_EXECUTION_TIME = 3600; // in seconds (1 hour)

        private const int MIN_PORT_VALUE = 1;
        private const int MAX_PORT_VALUE = 65535;

        // assuming input pattern is:
        // -testId {testId} -executionTime {executionTime} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}]
        private const string PARAMS_PATTERN = "-testId {testId} -executionTime {executionTime} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}]";

        public static ParsedParamsDto ParseInputParams(string[] inputParams)
        {
            int testId;
            int executionTime;
            List<Sensor> sensors = new List<Sensor>();

            if (inputParams == null || !inputParams.Any())
            {
                throw new FormatException(
                    $"No executing params were provided. " +
                    $"Use the following pattern for the program executing: \r\n \'{PARAMS_PATTERN}\'");
            }

            if (!inputParams[0].Equals(
                _testIdParamName,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FormatException(
                    $"Expected \'{_testIdParamName}\' but found \'{inputParams[0]}\'.");
            }

            if (!int.TryParse(inputParams[1], out testId))
            {
                throw new FormatException(
                    $"Provided value \'{inputParams[1]}\' is not a valid TestId, because it can not be parsed to int.");
            }

            testId = Math.Abs(testId);

            if (!inputParams[2].Equals(
                _executionTimeParamName,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FormatException(
                    $"Expected \'{_executionTimeParamName}\' but found \'{inputParams[2]}\'.");
            }

            if (!int.TryParse(inputParams[3], out executionTime))
            {
                throw new FormatException(
                    $"Provided value \'{inputParams[3]}\' is not a valid execution time, because it can not be parsed to int.");
            }

            executionTime = Math.Abs(executionTime);

            if (executionTime > MAX_EXECUTION_TIME)
            {
                throw new FormatException(
                    $"Provided value {executionTime}sec. for execution time is too large. " +
                    $"Max allowed value is {MAX_EXECUTION_TIME}sec. (1 hour).");
            }


            if (!inputParams[4].Equals(
                _sensorsParamName,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FormatException(
                    $"Expected \'{_sensorsParamName}\' but found \'{inputParams[4]}\'.");
            }

            var i = 5;

            while (i < inputParams.Length)
            {
                var splitSensorInfo = inputParams[i].Split(':');

                if (splitSensorInfo.Length != 2)
                {
                    throw new FormatException(
                        $"Provided value \'{inputParams[i]}\' is not a valid sensor info. " +
                        "Expected pattern is \'{sensorIpAddress:sensorPort}\'");
                }

                var sensorIpAddress = splitSensorInfo[0];
                var sensorPortStr = splitSensorInfo[1];

                if (!IPAddress.TryParse(sensorIpAddress, out _))
                {
                    throw new FormatException(
                        $"Provided value \'{sensorIpAddress}\' is not a valid sensor IP-address.");
                }

                if (!int.TryParse(sensorPortStr, out var sensorPort))
                {
                    throw new FormatException(
                        $"Provided value \'{sensorPortStr}\' is not a valid sensor port, because it can not be parsed to int.");
                }

                sensorPort = Math.Abs(sensorPort);

                if (sensorPort < MIN_PORT_VALUE || sensorPort > MAX_PORT_VALUE)
                {
                    throw new FormatException(
                        $"Provided value \'{sensorPort}\' for sensor port is not allowed. " +
                        $"Allowed values are from {MIN_PORT_VALUE} to {MAX_PORT_VALUE}");
                }

                sensors.Add(new Sensor(sensorIpAddress, sensorPort));

                i++;
            }

            return new ParsedParamsDto()
            {
                TestId = testId,
                ProgramExecutionTime = executionTime,
                Sensors = sensors
            };
        }
    }
}
