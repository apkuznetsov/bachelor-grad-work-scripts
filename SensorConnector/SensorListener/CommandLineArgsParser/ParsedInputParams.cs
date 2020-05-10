using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;

namespace SensorListener.CommandLineArgsParser
{
    public class ParsedInputParams
    {
        /// <summary>
        /// Represents all params parsed from app-execution input params.
        /// </summary>
        public int TestId { get; set; }
        public int ProgramExecutionTime { get; set; } // In seconds
        public List<Sensor> Sensors { get; set; }

        public ParsedInputParams()
        {
            Sensors = new List<Sensor>();
        }
    }
}
