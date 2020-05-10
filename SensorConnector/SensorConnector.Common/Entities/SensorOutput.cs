using System;
using Vibrant.InfluxDB.Client;

namespace SensorConnector.Common.Entities
{
    /// <summary>
    /// Represents data access object (DAO) to interact with Influx database.
    /// </summary>
    public class SensorOutput
    {
        [InfluxTimestamp]
        public DateTime Timestamp { get; set; }

        [InfluxTag("sensor_ip")]
        public string SensorIpAddress { get; set; }

        [InfluxTag("sensor_port")]
        public int SensorPort { get; set; }

        [InfluxField("data")]
        public string Data { get; set; }
    }
}
