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
    public class PhanCong
    {
        public string MADA { get; set; }
        public string MANV { get; set; }
        public string THOIGIAN { get; set; }
        public string TENNV { get; set; }
        public string NGAYBD { get; set; }
        public string TENDA { get; set; }
        public ObservableCollection<PhanCong> allPhanCong(OracleConnection connection)
        {
            ObservableCollection<PhanCong> list_phancong = new ObservableCollection<PhanCong>();

            DataTable result = new DataTable();

            string query = "SELECT * FROM ATBM_20H3T_22.v_ShowAllPhanCong";

            OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
            adapter.Fill(result);

            for (int i = 0; i < result.Rows.Count; i++)
            {
                DeAn dean = new DeAn();
                PhanCong phancong = new PhanCong();
                Employee employee = new Employee();

                DataRow row = result.Rows[i];
                dean = dean.detailDeAn(connection ,row["MADA"].ToString());
                phancong.MADA = dean.TENDA;

                employee = employee.Detail_Employee(connection, row["MANV"].ToString());
                phancong.MANV = employee.TENNV;

                phancong.THOIGIAN = row["THOIGIAN"].ToString();
                list_phancong.Add(phancong);
            }

            return list_phancong;
        }

        // phan cong = dean + nhanvien
        public ObservableCollection<PhanCong> getPhanCong_DB(OracleConnection connection)
        {
            ObservableCollection<PhanCong> list_phancong = new ObservableCollection<PhanCong>();
            DataTable result = new DataTable();

            string query = "SELECT * FROM ATBM_20H3T_22.ATBMHTTT_TABLE_PHANCONG";

            OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
            adapter.Fill(result);

            for (int i = 0; i < result.Rows.Count; i++)
            {
                DeAn dean = new DeAn();
                PhanCong phancong = new PhanCong();
                Employee employee = new Employee();

                DataRow row = result.Rows[i];
                // mã đề án
                phancong.MADA = row["MADA"].ToString();
                // tên đề án
                dean = dean.getOneDeAn_DB(connection, row["MADA"].ToString());
                phancong.TENDA = dean.TENDA;
                // ngày bắt đầu
                phancong.NGAYBD = dean.NGAYBD;
                // tên nhân viên
                employee = employee.getOneNhanVien_DB(connection, row["MANV"].ToString());
                phancong.TENNV = employee.TENNV;
                // thời gian
                phancong.THOIGIAN = row["THOIGIAN"].ToString();
                list_phancong.Add(phancong);
            }
            return list_phancong;
        }

        #region CUong
        public ObservableCollection<PhanCong> allPC(string MaNV, OracleConnection conn)
        {
            ObservableCollection<PhanCong> list_pc = new ObservableCollection<PhanCong>();

            OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_GetPhanCong", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter cursorParam = cmd.Parameters.Add("EMPLOYCURSOR", OracleDbType.RefCursor);
            cursorParam.Direction = ParameterDirection.Output;

            OracleParameter inputParam = new OracleParameter("MANV", OracleDbType.Char);
            inputParam.Value = MaNV;
            cmd.Parameters.Add(inputParam);


            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    PhanCong pc = new PhanCong();
                    pc.TENNV = reader["TENNV"].ToString();
                    pc.TENDA = reader["TENDA"].ToString();
                    pc.THOIGIAN = reader["THOIGIAN"].ToString();
                    list_pc.Add(pc);
                }
            }


            return list_pc;
        }
        #endregion
    }
}
