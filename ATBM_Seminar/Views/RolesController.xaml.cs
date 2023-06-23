using ATBM_Seminar.Models;
using ATBM_Seminar.ModelViews;
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
    /// Interaction logic for RolesController.xaml
    /// </summary>
    public partial class RolesController : UserControl
    {
        private ObservableCollection<Roles_Window> _listRole { get; set; }
        private AdminMV _admin;
        private UserControl _userControl;
        public RolesController(AdminMV admin, UserControl userControl)
        {
            _admin = admin;
            _userControl = userControl;
            InitializeComponent();
            _listRole = _admin.GetRolesDataGrid();
            rolesDataGrid.ItemsSource = _listRole;
            _userControl = userControl;
        }
        private void SearchRole_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = SearchRole.Text;
            rolesDataGrid.ItemsSource = _listRole;
            if (textSearch != null)
            {
                var resultSearch = _listRole.Where(t => t.Name.Contains(textSearch));
                rolesDataGrid.ItemsSource = resultSearch;
            }
        }
        private void buttonDeleteRoles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Roles_Window role = rolesDataGrid.SelectedItem as Roles_Window;
                _admin.DropRole(role);
                MessageBox.Show("Drop user is success!");
                //ClickButtonRole(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void rolesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_AddRole(object sender, RoutedEventArgs e)
        {
            _userControl.Content = new AddRoleController(_admin, _userControl);
        }

        private void buttonEditRoles_Click(object sender, RoutedEventArgs e)
        {
            var role = rolesDataGrid.SelectedItem as Roles_Window;
            var rolewindow = new RoleWindow(_admin, role.Name);
            rolewindow.Show();
        }
    }
}
