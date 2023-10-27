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
using BO;

namespace VoThanhTrong_2121110273_DoAnWindowsForms.AllUserControl
{
    public partial class UC_Employee : UserControl
    {
        private EmployeeDetailBLL employeedetailBLL;
        public UC_Employee()
        {
            InitializeComponent();
            employeedetailBLL = new EmployeeDetailBLL();
        }

        private void UC_Employee_Load(object sender, EventArgs e)
        {
            labelToSet.Text = (employeedetailBLL.GetMaxEmployeeId() + 1).ToString();
        }


        //******************************
    

        private void btnRegistation_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "" && txtPhoneNumber.Text != "" && txtEmail.Text!="" && txtGender.Text !="" && txtUserName.Text != "" && txtPassword.Text != "")
            {
                EmployeeDetailBO emp = new EmployeeDetailBO
                {
                    ename = txtName.Text,
                    mobile = Int64.Parse(txtPhoneNumber.Text),
                    gender = txtGender.Text,
                    emailid = txtEmail.Text,
                    username = txtUserName.Text,
                    pass = txtPassword.Text
                };

                employeedetailBLL.RegisterEmployee(emp);

                clearAll();
                labelToSet.Text = (employeedetailBLL.GetMaxEmployeeId() + 1).ToString();

            }
        }
        public void clearAll()
        {
            txtName.Clear();
            txtPhoneNumber.Clear();
            txtGender.SelectedIndex = -1;
            txtEmail.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
        }

        private void tabEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabEmployee.SelectedIndex == 1)
            {
                setEmployee(guna2DataGridView2);
            }
            else if (tabEmployee.SelectedIndex == 2)
            {
                setEmployee(guna2DataGridView4);
            }
        }






        public void setEmployee(DataGridView dgv)
        {
            dgv.DataSource = employeedetailBLL.GetAllEmployees();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa không !", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    employeedetailBLL.DeleteEmployee(int.Parse(txtID.Text));
                    // Refresh data after deleting:
                    tabEmployee_SelectedIndexChanged(this, null);
                    txtID.Clear();
                }
            }
        }

        private void UC_Employee_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo người dùng đã nhấp vào một dòng, không phải tiêu đề cột
            {
                DataGridViewRow row = guna2DataGridView4.Rows[e.RowIndex]; // Lấy dòng được chọn

                // Đặt giá trị cho txtID từ cột chứa ID. Giả sử ID là cột đầu tiên, index sẽ là 0.
                txtID.Text = row.Cells[0].Value.ToString();
            }
        }
    }
}
