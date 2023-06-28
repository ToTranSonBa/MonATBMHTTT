using ATBM_Seminar.Models;
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
    /// Interaction logic for PrivilegeOnColumn.xaml
    /// </summary>
    public partial class PrivilegeOnColumn : UserControl
    {
        private readonly AdminMV _adminMV;
        private readonly string _rolename;
        public PrivilegeOnColumn(AdminMV adminMV, string role)
        {
            _adminMV = adminMV;
            _rolename = role;
            InitializeComponent();
            UserOfRoleDataGrid.ItemsSource = _adminMV.GetPrivsOnColumnOfRole(_rolename);
        }

        private void buttonDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            var priv = UserOfRoleDataGrid.SelectedItem as privileges;
            var check = _adminMV.RevokePrivFromRole(_rolename, priv);
            if(check)
            {
                MessageBox.Show("Thu hồi thành công");
            }
            else
            {
                MessageBox.Show("Thu hồi thất bại");
            }
        }
    }
}
