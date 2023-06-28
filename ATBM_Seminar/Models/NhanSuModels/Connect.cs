using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.Models
{
    public class Connect
    {
        public OracleConnection connectDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}