using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
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
        public ObservableCollection<Room> showRoom(OracleConnection connection)
        {
            Room room = new Room();
            ObservableCollection<Room> rooms = new ObservableCollection<Room>();
            rooms = room.allRoom(connection);

            for (int i = 0; i < rooms.Count; i++)
            {
                Employee employee = new Employee();
                employee = employee.Detail_Employee(connection, rooms[i].MATRPHG);
                rooms[i].TRPHG = employee.TENNV;
            }

            return rooms;
        }
        // 
        public ObservableCollection<Room> phongBan_BL(OracleConnection connection)
        {
            Room room = new Room();
            ObservableCollection<Room> rooms = new ObservableCollection<Room>();
            rooms = room.getPhongBan_DB(connection);
            return rooms;
        }

        #region cuong
        public ObservableCollection<Room> showRoom1(OracleConnection connection)
        {
            Room room = new Room();
            ObservableCollection<Room> rooms = new ObservableCollection<Room>();
            rooms = room.allRoom(connection);

            for (int i = 0; i < rooms.Count; i++)
            {
                Employee employee = new Employee();
                employee = employee.detailEmployee(rooms[i].MATRPHG, connection);
                rooms[i].TRPHG = employee.TENNV;
            }

            return rooms;
        }
        #endregion
    }
}
