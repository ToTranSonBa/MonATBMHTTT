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
    /// Interaction logic for PrivsOfRole.xaml
    /// </summary>
    public partial class PrivsOfRole : UserControl
    {
        private readonly AdminMV _adminMV;
        private readonly string _roleName;
        public PrivsOfRole(AdminMV adminMV, string role)
        {
            _adminMV = adminMV;
            _roleName = role;
            InitializeComponent();
            UserOfRoleDataGrid.ItemsSource = _adminMV.GetPrivsOfRole(_roleName);
        }

        private void buttonDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            var priv = UserOfRoleDataGrid.SelectedItem as privileges;
            _adminMV.RevokePrivFromRole(_roleName, priv);
            UserOfRoleDataGrid.ItemsSource = _adminMV.GetPrivsOfRole(_roleName);
        }
    }
}
