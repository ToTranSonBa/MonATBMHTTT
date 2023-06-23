using ATBM_Seminar.Models;
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
        public ObservableCollection<PhanCong> showPhanCong()
        {
            ObservableCollection<PhanCong> list_phancong=new ObservableCollection<PhanCong>();
            PhanCong phanCong = new PhanCong();
            list_phancong = phanCong.allPhanCong();


            return list_phancong;
        }
    }
}
