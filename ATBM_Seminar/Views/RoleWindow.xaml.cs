using ATBM_Seminar.ModelViews;
using ATBM_Seminar.Views.RoleDetailView;
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
    /// Interaction logic for RoleWindow.xaml
    /// </summary>
    public partial class RoleWindow : Window
    {
        private AdminMV _admin { get; set; }
        public string roleName { get; set; }
        public RoleWindow(AdminMV admin, string roleChoosed)
        {
            _admin = admin;
            InitializeComponent();
            roleName = roleChoosed;
            buttonUserOfRole_Click(null, null);
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
            this.Close();
        }

        private void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            Window addUser = new Window();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        //

        //private void ShowUsersOfRole()
        //{
        //    try
        //    {
        //        var converter = new BrushConverter();
        //        ObservableCollection<users_role> priv= new ObservableCollection<users_role>();

        //        OracleConnection connection = connectDatabase();

        //        string SQLcontext = $"select * from dba_role_privs where granted_role = '{roleName}'";
        //        OracleCommand cmd = new OracleCommand(SQLcontext, connection);
        //        OracleDataReader reader = cmd.ExecuteReader();

        //        string[] Color = new string[] { "#1E88E5", "#FF8F00", "#FF5252", "#0CA678", "#6741D9", "#FF6D00", "#FF5252", "#1E88E5", "#0CA678" };


        //        int i = 1;
        //        while (reader.Read())
        //        {
        //            string grantee_tmp = reader.GetString(reader.GetOrdinal("GRANTEE"));
        //            string common_tmp = reader.GetString(reader.GetOrdinal("COMMON"));
        //            string Inherited_tmp = reader.GetString(reader.GetOrdinal("INHERITED"));
        //            string granted_role_tmpp = reader.GetString(reader.GetOrdinal("GRANTED_ROLE"));
        //            string admin_option_role = reader.GetString(reader.GetOrdinal("ADMIN_OPTION"));
        //            string default_role_tmp = reader.GetString(reader.GetOrdinal("DELEGATE_OPTION"));
        //            string delegate_option_tmp = reader.GetString(reader.GetOrdinal("DEFAULT_ROLE"));
        //            priv.Add(new users_role { grantee = grantee_tmp, BgColor = (Brush)converter.ConvertFromString(Color[(i % 7)]), Number = i.ToString(), granted_role = granted_role_tmpp,  delegate_option = delegate_option_tmp,
        //                admin_option = admin_option_role, default_role = default_role_tmp, common = common_tmp, Inherited = Inherited_tmp });
        //            i++;
        //        }
        //        UserOfRoleDataGrid.ItemsSource = priv;
        //        reader.Close();
        //        connection.Close();
        //    }
        //    catch (OracleException exp)
        //    {
        //        MessageBox.Show("Failed: " + exp.Message);
        //    }
        //}

        //
        //private void ShowPrivilegeOfRole()
        //{
        //    try
        //    {
        //        var converter = new BrushConverter();
        //        ObservableCollection<privileges_role> priv = new ObservableCollection<privileges_role>();

        //        OracleConnection connection = connectDatabase();

        //        string SQLcontext = $"select * from dba_tab_privs where grantee = '{roleName}'";
        //        OracleCommand cmd = new OracleCommand(SQLcontext, connection);
        //        OracleDataReader reader = cmd.ExecuteReader();

        //        string[] Color = new string[] { "#1E88E5", "#FF8F00", "#FF5252", "#0CA678", "#6741D9", "#FF6D00", "#FF5252", "#1E88E5", "#0CA678" };


        //        int i = 1;
        //        while (reader.Read())
        //        {
        //            string grantee_tmp = reader.GetString(reader.GetOrdinal("GRANTEE"));
        //            string owner_tmp = reader.GetString(reader.GetOrdinal("OWNER"));
        //            string Table_name_tmp = reader.GetString(reader.GetOrdinal("TABLE_NAME"));
        //            string Grantor_tmp = reader.GetString(reader.GetOrdinal("GRANTOR"));
        //            string Privs_tmp = reader.GetString(reader.GetOrdinal("PRIVILEGE"));
        //            string GrantTable_tmp = reader.GetString(reader.GetOrdinal("GRANTABLE"));
        //            string hierarchy_tmp = reader.GetString(reader.GetOrdinal("HIERARCHY"));
        //            string common_tmp = reader.GetString(reader.GetOrdinal("COMMON"));
        //            string Type_tmp = reader.GetString(reader.GetOrdinal("TYPE"));
        //            string Inherite_tmp = reader.GetString(reader.GetOrdinal("INHERITED"));
        //            priv.Add(new privileges_role
        //            {
        //                grantee = grantee_tmp,
        //                owner = owner_tmp,
        //                BgColor = (Brush)converter.ConvertFromString(Color[(i % 7)]),
        //                Number = i.ToString(),
        //                Table_name = Table_name_tmp,
        //                Grantor = Grantor_tmp,
        //                Privs = Privs_tmp,
        //                GrantTable = Grantor_tmp,
        //                hierarchy = hierarchy_tmp,
        //                common = common_tmp,
        //                Type = Type_tmp,
        //                Inherite = Inherite_tmp
        //            });
        //            i++;
        //        }
        //        PrivsDataRole.ItemsSource = priv;
        //        reader.Close();
        //        connection.Close();
        //    }
        //    catch (OracleException exp)
        //    {
        //        MessageBox.Show("Failed: " + exp.Message);
        //    }
        //}




        //private void ButtonChangePasswordUser_Click(object sender, RoutedEventArgs e)
        //{
        //    UserofRoleName.Visibility = Visibility.Hidden;
        //}

        //private void ButtonChangePrivsUser_Click(object sender, RoutedEventArgs e)
        //{

        //    UserofRoleName.Visibility = Visibility.Visible;

        //    ShowPrivilegeOfRole();
        //}

        private void ButtonChangeRoleUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private class users_role
        {
            public string grantee { get; set; }
            public string granted_role { get; set; }
            public string admin_option { get; set; }
            public string delegate_option { get; set; }
            public string default_role { get; set; }
            public string common { get; set; }
            public string Inherited { get; set; }
            public Brush BgColor { get; set; }

            public string Number { get; set; }
        }

        private class privileges_role
        {
            public string grantee { get; set; }
            public string owner { get; set; }
            public string Table_name { get; set; }
            public string Grantor { get; set; }
            public string Privs { get; set; }
            public string GrantTable { get; set; }
            public string hierarchy { get; set; }
            public string common { get; set; }
            public string Type { get; set; }
            public string Inherite { get; set; }
            public Brush BgColor { get; set; }

            public string Number { get; set; }
        }

        private void buttonUserOfRole_Click(object sender, RoutedEventArgs e)
        {
            buttonUserOfRole.Style = this.FindResource("isSelectButton") as Style;
            buttonPrivsOfRole.Style = this.FindResource("menuButton") as Style;
            addUser.Style = this.FindResource("menuButton") as Style;
            addPrivs.Style = this.FindResource("menuButton") as Style;
            PrivsCol.Style = this.FindResource("menuButton") as Style;
            RoleUserControl.Content = new UserOfRole(_admin, roleName);
        }

        private void buttonPrivsOfRole_Click(object sender, RoutedEventArgs e)
        {
            buttonUserOfRole.Style = this.FindResource("menuButton") as Style;
            buttonPrivsOfRole.Style = this.FindResource("isSelectButton") as Style;
            addUser.Style = this.FindResource("menuButton") as Style;
            addPrivs.Style = this.FindResource("menuButton") as Style;
            PrivsCol.Style = this.FindResource("menuButton") as Style;
            RoleUserControl.Content = new PrivsOfRole(_admin, roleName);
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            buttonUserOfRole.Style = this.FindResource("menuButton") as Style;
            buttonPrivsOfRole.Style = this.FindResource("menuButton") as Style;
            addUser.Style = this.FindResource("isSelectButton") as Style;
            addPrivs.Style = this.FindResource("menuButton") as Style;
            PrivsCol.Style = this.FindResource("menuButton") as Style;
            RoleUserControl.Content = new GrantRoleToUser(_admin, roleName);
        }

        private void addPrivs_Click(object sender, RoutedEventArgs e)
        {
            buttonUserOfRole.Style = this.FindResource("menuButton") as Style;
            buttonPrivsOfRole.Style = this.FindResource("menuButton") as Style;
            addUser.Style = this.FindResource("menuButton") as Style;
            addPrivs.Style = this.FindResource("isSelectButton") as Style;
            PrivsCol.Style = this.FindResource("menuButton") as Style;

            RoleUserControl.Content = new GrantPrivsToRole(_admin, roleName);
        }

        private void PrivsCol_Click(object sender, RoutedEventArgs e)
        {
            buttonUserOfRole.Style = this.FindResource("menuButton") as Style;
            buttonPrivsOfRole.Style = this.FindResource("menuButton") as Style;
            addUser.Style = this.FindResource("menuButton") as Style;
            addPrivs.Style = this.FindResource("menuButton") as Style;
            PrivsCol.Style = this.FindResource("isSelectButton") as Style;

            RoleUserControl.Content = new PrivilegeOnColumn(_admin, roleName);
        }

        //private void buttonRevokeUserOfRole_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //users_role users = UserOfRoleDataGrid.SelectedItem as users_role;
        //        OracleConnection connection = connectDatabase();
        //        string SQLcontex = $"Revoke  {roleName} from {users.grantee}";
        //        OracleCommand cmd = new OracleCommand(SQLcontex, connection);
        //        cmd.ExecuteNonQuery();
        //        connection.Close();
        //        MessageBox.Show($"Revoke {users.grantee} is success!");
        //        buttonUserOfRole_Click(null, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void buttonDeletePrivFromRole_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        privileges_role users = PrivsDataRole.SelectedItem as privileges_role;
        //        OracleConnection connection = connectDatabase();
        //        string SQLcontex = $"Revoke {users.Privs} on {users.Table_name}  from {roleName}";
        //        OracleCommand cmd = new OracleCommand(SQLcontex, connection);
        //        cmd.ExecuteNonQuery();
        //        connection.Close();
        //        MessageBox.Show("Revoke is success!");
        //        buttonUserOfRole_Click(null, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void buttonAddNewPrivs(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        privileges_role users = PrivsDataRole.SelectedItem as privileges_role;
        //        OracleConnection connection = connectDatabase();
        //        string SQLcontex = $" {users.Privs} on {users.Table_name}  from {roleName}";
        //        OracleCommand cmd = new OracleCommand(SQLcontex, connection);
        //        cmd.ExecuteNonQuery();
        //        connection.Close();
        //        MessageBox.Show("Revoke is success!");
        //        buttonUserOfRole_Click(null, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

    }
    
}
