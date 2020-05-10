using System;

namespace SensorConnector.Common
{
    /// <summary>
    /// Stores all constants and settings.
    /// </summary>
    public static class AppSettings
    {
        public static string InfluxHost = "http://localhost:8086";

        public static string DatabaseName = "dms_influx_db";
        public static string MeasurementNameBase = "sensor_outputs_test_";

        public static readonly string testIdParamName = "-testId";
        public static readonly string sensorsParamName = "-sensors";

        public static readonly int minPortValue = 1;
        public static readonly int maxPortValue = 65535;

        /// <summary>
        /// Stores constants and settings only related to the SensorListener app.
        /// </summary>
        public static class SensorListener
        {
            public static int ListenPort = 8888; // local port for listening incoming data

            public static readonly string executionTimeParamName = "-executionTime";

            public static readonly int maxExecutionTime = 3600; // in seconds (1 hour)

            /// <summary>
            ///  Assuming input pattern is: <br/>
            /// -testId {testId} -executionTime {executionTime} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}]
            /// </summary>
            public static string InputParamsPattern =
                "-testId {testId} -executionTime {executionTime} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}]";

            public static string ExecutionParamsStringExample =
                "-testId 555 -executionTime 2000 -sensors 127.0.0.1:1111 127.0.0.1:2222";
        }

        /// <summary>
        /// Stores constants and settings only related to the SensorOutputParser app.
        /// </summary>
        public static class SensorOutputParser
        {
            public static readonly string directoryPathParamName = "-directoryPath";
            public static readonly string leftTimeBorderParamName = "-leftTimeBorder";
            public static readonly string rightTimeBorderParamName = "-rightTimeBorder";

            public static string DefaultConnectionString = "Host=localhost;Port=5432;Database=MyDb;Username=postgres;Password=kurepin";
            public static string DefaultDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "parsed-files";

            /// <summary>
            /// Assuming input pattern is: <br/>
            ///  -directoryPath {directoryPath}  -leftTimeBorder {leftTimeBorder} -rightTimeBorder {rightTimeBorder} <br />
            /// -testId {testId} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}] [-testId {testId} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}]] <br/>
            /// <br/>
            /// Maybe we should also pass here Influx and Postgres connection details: <br/>
            ///  -infHost {influxHost} -infDb {influxDbName} -pgHost {pgHost} -pgPort {pgPort} -pgDb {pgDbName} -pgSchema {pgSchemaName} -pgUser {pgUsername} -pgPassword {pgPassword} <br/>
            /// + base pattern ...
            /// </summary>
            public static string InputParamsPattern = "-directoryPath {directoryPath}  -leftTimeBorder {leftTimeBorder} -rightTimeBorder {rightTimeBorder}\r\n" +
                                                      "-testId {testId} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}]\r\n" +
                                                      "[-testId {testId} -sensors {sensorIpAddress:sensorPort} [{sensorIp:sensorPort}]]";

            public static string ExecutionParamsStringExample =
                "-directoryPath path " +
                "-leftTimeBorder 2020-05-01T07:32:29Z -rightTimeBorder 2020-05-11T19:32:29Z " +
                "-testId 132 -sensors 127.0.0.1:1111 127.0.0.1:2222 " +
                "-testId 555 -sensors 127.0.0.1:1111 127.0.0.1:2222";

            public static class AllowedTypeNames
            {
                public const string Int = "int";
                public const string Double = "double";
                public const string String = "string";
                public const string Bool = "bool";
            }
        }
    }
}
