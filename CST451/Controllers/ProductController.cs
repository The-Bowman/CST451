using Microsoft.AspNetCore.Mvc;
using CST451.Models.ViewModels;
using CST451.Models.ViewModels.Products;
using System.Text.Json;
using Microsoft.CodeAnalysis;

namespace CST451.Controllers
{
    public class ProductController : Controller
    {
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
        /// Displays all Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BrowseAll()
        {
            List<ProductViewModel> products = oFactory.ProductHelper.GetAll();
            return View(products);
        }

        /// <summary>
        /// Adds Product to session cart if user is logged in.
        /// Alerts user to login if they are not
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IActionResult AddToCart(int productID)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                ViewBag.Message = "You must be signed in to perform this action";
                return View("BrowseAll", oFactory.ProductHelper.GetAll());
            }
            ProductViewModel product = oFactory.ProductHelper.GetOne(new ProductViewModel() { ID = productID });
            CartViewModel cart = oFactory.CartHelper.GetCart(HttpContext.Session.GetString("cart"));
            cart.Products.Add(product);
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));
            ViewBag.Message = "Item added to cart successfully";
            return View("BrowseAll", oFactory.ProductHelper.GetAll());
        }

        /// <summary>
        /// Form to Add a product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        /// <summary>
        /// Processes POST method to add a product to DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel product)
        {
            product = oFactory.ProductHelper.AddProduct(product);
            return View(product);
        }

        /// <summary>
        /// Form to find a product by Product ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FindProductBySKU()
        {
            return View();
        }

        /// <summary>
        /// Form for product edit to DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditProduct(ProductViewModel product)
        {
            product = oFactory.ProductHelper.GetOne(product);
            product.Success = null;
            return View(product);
        }

        /// <summary>
        /// Performs product edit in DB 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProcessEdit(ProductViewModel product)
        {
            product = oFactory.ProductHelper.EditProduct(product);
            return View("EditProduct", product);
        }


    }
}
