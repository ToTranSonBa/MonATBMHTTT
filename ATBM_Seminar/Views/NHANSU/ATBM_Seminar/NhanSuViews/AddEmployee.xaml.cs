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
    public partial class AddEmployee : Window
    {   
        public int number { get; set; }

        private readonly OracleConnection _oracleConnection;
        public AddEmployee(int count, OracleConnection conn)
        {
            InitializeComponent();
            number = count;
            _oracleConnection = conn;
            buttonEmpInfor_Click(null, null);
        }

        private void buttonEmpInfor_Click(object sender, RoutedEventArgs e)
        {

            try
            {

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
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }


        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {

                number = number + 1;
                string newEmpNum = "";
                if (number < 9)
                {
                    newEmpNum = "NV00" + number.ToString();
                }
                else
                {
                    newEmpNum = "NV0" + number.ToString();
                }

                string selectedPgh = phongComboBox.SelectedValue as string;
                string selecttedPhai = phaiComboBox.Text as string;
                string selectedVaiTro = vaiTroComboBox.SelectedValue as String;
                string selectedNQL = null;
                selectedNQL = nqlComboBox.SelectedValue as String;
                DateTime? insertNgaySinh = ngaysinhtxt.SelectedDate;
                //MessageBox.Show(selectedPgh +"   "+ selecttedPhai+"   "+ selectedVaiTro+"   "+ selectedNQL+"    "+ newEmpNum+"   "+ hotentxt.Text + "   " + ngsinhtxt.Text + "   " + sdttxt.Text + "   " + diachitxt.Text);
                if (insertNgaySinh.HasValue && phongComboBox.SelectedItem != null && vaiTroComboBox.SelectedItem != null&& phaiComboBox.Text != null && hotentxt.Text!="" && diachitxt.Text !="" && sdttxt.Text != "")
                {
                    string NgaySinh = insertNgaySinh.Value.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    EmployeeViewModel empInsert = new EmployeeViewModel();
                    empInsert.insertEmployee(_oracleConnection, newEmpNum, hotentxt.Text, selecttedPhai, NgaySinh, diachitxt.Text, sdttxt.Text, selectedVaiTro, selectedNQL, selectedPgh);
                    MessageBox.Show("thêm nhân viên thành công ");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại !");
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

