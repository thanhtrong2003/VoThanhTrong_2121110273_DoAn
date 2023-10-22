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

namespace VoThanhTrong_2121110273_DoAnWindowsForms.AllUserControl
{
    public partial class UC_CustomerRes : UserControl
    {

        Function fn = new Function();
        String query;

        public UC_CustomerRes()
        {
            InitializeComponent();

        }
        public bool IsRoomAvailable(string bedType, string roomType)
        {
            string query = $"SELECT COUNT(*) FROM rooms WHERE bed = '{bedType}' AND roomType = '{roomType}' AND booked = 'NO'";
            DataSet ds = fn.getData(query);
            int count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return count > 0;
        }

        public void setComboBox(String query , ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            if (sdr.HasRows) //kiểm tra dòng
            {
                while (sdr.Read())
                {
                    for (int i = 0; i < sdr.FieldCount; i++)
                    {
                        combo.Items.Add(sdr.GetString(i));//chuyển thành chuỗi , nếu có DateTime nó sẽ lỗi
                    }
                }
            }
       
            sdr.Close();
        }


        private void UC_CustomerRes_Load(object sender, EventArgs e)
        {
            query = "SELECT DISTINCT bed FROM rooms";
            setComboBox(query, txtBed);
            query = "SELECT DISTINCT roomType FROM rooms";
            setComboBox(query, txtRoom);
            query = "SELECT  DISTINCT roomNo FROM rooms";
            setComboBox(query, txtRoomNo);//truy vấn csdl và hiện thị dữ liệu liên quan
      


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
            query = "select roomNo from rooms where bed = '" + txtBed.Text + "' and roomType = '" + txtRoom.Text + "' and booked = 'NO' ";
            setComboBox(query, txtRoomNo);
     
        }

        int rid;
        private void txtRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomid from rooms where roomNo= '" + txtRoomNo.Text + "'";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString()); 
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

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ADMIN\\Documents\\dbMyHotel.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"insert into customer (cname, mobile, nationality, gender, dob, idproof, address, checkin, roomid) 
                          values (@name, @mobile, @national, @gender, @date, @idproof, @address, @checkin, @rid);
                          update rooms set booked = 'YES' where roomNo = @roomNo;";

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@mobile", Int64.Parse(txtContact.Text));
                    cmd.Parameters.AddWithValue("@national", txtNationality.Text);
                    cmd.Parameters.AddWithValue("@gender", txtGenter.Text);
                    cmd.Parameters.AddWithValue("@date", DateTime.Parse(txtDate.Text));
                    cmd.Parameters.AddWithValue("@idproof", txtId.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@checkin", DateTime.Parse(txtCheckin.Text));
                    cmd.Parameters.AddWithValue("@rid", rid);
                    cmd.Parameters.AddWithValue("@roomNo", txtRoomNo.Text);

                    cmd.ExecuteNonQuery();
                }
            }
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
