using Oracle.ManagedDataAccess.Client;
using ATBM_Seminar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATBM_Seminar.ModelViews
{
    internal class ConnectionDB
    {
        public OracleConnection OracleConnection(string username, string password)
        {
            try
            {
                string conString = $"User Id={username};Password={password};" +
                "Data Source=localhost:1521/xe;";
                if (username == "sys" || username == "SYS") { conString += "DBA PRIVILEGE=SYSDBA;"; }
                OracleConnection con = new OracleConnection(conString);
                con.Open();
                //con.Close();
                return con;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
