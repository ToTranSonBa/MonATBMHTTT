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
    /// Interaction logic for AssignmentUC.xaml
    /// </summary>
    public partial class AssignmentUC : UserControl
    {
        private readonly DepartmentMV _departmentMV;
        private readonly List<PhanCong_Class>  _Lphancong;
        public AssignmentUC(DepartmentMV departmentMV)
        {
            _departmentMV = departmentMV;
            InitializeComponent();
            assignmentDataGrid.ItemsSource = _departmentMV.getAssignment();
        }


        private void SearchView_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void employeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void detailEmpBtn(object sender, RoutedEventArgs e)
        {

        }

        private void deleteAssignmentBtn_Click(object sender, RoutedEventArgs e)
        {
            var emp = assignmentDataGrid.SelectedItem as PhanCong_Class;
            var check = _departmentMV.deleteAssignment(emp.MANV, emp.MADA);
            if(check)
            {
                MessageBox.Show("Xóa thành công.");
            }
            else 
            {
                MessageBox.Show("Xóa thất bại.");
            }
        }

        private void editAssignmentBtn_Click(object sender, RoutedEventArgs e)
        {
            var assign = assignmentDataGrid.SelectedItem as PhanCong_Class;
            var updateWindow = new UpdateAssignment(_departmentMV, assign);
            updateWindow.ShowDialog();
        }
    }
}
