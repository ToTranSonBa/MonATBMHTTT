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
    }
}
