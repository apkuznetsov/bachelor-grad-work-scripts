using System.Collections.Generic;

namespace SensorConnector.Persistence.Entities
{
    public class Datatype
    {
        public Datatype()
        {
            Sensors = new HashSet<Sensor>();
        }

        public int DataTypeId { get; set; }
        public string Metadata { get; set; }
        public string Schema { get; set; }

        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
