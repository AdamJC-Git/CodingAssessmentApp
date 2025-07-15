using CodingAssessmentApp.Models;
using CodingAssessmentApp.Models.DTO;
using CodingAssessmentApp.Services.IServices;

namespace CodingAssessmentApp.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClient;
        private string _baseUrl;
        public AuthService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;

            //have hardcoded base URL address and addresses to access API. In real world application, these settings should
            // be kept and retrieved from a Configuration file like appsettings.json
            _baseUrl = "https://dummyjson.com";
        }
        public Task<string> LoginAsync<T>(LoginRequestDTO loginRequestDto)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = ApiType.POST,
                Url = Path.Join(_baseUrl, "/auth/login"),
                Data = loginRequestDto
            };
            return SendAsync<string>(apiRequest);
        }
    }
}
