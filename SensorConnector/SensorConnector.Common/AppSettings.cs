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
                "-testId 132 -executionTime 20 -sensors 127.0.0.1:1111 127.0.0.1:2222 127.2.2.2:3333";

            public static int ListenPort = 8888; // local port for listening incoming data
        }
    }
}
