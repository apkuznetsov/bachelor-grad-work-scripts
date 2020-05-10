using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorOutputParser.SensorOutputParser
{
    /// <summary>
    /// Represents sensor with parsed associated datatype.
    /// </summary>
    public class SensorWithParsedDatatype : Sensor
    {
        public int SensorId { get; set; }
        public List<SensorFieldDescription> FieldDescriptions { get; set; }

        public SensorWithParsedDatatype()
        {
            FieldDescriptions = new List<SensorFieldDescription>();
        }
    }
}
