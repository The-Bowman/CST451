namespace CST451.Data.DataModel.OrderDataModels
{
    public class OrderDataModel
    {
        public int? OrderID { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerID { get; set; }
        public double? Total { get; set; }

        public OrderDataModel()
        {
        }

        public OrderDataModel(int? orderID, string customerName, int? customerID, double? total)
        {
            OrderID = orderID;
            CustomerName = customerName;
            CustomerID = customerID;
            Total = total;
        }
    }
}
