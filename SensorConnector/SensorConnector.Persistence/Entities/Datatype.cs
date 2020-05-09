using System;
using System.Collections.Generic;
using System.Text;

namespace SensorConnector.Persistence.Entities
{
    public class Datatype
    {
        public int DataTypeId { get; set; }
        public string Metadata { get; set; }
        public string Schema { get; set; }
    }
}
