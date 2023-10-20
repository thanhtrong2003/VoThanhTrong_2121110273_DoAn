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

        //object sender: Tham chiếu đến đối tượng kích hoạt sự kiện, trong trường hợp này là UC_AddRoom.
        //EventArgs e: Chứa thông tin chi tiết về sự kiện. Trong trường hợp này, không có thông tin chi tiết nào được sử dụng.
        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            query = "select * from rooms";//lấy tất cả dữ liệu của bảng rooms
            DataSet ds = fn.getData(query);//Tạo đối tượng và truyền tham số truy vấn
            DataGridView1.DataSource = ds.Tables[0];//hiện thị dữ liệu dưới dạng bảng

        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem tất cả các trường nhập liệu có được điền đầy đủ hay không
            if (txtRoomNo.Text != "" && txtRoomType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                // Lấy giá trị từ các trường nhập liệu
                string roomno = txtRoomNo.Text;
                string type = txtRoomType.Text;
                string bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);// Chuyển đổi giá từ chuỗi sang số nguyên lớn


                // Tạo truy vấn SQL để thêm dữ liệu vào cơ sở dữ liệu
                query = "Insert into rooms (roomNo,roomType,bed,price) values ('" + roomno + "','" + type + "','" + bed + "','" + price + "')";

                fn.setData(query, "Đã thêm phòng"); //dựa vào phương thức setData có dùng Message

                // Tải lại dữ liệu (có lẽ để cập nhật giao diện sau khi thêm phòng)
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
            txtRoomNo.Clear(); // dùng cho TextBox
            txtRoomType.SelectedIndex = -1; //dùng cho Combo Box ( không trảng thái nào được chọn )
            txtBed.SelectedIndex = -1; //dùng cho Combo Box ( không trảng thái nào được chọn )
            txtPrice.Clear();
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll(); //xóa khi chuyển từ mục này sang muc khác ( mất tiêu điểm )

        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);  // Nhận tiêu điểm khi chuyển từ mục khác sang và Load dữ liệu
        }

        private void btbDeleteRoom_Click(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows.Count > 0) // kiểm tra dòng mình chọn phải có
            {
                // Lấy dòng đang được chọn
                DataGridViewRow row = DataGridView1.SelectedRows[0];

                string roomno = row.Cells["roomNo"].Value.ToString();
                string type = row.Cells["roomType"].Value.ToString();
                string bed = row.Cells["bed"].Value.ToString();
                string price = row.Cells["price"].Value.ToString();

                query = $"DELETE FROM rooms WHERE roomNo = '{roomno}' AND roomType = '{type}' AND bed = '{bed}' AND price = {price}";
                fn.setData(query, "Đã xóa phòng"); // có messagebox

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
