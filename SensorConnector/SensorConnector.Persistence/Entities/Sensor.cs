namespace SensorConnector.Persistence.Entities
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string Metadata { get; set; }
        public int DataTypeId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int CommunicationProtocolId { get; set; }

        public virtual CommunicationProtocol CommunicationProtocol { get; set; }
        public virtual Datatype DataType { get; set; }
    }
}
