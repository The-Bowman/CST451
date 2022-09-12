using CST451.Models.Orders;
using CST451.Models.Users;
using CST451.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace CST451.Models.ViewModels.Checkout
{
    public class CheckoutModel
    {
        public CustomerViewModel? CustomerShippingInfo { get; set; }
        public CustomerViewModel? CustomerBillingInfo { get; set; }
        public CartViewModel? Cart { get; set; }
        public OrderViewModel? Order { get; set; }
        public List<OrderLineViewModel>? OrderLines { get; set; }
        public string CreditCardName { get; set; }
        public string ExpirationDate { get; set; }
        public string CreditCardNumber { get; set; }
        public int? CVV { get; set; }
        public bool? Success { get; set; }

    }
}
