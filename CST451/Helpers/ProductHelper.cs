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

        /// <summary>
        /// Get singular product from product ID 
        /// </summary>
        /// <param name="vmProduct"></param>
        /// <returns></returns>
        public ProductViewModel GetOne(ProductViewModel vmProduct)
        {
            ProductDataModel dbProduct = new ProductDataModel();
            dbProduct = ParseVMtoDB(vmProduct);
            dbProduct = oFactory.ProductBizLogic.GetOne(dbProduct);
            vmProduct = ParseDBtoVM(dbProduct);   
            return vmProduct;
        }

        /// <summary>
        /// Get all products from db
        /// </summary>
        /// <returns></returns>
        internal List<ProductViewModel> GetAll()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            products.AddRange(oFactory.ProductBizLogic.GetAll().Select(x => ParseDBtoVM(x)));

            return products;
        }

        /// <summary>
        /// Add product to db
        /// </summary>
        /// <param name="vmProduct"></param>
        /// <returns></returns>
        internal ProductViewModel AddProduct(ProductViewModel vmProduct)
        {
            ProductDataModel dbProduct = new ProductDataModel();
            dbProduct = ParseVMtoDB(vmProduct);
            dbProduct = oFactory.ProductBizLogic.Add(dbProduct);
            vmProduct = ParseDBtoVM(dbProduct);
            return vmProduct;
        }

        internal ProductViewModel EditProduct(ProductViewModel vmProduct)
        {
            ProductDataModel dbProduct = ParseVMtoDB(vmProduct);
            dbProduct = oFactory.ProductBizLogic.Edit(dbProduct);
            return ParseDBtoVM(dbProduct);
        }

        #region Parsing
        /// <summary>
        /// Parse from View and Data layers
        /// </summary>
        /// <param name="vmProduct"></param>
        /// <returns></returns>
        private ProductDataModel ParseVMtoDB(ProductViewModel vmProduct)
        {
            ProductDataModel dbProduct = new ProductDataModel()
            {
                Name = vmProduct.Name,
                Description = vmProduct.Description,
                Price = vmProduct.Price,
                Compatibility = vmProduct.Compatibility,
                ImagePath = vmProduct.ImagePath,
                Success = vmProduct.Success,
            };
            if (vmProduct.ID != null)
                dbProduct.ID = vmProduct.ID;

            return dbProduct;
        }

        /// <summary>
        /// Parse from Data to View models
        /// </summary>
        /// <param name="dbProduct"></param>
        /// <returns></returns>
        private ProductViewModel ParseDBtoVM(ProductDataModel dbProduct)
        {
            ProductViewModel vmProduct = new ProductViewModel()
            {
                Name = dbProduct.Name,
                Description = dbProduct.Description,
                Price = dbProduct.Price,
                Compatibility = dbProduct.Compatibility,
                ImagePath = dbProduct.ImagePath,
                Success = dbProduct.Success,
            };
            if (dbProduct.ID != null)
                vmProduct.ID = dbProduct.ID;

            return vmProduct;
        }

        #endregion

     }
}
