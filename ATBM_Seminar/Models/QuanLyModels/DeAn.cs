using Oracle.ManagedDataAccess.Client;
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
                    room = room.detailRoom(connection, row["PHONG"].ToString());
                    dean.PHONG = room.TENPB;
                    list_dean.Add(dean);
                }

                return list_dean;
        }

    }
}