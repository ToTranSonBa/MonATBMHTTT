using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using ATBM_Seminar.Views.QuanLyViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ATBM_Seminar.Views.QuanLyViews
{
    /// <summary>
    /// Interaction logic for ChangePwdUser_Window.xaml
    /// </summary>
    public partial class QLTrucTiep_Window : Window
    {
        private readonly Employee userName;
        private readonly OracleConnection _oracleConnection;
        public QLTrucTiep_Window(Employee nvql, OracleConnection conn)
        {
            _oracleConnection = conn;
            InitializeComponent();
            userName = nvql;
            ButtoninforEmp_Click(null,null);
        }

        private void ButtoninforEmp_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ButtoninforEmp.Style = this.FindResource("isSelectButton") as Style;
                ButtoninforPC.Style = this.FindResource("menuButton") as Style;
                PCWindow.Visibility = Visibility.Hidden;
                inforEmpWindow.Visibility = Visibility.Visible;
                Employee employee = new Employee();
                var manv = userName.MANV;
                employee = employee.detailEmployee(manv, _oracleConnection);
                hotentxt.Text = employee.TENNV;
                ngsinhtxt.Text = employee.NGAYSINH;
                sdttxt.Text = employee.SODT;
                phaitxt.Text = employee.PHAI;
                diachitxt.Text = employee.DIACHI;
                vaitrotxt.Text = employee.VAITRO;
                phongtxt.Text = employee.PHONG;
                luongtxt.Text = employee.LUONG;
                phucaptxt.Text = employee.PHUCAP;
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
            var admin = new QuanlyHome();
            admin.Show();
            this.Close();
        }

        //

        public void ShowPCOfEmp()
        {


            try
            {
                PhanCongViewModel pc = new PhanCongViewModel();
                ObservableCollection<PhanCong> list_pc = new ObservableCollection<PhanCong>();
                list_pc = pc.show_allPC(userName.MANV, _oracleConnection);
                if (pc == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    PCDataGrid.ItemsSource = list_pc;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }

        //

        public void ButtoninforPC_Click(object SENDER, RoutedEventArgs E)
        {
            ButtoninforEmp.Style = this.FindResource("menuButton") as Style;
            ButtoninforPC.Style = this.FindResource("isSelectButton") as Style;
            PCWindow.Visibility = Visibility.Visible;
            inforEmpWindow.Visibility = Visibility.Hidden;
            ShowPCOfEmp();
        }

   
    }

}
