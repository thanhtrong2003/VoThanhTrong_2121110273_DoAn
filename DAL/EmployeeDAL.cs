using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DAL
{
    public class EmployeeDAL
    {

        Function fn = new Function();

        public DataSet GetEmployeeByUsernameAndPassword(string username, string password)
        {
            string query = $"select username, pass from employee where username ='{username}' and pass='{password}'";
            return fn.getData(query);
        }
    }
}
