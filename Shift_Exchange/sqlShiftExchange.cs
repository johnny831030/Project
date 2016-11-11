using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace longtermcare.NursingPlan.Shift_Exchange
{
    public class sqlShiftExchange
    {
        private string shadowwarning; 
        public static string connect(string connection_id)
        {
            string datasource;
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            return datasource;
        }
        public static DataTable getDataTable(string strSQL, string connection_id) 
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            cmd.Dispose();
            adp.Dispose();
            con.Close();
            return dt;
        }

        public static DataTable getShiftExchange(string s_date, string e_date, string ip_no, string connection_id)
        {
            //string strSQL = string.Format("SELECT SHIFT_EXCHANGE_RECORD.*, IP_NO, R_DATE, R_TIME, NURSE_RECORD_SIMPLE.CREATE_USER AS NRS_CREATE_USER FROM SHIFT_EXCHANGE_RECORD LEFT JOIN NURSE_RECORD_SIMPLE ON SHIFT_EXCHANGE_RECORD.NRS_NO = NURSE_RECORD_SIMPLE.NO WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND NURSE_RECORD_SIMPLE.AccountEnable ='Y' AND IP_NO = '{0}' AND R_DATE BETWEEN '{1}' AND '{2}' ORDER BY R_DATE DESC, R_TIME DESC", ip_no, s_date, e_date);          

            string strSQL = "SELECT SHIFT_EXCHANGE_RECORD.NRS_NO, SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE, SHIFT_EXCHANGE_RECORD.A_TIME, SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO WHERE SHIFT_EXCHANGE_RECORD.IP_NO = '" + ip_no + "' AND SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND IP_INFORMATION.AccountEnable = 'Y' AND ROOM.AccountEnable = 'Y' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO";
            DataTable dt_ShiftExchange = getDataTable(strSQL, connection_id);
            return dt_ShiftExchange;
        }
        public static DataTable getShiftExchange_All_IP_All_Area(string s_date, string e_date, string connection_id)
        {
            //string strSQL = string.Format("SELECT SHIFT_EXCHANGE_RECORD.*, IP_NO, R_DATE, R_TIME, NURSE_RECORD_SIMPLE.CREATE_USER AS NRS_CREATE_USER FROM SHIFT_EXCHANGE_RECORD LEFT JOIN NURSE_RECORD_SIMPLE ON SHIFT_EXCHANGE_RECORD.NRS_NO = NURSE_RECORD_SIMPLE.NO WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND NURSE_RECORD_SIMPLE.AccountEnable ='Y' AND IP_NO = '{0}' AND R_DATE BETWEEN '{1}' AND '{2}' ORDER BY R_DATE DESC, R_TIME DESC", ip_no, s_date, e_date);          

            string strSQL = "SELECT SHIFT_EXCHANGE_RECORD.NRS_NO, SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE, SHIFT_EXCHANGE_RECORD.A_TIME, SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND IP_INFORMATION.AccountEnable = 'Y' AND ROOM.AccountEnable = 'Y' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO";
            DataTable dt_ShiftExchange = getDataTable(strSQL, connection_id);
            return dt_ShiftExchange;
        }
        public static DataTable getShiftExchange_All_IP_Some_Area(string s_date, string e_date, string area, string connection_id)
        {
            //string strSQL = string.Format("SELECT SHIFT_EXCHANGE_RECORD.*, IP_NO, R_DATE, R_TIME, NURSE_RECORD_SIMPLE.CREATE_USER AS NRS_CREATE_USER FROM SHIFT_EXCHANGE_RECORD LEFT JOIN NURSE_RECORD_SIMPLE ON SHIFT_EXCHANGE_RECORD.NRS_NO = NURSE_RECORD_SIMPLE.NO WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND NURSE_RECORD_SIMPLE.AccountEnable ='Y' AND IP_NO = '{0}' AND R_DATE BETWEEN '{1}' AND '{2}' ORDER BY R_DATE DESC, R_TIME DESC", ip_no, s_date, e_date);          

            string strSQL = "SELECT SHIFT_EXCHANGE_RECORD.NRS_NO, SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE, SHIFT_EXCHANGE_RECORD.A_TIME, SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO WHERE ROOM_AREA = '" + area + "' AND SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND IP_INFORMATION.AccountEnable = 'Y' AND ROOM.AccountEnable = 'Y' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO";
            DataTable dt_ShiftExchange = getDataTable(strSQL, connection_id);
            return dt_ShiftExchange;
        }

        public static DataTable getShiftExchange_NRS(string s_date, string e_date, string ip_no, string connection_id)
        {
            //string strSQL = string.Format("SELECT SHIFT_EXCHANGE_RECORD.*, IP_NO, R_DATE, R_TIME, NURSE_RECORD_SIMPLE.CREATE_USER AS NRS_CREATE_USER FROM SHIFT_EXCHANGE_RECORD LEFT JOIN NURSE_RECORD_SIMPLE ON SHIFT_EXCHANGE_RECORD.NRS_NO = NURSE_RECORD_SIMPLE.NO WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND NURSE_RECORD_SIMPLE.AccountEnable ='Y' AND IP_NO = '{0}' AND R_DATE BETWEEN '{1}' AND '{2}' ORDER BY R_DATE DESC, R_TIME DESC", ip_no, s_date, e_date);          

            string strSQL = "SELECT SHIFT_EXCHANGE_RECORD.NRS_NO, SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE, SHIFT_EXCHANGE_RECORD.A_TIME, SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO WHERE SHIFT_EXCHANGE_RECORD.IP_NO = '" + ip_no + "' AND SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO <> '0' AND IP_INFORMATION.AccountEnable = 'Y' AND ROOM.AccountEnable = 'Y' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO";
            DataTable dt_ShiftExchange = getDataTable(strSQL, connection_id);
            return dt_ShiftExchange;
        }

        internal static DataTable getShiftExchange_nonNRS(string s_date, string e_date, string ip_no, string connection_id)//沒在用
        {
            //string strSQL = string.Format("SELECT NRS_CONTENT, CREATE_DATE AS R_DATE, CREATE_TIME AS R_TIME, CREATE_USER AS NRS_CREATE_USER FROM SHIFT_EXCHANGE_RECORD WHERE NRS_NO = '0' AND (CREATE_DATE BETWEEN '{0}' AND '{1}') AND AccountEnable = 'Y'", s_date, e_date);
            string strSQL = "SELECT NRS_CONTENT, A_DATE, A_TIME, IP_NAME, ROOM_AREA, ROOM_BED, SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.CREATE_USER FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO WHERE NRS_NO = '0' AND (A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "') AND SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND SHIFT_EXCHANGE_RECORD.IP_NO = '" + ip_no + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO";
            
            DataTable dt_ShiftExchange = getDataTable(strSQL, connection_id);
            return dt_ShiftExchange; 
        }

        //FNursingRemind 有用到,請勿刪除
        public static DataTable getShiftExchangeWithIPInfo(string s_date, string e_date, string roo_area, string connection_id)
        {
            string strSQL = string.Format("SELECT SHIFT_EXCHANGE_RECORD.*, NURSE_RECORD_SIMPLE.IP_NO, R_DATE, R_TIME, NURSE_RECORD_SIMPLE.CREATE_USER AS NRS_CREATE_USER, ROOM.ROOM_AREA, ROOM_BED, IP_NAME FROM SHIFT_EXCHANGE_RECORD LEFT JOIN NURSE_RECORD_SIMPLE ON SHIFT_EXCHANGE_RECORD.NRS_NO = NURSE_RECORD_SIMPLE.NO LEFT JOIN ROOM ON NURSE_RECORD_SIMPLE.IP_NO = ROOM.IP_NO, IP_INFORMATION WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND NURSE_RECORD_SIMPLE.AccountEnable ='Y' AND R_DATE BETWEEN '{0}' AND '{1}' AND IP_INFORMATION.IP_NO = NURSE_RECORD_SIMPLE.IP_NO AND ROOM_AREA = '{2}' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO", s_date, e_date, roo_area);
            DataTable dt_ShiftExchange = getDataTable(strSQL, connection_id);
            return dt_ShiftExchange;
        }

        internal static DataTable getALLShiftExchange(string s_date, string e_date, string connection_id, string ip_no)
        {
            string strSQL = "SELECT SHIFT_EXCHANGE_RECORD.REC_NO, SHIFT_EXCHANGE_RECORD.NRS_NO, SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE, SHIFT_EXCHANGE_RECORD.A_TIME, SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED, NAME FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO  LEFT JOIN EMP_LOGIN ON EMP_LOGIN.Login = SHIFT_EXCHANGE_RECORD.A_USER WHERE SHIFT_EXCHANGE_RECORD.IP_NO = '" + ip_no + "' AND SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO";
            //string strSQL ="SELECT SHIFT_EXCHANGE_RECORD.NRS_NO, SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE, SHIFT_EXCHANGE_RECORD.A_TIME, SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO WHERE SHIFT_EXCHANGE_RECORD.IP_NO = '" + ip_no + "' AND SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND IP_INFORMATION.AccountEnable = 'Y' AND ROOM.AccountEnable = 'Y' AND NRS_NO <> '0' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO";
            DataTable dt_ShiftExchange = getDataTable(strSQL, connection_id);
            return dt_ShiftExchange;
        }

        internal static string addShiftExchange(string ip_no, string content, string create_date, string create_time, string create_user, string connection_id)
        {
            string warning;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = string.Format(@"INSERT INTO SHIFT_EXCHANGE_RECORD( NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable) VALUES ('{0}','{1}',N'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", "0", ip_no, content, create_date, create_time, create_user, create_date, create_time, create_user, "Y");
            SqlCommand cmd = new SqlCommand(strSQL, con);
            try
            {
                cmd.ExecuteNonQuery();
                warning = "新增成功";
            }
            catch
            {
                warning = "新增失敗";
            }
            finally 
            {
                con.Close();
            }
            return warning;
        }
        internal static string addShiftExchangeNR(string ip_no, string content, string create_date, string create_time, string create_user, string connection_id)
        {
            string warning;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = string.Format(@"INSERT INTO SHIFT_EXCHANGE_RECORD( NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable) VALUES ('{0}','{1}',N'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", "-999", ip_no, content, create_date, create_time, create_user, create_date, create_time, create_user, "Y");
            SqlCommand cmd = new SqlCommand(strSQL, con);
            try
            {
                cmd.ExecuteNonQuery();
                warning = "新增成功";
            }
            catch
            {
                warning = "新增失敗";
            }
            finally
            {
                con.Close();
            }
            return warning;
        }
        internal static string updateShiftContent(string rec_no, string content, string connection_id, string op_user)
        {
            string nrs_no= "";
            string create_date ="";
            string create_time ="";
            string create_user = "";
            string warning = "";
            string ip_no = "";
            string a_date = "";
            string a_time = "";
            string a_user = "";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();

            //取得欲修改的資料
            string str = "SELECT * FROM SHIFT_EXCHANGE_RECORD WHERE REC_NO ='"+rec_no+"'";
            SqlCommand cmd1 = new SqlCommand(str, con);
            SqlDataReader reader = cmd1.ExecuteReader();
            if (reader.Read())
            {
                nrs_no = reader["NRS_NO"].ToString();
                create_date = reader["CREATE_DATE"].ToString();
                create_time = reader["CREATE_TIME"].ToString();
                create_user = reader["CREATE_USER"].ToString();
                ip_no = reader["IP_NO"].ToString();
                a_date = reader["A_DATE"].ToString();
                a_time = reader["A_TIME"].ToString();
                a_user = reader["A_USER"].ToString();
            }
            reader.Close();

            SqlTransaction trans = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = trans;

            try
            {
                cmd.CommandText = "INSERT INTO SHIFT_EXCHANGE_RECORD (NRS_NO,IP_NO,NRS_CONTENT,A_DATE,A_TIME,A_USER,CREATE_DATE,CREATE_TIME,CREATE_USER, AccountEnable) VALUES('" + nrs_no + "','" + ip_no + "',N'" + content + "','" + a_date + "','" + a_time + "','" + a_user + "','" + create_date + "','" + create_time + "','" + create_user + "','Y')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE SHIFT_EXCHANGE_RECORD SET OP_DATE ='" + sqlTime.time() + "', OP_TIME ='" + sqlTime.hourminute() + "', OP_USER ='" + op_user + "', AccountEnable ='N' WHERE REC_NO ='" + rec_no + "'";
                cmd.ExecuteNonQuery();
                /*
                if (nrs_no != "0")
                {
                    cmd.CommandText = "UPDATE NURSE_RECORD_SIMPLE SET CONTENT ='" + content + "', OP_DATE ='" + sqlTime.time() + "', OP_TIME ='" + sqlTime.hourminute() + "', OP_USER ='" + op_user + "'  WHERE NO ='" + nrs_no + "'";
                    cmd.ExecuteNonQuery();
                }
                 */
                trans.Commit();
                warning = "修改成功";
            }
            catch
            {
                trans.Rollback();
                warning = "修改失敗";
            }
            finally 
            {
                trans.Dispose();
                con.Close();
            }
            return warning;
        }

        //雜項刪除
        internal static string delShiftContent(string rec_no, string connection_id, string user)
        {
            string warning;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = trans;
           
            try
            {
                cmd.CommandText = string.Format(@"SELECT NRS_NO FROM SHIFT_EXCHANGE_RECORD WHERE REC_NO ='{0}'", rec_no);
                SqlDataReader reader = cmd.ExecuteReader();
                string nrs_no = "";
                if (reader.Read())
                {
                    nrs_no = reader["NRS_NO"].ToString();                
                }
                reader.Close();

                cmd.CommandText = string.Format(@"UPDATE SHIFT_EXCHANGE_RECORD SET AccountEnable ='{0}', OP_DATE = '{1}', OP_TIME ='{2}', OP_USER ='{3}' WHERE AccountEnable ='Y' AND NRS_NO ='{4}'", "N", sqlTime.time(), sqlTime.hourminute(), user, nrs_no);
                cmd.ExecuteNonQuery();
                trans.Commit();
                warning = "刪除成功";
            }
            catch
            {
                trans.Rollback();
                warning = "刪除失敗";
            }
            finally
            {
                con.Close();
            }
            return warning;
        }

        internal static string delShift(string rec_no, string connection_id, string user)
        {
            string warning;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = trans;

            try
            {
                cmd.CommandText = string.Format(@"UPDATE SHIFT_EXCHANGE_RECORD SET AccountEnable ='{0}', OP_DATE = '{1}', OP_TIME ='{2}', OP_USER ='{3}' WHERE AccountEnable ='Y' AND REC_NO ='{4}'", "N", sqlTime.time(), sqlTime.hourminute(), user, rec_no);
                cmd.ExecuteNonQuery();
                trans.Commit();
                warning = "刪除成功";
            }
            catch
            {
                trans.Rollback();
                warning = "刪除失敗";
            }
            finally
            {
                con.Close();
            }
            return warning;
        }

        public static DataTable SearchLastSERecord(string ipno, string account, string connection_id, string sdate, string edate)
        {
            string sql = "SELECT NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE CREATE_USER ='" + account + "' AND IP_NO='" + ipno + "' AND NRS_NO = '0' AND AccountEnable = 'Y' AND CREATE_DATE BETWEEN '" + sdate + "' AND '" + edate + "' ORDER BY CREATE_DATE DESC , CREATE_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable SearchLastSERecordNR(string ipno, string account, string connection_id, string sdate, string edate)
        {
            string sql = "SELECT NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE CREATE_USER ='" + account + "' AND IP_NO='" + ipno + "' AND NRS_NO <> '0' AND AccountEnable = 'Y' AND CREATE_DATE BETWEEN '" + sdate + "' AND '" + edate + "' ORDER BY CREATE_DATE DESC , CREATE_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }

        public static DataTable SearchDitoSERecord(string ipno, string connection_id)
        {
            string sql = "SELECT TOP 1 NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE IP_NO='" + ipno + "' AND NRS_NO = '0' AND AccountEnable = 'Y' ORDER BY A_DATE DESC, A_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable SearchDitoSERecordNR(string ipno, string connection_id)
        {
            string sql = "SELECT TOP 1 NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE IP_NO='" + ipno + "' AND NRS_NO <> '0' AND AccountEnable = 'Y' ORDER BY A_DATE DESC, A_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }

        public static DataTable SearchIPThisSERecord(string ipno, string account, string nowdate, string connection_id)
        {
            string sql = "SELECT TOP 1 NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE CREATE_USER ='" + account + "' AND IP_NO='" + ipno + "' AND NRS_NO = '0' AND AccountEnable = 'Y' AND CREATE_DATE ='" + nowdate + "' ORDER BY CREATE_DATE DESC , CREATE_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable SearchIPThisSERecordNR(string ipno, string account, string nowdate, string connection_id)
        {
            string sql = "SELECT TOP 1 NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE CREATE_USER ='" + account + "' AND IP_NO='" + ipno + "' AND NRS_NO <> '0' AND AccountEnable = 'Y' AND CREATE_DATE ='" + nowdate + "' ORDER BY CREATE_DATE DESC , CREATE_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }

        internal static DataTable SearchIPInfo(string ip_no, string connection_id)
        {
            string strSQL = "SELECT IP_INFORMATION.IP_NAME, IP_INFORMATION.SEX, IP_INFORMATION.DOB, ROOM.ROOM_BED, IP_INFORMATION.IP_NO_NEW FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE IP_INFORMATION.IP_NO = '" + ip_no + "'";
            return sqlData.getDataTable(strSQL, connection_id);
        }
        internal static string SearchA_user(string a_user, string connection_id)
        {
            string datasource = connect(connection_id);
            string a_user_name = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            string sql = "SELECT Name FROM EMP_LOGIN WHERE EMP_LOGIN.Login = '" + a_user + "'";

            // SqlDataReader searchlist = search.ExecuteReader();
            SqlCommand search = new SqlCommand(sql, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_user_name = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_user_name;
        }

        public static DataTable getShift_Exchange_Record(string s_date, string e_date, string ip_no, string connection_id)
        {
            string sql = "SELECT NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE AccountEnable = 'Y'  AND IP_NO = '" + ip_no + "' AND NRS_NO = '0' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "'";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable getShift_Exchange_RecordNR(string s_date, string e_date, string ip_no, string connection_id)
        {
            string sql = "SELECT NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE AccountEnable = 'Y'  AND IP_NO = '" + ip_no + "' AND NRS_NO <> '0' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "'";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable getShift_Exchange_RecordAll(string s_date, string e_date, string ip_no, string connection_id)
        {
            string sql = "SELECT NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM SHIFT_EXCHANGE_RECORD WHERE AccountEnable = 'Y'  AND IP_NO = '" + ip_no + "' AND A_DATE BETWEEN '" + s_date + "' AND '" + e_date + "'";
            return sqlData.getDataTable(sql, connection_id);
        }

        public static DataTable getSelSERecord(string rec_no, string connection_id)
        {
            string sql = "SELECT SHIFT_EXCHANGE_RECORD.REC_NO,SHIFT_EXCHANGE_RECORD.NRS_NO,SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE,SHIFT_EXCHANGE_RECORD.A_TIME,SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.OP_DATE, SHIFT_EXCHANGE_RECORD.OP_TIME, SHIFT_EXCHANGE_RECORD.OP_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED, NAME FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO  LEFT JOIN EMP_LOGIN ON EMP_LOGIN.Login = SHIFT_EXCHANGE_RECORD.A_USER WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND SHIFT_EXCHANGE_RECORD.REC_NO = '" + rec_no + "'";
            return sqlData.getDataTable(sql, connection_id);
        }

        internal static string Search_ip_name(string ip_no, string connection_id)
        {
            string a_ip_name = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NO, IP_INFORMATION.IP_NAME FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO = '" + ip_no + "'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_name = searchlist.GetValue(1).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_name;
        }

        internal static string Search_ip_no(string ip_name, string connection_id)
        {
            string a_ip_no = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NO, IP_INFORMATION.IP_NAME FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NAME = '" + ip_name + "'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_no = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_no;
        }

        public static DataTable SearchRecData(string recno, string connection_id)
        {
            string strSQL = "SELECT REC_NO, NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER FROM SHIFT_EXCHANGE_RECORD WHERE REC_NO = '" + recno + "'";
            return sqlData.getDataTable(strSQL, connection_id);
        }

        //ShadowTB
        public void addshadowshiftexchangerecord(string REC_NO, string NRS_NO, string IP_NO, string NRS_CONTENT, string A_DATE, string A_TIME, string A_USER, string CREATE_DATE, string CREATE_TIME, string CREATE_USER, string OP_DATE, string OP_TIME, string OP_USER, string OPing_DATE, string OPing_TIME, string OPing_USER, string OPing_STATE, string connection_id)
        {
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            try
            {
                //將資料新增至Shadow交班紀錄資料表
                /* SREC_NO, REC_NO, NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER, OPing_DATE, OPing_TIME, OPing_USER                 */
                string cmdaddShadow = "INSERT INTO SHADOW_SHIFT_EXCHANGE_RECORD(REC_NO, NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER, OPing_DATE, OPing_TIME, OPing_USER) ";
                cmdaddShadow += "VALUES ('" + REC_NO + "','" + NRS_NO + "','" + IP_NO + "',N'" + NRS_CONTENT + "','" + A_DATE + "','" + A_TIME + "','" + A_USER + "','" + CREATE_DATE + "','" + CREATE_TIME + "','" + CREATE_USER + "','" + OP_DATE + "','" + OP_TIME + "','" + OP_USER +"','"+ OPing_DATE + "','" + OPing_TIME + "','" + OPing_USER + "')";
                SqlCommand cmd = new SqlCommand(cmdaddShadow, con);
                cmd.ExecuteNonQuery();
                shadowwarning = "新增資料成功";
            }
            catch
            {
                shadowwarning = "新增資料失敗";
            }
            finally
            {
                con.Close();
            }
        }

        public string GetShadowWarning()
        {
            return shadowwarning;
        }

        internal static string Search_ip_no_by_ip_no_new(string ip_no_new, string connection_id)
        {
            string a_ip_no = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NO FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO_NEW = '" + ip_no_new + "'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_no = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_no;
        }

        internal static string Search_ip_no_new_by_ip_no(string ip_no, string connection_id)
        {
            string a_ip_no_new = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NO_NEW FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO = '" + ip_no + "'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_no_new = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_no_new;
        }

        internal static string Search_ip_name_by_ip_no(string ip_no, string connection_id)
        {
            string a_ip_name = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NAME FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO = '" + ip_no + "'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_name = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_name;
        }

        internal static string Search_ip_name_by_ip_no_new(string ip_no_new, string connection_id)
        {
            string a_ip_name = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NAME FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO_NEW = '" + ip_no_new + "'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_name = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_name;
        }

        //護理交班
        internal static DataTable searchSHIFTE_RECORD(string connection_id, string sql)
        {
            string strSQL = "SELECT SHIFT_EXCHANGE_RECORD.*, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, EMP_LOGIN.Name, ROOM.ROOM_AREA FROM SHIFT_EXCHANGE_RECORD INNER JOIN EMP_LOGIN ON SHIFT_EXCHANGE_RECORD.A_USER = EMP_LOGIN.Login LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO=IP_INFORMATION.IP_NO WHERE SHIFT_EXCHANGE_RECORD.ACCOUNTENABLE ='Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO!='0' " + sql + "ORDER BY SHIFT_EXCHANGE_RECORD.A_DATE DESC,SHIFT_EXCHANGE_RECORD.REC_NO DESC";
            return sqlData.getDataTable(strSQL, connection_id);
        }

        internal static DataTable SearchLastSHIFTE_RECORD(string connection_id, string sql, string num)
        {
            string strSQL = "SELECT top " + num + " SHIFT_EXCHANGE_RECORD.*, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, EMP_LOGIN.Name, ROOM.ROOM_AREA FROM SHIFT_EXCHANGE_RECORD INNER JOIN EMP_LOGIN ON SHIFT_EXCHANGE_RECORD.A_USER = EMP_LOGIN.Login LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO=IP_INFORMATION.IP_NO WHERE SHIFT_EXCHANGE_RECORD.ACCOUNTENABLE ='Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO!='0' " + sql + "ORDER BY SHIFT_EXCHANGE_RECORD.A_DATE DESC,SHIFT_EXCHANGE_RECORD.REC_NO DESC";
            return sqlData.getDataTable(strSQL, connection_id);
        }

        //雜項交班
        internal static DataTable searchSHIFTE_RECORD2(string connection_id, string sql)
        {
            string strSQL = "SELECT SHIFT_EXCHANGE_RECORD.*, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, EMP_LOGIN.Name, ROOM.ROOM_AREA FROM SHIFT_EXCHANGE_RECORD INNER JOIN EMP_LOGIN ON SHIFT_EXCHANGE_RECORD.A_USER = EMP_LOGIN.Login LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO=IP_INFORMATION.IP_NO WHERE SHIFT_EXCHANGE_RECORD.ACCOUNTENABLE ='Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO='0' " + sql + "ORDER BY SHIFT_EXCHANGE_RECORD.A_DATE DESC,SHIFT_EXCHANGE_RECORD.REC_NO DESC";
            return sqlData.getDataTable(strSQL, connection_id);
        }

        internal static DataTable SearchLastSHIFTE_RECORD2(string connection_id, string sql, string num)
        {
            string strSQL = "SELECT top " + num + " SHIFT_EXCHANGE_RECORD.*, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, EMP_LOGIN.Name, ROOM.ROOM_AREA FROM SHIFT_EXCHANGE_RECORD INNER JOIN EMP_LOGIN ON SHIFT_EXCHANGE_RECORD.A_USER = EMP_LOGIN.Login LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO=IP_INFORMATION.IP_NO WHERE SHIFT_EXCHANGE_RECORD.ACCOUNTENABLE ='Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO='0' " + sql + "ORDER BY SHIFT_EXCHANGE_RECORD.A_DATE DESC,SHIFT_EXCHANGE_RECORD.REC_NO DESC";
            return sqlData.getDataTable(strSQL, connection_id);
        }

    }
}