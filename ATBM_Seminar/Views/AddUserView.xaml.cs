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
    /// Interaction logic for AddUserView.xaml
    /// </summary>
    public partial class AddUserView : UserControl
    {
        private readonly AdminMV _admin;
        private UserControl _userControl;
        public AddUserView(AdminMV admin, UserControl userControl)
        {
            _admin = admin;
            _userControl = userControl;
            InitializeComponent();
            comboboxAddUser.ItemsSource = _admin.getUserNotAccount();
        }

        private void addNewUser(object sender, RoutedEventArgs e)
        {
            string username = comboboxAddUser.Text.ToString();
            string password = passwordBox.Password.ToString();
            try
            {
                _admin.AddNewUser(username, password);
                MessageBox.Show("Create User Success!");
                _userControl.Content = new UserController(_admin, _userControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
