using ATBM_Seminar.ModelViews;
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

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for ViewController.xaml
    /// </summary>
    public partial class ViewController : UserControl
    {
        private AdminMV _admin;
        public ViewController(AdminMV admin)
        {
            _admin = admin;
            InitializeComponent();
            ViewDataGrid.ItemsSource = _admin.GetView();
        }

        private void SearchView_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ViewDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonDeleteView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
