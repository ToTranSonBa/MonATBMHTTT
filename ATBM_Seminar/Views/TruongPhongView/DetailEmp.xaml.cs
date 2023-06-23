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
    /// Interaction logic for DetailEmp.xaml
    /// </summary>
    public partial class DetailEmp : Window
    {
        private readonly NHANVIEN _NHANVIEN;
        public bool IsMaximized = false;
        public DetailEmp(NHANVIEN nHANVIEN)
        {
            _NHANVIEN = nHANVIEN;
            InitializeComponent();
            SetParamater();
        }

        public void SetParamater()
        {
            IdTextBox.Text = _NHANVIEN.MANV;
            nameTextBox.Text = _NHANVIEN.TENNV;
            sexTextBox.Text = _NHANVIEN.PHG;
            birthdayTextBox.Text = _NHANVIEN.NGAYSINH.ToString();
            phoneTextBox.Text = _NHANVIEN.S0DT.ToString();
            addrTextBox.Text = _NHANVIEN.DIACHI.ToString();
            departmentTextBox.Text = _NHANVIEN.PHG;
            roleTextBox.Text = _NHANVIEN.VAITRO;
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
    }
}
