using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class CheckoutDAL
    {
        private Function fn = new Function();


        public DataSet GetAllUnpaidCustomers()
        {
            string query = "select customer.cid ,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where chekout = 'NO' ";

            return fn.getData(query);
        }

        public DataSet GetCustomersByName(string name)
        {
           string query = $"select customer.cid,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where cname like '{name}%' and chekout = 'NO'";
            return fn.getData(query);
        }

        public void CheckoutCustomer(int id, string room, string cdate)
        {
            string query = $"update customer set checkout = 'YES', chekout = '{cdate}' where cid = {id}; update rooms set booked = 'NO' where roomNo = '{room}';";
            fn.setData2(query);
        }
    }
}
