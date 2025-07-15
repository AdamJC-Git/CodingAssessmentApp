using CodingAssessmentApp.Models;
using CodingAssessmentApp.Models.DTO;
using CodingAssessmentApp.Services.IServices;

namespace CodingAssessmentApp.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _baseUrl;
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;

            //have hardcoded base URL address and addresses to access API. In real world application, these settings should
            //be kept and retrieved from a Configuration file like appsettings.json
            _baseUrl = "https://dummyjson.com";
        }
        public Task<string> GetThreeMostExpensivePhonesAsync<T>(string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = Path.Join(_baseUrl, "/auth/products/category/smartphones?limit=3&sortBy=price&order=desc&select=brand,title,price"),
                Token = token
            };
            return SendAsync<string>(apiRequest);            
        }

        public Task<string> UpdateThreeMostExpensivePhonesAsync<T>(string token, int productId, ProductPriceDTO priceDto)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = ApiType.PUT,
                Url = Path.Join(_baseUrl, "/auth/products/", productId.ToString()),
                Data = priceDto,
                Token = token
            };
            return SendAsync<string>(apiRequest);
        }
    }
}
