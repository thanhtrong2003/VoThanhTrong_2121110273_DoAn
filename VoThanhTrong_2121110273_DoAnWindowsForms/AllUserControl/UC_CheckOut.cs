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
    public partial class UC_CheckOut : UserControl
    {
        private CheckoutBLL checkoutBLL = new CheckoutBLL();
        public UC_CheckOut()
        {
            InitializeComponent();
        }

        private void UC_CheckOut_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = checkoutBLL.GetAllUnpaidCustomers().Tables[0];//Kiểm tra những khách hàng chưa thanh toán hiện lên bảng
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = checkoutBLL.GetCustomersByName(txtName.Text).Tables[0];//Kiểm tra và lấy ra kí tự đầu tiên hiển thị lên bảng
        }

        int id;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //{
            //    id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            //    txtCName.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //    txtRoom.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["roomNo"].Value.ToString();
            //}
        }
 
        public void clearAll()
        {
            txtCName.Clear();
            txtName.Clear();
            txtRoom.Clear();
            txtCheckOutDate.ResetText(); // dùng cho ngày
        }

        private void UC_CheckOut_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không phải là header
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                // Đổ dữ liệu từ dòng được chọn vào các trường nhập liệu
                id = int.Parse(row.Cells["cid"].Value.ToString());
                txtCName.Text = row.Cells["cname"].Value.ToString();
                txtRoom.Text = row.Cells["roomNo"].Value.ToString();
            }
        }

        private void btnCheckout_Click_1(object sender, EventArgs e)
        {
            if (txtCName.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    checkoutBLL.CheckoutCustomer(id, txtRoom.Text, txtCheckOutDate.Text);
                    UC_CheckOut_Load(this, null);
                    clearAll();
                }
            }
            else
            {
                MessageBox.Show("Không có khách hàng để lựa chọn", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
