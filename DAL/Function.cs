using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;



namespace DAL
{
    public class Function
    {
        protected SqlConnection getConnection()
        {
            //Tạo đối tượng kết nối 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ADMIN\\Documents\\dbMyHotel.mdf;Integrated Security=True;Connect Timeout=30";
            return con;
        }

        //Data set là kiểu trả về của phương thức , biểu thị phương thức này là một đối tượng
        //getData là tên phương thức
        //String query là tham số truyền vào , biểu thị câu truy vấn SQL
        public DataSet getData(string query)
        {
            SqlConnection con = getConnection();//Tạo đối tượng kết nối bằng cách gọi phương thức getConnection
            SqlCommand cmd = new SqlCommand();//Tạo đối tượng SqlCommand dùng để thực thi câu lệnh hoặc thủ tục lưu trên Sql
            cmd.Connection = con;//Thiết lập kết nối cho đối tượng SqlCommand
            cmd.CommandText = query;//Thiết lập kết nối cho câu truy vấn
            SqlDataAdapter da = new SqlDataAdapter(cmd);//Tạo đối tượng SqlDataAdapter đưa vào tham số SqlCommand làm cầu nối với SetData và cập nhật dữ liệu
            DataSet ds = new DataSet();//Tạo đối tượng DataSet mới
            da.Fill(ds);//SqlDataAdapter tạo cầu nối và đỗ dữ liệu vào DataSet
            return ds;//Trả về DataSet chứa dữ liệu truy vấn
        }

        //Hàm thiết lập dữ liệu
        public void setData(String query, string message)
        {
            SqlConnection con = getConnection();//Tạo đối tượng kết nối 
            SqlCommand cmd = new SqlCommand();//Tạo đối tượng thực thi
            cmd.Connection = con;//Tạo kết nối cho SqlCOmmand
            con.Open();//Mở kết nối đến cơ sở dữ liệu.
            cmd.CommandText = query;//Thiết lập kết nối câu truye vấn
            cmd.ExecuteNonQuery();//Thực thi (câu này không dùng cho Insert , update , delete)
            con.Close();

        }
        public void setData2(String query)
        {
            SqlConnection con = getConnection();//Tạo đối tượng kết nối 
            SqlCommand cmd = new SqlCommand();//Tạo đối tượng thực thi
            cmd.Connection = con;//Tạo kết nối cho SqlCOmmand
            con.Open();//Mở kết nối đến cơ sở dữ liệu.
            cmd.CommandText = query;//Thiết lập kết nối câu truye vấn
            cmd.ExecuteNonQuery();//Thực thi (câu này không dùng cho Insert , update , delete)
            con.Close();


        }

        //là kiểu trả về của phương thức, biểu thị phương thức này trả về một đối tượng SqlDataReader.
        public SqlDataReader getForCombo(String query)
        {
            SqlConnection con = getConnection(); //Tạo đối tượng kết nối , gọi phương thức get
            SqlCommand cmd = new SqlCommand();// Tao đối tượng thực thi
            cmd.Connection = con;//thiết lập kết nối
            con.Open();//Mở kết nối đến cơ sở dữ liệu.
            cmd = new SqlCommand(query, con);//Khởi tạo lại đối tượng SqlCommand với câu truy vấn và kết nối đã được mở.
            SqlDataReader sdr = cmd.ExecuteReader(); //Thực thi câu truy vấn và lấy về kết quả vào đối tượng SqlDataReader.
            return sdr;
        }
    }
}
