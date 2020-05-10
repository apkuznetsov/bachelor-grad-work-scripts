using System;
using System.Collections.Generic;

namespace SensorOutputParser.CommandLineArgsParser
{
    public class ParsedInputParams
    {
        public ParsedInputParams()
        {
            TestSensorsInfo = new List<ParsedTestSensorsInfo>();
        }

        public string DirectoryPath { get; set; }
        public DateTime LeftTimeBorder { get; set; }
        public DateTime RightTimeBorder { get; set; }
        public List<ParsedTestSensorsInfo> TestSensorsInfo { get; set; }

    }
}
