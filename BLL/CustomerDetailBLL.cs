using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class CustomerDetailBLL
    {
        private CustomerDetailDAL customerdetail = new CustomerDetailDAL();

        public DataSet GetAllCustomers()
        {
            return customerdetail.GetAllCustomers();
        }

        public DataSet GetUnpaidCustomers()
        {
            return customerdetail.GetUnpaidCustomers();
        }

        public DataSet GetPaidCustomers()
        {
            return customerdetail.GetPaidCustomers();
        }
    }
}
