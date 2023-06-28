using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATBM_Seminar.ViewModels
{


    public class EmployeeViewModel : ViewModelBase
    {

        public Employee showDetail_Employee(string MANV, OracleConnection conn)
        {
            Employee employee = new Employee();
            employee = employee.detailEmployee(MANV,conn);

            return employee;
        }

        public ObservableCollection<Employee> showEmp(OracleConnection conn)
        {
            Employee emp = new Employee();
            ObservableCollection<Employee> list_dean = new ObservableCollection<Employee>();
            list_dean = emp.allEmp(conn);

            return list_dean;
        }

        public void updateQuanLy(OracleConnection connection, string NgaySinh, string DiaChi, string SoDT)
        {
            Employee dean = new Employee();
            try
            {
                dean.update(connection, NgaySinh, DiaChi, SoDT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
