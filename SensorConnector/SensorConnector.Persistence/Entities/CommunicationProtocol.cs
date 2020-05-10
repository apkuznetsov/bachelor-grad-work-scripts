using System.Collections.Generic;

namespace SensorConnector.Persistence.Entities
{
    public class CommunicationProtocol
    {
        public CommunicationProtocol()
        {
            Sensors = new HashSet<Sensor>();
        }

        public int CommunicationProtocolId { get; set; }
        public string ProtocolName { get; set; }

        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
