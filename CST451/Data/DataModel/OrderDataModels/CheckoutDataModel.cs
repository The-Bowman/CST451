using CST451.Data.DataModel.OrderDataModels;
using CST451.Data.DataModel.ProductDataModels;
using CST451.Data.DataModel.UserDataModels;
using CST451.Models.ViewModels;

namespace CST451.Data.DataModel.OrderDataModels
{
    public class CheckoutDataModel
    {
        public CustomerDataModel? CustomerShippingInfo { get; set; }
        public CustomerDataModel? CustomerBillingInfo { get; set; }
        public CartViewModel? Cart { get; set; }
        public OrderDataModel? Order { get; set; }
        public List<OrderLineDataModel>? OrderLines { get; set; }
        public string CreditCardName { get; set; }
        public string ExpirationDate { get; set; }
        public string CreditCardNumber { get; set; }
        public int? CVV { get; set; }
        public bool? Success { get; set; }

    }
}
