using System;
using System.Collections.Generic;
using SensorConnector.Common.CommonClasses;

namespace SensorOutputParser.Exporting
{
    public class SensorOutputForExport : Sensor
    {
        public DateTime CollectedAt { get; set; }

        public int SensorId { get; set; }

        public List<SensorFieldValue> ParsedData { get; set; }
    }
}
