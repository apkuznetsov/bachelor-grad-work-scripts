using System;
using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorOutputParser.CommandLineArgsParser
{
    public class ParsedInputParams
    {
        public int TestId { get; set; }
        public DateTime LeftTimeBorder { get; set; }
        public DateTime RightTimeBorder { get; set; }
        public List<Sensor> Sensors { get; set; }
    }
}
