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

namespace ATBM_Seminar.Views.NhanVienView
{
    /// <summary>
    /// Interaction logic for ProjectUC.xaml
    /// </summary>
    public partial class ProjectUC : UserControl
    {
        private readonly OracleConnection _oracleConnection;
        public ProjectUC(OracleConnection conn)
        {
            _oracleConnection = conn;
            InitializeComponent();
            LoadDeAn();
        }
        public void LoadDeAn()
        {
            try
            {
                ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
                DeAnViewModel dean = new DeAnViewModel();
                list_dean = dean.deAn_BL(_oracleConnection);

                if (list_dean == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    DeAnDataGrid.ItemsSource = list_dean;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
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
