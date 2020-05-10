namespace SensorOutputParser.Exporting
{
    /// <summary>
    /// Represents file created from sensor outputs with parsed data.
    /// </summary>
    public class ExportedSensorOutputsFile
    {
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
