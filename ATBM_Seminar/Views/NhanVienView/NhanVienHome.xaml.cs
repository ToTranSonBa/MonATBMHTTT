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
using ATBM_Seminar.Models;
using System.Configuration;
using System.Data;
using ATBM_Seminar.ViewModels;
using ATBM_Seminar.ModelViews;

namespace ATBM_Seminar.Views.NhanVienView
{
    /// <summary>
    /// Interaction logic for NhanVienHome.xaml
    /// </summary>
    public partial class NhanVienHome : Window
    {
        private string _userName { get; set; }

        private readonly OracleConnection _oracleConnection;
        public bool IsMaximized = false;
        public NhanVienHome(OracleConnection conn, string userName) // 
        {
            _userName = userName;
            _oracleConnection = conn;
            /*ConnectionDB conn = new ConnectionDB()*/;
            ////_oracleConnection = conn.OracleConnection("NV032", "NV032");
            InitializeComponent();
            Title.Text = _userName;
            CanhanBTN_Click(null, null);
            //DataContext = new MainViewModel();
        }

        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void CanhanBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("isSelectButton") as Style;
            PHANCONGBtn.Style = FindResource("menuButton") as Style;
            PHONGBANBtn.Style = FindResource("menuButton") as Style;
            DEANBtn.Style = FindResource("menuButton") as Style;
            Employee.Content = new PersonelUC(_oracleConnection, _userName);
        }

        private void PhancongBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("menuButton") as Style;
            PHANCONGBtn.Style = FindResource("isSelectButton") as Style;
            PHONGBANBtn.Style = FindResource("menuButton") as Style;
            DEANBtn.Style = FindResource("menuButton") as Style;
            Employee.Content = new AssignmentUC(_oracleConnection);
        }

        private void PhongbanBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("menuButton") as Style;
            PHANCONGBtn.Style = FindResource("menuButton") as Style;
            PHONGBANBtn.Style = FindResource("isSelectButton") as Style;
            DEANBtn.Style = FindResource("menuButton") as Style;
            Employee.Content = new DepartmentUC(_oracleConnection);
        }

        private void DeanBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("menuButton") as Style;
            PHANCONGBtn.Style = FindResource("menuButton") as Style;
            PHONGBANBtn.Style = FindResource("menuButton") as Style;
            DEANBtn.Style = FindResource("isSelectButton") as Style;
            Employee.Content = new ProjectUC(_oracleConnection);
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            _oracleConnection.Close();
            var login = new Login();
            this.Close();
            login.Show();
        }
    }
}
