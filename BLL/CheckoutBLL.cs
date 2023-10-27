
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class CheckoutBLL
    {
        private CheckoutDAL checkoutDAL = new CheckoutDAL();

        public DataSet GetAllUnpaidCustomers()
        {
            return checkoutDAL.GetAllUnpaidCustomers();
        }

        public DataSet GetCustomersByName(string name)
        {
            return checkoutDAL.GetCustomersByName(name);
        }

        public void CheckoutCustomer(int id, string room, string cdate)
        {
            checkoutDAL.CheckoutCustomer(id, room, cdate);
        }
    }
}
