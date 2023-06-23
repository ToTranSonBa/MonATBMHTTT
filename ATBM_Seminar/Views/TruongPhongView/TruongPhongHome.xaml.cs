using ATBM_Seminar.ModelViews;
using MaterialDesignThemes.Wpf;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace ATBM_Seminar.Views.TruongPhongView
{
    /// <summary>
    /// Interaction logic for TruongPhongHome.xaml
    /// </summary>
    public partial class TruongPhongHome : Window
    {
        private OracleConnection _connection;
        private string _role;
        private string _user;
        public bool IsMaximized = false;
        private readonly DepartmentMV _departmetMV;
        public TruongPhongHome(OracleConnection conn, string Role, string user)
        {
            _connection = conn;
            _role = Role;
            _user = user;
            _departmetMV = new DepartmentMV(conn, _role, user);
            InitializeComponent();
            Title.Text = _departmetMV.getPersonelInfomation(user).TENNV;
            empBtn_Click(null, null);
            
        }
        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            _departmetMV.connection.Close();
            var loginWindow = new Login();
            loginWindow.ShowDialog();
            this.Close();
        }
        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
        private void empBtn_Click(object sender, RoutedEventArgs e)
        {
            empBtn.Style = this.FindResource("isSelectButton") as Style;
            addAgmBtn.Style = this.FindResource("menuButton") as Style;
            agmBtn.Style = this.FindResource("menuButton") as Style;
            PersonalBtn.Style = this.FindResource("menuButton") as Style;
            DepartmentBtn.Style = this.FindResource("menuButton") as Style;
            ProjectBtn.Style = this.FindResource("menuButton") as Style;
            managerController.Content = new  EmployeeUC(_departmetMV);
        }

        private void agmBtn_Click(object sender, RoutedEventArgs e)
        {
            empBtn.Style = this.FindResource("menuButton") as Style;
            addAgmBtn.Style = this.FindResource("menuButton") as Style;
            agmBtn.Style = this.FindResource("isSelectButton") as Style;
            PersonalBtn.Style = this.FindResource("menuButton") as Style;
            DepartmentBtn.Style = this.FindResource("menuButton") as Style;
            ProjectBtn.Style = this.FindResource("menuButton") as Style;
            managerController.Content = new AssignmentUC(_departmetMV);
        }

        private void addAgmBtn_Click(object sender, RoutedEventArgs e)
        {
            empBtn.Style = this.FindResource("menuButton") as Style;
            addAgmBtn.Style = this.FindResource("isSelectButton") as Style;
            agmBtn.Style = this.FindResource("menuButton") as Style;
            PersonalBtn.Style = this.FindResource("menuButton") as Style;
            DepartmentBtn.Style = this.FindResource("menuButton") as Style;
            ProjectBtn.Style = this.FindResource("menuButton") as Style;
            managerController.Content = new AddAssignmentUC(_departmetMV);
        }

        private void Personal_Click(object sender, RoutedEventArgs e)
        {
            empBtn.Style = this.FindResource("menuButton") as Style;
            addAgmBtn.Style = this.FindResource("menuButton") as Style;
            agmBtn.Style = this.FindResource("menuButton") as Style;
            PersonalBtn.Style = this.FindResource("menuButton") as Style;
            DepartmentBtn.Style = this.FindResource("menuButton") as Style;
            ProjectBtn.Style = this.FindResource("menuButton") as Style;

            var emp = _departmetMV.getPersonelInfomation(_user);
            var personalLeader = new PersonalUC(emp, _departmetMV);
            personalLeader.ShowDialog();
        }

        private void Project_Click(object sender, RoutedEventArgs e)
        {
            empBtn.Style = this.FindResource("menuButton") as Style;
            addAgmBtn.Style = this.FindResource("menuButton") as Style;
            agmBtn.Style = this.FindResource("menuButton") as Style;
            PersonalBtn.Style = this.FindResource("menuButton") as Style;
            DepartmentBtn.Style = this.FindResource("menuButton") as Style;
            ProjectBtn.Style = this.FindResource("isSelectButton") as Style;
            managerController.Content = new ProjectUC(_departmetMV);
        }

        private void Department_Click(object sender, RoutedEventArgs e)
        {
            empBtn.Style = this.FindResource("menuButton") as Style;
            addAgmBtn.Style = this.FindResource("menuButton") as Style;
            agmBtn.Style = this.FindResource("menuButton") as Style;
            PersonalBtn.Style = this.FindResource("menuButton") as Style;
            DepartmentBtn.Style = this.FindResource("isSelectButton") as Style;
            ProjectBtn.Style = this.FindResource("menuButton") as Style;
            managerController.Content = new DepartmentUC(_departmetMV);
        }
    }
}
