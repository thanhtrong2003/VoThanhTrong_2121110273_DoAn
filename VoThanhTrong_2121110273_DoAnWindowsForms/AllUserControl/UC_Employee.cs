using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoThanhTrong_2121110273_DoAnWindowsForms.AllUserControl
{
    public partial class UC_Employee : UserControl
    {
        Function fn = new Function();
        String query;
        public UC_Employee()
        {
            InitializeComponent();
        }

        private void UC_Employee_Load(object sender, EventArgs e)
        {
            getMaxId();
        }


        //******************************
        public void getMaxId()
        {
            query = "select max(eid) from employee";
            DataSet ds = fn.getData(query);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                Int64 num = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                labelToSet.Text = (num + 1).ToString();// lấy ra giá trị lớn nhất của bảng e , sau đó + thêm 1 và hiển thị
            }    

        }

        private void btnRegistation_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "" && txtPhoneNumber.Text != "" && txtEmail.Text!="" && txtGender.Text !="" && txtUserName.Text != "" && txtPassword.Text != "")
            {
                String name = txtName.Text;
                Int64 phone = Int64.Parse(txtPhoneNumber.Text);
                String gender = txtGender.Text;
                String email = txtEmail.Text;
                String username = txtUserName.Text;
                String password = txtPassword.Text;


                query = "insert into employee (ename,mobile,gender,emailid,username,pass) values ('" + name + "'," + phone + ",'" + gender + "','" + email + "','" + username + "','" + password + "')";
                fn.setData(query, "Đăng ký nhân viên thành công");

                clearAll();
                getMaxId();
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
           if(tabEmployee.SelectedIndex == 1)
            {
                setEmployee(guna2DataGridView2);
            }else if(tabEmployee.SelectedIndex == 2)
            {
                setEmployee(guna2DataGridView4);
            }
        }

        public void setEmployee(DataGridView dgv)
        {
            query = "select * from employee";
            DataSet ds = fn.getData(query);
            dgv.DataSource = ds.Tables[0];
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa không !", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from employee where eid = " + txtID.Text + "";
                    fn.setData(query, "Thông tin nhân viên đã được xóa");
                    tabEmployee_SelectedIndexChanged(this, null);
                    txtID.Clear(); // Xóa giá trị trong txtID sau khi xóa
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
