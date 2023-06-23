using ATBM_Seminar.ModelViews;
using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
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


namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for ChangePwdUser_Window.xaml
    /// </summary>
    public partial class ChangePwdUser_Window : Window
    {
        private readonly AdminMV _admin;
        private readonly string _userName;
        public ChangePwdUser_Window(AdminMV admin, string username)
        {
            _admin = admin;
            InitializeComponent();
            _userName = username;
            txtUserName.Text = _userName;
            ListBoxUserchangeRole.Text = _userName;
            privilegeName.Text = privilegeName.Text + " " + _userName;
            ChangeRoleUserName.Text = ChangeRoleUserName.Text + " " + _userName;
            ChangePasswordNameUser.Text = ChangePasswordNameUser.Text + " " + _userName;
            ButtonChangePasswordUser_Click(null, null);

        }

        private void ChangePasswordUser(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBoxResult.None;
            try
            {
                
                // get user and password
                TextBox textBox = new TextBox();
                textBox = txtUserName;

                PasswordBox passwordBox = new PasswordBox();
                passwordBox = txtPassword;
                string pwd = passwordBox.Password;

                _admin.ChangePwdUser(_userName, pwd);
                //show message
                result = MessageBox.Show("Change Password is success!", "Change password user", MessageBoxButton.OK);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Change password user");
            }

            //catch result messagebox
            switch (result)
            {
                case MessageBoxResult.OK:
                    Admin_Window admin_Window = new Admin_Window(_admin.connection, _admin._role, _admin._user);
                    admin_Window.Show();
                    this.Close();
                    break;
                case MessageBoxResult.None:
                    MessageBox.Show("None");
                    break;
                default:
                    break;
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

        public OracleConnection connectDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin_Window admin = new Admin_Window(_admin.connection, _admin._role, _admin._user);
            admin.Show();
            this.Close();
        }
        private void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            Window addUser = new Window();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
        private void ShowPrivilegeOfUser()
        {
            try
            {
                ObservableCollection<privileges> priv = new ObservableCollection<privileges>();
                var converter = new BrushConverter();
                OracleConnection connection = connectDatabase();
                string SQLcontext = $"Select * from dba_tab_privs where grantee = '{_userName}'";
                OracleCommand cmd = new OracleCommand(SQLcontext, connection);
                OracleDataReader reader = cmd.ExecuteReader();
                string[] Color = new string[] { "#1E88E5", "#FF8F00", "#FF5252", "#0CA678", "#6741D9", "#FF6D00", "#FF5252", "#1E88E5", "#0CA678" };
                int i = 1;
                while (reader.Read())
                {
                    string grantee_tmp = reader.GetString(reader.GetOrdinal("GRANTEE"));
                    string owner_tmp = reader.GetString(reader.GetOrdinal("OWNER"));
                    string Table_name_tmp = reader.GetString(reader.GetOrdinal("TABLE_NAME"));
                    string Grantor_tmp = reader.GetString(reader.GetOrdinal("GRANTOR"));
                    string Privs_tmp = reader.GetString(reader.GetOrdinal("PRIVILEGE"));
                    string GrantTable_tmp = reader.GetString(reader.GetOrdinal("GRANTABLE"));
                    string hierarchy_tmp = reader.GetString(reader.GetOrdinal("HIERARCHY"));
                    string common_tmp = reader.GetString(reader.GetOrdinal("COMMON"));
                    string Type_tmp = reader.GetString(reader.GetOrdinal("TYPE"));
                    string Inherite_tmp = reader.GetString(reader.GetOrdinal("INHERITED"));
                    priv.Add(new privileges { grantee = grantee_tmp, owner = owner_tmp, BgColor = (Brush)converter.ConvertFromString(Color[(i % 7)]), Number = i.ToString(),
                            Table_name = Table_name_tmp, Grantor = Grantor_tmp, Privs = Privs_tmp, GrantTable = Grantor_tmp, hierarchy = hierarchy_tmp, common = common_tmp, Type = Type_tmp, Inherite = Inherite_tmp });
                    i++;
                }
                reader.Close();
                connection.Close();
                membersDataGrid.ItemsSource = priv;
                
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        private void Loader()
        {
            //create listbox role
            string[] roles = getRole().ToArray();
            ListBoxRoleOfUserchangeRole.ItemsSource = roles;

            //create listbox users
        }
        private void ChangeRoleOfUser(object sender, RoutedEventArgs e)
        {
            try
            {
                //connect database with privilege sysdba
                //OracleConnection connection = connectDatabase();
                // Define and execute SQL
                string commandText = $"GRANT {ListBoxRoleOfUserchangeRole.Text} TO {ListBoxUserchangeRole.Text} ";
                var isWithGrantOption = WithGrantOptionCheckBox.IsChecked;
                if (isWithGrantOption == true)
                {
                    commandText += "WITH ADMIN OPTION";
                }
                //OracleCommand cmd = new OracleCommand(commandText, connection);
                //OracleDataReader reader = cmd.ExecuteReader();
                //connection.Close();
                _admin.AddRoleForUser(commandText);
                MessageBox.Show("Change role of user is success! ", "Change Role user");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Change Role user");
            }
        }
        private List<string> getRole()
        {
            List<string> roles = new List<string>();
            try
            {
                roles =  _admin.Getrole();
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
        private void ButtonChangePasswordUser_Click(object sender, RoutedEventArgs e)
        {
            ButtonChangePassword.Style = this.FindResource("isSelectButton") as Style;
            ButtonChangeRoleUser.Style = this.FindResource("menuButton") as Style;
            ButtonChangePrivsUser.Style = this.FindResource("menuButton") as Style;
            ChangeRoleUserWindow.Visibility = Visibility.Hidden;
            ChangePasswordUserWindow.Visibility = Visibility.Visible;
            PrivilegeUser.Visibility = Visibility.Hidden;
        }

        private void ButtonChangePrivsUser_Click(object sender, RoutedEventArgs e)
        {
            ButtonChangePrivsUser.Style = this.FindResource("isSelectButton") as Style;
            ButtonChangePassword.Style = this.FindResource("menuButton") as Style;
            ButtonChangeRoleUser.Style = this.FindResource("menuButton") as Style;

            PrivilegeUser.Visibility = Visibility.Visible;
            ChangePasswordUserWindow.Visibility = Visibility.Hidden;
            ChangeRoleUserWindow.Visibility = Visibility.Hidden;

            ShowPrivilegeOfUser();
        }

        private void ButtonChangeRoleUser_Click(object sender, RoutedEventArgs e)
        {
            ButtonChangeRoleUser.Style = this.FindResource("isSelectButton") as Style;
            ButtonChangePassword.Style = this.FindResource("menuButton") as Style;
            ButtonChangePrivsUser.Style = this.FindResource("menuButton") as Style;
            Loader();
            ChangeRoleUserWindow.Visibility = Visibility.Visible;
            ChangePasswordUserWindow.Visibility = Visibility.Hidden;
            PrivilegeUser.Visibility = Visibility.Hidden;

        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void buttonDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                privileges privs = membersDataGrid.SelectedItem as privileges;
                string SQLcontex = $"Revoke {privs.Privs} on {privs.Table_name} from {_userName}";
                _admin.DeleteRole(SQLcontex);
                MessageBox.Show("Revoke is success!");
                ButtonChangePrivsUser_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonAddPrivsUser_Click(object sender, RoutedEventArgs e)
        {
            GrantPrivsUser window = new GrantPrivsUser(_admin, _userName);
            window.Show();
            this.Close();
        }
    }

}
