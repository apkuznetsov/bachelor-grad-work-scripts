namespace SensorConnector.Common.CommonClasses
{
    /// <summary>
    /// Represents sensor with basic information.
    /// </summary>
    public class Sensor
    {
        public string IpAddress { get; set; }

        public int Port { get; set; }

        public Sensor()
        {
        }

        public Sensor(string sensorIpAddress, int sensorPort)
        {
            IpAddress = sensorIpAddress;
            Port = sensorPort;
        }

        public override string ToString()
        {
            return $"{IpAddress}:{Port}";
        }


    }
}
