using CodingAssessmentApp.UserInteraction.IUserInteraction;

namespace CodingAssessmentApp.UserInteraction
{
    public class ConsoleInteractor : IUserInteractor
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
