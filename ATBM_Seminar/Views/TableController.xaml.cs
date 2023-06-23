using ATBM_Seminar.ModelViews;
using ATBM_Seminar.Models;
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

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for TableController.xaml
    /// </summary>
    public partial class TableController : UserControl
    {
        private readonly AdminMV _admin;
        private ObservableCollection<Models.Table> _listTable { get; set; }
        public TableController(AdminMV admin)
        {
            _admin = admin;
            InitializeComponent();
            _listTable = _admin.getTable();
           tableviewsDataGrid.ItemsSource = _listTable;
        }

        private void SearchTable_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = SearchTable.Text;
            tableviewsDataGrid.ItemsSource = _listTable;
            if (textSearch != null)
            {
                var resultSearch = _listTable.Where(t => t.Name.Contains(textSearch));
                tableviewsDataGrid.ItemsSource = resultSearch;
            }
        }

        private void tableviewsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
