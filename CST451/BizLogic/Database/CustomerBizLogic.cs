using CST451.Data.DataModel.UserDataModels;
using System.Data.SqlClient;

namespace CST451.BizLogic.Database
{
    public class CustomerBizLogic
    {
        // Connection string - temporary solution until appsettings.json is resolved
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

        public CustomerBizLogic(Factory factory)
        {
            _factory = factory;             
        }

        /// <summary>
        /// Executes SQL Statement to add customer to database
        /// </summary>
        /// <param name="customer">CustomerDataModel</param>
        /// <returns>CustomerDataModel</returns>
        /// <exception cref="Exception"></exception>
        public CustomerDataModel AddCustomer(CustomerDataModel customer)
        {
            // prepared statement setup
            string addCustomerQry = "INSERT INTO [dbo].[Customer] (Name, Address, City, State, Zip, Country, Email, Phone, Password) VALUES (@Name, @Address, @City, @State, @Zip, @Country, @Email, @Phone, @Password)";

            using (SqlConnection conn = new SqlConnection(_sql))
            {
                using (SqlCommand cmd = new SqlCommand(addCustomerQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@State", customer.State);
                    cmd.Parameters.AddWithValue("@Zip", customer.Zip);
                    cmd.Parameters.AddWithValue("@Country", customer.Country);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);

                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            customer.Success = true;
                            return customer;
                        }
                        // return with fail result
                        customer.Success = false;
                        return customer;

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
        /// Authenticates user against database
        /// </summary>
        /// <param name="customer">CustomerDataModel</param>
        /// <returns>CustomerDataModel</returns>
        /// <exception cref="Exception"></exception>
        public CustomerDataModel LoginCustomer(CustomerDataModel customer)
        {
            // prepared statement
            string loginQry = "SELECT * FROM [dbo].[Customer] WHERE Email=@Email and Password=@Password";

            using (SqlConnection conn = new SqlConnection(_sql))
            {
                using (SqlCommand cmd = new SqlCommand(loginQry, conn))
                {
                    // setup parameters
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        // successful result
                        if (reader.HasRows)
                        {
                            reader.Read();

                            // assign values
                            customer.ID = (int)reader["ID"];
                            customer.Name = reader["Name"].ToString();                            
                            customer.Success = true;
                            conn.Close();
                            return customer;
                        }

                        // return with fail result
                        customer.Success = false;
                        conn.Close();

                        return customer;

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
