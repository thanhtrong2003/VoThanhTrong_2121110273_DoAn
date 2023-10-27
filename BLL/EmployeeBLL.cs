using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmployeeBLL
    {
        private EmployeeDAL EmployeeDAL = new EmployeeDAL();

        public bool ValidateLogin(string username, string password)
        {
            DataSet ds = EmployeeDAL.GetEmployeeByUsernameAndPassword(username, password);
            return ds.Tables[0].Rows.Count != 0;
        }
    }
}
