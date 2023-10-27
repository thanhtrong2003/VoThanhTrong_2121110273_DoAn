using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BLL
{
    public class RoomsBLL
    {
        RoomsDAL dal = new RoomsDAL();
        Function fn = new Function();
     

        public DataSet GetAllRooms()
        {
            return dal.GetAllRooms();
        }

        public void AddRoom(string roomno, string type, string bed, Int64 price)
        {
            dal.AddRoom(roomno, type, bed, price);
        }

        public void DeleteRoom(string roomno, string type, string bed, Int64 price)
        {
            dal.DeleteRoom(roomno, type, bed, price);
        }

        public void UpdateRoom(string roomno, string type, string bed, Int64 price)
        {
            dal.UpdateRoom(roomno, type, bed, price);
        }

        public int CheckRoomExistence(string roomno)
        {
            return dal.CheckRoomExistence(roomno);
        }

        public bool GetAvailableRooms(string bedType, string roomType)
        {
            return dal.IsRoomAvailable(bedType, roomType);
        }

        public DataSet GetPriceAndRoomId(string roomNo)
        {
            return dal.GetPriceAndRoomId(roomNo);
        }


        public void setComboBox(String query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            //if (sdr.HasRows) //kiểm tra dòng
            //{
            //    while (sdr.Read())
            //    {
            //        for (int i = 0; i < sdr.FieldCount; i++)
            //        {
            //            combo.Items.Add(sdr.GetString(i));//chuyển thành chuỗi , nếu có DateTime nó sẽ lỗi
            //        }
            //    }
            //}

            //sdr.Close();
        }

        public List<string> GetDistinctBeds()
        {
            return ConvertToList(dal.GetDistinctBeds());
        }

        public List<string> GetDistinctRoomTypes()
        {
            return ConvertToList(dal.GetDistinctRoomTypes());
        }

        public List<string> GetDistinctRoomNos()
        {
            return ConvertToList(dal.GetDistinctRoomNos());
        }

        private List<string> ConvertToList(SqlDataReader reader)
        {
            List<string> items = new List<string>();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    items.Add(reader.GetString(i));
                }
            }
            reader.Close();
            return items;
        }


        public List<string> GetRoomNumbersByType(string bedType, string roomType)
        {
            SqlDataReader reader = dal.GetRoomNumbersByType(bedType, roomType);
            List<string> roomNumbers = new List<string>();
            while (reader.Read())
            {
                roomNumbers.Add(reader.GetString(0));
            }
            reader.Close();
            return roomNumbers;
        }

        public (string, int) GetRoomDetailsByRoomNumber(string roomNumber)
        {
            DataSet ds = dal.GetRoomDetailsByRoomNumber(roomNumber);
            string price = ds.Tables[0].Rows[0][0].ToString();
            int rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());
            return (price, rid);
        }

    }
}