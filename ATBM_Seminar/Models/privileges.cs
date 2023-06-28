using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ATBM_Seminar.Models
{
    public class privileges
    {
        public string grantee { get; set; }
        public string owner { get; set; }
        public string column { get; set; }
        public string Table_name { get; set; }
        public string Grantor { get; set; }
        public string Privs { get; set; }
        public string GrantTable { get; set; }
        public string hierarchy { get; set; }
        public string common { get; set; }
        public string Type { get; set; }
        public string Inherite { get; set; }
        public Brush BgColor { get; set; }

        public string Number { get; set; }
    }
}
