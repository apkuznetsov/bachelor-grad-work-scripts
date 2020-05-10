namespace SensorOutputParser.SensorOutputParser
{
    public class SensorFieldDescription
    {
        public SensorFieldDescription(string fieldName, string fieldTypeName)
        {
            FieldName = fieldName;
            FieldTypeName = fieldTypeName;
        }

        public string FieldName { get; set; }
        public string FieldTypeName { get; set; }
    }
}
