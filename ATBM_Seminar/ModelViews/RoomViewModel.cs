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

        #region cuong tao lao 2
        public ObservableCollection<Room> show_AllRoom(OracleConnection conn)
        {
            Room room = new Room();
            ObservableCollection<Room> list_Room = new ObservableCollection<Room>();
            list_Room = room.allRoom2(conn);

            return list_Room;
        }

        public void updateRoom(OracleConnection connection, string TP, string TrP, string MP)
        {
            Room room = new Room();
            try
            {
                room.Update(connection, TP, TrP, MP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void InsertRoom(OracleConnection connection, string TP, string TrP, string MP)
        {
            Room room = new Room();
            try
            {
                room.Insert(connection, TP, TrP, MP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
