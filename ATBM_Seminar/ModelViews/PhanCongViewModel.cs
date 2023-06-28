using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.ViewModels
{
    public class PhanCongViewModel:ViewModelBase
    {
        public ObservableCollection<PhanCong> showPhanCong(OracleConnection connection)
        {
            ObservableCollection<PhanCong> list_phancong=new ObservableCollection<PhanCong>();
            PhanCong phanCong = new PhanCong();
            list_phancong = phanCong.allPhanCong(connection);


            return list_phancong;
        }
        //
        public ObservableCollection<PhanCong> phanCong_BL(OracleConnection connection)
        {
            ObservableCollection<PhanCong> list_phancong = new ObservableCollection<PhanCong>();
            PhanCong phanCong = new PhanCong();
            list_phancong = phanCong.getPhanCong_DB(connection);
            return list_phancong;
        }
    }
}
