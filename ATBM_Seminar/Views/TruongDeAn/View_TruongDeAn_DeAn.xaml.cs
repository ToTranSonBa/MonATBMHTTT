﻿using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
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

namespace ATBM_Seminar.Views.TruongDeAn
{
    /// <summary>
    /// Interaction logic for View_TruongDeAn_DeAn.xaml
    /// </summary>
    public partial class View_TruongDeAn_DeAn : UserControl
    {
        public View_TruongDeAn_DeAn()
        {
            InitializeComponent();
            LoadDeAn();
        }
        public void LoadDeAn()
        {
            try
            {
                ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
                DeAnViewModel dean = new DeAnViewModel();
                list_dean = dean.showDeAn();

                if (list_dean == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    DeAnDataGrid.ItemsSource = list_dean;
                }

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        private void AddDeAn_Click(object sender, RoutedEventArgs e)
        {
            ADDDeAn adddean_window=new ADDDeAn();
            adddean_window.ShowDialog();
            LoadDeAn();
        }
        private void DeleteDeAn_Click(object sender, RoutedEventArgs e)
        {
            DeAn dean =new DeAn();    
            dean = ((FrameworkElement)sender).DataContext as DeAn;
            TrDeAnViewModel deanviewmodel=new TrDeAnViewModel();
            deanviewmodel.deleteDeAn(dean.MADA);
            LoadDeAn();
        }
        private void UpdateDeAn_Click(object sender, RoutedEventArgs e)
        {
            DeAn dean=new DeAn();
            dean = ((FrameworkElement)sender).DataContext as DeAn;

            UpdateDeAn updatedean_window = new UpdateDeAn(dean.MADA);
            updatedean_window.ShowDialog();
            LoadDeAn();
        }
    }
}
