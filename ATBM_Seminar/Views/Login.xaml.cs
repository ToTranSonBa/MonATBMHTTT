using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
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
using ATBM_Seminar.Views.NhanSuViews;
using ATBM_Seminar.Views.NhanVienView;
using ATBM_Seminar.Views.QuanLyViews;
using ATBM_Seminar.Views.TaiChinhView;
using ATBM_Seminar.Views.TruongDeAn;
using ATBM_Seminar.Views.TruongPhongView;
using Oracle.ManagedDataAccess.Client;

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Connect connect = new Connect();
        public Login()
        {
            InitializeComponent();
        }
        private void directWindowUser(OracleConnection conn, Session session)
        {
            string role = session.Role;
            switch (role)
            {
                case "ATBMHTTT_ROLE_NHANVIEN":
                    NhanVienHome nhanVien = new NhanVienHome(conn, session.Username);
                    this.Close();
                    nhanVien.Show();
                    break;
                case "ATBMHTTT_ROLE_TRUONGPHONG":
                    TruongPhongHome department = new TruongPhongHome(conn, role, session.Username);
                    this.Close();
                    department.Show();
                    break;
                case "ATBMHTTT_ROLE_GIAMDOC":
                    var giamdoc = new GiamDoc_Window(conn);
                    this.Close();
                    giamdoc.Show();
                    break;
                case "ATBMHTTT_ROLE_QLTRUCTIEP":
                    var qltructiep = new QuanlyHome(conn);
                    this.Close();
                    qltructiep.Show();
                    break;
                case "ATBMHTTT_ROLE_TAICHINH":
                    FinancialStaffHome taiChinh = new FinancialStaffHome(conn, session.Username);
                    this.Close();
                    taiChinh.Show();
                    break;
                case "ATBMHTTT_ROLE_NHANSU":
                    var nhansu = new NhanSuWindow(conn, session.Username);
                    this.Close();
                    nhansu.Show();
                    break;
                case "ATBMHTTT_ROLE_TRUONGDEAN":
                    var trdean = new TruongDeAn_Window(conn);
                    this.Close();
                    trdean.Show();
                    break;
                case "DBA":
                    {
                        Admin_Window admin_Window = new Admin_Window(conn, role, session.Username);
                        this.Close();
                        admin_Window.Show();
                        return;
                    }
                default:
                    MessageBox.Show($"The application don't has role {role}");
                    break;
            }
            
        }
        private void HandleMultiRole(OracleConnection conn, List<string> role, string user)
        {
            MultiSession session = new MultiSession(conn, role, user);
            session.Show();
            this.Close();
        }
        private void HandleSession(OracleConnection conn, string user)
        {
            Session session = new Session();
            List<string> Lrole = new List<string>();
            //using (conn)
            //{
                //conn.Open();
            //string SQLcontex = $"alter session set \"_ORACLE_SCRIPT\"=true";
            //using (OracleCommand cmd = new OracleCommand(SQLcontex, conn))
            //{
            //    cmd.ExecuteNonQuery();
            //}
            int countRole = 0;
            string sqlRoleOfUser = $"select ROLE from SESSION_ROLES";
            using (OracleCommand cmd = new OracleCommand(sqlRoleOfUser, conn))
            {
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    
                    while(reader.Read())
                    {
                        Lrole.Add(reader.GetString(reader.GetOrdinal("ROLE")));
                        countRole++;
                    }
                }
                var role = Lrole.Where(hh => hh == "DBA" || hh.Contains("ATBMHTTT_ROLE_")).ToList();
                if (countRole < 1)
                {
                    MessageBox.Show("Account don't has privigle in system!");
                    return;
                }
                else if (countRole == 1)
                {
                    session.Username = user;
                    session.Role = role[0];
                    directWindowUser(conn, session);
                }
                else
                {
                    if (role.Where(hh => hh == "DBA").Count() == 1)
                    {
                        session.Username = user;
                        session.Role = role[0];
                        directWindowUser(conn, session);
                    }
                    else
                    {
                        string vaitro = "";
                        string getVaitro = "select sys_context('atbm_user_ctx', 'atbm_role') ROLE from dual";
                        try
                        {
                            using (OracleCommand cmd1 = new OracleCommand(getVaitro, conn))
                            {
                                using (OracleDataReader reader1 = cmd1.ExecuteReader())
                                {
                                    while (reader1.Read())
                                    {
                                        vaitro = reader1.GetString(reader1.GetOrdinal("ROLE"));
                                    }
                                }
                            }
                            session.Username = user;
                            session.Role = vaitro;
                            directWindowUser(conn, session);
                        }
                        catch
                        {
                            MessageBox.Show("Người dùng không có quyền hạn trong hệ thống. Vui lòng liên hệ quản trị viên để biết thêm.");
                        }
                        //var nextwindow = new MultiSession(conn, role, user);
                        //this.Close();
                        //nextwindow.Show();
                    }
                }
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Replace(" ", "");
            string password = txtPassword.Password;


            ConnectionDB connectionDB = new ConnectionDB();
            OracleConnection oracleConnection = connectionDB.OracleConnection(username, password);
            if(oracleConnection != null)
            {
                HandleSession(oracleConnection, username);
            }
            else
            {
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}
