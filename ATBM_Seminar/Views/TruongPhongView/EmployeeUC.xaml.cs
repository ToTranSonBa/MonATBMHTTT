using ATBM_Seminar.ModelViews;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for EmployeeUC.xaml
    /// </summary>
    public partial class EmployeeUC : UserControl
    {
        private readonly DepartmentMV _departmentMV;
        private readonly List<NHANVIEN> _nHANVIENs;

        public EmployeeUC(DepartmentMV departmentMV)
        {
            _departmentMV = departmentMV;
            _nHANVIENs = _departmentMV.GetEmployee();
            InitializeComponent();
            EmpDataGrid.ItemsSource = _nHANVIENs;
        }



        private void editEmp_Click(object sender, RoutedEventArgs e)
        {
            var emp = EmpDataGrid.SelectedItem as NHANVIEN;
            var empWindow = new DetailEmp(emp);
            empWindow.ShowDialog();
        }

        private void EmpDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchViews_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
