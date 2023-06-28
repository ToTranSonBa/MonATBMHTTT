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

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for detailEmployeeWindow.xaml
    /// </summary>
    public partial class detailEmployeeWindow : Window
    {
        private readonly Employee nv;
        public string userName { get; set; }

        private readonly OracleConnection _oracleConnection;
        public detailEmployeeWindow(Employee NV,string username, OracleConnection conn)
        {
            InitializeComponent();
            nv = NV;
            userName= username;
            _oracleConnection= conn;
            buttonEmpInfor_Click(null,null);
        }

        private void buttonEmpInfor_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                buttonEmpInfor.Style = this.FindResource("isSelectButton") as Style;
                EmpInforWindow.Visibility = Visibility.Visible;
                Employee employee = new Employee();
                employee = employee.detailEmployee(nv.MANV, _oracleConnection);

                 
                hotentxt.Text = employee.TENNV;
                DateTime date;
                if (DateTime.TryParse(employee.NGAYSINH, out date))
                {
                    ngaysinh.SelectedDate = date;
                }

                sdttxt.Text = employee.SODT;
                phaitxt.Text = employee.PHAI;
                diachitxt.Text = employee.DIACHI;
                vaitrotxt.Text = employee.VAITRO;
                phongtxt.Text = employee.PHONG;
                qltxt.Text = nv.TENNQL;
                luongtxt.Text = employee.LUONG;
                phucaptxt.Text = employee.PHUCAP;

                ObservableCollection<Employee> list_QL = new ObservableCollection<Employee>();
                EmployeeViewModel QL = new EmployeeViewModel();
                list_QL = QL.show_AllQL(_oracleConnection);

                ObservableCollection<Employee> list_VaiTro = new ObservableCollection<Employee>();
                EmployeeViewModel VaiTro = new EmployeeViewModel();
                list_VaiTro = VaiTro.show_AllVaiTro(_oracleConnection);

                ObservableCollection<Room> list_Room = new ObservableCollection<Room>();
                RoomViewModel room = new RoomViewModel();
                list_Room = room.show_AllRoom(_oracleConnection);

                if (list_Room == null || list_QL == null || list_VaiTro == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    nqlComboBox.ItemsSource = list_QL;
                    phongComboBox.ItemsSource = list_Room;
                    vaiTroComboBox.ItemsSource = list_VaiTro;
                }
                if (userName == nv.MANV)
                {
                    hotentxt.IsEnabled = false;
                    phaitxt.IsEnabled = false;
                    vaitrotxt.IsEnabled = false;
                    phongtxt.IsEnabled = false;
                    qltxt.IsEnabled = false;
                    nqlComboBox.IsEnabled = false;
                    phongComboBox.IsEnabled = false;
                    vaiTroComboBox.IsEnabled = false;

                }
                else
                {
                    vaitrotxt.IsEnabled = false;
                    phongtxt.IsEnabled = false;
                    qltxt.IsEnabled = false;

                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }


        private void Button_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedNQL = null;
                selectedNQL = nqlComboBox.SelectedValue as string;
                string selectedPhg = phongComboBox.SelectedValue as string;
                string selectedVaiTro = vaiTroComboBox.SelectedValue as string;
                DateTime? updateNgaySinh = ngaysinh.SelectedDate;
                if(nv.MANV != userName)
                {
                    if (updateNgaySinh.HasValue && hotentxt.Text != "" && phaitxt.Text != "" && diachitxt.Text != "" && sdttxt.Text != "" && phongComboBox.SelectedItem != null && vaiTroComboBox.SelectedItem != null)
                    {
                        string NgaySinh = updateNgaySinh.Value.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        EmployeeViewModel empUpdate = new EmployeeViewModel();

                        var check = empUpdate.updateEmployee(_oracleConnection, nv.MANV, hotentxt.Text, phaitxt.Text, NgaySinh, diachitxt.Text, sdttxt.Text, selectedVaiTro, selectedNQL, selectedPhg);
                        if (check)
                        {
                            MessageBox.Show("cập nhật thành công ");
                        }
                        else
                        {
                            MessageBox.Show("cập nhật thất bại !");
                        }

                    }
                    else
                    {
                        MessageBox.Show("cập nhật thất bại !");
                    }
                }
                else
                {
                    if (updateNgaySinh.HasValue && diachitxt.Text != "" && sdttxt.Text != "")
                    {
                        string NgaySinh = updateNgaySinh.Value.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        EmployeeViewModel empUpdate = new EmployeeViewModel();

                        var check = empUpdate.updateEmployee(_oracleConnection, nv.MANV, hotentxt.Text, phaitxt.Text, NgaySinh, diachitxt.Text, sdttxt.Text, vaitrotxt.Text, nv.MANQL, nv.MAPHG);
                        if (check)
                        {
                            MessageBox.Show("cập nhật thành công ");
                        }
                        else
                        {
                            MessageBox.Show("cập nhật thất bại !");
                        }

                    }
                    else
                    {
                        MessageBox.Show("cập nhật thất bại !");
                    }
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
            NhanSuWindow nhansu = new NhanSuWindow();
            nhansu.buttonQLNhanVien_Click(null, null);
            nhansu.Show();
            this.Close();
        }

    }
}

