namespace SensorOutputParser.SensorOutputParser
{
    /// <summary>
    /// Represents key-value pair of a Json-datatype schema.
    /// </summary>
    public class SensorFieldDescription
    {
        public string FieldName { get; set; }
        public string FieldTypeName { get; set; }

        public SensorFieldDescription(string fieldName, string fieldTypeName)
        {
            FieldName = fieldName;
            FieldTypeName = fieldTypeName;
        }
    }
}
