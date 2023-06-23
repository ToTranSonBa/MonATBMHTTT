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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATBM_Seminar.Views.TruongPhongView
{
    /// <summary>
    /// Interaction logic for AddAssignmentUC.xaml
    /// </summary>
    public partial class AddAssignmentUC : UserControl
    {
        private readonly DepartmentMV _departmentMV;
        private readonly List<NHANVIEN> _nHANVIENs;
        private readonly List<DeAn_Class> _deAn_Classes;
        private readonly List<PhanCong_Class> _phancong;
        public AddAssignmentUC(DepartmentMV departmentMV)
        {
            _departmentMV = departmentMV;
            _nHANVIENs = _departmentMV.GetEmployee();
            _deAn_Classes = _departmentMV.getDean();
            _phancong = _departmentMV.getAssignment();

            List<string> danhSachTenNV = _nHANVIENs.Select(nv => nv.MANV).ToList();
            List<string> danhSachDeAn = _deAn_Classes.Select(nv => nv.MADA).ToList();

            InitializeComponent();

            Employee.ItemsSource = danhSachTenNV;
            project.ItemsSource = danhSachDeAn;
        }

        private void addAssignmentBtn(object sender, RoutedEventArgs e)
        {
            var manv = Employee.Text as string;
            var mada = project.Text as string;
            var tg = time.Text as string;
            var check = _departmentMV.AddAssignment(manv, mada, tg);
            if (check)
            {
                MessageBox.Show("Thêm phân công thành công");
            }
            else
            {
                MessageBox.Show("Thêm phân công không thành công");

            }
        }

        private void project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string assignment = project.SelectedItem.ToString();
            var list = (from nv in _nHANVIENs
                       join p in _phancong on nv.MANV equals p.MANV
                       join da in _deAn_Classes on p.MADA equals da.MADA
                       where da.MADA == assignment
                       select nv).ToList();
            var list2 = _nHANVIENs.Except(list).ToList();
            Employee.ItemsSource = list2.Select(nv => nv.MANV).ToList();
        }

        private void Employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string NV = Employee.SelectedItem.ToString();
            var list = (from nv in _nHANVIENs
                        join p in _phancong on nv.MANV equals p.MANV
                        join da in _deAn_Classes on p.MADA equals da.MADA
                        where nv.MANV == NV
                        select da).ToList();
            var list2 = _deAn_Classes.Except(list).ToList();
            project.ItemsSource = list2.Select(nv => nv.MADA).ToList();
        }
    }
}
