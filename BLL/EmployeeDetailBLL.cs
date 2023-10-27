using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace BLL
{
    public class EmployeeDetailBLL
    {
        private EmployeeDetailDAL employeedetailDAL;

        public EmployeeDetailBLL()
        {
            employeedetailDAL = new EmployeeDetailDAL();
        }

        public int GetMaxEmployeeId()
        {
            return employeedetailDAL.GetMaxEmployeeId();
        }

        public void RegisterEmployee(EmployeeDetailBO employeeDetailBO)
        {
            employeedetailDAL.RegisterEmployee(employeeDetailBO);
        }
        public DataTable GetAllEmployees()
        {
            return employeedetailDAL.GetAllEmployees().Tables[0];
        }

        public void DeleteEmployee(int id)
        {
            employeedetailDAL.DeleteEmployee(id);
        }
    }
}