using CodingAssessmentApp.Models.DTO;

namespace CodingAssessmentApp.Services.IServices
{
    public interface IProductService
    {
        Task<string> GetThreeMostExpensivePhonesAsync<T>(string token);
        Task<string> UpdateThreeMostExpensivePhonesAsync<T>(string token, int productId, ProductPriceDTO productDto);
    }
}
