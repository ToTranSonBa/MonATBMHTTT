using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATBM_Seminar.ViewModels
{
    public class TrDeAnViewModel:ViewModelBase
    {
        public ObservableCollection<DeAn> showDeAn(OracleConnection connection)
        {
            DeAn dean = new DeAn();
            ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
            list_dean = dean.allDeAn(connection);

            return list_dean;
        }
        public void deleteDeAn(OracleConnection connection, string MADA)
        {
            DeAn dean = new DeAn();
            try {
                dean.deleteDeAn(connection, MADA);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void addDeAn(OracleConnection connection,string MADA, string TENDA, string NGAYBD, string PHONG)
        {
            DeAn dean = new DeAn();
            try
            {
                dean.addDeAn(connection,MADA,TENDA,NGAYBD,PHONG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateDeAn(OracleConnection connection,string MADA, string TENDA, string NGAYBD, string PHONG)
        {
            DeAn dean = new DeAn();
            try
            {
                dean.updateDeAn(connection, MADA, TENDA, NGAYBD, PHONG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DeAn showDetail_DeAn(OracleConnection connection, string MADA)
        {
            DeAn dean = new DeAn();
            try
            {
                dean=dean.detailDeAn(connection, MADA);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dean;
        }
        public Room getDetailRoomByName(OracleConnection connection,string TENPB)
        {
            Room room = new Room();
            room=room.getDetailRoomByName(connection, TENPB);
            return room;
        }
    }
}
