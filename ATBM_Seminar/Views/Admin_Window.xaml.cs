using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using ATBM_Seminar.Models;
using System.Collections.Generic;
using ATBM_Seminar.ModelViews;
using ATBM_Seminar.Views.AdminView;

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        private OracleConnection _connection;
        private string _role;
        private string _user;
        private AdminMV _admin;
        private ObservableCollection<Users> _listUser { get; set; }
        private ObservableCollection<Roles_Window> _listRole { get; set; }
        private ObservableCollection<Table> _listTable { get; set; }
        public Admin_Window(OracleConnection conn, string Role, string user)
        {
            _connection = conn;
            _role = Role;
            _user = user;
            _admin = new AdminMV(_connection, _role, user);
            _listUser = _admin.GetUserDataGrid();
            _listRole = _admin.GetRolesDataGrid();
            _listTable = _admin.getTable();
            InitializeComponent();
            userButton.Style = this.FindResource("isSelectButton") as Style;
            tableviewButton.Style = this.FindResource("menuButton") as Style;
            LoaderWindow();
        }
        private void LoaderWindow()
        {
            try
            {
                _admin.startQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //ClickButtonUser(null, null);
            UserController.Content = new UserController(_admin, UserController);
        }


        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public bool IsMaximized = false;
        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2) {
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

        public void ClickButtonUser(object sender, RoutedEventArgs e)
        {
            userButton.Style = this.FindResource("isSelectButton") as Style;
            roleButton.Style = this.FindResource("menuButton") as Style;
            tableviewButton.Style = this.FindResource("menuButton") as Style;
            viewsViewButton.Style = this.FindResource("menuButton") as Style;
            auditingBtn.Style = this.FindResource("menuButton") as Style;
            UserController.Content = new UserController(_admin, UserController);
        }
        public void ClickButtonRole(object sender, RoutedEventArgs e)
        {
            roleButton.Style = this.FindResource("isSelectButton") as Style;
            userButton.Style = this.FindResource("menuButton") as Style;
            tableviewButton.Style = this.FindResource("menuButton") as Style;
            viewsViewButton.Style = this.FindResource("menuButton") as Style;
            auditingBtn.Style = this.FindResource("menuButton") as Style;
            UserController.Content = new RolesController(_admin, UserController);
        }

        private void tableviewButton_Click(object sender, RoutedEventArgs e)
        {
            userButton.Style = this.FindResource("menuButton") as Style;
            roleButton.Style = this.FindResource("menuButton") as Style;
            tableviewButton.Style = this.FindResource("isSelectButton") as Style;
            viewsViewButton.Style = this.FindResource("menuButton") as Style;
            auditingBtn.Style = this.FindResource("menuButton") as Style;
            UserController.Content = new TableController(_admin);
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            _connection.Close();
            var loginWindow = new Login();
            this.Close();
            loginWindow.ShowDialog();
            
        }

        private void viewsViewButton_Click(object sender, RoutedEventArgs e)
        {
            userButton.Style = this.FindResource("menuButton") as Style;
            roleButton.Style = this.FindResource("menuButton") as Style;
            tableviewButton.Style = this.FindResource("menuButton") as Style;
            viewsViewButton.Style = this.FindResource("isSelectButton") as Style;
            auditingBtn.Style = this.FindResource("menuButton") as Style;
            UserController.Content = new ViewController(_admin);
        }

        private void auditingBtn_Click(object sender, RoutedEventArgs e)
        {
            userButton.Style = this.FindResource("menuButton") as Style;
            roleButton.Style = this.FindResource("menuButton") as Style;
            tableviewButton.Style = this.FindResource("menuButton") as Style;
            viewsViewButton.Style = this.FindResource("menuButton") as Style;
            auditingBtn.Style = this.FindResource("isSelectButton") as Style;
            UserController.Content = new AuditingUC(_admin);
        }
    }
}
