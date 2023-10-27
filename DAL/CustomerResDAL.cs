using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BO;
namespace DAL
{
    public class CustomerResDAL
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ADMIN\\Documents\\dbMyHotel.mdf;Integrated Security=True;Connect Timeout=30";

        public void InsertCustomer(string name, long mobile, string nationality, string gender, DateTime dob, string idProof, string address, DateTime checkin, int roomId, string roomNo)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"insert into customer (cname, mobile, nationality, gender, dob, idproof, address, checkin, roomid) 
                          values (@name, @mobile, @national, @gender, @date, @idproof, @address, @checkin, @rid);
                          update rooms set booked = 'YES' where roomNo = @roomNo;";

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@national", nationality);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@date", dob);
                    cmd.Parameters.AddWithValue("@idproof", idProof);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@checkin", checkin);
                    cmd.Parameters.AddWithValue("@rid", roomId);
                    cmd.Parameters.AddWithValue("@roomNo", roomNo);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
