using CST451.Models.Orders;
using CST451.Models.ViewModels;
using CST451.Models.ViewModels.Checkout;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CST451.Controllers
{
    public class CheckoutController : Controller
    {

        // allow access to factory
        private Factory _factory;
        internal Factory oFactory
        {
            get
            {
                if (_factory == null)
                    _factory = new Factory();
                return _factory;
            }
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            // setup cart
            CartViewModel cart = GetCart();

            // setup order details
            Models.Orders.OrderViewModel orderViewModel = new Models.Orders.OrderViewModel()
            {
                CustomerID = cart.CustomerID,
                Total = cart.Total,
                CustomerName = cart.CustomerName
            };

            // setup details for each order line
            List<OrderLineViewModel> lines = GetOrderLines(cart);

            // pass in OrderCartModel to view
            CheckoutModel orderCartModel = new CheckoutModel()
            {
                Cart = cart,
                OrderLines = lines,
                Order = orderViewModel
            };
            return View(orderCartModel);
        }

        [HttpPost]
        public IActionResult ProcessCheckout(CheckoutModel model)
        {
            if (model == null)
            {
                return View("Error");
            }
            model.Cart = GetCart();
            model.OrderLines = GetOrderLines(model.Cart);
            model.Order.Total = model.Cart.Total;
            model.CreditCardName = model.CustomerBillingInfo.Name;

            model = oFactory.CheckoutHelper.AddOrder(model);
            return View(model);
        }

        private CartViewModel GetCart()
        {
            CartViewModel cart = oFactory.CartHelper.GetCart(HttpContext.Session.GetString("cart"));
            int customerID;
            cart.CartID = HttpContext.Session.Id;
            cart.CustomerName = HttpContext.Session.GetString("username") ?? "";
            if (int.TryParse(HttpContext.Session.GetString("userID"), out customerID))
                cart.CustomerID = customerID;
            cart.Total = cart.CalculateTotal();
            return cart;
        }

        private List<OrderLineViewModel> GetOrderLines(CartViewModel cart)
        {
            List<Models.Orders.OrderLineViewModel> lines = (from product in cart.Products
                                                            let lineViewModel = new Models.Orders.OrderLineViewModel()
                                                            {
                                                                ProductID = product.ID,
                                                                ProductName = product.Name,
                                                                ProductPrice = product.Price,
                                                                ProductQuantity = 1,
                                                            }
                                                            select lineViewModel).ToList();
            return lines;
        }
    }
}
