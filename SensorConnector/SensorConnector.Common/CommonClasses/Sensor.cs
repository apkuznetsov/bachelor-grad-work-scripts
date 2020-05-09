namespace SensorConnector.Common.CommonClasses
{
    public class Sensor
    {
        public Sensor(string sensorIpAddress, int sensorPort)
        {
            IpAddress = sensorIpAddress;
            Port = sensorPort;
        }

        public string IpAddress { get; set; }

        public int Port { get; set; }
    }
}
