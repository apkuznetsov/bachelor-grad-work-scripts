using SensorConnector.Common.CommonClasses;
using System.Collections.Generic;
using System.Text;
using static SensorConnector.Common.AppSettings;

namespace SensorOutputParser.Queries
{
    public static class QueryMaker
    {
        public static string GetSensorOutputsForTest(
            int testId,
            long leftTimeBorder,
            long rightTimeBorder,
            List<Sensor> targetSensors)
        {
            var measurementName = MeasurementNameBase + testId;

            StringBuilder composedQueryString = new StringBuilder(
                                                                  $"SELECT * FROM {measurementName} " +
                                                                  $"WHERE time >= {leftTimeBorder} AND " +
                                                                  $"time <= {rightTimeBorder} AND "
                                                                  );

            for (int i = 0; i < targetSensors.Count; i++)
            {
                var sensorIp = targetSensors[i].IpAddress;
                var sensorPort = targetSensors[i].Port;

                composedQueryString.Append(
                    $"sensor_ip =~ /^{sensorIp}$/ AND " +
                    $"sensor_port =~ /^{sensorPort}$/"
                );

                if (i < targetSensors.Count - 1)
                {
                    composedQueryString.Append(" OR ");
                }
            }

            return composedQueryString.ToString();
        }
    }
}
