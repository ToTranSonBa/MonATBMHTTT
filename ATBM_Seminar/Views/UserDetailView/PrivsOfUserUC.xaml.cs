using ATBM_Seminar.Models;
using ATBM_Seminar.ModelViews;
using Oracle.ManagedDataAccess.Client;
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

namespace ATBM_Seminar.Views.UserDetailView
{
    /// <summary>
    /// Interaction logic for PrivsOfUserUC.xaml
    /// </summary>
    public partial class PrivsOfUserUC : UserControl
    {
        private readonly AdminMV _adminMV;
        private readonly string _userName;
        public PrivsOfUserUC(AdminMV adminMV, string username)
        {
            _adminMV = adminMV;
            _userName = username;
            InitializeComponent();
            List<string> privKind = new List<string>();
            privKind.Add("Cấp trực tiếp");
            privKind.Add("Cấp qua role");
            ChoosePrivKind.ItemsSource = privKind;
        }

        private void ChoosePrivKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ChoosePrivKind.SelectedValue as string;
            if(text == "Cấp qua role")
            {
                roleUser.Visibility = Visibility.Visible;
                Option.Visibility = Visibility.Hidden;
                membersDataGrid.ItemsSource = _adminMV.GetPrivilegesOfUser(_userName, 0);
            }
            else
            {
                Option.Visibility = Visibility.Visible;
                roleUser.Visibility = Visibility.Hidden;
                membersDataGrid.ItemsSource = _adminMV.GetPrivilegesOfUser(_userName, 1);
            }
        }

        private void buttonDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            var priv = membersDataGrid.SelectedItem as privileges;
            _adminMV.RevokePrivsFromUser(priv, _userName);
            ChoosePrivKind.ItemsSource = _adminMV.GetPrivilegesOfUser(_userName, 1);
        }
    }
}









































