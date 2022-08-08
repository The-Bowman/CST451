namespace CST451.Models.Orders
{
    public class OrderViewModel
    {
        public int? OrderID { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerID { get; set; }
        public double? Total { get; set; }

        public OrderViewModel()
        {
        }

        public OrderViewModel(int? orderID, string customerName, int? customerID, double? total)
        {
            OrderID = orderID;
            CustomerName = customerName;
            CustomerID = customerID;
            Total = total;
        }
    }
}
