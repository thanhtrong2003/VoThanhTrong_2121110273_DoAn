
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using System.Web.UI.WebControls;
using BO;
namespace VoThanhTrong_2121110273_DoAnWindowsForms.AllUserControl
{
    public partial class UC_CustomerRes : UserControl
    {

        CustomerResBLL customerResBLL = new CustomerResBLL();
        RoomsBLL roomsBLL = new RoomsBLL();

        public UC_CustomerRes()
        {
            InitializeComponent();

        }
        public bool IsRoomAvailable(string bedType, string roomType)
        {
           return  roomsBLL.GetAvailableRooms(bedType, roomType);
        }

        public void setComboBox(List<string> items, ComboBox combo)
        {
            foreach (var item in items)
            {
                combo.Items.Add(item);
            }
        }

        private void UC_CustomerRes_Load(object sender, EventArgs e)
        {
            setComboBox(roomsBLL.GetDistinctBeds(), txtBed);
            setComboBox(roomsBLL.GetDistinctRoomTypes(), txtRoom);
            setComboBox(roomsBLL.GetDistinctRoomNos(), txtRoomNo);
        }

        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {


            txtRoom.SelectedIndex = -1;//dùng cho combobox , không có mục nào được chọn
            txtRoomNo.Items.Clear();// không có nội dung nài được chọn trong combox này
            txtPrice.Clear();

            
      
        }

        private void txtRoom_SelectedIndexChanged(object sender, EventArgs e)//Loại phòng
        {
            txtRoomNo.Items.Clear();
            List<string> roomNumbers = roomsBLL.GetRoomNumbersByType(txtBed.Text, txtRoom.Text);
            txtRoomNo.Items.AddRange(roomNumbers.ToArray());

        }

        int rid;
        private void txtRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                (string price, int roomId) = roomsBLL.GetRoomDetailsByRoomNumber(txtRoomNo.Text);
                txtPrice.Text = price;
                rid = roomId;
            }
        }
   

     
        private void btnAllotCustomer_Click(object sender, EventArgs e)
        {

            if (rid <= 0)
            {
                MessageBox.Show("Không tìm thấy mã phòng. Vui lòng chọn một phòng hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                string.IsNullOrWhiteSpace(txtNationality.Text) ||
                string.IsNullOrWhiteSpace(txtGenter.Text) ||
                string.IsNullOrWhiteSpace(txtDate.Text) ||
                string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtCheckin.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Xin vui lòng nhập đầy đủ thông tin", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
         
            customerResBLL.AddCustomer(txtName.Text, Int64.Parse(txtContact.Text), txtNationality.Text, txtGenter.Text, DateTime.Parse(txtDate.Text), txtId.Text, txtAddress.Text, DateTime.Parse(txtCheckin.Text), rid, txtRoomNo.Text);


            MessageBox.Show("Số phòng " + txtRoomNo.Text + " Đăng ký khách hàng thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearAll();
        }
        public void clearAll()
        {
            txtName.Clear();
            txtContact.Clear();
            txtNationality.Clear();
            txtGenter.SelectedIndex = -1;
            txtDate.ResetText();
            txtId.Clear();
            txtAddress.Clear();
            txtCheckin.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }

        private void UC_CustomerRes_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

 

    
    }
}
