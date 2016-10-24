using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;


namespace longtermcare.NursingRecordSimple
{
    public class sqlNursingRecordSimple
    {
        private string shadowwarning;
        public static string connect(string connection_id)
        {
            string datasource;
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            return datasource;
        }

        public static string insertRecord(string ipno, string Rdate, string Rtime, string content, string create_date, string create_time, string create_user, string addShiftExchange, string Diag, string connection_id)
        {
            string warning;
            string datasource = connect(connection_id);
            using (SqlConnection cn = new SqlConnection())
            {
                
                cn.ConnectionString = datasource;
                cn.Open();
                SqlTransaction trans = cn.BeginTransaction();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = trans;

                try
                {
                    
                    //cmd.CommandText = "INSERT INTO NURSE_RECORD_SIMPLE (IP_NO,R_DATE,R_TIME,CONTENT,CREATE_DATE,CREATE_TIME,CREATE_USER, DIAG_ID, AccountEnable, ShiftExchange) VALUES ('" + ipno + "','" + Rdate + "','" + Rtime + "', N'" + content + "','" + create_date + "','" + create_time + "','" + create_user + "','" + Diag + "','Y', '" + addShiftExchange + "')";

                    //cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO NURSE_RECORD_SIMPLE (IP_NO, R_DATE, R_TIME, CONTENT, CREATE_DATE, CREATE_TIME, CREATE_USER, DIAG_ID, AccountEnable, ShiftExchange) VALUES (@IP_NO, @R_DATE, @R_TIME, @CONTENT, @CREATE_DATE, @CREATE_TIME, @CREATE_USER, @DIAG_ID, 'Y', @ShiftExchange)";
                    cmd.Parameters.Add("IP_NO", SqlDbType.VarChar).Value = ipno;
                    cmd.Parameters.Add("R_DATE", SqlDbType.Char).Value = Rdate;
                    cmd.Parameters.Add("R_TIME", SqlDbType.Char).Value = Rtime;
                    cmd.Parameters.Add("CONTENT", SqlDbType.NVarChar).Value = content;
                    cmd.Parameters.Add("CREATE_DATE", SqlDbType.Char).Value = create_date;
                    cmd.Parameters.Add("CREATE_TIME", SqlDbType.Char).Value = create_time;
                    cmd.Parameters.Add("CREATE_USER", SqlDbType.VarChar).Value = create_user;
                    cmd.Parameters.Add("DIAG_ID", SqlDbType.VarChar).Value = Diag;
                    cmd.Parameters.Add("ShiftExchange", SqlDbType.Char).Value = addShiftExchange;
                    
                    //('" + ipno + "','" + Rdate + "','" + Rtime + "', N'" + content + "','" + create_date + "','" + create_time + "','" + create_user + "','" + Diag + "','Y', '" + addShiftExchange + "')";

                    cmd.ExecuteNonQuery();
                    
                    /*
                    if (addShiftExchange == "Y") //有加入交班記錄
                    {                       
                        cmd.CommandText = "SELECT ISNULL(MAX(NO), 0) AS MAX_NUM FROM NURSE_RECORD_SIMPLE";
                        SqlDataReader reader = cmd.ExecuteReader();
                        string rec_no = "";
                        if (reader.Read())
                        {
                            rec_no = reader["MAX_NUM"].ToString();
                        }
                        reader.Close();

                        cmd.CommandText = string.Format(@"INSERT INTO SHIFT_EXCHANGE_RECORD (NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", rec_no, ipno, content, Rdate, Rtime, create_user, create_date, create_time, create_user, "Y");
                        cmd.ExecuteNonQuery();
                    }
                    */
                    trans.Commit();
                    warning = "新增成功";
                }
                catch
                {
                    trans.Rollback();
                    warning = "新增失敗";
                }
                finally
                {
                    cn.Close();
                }
            }
            return warning;
        }
        public static DataTable selRecord(string no, string connection_id)
        {
            string datasource = connect(connection_id);
            string strSQL = "SELECT A1.IP_NO,A1.R_DATE,A1.R_TIME,A1.CONTENT,A1.CREATE_DATE,A1.CREATE_TIME,A1.CREATE_USER, A1.DIAG_ID, A1.AccountEnable, A1.ShiftExchange, A2.IP_NO_NEW, A2.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN IP_INFORMATION AS A2 ON A1.IP_NO = A2.IP_NO WHERE NO='" + no + "'";
            DataTable dt_selRecord = getDataTable(strSQL, connection_id);
            return dt_selRecord;
        }

        private static DataTable getDataTable(string strSQL,string connection_id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connect(connection_id);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            cmd.Dispose();
            con.Close();
            return dt;
        }

        internal static DataTable searchRecordDT(string connection_id, string strSQL)
        {
            return sqlData.getDataTable(strSQL, connection_id);
        }

        public static string delRecord(string no, string date, string user, string connection_id)
        {
            string warning = "";
            string datasource = connect(connection_id);
            using (SqlConnection cn = new SqlConnection())
            {
                //s = datasource;
                cn.ConnectionString = datasource;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cn.Open();
                string cmdmodify = "UPDATE NURSE_RECORD_SIMPLE set AccountEnable='N' where NO='" + no + "'";
                SqlCommand updateinfo = new SqlCommand(cmdmodify, cn);
                try
                {
                    updateinfo.ExecuteNonQuery();
                    warning = "刪除成功";
                }
                catch
                {
                    warning = "刪除失敗";
                }
                finally
                {
                    cn.Close();
                }
            }
            return warning;
        }
        internal static DataTable getDDL3Source(string ip_no, string connection_id)
        {
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = connect(connection_id);
            con.Open();
            string strSQL = "SELECT DISTINCT PLAN_DIAG.DIAG_ID, NURSING_DIAGNOSIS.DIAG_NAME FROM PLAN_DIAG LEFT JOIN NURSING_DIAGNOSIS ON PLAN_DIAG.DIAG_ID = NURSING_DIAGNOSIS.DIAG_ID WHERE USE_STATUS = '1' AND PID = '" + ip_no + "' ";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt_ip_ques_sel = new DataTable();
            adp.Fill(dt_ip_ques_sel); //取得目前住民健康問題          

            cmd.Dispose();
            StringBuilder tSQL = new StringBuilder();
            DataTable dt_DIAG_ID = dt_ip_ques_sel.DefaultView.ToTable(true, new string[] { "DIAG_ID" });
            if (dt_DIAG_ID.Rows.Count != 0)
            {
                tSQL.AppendFormat("SELECT DISTINCT PLAN_DIAG.DIAG_ID, NURSING_DIAGNOSIS.DIAG_NAME ");
                tSQL.AppendFormat("FROM PLAN_DIAG LEFT JOIN NURSING_DIAGNOSIS ON PLAN_DIAG.DIAG_ID = NURSING_DIAGNOSIS.DIAG_ID ");
                tSQL.AppendFormat("WHERE USE_STATUS = '1' ");
                tSQL.AppendFormat("UNION ALL ");
                tSQL.AppendFormat("SELECT DISTINCT DIAG_ID, DIAG_NAME ");
                tSQL.AppendFormat("FROM NURSING_DIAGNOSIS ");
                tSQL.AppendFormat("WHERE USE_STATUS = '1' ");

                foreach (DataRow row in dt_DIAG_ID.Rows)
                {
                    tSQL.AppendFormat("AND DIAG_ID <> '{0}' ", row["DIAG_ID"]);
                }
            }
            else
            {
                tSQL.AppendFormat("SELECT DISTINCT DIAG_ID, DIAG_NAME FROM NURSING_DIAGNOSIS WHERE USE_STATUS = '1'");
            }

            SqlCommand cmd1 = new SqlCommand(tSQL.ToString(), con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dd3_source = new DataTable();
            adp1.Fill(dd3_source); //取得目前住民健康問題            
            cmd1.Dispose();
            con.Close();
            return dd3_source;
        }
        /*
        public static string systemdate()
        {
            string warning = "";
            string datasource = connect(connection_id);
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = datasource;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cn.Open();
                string cmdtime = "select CONVERT(varchar, getdate(), 120)";
                SqlCommand gettime = new SqlCommand(cmdtime, cn);
                SqlDataReader time = gettime.ExecuteReader();
                string now = "";
                if (time.Read())
                {
                    now = time.GetValue(0).ToString();
                }
                time.Close();
                cn.Close();
                return now;
            }
        }
        */
        public static string modify(string r_date, string r_time, string op_date, string op_time, string op_user, string content, string Diag, string rec_no, string connection_id)
        {
            string warning = "";
            string datasource = connect(connection_id);
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = datasource;
                cn.Open();
                SqlTransaction trans = cn.BeginTransaction();
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = trans;
                
                try
                {
                    //cmd.CommandText = "Update NURSE_RECORD_SIMPLE SET R_DATE ='" + r_date + "',R_TIME ='" + r_time + "',OP_DATE ='" + op_date + "',OP_TIME ='" + op_time + "',OP_USER ='" + op_user + "',CONTENT = N'" + content + "',DIAG_ID ='" + Diag + "'  WHERE NO ='" + rec_no + "'";
                    //cmd.ExecuteNonQuery();

                    //cmd.CommandText = "Update SHIFT_EXCHANGE_RECORD SET A_DATE ='" + r_date + "',A_TIME ='" + r_time + "', OP_DATE ='" + op_date + "',OP_TIME ='" + op_time + "',OP_USER ='" + op_user + "',NRS_CONTENT ='" + content + "'  WHERE NRS_NO ='" + rec_no + "' AND AccountEnable = 'Y'";
                    //cmd.ExecuteNonQuery();

                    cmd.CommandText = "Update NURSE_RECORD_SIMPLE SET R_DATE =@R_DATE, R_TIME =@R_TIME ,OP_DATE =@OP_DATE ,OP_TIME =@OP_TIME ,OP_USER =@OP_USER ,CONTENT =@CONTENT ,DIAG_ID =@DIAG_ID  WHERE NO = @NO";
                    cmd.Parameters.Add("R_DATE", SqlDbType.Char).Value = r_date;
                    cmd.Parameters.Add("R_TIME", SqlDbType.Char).Value = r_time;
                    cmd.Parameters.Add("CONTENT", SqlDbType.NVarChar).Value = content;
                    cmd.Parameters.Add("OP_DATE", SqlDbType.Char).Value = op_date;
                    cmd.Parameters.Add("OP_TIME", SqlDbType.Char).Value = op_time;
                    cmd.Parameters.Add("OP_USER", SqlDbType.VarChar).Value = op_user;
                    cmd.Parameters.Add("DIAG_ID", SqlDbType.VarChar).Value = Diag;
                    cmd.Parameters.Add("NO", SqlDbType.Int).Value = rec_no;
                    cmd.ExecuteNonQuery();

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
                    cn.Close();
                }
            }
            return warning;
        }
        public static void searchNRS(string rec_no, string connection_id)
        {
            string warning = "";
            string create_date = "";
            string create_time = "";
            string content = "";

            string datasource = connect(connection_id);
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = datasource;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cn.Open();

                string cmdsearch = "SELECT NO, IP_NO, R_DATE, R_TIME, CONTENT, FROM NURSE_RECORD_SIMPLE where REC_NO = '" + rec_no + "'AND AccountEnable = 'Y'";
                SqlCommand searchNRS = new SqlCommand(cmdsearch, cn);
                SqlDataReader aftersearchNRS = searchNRS.ExecuteReader();
                if (rec_no != "")
                {
                    if (aftersearchNRS.Read())
                    {
                        create_date = aftersearchNRS.GetValue(2).ToString();
                        create_time = aftersearchNRS.GetValue(3).ToString();
                        content = aftersearchNRS.GetValue(4).ToString();
                        warning = "";
                    }
                    else
                    {
                        warning = "無此日期資料";
                        create_date = "";
                        create_time = "";
                        content = "";
                    }
                    aftersearchNRS.Close();
                    cn.Close();
                }
                else
                {
                    warning = "請輸入日期";
                    create_date = "";
                    create_time = "";
                    content = "";
                    aftersearchNRS.Close();
                    cn.Close();
                }
            }
        }



        internal static DataTable SearchIPInfo(string ip_no, string connection_id)
        {
            string datasource = connect(connection_id);
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NAME, IP_INFORMATION.SEX, IP_INFORMATION.DOB, ROOM.ROOM_BED, IP_INFORMATION.IP_NO_NEW FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE IP_INFORMATION.IP_NO = '" + ip_no + "'";
            // SqlDataReader searchlist = search.ExecuteReader();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            cmd.Dispose();
            con.Close();
            return dt;
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

        internal static bool updateShiftExchange(string flag, string rec_no, string user, string content, string connection_id, string rdate, string rtime, string ip_no)
        {
            string datasource = connect(connection_id);
            bool success = false;
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            SqlCommand cmd = con.CreateCommand();
            cmd.Transaction = trans;
            try
            {
                string nowdate = sqlTime.time();
                string nowtime = sqlTime.hourminute();
                cmd.CommandText = string.Format("UPDATE NURSE_RECORD_SIMPLE SET ShiftExchange ='{0}' WHERE NO = '{1}'", flag, rec_no);
                cmd.ExecuteNonQuery();
                if (flag == "Y")
                {
                    cmd.CommandText = string.Format(@"INSERT INTO SHIFT_EXCHANGE_RECORD (NRS_NO,IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable) VALUES ('{0}','{1}',N'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", rec_no, ip_no, content, rdate, rtime, user, sqlTime.time(), sqlTime.hourminute(), user, "Y");

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = string.Format("UPDATE SHIFT_EXCHANGE_RECORD SET OP_DATE = '{0}', OP_TIME = '{1}', OP_USER = '{2}', AccountEnable='{3}' WHERE NRS_NO = '{4}'", nowdate, nowtime, user, "N", rec_no);
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                success = true;
            }
            catch
            {
                trans.Rollback();
                success = false;
            }
            finally 
            {
                con.Close();
            }
            return success;
        }

        internal static DataTable getContent(string rec_no, string connection_id)
        {                     
            string strSQL = string.Format("SELECT IP_NO, R_DATE, R_TIME, CONTENT FROM NURSE_RECORD_SIMPLE WHERE NO ='{0}'", rec_no);
            DataTable content = getDataTable(strSQL, connection_id);
            return content;
        }

        internal static bool checkShiftExchange(string rec_no, string connection_id)
        {
            string datasource = connect(connection_id);
            bool check = true;
            string shiftExchange = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            con.Open();

            string strSQL = string.Format("SELECT ShiftExchange FROM NURSE_RECORD_SIMPLE WHERE NO ='{0}'", rec_no);
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                shiftExchange = reader.GetValue(0).ToString();
            }
            cmd.Dispose();
            reader.Close();
            con.Close();
            if (shiftExchange.Trim() == null || shiftExchange.Trim()=="N" || shiftExchange.Trim() == "")
            {
                check = false;
            }
            return check;
        }
        public static DataTable SearchLastNRecord(string ipno, string connection_id)
        {
            string sql = "SELECT TOP 1 NURSE_RECORD_SIMPLE.R_DATE, NURSE_RECORD_SIMPLE.R_TIME, NURSE_RECORD_SIMPLE.CONTENT, EMP_LOGIN.Name AS CREATE_USER " +
                         "FROM NURSE_RECORD_SIMPLE LEFT JOIN EMP_LOGIN ON NURSE_RECORD_SIMPLE.CREATE_USER = EMP_LOGIN.Login " +
                         "WHERE NURSE_RECORD_SIMPLE.IP_NO='" + ipno + "' AND NURSE_RECORD_SIMPLE.AccountEnable = 'Y' ORDER BY NURSE_RECORD_SIMPLE.R_DATE DESC, NURSE_RECORD_SIMPLE.R_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable SearchLastFiveNRecord(string ipno, string connection_id)
        {
            string sql = "SELECT TOP 5 NURSE_RECORD_SIMPLE.R_DATE, NURSE_RECORD_SIMPLE.R_TIME, NURSE_RECORD_SIMPLE.CONTENT, EMP_LOGIN.Name AS CREATE_USER " +
                         "FROM NURSE_RECORD_SIMPLE LEFT JOIN EMP_LOGIN ON NURSE_RECORD_SIMPLE.CREATE_USER = EMP_LOGIN.Login " +
                         "WHERE NURSE_RECORD_SIMPLE.IP_NO='" + ipno + "' AND NURSE_RECORD_SIMPLE.AccountEnable = 'Y' ORDER BY NURSE_RECORD_SIMPLE.R_DATE DESC, NURSE_RECORD_SIMPLE.R_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable SearchDitoNRecord(string ipno, string connection_id)
        {
            string sql = "SELECT TOP 1 IP_NO, R_DATE, R_TIME, CONTENT, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable, ShiftExchange FROM NURSE_RECORD_SIMPLE WHERE IP_NO='" + ipno + "' AND AccountEnable = 'Y' ORDER BY R_DATE DESC, R_TIME DESC";
            return sqlData.getDataTable(sql, connection_id);
        }
        public static DataTable getNursingRecord(string s_date, string e_date, string ip_no, string connection_id)
        {
            string sql = "SELECT IP_NO, R_DATE, R_TIME, CONTENT, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable FROM NURSE_RECORD_SIMPLE WHERE AccountEnable = 'Y'  AND IP_NO = '" + ip_no + "' AND R_DATE BETWEEN '" + s_date + "' AND '" + e_date + "'";
            return sqlData.getDataTable(sql, connection_id);
        }

        public static DataTable getNursingRecordThisIP(string nrssql, string connection_id)
        {
            return sqlData.getDataTable(nrssql, connection_id);
        }

        public static DataTable getNursingRecordAllIP(string nrssql, string connection_id)
        {
            return sqlData.getDataTable(nrssql, connection_id);
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

        internal static string Search_ip_no_by_ip_no_new(string ip_no_new, string connection_id)
        {
            string a_ip_no = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NO, IP_INFORMATION.IP_NAME FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO_NEW = '" + ip_no_new + "'";
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
            string strSQL = "SELECT NO, IP_NO, R_DATE, R_TIME, CONTENT, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER, ShiftExchange, DIAG_ID FROM NURSE_RECORD_SIMPLE WHERE NO = '" + recno + "'";
            return sqlData.getDataTable(strSQL, connection_id);
        }
        
        //ShadowTB
        public void addshadowNursingRecordSimple(string NO, string IP_NO, string R_DATE, string R_TIME, string CONTENT, string CREATE_DATE, string CREATE_TIME, string CREATE_USER, string OP_DATE, string OP_TIME, string OP_USER, string ShiftExchange, string DIAG_ID, string OPing_DATE, string OPing_TIME, string OPing_USER, string OPing_STATE, string connection_id)
        {
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            try
            {
                //將資料新增至Shadow簡單版護理紀錄紀錄資料表
                string cmdaddShadow = "INSERT INTO SHADOW_NURSE_RECORD_SIMPLE(NO, IP_NO, R_DATE, R_TIME, CONTENT, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER, ShiftExchange, DIAG_ID, OPing_DATE, OPing_TIME, OPing_USER, OPing_STATE) ";
                cmdaddShadow += "VALUES ('" + NO + "','" + IP_NO + "','" + R_DATE + "','" + R_TIME + "',N'" + CONTENT + "','" + CREATE_DATE + "','" + CREATE_TIME + "','" + CREATE_USER + "','" + OP_DATE + "','" + OP_TIME + "','" + OP_USER + "','" + ShiftExchange + "','" + DIAG_ID + "','" + OPing_DATE + "','" + OPing_TIME + "','" + OPing_USER + "','" + OPing_STATE + "')";
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
    }
}