using CST451.Models.ViewModels;
using System.Text.Json;

namespace CST451.Helpers
{
    public class CartHelper
    {
        /// <summary>
        /// Allows access to Factory
        /// </summary>
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
        /// Convert JSON cart to cart object
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public CartViewModel GetCart(string cart)
        {
            CartViewModel cartViewModel = new CartViewModel();
            if (!string.IsNullOrEmpty(cart))
            {                
                cartViewModel = JsonSerializer.Deserialize<CartViewModel>(cart);
            }
            return cartViewModel;

        }
    }
}
