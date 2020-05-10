using System;
using System.Collections.Generic;

namespace SensorOutputParser.CommandLineArgsParser
{
    /// <summary>
    /// Represents all params parsed from app-execution input params.
    /// </summary>
    public class ParsedInputParams
    {
        public string DirectoryPath { get; set; }
        public DateTime LeftTimeBorder { get; set; }
        public DateTime RightTimeBorder { get; set; }
        public List<ParsedTestSensorsInfo> TestSensorsInfo { get; set; }

        public ParsedInputParams()
        {
            TestSensorsInfo = new List<ParsedTestSensorsInfo>();
        }
    }
}
