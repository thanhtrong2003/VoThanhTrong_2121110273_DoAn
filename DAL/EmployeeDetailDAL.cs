using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmployeeDetailDAL
    {
         Function fn = new Function();

        public int GetMaxEmployeeId()
        {
            string query = "select max(eid) from employee";
            var ds = fn.getData(query);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            return 0;
        }

        public void RegisterEmployee(EmployeeDetailBO employeeDetailBO)
        {
            string query = $"insert into employee (ename, mobile, gender, emailid, username, pass) values ('{employeeDetailBO.ename}', {employeeDetailBO.mobile}, '{employeeDetailBO.gender}', '{employeeDetailBO.emailid}', '{employeeDetailBO.username}', '{employeeDetailBO.pass}')";
            fn.setData2(query);
        }

        public DataSet GetAllEmployees()
        {
            string query = "select * from employee";
            return fn.getData(query);
        }

        public void DeleteEmployee(int id)
        {
            string query = $"delete from employee where eid = {id}";
            fn.setData2(query);
        }
    }
}
