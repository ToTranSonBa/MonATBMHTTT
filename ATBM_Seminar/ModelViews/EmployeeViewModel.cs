using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.ViewModels
{
    public class EmployeeViewModel:ViewModelBase
    {
        public ObservableCollection<Employee> showEmployee_Agent(OracleConnection conn, string role_name)
        {
            Employee employee = new Employee();
            ObservableCollection<Employee> employees = employee.allEmployee(conn, role_name);

            return employees;
        }
        public Employee showDetail_Employee(OracleConnection connection, string MANV)
        {
            Employee employee = new Employee();
            employee = employee.Detail_Employee(connection,MANV);

            return employee;
        }
    }
}
