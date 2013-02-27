The Bellpeek Software Company has obtained a contract from Northwind Traders.  Northwind wishes to redesign its in-house systems.  Bellpeek developers will begin the re-design process with the Order Entry (Sales) system by developing a proto-type.  The prototype will use n-tier architecture. 

Detailed Problem Specification: 
 
The application must provide the following functionality: 
 
1)	Provide capability that allows entry of a new customer into the Northwind database. 
a.	You must provide data validation.  However, data validation may be limited to checking the length of the text to make sure that it will “fit” in the database. 
 
2)	Provide capability that allows update of information about an existing customer in the Northwind database 
a.	You must provide data validation.  However, data validation may be limited to checking the length of the text to make sure that it will “fit” in the database. 
 
Note:  You may choose a very simple user interface. 
 
 
Note:  This is just a sample app with limited functionality.  It does not contain all the functionality that would be expected in a real application.   
    
 
Technical Requirements: 
 
 
1)	You must provide a business/middle tier class to do data validation.  (Remember the Building Permit application from PROG 120.) 
 
2)	For entry of a new customer, you must call the existing stored procedure:  InsertCustomerInfo.  You may NOT write your own SQL. 
 
3)	For update of an existing customer, you must call the existing stored procedure: UpdateCustomerInfo.  You may NOT write your own SQL. 
 
4)	Data access functionality should be developed in a class in a class library.  
 
5)	You must include exception handling. 
 
6)	You must use secure measures to guard against SQL injection and to avoid exposing database information. 
 
7)	You must provide comments in your data access class that explain: 
 
Page 1 of 2 
 	 	 
a.	Whether or not you are using connection pooling 
b.	Whether or not you have ever have more than 1 actual connection to the database open at any one time in your application 
c.	When your actual connection(s) to the database is closed  
