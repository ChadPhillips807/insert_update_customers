using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DataAccessLib
{
    public static class DataAccess
    {
        // 1) You must provide a business/middle tier class to do data validation. (Remember the Building Permit
        // application from PROG 120.)

        private const string sqlExcptMsg = "The database is not responding. Please contact support.";
        private const string connectionString = @"Server = SLAVE_1\SQLEXPRESS;Database = northwind;Integrated Security=SSPI";

        /// <summary>
        /// string companyName, string contactName, string contactTitle, string address, string city,
        /// string region, string postalCode, string country, string fax
        /// </summary>
        /// <param name="argsForParams"></param>
        /// <returns></returns>
        public static string InsertOrUpdateCustInfo(List<string> argsForParams, string functionName)            
        {
            SqlParameter sqlParam;
            SqlCommand spCommand = new SqlCommand();

            try
            {
                //Instantiate object -- Couldn't figure out how to make this static ugh
                RequiredParameters reqParam = new RequiredParameters();

                //Set list to insertCustInfoParams for comparison against customer input
                List<string> requiredParams = reqParam.InsertCustInfoParams;

                Regex regex = new Regex("([^@][a-zA-Z]+)");

                spCommand.Connection = new SqlConnection(connectionString);

                if (functionName == "insert")
                {
                    spCommand.CommandText = "InsertCustomerInfo";
                }
                else
                {
                    spCommand.CommandText = "UpdateCustomerInfo";
                }

                spCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //@ReturnValue,int,output,0,NOTNULL
                for (int i = 0; i < argsForParams.Count; i++)
                {
                    //Store required params into an array....
                    //ex. eachParamFromReqParams[0] = "@CompanyName", [1] = "nvarchar", [2] = "input", [3] = "NULL"
                    //@ReturnValue,int,output,0,NOTNULL
                    var eachParamFromReqParams = requiredParams[i].Split(new[] { ',' });

                    sqlParam = new SqlParameter();
                    //Set sqlParam.Direction = System.Data.ParameterDirection.<Input or Output>;
                    sqlParam = GetParamDirection(sqlParam, eachParamFromReqParams, argsForParams[i]);// should return sqlParam

                    GetSqlDbType(sqlParam, eachParamFromReqParams);

                    //Change to int for the size argument
                    int size = (Convert.ToInt32(eachParamFromReqParams[3]));

                    //Check for the size
                    if (size.GetType() == typeof(int))
                    {
                        sqlParam.Size = size;
                    }

                    //Check if null is allowed as parameter
                    if (eachParamFromReqParams[4].ToLower().Trim() == "null")
                    {
                        sqlParam.IsNullable = true;
                    }
                    else if (eachParamFromReqParams[2].ToLower().Trim() == "output")
                    {
                        break; //ignore this setting for the Output param
                    }
                    else
                    {
                        sqlParam.IsNullable = false;
                    }

                    spCommand.Parameters.Add(sqlParam);
                }

                spCommand.Connection.Open();//One Connection open
                
                spCommand.ExecuteNonQuery();

                //If the parameterDirection is an output, return string.empty
                if (functionName == "insert")
                {
                    return spCommand.Parameters["@CustomerID"].Value.ToString();
                }
                else
                {
                    return string.Empty;//There is no return value
                }
            }
            catch (SqlException)
            {

                throw new ApplicationException(sqlExcptMsg); ;
            }
            finally
            {
                spCommand.Connection.Close();//Now freed up for use, still open on the DB
            }
        }
        /// <summary>
        /// Get the direction of the parameter i.e. "input" or "output"
        /// </summary>
        /// <param name="sqlParam"></param>
        /// <param name="eachParamFromReqParams"></param>
        /// <param name="argsParam"></param>
        /// <returns></returns>
        private static SqlParameter GetParamDirection(SqlParameter sqlParam, string[] eachParamFromReqParams, string argsParam)
        {
            if (eachParamFromReqParams[2].ToLower().Trim() == "input")
            {
                sqlParam = new SqlParameter(eachParamFromReqParams[0], argsParam);
                sqlParam.Direction = System.Data.ParameterDirection.Input;
                
            }
            else
            {
                sqlParam = new SqlParameter();
                sqlParam.ParameterName = "@ReturnValue";
                sqlParam.Direction = System.Data.ParameterDirection.Output;
                
            }
            return sqlParam;
        }

        /// <summary>
        /// This method gets the Type for the parameter
        /// </summary>
        /// <param name="sqlParam"></param>
        /// <param name="eachParamFromReqParams"></param>
        private static void GetSqlDbType(SqlParameter sqlParam, string[] eachParamFromReqParams)
        {
            switch (eachParamFromReqParams[1].ToLower().Trim())
            {
                // Added the most used SQL data types to
                // This statement.
                case "bigint":
                    sqlParam.SqlDbType = System.Data.SqlDbType.BigInt;
                    break;
                case "binary":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Binary;
                    break;
                case "bit":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Bit;
                    break;
                case "char":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Char;
                    break;
                case "datetime":
                    sqlParam.SqlDbType = System.Data.SqlDbType.DateTime;
                    break;
                case "date":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Date;
                    break;
                case "decimal":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Decimal;
                    break;
                case "float":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Float;
                    break;
                case "int":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Int;
                    break;
                case "nchar":
                    sqlParam.SqlDbType = System.Data.SqlDbType.NChar;
                    break;
                case "ntext":
                    sqlParam.SqlDbType = System.Data.SqlDbType.NText;
                    break;
                case "nvarchar":
                    sqlParam.SqlDbType = System.Data.SqlDbType.NVarChar;
                    break;
                case "varchar":
                    sqlParam.SqlDbType = System.Data.SqlDbType.VarChar;
                    break;
                case "text":
                    sqlParam.SqlDbType = System.Data.SqlDbType.Text;
                    break;
            }
        }

        //static void Check<T>(Expression<Func<T>> expr)
        //{
        //    var body = ((MemberExpression)expr.Body);
        //    Console.WriteLine("Name is: {0}", body.Member.Name);
        //    Console.WriteLine("Value is: {0}", ((FieldInfo)body.Member)
        //   .GetValue(((ConstantExpression)body.Expression).Value));
        //}
    }
}
