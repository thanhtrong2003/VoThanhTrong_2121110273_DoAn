using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace DAL
{
    public class CustomerResDAL
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ADMIN\\Documents\\dbMyHotel.mdf;Integrated Security=True;Connect Timeout=30";

        public void InsertCustomer(CustomerResBO customer)
        {
          
         
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = @"insert into customer (cname, mobile, nationality, gender, dob, idproof, address, checkin, roomid) 
                                       values (@cname, @mobile, @nationality, @gender, @dob, @idproof, @address, @checkin, @rid)";

                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.Parameters.AddWithValue("@cname", customer.cname);

                        cmd.Parameters.AddWithValue("@mobile", customer.mobile);
                        cmd.Parameters.AddWithValue("@nationality", customer.nationality);
                        cmd.Parameters.AddWithValue("@gender", customer.gender);
                        cmd.Parameters.AddWithValue("@dob", customer.dob);
                        cmd.Parameters.AddWithValue("@idproof", customer.idproof);
                        cmd.Parameters.AddWithValue("@address", customer.address);
                        cmd.Parameters.AddWithValue("@checkin", customer.checkin);
                        cmd.Parameters.AddWithValue("@rid", customer.roomid);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý hoặc ghi log ngoại lệ tại đây
                throw;  // hoặc bạn có thể re-throw ngoại lệ nếu muốn
            }
        }
    }
}
