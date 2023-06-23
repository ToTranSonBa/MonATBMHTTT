using ATBM_Seminar.Models;
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
        public ObservableCollection<DeAn> showDeAn()
        {
            DeAn dean = new DeAn();
            ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
            list_dean = dean.allDeAn();

            return list_dean;
        }
        public void deleteDeAn(string MADA)
        {
            DeAn dean = new DeAn();
            try {
                dean.deleteDeAn(MADA);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void addDeAn(string MADA, string TENDA, string NGAYBD, string PHONG)
        {
            DeAn dean = new DeAn();
            try
            {
                dean.addDeAn(MADA,TENDA,NGAYBD,PHONG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateDeAn(string MADA, string TENDA, string NGAYBD, string PHONG)
        {
            DeAn dean = new DeAn();
            try
            {
                dean.updateDeAn(MADA, TENDA, NGAYBD, PHONG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DeAn showDetail_DeAn(string MADA)
        {
            DeAn dean = new DeAn();
            try
            {
                dean=dean.detailDeAn(MADA);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dean;
        }
        public Room getDetailRoomByName(string TENPB)
        {
            Room room = new Room();
            room=room.getDetailRoomByName(TENPB);
            return room;
        }
    }
}
