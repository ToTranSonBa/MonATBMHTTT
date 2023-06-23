using ATBM_Seminar.ModelViews;
using ServerATBM.Controllers;
using System;
using System.Collections.Generic;
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

namespace ATBM_Seminar.Views.TruongPhongView
{
    /// <summary>
    /// Interaction logic for PersonalUC.xaml
    /// </summary>
    public partial class PersonalUC : Window
    {
        private readonly NHANVIEN _NHANVIEN;
        private readonly DepartmentMV _departmentMV;
        public bool IsMaximized = false;
        public PersonalUC(NHANVIEN nHANVIEN, DepartmentMV departmentMV)
        {
            _NHANVIEN = nHANVIEN;
            _departmentMV = departmentMV;
            InitializeComponent();
            SetParamater();
        }

        public void SetParamater()
        {
            IdTextBox.Text = _NHANVIEN.MANV;
            nameTextBox.Text = _NHANVIEN.TENNV;
            sexTextBox.Text = _NHANVIEN.PHG;
            birthdayTextBox.Text = _NHANVIEN.NGAYSINH.ToString();
            phoneTextBox.Text = _NHANVIEN.S0DT;
            addrTextBox.Text = _NHANVIEN.DIACHI;
            departmentTextBox.Text = _NHANVIEN.PHG;
            roleTextBox.Text = _NHANVIEN.VAITRO;
            salaryTextBox.Text = _NHANVIEN.LUONG.ToString();
            phucapTextBox.Text = _NHANVIEN.PHUCAP.ToString();
        }

        private void exitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var birthday = birthdayTextBox.Text as string;
            var numberPhone = phoneTextBox.Text;
            var addr = addrTextBox.Text;
            if (birthday!= null && numberPhone != null  && addr != null)
            {
                bool check = _departmentMV.UpdatePersonalInfomation(_NHANVIEN.MANV, numberPhone, birthday, addr);
                if (check)
                {
                    MessageBox.Show("cập nhật thành công.");
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.");
                }
            }
            else
            {
                MessageBox.Show("Thông tin bị rỗng, không thể cập nhật.");
            }
        }
    }
}
