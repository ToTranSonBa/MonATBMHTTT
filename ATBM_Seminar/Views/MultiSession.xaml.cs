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
using ATBM_Seminar.Views.TruongDeAn;
using ATBM_Seminar.Views.TruongPhongView;
using static System.Collections.Specialized.BitVector32;
using System.Data;

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
        private void directWindowUser(string role)
        {
            switch (role)
            {
                case "ATBMHTTT_ROLE_NHANVIEN":
                    break;
                case "ATBMHTTT_ROLE_TRUONGPHONG":
                    TruongPhongHome department = new TruongPhongHome(_connection, role, _user);
                    this.Close();
                    department.Show();
                    break;
                case "ATBMHTTT_ROLE_GIAMDOC":
                    var giamdoc = new GiamDoc_Window(_connection);
                    this.Close();
                    giamdoc.Show();
                    break;
                case "ATBMHTTT_ROLE_QLTRUCTIEP":
                    break;
                case "ATBMHTTT_ROLE_TAICHINH":
                    break;
                case "ATBMHTTT_ROLE_NHANSU":
                    break;
                case "ATBMHTTT_ROLE_TRUONGDEAN":
                    var trdean = new TruongDeAn_Window(_connection);
                    this.Close();
                    trdean.Show();
                    break;
                case "DBA":
                    {
                        Admin_Window admin_Window = new Admin_Window(_connection, role, _user);
                        admin_Window.Show();
                        this.Close();
                        return;
                    }
                default:
                    MessageBox.Show($"The application don't has role {role}");
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
