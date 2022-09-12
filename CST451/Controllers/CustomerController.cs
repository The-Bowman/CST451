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
            customer = oFactory.CustomerHelper.AddCustomer(customer);
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
            customer = oFactory.CustomerHelper.Login(customer);

            if (!(bool)customer.Success)
            {
                ViewBag.Message = "Login Failed - Please Try Again";
                return View("LoginResult", customer);
            }

            HttpContext.Session.SetString("username", customer.Name);
            HttpContext.Session.SetString("userID", customer.ID.ToString());
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new CartViewModel()));
            return View("LoginResult", customer);
        }

        /// <summary>
        /// Remove session keys and destroy cart
        /// </summary>
        /// <returns>Index page</returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("userID");
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Return cart from session key
        /// and parse into View object
        /// then pass object to view
        /// </summary>
        /// <returns>ViewCart view</returns>
        [HttpGet]
        public IActionResult GetCart()
        {
            CartViewModel cart;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
            {
                cart = new CartViewModel();
            }
            else
            {
                cart = JsonSerializer.Deserialize<CartViewModel>(HttpContext.Session.GetString("cart"));
            }

            return View("Cart", cart);
        }

        /// <summary>
        /// Edit customer data in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditCustomer()
        {
            CustomerViewModel customer = new CustomerViewModel()
            {
                ID = Convert.ToInt32(HttpContext.Session.GetString("userID"))
            };
            customer = oFactory.CustomerHelper.GetOne(customer);
            return View(customer);
        }
    }
}
