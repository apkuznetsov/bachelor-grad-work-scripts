using System;
using System.IO;

namespace SensorOutputParser
{
    /// <summary>
    /// Writes array of bytes too the given directory path.
    /// </summary>
    public static class FileWriter
    {
        public static void WriteFileToDirectory(string directoryPath, byte[] fileContents, string fileName)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var path = $"{directoryPath}\\{fileName}";

            File.WriteAllBytes(path, fileContents);
        }
    }
}
