using SensorConnector.Common.CommonClasses;
using System;
using System.Collections.Generic;

namespace SensorOutputParser.Exporting
{
    public class SensorOutputForExport : Sensor
    {
        public DateTime CollectedAt { get; set; }

        public int SensorId { get; set; }

        public List<SensorFieldValue> ParsedData { get; set; }
    }
}
