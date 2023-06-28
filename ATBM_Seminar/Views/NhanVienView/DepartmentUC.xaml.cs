using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATBM_Seminar.Views.NhanVienView
{
    /// <summary>
    /// Interaction logic for DepartmentUC.xaml
    /// </summary>
    public partial class DepartmentUC : UserControl
    {
        private readonly OracleConnection _oracleConnection;
        public DepartmentUC(OracleConnection conn)
        {
            _oracleConnection = conn;
            InitializeComponent();
            LoadRooms();
        }
        public void LoadRooms()
        {
            try
            {
                RoomViewModel room = new RoomViewModel();
                ObservableCollection<Room> rooms = new ObservableCollection<Room>();
                rooms = room.phongBan_BL(_oracleConnection);

                if (rooms == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    RoomsDataGrid.ItemsSource = rooms;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }

        private void equipmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
