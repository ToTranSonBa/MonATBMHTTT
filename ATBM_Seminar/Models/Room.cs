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
    public class Room
    {
        public string MAPB { get; set; }
        public string TENPB { get; set; }
        public string MATRPHG { get; set; }
        public string TRPHG { get; set; }

        public ObservableCollection<Room> allRoom()
        {
            ObservableCollection<Room> list_room = new ObservableCollection<Room>();


            DataTable result = new DataTable();
            Connect conn = new Connect();

            string query = "SELECT * FROM ATBM_20H3T_22.v_ShowAllPhongBan";

            using (OracleConnection connection = conn.connectDatabase())
            {
                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                adapter.Fill(result);
            }

            for (int i = 0; i < result.Rows.Count; i++)
            {
                Room room=new Room();
                    
                DataRow row = result.Rows[i];
                room.MAPB = row["MAPB"].ToString();
                room.TENPB = row["TENPB"].ToString();
                room.MATRPHG = row["TRPHG"].ToString();
                list_room.Add(room);
            }
           
            return list_room;
        }
        public Room detailRoom(string MaPB)
        {
            Room room = new Room();
            Connect conn = new Connect();

            using (OracleConnection connection = conn.connectDatabase())
            {

                OracleCommand cmd = new OracleCommand("sp_GetRoom", connection);
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
                        room.MATRPHG= reader.GetString(2);
                    }
                }

            }
            return room;
        }
        public Room getDetailRoomByName(string TENPB)
        {
            Room room = new Room();
            Connect conn = new Connect();

            using (OracleConnection connection = conn.connectDatabase())
            {

                OracleCommand cmd = new OracleCommand("sp_GetRoomByName", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter cursorParam = cmd.Parameters.Add("ROOMCURSOR", OracleDbType.RefCursor);
                cursorParam.Direction = ParameterDirection.Output;

                OracleParameter inputParam = new OracleParameter("TEN", OracleDbType.Char);
                inputParam.Value = TENPB;
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

            }
            return room;
        }
    }
}
