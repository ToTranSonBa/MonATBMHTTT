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
        public ObservableCollection<Employee> show_AllEmp(OracleConnection conn)
        {
            Employee emp = new Employee();
            ObservableCollection<Employee> list_Emp = new ObservableCollection<Employee>();
            list_Emp = emp.allEmp(conn);

            return list_Emp;
        }

        public ObservableCollection<Employee> show_AllTP(OracleConnection conn)
        {
            Employee TP = new Employee();
            ObservableCollection<Employee> list_TP = new ObservableCollection<Employee>();
            list_TP = TP.allTruongPhong( conn);

            return list_TP;
        }

        public ObservableCollection<Employee> show_AllQL(OracleConnection conn)
        {
            Employee QL = new Employee();
            ObservableCollection<Employee> list_QL = new ObservableCollection<Employee>();
            list_QL = QL.allQLTrucTiep(conn);

            return list_QL;
        }

        public ObservableCollection<Employee> show_AllVaiTro(OracleConnection conn)
        {
            Employee VaiTro = new Employee();
            ObservableCollection<Employee> list_VaiTro = new ObservableCollection<Employee>();
            list_VaiTro = VaiTro.allVaiTro(conn);

            return list_VaiTro;
        }

        public Employee showDetail_Employee(string MANV, OracleConnection conn)
        {
            Employee employee = new Employee();
            employee = employee.detailEmployee(MANV, conn);

            return employee;
        }

        public bool updateEmployee(OracleConnection connection, string ma, string ht, string p, string ns, string dc, string sdt, string vt, string ql, string ph)
        {
            Employee emp = new Employee();
            try
            {
                return emp.Update(connection, ma, ht, p, ns, dc, sdt, vt, ql, ph);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool insertEmployee(OracleConnection connection, string ma, string ht, string p, string ns, string dc, string sdt, string vt, string ql, string ph)
        {
            Employee emp = new Employee();
            try
            {
                return emp.Insert(connection, ma, ht, p, ns, dc, sdt, vt, ql, ph);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
