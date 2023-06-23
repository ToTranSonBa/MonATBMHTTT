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
    /// Interaction logic for ProjectUC.xaml
    /// </summary>
    public partial class ProjectUC : UserControl
    {
        private readonly DepartmentMV _departmentMV;
        private readonly List<DeAn_Class> _deAn_Classes;
        public ProjectUC(DepartmentMV departmentUC)
        {
            _departmentMV = departmentUC;
            _deAn_Classes = _departmentMV.GetDeAn_Classes();
            InitializeComponent();
            projectDataGrid.ItemsSource = _deAn_Classes;
        }
        private void SearchView_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void employeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
