using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;
using System.Text;

namespace SensorConnector.Common.SensorExtensions
{
    /// <summary>
    /// Contains extension methods for the basic Sensor entity.
    /// </summary>
    public static class SensorExtensions
    {
        public static string ListOfSensorsToString(this List<Sensor> sensors)
        {
            StringBuilder sensorsStringBuilder = new StringBuilder();

            foreach (var sensor in sensors)
            {
                sensorsStringBuilder.Append(sensor + " ");
            }

            sensorsStringBuilder.Remove(sensorsStringBuilder.Length - 1, 1);

            return sensorsStringBuilder.ToString();
        }
    }
}
