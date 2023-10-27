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
        CustomerResDAL customerResDAL = new CustomerResDAL();

        public void AddCustomer(CustomerResBO customer)
        {
            customerResDAL.InsertCustomer(customer);
        }
    }
}
