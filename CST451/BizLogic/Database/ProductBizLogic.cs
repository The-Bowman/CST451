using CST451.Controllers;
using CST451.Data.DataModel.ProductDataModels;
using CST451.Data.DataModel.UserDataModels;
using System.Data.SqlClient;

namespace CST451.BizLogic.Database
{
    public class ProductBizLogic
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

        public ProductBizLogic(Factory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Get singular product from db from product ID
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ProductDataModel GetOne(ProductDataModel product)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            // prepared statement
            string getQry = "SELECT * FROM [dbo].[Product] WHERE ID=@ID";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(getQry, conn))
                {
                    // setup parameters
                    cmd.Parameters.AddWithValue("@ID", product.ID);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        // successful result
                        if (reader.HasRows)
                        {
                            reader.Read();

                            // assign values
                            product.ID = (int)reader["ID"];
                            product.Name = reader["Name"].ToString();
                            product.Description = reader["Description"].ToString();
                            product.Price = (double?)Convert.ToDecimal(reader["Price"]);
                            product.Compatibility = (int?)Convert.ToInt32(reader["Compatibility"]);
                            product.ImagePath = reader["ImagePath"].ToString();
                            product.Success = true;
                            conn.Close();
                            return product;
                        }

                        // return with fail result
                        product.Success = false;
                        conn.Close();

                        return product;

                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                        throw new Exception("An error occured trying to add the customer\nError: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Get all products from db
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ProductDataModel> GetAll()
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            string getQry = "SELECT * FROM [dbo].[Product];";

            List<ProductDataModel> productList = new List<ProductDataModel>();

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(getQry, conn))
                {

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        // successful result
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductDataModel product = new ProductDataModel()
                                {
                                    ID = (int)reader["ID"],
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (double?)Convert.ToDecimal(reader["Price"]),
                                    Compatibility = (int?)Convert.ToInt32(reader["Compatibility"]),
                                    ImagePath = reader["ImagePath"].ToString()
                                };

                                productList.Add(product);
                            }

                        }

                        conn.Close();

                        return productList;

                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                        throw new Exception("An error occured trying to add the customer\nError: " + ex.Message);
                    }
                }

            }
        }

        /// <summary>
        /// Add product to db
        /// </summary>
        /// <param name="dbProduct"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ProductDataModel Add(ProductDataModel dbProduct)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            // prepared statement setup
            string addCustomerQry = "INSERT INTO [dbo].[Product] (Name, Description, Compatibility, Price) VALUES (@Name, @Description, @Compatibility, @Price)";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(addCustomerQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@Name", dbProduct.Name);
                    cmd.Parameters.AddWithValue("@Description", dbProduct.Description);
                    cmd.Parameters.AddWithValue("@Compatibility", dbProduct.Compatibility);
                    cmd.Parameters.AddWithValue("@Price", dbProduct.Price);


                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            dbProduct.Success = true;
                            return dbProduct;
                        }
                        // return with fail result
                        dbProduct.Success = false;
                        return dbProduct;

                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                        throw new Exception("An error occured trying to add the customer\nError: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Update Product data in DB
        /// </summary>
        /// <param name="dbProduct"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ProductDataModel Edit(ProductDataModel dbProduct)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            string addCustomerQry = "UPDATE [dbo].[Product] SET Name = @Name, Description = @Description, Compatibility =  @Compatibility, Price = @Price WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(addCustomerQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@Name", dbProduct.Name);
                    cmd.Parameters.AddWithValue("@Description", dbProduct.Description);
                    cmd.Parameters.AddWithValue("@Compatibility", dbProduct.Compatibility);
                    cmd.Parameters.AddWithValue("@Price", dbProduct.Price);
                    cmd.Parameters.AddWithValue("@ID", dbProduct.ID);


                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            dbProduct.Success = true;
                            return dbProduct;
                        }
                        // return with fail result
                        dbProduct.Success = false;
                        return dbProduct;

                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                        throw new Exception("An error occured trying to edit the product\nError: " + ex.Message);
                    }
                }

            }
        }

        /// <summary>
        /// Delete Product from DB
        /// </summary>
        /// <param name="dbProduct"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ProductDataModel Delete(ProductDataModel dbProduct)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            string deleteProductQry = "DELETE FROM [dbo].[Product] WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(deleteProductQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@ID", dbProduct.ID);

                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            dbProduct.Success = true;
                            return dbProduct;
                        }
                        // return with fail result
                        dbProduct.Success = false;
                        return dbProduct;

                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                        throw new Exception("An error occured trying to edit the product\nError: " + ex.Message);
                    }
                }
            }
        }
    }
}
