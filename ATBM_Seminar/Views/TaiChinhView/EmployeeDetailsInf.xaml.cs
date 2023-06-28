using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace ATBM_Seminar.Views.TaiChinhView
{
    /// <summary>
    /// Interaction logic for EmployeeDetailsInf.xaml
    /// </summary>
    public partial class EmployeeDetailsInf : Window
    {
        private readonly OracleConnection _oracleConnection;
        private string _username { get; set; }
        public EmployeeDetailsInf(OracleConnection conn ,string MANV)
        {
            _oracleConnection = conn;
            _username = MANV;
            InitializeComponent();
            LoadDetailEmployee(MANV);
        }
        public void LoadDetailEmployee(string MANV)
        {
            try
            {
                Employee employee = new Employee();
                employee = employee.getOneNhanVien_DB( _oracleConnection, MANV);
                this.DataContext = employee;
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        // nút thoát
        private void click_BtnExit(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }

        //để kéo thả window khi set window=none
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            string LUONG_update = txbLuong.Text;
            string PHUCAP_update = txbPhuCap.Text;
            
            if (LUONG_update != null && PHUCAP_update != null)
            {
                EmployeeViewModel updateNhanVien = new EmployeeViewModel();
                updateNhanVien.updateTaiChinhNhanVien_BL(_oracleConnection, _username, LUONG_update, PHUCAP_update);
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng diền đầy đủ thông tin");
            }
        }
    }
}
