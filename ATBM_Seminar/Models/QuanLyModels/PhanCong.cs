using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATBM_Seminar.Models
{
    public class PhanCong
    {
        public string TENNV { get; set; }
        public string TENDA { get; set; }
        public string TIME { get; set; }
        public string Number { get; set; }



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
                    pc.TIME = reader["THOIGIAN"].ToString();
                    pc.Number = (list_pc.Count + 1).ToString();
                    list_pc.Add(pc);
                }
            }


            return list_pc;
        }
    }
}
