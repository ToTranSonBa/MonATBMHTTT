using System;
using System.ComponentModel.DataAnnotations;

namespace ServerATBM.Controllers
{
    public class PhanCong_Class
    {
        public string MANV { get; set; }
        public string TENNV { get; set; }
        public string MADA { get; set; }
        public string TENDA { get; set; }
        public DateTime THOIGIAN { get; set; }
    }
}
