namespace CodingAssessmentApp.Models.DTO
{   

    public class ProductsResponseDTO
    {
        public List<ProductDTO> products { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }

}
