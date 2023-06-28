using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using ATBM_Seminar.Models;
using ATBM_Seminar.ModelViews;

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for ChangeRoleUser.xaml
    /// </summary>
    public partial class ChangeRoleUser : Window
    {
        private AdminMV _admin { get; set; }
        public string roleName { get; set; }
        public ChangeRoleUser(AdminMV admin, string role)
        {
            _admin = admin;
            roleName = role;
            InitializeComponent();
            Loader();
        }

        private void Loader()
        {
            //create listbox role
            string[] roles = getRole().ToArray();
            ListBoxRoleOfUserchangeRole.ItemsSource = roles;

            //create listbox users
            string[] users = getuser().ToArray();
            ListBoxUserchangeRole.ItemsSource = users;
        }

        private void ChangeRoleOfUser(object sender, RoutedEventArgs e)
        {
            try
            {
                string commandText = $"GRANT {ListBoxRoleOfUserchangeRole.Text} TO {ListBoxUserchangeRole.Text} ";
                var isWithGrantOption = WithGrantOptionCheckBox.IsChecked;
                if (isWithGrantOption == true)
                {
                    commandText += "WITH ADMIN OPTION";
                }
                _admin.AddRoleForUser(commandText);
                MessageBox.Show("Change role of user is success! ", "Change Role user");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Change Role user");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            //Admin_Window admin = new Admin_Window(_admin.connection, _admin._role, _admin._user);
            this.Close();
            //admin.Show();
        }
        private List<string> getRole()
        {
            List<string> roles = new List<string>();
            try
            {
                roles = _admin.Getrole();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Change password user");
            }
            return roles;
        }
        private List<string> getuser()
        {
            List<string> users = new List<string>();
            try
            {
                users = _admin.GetUsers();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Change password user");
            }
            return users;
        }
        private void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            Window addUser = new Window();
        }
    }
}
