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
    /// Interaction logic for UserOfRole.xaml
    /// </summary>
    public partial class UserOfRole : UserControl
    {
        private readonly AdminMV _adminMV;
        private readonly string _roleName;
        public UserOfRole(AdminMV adminMV, string Rolename)
        {
            _adminMV = adminMV;
            _roleName = Rolename;
            InitializeComponent();
            UserOfRoleDataGrid.ItemsSource = _adminMV.GetUsersOfRole(_roleName);
        }

        private void buttonDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            var user = UserOfRoleDataGrid.SelectedItem as Users;
            var check = _adminMV.RevokeUserFromRole(_roleName, user.Name);
            if(check)
            {
                MessageBox.Show("Xóa thành công.");
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
            UserOfRoleDataGrid.ItemsSource = _adminMV.GetUsersOfRole(_roleName);
        }
    }
}
