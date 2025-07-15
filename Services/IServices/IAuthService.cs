using CodingAssessmentApp.Models.DTO;

namespace CodingAssessmentApp.Services.IServices
{
    public interface IAuthService
    {
        Task<string> LoginAsync<T>(LoginRequestDTO loginRequestDto);
    }
}
