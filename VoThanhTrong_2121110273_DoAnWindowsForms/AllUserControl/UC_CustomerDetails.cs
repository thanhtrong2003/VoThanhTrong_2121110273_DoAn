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
    public partial class UC_CustomerDetails : UserControl
    {
        Function fn = new Function();
        String query;
        public UC_CustomerDetails()
        {
            InitializeComponent();
        }

        private void txtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtSearchBy.SelectedIndex == 0)
            {
                query = "select customer.cid ,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid "; // tất cả 
                getRecord(query);
            }
            else if(txtSearchBy.SelectedIndex == 1)
            {
                query = "select customer.cid ,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is null";//khách chưa thah toán
                getRecord(query);
            }
            else if (txtSearchBy.SelectedIndex == 2)
            {
                query = "select customer.cid ,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is not null";// khách đã thanh toán
                getRecord(query);
            }
        }

        private void getRecord(String query)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }

        private void UC_CustomerDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
