using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        public string MAPHG { get; set; }

        public string LUONG { get; set; }
        public string PHUCAP { get; set; }
        public string MANQL { get; set; }
        public string TENNQL { get; set; }



        public ObservableCollection<Employee> allEmp(OracleConnection conn)
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
                employee.MAPHG = row["PHG"].ToString();

                if (row["MANQL"].ToString() !="")
                {
                    Employee employee1 = new Employee();
                    employee1 = employee1.detailEmployee(row["MANQL"].ToString(), conn);
                    employee.TENNQL = employee1.TENNV;
                    employee.MANQL = row["MANQL"].ToString();
                }

                list_employee.Add(employee);
            }

            return list_employee;
        }

        public Employee detailEmployee(string Ma, OracleConnection conn)
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
            catch { 
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
            catch {
                return false;
            }
            
        }

    }
}
