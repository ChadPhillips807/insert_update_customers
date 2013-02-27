using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccessLib;
using System.Windows.Forms.Layout;

namespace CustomerDetailsForm
{
    public partial class InsertCustFrm : Form
    {
        public InsertCustFrm()
        {
            InitializeComponent();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(customerIdTextBox.Text) || !string.IsNullOrEmpty(companyNameTextBox.Text))
            {
                // return value storage
                string customerId;

                // list of parameters for the insert cust info stored proc
                List<string> paramList = new List<string>() { customerIdTextBox.Text, companyNameTextBox.Text,
                    contactNameTextBox.Text, contactTitleTextBox.Text, addressTextBox.Text, cityTextBox.Text,
                    regionTextBox.Text, postalCodeTextBox.Text, countryTextBox.Text, phoneTextBox.Text, faxTextBox.Text };

                customerId = DataAccess.InsertOrUpdateCustInfo(paramList, "insert");

                MessageBox.Show(string.Format("The data for {0} has successfully been entered into the database.", customerId));

                clearTextBoxes();
            }
            else
            {
                MessageBox.Show("CustomerId and CompanyName fields must be entered.");
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }

}
