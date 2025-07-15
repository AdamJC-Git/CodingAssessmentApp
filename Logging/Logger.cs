namespace CodingAssessmentApp.Logging
{
    public class Logger : ILogger
    {
        private readonly string _logFileName;

        public Logger(string logFileName)
        {
            _logFileName = logFileName;
        }

        public void LogError(Exception ex)
        {
            var entry = $@"[{DateTime.Now}] - Error Exception message: {ex.Message}: Stack trace: {ex.StackTrace}";
            File.AppendAllText(_logFileName, entry);
        }

        public void LogInformation(string message)
        {
            var entry = $@"[{DateTime.Now}] - Information: {message}";
            File.AppendAllText(_logFileName, entry);
        }

        public void LogWarning(string message)
        {
            var entry = $@"[{DateTime.Now}] - Warning: {message}";
            File.AppendAllText(_logFileName, entry);
        }
    }
}
