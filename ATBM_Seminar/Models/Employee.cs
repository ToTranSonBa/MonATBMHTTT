using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ATBM_Seminar.Models
{
    public class Employee
    {
        public string MANV { get; set; }
        public string TENNV { get; set; }
        public string PHAI { get; set; }
        public string NGAYSINH { get; set; }
        public string DIACHI { get; set; }
        public string SODT { get; set; }
        public string LUONG { get; set; }
        public string PHUCAP { get; set; }
        public string VAITRO { get; set; }
        public string MANQL { get; set; }
        public string PHG { get; set; }
        public ObservableCollection<Employee> allEmployee(OracleConnection connection, string role_name)
        {
            ObservableCollection<Employee> list_employee =new ObservableCollection<Employee>();
            
            DataTable result = new DataTable();

            string query = "SELECT * FROM ATBM_20H3T_22.v_ShowAllEmployee";
            OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
            adapter.Fill(result);
 

            for(int i=0; i<result.Rows.Count;i++)
            {
                DataRow row = result.Rows[i];
                if (row["VAITRO"].ToString() == role_name) {
                    Employee employee = new Employee();
                    employee.MANV = row["MANV"].ToString();
                    employee.TENNV = row["TENNV"].ToString();
                    employee.PHAI = row["PHAI"].ToString();
                    employee.NGAYSINH = row["NGAYSINH"].ToString();
                    employee.DIACHI = row["DIACHI"].ToString();
                    employee.SODT = row["SODT"].ToString();
                    employee.LUONG = (row["LUONG"]).ToString();
                    employee.PHUCAP = (row["PHUCAP"]).ToString();
                    employee.VAITRO = row["VAITRO"].ToString();
                    employee.MANQL = row["MANQL"].ToString();
                    employee.PHG = row["PHG"].ToString();
                    list_employee.Add(employee);
                }
            }

            return list_employee;
        }
        public Employee Detail_Employee(OracleConnection connection, string MANV)
        {
            Employee employee = new Employee();
            

                OracleCommand cmd = new OracleCommand("ATBM_20H3T_22.sp_Get1Employee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter cursorParam = cmd.Parameters.Add("EMPLOYCURSOR", OracleDbType.RefCursor);
                cursorParam.Direction = ParameterDirection.Output;

                OracleParameter inputParam = new OracleParameter("MANV", OracleDbType.Char);
                inputParam.Value = MANV;
                cmd.Parameters.Add(inputParam);

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee.MANV=reader.GetString(0);
                        employee.TENNV = reader.GetString(1);
                        employee.PHAI = reader.GetString(2);
                        employee.NGAYSINH = reader.GetString(3);
                        employee.DIACHI = reader.GetString(4);
                        employee.SODT = reader.GetString(5);
                        //employee.LUONG = reader.GetString(6);
                        //employee.PHUCAP = reader.GetString(7);
                        employee.VAITRO = reader.GetString(8);
                        if (!reader.IsDBNull(9))
                        {
                            Employee employee1 = new Employee();
                            employee1 = employee1.Detail_Employee(connection,reader.GetString(9));
                            employee.MANQL = employee1.TENNV;
                        }
                        Room room=new Room();
                        room=room.detailRoom(connection, reader.GetString(10));
                        employee.PHG = room.TENPB;
                    }
                }

            
            return employee;
        }
    }
}
