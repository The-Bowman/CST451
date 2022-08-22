using CST451.Models.ViewModels.Products;
using CST451.Data.DataModel.ProductDataModels;

namespace CST451.Helpers
{
    public class ProductHelper
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

        public ProductViewModel GetOne(ProductViewModel vmProduct)
        {
            ProductDataModel dbProduct = new ProductDataModel();
            dbProduct = ParseVMtoDB(vmProduct);
            dbProduct = oFactory.ProductBizLogic.GetOne(dbProduct);
            vmProduct = ParseDBtoVM(dbProduct);   
            return vmProduct;
        }

        internal List<ProductViewModel> GetAll()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            products.AddRange(oFactory.ProductBizLogic.GetAll().Select(x => ParseDBtoVM(x)));

            return products;
        }

        #region "Parsing
        private ProductDataModel ParseVMtoDB(ProductViewModel vmProduct)
        {
            ProductDataModel dbProduct = new ProductDataModel()
            {
                Name = vmProduct.Name,
                Description = vmProduct.Description,
                Price = vmProduct.Price,
                Compatibility = vmProduct.Compatibility,
            };
            if (vmProduct.ID != null)
                dbProduct.ID = vmProduct.ID;

            return dbProduct;
        }

        private ProductViewModel ParseDBtoVM(ProductDataModel dbProduct)
        {
            ProductViewModel vmProduct = new ProductViewModel()
            {
                Name = dbProduct.Name,
                Description = dbProduct.Description,
                Price = dbProduct.Price,
                Compatibility = dbProduct.Compatibility,
                ImagePath = dbProduct.ImagePath
            };
            if (dbProduct.ID != null)
                vmProduct.ID = dbProduct.ID;

            return vmProduct;
        }

        #endregion

       
    }
}
