namespace CST451
{
    /// <summary>
    /// Factory class to reduce instances of classes
    /// The Factory will produce a new instance of 
    /// a class if it does not already exist to be used
    /// </summary>
    public class Factory
    {
        private Helpers.CartHelper _cartHelper;
        internal Helpers.CartHelper CartHelper
        {
            get
            {
                if (_cartHelper == null)
                    _cartHelper = new Helpers.CartHelper();
                return _cartHelper;
            }
        }

        private Helpers.ConnectionHelper _connectionHelper;
        internal Helpers.ConnectionHelper ConnectionHelper
        {
            get
            {
                if (_connectionHelper == null)
                    _connectionHelper = new Helpers.ConnectionHelper();
                return _connectionHelper;
            }
        }

        private Helpers.CustomerHelper _customerHelper;
        internal Helpers.CustomerHelper CustomerHelper
        {
            get
            {
                if (_customerHelper == null)
                    _customerHelper = new Helpers.CustomerHelper();
                return _customerHelper;
            }
        }

        private Helpers.EmployeeHelper _employeeHelper;
        internal Helpers.EmployeeHelper EmployeeHelper
        {
            get
            {
                if (_employeeHelper == null)
                    _employeeHelper = new Helpers.EmployeeHelper();
                return _employeeHelper;
            }
        }

        private Helpers.ProductHelper _productHelper;
        internal Helpers.ProductHelper ProductHelper
        {
            get
            {
                if (_productHelper == null)
                    _productHelper = new Helpers.ProductHelper();
                return _productHelper;
            }
        }

        private Helpers.CheckoutHelper _checkoutHelper;
        internal Helpers.CheckoutHelper CheckoutHelper
        {
            get
            {
                if (_checkoutHelper == null)
                    _checkoutHelper = new Helpers.CheckoutHelper();
                return _checkoutHelper;
            }
        }

        private BizLogic.Database.CustomerBizLogic _customerBizLogic;
        internal BizLogic.Database.CustomerBizLogic CustomerBizLogic
        {
            get
            {
                if (_customerBizLogic == null)
                    _customerBizLogic = new BizLogic.Database.CustomerBizLogic(this);
                return _customerBizLogic;
            }
        }

        private BizLogic.Database.ProductBizLogic _productBizLogic;
        internal BizLogic.Database.ProductBizLogic ProductBizLogic
        {
            get
            {
                if (_productBizLogic == null)
                    _productBizLogic = new BizLogic.Database.ProductBizLogic(this);
                return _productBizLogic;
            }
        }

        private BizLogic.Database.EmployeeBizLogic _employeeBizLogic;
        internal BizLogic.Database.EmployeeBizLogic EmployeeBizLogic
        {
            get
            {
                if (_employeeBizLogic == null)
                    _employeeBizLogic = new BizLogic.Database.EmployeeBizLogic(this);
                return _employeeBizLogic;
            }
        }

        private BizLogic.Database.CheckoutBizLogic _checkoutBizLogic;
        internal BizLogic.Database.CheckoutBizLogic CheckoutBizLogic
        {
            get
            {
                if (_checkoutBizLogic == null)
                    _checkoutBizLogic = new BizLogic.Database.CheckoutBizLogic(this);
                return _checkoutBizLogic;
            }
        }
    }
}
