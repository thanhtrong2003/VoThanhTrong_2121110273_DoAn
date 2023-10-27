using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAL
{
    public class RoomsDAL
    {
        Function fn = new Function();

        public DataSet GetAllRooms()
        {
            string query = "select * from rooms";
            return fn.getData(query);
        }

        public void AddRoom(string roomno, string type, string bed, Int64 price)
        {
            string query = $"Insert into rooms (roomNo,roomType,bed,price) values ('{roomno}','{type}','{bed}','{price}')";
            fn.setData2(query);
        }

        public void DeleteRoom(string roomno, string type, string bed, Int64 price)
        {
            string query = $"DELETE FROM rooms WHERE roomNo = '{roomno}' AND roomType = '{type}' AND bed = '{bed}' AND price = {price}";
            fn.setData2(query);
        }

        public void UpdateRoom(string roomno, string type, string bed, Int64 price)
        {
            string query = $"UPDATE rooms SET roomType='{type}', bed='{bed}', price='{price}' WHERE roomNo='{roomno}'";
            fn.setData2(query);
        }

        public int CheckRoomExistence(string roomno)
        {
            string query = $"SELECT COUNT(*) FROM rooms WHERE roomNo = '{roomno}'";
            return Convert.ToInt32(fn.getData(query).Tables[0].Rows[0][0]);
        }




        public bool IsRoomAvailable(string bedType, string roomType)
        {
            string query = $"SELECT COUNT(*) FROM rooms WHERE bed = '{bedType}' AND roomType = '{roomType}' AND booked = 'NO'";
            DataSet ds = fn.getData(query);
            int count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return count > 0;
        }

        public DataSet GetPriceAndRoomId(string roomNo)
        {
            string query = $"SELECT price, roomid FROM rooms WHERE roomNo = '{roomNo}'";
            return fn.getData(query);
        }


        public SqlDataReader getForCombo(String query)
        {
           return fn.getForCombo(query);
        }

        public SqlDataReader GetDistinctBeds()
        {
            return getForCombo("SELECT DISTINCT bed FROM rooms");
        }

        public SqlDataReader GetDistinctRoomTypes()
        {
            return getForCombo("SELECT DISTINCT roomType FROM rooms");
        }

        public SqlDataReader GetDistinctRoomNos()
        {
            return getForCombo("SELECT DISTINCT roomNo FROM rooms");
        }


        public SqlDataReader GetRoomNumbersByType(string bedType, string roomType)
        {
            string query = $"select roomNo from rooms where bed = '{bedType}' and roomType = '{roomType}' and booked = 'NO'";
            return fn.getForCombo(query);
        }

        public DataSet GetRoomDetailsByRoomNumber(string roomNumber)
        {
            string query = $"select price, roomid from rooms where roomNo= '{roomNumber}'";
            return fn.getData(query);
        }

    }
}