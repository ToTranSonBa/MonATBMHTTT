using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ServerATBM.Controllers
{
    public class NHANVIEN
    {
        public string MANV { get; set; }

        public string TENNV { get; set; }

        public string PHAI { get; set; }
        public DateTime NGAYSINH { get; set; }

        public string DIACHI { get; set; }
        public string S0DT { get; set; }

        public decimal LUONG { get; set; }
        public decimal PHUCAP { get; set; }
        public string VAITRO { get; set; }
        public string MANQL { get; set; }
        public string PHG { get; set; }
    }
}
