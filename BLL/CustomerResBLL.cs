using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BO;
namespace BLL
{
    public class CustomerResBLL
    {
        private CustomerResDAL dal = new CustomerResDAL();

        public void AddCustomer(string name, long mobile, string nationality, string gender, DateTime dob, string idProof, string address, DateTime checkin, int roomId, string roomNo)
        {
            // Bạn có thể thêm bất kỳ logic kiểm tra nghiệp vụ nào ở đây trước khi chèn vào cơ sở dữ liệu
            dal.InsertCustomer(name, mobile, nationality, gender, dob, idProof, address, checkin, roomId, roomNo);
        }
    }
}
