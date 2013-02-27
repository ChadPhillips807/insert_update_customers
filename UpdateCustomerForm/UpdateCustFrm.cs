using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLib;

namespace UpdateCustomerForm
{
    public partial class UpdateCustFrm : Form
    {
        DataSet customersDataSet;

        const string errorMsg = "contact support!";

        public UpdateCustFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception)
            {

                MessageBox.Show(errorMsg); ;
            }

            //customerDataGridView;
        }

        private void BindData()
        {
            try
            {
                customersDataSet = CustomerDataAccess.GetCustomers();
                
                customerIdTextBox.DataBindings.Add("Text", customersDataSet, "Customers.CustomerID");
                customerNameTextBox.DataBindings.Add("Text", customersDataSet, "Customers.CompanyName");
                contactNameTextBox.DataBindings.Add("Text", customersDataSet, "Customers.ContactName");
                contactTitleTextBox.DataBindings.Add("Text", customersDataSet, "Customers.ContactTitle");
                addressTextBox.DataBindings.Add("Text", customersDataSet, "Customers.Address");
                cityTextBox.DataBindings.Add("Text", customersDataSet, "Customers.City");
                regionTextBox.DataBindings.Add("Text", customersDataSet, "Customers.Region");
                zipTextBox.DataBindings.Add("Text", customersDataSet, "Customers.PostalCode");
                countryTextBox.DataBindings.Add("Text", customersDataSet, "Customers.Country");


                customerDataGridView.DataSource = customersDataSet;
                customerDataGridView.DataMember = "Customers";
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void topOfListButton_Click(object sender, EventArgs e)
        {
            if (BindingContext[customersDataSet, "Customers"].Position != 0)// If you're at the top of the list already
            {
                BindingContext[customersDataSet, "Customers"].Position = 0;
            }
            else
            {
                MessageBox.Show("Can't go up any higher");
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (BindingContext[customersDataSet, "Customers"].Position != BindingContext[customersDataSet, "Customers"].Count - 1)// If you're at the Bottom of the list already
            {
                BindingContext[customersDataSet, "Customers"].Position += 1;
            }
            else
            {
                MessageBox.Show("Can't go down any LOWER");
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (BindingContext[customersDataSet, "Customers"].Position != 0)// If you're at the top of the list already
            {
                BindingContext[customersDataSet, "Customers"].Position -= 1;
            }
            else
            {
                MessageBox.Show("Can't go up any higher");
            }
        }

        private void bottomOfListButton_Click(object sender, EventArgs e)
        {
            if (BindingContext[customersDataSet, "Customers"].Position != BindingContext[customersDataSet, "Customers"].Count - 1)// If you're at the Bottom of the list already
            {
                BindingContext[customersDataSet, "Customers"].Position = BindingContext[customersDataSet, "Customers"].Count - 1;
            }
            else
            {
                MessageBox.Show("Can't go down any LOWER");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                saveUpdateButton.Enabled = true;
            }
            else
            {
                saveUpdateButton.Enabled = false;
            }
        }

        // This is the Save Update Button. I double clicked it too soon.
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(customerIdTextBox.Text) || !string.IsNullOrEmpty(customerNameTextBox.Text))
                {
                    // return value storage
                    string customerId;

                    // list of parameters for the insert cust info stored proc
                    List<string> paramList = new List<string>() { customerIdTextBox.Text, customerNameTextBox.Text,
                    contactNameTextBox.Text, contactTitleTextBox.Text, addressTextBox.Text, cityTextBox.Text,
                    regionTextBox.Text, zipTextBox.Text, countryTextBox.Text, phoneTextBox.Text, faxTextBox.Text };

                    customerId = DataAccess.InsertOrUpdateCustInfo(paramList, "update");

                    MessageBox.Show(string.Format("The data for {0} has successfully been entered into the database.", customerNameTextBox.Text));

                    clearDataBindings();

                    customersDataSet.Dispose();//Dispose to reload data set

                    clearTextBoxes();

                    BindData();
                }
                else
                {
                    MessageBox.Show("CustomerId and CompanyName fields must be filled in.");
                }
            }
            catch (Exception)
            {

                MessageBox.Show(errorMsg); ;
            }

        }

        public void clearTextBoxes()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    c.Text = string.Empty;
                }
            }
        }

        public void clearDataBindings()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    c.DataBindings.Clear();
                }
            }
        }
    }
}
