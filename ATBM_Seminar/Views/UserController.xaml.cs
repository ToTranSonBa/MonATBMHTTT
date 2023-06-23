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
    /// Interaction logic for UserController.xaml
    /// </summary>
    public partial class UserController : UserControl
    {
        private ObservableCollection<Users> _listUser { get; set; }
        private AdminMV _admin;
        private UserControl _userControl;
        public UserController(AdminMV admin, UserControl userControl)
        {
            _admin = admin;
            _userControl = userControl;
            _listUser = _admin.GetUserDataGrid();
            InitializeComponent();
            membersDataGrid.ItemsSource = _listUser;
        }
        private void SearchUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = SearchUser.Text;
            membersDataGrid.ItemsSource = _listUser;
            if (textSearch != null)
            {
                var resultSearch = _listUser.Where(t => t.Name.Contains(textSearch));
                membersDataGrid.ItemsSource = resultSearch;
            }
        }
        private void buttonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users user = membersDataGrid.SelectedItem as Users;
                _admin.DropUser(user);
                MessageBox.Show("Drop user is success!");
                _userControl.Content = new UserController(_admin, _userControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            _userControl.Content = new AddUserView(_admin, _userControl);
        }

        private void buttonEditUserclick(object sender, RoutedEventArgs e)
        {
            Users username = membersDataGrid.SelectedItem as Users;
            ChangePwdUser_Window user_Window = new ChangePwdUser_Window(_admin, username.Name);
            user_Window.Show();
        }
    }
}
