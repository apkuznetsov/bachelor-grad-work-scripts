using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorOutputParser.SensorOutputParser
{
    public class SensorWithParsedDatatype : Sensor
    {
        public int SensorId { get; set; }
        public List<SensorFieldDescription> FieldDescriptions { get; set; }
    }
}
