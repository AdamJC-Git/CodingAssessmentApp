using CodingAssessmentApp.Models;

namespace CodingAssessmentApp.Services.IServices
{
    public interface IBaseService
    {
        public Task<string> SendAsync<T>(APIRequest aPIRequest);
    }
}
