using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorOutputParser.CommandLineArgsParser
{
    public class ParsedInputParams
    {
        public int TestId { get; set; }
        public long LeftTimeBorder { get; set; } // in seconds
        public long RightTimeBorder { get; set; } // in seconds
        public List<Sensor> Sensors { get; set; }
    }
}
