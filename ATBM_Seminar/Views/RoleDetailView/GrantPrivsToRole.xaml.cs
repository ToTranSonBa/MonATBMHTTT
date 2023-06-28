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

namespace ATBM_Seminar.Views.RoleDetailView
{
    /// <summary>
    /// Interaction logic for GrantPrivsToRole.xaml
    /// </summary>
    public partial class GrantPrivsToRole : UserControl
    {
        private readonly AdminMV _adminMV;
        private readonly string _roleName;
        private readonly List<string> privs;
        private readonly List<string> tabs;
        public GrantPrivsToRole(AdminMV adminMV, string rolename)
        {
            _adminMV = adminMV;
            _roleName = rolename;
            InitializeComponent();
            privs = new List<string>();
            privs.Add("insert");
            privs.Add("update");
            privs.Add("delete");
            privs.Add("select");
            var listtable = _adminMV.getTable();
            tabs = listtable.Select(x => x.Name).ToList();
            tableCB.ItemsSource = tabs;
            PrivCB.ItemsSource = privs;
        }

        private void tableCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tab = tableCB.SelectedItem as string;
            List<string> cols = _adminMV.getColOfTable(tab);
            colCB.ItemsSource = cols;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sql = $"Grant";
            string col = colCB.Text as string;
            string tab = tableCB.Text as string;
            string priv = PrivCB.Text as string;
            if (priv != "" && priv != null && priv != "Privilege")
            {
                sql += " " + priv;
            }
            else
            {
                MessageBox.Show("Chưa có quyền.");
                return;
            }
            if (col != "" && col != null && col != "Column")
            {
                sql  +=  $"({col}) ";
            }
            if (tab != "" && tab != null && tab != "Table")
            {
                sql += " on ATBMHTTT_TABLE_" + tab;
            }
            else
            {
                MessageBox.Show("Chưa có bảng.");
                return;
            }
            sql += $" to ATBMHTTT_ROLE_{_roleName}";
            var check = _adminMV.executeSQL(sql);
             if(check)
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại.");
            }
        }

        private void colCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string col = colCB.SelectedItem as string;
            if (col != null && col != "" && col != "Column")
            {
                PrivCB.ItemsSource = privs.Where(p => p != "insert" && p != "delete");
            }
        }
    }
}
