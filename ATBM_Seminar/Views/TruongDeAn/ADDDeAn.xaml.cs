using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for ADDDeAn.xaml
    /// </summary>
    public partial class ADDDeAn : Window
    {
        public ADDDeAn()
        {
            InitializeComponent();
            LoadRoom();
        }
        public void AddDeAn_Click(object sender, RoutedEventArgs e)
        {
            string TENDA = Ten.Text;

            DateTime? selectedDate = Day.SelectedDate;

            string TENPB= Phong.SelectedValue?.ToString();

            Room room = new Room();
            TrDeAnViewModel trdean = new TrDeAnViewModel();
            room = trdean.getDetailRoomByName(TENPB);

            DeAn deAn=new DeAn();
            ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
            list_dean = deAn.allDeAn();

            string MADA;

            deAn = list_dean.Last();
            string input = deAn.MADA;
            string s1 = input.Substring(0, 2);
            string s2 = input.Substring(2);
            int number = int.Parse(s2);
            number += 1;
            if (number < 100)
            {
                MADA = s1+"0" + number.ToString();
            }
            else
            {
                MADA = s1+number.ToString();
            }
            if (selectedDate.HasValue && TENDA != null && TENPB != null)
            {
                string NGAYBD = selectedDate.Value.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                TrDeAnViewModel deanviewmodel = new TrDeAnViewModel();
                deanviewmodel.addDeAn(MADA, TENDA, NGAYBD, room.MAPB);

                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng diền đầy đủ thông tin");
            }
        }
        public void LoadRoom()
        {
            try
            {
                RoomViewModel room = new RoomViewModel();
                ObservableCollection<Room> rooms = new ObservableCollection<Room>();
                rooms = room.showRoom();
                Phong.ItemsSource = rooms;
                Phong.DisplayMemberPath = "TENPB";

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
