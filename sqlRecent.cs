using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace longtermcare
{

    public class sqlRecent
    {
        private static string datasource;

        public static void connect(string connection_id)
        {
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
        }

        public static DataSet ReturnForm()
        {
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            con.Open();

            string cmd = "SELECT FORM_NAME,SELECT_COMM_COL,SELECT_COMM_COL_NAME,SELECT_COMM_TABLE,SELECT_COMM_WHERE_DATE,SELECT_COMM_ORDERBY_DATE,NO_STRING FROM CODE_RECENT_STATE order by FORM_SEQ";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd, con);

            DataSet Form = new DataSet();
            adapter.Fill(Form, "Form");


            con.Close();
            return Form;
        }

        public static DataSet ReturnData(string SELECT_COMM_COL, string SELECT_COMM_TABLE, string SELECT_COMM_ORDERBY_DATE, string NO_STRING, string ipno, string edate)
        {
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            con.Open();

            string cmd = "";
            string[] order = SELECT_COMM_ORDERBY_DATE.Split(',');

            if (order.Length != 2)
            {
                cmd = "SELECT TOP 1" + SELECT_COMM_COL + " FROM " + SELECT_COMM_TABLE + " WHERE " + NO_STRING + " ='" + ipno + "' AND AccountEnable='Y' AND " + SELECT_COMM_ORDERBY_DATE + "<='" + edate + "' ORDER BY " + SELECT_COMM_ORDERBY_DATE + " DESC";
            }
            else
            {
                cmd = "SELECT TOP 1" + SELECT_COMM_COL + " FROM " + SELECT_COMM_TABLE + " WHERE " + NO_STRING + " ='" + ipno + "' AND AccountEnable='Y' AND " + order[0] + "<='" + edate + "' ORDER BY " + order[0] + " DESC," + order[1] + " DESC";

            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd, con);

            DataSet Data = new DataSet();
            adapter.Fill(Data, "Data");


            con.Close();
            return Data;
        }

        public static DataSet Return3Data(string SELECT_COMM_COL, string SELECT_COMM_TABLE, string SELECT_COMM_ORDERBY_DATE, string NO_STRING, string ipno, string edate)
        {
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            con.Open();

            string cmd = "";
            string[] order = SELECT_COMM_ORDERBY_DATE.Split(',');
            
            if (order.Length != 2)
            {
                cmd = "SELECT TOP 3" + SELECT_COMM_COL + " FROM " + SELECT_COMM_TABLE + " WHERE " + NO_STRING + " ='" + ipno + "' AND AccountEnable='Y' AND " + SELECT_COMM_ORDERBY_DATE + "<='" + edate + "' ORDER BY " + SELECT_COMM_ORDERBY_DATE + " DESC";
            }
            else
            {
                cmd = "SELECT TOP 3" + SELECT_COMM_COL + " FROM " + SELECT_COMM_TABLE + " WHERE " + NO_STRING + " ='" + ipno + "' AND AccountEnable='Y' AND " + order[0] + "<='" + edate + "' ORDER BY " + order[0] + " DESC," + order[1] + " DESC";
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd, con);

            DataSet Data = new DataSet();
            adapter.Fill(Data, "Data");
            
            con.Close();
            return Data;
        }

    }
}