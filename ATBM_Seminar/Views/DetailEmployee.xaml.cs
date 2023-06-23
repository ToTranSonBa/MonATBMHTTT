using Oracle.ManagedDataAccess.Client;
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
using Oracle.ManagedDataAccess.Types;
using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for DetailEmployee.xaml
    /// </summary>
    public partial class DetailEmployee : Window
    {
        public DetailEmployee(string MANV)
        {
            InitializeComponent();
            LoadDetailEmployee(MANV);
        }
        public void LoadDetailEmployee(string MANV)
        {
            try
            {
                Employee employee = new Employee();

                EmployeeViewModel empviewmoddel= new EmployeeViewModel();   
                employee = empviewmoddel.showDetail_Employee(MANV);
                this.DataContext = employee;

            }
            catch(OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
