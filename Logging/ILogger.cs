namespace CodingAssessmentApp.Logging
{
    public interface ILogger
    {
        public void LogInformation(string message);
        public void LogError(Exception ex);
        public void LogWarning(string message);
    }
}
