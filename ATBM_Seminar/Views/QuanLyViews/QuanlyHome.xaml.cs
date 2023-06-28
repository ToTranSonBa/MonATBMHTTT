using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
using System.Data;
using System.Numerics;
using ATBM_Seminar.ModelViews;
using System.Data.Common;

namespace ATBM_Seminar.Views.QuanLyViews
{
    /// <summary>
    /// Interaction logic for Admin_Window.xaml
    /// </summary>
    public partial class QuanlyHome : Window
    {
        private readonly OracleConnection _oracleConnection;
        public QuanlyHome(OracleConnection oracleConnection)
        {
            _oracleConnection = oracleConnection;
            
            InitializeComponent();
            QuanLyButton_Click(null,null);

        }
        public void LoaderWindow()
        {

            try
            {
                ObservableCollection<Employee> list_emp = new ObservableCollection<Employee>();
                EmployeeViewModel emp = new EmployeeViewModel();
                list_emp = emp.showEmp(_oracleConnection);

                if (list_emp == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    QLDataGrid.ItemsSource = list_emp;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public bool IsMaximized = false;
        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2) {
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

        private void buttonEditQLclick(object sender, RoutedEventArgs e)
        {
            Employee nvql = QLDataGrid.SelectedItem as Employee;
            QLTrucTiep_Window editQuanLy = new QLTrucTiep_Window(nvql,_oracleConnection);
            editQuanLy.Show();
            this.Close();
        }

        private void QuanLyButton_Click(object sender, RoutedEventArgs e)
        {
            QuanLyButton.Style = this.FindResource("isSelectButton") as Style;
            UpdateButtom.Style = this.FindResource("menuButton") as Style;
            PhongBanButtom.Style = this.FindResource("menuButton") as Style;
            DeAnButtom.Style = this.FindResource("menuButton") as Style;
            QuanLyGrid.Visibility = Visibility.Visible;
            UpdateWindow.Visibility = Visibility.Hidden;
            PhongBanWindow.Visibility = Visibility.Hidden;
            DeAnWindow.Visibility = Visibility.Hidden;
            LoaderWindow();
        }

        private void UpdateButtom_Click(object sender, RoutedEventArgs e)
        {
            UpdateButtom.Style = this.FindResource("isSelectButton") as Style;
            QuanLyButton.Style = this.FindResource("menuButton") as Style;
            PhongBanButtom.Style = this.FindResource("menuButton") as Style;
            DeAnButtom.Style = this.FindResource("menuButton") as Style;

            QuanLyGrid.Visibility = Visibility.Hidden;
            UpdateWindow.Visibility = Visibility.Visible;
            PhongBanWindow.Visibility = Visibility.Hidden;
            DeAnWindow.Visibility = Visibility.Hidden;
            LoaderWindow();


        }


        private void Button_Updateinfor_Click(object sender, RoutedEventArgs e)
        {
            DateTime? updateNgaySinh =   Day.SelectedDate;
            string updateDiaChi = diachitxt.Text;
            string updateSoDT = sdttxt.Text;
            if(updateNgaySinh.HasValue && updateDiaChi !="" && updateSoDT !="") { 
                string NGAYBD = updateNgaySinh.Value.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                EmployeeViewModel ql = new EmployeeViewModel();
                ql.updateQuanLy(_oracleConnection, NGAYBD, updateDiaChi, updateSoDT);
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại !");
            }

        }

        private void PhongBanButtom_Click(object sender, RoutedEventArgs e)
        {
            PhongBanButtom.Style = this.FindResource("isSelectButton") as Style;
            UpdateButtom.Style = this.FindResource("menuButton") as Style;
            QuanLyButton.Style = this.FindResource("menuButton") as Style;
            DeAnButtom.Style = this.FindResource("menuButton") as Style;

            QuanLyGrid.Visibility = Visibility.Hidden;
            UpdateWindow.Visibility = Visibility.Hidden;
            PhongBanWindow.Visibility = Visibility.Visible;
            DeAnWindow.Visibility = Visibility.Hidden;

            try
            {
                RoomViewModel room = new RoomViewModel();
                ObservableCollection<Room> rooms = new ObservableCollection<Room>();
                rooms = room.showRoom1(_oracleConnection);

                if (rooms == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    PBDataGrid.ItemsSource = rooms;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }

        private void DeAnButtom_Click(object sender, RoutedEventArgs e)
        {
            DeAnButtom.Style = this.FindResource("isSelectButton") as Style;
            PhongBanButtom.Style = this.FindResource("menuButton") as Style;
            UpdateButtom.Style = this.FindResource("menuButton") as Style;
            QuanLyButton.Style = this.FindResource("menuButton") as Style;

            QuanLyGrid.Visibility = Visibility.Hidden;
            UpdateWindow.Visibility = Visibility.Hidden;
            PhongBanWindow.Visibility = Visibility.Hidden;
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
    }
}
