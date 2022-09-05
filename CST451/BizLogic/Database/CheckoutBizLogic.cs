using CST451.Data.DataModel.OrderDataModels;

namespace CST451.BizLogic.Database
{
    public class CheckoutBizLogic
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

        public CheckoutBizLogic(Factory factory)
        {
            _factory = factory;
        }

        public string GetConnection()
        {
            string conn = oFactory.ConnectionHelper.Connection();
            return conn;
        }

        public CheckoutDataModel AddOrder(CheckoutDataModel dbCheckout)
        {
            string conn = GetConnection();
            return dbCheckout;
        }
        
    }
}
