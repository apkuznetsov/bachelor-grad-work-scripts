using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorOutputParser.CommandLineArgsParser
{
    /// <summary>
    /// Represents Test id with associated sensors parsed from app-execution input params.
    /// </summary>
    public class ParsedTestSensorsInfo
    {
        public int TestId { get; set; }
        public List<Sensor> Sensors { get; set; }
        
        public ParsedTestSensorsInfo()
        {
            Sensors = new List<Sensor>();
        }
    }
}
