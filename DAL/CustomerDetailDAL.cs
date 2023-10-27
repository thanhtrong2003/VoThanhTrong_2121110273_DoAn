using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerDetailDAL
    {
        private Function fn = new Function();

        public DataSet GetAllCustomers()
        {
           String query = "select customer.cid ,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid "; // tất cả 
            return fn.getData(query);
        }

        public DataSet GetUnpaidCustomers()
        {
            string query = "select customer.cid ,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is null";//khách chưa thah toán

            return fn.getData(query);
        }

        public DataSet GetPaidCustomers()
        {
            string query = "select customer.cid ,customer.cname ,customer.mobile , customer.nationality , customer.gender , customer.dob ,customer.idproof , customer.address,customer.checkin,rooms.roomNo , rooms.roomType , rooms.bed ,rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is not null";// khách đã thanh toán
            return fn.getData(query);
        }
    }
}
