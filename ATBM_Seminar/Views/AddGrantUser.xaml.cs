using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Controls.Primitives;
using System.Runtime.Remoting.Messaging;
using ATBM_Seminar.Models;
using ATBM_Seminar.ModelViews;

namespace ATBM_Seminar.Views
{
    public partial class AddGrantUser : Window
    {
        private AdminMV _admin { get; set; }    
        private string usernametext { get; set; }
        public AddGrantUser(AdminMV admin, string username)
        {
            _admin = admin;
            usernametext = username;
            InitializeComponent();
            loadtable();
        }
        string priv;
        public void loadtable()
        {
            try
            {
                UserDataGrid.Items.Add(usernametext);
                ObservableCollection<Table> tables = _admin.getTable();
                foreach(Table table in tables)
                {
                    comboBoxTable.Items.Add(table.Name);
                }
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }

        public class UN1
        {
            public string Character { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePwdUser_Window window = new ChangePwdUser_Window(_admin, usernametext);
            window.Show();
            this.Close();
        }

       
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.IsChecked == true)
            {
                priv = "select";
            }
            
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.IsChecked == true)
            {
                priv = "insert";
            }
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.IsChecked == true)
            {
                priv = "delete";
            }

        }
         string table;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string isChecked_Sel = (Select.IsChecked.HasValue && Select.IsChecked.Value).ToString();
            string isChecked_Ins = (Insert.IsChecked.HasValue && Insert.IsChecked.Value).ToString();
            string isChecked_Upd = (Update.IsChecked.HasValue && Update.IsChecked.Value).ToString();
            string isChecked_Del = (Delete.IsChecked.HasValue && Delete.IsChecked.Value).ToString();
            string isChecked_Opt = (Option.IsChecked.HasValue && Option.IsChecked.Value).ToString();

            Stack<string> privsgrant = new Stack<string>();
            Stack<string> privsrevoke = new Stack<string>();
            string query_grant = "GRANT ";
            string query_revoke = "REVOKE ";

            for (int i = 0; i < 5; i++)
            {
                if (isChecked_Sel == "True")
                {
                    privsgrant.Push("SELECT");
                    privsrevoke.Push("");
                }
                else
                {
                    privsgrant.Push("");
                    privsrevoke.Push("SELECT");
                }
                //
                if (isChecked_Ins == "True")
                {
                    privsgrant.Push("INSERT");
                    privsrevoke.Push("");
                }
                else
                {
                    privsgrant.Push("");
                    privsrevoke.Push("INSERT");
                }
                //
                if (isChecked_Upd == "True")
                {
                    privsgrant.Push("UPDATE");
                    privsrevoke.Push("");
                }
                else
                {
                    privsgrant.Push("");
                    privsrevoke.Push("UPDATE");
                }
                //
                if (isChecked_Del == "True")
                {
                    privsgrant.Push("DELETE");
                    privsrevoke.Push("");
                }
                else
                {
                    privsgrant.Push("");
                    privsrevoke.Push("DELETE");
                }
                //
                if (isChecked_Opt == "True")
                {
                    privsgrant.Push("WITH GRANT OPTION");
                    privsrevoke.Push("");
                }
                else
                {
                    privsgrant.Push("");
                    privsrevoke.Push("WITH GRANT OPTION");
                }
            }
            //revoke
            if (privsrevoke.ElementAt(1) == "" && privsrevoke.ElementAt(2) == "" && privsrevoke.ElementAt(3) == "" && privsrevoke.ElementAt(4) == "")
            {
                query_revoke = "";
            }
            else
            {
                
                int count1 = 0;
                for (int i = 4; i > 0; i--)
                {
                    int count = 0;
                    for (int j = 0; j < privs.Count(); j++)
                    {
                        if (privsrevoke.ElementAt(i) == privs.ElementAt(j))
                        {
                            count++;
                            count1++;
                            break;
                        }
                    }
                    if (privsrevoke.ElementAt(i) != ""&& count!=0)
                    {
                        query_revoke += privsrevoke.ElementAt(i);
                        query_revoke += ",";
                    }
                }
                if (count1 == 0)
                {
                    query_revoke = "";
                }
                else
                {
                    query_revoke = query_revoke.Substring(0, query_revoke.Length - 1);

                    query_revoke += $" ON {table} FROM {usernametext} ";
                }

            }


            //GRANT
            if (privsgrant.ElementAt(1) == "" && privsgrant.ElementAt(2) == "" && privsgrant.ElementAt(3) == "" && privsgrant.ElementAt(4) == "")
            {
                query_grant = "";
            }
            else
            {
                for (int i = 4; i > 0; i--)
                {
                    if (privsgrant.ElementAt(i) != "")
                    {
                        query_grant += privsgrant.ElementAt(i);
                        query_grant += ",";

                    }
                }
                query_grant = query_grant.Substring(0, query_grant.Length - 1);


                query_grant += $" ON {table} TO {usernametext} ";
            }

            if (privsgrant.ElementAt(0) != "")
            {
                query_grant += privsgrant.ElementAt(0);
            }

            //excute
            try
            {

                if (query_grant != "")
                {
                    string sql = $"{query_grant}";
                    OracleCommand command = new OracleCommand(sql, _admin.connection);
                    OracleDataReader reader = command.ExecuteReader();

                    reader.Close();
                }

                if (query_revoke != "")
                {
                    string sql2 = $"{query_revoke}";


                    OracleCommand command2 = new OracleCommand(sql2, _admin.connection);
                    OracleDataReader reader2 = command2.ExecuteReader();
                    reader2.Close();
                }
                MessageBox.Show("Add privilege is success!");
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }

        }
        Stack<string> privs = new Stack<string>();
        private void comboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                table = "ATBMHTTT_TABLE_" + comboBoxTable.SelectedItem.ToString();
                
                string sql = $" SELECT * FROM USER_TAB_PRIVS WHERE TABLE_NAME = '{table}' AND GRANTEE = '{usernametext}' ";

                using (OracleCommand command = new OracleCommand(sql, _admin.connection))
                {

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                       
                        while (reader.Read())
                        {
                            string privilege = reader.GetString(reader.GetOrdinal("PRIVILEGE"));
                            privs.Push(privilege);
                            
                        }
                        for(int i=0;i<privs.Count;i++)
                        {
                            
                            if (privs.ElementAt(i) == "SELECT")
                            {
                                Select.IsChecked= true;
                            }
                            else if (privs.ElementAt(i) == "INSERT")
                            {
                                Insert.IsChecked = true;
                            }
                            else if (privs.ElementAt(i) == "UPDATE")
                            {
                                Update.IsChecked = true;
                            }
                            else if (privs.ElementAt(i) == "DELETE")
                            {
                                Delete.IsChecked = true;
                            }
                            else if (privs.ElementAt(i) == "WITH GRANT OPTION")
                            {
                                Option.IsChecked = true;
                            }

                        }
                        reader.Close();
                    }
                }  
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }
        }
    }
}
