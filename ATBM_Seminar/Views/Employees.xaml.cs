using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : UserControl
    {
        private readonly OracleConnection _connection;
        public Employees(OracleConnection conn)
        {
              _connection = conn;
            InitializeComponent();
        }

        public void LoadAgents(string role)
        {
            try
            {
                EmployeeViewModel employee = new EmployeeViewModel();
                ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
                employees = employee.showEmployee_Agent(_connection, role);
                if (employees == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    AgentsDataGrid.ItemsSource = employees;
                }
            }
            catch(OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }

        private void VaiTro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)VaiTro.SelectedItem;
            string value = typeItem.Content.ToString();
            if (value != "VAI TRÒ")
            {
                LoadAgents(value);
            }
        }

        private void EmployeeDetail_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee = ((FrameworkElement)sender).DataContext as Employee;

            DetailEmployee detailEmployee_window = new DetailEmployee(_connection, employee.MANV);
            detailEmployee_window.ShowDialog();
        }
    }
}
