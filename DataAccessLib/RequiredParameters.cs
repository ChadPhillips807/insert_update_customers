using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLib
{
    public class RequiredParameters
    {
        // This is a list of input parameters that will be arguements for the insertCustInfoParams stored proc
        private List<string> insertCustInfoParams;
        private string insertCustInfoOutput;

        public RequiredParameters()
        {
            insertCustInfoParams = new List<string>() { "@CustomerID,nchar,input,5,NOTNULL", "@CompanyName,nvarchar,input,40,NOTNULL", "@ContactName,nvarchar,input,30,NULL",
            "@ContactTitle,nvarchar,input,30,NULL", "@Address,nvarchar,input,60,NULL", "@City,nvarchar,input,15,NULL", "@Region,nvarchar,input,15,NULL",
            "@PostalCode,nvarchar,input,10,NULL", "@Country,nvarchar,input,15,NULL", "@Phone,nvarchar,input,24,NULL", "@Fax,nvarchar,input,24,NULL", "@ReturnValue,int,output,0,NOTNULL" };

            insertCustInfoOutput = "";
        
        }
        
        public List<string> InsertCustInfoParams
        {
            get { return insertCustInfoParams; }
        }
        public string InsertCustInfoOutput
        {
            get { return insertCustInfoOutput; }
        }
    }
}
