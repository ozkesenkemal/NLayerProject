namespace NLayer.Data.Entity
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Heigth { get; set; }
        public int Weigth { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}