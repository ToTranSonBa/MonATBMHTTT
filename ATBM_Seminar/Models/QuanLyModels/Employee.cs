using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.Models
{
    public class Employee
    {
        public string MANV { get; set; }

        public string TENNV { get; set; }
        public string PHAI { get; set; }
        public string NGAYSINH { get; set; }
        public string DIACHI { get; set; }
        public string SODT { get; set; }
        public string VAITRO { get; set; }
        public string PHONG { get; set; }
        public string LUONG { get; set; }
        public string PHUCAP { get; set; }

        public ObservableCollection<Employee> allEmp(OracleConnection conn)
        {

            try
            {
                ObservableCollection<Employee> list_emp = new ObservableCollection<Employee>();
                string SQLcontext = $"SELECT * FROM ATBM_20H3T_22.ATBMHTTT_VIEW_NHANVIEN WHERE MANQL = sys_context('userenv','session_user') OR MANV = sys_context('userenv','session_user')";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, conn))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            list_emp.Add(new Employee
                            {

                                MANV = reader.GetString(reader.GetOrdinal("MANV")),
                                TENNV = reader.GetString(reader.GetOrdinal("TENNV")),
                                VAITRO = reader.GetString(reader.GetOrdinal("VAITRO")),
                            });
                            i++;
                        }
                    }
                }
                return list_emp;

            }
            catch
            {
                return new ObservableCollection<Employee>();
            }

        }

        public Employee detailEmployee(string MaNV, OracleConnection conn)
        {

            Employee employee = new Employee();


            OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_GetDetalEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter cursorParam = cmd.Parameters.Add("EMP", OracleDbType.RefCursor);
            cursorParam.Direction = ParameterDirection.Output;

            OracleParameter inputParam = new OracleParameter("Ma", OracleDbType.Char);
            inputParam.Value = MaNV;
            cmd.Parameters.Add(inputParam);

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                int ordinal1;
                try
                {
                    ordinal1 = reader.GetOrdinal("LUONG");
                }
                catch (IndexOutOfRangeException)
                {
                    ordinal1 = -1; // Gán giá trị âm để đại diện cho cột không tồn tại
                }
                while (reader.Read())
                {
                    employee.MANV = reader.GetString(reader.GetOrdinal("MANV"));
                    employee.TENNV = reader.GetString(reader.GetOrdinal("TENNV"));
                    employee.PHAI = reader.GetString(reader.GetOrdinal("PHAI"));
                    employee.NGAYSINH = reader.GetString(reader.GetOrdinal("NGAYSINH"));
                    employee.DIACHI = reader.GetString(reader.GetOrdinal("DIACHI"));
                    employee.SODT = reader.GetString(reader.GetOrdinal("SODT"));
                    employee.VAITRO = reader.GetString(reader.GetOrdinal("VAITRO"));
                    employee.PHONG = reader.GetString(reader.GetOrdinal("TENPB"));

                    if (ordinal1 >= 0 && !reader.IsDBNull(ordinal1))
                    {
                        employee.LUONG = reader.GetString(ordinal1);
                    }
                    if(ordinal1 >= 0 && !reader.IsDBNull(reader.GetOrdinal("PHUCAP")))
                    {
                        employee.PHUCAP = reader.GetString(reader.GetOrdinal("PHUCAP"));
                    }
                }

            }
            return employee;

        }

        public void update(OracleConnection connection, string NgaySinh, string DiaChi, string SoDT)
        {
            OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_UPDATEQUANLY", connection);
            cmd.CommandType = CommandType.StoredProcedure; ;

            OracleParameter inputParam2 = new OracleParameter("NS", OracleDbType.NVarchar2);
            inputParam2.Value = NgaySinh;
            cmd.Parameters.Add(inputParam2);

            OracleParameter inputParam3 = new OracleParameter("DC", OracleDbType.Char);
            inputParam3.Value = DiaChi;
            cmd.Parameters.Add(inputParam3);

            OracleParameter inputParam4 = new OracleParameter("SDT", OracleDbType.Char);
            inputParam4.Value = SoDT;
            cmd.Parameters.Add(inputParam4);

            OracleDataReader reader = cmd.ExecuteReader();

        }
    }
}
