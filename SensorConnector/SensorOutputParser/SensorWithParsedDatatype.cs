using System.Collections.Generic;
using SensorConnector.Common.CommonClasses;

namespace SensorOutputParser
{
    public class SensorWithParsedDatatype : Sensor
    {
        public int SensorId { get; set; }
        public List<SensorFieldDescription> FieldDescriptions { get; set; }
    }
}
