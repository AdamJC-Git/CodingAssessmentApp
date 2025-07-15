using CodingAssessmentApp.App;
using CodingAssessmentApp.Logging;
using Microsoft.Extensions.DependencyInjection;
using CodingAssessmentApp.Services;
using CodingAssessmentApp.UserInteraction;

//create basic logger. In real application would consider using Log4Net or Serilog package for logging.
//In a real world application you would use a Singleton instance of Logger for the entire application
ILogger logger = new Logger("LogFile.txt");

//The below httpClientFactory should be configured in Program.cs for .Net Core and injected into application
var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

//Global try catch block
try
{
    //the below AuthService and ProductService should be configured in Program.cs
    //for dependency injection using .Net Core
    var userDataApp = new DummyDataApp(logger, 
                        new ConsoleInteractor(),
                        new AuthService(httpClientFactory),
                        new ProductService(httpClientFactory));
    userDataApp.Run();

}
catch (Exception ex)
{ 
    Console.WriteLine($"An unexpected error has occurred: {ex.Message}: {ex.StackTrace} - {ex.InnerException}");
    //logger.LogError($"{ex.Message}: {ex.StackTrace} - {ex.InnerException}");
}
