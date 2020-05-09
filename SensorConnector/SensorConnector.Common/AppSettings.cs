namespace SensorConnector.Common
{
    public static class AppSettings
    {
        public static string InfluxHost = "http://localhost:8086";

        public static string DatabaseName = "dms_influx_db";
        public static string MeasurementNameBase = "sensor_outputs_test_";

        public static class SensorListener
        {
            public static string ExecutionParamsStringExample =
                "-testId 555 -executionTime 2000 -sensors 127.0.0.1:1111";

            public static int ListenPort = 8888; // local port for listening incoming data
        }

        public static class SensorOutputParser
        {
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
