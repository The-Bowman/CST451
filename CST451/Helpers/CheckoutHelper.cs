using CST451.Data.DataModel.OrderDataModels;
using CST451.Data.DataModel.UserDataModels;
using CST451.Models.Orders;
using CST451.Models.Users;
using CST451.Models.ViewModels.Checkout;
using System.Linq;

namespace CST451.Helpers
{
    public class CheckoutHelper
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

        public CheckoutModel AddOrder(CheckoutModel vmCheckout)
        {
            CheckoutDataModel dbCheckout = ParseVMCheckouttoDMCheckout(vmCheckout);
            dbCheckout = oFactory.CheckoutBizLogic.AddOrder(dbCheckout);
            return ParseDMCheckouttoVMCheckout(dbCheckout);

        }



        #region CheckoutModel Parsing

        private CheckoutDataModel ParseVMCheckouttoDMCheckout(CheckoutModel vmCheckout)
        {
            CheckoutDataModel dbCheckout = new CheckoutDataModel()
            {
                CustomerShippingInfo = ParseVMCustomerToDMCustomer(vmCheckout.CustomerShippingInfo),
                CustomerBillingInfo = ParseVMCustomerToDMCustomer(vmCheckout.CustomerBillingInfo),
                Order = ParseVMOrdertoDMOrder(vmCheckout.Order),
                Cart = vmCheckout.Cart,
                CreditCardName = vmCheckout.CustomerBillingInfo.Name,
                ExpirationDate = vmCheckout.ExpirationDate,
                CreditCardNumber = vmCheckout.CreditCardNumber,
                OrderLines = new List<OrderLineDataModel>(),
                Success = vmCheckout.Success,
            };
            dbCheckout.OrderLines.AddRange(vmCheckout.OrderLines.Select(item => ParseVMOrderLinetoDMOrderLine(item)));
            return dbCheckout;
        }

        private CheckoutModel ParseDMCheckouttoVMCheckout(CheckoutDataModel dbCheckout)
        {
            CheckoutModel vmCheckout = new CheckoutModel()
            {
                CustomerShippingInfo = ParseDMCustomerToVMCustomer(dbCheckout.CustomerShippingInfo),
                CustomerBillingInfo = ParseDMCustomerToVMCustomer(dbCheckout.CustomerBillingInfo),
                Order = ParseDMOrdertoVMOrder(dbCheckout.Order),
                Cart = dbCheckout.Cart,
                CreditCardName = dbCheckout.CreditCardName,
                ExpirationDate = dbCheckout.ExpirationDate,
                CreditCardNumber = dbCheckout.CreditCardNumber,
                OrderLines = new List<OrderLineViewModel>(),
                Success = dbCheckout.Success,
            };
            vmCheckout.OrderLines.AddRange(dbCheckout.OrderLines.Select(item => ParseDMOrderLinetoVMOrderLine(item)));
            return vmCheckout;  
        }


        #endregion

        #region CustomerModel Parsing

        /// <summary>
        /// Parse from View to Data layers
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private CustomerDataModel ParseVMCustomerToDMCustomer(CustomerViewModel customer)
        {
            CustomerDataModel customerDataModel = new CustomerDataModel()
            {
                Name = customer.Name,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                Zip = customer.Zip,
                Country = customer.Country,
                Email = customer.Email,
                Phone = customer.Phone,
                Password = customer.Password
            };
            if (customer.ID != null)
                customerDataModel.ID = customer.ID;

            return customerDataModel;
        }

        /// <summary>
        /// Parse from Data to View layers
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private CustomerViewModel ParseDMCustomerToVMCustomer(CustomerDataModel customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Name = customer.Name,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                Zip = customer.Zip,
                Country = customer.Country,
                Email = customer.Email,
                Phone = customer.Phone,
                Password = customer.Password,
                Success = customer.Success
            };
            if (customer.ID != null)
                customerViewModel.ID = customer.ID;

            return customerViewModel;
        }

        #endregion

        #region OrderLine Parsing

        private OrderLineDataModel ParseVMOrderLinetoDMOrderLine(OrderLineViewModel vmOrderLine)
        {
            OrderLineDataModel dbOrderLine = new OrderLineDataModel()
            {
                ProductID = vmOrderLine.ProductID,
                ProductName = vmOrderLine.ProductName,
                ProductPrice = vmOrderLine.ProductPrice,
                ProductQuantity = vmOrderLine.ProductQuantity,
            };

            dbOrderLine.OrderID = vmOrderLine.OrderID == null ? null : vmOrderLine.OrderID;
            dbOrderLine.LineID = vmOrderLine.LineID == null ? null : vmOrderLine.LineID;
            return dbOrderLine;
        }

        private OrderLineViewModel ParseDMOrderLinetoVMOrderLine(OrderLineDataModel dbOrderLine)
        {
            OrderLineViewModel vmOrderLine = new OrderLineViewModel()
            {
                ProductID = dbOrderLine.ProductID,
                ProductName = dbOrderLine.ProductName,
                ProductPrice = dbOrderLine.ProductPrice,
                ProductQuantity = dbOrderLine.ProductQuantity,
            };

            vmOrderLine.OrderID = dbOrderLine.OrderID == null ? null : dbOrderLine.OrderID;
            vmOrderLine.LineID = dbOrderLine.LineID == null ? null : dbOrderLine.LineID;
            return vmOrderLine;
        }

        #endregion

        #region Order Parsing

        public OrderDataModel ParseVMOrdertoDMOrder(OrderViewModel vmOrder)
        {
            OrderDataModel dbOrder = new OrderDataModel()
            {
                CustomerID = vmOrder.CustomerID,
                CustomerName = vmOrder.CustomerName,
                Total = vmOrder.Total,
            };

            dbOrder.OrderID = vmOrder.OrderID == null ? null : vmOrder.OrderID;

            return dbOrder;
        }

        public OrderViewModel ParseDMOrdertoVMOrder(OrderDataModel dbOrder)
        {
            OrderViewModel vmOrder = new OrderViewModel()
            {
                CustomerName = dbOrder.CustomerName,
                CustomerID = dbOrder.CustomerID,
                Total = dbOrder.Total,
            };

            vmOrder.OrderID = dbOrder.OrderID == null ? null : dbOrder.OrderID;

            return vmOrder;
        }

        #endregion
    }
}
