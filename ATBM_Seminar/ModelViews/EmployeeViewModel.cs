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
        #region cuong
        public Employee showDetail_Employee(string MANV, OracleConnection conn)
        {
            Employee employee = new Employee();
            employee = employee.detailEmployee(MANV, conn);

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
        #endregion

        #region Cuong tao lao 2
        public ObservableCollection<Employee> show_AllEmp(OracleConnection conn)
        {
            Employee emp = new Employee();
            ObservableCollection<Employee> list_Emp = new ObservableCollection<Employee>();
            list_Emp = emp.allEmp2(conn);

            return list_Emp;
        }

        public ObservableCollection<Employee> show_AllTP(OracleConnection conn)
        {
            Employee TP = new Employee();
            ObservableCollection<Employee> list_TP = new ObservableCollection<Employee>();
            list_TP = TP.allTruongPhong(conn);

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

        public Employee showDetail_Employee2(string MANV, OracleConnection conn)
        {
            Employee employee = new Employee();
            employee = employee.detailEmployee2(MANV, conn);

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
        #endregion
    }
}
