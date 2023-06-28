using ATBM_Seminar.Models;
using ATBM_Seminar.ModelViews;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ATBM_Seminar.Views.NhanSuViews
{
    /// <summary>
    /// Interaction logic for RoleWindow.xaml
    /// </summary>
    public partial class NhanSuWindow : Window
    {
        private readonly OracleConnection _oracleConnection;

        public string userName { get; set; }
        public NhanSuWindow(OracleConnection oracleConnection, string user)
        {
            //string username = "NV018";
            //string password = "1";
            
            //ConnectionDB conn = new ConnectionDB();
            //_oracleConnection = conn.OracleConnection(username, password);
            userName = user;
            _oracleConnection = oracleConnection;
            InitializeComponent();
            buttonQLPhong_Click(null, null);
        }

        public void showRooms()
        {

            try
            {
                ObservableCollection<Room> list_Room = new ObservableCollection<Room>();
                RoomViewModel room = new RoomViewModel();
                list_Room = room.show_AllRoom(_oracleConnection);

                ObservableCollection<Employee> list_TP = new ObservableCollection<Employee>();
                EmployeeViewModel trPhg = new EmployeeViewModel();
                list_TP = trPhg.show_AllTP(_oracleConnection);

                if (list_Room == null || list_TP == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    roomsDataGrid.ItemsSource = list_Room;
                    phgComboBox.ItemsSource = list_Room;
                    trPhgComboBox.ItemsSource = list_TP;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }




        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            _oracleConnection.Close();
            var login = new Login();
            this.Close();
            login.Show();

        }

        public void Button_UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedRoom = phgComboBox.SelectedValue as string;
                string selectedTrPhg = trPhgComboBox.SelectedValue as string;
                string roomName = tenPhgtxt.Text;
                if (phgComboBox.SelectedItem != null && trPhgComboBox.SelectedItem != null && roomName != "")
                {
                    RoomViewModel roomUpdate = new RoomViewModel();
                    roomUpdate.updateRoom(_oracleConnection, roomName, selectedTrPhg, selectedRoom);
                    MessageBox.Show("Cập nhật thành công");
                    showRooms();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại !");
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        //

        public void ShowEmps()
        {

            try
            {
                ObservableCollection<Employee> list_Emp = new ObservableCollection<Employee>();
                EmployeeViewModel emp = new EmployeeViewModel();
                list_Emp = emp.show_AllEmp(_oracleConnection);

                if (list_Emp == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    empManagerDataGrid.ItemsSource = list_Emp;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }

        }




        private void buttonQLPhong_Click(object sender, RoutedEventArgs e)
        {
            buttonQLPhong.Style = this.FindResource("isSelectButton") as Style;
            buttonQLNhanVien.Style = this.FindResource("menuButton") as Style;
            DeAnButtom.Style = this.FindResource("menuButton") as Style;

            RoomManager.Visibility = Visibility.Visible;
            empManager.Visibility = Visibility.Hidden;
            DeAnWindow.Visibility = Visibility.Hidden;
            showRooms();
        }

        private void Button_Click_AddRoom(object sender, RoutedEventArgs e)
        {
            int i = phgComboBox.Items.Count;
            AddRoom addRoom = new AddRoom(i, _oracleConnection);
            addRoom.Show();

        }

        private void Button_AddEmp(object sender, RoutedEventArgs e)
        {
            int i = empManagerDataGrid.Items.Count;
            AddEmployee addEmp = new AddEmployee(i, _oracleConnection, userName);
            addEmp.Show();
        }

        public void buttonQLNhanVien_Click(object sender, RoutedEventArgs e)
        {
            buttonQLNhanVien.Style = this.FindResource("isSelectButton") as Style;
            buttonQLPhong.Style = this.FindResource("menuButton") as Style;
            DeAnButtom.Style = this.FindResource("menuButton") as Style;

            RoomManager.Visibility = Visibility.Hidden;
            empManager.Visibility = Visibility.Visible;
            DeAnWindow.Visibility = Visibility.Hidden;



            ShowEmps();
        }

        private void buttonEditQLclick(object sender, RoutedEventArgs e)
        {
            Employee emp = empManagerDataGrid.SelectedItem as Employee;
            detailEmployeeWindow INFOR_EMP = new detailEmployeeWindow(emp,userName, _oracleConnection);
            INFOR_EMP.Show();
            this.Close();
        }

        private void DeAnButtom_Click(object sender, RoutedEventArgs e)
        {
            DeAnButtom.Style = this.FindResource("isSelectButton") as Style;
            buttonQLPhong.Style = this.FindResource("menuButton") as Style;
            buttonQLNhanVien.Style = this.FindResource("menuButton") as Style;

            RoomManager.Visibility = Visibility.Hidden;
            empManager.Visibility = Visibility.Hidden;
            DeAnWindow.Visibility = Visibility.Visible;

            try
            {
                DeAnViewModel dean = new DeAnViewModel();
                ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
                list_dean = dean.showDeAn(_oracleConnection);

                if (list_dean == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    DADataGrid.ItemsSource = list_dean;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }


        private void empManagerDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Employee dataObject = e.Row.Item as Employee; // Kiểu đối tượng dữ liệu của bạn

            if (dataObject != null && dataObject.MANV == userName)
            {
                //e.Row.Background = Brushes.Yellow; // Màu nền tô màu
            }
        }


    }

}
