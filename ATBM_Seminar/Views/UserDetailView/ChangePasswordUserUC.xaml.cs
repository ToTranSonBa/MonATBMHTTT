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

namespace ATBM_Seminar.Views.UserDetailView
{
    /// <summary>
    /// Interaction logic for ChangePasswordUserUC.xaml
    /// </summary>
    public partial class ChangePasswordUserUC : UserControl
    {
        private readonly AdminMV _admin;
        private readonly string _userName;
        public ChangePasswordUserUC(AdminMV adminMV, string username)
        {
            _admin = adminMV;
            _userName = username;
            InitializeComponent();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Change password user");
            }
        }
    }
}
