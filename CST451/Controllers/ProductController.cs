using Microsoft.AspNetCore.Mvc;
using CST451.Models.ViewModels.Products;

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
        public IActionResult Index()
        {
            List<ProductViewModel> products = oFactory.ProductHelper.GetAll();
            return View(products);
        }
    }
}
