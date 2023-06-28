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
        public string PHONG { get; set; }
        public string TENNQL { get; set; }
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

            string query = "SELECT MANV, tennv, phai, ngaysinh, diachi, sodt, vaitro, MANQL, PHG, atbm_20h3t_22.decryto_function(LUONG, MANV) LUONG, atbm_20h3t_22.decryto_function(PHUCAP, MANV) PHUCAP FROM ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN";


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
                employee.LUONG = (row["LUONG"]).ToString();
                employee.PHUCAP = (row["PHUCAP"]).ToString();
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

            string query = $"SELECT MANV, tennv, phai, ngaysinh, diachi, sodt, vaitro, MANQL, PHG, atbm_20h3t_22.decryto_function(LUONG, MANV) LUONG, atbm_20h3t_22.decryto_function(PHUCAP, MANV) PHUCAP FROM ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN WHERE MANV = '{MANV}' ";
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
                    employee.LUONG = reader["LUONG"].ToString();
                    employee.PHUCAP = reader["PHUCAP"].ToString();
                    employee.VAITRO = reader["VAITRO"].ToString();
                    employee.MANQL = reader["MANQL"].ToString();

                    Room room = new Room();
                    room = room.getOnePhongBan_DB(connection, reader["PHG"].ToString());
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
            string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET NGAYSINH = TO_DATE('{NGAYSINH}','MM/DD/YYYY'), DIACHI = '{DIACHI}', SODT = '{SODT}' WHERE MANV = '{MANV}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        // update lương one employee
        public void updateThongTinNhanVienNV_DB(OracleConnection connection, string NGAYSINH, string DIACHI, string SODT)
        {
            try
            {
                string query = $"UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN SET NGAYSINH = TO_DATE('{NGAYSINH}','MM/DD/YYYY'), DIACHI = '{DIACHI}', SODT = '{SODT}'";
                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region CUong
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
                    if (ordinal1 >= 0 && !reader.IsDBNull(reader.GetOrdinal("PHUCAP")))
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
        #endregion

        #region Cuong Tao lao 2
        public ObservableCollection<Employee> allEmp2(OracleConnection conn)
        {
            ObservableCollection<Employee> list_employee = new ObservableCollection<Employee>();

            DataTable result = new DataTable();

            string query = "SELECT * FROM ATBM_20H3T_22.ATBMHTTT_VIEW_NHANVIEN";
            OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(result);


            for (int i = 0; i < result.Rows.Count; i++)
            {
                DataRow row = result.Rows[i];
                Employee employee = new Employee();
                employee.MANV = row["MANV"].ToString();
                employee.TENNV = row["TENNV"].ToString();
                employee.VAITRO = row["VAITRO"].ToString();
                employee.PHG = row["PHG"].ToString();

                if (row["MANQL"].ToString() != "")
                {
                    Employee employee1 = new Employee();
                    employee1 = employee1.detailEmployee2(row["MANQL"].ToString(), conn);
                    employee.TENNQL = employee1.TENNV;
                    employee.MANQL = row["MANQL"].ToString();
                }

                list_employee.Add(employee);
            }

            return list_employee;
        }

        public Employee detailEmployee2(string Ma, OracleConnection conn)
        {
            Employee employee = new Employee();

            using (OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_GetDetalEmployee", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter cursorParam = cmd.Parameters.Add("EMP", OracleDbType.RefCursor);
                cursorParam.Direction = ParameterDirection.Output;

                OracleParameter inputParam1 = new OracleParameter("Ma", OracleDbType.NVarchar2);
                inputParam1.Value = Ma;
                cmd.Parameters.Add(inputParam1);
                OracleDataReader reader = cmd.ExecuteReader();
                int ordinal1 = -1;
                int ordinal2 = -1;

                try
                {
                    ordinal1 = reader.GetOrdinal("LUONG");
                }
                catch (IndexOutOfRangeException)
                {
                    ordinal1 = -1; // Gán giá trị âm để đại diện cho cột không tồn tại
                }

                try
                {
                    ordinal2 = reader.GetOrdinal("PHUCAP");
                }
                catch (IndexOutOfRangeException)
                {
                    ordinal2 = -1; // Gán giá trị âm để đại diện cho cột không tồn tại
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

                    if (ordinal2 >= 0 && !reader.IsDBNull(ordinal2))
                    {
                        employee.PHUCAP = reader.GetString(ordinal2);
                    }
                }
                reader.Close();
            }

            return employee;
        }


        public ObservableCollection<Employee> allTruongPhong(OracleConnection conn)
        {
            ObservableCollection<Employee> list_TP = new ObservableCollection<Employee>();

            DataTable result = new DataTable();
            string query = $"SELECT * FROM ATBM_20H3T_22.v_ShowAllTruongPhong";
            OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(result);

            for (int i = 0; i < result.Rows.Count; i++)
            {
                Employee trPhg = new Employee();

                DataRow row = result.Rows[i];
                trPhg.TENNV = row["TENNV"].ToString();
                trPhg.MANV = row["MANV"].ToString();

                list_TP.Add(trPhg);
            }

            return list_TP;
        }

        public ObservableCollection<Employee> allQLTrucTiep(OracleConnection conn)
        {
            ObservableCollection<Employee> list_QL = new ObservableCollection<Employee>();

            DataTable result = new DataTable();
            string query = $"SELECT * FROM ATBM_20H3T_22.v_ShowAllQuanLyTrucTiep";
            OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(result);

            for (int i = 0; i < result.Rows.Count; i++)
            {
                Employee ql = new Employee();

                DataRow row = result.Rows[i];
                ql.TENNV = row["TENNV"].ToString();
                ql.MANV = row["MANV"].ToString();

                list_QL.Add(ql);
            }

            return list_QL;
        }

        public ObservableCollection<Employee> allVaiTro(OracleConnection conn)
        {
            ObservableCollection<Employee> list_VaiTro = new ObservableCollection<Employee>();

            DataTable result = new DataTable();

            string query = "SELECT distinct VAITRO FROM ATBM_20H3T_22.ATBMHTTT_VIEW_NHANVIEN";
            OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(result);


            for (int i = 0; i < result.Rows.Count; i++)
            {
                DataRow row = result.Rows[i];
                Employee employee = new Employee();
                employee.VAITRO = row["VAITRO"].ToString();

                list_VaiTro.Add(employee);
            }

            return list_VaiTro;
        }


        public bool Update(OracleConnection connection, string ma, string ht, string p, string ns, string dc, string sdt, string vt, string ql, string ph)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_UPDATEEMPLOYEE", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter inputParam1 = new OracleParameter("ma", OracleDbType.NVarchar2);
                inputParam1.Value = ma;
                cmd.Parameters.Add(inputParam1);

                OracleParameter inputParam2 = new OracleParameter("ht", OracleDbType.Char);
                inputParam2.Value = ht;
                cmd.Parameters.Add(inputParam2);

                OracleParameter inputParam3 = new OracleParameter("p", OracleDbType.Char);
                inputParam3.Value = p;
                cmd.Parameters.Add(inputParam3);

                OracleParameter inputParam4 = new OracleParameter("ns", OracleDbType.Char);
                inputParam4.Value = ns;
                cmd.Parameters.Add(inputParam4);

                OracleParameter inputParam5 = new OracleParameter("dc", OracleDbType.Char);
                inputParam5.Value = dc;
                cmd.Parameters.Add(inputParam5);

                OracleParameter inputParam6 = new OracleParameter("sdt", OracleDbType.Char);
                inputParam6.Value = sdt;
                cmd.Parameters.Add(inputParam6);

                OracleParameter inputParam7 = new OracleParameter("vt", OracleDbType.Char);
                inputParam7.Value = vt;
                cmd.Parameters.Add(inputParam7);

                OracleParameter inputParam8 = new OracleParameter("ql", OracleDbType.Char);
                inputParam8.Value = ql;
                cmd.Parameters.Add(inputParam8);

                OracleParameter inputParam9 = new OracleParameter("ph", OracleDbType.Char);
                inputParam9.Value = ph;
                cmd.Parameters.Add(inputParam9);

                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                }
                return true;
            }
            catch
            {
                return false;
            }


        }


        public bool Insert(OracleConnection connection, string ma, string ht, string p, string ns, string dc, string sdt, string vt, string ql, string ph)
        {

            try
            {
                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_INSERTMPLOYEE", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter inputParam1 = new OracleParameter("ma", OracleDbType.NVarchar2);
                inputParam1.Value = ma;
                cmd.Parameters.Add(inputParam1);

                OracleParameter inputParam2 = new OracleParameter("ht", OracleDbType.NVarchar2);
                inputParam2.Value = ht;
                cmd.Parameters.Add(inputParam2);

                OracleParameter inputParam3 = new OracleParameter("p", OracleDbType.Char);
                inputParam3.Value = p;
                cmd.Parameters.Add(inputParam3);

                OracleParameter inputParam4 = new OracleParameter("ns", OracleDbType.Char);
                inputParam4.Value = ns;
                cmd.Parameters.Add(inputParam4);

                OracleParameter inputParam5 = new OracleParameter("dc", OracleDbType.Char);
                inputParam5.Value = dc;
                cmd.Parameters.Add(inputParam5);

                OracleParameter inputParam6 = new OracleParameter("sdt", OracleDbType.Char);
                inputParam6.Value = sdt;
                cmd.Parameters.Add(inputParam6);

                OracleParameter inputParam7 = new OracleParameter("vt", OracleDbType.Char);
                inputParam7.Value = vt;
                cmd.Parameters.Add(inputParam7);

                OracleParameter inputParam8 = new OracleParameter("ql", OracleDbType.Char);
                inputParam8.Value = ql;
                cmd.Parameters.Add(inputParam8);

                OracleParameter inputParam9 = new OracleParameter("ph", OracleDbType.Char);
                inputParam9.Value = ph;
                cmd.Parameters.Add(inputParam9);

                using (OracleDataReader reader = cmd.ExecuteReader())
                {

                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion
    }
}
