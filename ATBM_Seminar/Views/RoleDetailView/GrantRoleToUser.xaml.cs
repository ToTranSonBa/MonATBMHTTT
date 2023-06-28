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

namespace ATBM_Seminar.Views.RoleDetailView
{
    /// <summary>
    /// Interaction logic for GrantRoleToUser.xaml
    /// </summary>
    public partial class GrantRoleToUser : UserControl
    {
        private readonly AdminMV _adminMV;
        private readonly string _roleName;
        public GrantRoleToUser(AdminMV adminMV, string role)
        {
            _adminMV = adminMV;
            _roleName = role;
            InitializeComponent();
            var list = _adminMV.GetUsersNotInRole(_roleName);
            List<string> list2 = list.Select(x => x.Name).ToList();
            txtUserName.ItemsSource = list2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUserName.Text;
            string sql = $"grant ATBMHTTT_ROLE_{_roleName} TO {user}";
            var check = _adminMV.AddRoleForUser(sql);
            if(check)
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }
    }
}
