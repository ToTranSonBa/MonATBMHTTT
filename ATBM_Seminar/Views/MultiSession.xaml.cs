using Oracle.ManagedDataAccess.Client;
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
using System.Windows.Shapes;
using ATBM_Seminar.Models;
using ATBM_Seminar.ModelViews;

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for MultiSession.xaml
    /// </summary>
    /// 
    /// Xử lý user có nhiều role
    public partial class MultiSession : Window
    {
        private OracleConnection _connection;
        private List<string> _role;
        private string _user;
        public MultiSession(OracleConnection conn, List<string> Role, string user)
        {
            _connection = conn;
            _role = Role;
            _user = user;
            InitializeComponent();
            loaderSession();
        }
        private void directWindowUser(string Role)
        {
            switch (Role)
            {
                case "R_NHANVIEN":
                    break;
                case "R_TRUONGPHONG":
                    break;
                case "R_GIAMDOC":
                    break;
                case "DBA":
                    {
                        Admin_Window admin_Window = new Admin_Window(_connection, Role, _user);
                        admin_Window.Show();
                        this.Close();
                        return;
                    }
                default:
                    MessageBox.Show($"The application don't has role {_role}");
                    break;
            }

        }

        private void loaderSession() {
            listSession.ItemsSource = _role;
        }

        private void BtnSubmitMultiSession_Click(object sender, RoutedEventArgs e)
        {
            string role = listSession.SelectedItem.ToString();
            directWindowUser(role);
        }
    }
}
