using CST451.Models.ViewModels;
using CST451.Models.ViewModels.Checkout;
using Microsoft.AspNetCore.Mvc;

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
            CartViewModel cart = oFactory.CartHelper.GetCart(HttpContext.Session.GetString("cart"));
            int customerID;
            cart.CartID = HttpContext.Session.Id;
            cart.CustomerName = HttpContext.Session.GetString("username") ?? "";
            if (int.TryParse(HttpContext.Session.GetString("userID"), out customerID))
                cart.CustomerID = customerID;
            cart.Total = cart.CalculateTotal();


            // setup order details
            Models.Orders.OrderViewModel orderViewModel = new Models.Orders.OrderViewModel()
            {
                CustomerID = customerID,
                Total = cart.Total,
                CustomerName = cart.CustomerName
            };


            // setup details for each order line
            List<Models.Orders.OrderLineViewModel> lines = new List<Models.Orders.OrderLineViewModel>();
            foreach( var product in cart.Products)
            {
                Models.Orders.OrderLineViewModel lineViewModel = new Models.Orders.OrderLineViewModel()
                {
                    ProductID = product.ID,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                };
                lines.Add(lineViewModel);
            }

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
            return View();
        }
    }
}
