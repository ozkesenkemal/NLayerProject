namespace NLayer.Data.Dto
{
    public class CategoryWithProductDto : CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
