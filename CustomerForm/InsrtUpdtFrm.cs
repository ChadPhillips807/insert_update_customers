using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdateCustomerForm;
using CustomerDetailsForm;// This is the InsertCustomerForm... I renamed it.

namespace CustomerForm
{
    public partial class chooseForm : Form
    {
        public chooseForm()
        {
            InitializeComponent();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            InsertCustFrm frm = new InsertCustFrm();
            frm.Show();

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateCustFrm frm = new UpdateCustFrm();
            frm.Show();
        }

        public static void ThreadProc()
        {

            Application.Run(new Form());

        }

    }

}
