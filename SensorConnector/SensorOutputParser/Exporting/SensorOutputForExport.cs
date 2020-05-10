using SensorConnector.Common.CommonClasses;
using System;
using System.Collections.Generic;

namespace SensorOutputParser.Exporting
{
    /// <summary>
    /// Represents sensor output with data parsed according to sensor datatype.
    /// </summary>
    public class SensorOutputForExport : Sensor
    {
        public DateTime CollectedAt { get; set; }

        public int SensorId { get; set; }

        public List<SensorFieldValue> ParsedData { get; set; }

        public SensorOutputForExport()
        {
            ParsedData = new List<SensorFieldValue>();
        }
    }
}
