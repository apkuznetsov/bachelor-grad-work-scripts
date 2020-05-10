using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorOutputParser.CommandLineArgsParser
{
    public class ParsedTestSensorsInfo
    {
        public ParsedTestSensorsInfo()
        {
            Sensors = new List<Sensor>();
        }

        public int TestId { get; set; }
        public List<Sensor> Sensors { get; set; }
    }
}
