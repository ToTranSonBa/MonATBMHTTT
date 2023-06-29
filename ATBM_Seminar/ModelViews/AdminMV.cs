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
using ATBM_Seminar.Views.RoleDetailView;

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
        #region user
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
        public bool executeSQL(string sql)
        {
            try
            {
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ObservableCollection<string> GetNewID()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            string SQLcontex = "select MANV from ATBMHTTT_TABLE_NHANVIEN where MANV NOT IN (SELECT USERNAME FROM ALL_USERS)";
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
            OracleCommand cmdGrantSession = new OracleCommand(SQLGrantSession, connection);
            cmdGrantSession.ExecuteNonQuery();
        }
        public void DropUser(Users user)
        {
            string SQLcontex = $"Drop user {user.Name}";
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }
        public void ChangePwdUser(string username, string pwd)
        {
            string SQLcontex = $"ALTER USER {username} IDENTIFIED BY {pwd}";
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }
        public bool AddRoleForUser(string commandText)
        {
            //connect database with privilege sysdba
            try
            {
                OracleCommand cmd = new OracleCommand(commandText, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
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

        public List<RoleOfUserModel> GetRolesOfUser(string manv)
        {
            try
            {
                var list = new List<RoleOfUserModel>();
                string SQLcontext = $"select * from dba_role_privs where grantee = '{manv}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new RoleOfUserModel
                            {
                                GRANTED_ROLE = reader.GetString(reader.GetOrdinal("GRANTED_ROLE")),
                                ADMIN_OPTION = reader.GetString(reader.GetOrdinal("ADMIN_OPTION"))
                            });
                        }
                    }
                }
                return list;
            }
            catch
            {
                return new List<RoleOfUserModel>();
            }
        }

        public List<privileges> GetPrivilegesOfUser (string manv, int privkind)
        {
            try
            {
                string SQLcontext = "";
                List<privileges> privs = new List<privileges>();
                if (privkind == 0)
                {
                    SQLcontext = $"SELECT grantee, owner, table_name, privilege, grantor FROM dba_tab_privs where grantee in (select granted_role from DBA_role_privs where grantee = '{manv}')";
                }
                else
                {
                    SQLcontext = $"Select grantee, owner, table_name, privilege, grantor from dba_tab_privs where grantee = '{manv}'";
                }
                using(OracleCommand cmd = new OracleCommand( SQLcontext, connection))
                {
                    using(OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string grantee_tmp = reader.GetString(reader.GetOrdinal("GRANTEE"));
                            string owner_tmp = reader.GetString(reader.GetOrdinal("OWNER"));
                            string Table_name_tmp = reader.GetString(reader.GetOrdinal("TABLE_NAME"));
                            string Grantor_tmp = reader.GetString(reader.GetOrdinal("GRANTOR"));
                            string Privs_tmp = reader.GetString(reader.GetOrdinal("PRIVILEGE"));
                            privs.Add(new privileges
                            {
                                grantee = grantee_tmp,
                                owner = owner_tmp,
                                Table_name = Table_name_tmp,
                                Grantor = Grantor_tmp,
                                Privs = Privs_tmp,
                                GrantTable = Grantor_tmp
                            });
                        }

                    }
                }
                return privs;
            }
            catch 
            {
                return new List<privileges>();
            }
        }
        public bool RevokePrivsFromUser(privileges priv, string username)
        {
            try
            {
                string SQLcontex = $"Revoke {priv.Privs} on {priv.Table_name} from {username}";
                OracleCommand cmd = new OracleCommand(SQLcontex, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<string> getUserNotAccount()
        {
            try
            {
                var list = new List<string>();
                string SQLcontext = $"select MANV from ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN WHERE MANV NOT IN (SELECT USERNAME FROM ALL_USERS)";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(reader.GetOrdinal("MANV")));
                        }
                    }
                }
                return list;
            }
            catch
            {
                return new List<string>();
            }
        }
        #endregion

        #region role
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
        public void DropRole(Roles_Window role)
        {
            string SQLcontex = $"Drop Role ATBMHTTT_ROLE_{role.Name}";
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }


        public List<string> Getrole()
        {
            List<string> roles = new List<string>();
            //connection.Open();
            // Define and execute SQL
            string commandText = $"select role from dba_roles where role like 'ATBMHTTT_ROLE%'";
            OracleCommand cmd = new OracleCommand(commandText, connection);
            using (OracleDataReader reader = cmd.ExecuteReader())
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

        public void DeleteRole(string SQLcontex)
        {
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }

        public List<Users> GetUsersOfRole(string role)
        {
            try
            {
                var converter = new BrushConverter();
                List<Users> user = new List<Users>();


                string SQLcontext = $"select * from dba_role_privs where granted_role = 'ATBMHTTT_ROLE_{role}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Add(new Users
                            {
                                Name = reader.GetString(reader.GetOrdinal("GRANTEE")),
                                admin_option = reader.GetString(reader.GetOrdinal("ADMIN_OPTION"))

                            });
                        }
                    }
                }
                return user;
            }
            catch
            {
                return new List<Users>();
            }
        }
        public List<privileges> GetPrivsOfRole(string role)
        {
            try
            {
                var converter = new BrushConverter();
                List<privileges> Privs = new List<privileges>();


                string SQLcontext = $"SELECT  * FROM DBA_tab_PRIVS  where grantee LIKE 'ATBMHTTT_ROLE_{role}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string grantee_tmp = reader.GetString(reader.GetOrdinal("GRANTEE"));
                            string owner_tmp = reader.GetString(reader.GetOrdinal("OWNER"));
                            string Table_name_tmp = reader.GetString(reader.GetOrdinal("TABLE_NAME"));
                            string Grantor_tmp = reader.GetString(reader.GetOrdinal("GRANTOR"));
                            string Privs_tmp = reader.GetString(reader.GetOrdinal("PRIVILEGE"));
                            Privs.Add(new privileges
                            {
                                grantee = grantee_tmp,
                                owner = owner_tmp,
                                Table_name = Table_name_tmp,
                                Grantor = Grantor_tmp,
                                Privs = Privs_tmp,
                                GrantTable = Grantor_tmp
                            });
                        }
                    }
                }
                return Privs;
            }
            catch
            {
                return new List<privileges>();
            }
        }
        public List<privileges> GetPrivsOnColumnOfRole(string role)
        {
            try
            {
                var converter = new BrushConverter();
                List<privileges> Privs = new List<privileges>();


                string SQLcontext = $"select * from DBA_COL_PRIVS where grantee LIKE 'ATBMHTTT_ROLE_{role}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string grantee_tmp = reader.GetString(reader.GetOrdinal("GRANTEE"));
                            string owner_tmp = reader.GetString(reader.GetOrdinal("OWNER"));
                            string Table_name_tmp = reader.GetString(reader.GetOrdinal("TABLE_NAME"));
                            string Grantor_tmp = reader.GetString(reader.GetOrdinal("GRANTOR"));
                            string Privs_tmp = reader.GetString(reader.GetOrdinal("PRIVILEGE"));
                            string col = reader.GetString(reader.GetOrdinal("COLUMN_NAME"));
                            Privs.Add(new privileges
                            {
                                grantee = grantee_tmp,
                                owner = owner_tmp,
                                Table_name = Table_name_tmp,
                                Grantor = Grantor_tmp,
                                Privs = Privs_tmp,
                                GrantTable = Grantor_tmp,
                                column = col
                            });
                        }
                    }
                }
                return Privs;
            }
            catch
            {
                return new List<privileges>();
            }
        }
        public bool RevokeUserFromRole(string role, string username)
        {
            try
            {
                string r = "ATBMHTTT_ROLE_" + role;
                string SQLcontex = $"Revoke {r} from {username}";
                OracleCommand cmd = new OracleCommand(SQLcontex, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RevokePrivFromRole(string role, privileges privileges)
        {
            try
            {
                string r = "ATBMHTTT_ROLE_" + role;
                string SQLcontex = $"Revoke {privileges.Privs} on {privileges.Table_name} from {r}";
                OracleCommand cmd = new OracleCommand(SQLcontex, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Users> GetUsersNotInRole(string role)
        {
            try
            {
                var converter = new BrushConverter();
                List<Users> user = new List<Users>();


                string SQLcontext = $"select * from ALL_USERS where USERNAME LIKE 'NV%' AND USERNAME NOT IN (SELECT GRANTEE FROM DBA_ROLE_PRIVS WHERE GRANTED_ROLE = '{role}')";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Add(new Users
                            {
                                Name = reader.GetString(reader.GetOrdinal("USERNAME"))
                            });
                        }
                    }
                }
                return user;
            }
            catch
            {
                return new List<Users>();
            }
        }
        #endregion

        #region table
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
        public List<string> getColOfTable(string tableName)
        {
            try
            {
                var  list = new List<string>();
                string SQLcontext = $"SELECT column_name FROM all_tab_columns WHERE table_name = 'ATBMHTTT_TABLE_{tableName}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(reader.GetOrdinal("COLUMN_NAME")));
                        }
                    }
                }
                return list;
            }
            catch
            {
                return new List<string>();
            }
        }
        #endregion
        #region auditing
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
        #endregion
        #region View
        public List<View> GetView()
        {
            try
            {
                var list = new List<View>();
                string SQLcontext = $"select * from all_views where owner = '{_user}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new View
                            {
                                Name = reader.GetString(reader.GetOrdinal("VIEW_NAME")),
                                Text = reader.GetString(reader.GetOrdinal("TEXT")),
                                Length = reader.GetString(reader.GetOrdinal("TEXT_LENGTH"))
                            });
                        }
                    }
                }
                return list;

            }
            catch
            {
                return new List<View>();
            }
        }
        #endregion
    }
}
