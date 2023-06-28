using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

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
        public string LUONG { get; set; }
        public string PHUCAP { get; set; }
        public string VAITRO { get; set; }
        public string MANQL { get; set; }
        public string PHG { get; set; }
        public ObservableCollection<Employee> allEmployee(OracleConnection connection, string role_name)
        {
            ObservableCollection<Employee> list_employee =new ObservableCollection<Employee>();
            
            DataTable result = new DataTable();

            string query = "SELECT * FROM ATBM_20H3T_22.v_ShowAllEmployee";
            OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
            adapter.Fill(result);
 

            for(int i=0; i<result.Rows.Count;i++)
            {
                DataRow row = result.Rows[i];
                if (row["VAITRO"].ToString() == role_name) {
                    Employee employee = new Employee();
                    employee.MANV = row["MANV"].ToString();
                    employee.TENNV = row["TENNV"].ToString();
                    employee.PHAI = row["PHAI"].ToString();
                    employee.NGAYSINH = row["NGAYSINH"].ToString();
                    employee.DIACHI = row["DIACHI"].ToString();
                    employee.SODT = row["SODT"].ToString();
                    employee.LUONG = (row["LUONG"]).ToString();
                    employee.PHUCAP = (row["PHUCAP"]).ToString();
                    employee.VAITRO = row["VAITRO"].ToString();
                    employee.MANQL = row["MANQL"].ToString();
                    employee.PHG = row["PHG"].ToString();
                    list_employee.Add(employee);
                }
            }

            return list_employee;
        }
        public Employee Detail_Employee(OracleConnection connection, string MANV)
        {
            Employee employee = new Employee();
            

                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_Get1Employee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter cursorParam = cmd.Parameters.Add("EMPLOYCURSOR", OracleDbType.RefCursor);
                cursorParam.Direction = ParameterDirection.Output;

                OracleParameter inputParam = new OracleParameter("MANV", OracleDbType.Char);
                inputParam.Value = MANV;
                cmd.Parameters.Add(inputParam);

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee.MANV=reader.GetString(0);
                        employee.TENNV = reader.GetString(1);
                        employee.PHAI = reader.GetString(2);
                        employee.NGAYSINH = reader.GetString(3);
                        employee.DIACHI = reader.GetString(4);
                        employee.SODT = reader.GetString(5);
                        //employee.LUONG = reader.GetString(6);
                        //employee.PHUCAP = reader.GetString(7);
                        employee.VAITRO = reader.GetString(8);
                        if (!reader.IsDBNull(9))
                        {
                            Employee employee1 = new Employee();
                            employee1 = employee1.Detail_Employee(connection,reader.GetString(9));
                            employee.MANQL = employee1.TENNV;
                        }
                        Room room=new Room();
                        room=room.detailRoom(connection, reader.GetString(10));
                        employee.PHG = room.TENPB;
                    }
                }

            
            return employee;
        }


        // get all employees in company
        public ObservableCollection<Employee> getNhanVien_DB(OracleConnection connection)
        {
            ObservableCollection<Employee> list_employee = new ObservableCollection<Employee>();

            DataTable result = new DataTable();
            //Connect conn = new Connect();

            string query = "SELECT * FROM ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN";


            OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
            adapter.Fill(result);


            for (int i = 0; i < result.Rows.Count; i++)
            {
                DataRow row = result.Rows[i];

                Employee employee = new Employee();
                employee.MANV = row["MANV"].ToString();
                employee.TENNV = row["TENNV"].ToString();
                employee.PHAI = row["PHAI"].ToString();
                employee.NGAYSINH = row["NGAYSINH"].ToString();
                employee.DIACHI = row["DIACHI"].ToString();
                employee.SODT = row["SODT"].ToString();
                employee.LUONG = Convert.ToInt32(row["LUONG"]);
                employee.PHUCAP = Convert.ToInt32(row["PHUCAP"]);
                employee.VAITRO = row["VAITRO"].ToString();
                employee.MANQL = row["MANQL"].ToString();
                employee.PHG = row["PHG"].ToString();
                list_employee.Add(employee);

            }
            return list_employee;
        }

        // get one record by where
        public Employee getOneNhanVien_DB(OracleConnection connection, string MANV)
        {
            Employee employee = null;

            string query = $"SELECT * FROM ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN WHERE MANV = '{MANV}' ";

            OracleCommand cmd = new OracleCommand(query, connection);

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read()) // Kiểm tra xem có dữ liệu trả về hay không
                {
                    employee = new Employee();

                    employee.MANV = reader.GetString(0);
                    employee.TENNV = reader.GetString(1);
                    employee.PHAI = reader.GetString(2);
                    employee.NGAYSINH = reader.GetString(3);
                    employee.DIACHI = reader.GetString(4);
                    employee.SODT = reader.GetString(5);
                    employee.LUONG = reader.GetDecimal(6);
                    employee.PHUCAP = reader.GetDecimal(7);
                    employee.VAITRO = reader.GetString(8);
                    employee.MANQL = reader["MANQL"].ToString();

                    Room room = new Room();
                    room = room.getOnePhongBan_DB(connection, reader.GetString(10));
                    employee.PHG = room.TENPB;
                }
            }
            return employee;
        }

        // update ngày sinh one employee
        public void updateNgSinhNhanVien_DB(OracleConnection connection, string MANV, string NGAYSINH)
        {
            string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET NGAYSINH = TO_DATE('{NGAYSINH}','MM/DD/YYYY') WHERE MANV = '{MANV}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.ExecuteNonQuery();

        }

        // update ngày sinh one employee
        public void updateDiaChiNhanVien_DB(OracleConnection connection, string MANV, string DIACHI)
        {
            string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET DIACHI = '{DIACHI}' WHERE MANV = '{MANV}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        // update ngày sinh one employee
        public void updateSoDTNhanVien_DB(OracleConnection connection, string MANV, string SODT)
        {
            string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET SODT = '{SODT}' WHERE MANV = '{MANV}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        // update lương one employee
        public void updateLuongNhanVien_DB(OracleConnection connection, string MANV, string LUONG)
        {
            string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET LUONG = {LUONG} WHERE MANV = '{MANV}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        // update lương one employee
        public void updatePhuCapNhanVien_DB(OracleConnection connection, string MANV, string PHUCAP)
        {
            string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET PHUCAP = {PHUCAP} WHERE MANV = '{MANV}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        // update lương one employee
        public void updateThongTinNhanVien_DB(OracleConnection connection, string MANV, string NGAYSINH, string DIACHI, string SODT)
        {
            string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET NGAYSINH = TO_DATE('{NGAYSINH}','MM/DD/YYYY'), DIACHI = '{DIACHI}', SODT = {SODT} WHERE MANV = '{MANV}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        // update lương one employee
        public void updateThongTinNhanVienNV_DB(OracleConnection connection, string NGAYSINH, string DIACHI, string SODT)
        {
            try
            {
                string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET NGAYSINH = TO_DATE('{NGAYSINH}','MM/DD/YYYY'), DIACHI = '{DIACHI}', SODT = {SODT}";
                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
