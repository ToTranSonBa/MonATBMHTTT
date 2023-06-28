using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ATBM_Seminar.Views.TaiChinhView
{
    /// <summary>
    /// Interaction logic for FinanEmployeesUC.xaml
    /// </summary>
    /// 
    public partial class FinanEmployeesUC : UserControl
    {
        private readonly OracleConnection _oracleConnection;
        public FinanEmployeesUC(OracleConnection conn)
        {
            _oracleConnection = conn;
            InitializeComponent();
            LoadEmployees();
        }
        public void LoadEmployees()
        {
            try
            {
                EmployeeViewModel employee = new EmployeeViewModel();
                ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
                employees = employee.nhanVien_BL(_oracleConnection);
                if (employees == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    AgentsDataGrid.ItemsSource = employees;
                }
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        private void EmployeeDetail_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee = ((FrameworkElement)sender).DataContext as Employee;

            EmployeeDetailsInf detailEmployee_window = new EmployeeDetailsInf(_oracleConnection ,employee.MANV);
            detailEmployee_window.ShowDialog();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void equipmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }        
    }
}
