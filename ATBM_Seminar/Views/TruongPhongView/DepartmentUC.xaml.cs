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
    /// Interaction logic for DepartmentUC.xaml
    /// </summary>
    public partial class DepartmentUC : UserControl
    {
        private readonly DepartmentMV _departmentMV;
        private readonly List<PhongBan_Class> _phongBan_Classes;
        public DepartmentUC(DepartmentMV departmentMV)
        {
            _departmentMV = departmentMV;
            _phongBan_Classes = _departmentMV.GetPhongBanList();
            
            InitializeComponent();

            departmentDataGrid.ItemsSource = _phongBan_Classes;
        }
        private void SearchView_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void employeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
