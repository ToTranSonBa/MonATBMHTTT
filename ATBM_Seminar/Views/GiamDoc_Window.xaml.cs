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
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using ATBM_Seminar.Models;
using System.Configuration;
using System.Data;
using ATBM_Seminar.ViewModels;
using MaterialDesignThemes.Wpf;

namespace ATBM_Seminar.Views
{
    /// <summary>
    /// Interaction logic for Admin_Window.xaml
    /// </summary>
    public partial class GiamDoc_Window : Window
    {
        
        private readonly OracleConnection _oracleConnection;
        public GiamDoc_Window(OracleConnection conn)
        {
            _oracleConnection = conn;
            InitializeComponent();
            DataContext = new MainViewModel();
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

        private void nvBtn_Click(object sender, RoutedEventArgs e)
        {
            giamdocUC.Content = new Employees(_oracleConnection);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _oracleConnection.Close();
            var login = new Login();
            this.Close ();
            login.Show();
        }
    }
}
