using Microsoft.AspNetCore.Mvc;
using CST451.Models.Users;
using CST451.Models.ViewModels;
using System.Text.Json;

namespace CST451.Controllers
{
    public class Customer : Controller
    {
        // allow access to factory
        private Factory _factory;
        internal Factory _Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new Factory();
                return _factory;
            }
        }

        /// <summary>
        /// View for adding a customer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }


        /// <summary>
        /// Takes in a CustomerViewModel object to add to database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddCustomer(CustomerViewModel customer)
        {
            customer = _Factory.CustomerHelper.AddCustomer(customer);
            return View("AddCustomerResult", customer);
        }

        /// <summary>
        /// View to login customer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Executes login authentication and adds to session
        /// </summary>
        /// <param name="customer">CustomerViewModel</param>
        /// <returns>CustomerViewModel</returns>
        [HttpPost]
        public IActionResult Login(CustomerViewModel customer)
        {
            customer = _Factory.CustomerHelper.Login(customer);
            HttpContext.Session.SetString("username", customer.Name);            
            HttpContext.Session.SetString("userID", customer.ID.ToString());
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new CartViewModel()));
            return View("LoginResult", customer);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            CartViewModel cart;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
            {
                 cart = new CartViewModel();
                return View("Cart", cart);
            }
            cart = JsonSerializer.Deserialize<CartViewModel>(HttpContext.Session.GetString("cart"));
            return View("Cart");
        }
    }
}
