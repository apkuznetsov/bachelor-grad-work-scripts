using System.Collections.Generic;

namespace SensorConnector.CommandLineArgsParser
{
    public class ParsedParamsDto
    {
        public int TestId { get; set; }
        public int ProgramExecutionTime { get; set; } // in seconds
        public List<Sensor> Sensors { get; set; }
    }
}
