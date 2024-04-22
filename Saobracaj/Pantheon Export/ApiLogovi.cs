using System;
using System.IO;

namespace Saobracaj.Pantheon_Export
{
    internal class ApiLogovi
    {
        private static readonly string LogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ApiLogovi.txt");
        private static StreamWriter logWriter;
        static ApiLogovi()
        {
            InitializeLogFile();
        }
        private static void InitializeLogFile()
        {
            try
            {
                logWriter = File.AppendText(LogFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing log file: {ex.Message}");
            }
        }
        public static void Log(string tip, string id, string request, string response)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(LogFilePath))
                {
                    string logEntry = $"{tip}-{id}\n{request}\n{response}\n{DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log: {ex.Message}");
            }
        }
        public static void Save()
        {
            try
            {
                if (logWriter != null)
                {
                    logWriter.Flush();
                    logWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving log file: {ex.Message}");
            }
        }
    }
}
