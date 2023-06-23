using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.Models
{
    public class Auditting_Class
    {
        public string SESSION_ID { get; set; }
        public string CURRENT_USER { get; set; }
        public string TIMESTAMP { get; set; }
        public string OBJECT_NAME { get; set; }
        public string SQL_TEXT { get; set;}
    }
}
