using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.Models
{
    public class Table
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string NUM_ROWS { get; set; }
    }

    public class View
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Tablespace { get; set; }
    }
}
