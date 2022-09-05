using Microsoft.AspNetCore.Mvc;
using CST451.Models.ViewModels;
using System.Text.Json;
using CST451.Models.ViewModels.Products;

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
            CartViewModel cart = oFactory.CartHelper.GetCart(HttpContext.Session.GetString("cart"));
            int customerID;
            cart.CartID = HttpContext.Session.Id;
            cart.CustomerName = HttpContext.Session.GetString("username");
            if (int.TryParse(HttpContext.Session.GetString("userID"), out customerID))
                cart.CustomerID = customerID;
            cart.Total = cart.CalculateTotal();
            return View(cart);
        }

        /// <summary>
        /// Remove one product form the cart
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RemoveFromCart(int productID)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                ViewBag.Message = "You must be signed in to perform this action";
                return View("BrowseAll", oFactory.ProductHelper.GetAll());
            }
            ProductViewModel productToRemove = oFactory.ProductHelper.GetOne(new ProductViewModel() { ID = productID });
            CartViewModel cart = oFactory.CartHelper.GetCart(HttpContext.Session.GetString("cart"));
            ProductViewModel productInCart = cart.Products.Where(p => p.ID == productID).FirstOrDefault();  
            cart.Products.Remove(productInCart);
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
            ViewBag.Message = "Item removed from cart successfully";
            return View("ViewCart", cart);
        }
    }
}
