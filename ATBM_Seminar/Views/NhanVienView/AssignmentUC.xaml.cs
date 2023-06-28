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
    /// Interaction logic for AssignmentUC.xaml
    /// </summary>
    public partial class AssignmentUC : UserControl
    {
        private readonly OracleConnection _oracleConnection;
        public AssignmentUC(OracleConnection connn)
        {
            _oracleConnection = connn;
            InitializeComponent();
            LoadPhanCong();
        }

        public void LoadPhanCong()
        {
            try
            {
                PhanCongViewModel phancong = new PhanCongViewModel();
                ObservableCollection<PhanCong> list_phancong = new ObservableCollection<PhanCong>();
                list_phancong = phancong.phanCong_BL(_oracleConnection);

                if (list_phancong == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    PhanCongDataGrid.ItemsSource = list_phancong;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
             
        }

        private void equipmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
