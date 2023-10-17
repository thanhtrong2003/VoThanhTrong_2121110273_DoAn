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
    public partial class UC_AddRoom : UserControl
    {

        Function fn = new Function();
        String query;

        public UC_AddRoom()
        {
            InitializeComponent();
        }

        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            query = "select * from rooms";
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];

        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
           if(txtRoomNo.Text != "" && txtRoomType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                string roomno = txtRoomNo.Text;
                string type = txtRoomType.Text;
                string bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                query = "Insert into rooms (roomNo,roomType,bed,price) values ('" + roomno + "','" + type + "','" + bed + "','" + price + "')";

                fn.setData(query, "Đã thêm phòng");

                UC_AddRoom_Load(this, null);
                clearAll();
            }
            else
            {
                MessageBox.Show("Xin vui lòng nhập đủ thông tin", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void clearAll()
        {
            txtRoomNo.Clear();
            txtRoomType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll();

        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);
        }

        private void btbDeleteRoom_Click(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows.Count > 0)
            {
                // Lấy dòng đang được chọn
                DataGridViewRow row = DataGridView1.SelectedRows[0];

                string roomno = row.Cells["roomNo"].Value.ToString();
                string type = row.Cells["roomType"].Value.ToString();
                string bed = row.Cells["bed"].Value.ToString();
                string price = row.Cells["price"].Value.ToString();

                query = $"DELETE FROM rooms WHERE roomNo = '{roomno}' AND roomType = '{type}' AND bed = '{bed}' AND price = {price}";
                fn.setData(query, "Đã xóa phòng");

                // Cập nhật DataGridView
                UC_AddRoom_Load(this, null);
            }

        }

        private void txtRoomNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này
                MessageBox.Show("Vui lòng chỉ nhập số cho số phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtRoomNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
