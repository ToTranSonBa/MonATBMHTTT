using Microsoft.SqlServer.Server;
using Oracle.ManagedDataAccess.Client;
using ServerATBM.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATBM_Seminar.ModelViews
{
    public class DepartmentMV
    {
        public readonly OracleConnection connection;
        public readonly string _role;
        public readonly string _user;
        public DepartmentMV(OracleConnection conn, string Role, string user)
        {
            connection = conn;
            _user = user;
            _role = Role;
        }

        public void startQuery()
        {
            string SQLcontex = $"alter session set \"_ORACLE_SCRIPT\"=true";
            OracleCommand cmd = new OracleCommand(SQLcontex, connection);
            cmd.ExecuteNonQuery();
        }
        public List<DeAn_Class> getDean() { 
            try
            {
                var list = new List<DeAn_Class>();
                string SQLcontext = "SELECT * FROM atbm_20h3t_22.atbmhttt_table_dean";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DeAn_Class
                            {
                               MADA = reader.GetString(reader.GetOrdinal("MADA")),
                               NGAYBD = reader.GetDateTime(reader.GetOrdinal("NGAYBD")),
                               TENDA = reader.GetString(reader.GetOrdinal("TENDA")),
                               PHONG = reader.GetString(reader.GetOrdinal("PHONG")),
                            });
                        }
                    }
                }
                return list;
            }
            catch
            {
                return new List<DeAn_Class>();
            } 
            
        }
        public List<NHANVIEN> GetEmployee()
        {
            try
            {
                List<NHANVIEN> list = new List<NHANVIEN>();
                string SQLcontext = "SELECT * FROM atbm_20h3t_22.atbmhttt_table_nhanvien";
                using (OracleCommand cmd = new OracleCommand( SQLcontext, connection))
                {
                    using(OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NHANVIEN
                            {
                                MANV = reader.GetString(reader.GetOrdinal("MANV")),
                                TENNV = reader.GetString(reader.GetOrdinal("TENNV")),
                                PHAI = reader.GetString(reader.GetOrdinal("PHAI")),
                                NGAYSINH = reader.GetDateTime(reader.GetOrdinal("NGAYSINH")),
                                DIACHI = reader.GetString(reader.GetOrdinal("DIACHI")),
                                S0DT = reader.GetString(reader.GetOrdinal("SODT")),
                                VAITRO = reader.GetString(reader.GetOrdinal("VAITRO")),
                                PHG = reader.GetString(reader.GetOrdinal("PHG"))
                            });
                        }
                    }
                }
                return list;
            } 
            catch (Exception ex)
            {
                return new List<NHANVIEN>();
            }
        }
        public List<PhanCong_Class> getAssignment()
        {
            try
            {
                var list = new List<PhanCong_Class>();
                string SQLcontext = "SELECT * FROM atbm_20h3t_22.atbmhttt_view_truongphong_phancong";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhanCong_Class
                            { 
                                TENDA = reader.GetString(reader.GetOrdinal("TENDA")),
                                TENNV = reader.GetString(reader.GetOrdinal("TENNV")),
                                MADA = reader.GetString(reader.GetOrdinal("MADA")),
                                MANV = reader.GetString(reader.GetOrdinal("MANV")),
                                THOIGIAN = reader.GetDateTime(reader.GetOrdinal("THOIGIAN"))
                            });
                        }
                    }
                }
                return list;

            } catch
            {
                return new List<PhanCong_Class>();
            }
        }
        public bool AddAssignment(string MANV, string MADA, string date)
        {
            try
            {
                string SQLcontext = $"INSERT INTO atbm_20h3t_22.ATBMHTTT_TABLE_PHANCONG(MANV, MADA, THOIGIAN)" +
                    $" VALUES ('{MANV}', '{MADA}', TO_DATE('{date}','MM/DD/YYYY'))";
                OracleCommand cmd = new OracleCommand(SQLcontext, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            } 
            
        }

        public NHANVIEN getPersonelInfomation(string user)
        {
            try
            {
                List<NHANVIEN> list = new List<NHANVIEN>();
                string SQLcontext = $"SELECT * FROM atbm_20h3t_22.atbmhttt_table_nhanvien WHERE MANV = '{user}'";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NHANVIEN
                            {
                                MANV = reader.GetString(reader.GetOrdinal("MANV")),
                                TENNV = reader.GetString(reader.GetOrdinal("TENNV")),
                                PHAI = reader.GetString(reader.GetOrdinal("PHAI")),
                                NGAYSINH = reader.GetDateTime(reader.GetOrdinal("NGAYSINH")),
                                DIACHI = reader.GetString(reader.GetOrdinal("DIACHI")),
                                S0DT = reader.GetString(reader.GetOrdinal("SODT")),
                                VAITRO = reader.GetString(reader.GetOrdinal("VAITRO")),
                                PHG = reader.GetString(reader.GetOrdinal("PHG")),
                                LUONG = reader.GetDecimal(reader.GetOrdinal("LUONG")),
                                PHUCAP = reader.GetDecimal(reader.GetOrdinal("PHUCAP"))
                            });
                        }
                    }
                }
                var result = list.SingleOrDefault(nv => nv.MANV == user);
                return result;
            }
            catch (Exception ex)
            {
                return new NHANVIEN();
            }

        }

        public bool UpdatePersonalInfomation(string USER, string SDT, string Birthday, string Addr)
        {
            try
            {
                string SQLcontext = $"Update ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN " +
                                     $"SET NGAYSINH = TO_DATE('{Birthday}','MM/DD/YYYY'), SODT = '{SDT}', DIACHI = '{Addr}'";
                OracleCommand cmd = new OracleCommand(SQLcontext, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PhongBan_Class> GetPhongBanList()
        {
            try
            {
                var list = new List<PhongBan_Class>();
                string SQLcontext = $"select * from atbm_20h3t_22.atbmhttt_table_phongban";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhongBan_Class()
                            {
                                MAPB = reader.GetString(reader.GetOrdinal("MAPB")),
                                TENPB = reader.GetString(reader.GetOrdinal("TENPB")),
                                TRPHG = reader.GetString(reader.GetOrdinal("TRPHG"))
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<PhongBan_Class>();
            }
        }

        public List<DeAn_Class> GetDeAn_Classes()
        {
            try
            {
                var list = new List<DeAn_Class>();
                string SQLcontext = $"select * from atbm_20h3t_22.atbmhttt_table_dean";
                using (OracleCommand cmd = new OracleCommand(SQLcontext, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DeAn_Class()
                            {
                                MADA = reader.GetString(reader.GetOrdinal("MADA")),
                                TENDA = reader.GetString(reader.GetOrdinal("TENDA")),
                                NGAYBD = reader.GetDateTime(reader.GetOrdinal("NGAYBD")),
                                PHONG = reader.GetString(reader.GetOrdinal("PHONG"))
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<DeAn_Class>();
            }
        }

        public bool deleteAssignment(string manv, string mada)
        {
            try
            {
                string SQLcontext = $"delete atbm_20h3t_22.atbmhttt_table_phancong where manv = '{manv}' AND MADA = '{mada}'";
                OracleCommand cmd = new OracleCommand(SQLcontext, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool updateAssignment(string manv, string mada, string time)
        {
            try
            {
                string SQLcontext = $"update atbm_20h3t_22.atbmhttt_table_phancong set THOIGIAN = TO_DATE('{time}','MM/DD/YYYY') where manv = '{manv}' AND MADA = '{mada}'";
                OracleCommand cmd = new OracleCommand(SQLcontext, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
