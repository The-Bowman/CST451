using CST451.Controllers;
using CST451.Data.DataModel.OrderDataModels;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

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


        /// <summary>
        /// Insert Order and OrderLines into DB
        /// </summary>
        /// <param name="dbCheckout"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public CheckoutDataModel AddOrder(CheckoutDataModel dbCheckout)
        {
            string dbConn = oFactory.ConnectionHelper.GetConnection();

            string insertOrder = "INSERT INTO [dbo].[Orders] (Customer_Name, Customer_ID, Total) OUTPUT INSERTED.Order_ID VALUES (@CustomerName, @CustomerID, @Total)";

            using (SqlConnection conn = new SqlConnection(dbConn))
            {
                using (SqlCommand cmd = new SqlCommand(insertOrder, conn))
                {
                    // add parameters to statement
                    cmd.Parameters.AddWithValue("@CustomerName", dbCheckout.CustomerBillingInfo.Name);
                    cmd.Parameters.AddWithValue("@CustomerID", dbCheckout.Cart.CustomerID);
                    cmd.Parameters.AddWithValue("@Total", dbCheckout.Cart.Total);
                    try
                    {
                        conn.Open();

                        // returns the ID from Order inserted to use for Order Lines table
                        var id = cmd.ExecuteScalar();
                        
                        if (id != null)
                        {
                            int results = 0;
                            foreach (var item in dbCheckout.OrderLines)
                            {
                                string insertOrderLine = "INSERT INTO [dbo].[OrderLines] (Order_ID, Product_ID, Product_Name, Product_Price, Product_Quantity) VALUES(@Order_ID, @Product_ID, @Product_Name, @Product_Price, @Product_Quantity)";

                                using (SqlCommand command = new SqlCommand(insertOrderLine, conn))
                                {
                                    command.Parameters.AddWithValue("@Order_ID", id);
                                    command.Parameters.AddWithValue("@Product_ID", item.ProductID);
                                    command.Parameters.AddWithValue("@Product_Name", item.ProductName);
                                    command.Parameters.AddWithValue("@Product_Price", item.ProductPrice);
                                    command.Parameters.AddWithValue("@Product_Quantity", item.ProductQuantity);

                                    results += command.ExecuteNonQuery();
                                }

                            }

                            if (results == dbCheckout.OrderLines.Count)
                            {
                                conn.Close();
                                dbCheckout.Order.OrderID = (int?)id;
                                dbCheckout.Success = true;
                                return dbCheckout;
                            }
                            
                        }
                        conn.Close();
                        dbCheckout.Success = false;
                        return dbCheckout;

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
