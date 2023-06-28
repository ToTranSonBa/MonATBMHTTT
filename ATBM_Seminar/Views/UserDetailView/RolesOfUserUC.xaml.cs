using ATBM_Seminar.Models;
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
    /// Interaction logic for RolesOfUserUC.xaml
    /// </summary>
    public partial class RolesOfUserUC : UserControl
    {
        private readonly AdminMV _adminMV;
        private readonly List<RoleOfUserModel> _roleOfUserModels;
        private readonly string _username;

        public RolesOfUserUC(AdminMV adminMV, string username)
        {
            _adminMV = adminMV;
            _username = username;
            _roleOfUserModels = _adminMV.GetRolesOfUser(_username);
            InitializeComponent();
            RoleOfUserDataGrid.ItemsSource = _roleOfUserModels;
        }

        private void revokeRoleFromBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
