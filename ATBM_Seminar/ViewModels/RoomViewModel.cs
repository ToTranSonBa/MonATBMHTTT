using ATBM_Seminar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.ViewModels
{
    public class RoomViewModel:ViewModelBase
    {
        public ObservableCollection<Room> showRoom()
        {
            Room room = new Room();
            ObservableCollection<Room> rooms = new ObservableCollection<Room>();
            rooms = room.allRoom();

            for (int i = 0; i < rooms.Count; i++)
            {
                Employee employee = new Employee();
                employee = employee.Detail_Employee(rooms[i].MATRPHG);
                rooms[i].TRPHG = employee.TENNV;
            }

            return rooms;
        }
    }
}
