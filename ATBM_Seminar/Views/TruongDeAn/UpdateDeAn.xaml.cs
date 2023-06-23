using ATBM_Seminar.Models;
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
using System.Windows.Shapes;

namespace ATBM_Seminar.Views.TruongDeAn
{
    /// <summary>
    /// Interaction logic for UpdateDeAn.xaml
    /// </summary>
    public partial class UpdateDeAn : Window
    {
        private readonly OracleConnection _connection;
        public string global_MADA { get; set; }
        public UpdateDeAn(OracleConnection conn,string MADA)
        {
            _connection = conn;
            global_MADA = MADA;
            InitializeComponent();
            LoadDeAnDetail(MADA);
        }
        public void LoadDeAnDetail(string MADA)
        {
            try
            {
                DeAn dean = new DeAn();

                TrDeAnViewModel trdean = new TrDeAnViewModel();
                dean = trdean.showDetail_DeAn(_connection, MADA);

                RoomViewModel room = new RoomViewModel();
                ObservableCollection<Room> rooms = new ObservableCollection<Room>();
                rooms = room.showRoom(_connection);
                Phong.ItemsSource = rooms;
                Phong.DisplayMemberPath = "TENPB";

                this.DataContext = dean;

            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
            
        }
       
        public void UpdateDeAn_Click(object sender, RoutedEventArgs e)
        {
            string TENDA = Ten.Text;

            DateTime? selectedDate = Day.SelectedDate;

            string TENPB = Phong.SelectedValue?.ToString();

            Room room = new Room();
            TrDeAnViewModel trdean = new TrDeAnViewModel();
            room = trdean.getDetailRoomByName(_connection, TENPB);


            if (selectedDate.HasValue && TENDA != null && TENPB != null)
            {
                string NGAYBD = selectedDate.Value.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                TrDeAnViewModel deanviewmodel = new TrDeAnViewModel();
                deanviewmodel.updateDeAn(_connection, global_MADA, TENDA, NGAYBD, room.MAPB);

                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng diền đầy đủ thông tin");
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
