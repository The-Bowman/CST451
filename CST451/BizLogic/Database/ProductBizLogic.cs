using CST451.Data.DataModel.ProductDataModels;
using System.Data.SqlClient;

namespace CST451.BizLogic.Database
{
    public class ProductBizLogic
    {
        private string _sql = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbPCPARTS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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

        public ProductBizLogic(Factory factory)
        {
            _factory = factory;
        }

        public ProductDataModel GetOne(ProductDataModel product)
        {
            // prepared statement
            string getQry = "SELECT * FROM [dbo].[Product] WHERE ID=@ID";

            using (SqlConnection conn = new SqlConnection(_sql))
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

        public List<ProductDataModel> GetAll()
        {
            string getQry = "SELECT * FROM [dbo].[Product];";

            List<ProductDataModel> productList = new List<ProductDataModel>();

            using (SqlConnection conn = new SqlConnection(_sql))
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
    }
}
