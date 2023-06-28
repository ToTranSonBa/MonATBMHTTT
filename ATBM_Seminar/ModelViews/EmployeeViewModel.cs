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
        //
        public ObservableCollection<Employee> nhanVien_BL(OracleConnection connection)
        {
            Employee employee = new Employee();
            ObservableCollection<Employee> employees = employee.getNhanVien_DB(connection);

            return employees;
        }
        public Employee getOneNhanVien_BL(OracleConnection connection, string MANV)
        {
            Employee employee = new Employee();
            employee = employee.getOneNhanVien_DB(connection, MANV);

            return employee;
        }
        // update nhân viên
        public void updateThongTinNhanVien_BL(OracleConnection connection, string MANV, string NGAYSINH, string DIACHI, string SODT)
        {
            Employee nhanVien = new Employee();
            try
            {
                nhanVien.updateThongTinNhanVien_DB(connection, MANV, NGAYSINH, DIACHI, SODT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // update nhân viên
        public void updateThongTinNhanVienNV_BL(OracleConnection connection, string NGAYSINH, string DIACHI, string SODT)
        {
            Employee nhanVien = new Employee();
            try
            {
                nhanVien.updateThongTinNhanVienNV_DB(connection, NGAYSINH, DIACHI, SODT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // update tài chính nhân viên
        public void updateTaiChinhNhanVien_BL(OracleConnection connection, string MANV, string LUONG, string PHUCAP)
        {
            Employee nhanVien = new Employee();
            try
            {
                if (LUONG != null)
                {
                    nhanVien.updateLuongNhanVien_DB(connection, MANV, LUONG);
                }
                if (PHUCAP != null)
                {
                    nhanVien.updatePhuCapNhanVien_DB(connection, MANV, PHUCAP);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
