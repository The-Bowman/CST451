using CST451.Data.DataModel.UserDataModels;
using System.Data.SqlClient;

namespace CST451.BizLogic.Database
{
    public class CustomerBizLogic
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
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            // prepared statement setup
            string addCustomerQry = "INSERT INTO [dbo].[Customer] (Name, Address, City, State, Zip, Country, Email, Phone, Password) VALUES (@Name, @Address, @City, @State, @Zip, @Country, @Email, @Phone, @Password)";

            using (SqlConnection conn = new SqlConnection(dbConn))
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
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            // prepared statement
            string loginQry = "SELECT * FROM [dbo].[Customer] WHERE Email=@Email and Password=@Password";

            using (SqlConnection conn = new SqlConnection(dbConn))
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
                            customer.Email = reader["Email"].ToString();
                            customer.Address = reader["Address"].ToString();
                            customer.City = reader["City"].ToString();
                            customer.State = reader["State"].ToString();
                            customer.Zip = (int)reader["Zip"];
                            customer.Phone = reader["Phone"].ToString();
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

        /// <summary>
        /// Get singular employee from db by employee ID
        /// </summary>
        /// <param name="dbCustomer"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CustomerDataModel GetOne(CustomerDataModel dbCustomer)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            // prepared statement
            string getQry = "SELECT * FROM [dbo].[Customer] WHERE ID=@ID";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(getQry, conn))
                {
                    // setup parameters
                    cmd.Parameters.AddWithValue("@ID", dbCustomer.ID);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        // successful result
                        if (reader.HasRows)
                        {
                            reader.Read();

                            // assign values
                            dbCustomer.ID = (int)reader["ID"];
                            dbCustomer.Name = reader["Name"].ToString();
                            dbCustomer.Address = reader["Address"].ToString();
                            dbCustomer.City = reader["City"].ToString();
                            dbCustomer.State = reader["State"].ToString();
                            dbCustomer.Zip = (int?)Convert.ToInt32(reader["Zip"]);
                            dbCustomer.Phone = reader["Phone"].ToString();
                            dbCustomer.Email = reader["Email"].ToString();
                            dbCustomer.Password = reader["Password"].ToString();
                            dbCustomer.Success = true;
                            conn.Close();
                            return dbCustomer;
                        }

                        // return with fail result
                        dbCustomer.Success = false;
                        conn.Close();

                        return dbCustomer;

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
        /// Update Customer in DB
        /// </summary>
        /// <param name="dbCustomer"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CustomerDataModel Update(CustomerDataModel dbCustomer)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            string updateCustomerQry = "UPDATE [dbo].[Customer] SET Name = @Name, Address = @Address, City =  @City, State = @State, Zip = @Zip, Country = @Country, Phone = @Phone, Email = @Email, Password = @Password  WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(updateCustomerQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@Name", dbCustomer.Name);
                    cmd.Parameters.AddWithValue("@Address", dbCustomer.Address);
                    cmd.Parameters.AddWithValue("@City", dbCustomer.City);
                    cmd.Parameters.AddWithValue("@State", dbCustomer.State);
                    cmd.Parameters.AddWithValue("@Zip", dbCustomer.Zip);
                    cmd.Parameters.AddWithValue("@Country", dbCustomer.Country);
                    cmd.Parameters.AddWithValue("@Phone", dbCustomer.Phone);
                    cmd.Parameters.AddWithValue("@Email", dbCustomer.Email);
                    cmd.Parameters.AddWithValue("@Password", dbCustomer.Password);
                    cmd.Parameters.AddWithValue("@ID", dbCustomer.ID);


                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            dbCustomer.Success = true;
                            return dbCustomer;
                        }
                        // return with fail result
                        dbCustomer.Success = false;
                        return dbCustomer;

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
        /// Delete customer from DB
        /// </summary>
        /// <param name="dbCustomer"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CustomerDataModel Delete(CustomerDataModel dbCustomer)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            string deleteCustomerQry = "DELETE FROM [dbo].[Customer] WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(deleteCustomerQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@ID", dbCustomer.ID);

                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            dbCustomer.Success = true;
                            return dbCustomer;
                        }
                        // return with fail result
                        dbCustomer.Success = false;
                        return dbCustomer;

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

