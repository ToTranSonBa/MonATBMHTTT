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
    /// Interaction logic for UpdateAssignment.xaml
    /// </summary>
    public partial class UpdateAssignment : Window
    {
        private readonly DepartmentMV _departmentMV;
        private readonly PhanCong_Class _phanCong_Class;
        public bool IsMaximized = false;
        public UpdateAssignment(DepartmentMV departmentMV, PhanCong_Class phanCong_Class)
        {
            _departmentMV = departmentMV;
            _phanCong_Class = phanCong_Class;
            InitializeComponent();
            projectTextBox.Text = _phanCong_Class.TENDA;
            empTextBox.Text = _phanCong_Class.TENNV;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var time = timeTextBox.Text as string;
            var check = _departmentMV.updateAssignment(_phanCong_Class.MANV, _phanCong_Class.MADA, time);
            if(check )
            {
                MessageBox.Show("cập nhật thành công.");
            }
            else
            {
                MessageBox.Show("cập nhật thất bại.");
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

        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
