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

        [HttpGet]
        public IActionResult BrowseAll()
        {
            List<ProductViewModel> products = oFactory.ProductHelper.GetAll();
            return View(products);
        }

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

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel product)
        {
            product = oFactory.ProductHelper.AddProduct(product);
            return View(product);
        }

        [HttpGet]
        public IActionResult FindProductBySKU()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel product)
        {
            product = oFactory.ProductHelper.GetOne(product);
            product.Success = null;
            return View(product);
        }

        [HttpPost]
        public IActionResult ProcessEdit(ProductViewModel product)
        {
            product = oFactory.ProductHelper.EditProduct(product);
            return View("EditProduct", product);
        }


    }
}
