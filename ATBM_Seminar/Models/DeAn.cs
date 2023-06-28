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
    public class DeAn
    {
        public string MADA { get; set; }
        public string TENDA { get; set; }
        public string NGAYBD { get; set; }
        public string PHONG { get; set; }


        public ObservableCollection<DeAn> allDeAn(OracleConnection connection)
        {
            ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();


            DataTable result = new DataTable();

            string query = "SELECT * FROM ATBM_20H3T_22.v_ShowAllDeAn";

            
                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                adapter.Fill(result);
           

            for (int i = 0; i < result.Rows.Count; i++)
            {
                DeAn dean = new DeAn();
                Room room = new Room();

                DataRow row = result.Rows[i];
                dean.MADA = row["MADA"].ToString();
                dean.TENDA = row["TENDA"].ToString();
                dean.NGAYBD = row["NGAYBD"].ToString();
                room=room.detailRoom(connection,row["PHONG"].ToString());
                dean.PHONG =room.TENPB;
                list_dean.Add(dean);
            }

            return list_dean;
        }
        public DeAn detailDeAn(OracleConnection connection, string MADA)
        {
            DeAn dean = new DeAn();

            

                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_GetDeAn", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter cursorParam = cmd.Parameters.Add("DEANCURSOR", OracleDbType.RefCursor);
                cursorParam.Direction = ParameterDirection.Output;

                OracleParameter inputParam = new OracleParameter("MADA", OracleDbType.Char);
                inputParam.Value = MADA;
                cmd.Parameters.Add(inputParam);

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dean.MADA = reader.GetString(0);
                        dean.TENDA= reader.GetString(1);
                        dean.NGAYBD= reader.GetString(2);
                        dean.PHONG= reader.GetString(3);
                    }
                }

            
            return dean;
        }
        public void deleteDeAn(OracleConnection connection, string MADA)
        {
                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_DELETEDEAN", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter inputParam = new OracleParameter("MA", OracleDbType.Char);
                inputParam.Value = MADA;
                cmd.Parameters.Add(inputParam);

                OracleDataReader reader = cmd.ExecuteReader();
            
        }
        public void addDeAn(OracleConnection connection, string MADA,string TENDA, string NGAYBD, string PHONG)
        {

            

                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_ADDDEAN", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter inputParam = new OracleParameter("MA", OracleDbType.Char);
                inputParam.Value = MADA;
                cmd.Parameters.Add(inputParam);

                OracleParameter inputParam2 = new OracleParameter("TEN", OracleDbType.NVarchar2);
                inputParam2.Value = TENDA;
                cmd.Parameters.Add(inputParam2);

                OracleParameter inputParam3 = new OracleParameter("NGAY", OracleDbType.Char);
                inputParam3.Value = NGAYBD;
                cmd.Parameters.Add(inputParam3);

                OracleParameter inputParam4 = new OracleParameter("PHG", OracleDbType.Char);
                inputParam4.Value = PHONG;
                cmd.Parameters.Add(inputParam4);

                OracleDataReader reader = cmd.ExecuteReader();
            
        }
        public void updateDeAn(OracleConnection connection, string MADA, string TENDA, string NGAYBD, string PHONG)
        {
            

            

                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_UPDATEDEAN", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter inputParam = new OracleParameter("MA", OracleDbType.Char);
                inputParam.Value = MADA;
                cmd.Parameters.Add(inputParam);

                OracleParameter inputParam2 = new OracleParameter("TEN", OracleDbType.NVarchar2);
                inputParam2.Value = TENDA;
                cmd.Parameters.Add(inputParam2);

                OracleParameter inputParam3 = new OracleParameter("NGAY", OracleDbType.Char);
                inputParam3.Value = NGAYBD;
                cmd.Parameters.Add(inputParam3);

                OracleParameter inputParam4 = new OracleParameter("PHG", OracleDbType.Char);
                inputParam4.Value = PHONG;
                cmd.Parameters.Add(inputParam4);

                OracleDataReader reader = cmd.ExecuteReader();
            
        }

        // get all dean by select table
        public ObservableCollection<DeAn> getDeAn_DB(OracleConnection connection)
        {
            ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();


            DataTable result = new DataTable();

            string query = "SELECT * FROM ATBM_20H3T_22.ATBMHTTT_TABLE_DEAN";


            OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
            adapter.Fill(result);


            for (int i = 0; i < result.Rows.Count; i++)
            {
                DeAn dean = new DeAn();
                Room room = new Room();

                DataRow row = result.Rows[i];
                dean.MADA = row["MADA"].ToString();
                dean.TENDA = row["TENDA"].ToString();
                dean.NGAYBD = row["NGAYBD"].ToString();
                room = room.getOnePhongBan_DB(connection, row["PHONG"].ToString());
                dean.PHONG = room.TENPB;
                list_dean.Add(dean);
            }

            return list_dean;
        }

        // get one dean
        public DeAn getOneDeAn_DB(OracleConnection connection, string MADA)
        {
            DeAn dean = null;

            string query = $"SELECT * FROM ATBM_20H3T_22.ATBMHTTT_TABLE_DEAN WHERE MADA = '{MADA}' ";

            OracleCommand cmd = new OracleCommand(query, connection);

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read()) // Kiểm tra xem có dữ liệu trả về hay không
                {
                    dean = new DeAn();
                    dean.MADA = reader.GetString(0);
                    dean.TENDA = reader.GetString(1);
                    dean.NGAYBD = reader.GetString(2);
                    dean.PHONG = reader.GetString(3);
                }
            }
            return dean;
        }


    }
}
