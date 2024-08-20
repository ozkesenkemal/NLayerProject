namespace NLayer.Data.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        List<Product> Products { get; set; }
    }
}
