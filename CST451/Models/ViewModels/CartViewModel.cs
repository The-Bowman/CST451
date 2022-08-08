using CST451.Models.ViewModels.Products;

namespace CST451.Models.ViewModels
{
    public class CartViewModel
    {
        public int? CartID { get; set; }
        public List<Products.ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }

        public CartViewModel() { }

        public CartViewModel(int? cartID, List<ProductViewModel> products, int? customerID, string customerName)
        {
            CartID = cartID;
            Products = products;
            CustomerID = customerID;
            CustomerName = customerName;
        }
    }
}
