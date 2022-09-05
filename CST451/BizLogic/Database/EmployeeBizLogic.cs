using CST451.Data.DataModel.UserDataModels;
using System.Data.SqlClient;

namespace CST451.BizLogic.Database
{
    public class EmployeeBizLogic
    {
        // Connection string - temporary solution until appsettings.json is resolved
        private string _sql = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbPCPARTS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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

        public EmployeeBizLogic(Factory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Executes SQL Statement to add customer to database
        /// </summary>
        /// <param name="employee">CustomerDataModel</param>
        /// <returns>CustomerDataModel</returns>
        /// <exception cref="Exception"></exception>
        public EmployeeDataModel Add(EmployeeDataModel employee, bool isAdmin)
        {
            if (!isAdmin)
            {
                employee.Success = false;
                return employee;
            }

            // prepared statement setup
            string addEmployeeQry = "INSERT INTO [dbo].[Employee] (Name, Address, City, State, Zip, Country, Username, Email, Phone, IsAdmin, Password) VALUES (@Name, @Address, @City, @State, @Zip, @Country, @Username, @Email, @Phone, @IsAdmin, @Password)";

            using (SqlConnection conn = new SqlConnection(_sql))
            {
                using (SqlCommand cmd = new SqlCommand(addEmployeeQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    cmd.Parameters.AddWithValue("@City", employee.City);
                    cmd.Parameters.AddWithValue("@State", employee.State);
                    cmd.Parameters.AddWithValue("@Zip", employee.Zip);
                    cmd.Parameters.AddWithValue("@Country", employee.Country);
                    cmd.Parameters.AddWithValue("@Username", employee.Username);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                    cmd.Parameters.AddWithValue("@Password", employee.Password);
                    cmd.Parameters.AddWithValue("@IsAdmin", 0);
                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            employee.Success = true;
                            return employee;
                        }
                        // return with fail result
                        employee.Success = false;
                        return employee;

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
        /// <param name="employee">CustomerDataModel</param>
        /// <returns>CustomerDataModel</returns>
        /// <exception cref="Exception"></exception>
        public EmployeeDataModel Login(EmployeeDataModel employee)
        {
            // prepared statement
            string loginQry = "SELECT * FROM [dbo].[Employee] WHERE Username=@Username and Password=@Password";

            using (SqlConnection conn = new SqlConnection(_sql))
            {
                using (SqlCommand cmd = new SqlCommand(loginQry, conn))
                {
                    // setup parameters
                    cmd.Parameters.AddWithValue("@Username", employee.Username);
                    cmd.Parameters.AddWithValue("@Password", employee.Password);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        // successful result
                        if (reader.HasRows)
                        {
                            reader.Read();

                            // assign values
                            employee.ID = (int)reader["ID"];
                            employee.Name = reader["Name"].ToString();
                            employee.Email = reader["Email"].ToString();
                            employee.Username = reader["Username"].ToString();
                            employee.Address = reader["Address"].ToString();
                            employee.City = reader["City"].ToString();
                            employee.State = reader["State"].ToString();
                            employee.Zip = (int)reader["Zip"];
                            employee.Phone = reader["Phone"].ToString();
                            employee.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                            employee.Success = true;
                            conn.Close();
                            return employee;
                        }

                        // return with fail result
                        employee.Success = false;
                        conn.Close();

                        return employee;

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
        /// <param name="dbEmployee"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public EmployeeDataModel GetOne(EmployeeDataModel dbEmployee)
        {
            // prepared statement
            string getQry = "SELECT * FROM [dbo].[Employee] WHERE ID=@ID";

            using (SqlConnection conn = new SqlConnection(_sql))
            {
                using (SqlCommand cmd = new SqlCommand(getQry, conn))
                {
                    // setup parameters
                    cmd.Parameters.AddWithValue("@ID", dbEmployee.ID);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        // successful result
                        if (reader.HasRows)
                        {
                            reader.Read();

                            // assign values
                            dbEmployee.ID = (int)reader["ID"];
                            dbEmployee.Name = reader["Name"].ToString();
                            dbEmployee.Address = reader["Address"].ToString();
                            dbEmployee.City = reader["City"].ToString();
                            dbEmployee.State = reader["State"].ToString();
                            dbEmployee.Zip = (int?)Convert.ToInt32(reader["Zip"]);
                            dbEmployee.Phone = reader["Phone"].ToString();
                            dbEmployee.Username = reader["Username"].ToString();
                            dbEmployee.Email = reader["Email"].ToString();
                            dbEmployee.Password = reader["Password"].ToString();
                            dbEmployee.IsAdmin = (int?)Convert.ToInt32(reader["IsAdmin"]) == 0 ? false : true;
                            dbEmployee.Success = true;
                            conn.Close();
                            return dbEmployee;
                        }

                        // return with fail result
                        dbEmployee.Success = false;
                        conn.Close();

                        return dbEmployee;

                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                        throw new Exception("An error occured trying to add the customer\nError: " + ex.Message);
                    }
                }
            }
        }

        public EmployeeDataModel Update(EmployeeDataModel dbEmployee)
        {
            string updateEmployeeQry = "UPDATE [dbo].[Employee] SET Name = @Name, Address = @Address, City =  @City, State = @State, Zip = @Zip, Country = @Country, Phone = @Phone, Email = @Email, Username = @Username, Password = @Password  WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(_sql))
            {
                using (SqlCommand cmd = new SqlCommand(updateEmployeeQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@Name", dbEmployee.Name);
                    cmd.Parameters.AddWithValue("@Address", dbEmployee.Address);
                    cmd.Parameters.AddWithValue("@City", dbEmployee.City);
                    cmd.Parameters.AddWithValue("@State", dbEmployee.State);
                    cmd.Parameters.AddWithValue("@Zip", dbEmployee.Zip);
                    cmd.Parameters.AddWithValue("@Country", dbEmployee.Country);
                    cmd.Parameters.AddWithValue("@Phone", dbEmployee.Phone);
                    cmd.Parameters.AddWithValue("@Email", dbEmployee.Email);
                    cmd.Parameters.AddWithValue("@Username", dbEmployee.Username);
                    cmd.Parameters.AddWithValue("@Password", dbEmployee.Password);   
                    cmd.Parameters.AddWithValue("@ID", dbEmployee.ID);   


                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            dbEmployee.Success = true;
                            return dbEmployee;
                        }
                        // return with fail result
                        dbEmployee.Success = false;
                        return dbEmployee;

                    }
                    catch (SqlException ex)
                    {
                        conn.Close();
                        throw new Exception("An error occured trying to edit the product\nError: " + ex.Message);
                    }
                }

            }
        }

        public EmployeeDataModel Delete(EmployeeDataModel dbEmployee)
        {
            string deleteEmployeeQry = "DELETE FROM [dbo].[Employee] WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(_sql))
            {
                using (SqlCommand cmd = new SqlCommand(deleteEmployeeQry, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@ID", dbEmployee.ID);

                    try
                    {
                        conn.Open();
                        int results = cmd.ExecuteNonQuery();
                        conn.Close();

                        // successful result 
                        if (results == 1)
                        {
                            dbEmployee.Success = true;
                            return dbEmployee;
                        }
                        // return with fail result
                        dbEmployee.Success = false;
                        return dbEmployee;

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

