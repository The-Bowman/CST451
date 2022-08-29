using Microsoft.AspNetCore.Mvc;
using CST451.Models.ViewModels;
using System.Text.Json;

namespace CST451.Controllers
{
    public class CartController : Controller
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

        /// <summary>
        /// View to display cart from session
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ViewCart()
        {             
            CartViewModel cartViewModel = oFactory.CartHelper.GetCart(HttpContext.Session.GetString("cart"));
            cartViewModel.CartID = HttpContext.Session.Id;
            cartViewModel.CustomerName = HttpContext.Session.GetString("username");
            cartViewModel.CustomerID = int.Parse(HttpContext.Session.GetString("userID"));
            cartViewModel.Total = cartViewModel.CalculateTotal();
            return View(cartViewModel);
        }
    }
}
