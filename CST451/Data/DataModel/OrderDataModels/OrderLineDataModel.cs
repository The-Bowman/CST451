namespace CST451.Data.DataModel.OrderDataModels
{
    public class OrderLineDataModel
    {
        public int? LineID { get; set; }
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public int? ProductQuantity { get; set; }

        public OrderLineDataModel()
        {
        }

        public OrderLineDataModel(int? lineID, int? orderID, int? productID, string productName, double? productPrice, int? productQuantity)
        {
            LineID = lineID;
            OrderID = orderID;
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
        }
    }
}
