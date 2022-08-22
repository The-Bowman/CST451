namespace CST451.Models.ViewModels.Products
{
    public class ProductViewModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Compatibility { get; set; }
        public double? Price { get; set; }
        public string ImagePath { get; set; }
    }
}
