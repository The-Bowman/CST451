using CST451.Models.ViewModels.Products;

namespace CST451.Models.ViewModels
{
    public class CartViewModel
    {
        public string CartID { get; set; }
        public List<Products.ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public double? Total { get; set; }

        public CartViewModel() { }

        public CartViewModel(string cartID, List<ProductViewModel> products, int? customerID, string customerName)
        {
            CartID = cartID;
            Products = products;
            CustomerID = customerID;
            CustomerName = customerName;
        }

        public double? CalculateTotal()
        {
            double? total = 0;
            if (Products.Count <= 0)
                return 0.00;
            foreach (var item in Products)
            {
                total += item.Price;
            }
            return total;
        }
    }
}
