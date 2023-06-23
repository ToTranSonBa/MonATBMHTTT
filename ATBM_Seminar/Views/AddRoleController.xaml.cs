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
    /// Interaction logic for AddRoleController.xaml
    /// </summary>
    public partial class AddRoleController : UserControl
    {
        private readonly UserControl _userControl;
        private readonly AdminMV _admin;
        public AddRoleController(AdminMV adminMV, UserControl userControl)
        {
            _admin = adminMV;
            _userControl = userControl;
            InitializeComponent();
        }

        private void addNewRole(object sender, RoutedEventArgs e)
        {
            string text = "ATBMHTTT_ROLE_" + txtAddRole.Text;
            try
            {
                _admin.CreateRole(text);
                MessageBox.Show("Create role " + text + " Success!");
                _userControl.Content = new UserController(_admin, _userControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
