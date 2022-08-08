using CST451.Models.Users;
using CST451.Data.DataModel.UserDataModels;

namespace CST451.Helpers
{
    public class CustomerHelper
    {
        /// <summary>
        /// Allows access to Factory
        /// </summary>
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
        /// Reaches out to BizLogic layer to add customer to database
        /// </summary>
        /// <param name="customer">CustomerViewModel</param>
        /// <returns>CustomerViewModel</returns>
        public CustomerViewModel AddCustomer(CustomerViewModel customer)
        {
            CustomerDataModel dmCustomer = ParseVMCustomerToDMCustomer(customer);
            dmCustomer = _Factory.BizLogic.AddCustomer(dmCustomer);
            customer = ParseDMCustomerToVMCustomer(dmCustomer);
            return customer;
        }

        /// <summary>
        /// Reaches out to BizLogic layer to authenticate customer
        /// </summary>
        /// <param name="vmCustomer">CustomerViewModel</param>
        /// <returns>CustomerViewModel</returns>
        public CustomerViewModel Login(CustomerViewModel vmCustomer)
        {
            CustomerDataModel dmCustomer = ParseVMCustomerToDMCustomer(vmCustomer);
            dmCustomer = _Factory.BizLogic.LoginCustomer(dmCustomer);
            return ParseDMCustomerToVMCustomer(dmCustomer);
        }




        #region "Parsing

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


    }
}
