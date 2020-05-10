namespace SensorOutputParser.Exporting
{
    public class SensorFieldValue
    {
        /// <summary>
        /// Represents sensor output parsed data value with arbitrary type.
        /// </summary>
        public string FieldName { get; set; }
        public object FieldValue { get; set; }
    }
}
