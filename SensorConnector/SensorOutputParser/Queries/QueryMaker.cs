using SensorConnector.Common.CommonClasses;
using System;
using System.Collections.Generic;
using System.Text;
using static SensorConnector.Common.AppSettings;

namespace SensorOutputParser.Queries
{
    /// <summary>
    /// Composes SQL-query queries to databases.
    /// </summary>
    public static class QueryMaker
    {
        /// <summary>
        /// Composes SQL-query conditional query to get sensors outputs from Influx database.
        /// </summary>
        /// <param name="testId">Id of the target Test.
        /// Used to compose Influx measurement name where Test related sensors outputs are stored.</param>
        /// <param name="leftTimeBorder">Left border of the allowed time range for a sensor's output time of collecting.</param>
        /// <param name="rightTimeBorder">Right border of the allowed time range for a sensor's output time of collecting.</param>
        /// <param name="targetSensors">List of sensors for which data will be retrieved.
        /// Each sensor info consist of sensor's Ip-address and port.
        /// This information is enough to definitely define sensor in the measurement and filter outputs by required sensors.</param>
        /// <returns>Composed SQL-query as string.</returns>
        public static string GetSensorOutputsForTest(
            int testId,
            DateTime leftTimeBorder,
            DateTime rightTimeBorder,
            List<Sensor> targetSensors)
        {
            var measurementName = MeasurementNameBase + testId;

            var leftTimeBorderStr = leftTimeBorder.ToUniversalTime().ToString("O");
            var rightTimeBorderStr = rightTimeBorder.ToUniversalTime().ToString("O");

            StringBuilder composedQueryString = new StringBuilder(
                                                                  $"SELECT * FROM {measurementName} " +
                                                                  $"WHERE time >= \'{leftTimeBorderStr}\' AND " +
                                                                  $"time <= \'{rightTimeBorderStr}\' AND "
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
