using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
    /// Interaction logic for PersonelUC.xaml
    /// </summary>
    public partial class PersonelUC : UserControl
    {
        private string _userName {  get; set; }
        private readonly OracleConnection _oracleConnection;
        public PersonelUC(OracleConnection conn, string username)
        {
            _userName = username;
            _oracleConnection = conn;
            InitializeComponent();
            LoadDetailEmployee();
        }
        public void LoadDetailEmployee()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            EmployeeViewModel employee = new EmployeeViewModel();
            employees = employee.nhanVien_BL(_oracleConnection);

            if (employees == null)
            {
                MessageBox.Show("NULL");
            }
            else
            {
                dataNhanVien.DataContext = employees;
            }
        }
        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            string ngaySinh_update = txbNgSinh.Text;
            string diaChi_update  = txbDiaChi.Text;
            string soDT_update = txbSDT.Text;

            if (ngaySinh_update != null && diaChi_update != null && soDT_update != null)
            {
                EmployeeViewModel updateNhanVien = new EmployeeViewModel();
                updateNhanVien.updateThongTinNhanVienNV_BL(_oracleConnection, ngaySinh_update, diaChi_update, soDT_update);
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập lại thông tin");
            }
        }
    }
}
