using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for View_PhanCong.xaml
    /// </summary>
    public partial class View_PhanCong : UserControl
    {
        private readonly OracleConnection _connection;
        public View_PhanCong(OracleConnection conn)
        {
            _connection = conn;
            InitializeComponent();
            LoadPhanCong();
        }
        public void LoadPhanCong()
        {
            try
            {
                PhanCongViewModel phancong = new PhanCongViewModel();
                ObservableCollection<PhanCong> list_phancong = new ObservableCollection<PhanCong>();
                list_phancong = phancong.showPhanCong(_connection);

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
    }
}
