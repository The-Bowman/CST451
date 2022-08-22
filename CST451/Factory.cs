namespace CST451
{
    /// <summary>
    /// Factory class to reduce instances of classes
    /// The Factory will produce a new instance of 
    /// a class if it does not already exist to be used
    /// </summary>
    public class Factory
    {

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

        private BizLogic.Database.CustomerBizLogic _customerBizLogic;
        internal BizLogic.Database.CustomerBizLogic BizLogic
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
    }
}
