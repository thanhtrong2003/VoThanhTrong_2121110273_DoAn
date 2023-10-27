using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
namespace VoThanhTrong_2121110273_DoAnWindowsForms.AllUserControl
{
    public partial class UC_CustomerDetails : UserControl
    {
        private CustomerDetailBLL customerdetail = new CustomerDetailBLL();
        public UC_CustomerDetails()
        {
            InitializeComponent();
        }

        private void txtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtSearchBy.SelectedIndex == 0)
            {
                guna2DataGridView1.DataSource = customerdetail.GetAllCustomers().Tables[0];
            }
            else if(txtSearchBy.SelectedIndex == 1)
            {
                guna2DataGridView1.DataSource = customerdetail.GetUnpaidCustomers().Tables[0];
            }
            else if (txtSearchBy.SelectedIndex == 2)
            {
                guna2DataGridView1.DataSource = customerdetail.GetPaidCustomers().Tables[0];
            }
        }


        private void UC_CustomerDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
