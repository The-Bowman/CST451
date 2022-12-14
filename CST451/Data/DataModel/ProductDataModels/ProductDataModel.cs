namespace CST451.Data.DataModel.ProductDataModels
{
    public class ProductDataModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Compatibility { get; set; }
        public double? Price { get; set; }
        public bool? Success { get; set; }
        public string ImagePath { get; set; }

        public ProductDataModel() { }

        public ProductDataModel(int? iD, string name, string description, int? compatibility, double? price, string imagePath)
        {
            ID = iD;
            Name = name;
            Description = description;
            Compatibility = compatibility;
            Price = price;
            ImagePath = imagePath;
        }
    }
}
