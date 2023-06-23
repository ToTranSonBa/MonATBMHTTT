using ATBM_Seminar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Configuration;
using ATBM_Seminar.ModelViews;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SqlServer.Server;
using System.Runtime.Remoting.Messaging;
using ServerATBM.Controllers;

namespace ATBM_Seminar.ModelViews
{
    public class AdminMV
    {
        public OracleConnection connection { get; set; }
        public string _role { get; set; }
        public string _user { get; set; }
        private object converter;
        private string[] Color = new string[] { "#1E88E5", "#FF8F00", "#FF5252", "#0CA678", "#6741D9", "#FF6D00", "#FF5252", "#1E88E5", "#0CA678" };
        public AdminMV(OracleConnection conn, string Role, string user)
        {
            connection = conn;
            _role = Role;
            _user = user;
        }
        public void startQuery()
        {
                //connection.Open();
                string SQLcontex = $"alter session set \"_ORACLE_SCRIPT\"=true";
                ObservableCollection<string> list = new ObservableCollection<string>();
                OracleCommand cmd = new OracleCommand(SQLcontex, connection);
                cmd.ExecuteNonQuery();
                //connection.Close();
        }
        public ObservableCollection<Users> GetUserDataGrid()
        {
            ObservableCollection<Users> members = new ObservableCollection<Users>();
            var converter = new BrushConverter();
            //connection.Open();
            string SQLcontext = "SELECT * FROM dba_users WHERE USERNAME LIKE 'NV%'";
            using (OracleCommand cmd = new OracleCommand(SQLcontext , connection))
            {
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        string empName = reader.GetString(reader.GetOrdinal("USERNAME"));
                        string dayCreated = reader.GetString(reader.GetOrdinal("CREATED"));
                        char firstCharName = empName[0];
                        members.Add(new Users { Number = i.ToString(), Character = firstCharName.ToString(), BgColor = (Brush)converter.ConvertFromString(Color[(i % 7)]), Name = empName, Position = dayCreated });
                        i++;
                    }
                    reader.Close();
                }
            }
            return members;
        }
        public ObservableCollection<Roles_Window> GetRolesDataGrid()
        {
            ObservableCollection<Roles_Window> roles = new ObservableCollection<Roles_Window>();
            var converter = new BrushConverter();
            //connection.Open();
            string SQLcontext = "select distinct granted_role from dba_role_privs where granted_role like 'ATBMHTTT_ROLE_%'";
            OracleCommand cmd = new OracleCommand(SQLcontext, connection);
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                int i = 1;
                while (reader.Read())
                {
                    string empName = reader.GetString(reader.GetOrdinal("GRANTED_ROLE"));
                    //string dayCreated = reader.GetString(reader.GetOrdinal("NUMBERROLE"));
                    char firstCharName = empName[0];
                    roles.Add(new Roles_Window { Number = i.ToString(), Character = firstCharName.ToString(), BgColor = (Brush)converter.ConvertFromString(Color[(i % 7)]), Name = empName });
                    i++;
                }
                reader.Close();
                //connection.Close();
            }
            foreach(var role in roles)
            {
                role.Name = role.Name.Replace("ATBMHTTT_ROLE_", "");
            }
            return roles;
        }
        public void CreateRole(string roleName)
        {
            string role = "ATBMHTTT_ROLE_" + roleName;
            string SQLcontext = $"CREATE ROLE {role}";
            OracleCommand cmd = new OracleCommand(SQLcontext, connection);
            cmd.ExecuteNonQuery();
        }
        public ObservableCollection<string> GetNewID()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            string SQLcontex = "select MANV from A_NHANVIEN where MANV NOT IN (SELECT USERNAME FROM ALL_USERS)";
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(reader.GetOrdinal("MANV")));
            }
            reader.Close();
            return list;
        }
        public void AddNewUser(string userID, string password)
        {
            string SQLCreateUser = $"CREATE USER {userID} IDENTIFIED BY {password}";
            OracleCommand cmdCreateUser = new OracleCommand(SQLCreateUser, connection);
            cmdCreateUser.ExecuteNonQuery();

            string SQLGrantSession = $"GRANT CREATE SESSION TO {userID}";
            OracleCommand cmdGrantSession = new OracleCommand(SQLCreateUser, connection);
            cmdGrantSession.ExecuteNonQuery();
        }

        public void DropUser(Users user)
        {
            string SQLcontex = $"Drop user {user.Name}";
            ObservableCollection<string> list = new ObservableCollection<string>();
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }
        public void DropRole(Roles_Window role)
        {
            string SQLcontex = $"Drop Role {role.Name}";
            ObservableCollection<string> list = new ObservableCollection<string>();
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }
        public void ChangePwdUser(string username, string pwd)
        {
            string SQLcontex = $"ALTER USER {username} IDENTIFIED BY {pwd}";
            ObservableCollection<string> list = new ObservableCollection<string>();
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }
        public void AddRoleForUser(string commandText)
        {
                //connect database with privilege sysdba
                OracleCommand cmd = new OracleCommand(commandText, connection);
                cmd.ExecuteNonQuery();
        }
        public List<string> Getrole()
        {
            List<string> roles = new List<string>();
            //connection.Open();
            // Define and execute SQL
            string commandText = $"select role from dba_roles where role like 'ATBMHTTT_ROLE%'";
            OracleCommand cmd = new OracleCommand(commandText, connection);
            using(OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string role = reader.GetString(0);
                    roles.Add(role);
                }
            }
            //connection.Close();
            return roles;
        }
        public List<string> GetUsers()
        {
            List<string> Users = new List<string>();
            // Define and execute SQL
            string commandText = $"select DISTINCT GRANTEE from dba_role_privs where grantee like 'NV%' ORDER BY GRANTEE";
            OracleCommand cmd = new OracleCommand(commandText, connection);
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string user = reader.GetString(0);
                    Users.Add(user);
                }
            }
            return Users;
        }
        public void DeleteRole(string SQLcontex) 
        {
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }
        public ObservableCollection<Table> getTable()
        {
            ObservableCollection<Table> tables = new ObservableCollection<Table>();
            string SQLcontext = "select * from dba_tables WHERE TABLE_NAME LIKE 'ATBMHTTT_TABLE_%'";
            OracleCommand cmd = new OracleCommand( SQLcontext, connection);
            using(OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Table table = new Table();
                    table.Name = reader.GetString(reader.GetOrdinal("TABLE_NAME"));
                    table.Owner = reader.GetString(reader.GetOrdinal("OWNER"));
                    //table.NUM_ROWS = reader.GetString(reader.GetOrdinal("NUM_ROWS"));
                    tables.Add(table);
                }
            }
            foreach(var table in tables)
            {
                table.Name = table.Name.Replace("ATBMHTTT_TABLE_", "");
            }
            return tables;
        }

        public List<Auditting_Class> GetAuditting_Classes(string policy_name) 
        {
            try
            {
                var list = new List<Auditting_Class>();
                string SQLcontext = $"SELECT * FROM DBA_FGA_AUDIT_TRAIL WHERE POLICY_NAME = '{policy_name}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Auditting_Class
                            {
                                SESSION_ID = reader.GetString(reader.GetOrdinal("SESSION_ID")),
                                CURRENT_USER = reader.GetString(reader.GetOrdinal("CURRENT_USER")),
                                OBJECT_NAME = reader.GetString(reader.GetOrdinal("OBJECT_NAME")),
                                SQL_TEXT = reader.GetString(reader.GetOrdinal("SQL_TEXT")),
                                TIMESTAMP = reader.GetString(reader.GetOrdinal("EXTENDED_TIMESTAMP"))
                            });
                        }
                    }
                }
                return list;

            }
            catch
            {
                return new List<Auditting_Class>();
            }
        }

        public List<Auditting_Class> GetAudittingOnSystemLog()
        {
            try
            {
                var list = new List<Auditting_Class>();
                string SQLcontext = $"SELECT * FROM SYS.AUD$ where obj$name = 'DBA_FGA_AUDIT_TRAIL' ORDER BY SESSIONID DESC";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Auditting_Class
                            {
                                SESSION_ID = reader.GetString(reader.GetOrdinal("SESSIONID")),
                                CURRENT_USER = reader.GetString(reader.GetOrdinal("CURRENT_USER")),
                                OBJECT_NAME = reader.GetString(reader.GetOrdinal("OBJ$NAME")),
                                //SQL_TEXT = reader.GetString(reader.GetOrdinal("SQLTEXT")),
                                TIMESTAMP = reader.GetString(reader.GetOrdinal("NTIMESTAMP#"))
                            });
                        }
                    }
                }
                return list;
            }
            catch
            {
                return new List<Auditting_Class>();
            }
        }
    }
}
