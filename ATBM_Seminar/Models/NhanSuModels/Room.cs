using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATBM_Seminar.Models
{
    public class Room
    {
        public string MAPB { get; set; }
        public string TENPB { get; set; }
        public string TRPHG { get; set; }
        public string MATRPHG { get; set; }

        public ObservableCollection<Room> allRoom(OracleConnection conn)
        {
            ObservableCollection<Room> list_room = new ObservableCollection<Room>();
            DataTable result = new DataTable();
            string query = "SELECT * FROM ATBM_20H3T_22.v_ShowAllPhongBan";

            OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
            adapter.Fill(result);


            for (int i = 0; i < result.Rows.Count; i++)
            {
                Room room = new Room();
                Employee employee = new Employee();
                DataRow row = result.Rows[i];
                room.MAPB = row["MAPB"].ToString();
                room.TENPB = row["TENPB"].ToString();
                room.MATRPHG = row["TRPHG"].ToString();
                employee = employee.detailEmployee(row["TRPHG"].ToString(), conn);
                room.TRPHG = employee.TENNV;
                list_room.Add(room);
            }

            return list_room;
            
        }

        public Room detailRoom(OracleConnection connection, string MaPB)
        {
            Room room = new Room();



            OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_GetRoom", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter cursorParam = cmd.Parameters.Add("ROOMCURSOR", OracleDbType.RefCursor);
            cursorParam.Direction = ParameterDirection.Output;

            OracleParameter inputParam = new OracleParameter("MAPB", OracleDbType.Char);
            inputParam.Value = MaPB;
            cmd.Parameters.Add(inputParam);

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    room.MAPB = reader.GetString(0);
                    room.TENPB = reader.GetString(1);
                    room.MATRPHG = reader.GetString(2);
                }
            }
            return room;
        }

        public void Update(OracleConnection connection, string TP, string TrP,string MP)
        {
            OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_UPDATEROOM", connection);
            cmd.CommandType = CommandType.StoredProcedure; ;

            OracleParameter inputParam2 = new OracleParameter("TP", OracleDbType.NVarchar2);
            inputParam2.Value = TP;
            cmd.Parameters.Add(inputParam2);

            OracleParameter inputParam3 = new OracleParameter("TrP", OracleDbType.Char);
            inputParam3.Value = TrP;
            cmd.Parameters.Add(inputParam3);

            OracleParameter inputParam4 = new OracleParameter("MP", OracleDbType.Char);
            inputParam4.Value = MP;
            cmd.Parameters.Add(inputParam4);
            
            using (OracleDataReader reader = cmd.ExecuteReader())
            {

            }
        }

        public void Insert(OracleConnection connection, string TP, string TrP, string MP)
        {
            OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_INSERTROOM", connection);
            cmd.CommandType = CommandType.StoredProcedure; ;

            OracleParameter inputParam2 = new OracleParameter("TP", OracleDbType.NVarchar2);
            inputParam2.Value = TP;
            cmd.Parameters.Add(inputParam2);

            OracleParameter inputParam3 = new OracleParameter("TrP", OracleDbType.Char);
            inputParam3.Value = TrP;
            cmd.Parameters.Add(inputParam3);

            OracleParameter inputParam4 = new OracleParameter("MP", OracleDbType.Char);
            inputParam4.Value = MP;
            cmd.Parameters.Add(inputParam4);

            using (OracleDataReader reader = cmd.ExecuteReader())
            {

            }
            
        }
    }
}
