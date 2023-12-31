﻿using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATBM_Seminar.ViewModels
{
    public class DeAnViewModel:ViewModelBase
    {
        public ObservableCollection<DeAn> showDeAn(OracleConnection connection)
        {
            DeAn dean = new DeAn();
            ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
            list_dean = dean.allDeAn(connection);

            return list_dean;
        }

        //
        public ObservableCollection<DeAn> deAn_BL(OracleConnection connection)
        {
            DeAn dean = new DeAn();
            ObservableCollection<DeAn> list_dean = new ObservableCollection<DeAn>();
            list_dean = dean.getDeAn_DB(connection);

            return list_dean;
        }
    }
}
