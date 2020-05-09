using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorListener.CommandLineArgsParser
{
    public class ParsedInputParams
    {
        public int TestId { get; set; }
        public int ProgramExecutionTime { get; set; } // in seconds
        public List<Sensor> Sensors { get; set; }
    }
}
