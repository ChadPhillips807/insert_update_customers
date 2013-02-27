using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLib
{


    //TODO: Get the CustomerID column for the combobox and fill the table values
    // with query for Comboboxes selected value;
    public static class CustomerDataAccess
    {
        private const string sqlExcptMsg = "The database is not responding. Please contact support.";
        private const string connectionString = @"Server = SLAVE_1\SQLEXPRESS;Database = northwind;Integrated Security=SSPI";

        //b. All columns from the Customer table in the Northwind database should be displayed for each customer.
        private const string selectAllFromCustomers = "SELECT * FROM Customers ORDER BY CustomerID";

        public static DataSet GetCustomers()
        {
            SqlDataAdapter northwindDataAdapter = new SqlDataAdapter();

            northwindDataAdapter.SelectCommand = new SqlCommand();
            northwindDataAdapter.SelectCommand.Connection = new SqlConnection();

            DataSet northwndCustOrdersDataSet;

            try
            {
                northwindDataAdapter.SelectCommand.Connection.ConnectionString = connectionString;

                // Create the DataTable to store the Customer table 
                northwndCustOrdersDataSet = new DataSet("CustomersDataSet");

                // change command text to select Customers
                northwindDataAdapter.SelectCommand.CommandText = selectAllFromCustomers;

                // Fill the DataTable with Customers
                northwindDataAdapter.Fill(northwndCustOrdersDataSet, "Customers");//Connection is open #1 connection if this form was opened first o.O

                // returning the filled DataTable to whatever calls for it!!
                return northwndCustOrdersDataSet;
            }
            catch (SqlException)
            {
                // Use generic message to not give the actual exception to the hackers
                throw new ApplicationException(sqlExcptMsg);
            }
            finally
            {
                northwindDataAdapter.SelectCommand.Connection.Close();//Connection is given to the conection pool
            }
        }

    }
}
