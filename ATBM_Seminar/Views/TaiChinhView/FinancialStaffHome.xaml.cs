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
using ATBM_Seminar.Views.NhanVienView;
using ATBM_Seminar.ModelViews;

namespace ATBM_Seminar.Views.TaiChinhView
{
    /// <summary>
    /// Interaction logic for FinancialStaffHome.xaml
    /// </summary>
    public partial class FinancialStaffHome : Window
    {
        private readonly OracleConnection _oracleConnection;
        private string _userName {  get; set; }
        public bool IsMaximized = false;
        public FinancialStaffHome(OracleConnection conn, string userName) //
        {
            _userName = userName;
            _oracleConnection = conn;
            //string username = "NV013";
            //string password = "NV013";
            //ConnectionDB conn = new ConnectionDB();
            //_oracleConnection =  conn.OracleConnection(username, password);
            InitializeComponent();
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
            NhanVienBtn.Style = FindResource("menuButton") as Style;
            PHANCONGBtn.Style = FindResource("menuButton") as Style;
            PHONGBANBtn.Style = FindResource("menuButton") as Style;
            DEANBtn.Style = FindResource("menuButton") as Style;
            Financial.Content = new FinanPersonelUC(_oracleConnection, _userName);
        }

        private void NhanvienBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("menuButton") as Style;
            NhanVienBtn.Style = FindResource("isSelectButton") as Style;
            PHANCONGBtn.Style = FindResource("menuButton") as Style;
            PHONGBANBtn.Style = FindResource("menuButton") as Style;
            DEANBtn.Style = FindResource("menuButton") as Style;
            Financial.Content = new FinanEmployeesUC(_oracleConnection);
        }

        private void PhancongBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("menuButton") as Style;
            NhanVienBtn.Style = FindResource("menuButton") as Style;
            PHANCONGBtn.Style = FindResource("isSelectButton") as Style;
            PHONGBANBtn.Style = FindResource("menuButton") as Style;
            DEANBtn.Style = FindResource("menuButton") as Style;
            Financial.Content = new FinanAssignmentUC(_oracleConnection);
        }

        private void PhongbanBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("menuButton") as Style;
            NhanVienBtn.Style = FindResource("menuButton") as Style;
            PHANCONGBtn.Style = FindResource("menuButton") as Style;
            PHONGBANBtn.Style = FindResource("isSelectButton") as Style;
            DEANBtn.Style = FindResource("menuButton") as Style;
            Financial.Content = new DepartmentUC(_oracleConnection);
        }

        private void DeanBTN_Click(object sender, RoutedEventArgs e)
        {
            CANHANBtn.Style = FindResource("menuButton") as Style;
            NhanVienBtn.Style = FindResource("menuButton") as Style;
            PHANCONGBtn.Style = FindResource("menuButton") as Style;
            PHONGBANBtn.Style = FindResource("menuButton") as Style;
            DEANBtn.Style = FindResource("isSelectButton") as Style;
            Financial.Content = new ProjectUC(_oracleConnection);
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
