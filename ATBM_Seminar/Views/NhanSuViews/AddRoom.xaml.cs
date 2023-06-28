using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ATBM_Seminar.Views.NhanSuViews
{
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        public int RoomNumber = 0;
        private readonly OracleConnection _oracleConnection;
        public AddRoom(int RoomNum, OracleConnection conn)
        {
            InitializeComponent();
            RoomNumber = RoomNum;
            _oracleConnection = conn;
            loadRoomWindow();
        }

        private void loadRoomWindow()
        {
            try
            {

                ObservableCollection<Employee> list_TP = new ObservableCollection<Employee>();
                EmployeeViewModel trPhg = new EmployeeViewModel();
                list_TP = trPhg.show_AllTP(_oracleConnection);

                if ( list_TP == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    TrgPhgComboBox.ItemsSource = list_TP;
                }
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }

        private void Button_AddRoom(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomNumber = RoomNumber + 1;
                string newRoomNum = "";
                if (RoomNumber < 9)
                {
                    newRoomNum = "PB00" + RoomNumber.ToString();
                }
                else
                {
                    newRoomNum = "PB0" + RoomNumber.ToString();
                }

                string selectedTrPhg = TrgPhgComboBox.SelectedValue as string;
                string roomName = RoomNameTxt.Text;
                if ( TrgPhgComboBox.SelectedItem != null && roomName != "")
                {
                    RoomViewModel roomUpdate = new RoomViewModel();
                    roomUpdate.InsertRoom(_oracleConnection, roomName, selectedTrPhg, newRoomNum);
                    MessageBox.Show("thêm phòng ban thành công");

                }
                else
                {
                    MessageBox.Show("thêm phòng ban thất bại !");
                }
                
                this.Close();
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {;
            this.Close();
        }
    }
}
