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

    public class PhanCongViewModel : ViewModelBase
    {

        public ObservableCollection<PhanCong> show_allPC(string MaNV, OracleConnection conn)
        {
            PhanCong pc = new PhanCong();
            ObservableCollection<PhanCong> list_pc = new ObservableCollection<PhanCong>();
            list_pc= pc.allPC(MaNV,conn);
            return list_pc;
        }
    }
}
