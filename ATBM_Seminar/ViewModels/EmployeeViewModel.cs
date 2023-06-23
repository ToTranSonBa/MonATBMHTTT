using ATBM_Seminar.Models;
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
        public ObservableCollection<Employee> showEmployee_Agent(string role_name)
        {
            Employee employee = new Employee();
            ObservableCollection<Employee> employees = employee.allEmployee(role_name);

            return employees;
        }
        public Employee showDetail_Employee(string MANV)
        {
            Employee employee = new Employee();
            employee = employee.Detail_Employee(MANV);

            return employee;
        }
    }
}
