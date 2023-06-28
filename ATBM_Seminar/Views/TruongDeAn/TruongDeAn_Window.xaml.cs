using ATBM_Seminar.ViewModels;
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

namespace ATBM_Seminar.Views.TruongDeAn
{
    /// <summary>
    /// Interaction logic for TruongDeAn_Window.xaml
    /// </summary>
    public partial class TruongDeAn_Window : Window
    {
        private readonly OracleConnection _oracleConnection;
        public TruongDeAn_Window(OracleConnection conn)
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
        private void phBtn_Click(object sender, RoutedEventArgs e)
        {
            giamdocUC.Content = new Rooms(_oracleConnection);
        }
        private void daBtn_Click(object sender, RoutedEventArgs e)
        {
            giamdocUC.Content = new View_TruongDeAn_DeAn(_oracleConnection);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _oracleConnection.Close();
            var loginwindow = new Login();
            this.Close();
            loginwindow.Show();
        }
    }
}
