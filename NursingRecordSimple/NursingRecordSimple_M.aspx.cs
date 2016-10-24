using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using AjaxControlToolkit;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Net;
using Ionic.Zip;
using System.Drawing.Imaging;
using System.Web.UI.HtmlControls;

namespace longtermcare.NursingRecordSimple
{
    public partial class NursingRecordSimple_M : System.Web.UI.Page
    {
        private string ip_no;
        private string rec_no;
        private string r_date;
        private string r_time;
        private string op_date;
        private string op_time;
        private string op_user;
        private string warning;
        public string connection_id;
        CheckBoxList[] chklp = new CheckBoxList[1000];
        CheckBoxList[] chklp_p = new CheckBoxList[1000];

        System.Web.UI.WebControls.Image sortImage = new System.Web.UI.WebControls.Image();
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }
        //離開網頁時清除狀態轉換的Session
        /*
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_NursingRecordSimple_M");
        }
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["ipno"] != null)
            {
                ip_no = Session["ipno"].ToString();
                if (Session["ipno"] == null)
                {
                    HttpCookie myipno_Cookie = Request.Cookies["ipno"];
                    if (myipno_Cookie != null)
                    {
                        Session["ipno"] = myipno_Cookie.Values["ipno"];
                        ip_no = myipno_Cookie.Values["ipno"];
                        str_ip_no.Text = myipno_Cookie.Values["ipno"];
                    }
                    else
                    {
                        Response.Write("<script>alert('請先點選住民以便查詢/修改/刪除');</script>");
                    }
                }
                else
                {
                    str_ip_no.Text = Session["ipno"].ToString();
                    ip_no = Session["ipno"].ToString();
                }

                if (Session["H_id"] == null)
                {
                    HttpCookie myH_id_Cookie = Request.Cookies["H_id"];
                    Session["H_id"] = myH_id_Cookie.Values["H_id"];
                    connection_id = myH_id_Cookie.Values["H_id"];
                    str_connection_id.Text = myH_id_Cookie.Values["H_id"];
                }
                else
                {
                    connection_id = Session["H_id"].ToString();
                    str_connection_id.Text = Session["H_id"].ToString();
                }

                if (Session["account"] != null)
                {
                    str_account.Text = Session["account"].ToString();
                }
                else
                {
                    HttpCookie myaccount_Cookie = Request.Cookies["account"];
                    Session["account"] = myaccount_Cookie.Values["account"];
                    str_account.Text = myaccount_Cookie.Values["account"];
                }
                sqlNursingRecordSimple.connect(connection_id);
                SqlDataSource3.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
                //SqlDataSource3.SelectCommand = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE (A1.IP_NO = @ipno) AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST(@Sdate AS DateTime), 112) AND CONVERT(varchar(8), CAST(@Edate AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ORDER BY A1.R_DATE DESC, A1.R_TIME DESC, A1.NO DESC";
                SqlDataSource3.DeleteCommand = "UPDATE [NURSE_RECORD_SIMPLE] SET [AccountEnable] ='N' WHERE NO = @NO";

                SqlDataSource5.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
                SqlDataSource5.SelectCommand = "SELECT [AREA_STATE] FROM [CODE_ROOM_AREA] ORDER BY [AREA_ID] ASC";

                this.Form.Attributes.Add("autocomplete", "off");
                string language = "";
                if (Session["language"] == null)
                {
                    Session["language"] = "ch";
                    language = "ch";
                }
                else
                {
                    language = Session["language"].ToString();
                }

                tab_state_NursingRecordSimple_M.Text = "20";
                //權限檢查
                //查詢權限表此人在此表單的權限並寫入至session
                //無查詢權限
                //if(權限session==no query && tab_state_NursingRecordSimple_M.Text=="20")
                //{
                //Session["tab_state_NursingRecordSimple_A"] = "10";
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel5, typeof(UpdatePanel), "test", "alert('" + "您的權限在此無查詢權限" + "'); location.href='NursingRecordSimple_A.aspx';", true);
                //}
                //只有查詢權限
                //else if(權限session==only query && tab_state_NursingRecordSimple_M.Text=="20")
                //{ 
                //tab_state_NursingRecordSimple_M.Text = "200";  
                //}
                //有查詢/修改權限
                //else if(權限session==query and update && tab_state_NursingRecordSimple_M.Text=="20")
                //{ 
                //tab_state_NursingRecordSimple_M.Text = "201";  
                //}
                //有查詢/刪除權限
                //else if(權限session==query and delete && tab_state_NursingRecordSimple_M.Text=="20")
                //{ 
                //tab_state_NursingRecordSimple_M.Text = "202";  
                //}
                //有查詢/修改/刪除權限
                //else if(權限session==query and update and delete && tab_state_NursingRecordSimple_M.Text=="20")
                //{ 
                tab_state_NursingRecordSimple_M.Text = "203";
                //}
                
                    State1();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text + "1";

                    /*
                    if (tab_state_NursingRecordSimple_M.Text == "200")//只能查
                    {
                         State1();
                         tab_state_NursingRecordSimple_M.Text=tab_state_NursingRecordSimple_M.Text + "1";
                    }
                    if (tab_state_NursingRecordSimple_M.Text == "201")//查修
                    {
                         State1();
                         tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text + "1";
                    }
                    if (tab_state_NursingRecordSimple_M.Text == "202")//查刪
                    {
                         State1();
                         tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text + "1";
                    }
                    if (tab_state_NursingRecordSimple_M.Text == "203")//查刪修
                    {
                         State1();
                         tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text + "1";
                    }
                    */
                    if (IsPostBack != true)
                    {
                       DateTimeQueryShow();
                       //DropDownList3.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                       //DropDownList3.DataSource = sqlNursingRecordSimple.getDDL3Source(ip_no, connection_id);
                       //DropDownList3.DataBind();
                       querytype.Text = "0";
                        /*
                        if (String.IsNullOrEmpty(GridView1.SortExpression))
                         {
                             GridView1.Sort("R_DATE", SortDirection.Descending);
                         }
                         */
                    }
            }
            else
            {
                Response.Write("<script>alert('請先點選住民'); location.href='../../FrontPage.aspx'; </script>");
            }
            
        }

        protected void txtstartdate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strS_Date = txtstartdate.Text.Substring(0, 4) + txtstartdate.Text.Substring(5, 2) + txtstartdate.Text.Substring(8, 2);
                int vsdate = Convert.ToInt32(strS_Date);
                string datewarning = DateTimeCheck.datevaild(strS_Date);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
            }
        }

        protected void txtenddate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strE_Date = txtenddate.Text.Substring(0, 4) + txtenddate.Text.Substring(5, 2) + txtenddate.Text.Substring(8, 2);
                int vedate = Convert.ToInt32(strE_Date);
                string datewarning = DateTimeCheck.datevaild(strE_Date);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
            }
        }

        protected void DropDownListIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListIP.SelectedIndex == 0)
            {
                DropDownListArea.Enabled = false;
                DropDownListArea.SelectedIndex = -1;
            }
            else
            {
                DropDownListArea.Enabled = true;
            }
        }

        //查詢
        //查詢/排序 會用到的程序(產生GridView來源)
        private DataTable BindGridView(string strsql)
        {
            DataTable dt = sqlNursingRecordSimple.searchRecordDT(str_connection_id.Text, strsql);
            return dt;
        }
        //用此一住民與所有住民之起迄日查詢(querytype.Text = "1")
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            try
            {
                string s_date = sqlTime.DateSplitSlash(txtstartdate.Text);
                string e_date = sqlTime.DateSplitSlash(txtenddate.Text);
                int vsdate = Convert.ToInt32(s_date);
                int vedate = Convert.ToInt32(e_date);
                string datewarning = DateTimeCheck.datevaild(s_date);
                string datewarning1 = DateTimeCheck.datevaild(e_date);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                }
                else if (datewarning1 == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                }
                else
                {
                    string nrssql, nrssql1;
                    if (DropDownListIP.SelectedValue == "0")//此一住民
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO "+
                                  "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO "+
                                  "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//所有住民
                    {
                        if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                        {
                            if (RadioButtonList1.SelectedIndex == 0)
                            {
                                nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                      "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ";
                                nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else if (RadioButtonList1.SelectedIndex == 1)
                            {
                                nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                      "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else
                            {
                                nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                      "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                        }
                        else//有選擇特定區域和樓層
                        {
                            if (RadioButtonList1.SelectedIndex == 0)
                            {
                                nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                      "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else if (RadioButtonList1.SelectedIndex == 1)
                            {
                                nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                      "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else
                            {
                                nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                      "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                 nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                        }
                    }
                    SqlDataSource3.SelectCommand = nrssql + nrssql1;
                    txt_sql.Text = nrssql;
                    txt_sql1.Text = nrssql1;
                    DataView sortedView = new DataView(BindGridView(nrssql + nrssql1));
                    GridView1.DataSource = sortedView;
                    GridView1.DataBind();
                    int count = sortedView.Count; //(資料總筆數)view.Count;
                    if (count == 0)
                    {
                        querytype.Text = "-99";
                        State1();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                        lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                        
                    }
                    else
                    {
                        querytype.Text = "1";
                        lblQueryResult.Text = "共" + count + "筆";
                        State2();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "2";
                        lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    }
                    sortedView.Dispose();
                    
                }
            }
            catch
            {
                querytype.Text = "-99";
                State1();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
            }
 
        }
        //最近1筆(querytype.Text = "2")
        protected void btnSearchLast1_Click(object sender, EventArgs e)
        {
            try
            {
                    string nrssql, nrssql1;
                    if (DropDownListIP.SelectedValue == "0")//此一住民
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1="ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1="ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1="ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//所有住民
                    {
                        if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                        {
                            if (RadioButtonList1.SelectedIndex == 0)
                            {
                                nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                      "WHERE (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else if (RadioButtonList1.SelectedIndex == 1)
                            {
                                nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                      "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else
                            {
                                nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                      "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                        }
                        else//有選擇特定區域和樓層
                        {
                            if (RadioButtonList1.SelectedIndex == 0)
                            {
                                nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                      "WHERE (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else if (RadioButtonList1.SelectedIndex == 1)
                            {
                                nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                      "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                            else
                            {
                                nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                      "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                      "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                            }
                        }
                    }
                    SqlDataSource3.SelectCommand = nrssql + nrssql1;
                    txt_sql.Text = nrssql;
                    txt_sql1.Text = nrssql1;
                    DataView sortedView = new DataView(BindGridView(nrssql + nrssql1));
                    GridView1.DataSource = sortedView;
                    GridView1.DataBind();
                    int count = sortedView.Count; //(資料總筆數)view.Count;
                    if (count == 0)
                    {
                        querytype.Text = "-99";
                        State1();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                        lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
                    }
                    else
                    {
                        querytype.Text = "2";
                        lblQueryResult.Text = "共" + count + "筆";
                        State2();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "2";
                        lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    }
                    sortedView.Dispose();

            }
            catch
            {
                querytype.Text = "-99";
                State1();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
            }
        }
        //最近5筆(querytype.Text = "3")
        protected void btnSearchLast5_Click(object sender, EventArgs e)
        {
            try
            {
                string nrssql, nrssql1;
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.AccountEnable = 'Y') ";
                        nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                        nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                        nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
                SqlDataSource3.SelectCommand = nrssql + nrssql1;
                txt_sql.Text = nrssql;
                txt_sql1.Text = nrssql1;
                DataView sortedView = new DataView(BindGridView(nrssql + nrssql1));
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
                int count = sortedView.Count; //(資料總筆數)view.Count;
                if (count == 0)
                {
                    querytype.Text = "-99";
                    State1();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
                }
                else
                {
                    querytype.Text = "3";
                    lblQueryResult.Text = "共" + count + "筆";
                    State2();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "2";
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }
                sortedView.Dispose();

            }
            catch
            {
                querytype.Text = "-99";
                State1();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
            }
        }
        //最近7日(querytype.Text = "4")
        protected void btnSearchLastDay7_Click(object sender, EventArgs e)
        {
            try
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Startdate = Enddate.AddDays(-7);
                string strsdate = Startdate.ToString("yyyy/MM/dd");

                string nrssql, nrssql1;
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ";
                                  nrssql1="ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                                  nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
                SqlDataSource3.SelectCommand = nrssql + nrssql1;
                txt_sql.Text = nrssql;
                txt_sql1.Text = nrssql1;
                DataView sortedView = new DataView(BindGridView(nrssql + nrssql1));
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
                int count = sortedView.Count; //(資料總筆數)view.Count;
                if (count == 0)
                {
                    querytype.Text = "-99";
                    State1();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
                }
                else
                {
                    querytype.Text = "4";
                    lblQueryResult.Text = "共" + count + "筆";
                    State2();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "2";
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }
                sortedView.Dispose();

            }
            catch
            {
                querytype.Text = "-99";
                State1();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
            }
        }
        //最近14日(querytype.Text = "5")
        protected void btnSearchLastDay14_Click(object sender, EventArgs e)
        {
            try
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Startdate = Enddate.AddDays(-14);
                string strsdate = Startdate.ToString("yyyy/MM/dd");

                string nrssql, nrssql1;
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ";
                        nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                        nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                        nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ";
                            nrssql1 = "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
                SqlDataSource3.SelectCommand = nrssql + nrssql1;
                txt_sql.Text = nrssql;
                txt_sql1.Text = nrssql1;
                DataView sortedView = new DataView(BindGridView(nrssql + nrssql1));
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
                int count = sortedView.Count; //(資料總筆數)view.Count;
                if (count == 0)
                {
                    querytype.Text = "-99";
                    State1();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
                }
                else
                {
                    querytype.Text = "5";
                    lblQueryResult.Text = "共" + count + "筆";
                    State2();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "2";
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }
                sortedView.Dispose();

            }
            catch
            {
                querytype.Text = "-99";
                State1();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查無此資料";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
            }
        }

        //分頁
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            /*
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = BindGridView(txt_sql.Text + txt_sql1.Text);
            GridView1.DataBind();
            if (querytype.Text == "-99")//查不到資料
            {
                State1();
            }
            else
            {
                if (querytype.Text == "0")//網頁載入初始查詢型態值
                {
                    State1();
                }
                else
                {
                    State2();
                }
            }
             */
            
            GridView1.PageIndex = e.NewPageIndex;
            DataView sortedView = new DataView(BindGridView(txt_sql.Text + txt_sql1.Text));
            GridView1.DataSource = sortedView;
            GridView1.DataBind();

            if (direction == SortDirection.Ascending)
            {
                sortImage.ImageUrl = "~/Image/WebImage/up.png";
                GridView1.HeaderRow.Cells[Convert.ToInt16(txt_columnIndex.Text)].Controls.Add(sortImage);
            }
            else
            {
                sortImage.ImageUrl = "~/Image/WebImage/down.png";
                GridView1.HeaderRow.Cells[Convert.ToInt16(txt_columnIndex.Text)].Controls.Add(sortImage);
            }

            if (querytype.Text == "-99")//查不到資料
            {
                State1();
            }
            else
            {
                if (querytype.Text == "0")//網頁載入初始查詢型態值
                {
                    State1();
                }
                else
                {
                    State2();
                }
            }
            
        }

        //排序
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string nrssql;
            if (querytype.Text == "0")
            {
                nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                         "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO "+
                         "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') "+
                         "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
            }
            else if (querytype.Text == "1")
            {
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
            }
            else if (querytype.Text == "2")
            {
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
            }
            else if (querytype.Text == "3")
            {
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT TOP 5 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
            }
            else if (querytype.Text == "4")
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Startdate = Enddate.AddDays(-7);
                string strsdate = Startdate.ToString("yyyy/MM/dd");
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
            }
            else if (querytype.Text == "5")
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Startdate = Enddate.AddDays(-14);
                string strsdate = Startdate.ToString("yyyy/MM/dd");
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//所有住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                    else//有選擇特定區域和樓層
                    {
                        if (RadioButtonList1.SelectedIndex == 0)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else if (RadioButtonList1.SelectedIndex == 1)
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                        else
                        {
                            nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                                  "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                                  "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + strsdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                                  "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                        }
                    }
                }
            }
            else//-99(查不到)
            {
                nrssql = "";
            }

            try
            {
                string sortingDirection = string.Empty;
                if (direction == SortDirection.Ascending)
                {
                    direction = SortDirection.Descending;
                    sortingDirection = "desc";
                    sortImage.ImageUrl = "~/Image/WebImage/down.png";
                }
                else
                {
                    direction = SortDirection.Ascending;
                    sortingDirection = "asc";
                    sortImage.ImageUrl = "~/Image/WebImage/up.png";
                }
                DataView sortedView = new DataView(BindGridView(nrssql));
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                GridView1.DataSource = sortedView;
                GridView1.DataBind();

                int columnIndex = 0;
                foreach (DataControlFieldHeaderCell headerCell in GridView1.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression == e.SortExpression)
                    {
                        columnIndex = GridView1.HeaderRow.Cells.GetCellIndex(headerCell);
                        txt_columnIndex.Text = columnIndex.ToString();
                    }
                }
                GridView1.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
                if (querytype.Text == "-99")//查不到資料
                {
                    State1();
                }
                else
                {
                    if (querytype.Text == "0")//網頁載入初始查詢型態值
                    {
                        State1();
                    }
                    else
                    {
                        State2();
                    }
                }
            }
            catch
            {
                State1();
            }

        }

        //列印查詢區間所有紀錄
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (DropDownListIP.SelectedValue == "0")//此一住民
            {
                printQAllforThisIP(txt_sql.Text, txt_sql1.Text);
            }
            else//所有住民
            {
                printQAllforAllIP(txt_sql.Text, txt_sql1.Text);
            }
        }
        private void printQAllforThisIP(string nrssql, string nrssql1)
        {
            string s_date = sqlTime.DateSplitSlash(txtstartdate.Text);
            string e_date = sqlTime.DateSplitSlash(txtenddate.Text);
            string ip_no = str_ip_no.Text;
            string connection_id = str_connection_id.Text;
            string print_time = sqlTime.hourminute();
            string print_user = sqlNursingRecordSimple.SearchA_user(str_account.Text, connection_id);
           
            /*
            if(querytype.Text.Equals("1")) //區間查詢
            {
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
            }
            else if(querytype.Text.Equals("2")) //最近一筆
            {
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                }
                else
                {
                    nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                }
            }
            else if (querytype.Text.Equals("3")) //最近五筆
            {
                if (RadioButtonList1.SelectedIndex == 0)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE A1.NO in(" + 
                          "SELECT TOP 5 A1.NO " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE A1.NO in(" + 
                         "SELECT TOP 5 A1.NO " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE A1.NO in(" + 
                        "SELECT TOP 5 A1.NO " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
            }
            else if (querytype.Text.Equals("4"))//最近七日
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                string Startdate = Enddate.AddDays(-7).ToString("yyyyMMdd");

                if (RadioButtonList1.SelectedIndex == 0)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
            }
            else if (querytype.Text.Equals("5")) //最近14日
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                string Startdate = Enddate.AddDays(-14).ToString("yyyyMMdd");

                if (RadioButtonList1.SelectedIndex == 0)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else if (RadioButtonList1.SelectedIndex == 1)
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
                else
                {
                    nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                          "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                          "WHERE (A1.IP_NO = '" + str_ip_no.Text + "') AND (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC";
                }
            }
            */

            DataTable dt_Nursing_Record_sample = sqlNursingRecordSimple.getNursingRecordThisIP(nrssql + nrssql1, connection_id);
            int rec_count = dt_Nursing_Record_sample.Rows.Count; //區間記錄筆數
            int total_cell = 8 * rec_count;//每一筆資料需要8個cell(好像用不到) 

            string ip_name = SqlMain.GetIPName(ip_no);
            string ip_sex = SqlMain.GetIPSex(ip_no).ToString() == "1" ? "男" : "女";
            int dob = Convert.ToInt32(sqlTime.cut(SqlMain.GetIPBirthday(ip_no).Trim()));
            int nowyear = Convert.ToInt32(sqlTime.time_year());
            string age = (nowyear - dob).ToString();
            age = age == "-1" ? "" : age;
            string room_bed = SqlMain.GetIPBed(ip_no);

            //DataTable dt_ip_info = sqlNursingRecordSimple.SearchIPInfo(ip_no, connection_id);
            //string ip_name = dt_ip_info.Rows[0][0].ToString();
            //string ip_sex = dt_ip_info.Rows[0][1].ToString() == "1" ? "男" : "女";
            //int dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0][2].ToString().Trim()));
            //int nowyear = Convert.ToInt32(sqlTime.time_year());
            //string age = (nowyear - dob).ToString();
            //age = age == "-1" ? "" : age;
            //string room_bed = dt_ip_info.Rows[0][3].ToString();



            try
            {
                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                //設定頁首頁尾
                //宣告檔案名稱
                string pdffilename = "NursingRecordSimpleInterval_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf";
                //讀取頁首尾格式參數
                string pagehttypeval = SettingXML.PrintPDFSetting.SearchPrintPageHTType(Server.MapPath("~/SettingXML/").ToString());
                switch (pagehttypeval.ToString().Trim())
                {
                    case "sqlPDFFooter":
                        sqlPDFFooter.Headercontent(pdffilename);
                        pdfWriter.PageEvent = new sqlPDFFooter();
                        break;
                    case "sqlPDFFooterV2":
                        sqlPDFFooterV2.Headercontent(pdffilename, ip_no, str_account.Text);
                        pdfWriter.PageEvent = new sqlPDFFooterV2();
                        break;
                    case "sqlPDFFooterV3":
                        sqlPDFFooterV3.Headercontent(pdffilename);
                        pdfWriter.PageEvent = new sqlPDFFooterV3();
                        break;
                    default:
                        break;
                }
                
                
                //sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                //pdfWriter.PageEvent = new sqlPDFFooter();
                
                //字型設定     
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\mingliu.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 10);
                iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
                iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font ChFont4 = new iTextSharp.text.Font(bfChinese, 12);

                Doc.Open();

                // 加入自動列印指令碼
                //pdfWriter.AddJavaScript(@"var pp = this.getPrintParams(); pp.interactive = pp.constants.interactionLevel.silent; pp.pageHandling = pp.constants.handling.none;
                //var fv = pp.constants.flagValues; pp.flags |= fv.setPageSize; pp.flags |= (fv.suppressCenter | fv.suppressRotate); this.print(pp);");
                if (Session["hp_name"] == null)
                {
                    HttpCookie myhp_name_Cookie = Request.Cookies["hp_name"];
                    if (myhp_name_Cookie != null)
                    {
                        Session["hp_name"] = myhp_name_Cookie.Values["hp_name"];
                    }
                }
                //Paragraph Title1 = new Paragraph("單位：" + Session["hp_name"], ChFont2);
                Paragraph Title2 = new Paragraph(Session["hp_name"] + "\n護理紀錄", ChFont2);
                //Paragraph Title3 = new Paragraph(lblPrintReviseDate.Text, ChFont);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                //Title3.Alignment = 2;//設定左右對齊 0:左 1:置中 2:右               
                PdfPTable title = new PdfPTable(5);
                title.SpacingBefore = 10f;
                title.WidthPercentage = 100;
                PdfPCell[] info = new PdfPCell[5];
                info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                info[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號：" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no), ChFont));
                info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));
   

                //Doc.Add(Title1);
                Doc.Add(Title2);
                //Doc.Add(Title3);
                //Paragraph Title = new Paragraph("\n", ChFont);
                //Doc.Add(Title);
                for (int i = 0; i < info.Length; i++)
                {
                    info[i].BorderWidth = 0;
                    title.AddCell(info[i]);

                }
                Doc.Add(title);

                PdfPTable table = new PdfPTable(new float[] { 1.5f, 5, 1 });
                table.WidthPercentage = 100; //一整頁
                table.SpacingBefore = 5;

                int count = 3 + dt_Nursing_Record_sample.Rows.Count * 3;
                PdfPCell[] cell = new PdfPCell[count];

                cell[0] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄日期/時間", ChFont3));
                cell[0].GrayFill = 0.9f;
                cell[1] = new PdfPCell(new iTextSharp.text.Paragraph("護理紀錄內容", ChFont3));
                cell[1].GrayFill = 0.9f;
                cell[2] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄人員", ChFont3));
                cell[2].GrayFill = 0.9f;

                for (int i = 0; i < rec_count; i++)
                {                
                    string r_date = sqlTime.DateAddSlash(dt_Nursing_Record_sample.Rows[i][2].ToString());
                    string r_time = dt_Nursing_Record_sample.Rows[i][3].ToString();
                    r_time = r_time.Substring(0, 2) + ":" + r_time.Substring(2, 2);
                    string r_user = dt_Nursing_Record_sample.Rows[i][5].ToString();
                    string content = dt_Nursing_Record_sample.Rows[i][4].ToString();
                    content = SpecialCharactersTransform.strtosc(content);
                    if (content.Trim() == "")
                    {
                        content = "***";
                    }

                    cell[3+i*3] =new PdfPCell(new iTextSharp.text.Phrase( r_date + " " + r_time, ChFont));
                    cell[4 + i * 3] = new PdfPCell(new iTextSharp.text.Phrase(content, ChFont));
                    cell[5 + i * 3] = new PdfPCell(new iTextSharp.text.Phrase(r_user, ChFont));
                }

                for (int j = 0; j < cell.Length; j++)
                {
                    if (cell[j] != null)
                    {
                        table.AddCell(cell[j]);
                    }
                }

                Doc.Add(table);
                Doc.Close();
                dt_Nursing_Record_sample.Dispose();
                //檔案下載*/
                Response.Clear();
                Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                //Response.Clear();
                ////Response.AddHeader("Content-Disposition", "attachment; filename=NursingRecordSimpleInterval_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + "_" + sqlTime.time() + ".pdf");
                //Response.AddHeader("Content-Disposition", "inline; filename="+pdffilename);
                ////Response.ContentType = "application/octet-stream";
                //Response.ContentType = "application/pdf";
                //Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                //Response.OutputStream.Flush();
                //Response.OutputStream.Close();
                //Response.Flush();
                Response.End();
            }
            catch (DocumentException de)
            {
                Response.Write(de.ToString());
            }
        }
        private void printQAllforAllIP(string nrssql, string nrssql1)
        {
            string s_date = sqlTime.DateSplitSlash(txtstartdate.Text);
            string e_date = sqlTime.DateSplitSlash(txtenddate.Text);
            string connection_id = str_connection_id.Text;
            string print_time = sqlTime.hourminute();
            string print_user = sqlNursingRecordSimple.SearchA_user(str_account.Text, connection_id);
            /*
            if (querytype.Text.Equals("1")) //區間查詢
            {
                if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
                else//有選擇特定區域和樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                          "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                          "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + txtstartdate.Text + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + txtenddate.Text + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                          "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
            }
            else if (querytype.Text.Equals("2")) //最近一筆
            {
                if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
                else//有選擇特定區域和樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                    else
                    {
                        nrssql = "SELECT TOP 1 A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                          "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC";
                    }
                }
            }
            else if (querytype.Text.Equals("3")) //最近五筆
            {
                if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE A1.NO in(" + 
                            "SELECT TOP 5 A1.NO " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE A1.NO in(" + 
                            "SELECT TOP 5 A1.NO " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO WHERE A1.NO in(" + 
                            "SELECT TOP 5 A1.NO " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
                else//有選擇特定區域和樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO WHERE A1.NO in(" + 
                            "SELECT TOP 5 A1.NO " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO WHERE A1.NO in(" +
                            "SELECT TOP 5 A1.NO " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO WHERE A1.NO in(" + 
                            "SELECT TOP 5 A1.NO " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.R_DATE DESC, A1.R_TIME DESC" + ") ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
            }
            else if (querytype.Text.Equals("4"))//最近七日
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                string Startdate = Enddate.AddDays(-7).ToString("yyyyMMdd");

                if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
                else//有選擇特定區域和樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
            }
            else if (querytype.Text.Equals("5")) //最近14日
            {
                string nowdate = sqlTime.time();
                string stredate = sqlTime.DateAddSlash(nowdate);
                DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                string Startdate = Enddate.AddDays(-14).ToString("yyyyMMdd");

                if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
                else//有選擇特定區域和樓層
                {
                    if (RadioButtonList1.SelectedIndex == 0)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else if (RadioButtonList1.SelectedIndex == 1)
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                    else
                    {
                        nrssql = "SELECT A1.NO, A1.IP_NO, A1.R_DATE, A1.R_TIME, A1.[CONTENT], A2.Name, A3.IP_NO_NEW, A1.ShiftExchange, A3.IP_NAME " +
                              "FROM NURSE_RECORD_SIMPLE AS A1 INNER JOIN EMP_LOGIN AS A2 ON A1.CREATE_USER = A2.Login INNER JOIN IP_INFORMATION AS A3 ON A1.IP_NO = A3.IP_NO LEFT JOIN ROOM ON A1.IP_NO = ROOM.IP_NO " +
                              "WHERE (A1.R_DATE BETWEEN CONVERT(varchar(8), CAST('" + Startdate + "' AS DateTime), 112) AND CONVERT(varchar(8), CAST('" + stredate + "' AS DateTime), 112)) AND (A1.ShiftExchange = '" + RadioButtonList1.SelectedValue.ToString() + "') AND (A1.AccountEnable = 'Y') AND ROOM.ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' " +
                              "ORDER BY A1.IP_NO ASC,A1.R_DATE ASC, A1.R_TIME ASC";
                    }
                }
            }
            */
            DataTable dt_Nursing_Record_sample_allip = sqlNursingRecordSimple.getNursingRecordAllIP(nrssql + nrssql1, connection_id);
            int rec_count = dt_Nursing_Record_sample_allip.Rows.Count; //區間記錄筆數
            int total_cell = 8 * rec_count;//每一筆資料需要8個cell(好像用不到)
            try
            {
                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                //設定頁首頁尾
                //宣告檔案名稱
                string pdffilename = "NursingRecordSimpleInterval_AllIP_" + sqlTime.time() + ".pdf";
                //讀取頁首尾格式參數
                string pagehttypeval = SettingXML.PrintPDFSetting.SearchPrintPageHTType(Server.MapPath("~/SettingXML/").ToString());
                switch (pagehttypeval.ToString().Trim())
                {
                    case "sqlPDFFooter":
                        sqlPDFFooter.Headercontent(pdffilename);
                        pdfWriter.PageEvent = new sqlPDFFooter();
                        break;
                    case "sqlPDFFooterV2"://這是所有住民列印, 故無法適用此格式
                        //sqlPDFFooterV2.Headercontent(pdffilename, ip_no, str_account.Text);
                        //pdfWriter.PageEvent = new sqlPDFFooterV2();
                        sqlPDFFooterV3.Headercontent(pdffilename);
                        pdfWriter.PageEvent = new sqlPDFFooterV3();
                        break;
                    case "sqlPDFFooterV3":
                        sqlPDFFooterV3.Headercontent(pdffilename);
                        pdfWriter.PageEvent = new sqlPDFFooterV3();
                        break;
                    default:
                        break;
                }

                //sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                //pdfWriter.PageEvent = new sqlPDFFooter();

                //字型設定     
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\mingliu.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 10);
                iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
                iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font ChFont4 = new iTextSharp.text.Font(bfChinese, 12);

                Doc.Open();

                // 加入自動列印指令碼
                //pdfWriter.AddJavaScript(@"var pp = this.getPrintParams(); pp.interactive = pp.constants.interactionLevel.silent; pp.pageHandling = pp.constants.handling.none;
                //var fv = pp.constants.flagValues; pp.flags |= fv.setPageSize; pp.flags |= (fv.suppressCenter | fv.suppressRotate); this.print(pp);");

                //Paragraph Title1 = new Paragraph("單位：" + Session["hp_name"], ChFont2);
                if (Session["hp_name"] == null)
                {
                    HttpCookie myhp_name_Cookie = Request.Cookies["hp_name"];
                    if (myhp_name_Cookie != null)
                    {
                        Session["hp_name"] = myhp_name_Cookie.Values["hp_name"];
                    }
                }
                Paragraph Title2 = new Paragraph(Session["hp_name"] + "\n護理紀錄", ChFont2);
                //Paragraph Title3 = new Paragraph(lblPrintReviseDate.Text, ChFont);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                //Title3.Alignment = 2;//設定左右對齊 0:左 1:置中 2:右
                Doc.Add(Title2);
                
              

                //int count = 3 + dt_Nursing_Record_sample_allip.Rows.Count * 3;
              

               

                //紀錄住民ip_no
                string tmpDate = "";

                for (int i = 0; i < rec_count; i++)
                {
                    string IP_NO = dt_Nursing_Record_sample_allip.Rows[i][1].ToString().Trim();
                    string r_date = sqlTime.DateAddSlash(dt_Nursing_Record_sample_allip.Rows[i][2].ToString());
                    string r_time = dt_Nursing_Record_sample_allip.Rows[i][3].ToString();
                    r_time = r_time.Substring(0, 2) + ":" + r_time.Substring(2, 2);
                    string r_user = dt_Nursing_Record_sample_allip.Rows[i][5].ToString();
                    string content = dt_Nursing_Record_sample_allip.Rows[i][4].ToString();
                    content = SpecialCharactersTransform.strtosc(content);
                    if (content.Trim() == "")
                    {
                        content = "***";
                    }

                    //不同住民資料
                    if (!tmpDate.Equals(IP_NO))
                    {         
                        string ip_no_new, ipname, ip_sex, age, room_bed;
                        SqlMain.Get_IP_Information_now(connection_id, IP_NO, out ip_no_new, out ipname, out ip_sex, out age, out room_bed);

                        PdfPTable title = new PdfPTable(5);
                        title.SpacingBefore = 10f;
                        title.WidthPercentage = 100;
                        PdfPCell[] info = new PdfPCell[5];
                        info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ipname, ChFont));
                        info[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號：" + ip_no_new, ChFont));
                        info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                        info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                        info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));

                        for (int j = 0; j < info.Length; j++)
                        {
                            info[j].BorderWidth = 0;
                            title.AddCell(info[j]);
                        }

                        Doc.Add(title);
                        tmpDate = IP_NO;

                        PdfPTable table = new PdfPTable(new float[] { 1.5f, 5, 1 });
                        table.WidthPercentage = 100; //一整頁
                        table.SpacingBefore = 5;

                        PdfPCell[] cell = new PdfPCell[6];
                        cell[0] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄日期/時間", ChFont3));
                        cell[0].GrayFill = 0.9f;
                        cell[1] = new PdfPCell(new iTextSharp.text.Paragraph("護理紀錄內容", ChFont3));
                        cell[1].GrayFill = 0.9f;
                        cell[2] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄人員", ChFont3));
                        cell[2].GrayFill = 0.9f;
                        
                        cell[3] = new PdfPCell(new iTextSharp.text.Phrase(r_date + " " + r_time, ChFont));
                        cell[4] = new PdfPCell(new iTextSharp.text.Phrase(content, ChFont));
                        cell[5] = new PdfPCell(new iTextSharp.text.Phrase(r_user, ChFont));

                        for (int j = 0; j < cell.Length; j++)
                        {
                            if (cell[j] != null)
                            {
                                table.AddCell(cell[j]);
                            }
                        }
                        Doc.Add(table);
                    }
                    else 
                    {
                        PdfPTable table = new PdfPTable(new float[] { 1.5f, 5, 1 });
                        table.WidthPercentage = 100; //一整頁

                        PdfPCell[] cell = new PdfPCell[3];
                        cell[0] = new PdfPCell(new iTextSharp.text.Phrase(r_date + " " + r_time, ChFont));
                        cell[1] = new PdfPCell(new iTextSharp.text.Phrase(content, ChFont));
                        cell[2] = new PdfPCell(new iTextSharp.text.Phrase(r_user, ChFont));

                        for (int j = 0; j < cell.Length; j++)
                        {
                            if (cell[j] != null)
                            {
                                table.AddCell(cell[j]);
                            }
                        }
                        Doc.Add(table);
                    }
                }

                //for (int j = 0; j < cell.Length; j++)
                //{
                //    if (cell[j] != null)
                //    {
                //        table.AddCell(cell[j]);
                //    }
                //}

               // Doc.Add(table);
                Doc.Close();
                dt_Nursing_Record_sample_allip.Dispose();
                //檔案下載*/
                Response.Clear();
                Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                //Response.Clear();
                ////Response.AddHeader("Content-Disposition", "attachment; filename=NursingRecordSimpleInterval_AllIP_" + sqlTime.time() + ".pdf");
                //Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
                ////Response.ContentType = "application/octet-stream";
                //Response.ContentType = "application/pdf";
                //Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                //Response.OutputStream.Flush();
                //Response.OutputStream.Close();
                //Response.Flush();
                Response.End();
            }
            catch (DocumentException de)
            {
                Response.Write(de.ToString());
            }
        }

        //檢視查詢區間所有紀錄
        protected void btnQTotalDetailView_Click(object sender, EventArgs e)
        {
            DataTable dt_Nursing_Record_sample = new DataTable();
            if (DropDownListIP.SelectedValue == "0")//此一住民
            {
                dt_Nursing_Record_sample = sqlNursingRecordSimple.getNursingRecordThisIP(txt_sql.Text+"ORDER BY A1.R_DATE ASC, A1.R_TIME ASC", connection_id);
            }
            else//所有住民
            {
                dt_Nursing_Record_sample = sqlNursingRecordSimple.getNursingRecordAllIP(txt_sql.Text + "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC", connection_id);
            }
            rp_detailTable.DataSource = dt_Nursing_Record_sample;
            rp_detailTable.DataBind();
            State2_1();
        }
        //列印檢視的結果
        protected void btnPrint2_1_Click(object sender, EventArgs e)
        {
            if (DropDownListIP.SelectedValue == "0")//此一住民
            {
                printQAllforThisIP(txt_sql.Text, "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC");
            }
            else//所有住民
            {
                printQAllforAllIP(txt_sql.Text, "ORDER BY A1.R_DATE ASC, A1.R_TIME ASC");
            }
        }

        //選擇某筆紀錄
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt_SqlRecord = sqlNursingRecordSimple.selRecord(GridView1.SelectedValue.ToString(), connection_id);
            /*
            try
            {
                DropDownList3.SelectedValue = dt_SqlRecord.Rows[0]["DIAG_ID"].ToString();
            }
            catch
            {
                DropDownList3.SelectedIndex = -1;
            }
            */
            //lblid.Text = DropDownList3.SelectedValue.ToString();
            //lblname.Text = DropDownList3.SelectedItem.Text;
            txtIPName.Text = dt_SqlRecord.Rows[0]["IP_NAME"].ToString();
            txtIPNo.Text = dt_SqlRecord.Rows[0]["IP_NO_NEW"].ToString();
            lblIPNO.Text = dt_SqlRecord.Rows[0]["IP_NO"].ToString();
            txtR_Date.Text = dt_SqlRecord.Rows[0]["R_DATE"].ToString().Substring(0, 4) + "/" + dt_SqlRecord.Rows[0]["R_DATE"].ToString().Substring(4, 2) + "/" + dt_SqlRecord.Rows[0]["R_DATE"].ToString().Substring(6, 2);
            txtR_Time.Text = dt_SqlRecord.Rows[0]["R_TIME"].ToString().Substring(0, 2) + ":" + dt_SqlRecord.Rows[0]["R_TIME"].ToString().Substring(2, 2);
            txtContent.Text = dt_SqlRecord.Rows[0]["CONTENT"].ToString();
            txtContent.Text = SpecialCharactersTransform.strtosc(txtContent.Text);
            txtCUserName.Text = sqlNursingRecordSimple.SearchA_user(dt_SqlRecord.Rows[0]["CREATE_USER"].ToString(), str_connection_id.Text);
            lblRecUserID.Text = dt_SqlRecord.Rows[0]["CREATE_USER"].ToString();

            dt_SqlRecord.Dispose();
            if (str_account.Text == lblRecUserID.Text)//登入者與記錄者一致
            {
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                {
                    State3();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                {
                    State4();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                {
                    State8();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                {
                    State6();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "6";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
            }
            else//登入者與記錄者不相同此登入者無法對此記錄進行修改或刪除
            {
                string modbyouseryn = SettingXML.UserAuth.SearchModbyOUserYN(Server.MapPath("~/SettingXML/").ToString());//是否允許別人修改
                string modbyalluser = SettingXML.UserAuth.SearchModbyAllUser(Server.MapPath("~/SettingXML/").ToString());//是否為所有任何人皆可修改別人的紀錄
                string delbyouseryn = SettingXML.UserAuth.SearchDelbyOUserYN(Server.MapPath("~/SettingXML/").ToString());//是否允許別人刪除
                string delbyalluser = SettingXML.UserAuth.SearchDelbyAllUser(Server.MapPath("~/SettingXML/").ToString());//是否為所有任何人皆可刪除別人的紀錄
                if (modbyouseryn == "N" && delbyouseryn == "N")
                {
                    State3();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                }
                else if (modbyouseryn == "N" && delbyouseryn == "Y")
                {
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                    {
                        State3();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                    {
                        State3();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                    {
                        State8();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                    {
                        State8();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                }
                else if (modbyouseryn == "Y" && delbyouseryn == "N")
                {
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                    {
                        State3();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                    {
                        State4();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                    {
                        State3();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                    if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                    {
                        State4();
                        tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                        //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                    }
                }
                else//Y,Y
                {
                    if (modbyalluser == "Y" && delbyalluser == "Y")
                    {
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                        {
                            State3();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                        {
                            State4();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                        {
                            State8();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                        {
                            State6();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "6";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                    }
                    else if (modbyalluser == "N" && delbyalluser == "Y")
                    {
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                        {
                            State3();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                        {
                            State3();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                        {
                            State8();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                        {
                            State8();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                    }
                    else if (modbyalluser == "Y" && delbyalluser == "N")
                    {
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                        {
                            State3();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                        {
                            State4();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                        {
                            State3();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                        if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                        {
                            State4();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                        }
                    }
                    else//N,N
                    {
                        int valuser = SettingXML.UserAuth.ValModbySUserID(Server.MapPath("~/SettingXML/").ToString(), Session["account"].ToString().Trim());//比對是否為可修改別人記錄的特定人
                        int valuser1 = SettingXML.UserAuth.ValDelbySUserID(Server.MapPath("~/SettingXML/").ToString(), Session["account"].ToString().Trim());//比對是否為可刪除別人記錄的特定人
                        if (valuser == 0 && valuser1 == 0)
                        {
                            State3();
                            tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                        }
                        else if (valuser == 0 && valuser1 == 1)
                        {
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                            {
                                State3();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                            {
                                State3();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                            {
                                State8();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                            {
                                State8();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                        }
                        else if (valuser == 1 && valuser1 == 0)
                        {
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                            {
                                State3();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                            {
                                State4();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                            {
                                State3();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                            {
                                State4();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                        }
                        else
                        {
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "200")//只能查詢
                            {
                                State3();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "3";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                            {
                                State4();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "202")//查刪
                            {
                                State8();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "8";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                            {
                                State6();
                                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "6";
                                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                            }
                        }
                    }
                }
            }
            MultiView1.ActiveViewIndex = 1;
        }

        //修改這一筆紀錄
        protected void Button5_Click(object sender, EventArgs e)
        {
            OnClickUpdateThisRec();
        }
        //按下修改這一筆交易後的狀態
        private void OnClickUpdateThisRec()
        {
            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")
            {
                State5();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "5";
            }
            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")
            {
                State7();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "7";
            }
        }
        //日期驗證
        protected void txtR_Date_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strR_Date = txtR_Date.Text.Substring(0, 4) + txtR_Date.Text.Substring(5, 2) + txtR_Date.Text.Substring(8, 2);
                int vsdate = Convert.ToInt32(strR_Date);
                string datewarning = DateTimeCheck.datevaild(strR_Date);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
        
                }
                else
                {
                    string testtime = sqlTime.time();
                    string testkeytime = txtR_Date.Text;
                    if (sqlTime.test_vaild_time(testtime, testkeytime).ToString() == "請勿輸入未來的日期")
                    {
                        //txtR_Date.Text = "";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "alert('請勿輸入未來的日期');", true);
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
   
            }
            OnClickUpdateThisRec();
            MultiView1.ActiveViewIndex = 1;
        }
        //時間驗證
        protected void txtR_Time_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strTime = txtR_Time.Text.Substring(0, 2) + txtR_Time.Text.Substring(3, 2);
                int vtime = Convert.ToInt32(strTime);
                string timewarning = DateTimeCheck.timevaild(strTime);
                if (timewarning == "時間格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);

                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);

            }
            OnClickUpdateThisRec();
            MultiView1.ActiveViewIndex = 1;
        }
        
        //刪除這一筆紀錄
        protected void Button6_Click(object sender, EventArgs e)
        {
            rec_no = GridView1.SelectedValue.ToString();
            op_user = str_account.Text;
            op_date = sqlTime.time();

            //Shadow處理
            ShadowNursingRecordSimple(rec_no, sqlTime.time(), sqlTime.hourminute(), op_user, "D");
            //刪除處理
            string warning = sqlNursingRecordSimple.delRecord(rec_no, op_user, op_date, str_connection_id.Text);
            if (warning == "刪除成功")
            {
                lblShowMsg.Text = "刪除成功";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "location.href='NursingRecordSimple_M.aspx'; runEffect();", true);
                DataView sortedView = new DataView(BindGridView(txt_sql.Text));
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
                GridView1.SelectedIndex = -1;
                int count = sortedView.Count; //(資料總筆數)
                if (count == 0)
                {
                    lblQueryResult.Text = "";
                    State1();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                }
                else
                {
                    lblQueryResult.Text = "共" + sortedView.Count.ToString() + "筆";
                    State2();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "2";
                }
                sortedView.Dispose();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('刪除失敗');", true);
                MultiView1.ActiveViewIndex = 1;
            }

        }
        
        //將這一筆紀錄帶入護理交班
        protected void Button8_Click(object sender, EventArgs e)
        {
             txtNurseExchangeTemp.Text = "護理紀錄(簡單版)\n";
             txtNurseExchangeTemp.Text += "紀錄日期:" + txtR_Date.Text + "\n";
             txtNurseExchangeTemp.Text += "紀錄時間:" + txtR_Time.Text + "\n";
             txtNurseExchangeTemp.Text += "紀錄人員:" + sqlNursingRecordSimple.SearchA_user(str_account.Text, str_connection_id.Text) + "\n";
             txtNurseExchangeTemp.Text += "個案狀況及處置:" + txtContent.Text + "\n";
             State9();
             tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "9";
             //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
        }
        //儲存護理交班
        protected void btnInsertNE_Click(object sender, EventArgs e)
        {
            string rec_no = GridView1.SelectedValue.ToString();
            connection_id = str_connection_id.Text;
            string datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            SqlConnection con = new SqlConnection(datasource);
            con.Open();
            string updateRecord = "update NURSE_RECORD_SIMPLE set ShiftExchange='Y' where NO='" + rec_no + "'";
            SqlCommand update1 = new SqlCommand(updateRecord, con);
            update1.ExecuteNonQuery();
            string cdate = sqlTime.time().Substring(0, 8);
            string ctime = sqlTime.datetime().Substring(8, 4);
            string warning;
            try
            {
                //string date = txtR_Date.Text.Substring(0, 4) + txtR_Date.Text.Substring(5, 2) + txtR_Date.Text.Substring(8, 2);
                //string time = txtR_Time.Text.Substring(0, 2) + txtR_Time.Text.Substring(3, 2);
                string insertRecord1 = "insert into SHIFT_EXCHANGE_RECORD(NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable) values ('" + rec_no + "','" + str_ip_no.Text + "',N'" + SpecialCharactersTransform.sctostr(txtNurseExchangeTemp.Text) + "','" + cdate + "','" + ctime + "','" + str_account.Text + "','" + cdate + "','" + ctime + "','" + str_account.Text + "','Y')";
                SqlCommand insert1 = new SqlCommand(insertRecord1, con);
                insert1.ExecuteNonQuery();
                warning = "新增護理交班成功";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                txtNurseExchangeTemp.Text = "";
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                {
                    State1();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                {
                    State1();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
            }
            catch
            {
                warning = "新增護理交班失敗";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                {
                    State4();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                {
                    State6();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "6";
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
                }
                MultiView1.ActiveViewIndex = 1;
            }
            finally
            {
                con.Close();
            }
            
        }
        //放棄護理交班
        protected void btnNurseExchangeClear_Click(object sender, EventArgs e)
        {
            txtNurseExchangeTemp.Text = "";
            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
            {
                State4();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
            }
            if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
            {
                State6();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "6";
                //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
            }
            MultiView1.ActiveViewIndex = 1;
        }
        //返回查詢
        protected void Button7_Click(object sender, EventArgs e)
        {
            DateTimeQueryShow();
            DataView sortedView = new DataView(BindGridView(txt_sql.Text));
            GridView1.DataSource = sortedView;
            GridView1.DataBind();
            GridView1.SelectedIndex = -1;
            int count = sortedView.Count; //(資料總筆數)
            if (count == 0)
            {
                lblQueryResult.Text = "";
                State1();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
            }
            else
            {
                lblQueryResult.Text = "共" + sortedView.Count.ToString() + "筆";
                State2();
                tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "2";
            }
            sortedView.Dispose();
            //State1();
            //tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "1";
            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_M";
        }

        private string DateCut(string txtdate)
        {
            string Rdate;
            try
            {
                Rdate = txtdate.Substring(0, 4) + txtdate.Substring(5, 2) + txtdate.Substring(8, 2);
            }
            catch
            {
                Rdate = "日期格式錯誤";
            }
            return Rdate;

        }
        private string TimeCut(string txttime)
        {
            string Rtime;
            try
            {
                Rtime = txttime.Substring(0, 2) + txttime.Substring(3, 2);
            }
            catch
            {
                Rtime = "時間格式錯誤";
            }
            return Rtime;
        }
        
        //儲存修改
        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            try
            {
                r_date = DateCut(txtR_Date.Text);//txtR_Date.Text.Substring(0, 4) + txtR_Date.Text.Substring(5, 2) + txtR_Date.Text.Substring(8, 2);
                r_time = TimeCut(txtR_Time.Text);//txtR_Time.Text.Substring(0, 2) + txtR_Time.Text.Substring(3, 2);
                int vrdate = Convert.ToInt32(r_date);
                string datewarning = DateTimeCheck.datevaild(r_date);
                int vrtime = Convert.ToInt32(r_time);
                string timewarning = DateTimeCheck.timevaild(r_time);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    if (timewarning == "時間格式錯誤")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);
                        MultiView1.ActiveViewIndex = 1;
                    }
                    else
                    {
                        try
                        {
                            string ip_no = lblIPNO.Text;
                            string content = SpecialCharactersTransform.sctostr(txtContent.Text);
                            //string qid = lblid.Text;
                            op_time = sqlTime.hourminute();
                            op_date = sqlTime.time();
                            op_user = str_account.Text;
                            rec_no = GridView1.SelectedValue.ToString();

                            //Shadow處理
                            ShadowNursingRecordSimple(rec_no, sqlTime.time(), sqlTime.hourminute(), op_user, "U");
                            //修改處理
                            //string warning = sqlNursingRecordSimple.modify(r_date, r_time, op_date, op_time, op_user, content, qid, rec_no, connection_id);
                            string warning = sqlNursingRecordSimple.modify(r_date, r_time, op_date, op_time, op_user, content, "", rec_no, str_connection_id.Text);
                            if (warning == "修改成功")
                            {
                                lblShowMsg.Text = "修改成功";
                                ScriptManager.RegisterClientScriptBlock(UpdatePanelMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                                //修改成功後的程序
                                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")//查修
                                {
                                    State4();
                                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "4";
                                }
                                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")//查刪修
                                {
                                    State6();
                                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "6";
                                }
                                MultiView1.ActiveViewIndex = 1;
                            }
                            else 
                            {
                                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "修改失敗" + "');", true);
                                MultiView1.ActiveViewIndex = 1;
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "修改失敗" + "');", true);
                            MultiView1.ActiveViewIndex = 1;
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期或時間格式錯誤" + "');", true);
                MultiView1.ActiveViewIndex = 1;
            }
        }

        //列印這筆紀錄
        protected void btnPrintThisRecord_Click(object sender, EventArgs e)
        {
            
                string no = GridView1.SelectedValue.ToString();
                string ip_no_print = lblIPNO.Text;//sqlNursingRecordSimple.Search_ip_no(txtIPName.Text, connection_id);

                string ip_name = SqlMain.GetIPName(ip_no_print);
                string ip_sex = SqlMain.GetIPSex(ip_no_print).ToString() == "1" ? "男" : "女";
                int dob = Convert.ToInt32(sqlTime.cut(SqlMain.GetIPBirthday(ip_no_print).Trim()));
                int nowyear = Convert.ToInt32(sqlTime.time_year());
                string age = (nowyear - dob).ToString();
                age = age == "-1" ? "" : age;
                string room_bed = SqlMain.GetIPBed(ip_no_print);

                string p_date = txtR_Date.Text;
                string p_time = txtR_Time.Text;
                //string nursin_ques = DropDownList3.SelectedValue;
                //string nursin_questext = DropDownList3.SelectedItem.Text;
                //if (nursin_questext.Trim() == "" || nursin_questext.Trim() == "請選擇")
                //{
                    //nursin_questext = "***";
                //}
                string content = txtContent.Text;
                if (content.Trim() == "")
                {
                    content = "***";
                }
                string print_time = sqlTime.hourminute();
                string p_user = sqlNursingRecordSimple.SearchA_user(str_account.Text, connection_id);
                
                //DataTable dt_ip_info = sqlNursingRecordSimple.SearchIPInfo(ip_no_print, connection_id);
                //string ip_name = dt_ip_info.Rows[0][0].ToString();
                //string ip_sex = dt_ip_info.Rows[0][1].ToString() == "1" ? "男" : "女";
                //int dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0][2].ToString().Trim()));
                //int nowyear = Convert.ToInt32(sqlTime.time_year());
                //string age = (nowyear - dob).ToString();
                //age = age == "-1" ? "" : age;
                //string room_bed = ip_sex = dt_ip_info.Rows[0][3].ToString();

                string print_user = sqlNursingRecordSimple.SearchA_user(str_account.Text, connection_id);
             try
             {
                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                //設定頁首頁尾
                //宣告檔案名稱
                string pdffilename = "NursingRecordSimple_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no_print) + ".pdf";
                //讀取頁首尾格式參數
                string pagehttypeval = SettingXML.PrintPDFSetting.SearchPrintPageHTType(Server.MapPath("~/SettingXML/").ToString());
                switch (pagehttypeval.ToString().Trim())
                {
                    case "sqlPDFFooter":
                        sqlPDFFooter.Headercontent(pdffilename);
                        pdfWriter.PageEvent = new sqlPDFFooter();
                        break;
                    case "sqlPDFFooterV2":
                        sqlPDFFooterV2.Headercontent(pdffilename, ip_no_print, str_account.Text);
                        pdfWriter.PageEvent = new sqlPDFFooterV2();
                        break;
                    case "sqlPDFFooterV3":
                        sqlPDFFooterV3.Headercontent(pdffilename);
                        pdfWriter.PageEvent = new sqlPDFFooterV3();
                        break;
                    default:
                        break;
                }

                //sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                //pdfWriter.PageEvent = new sqlPDFFooter();

                //字型設定     
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\mingliu.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 10);
                iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
                iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font ChFont4 = new iTextSharp.text.Font(bfChinese, 12);

                Doc.Open();

                // 加入自動列印指令碼
                pdfWriter.AddJavaScript(@"var pp = this.getPrintParams(); pp.interactive = pp.constants.interactionLevel.silent; pp.pageHandling = pp.constants.handling.none;
                var fv = pp.constants.flagValues; pp.flags |= fv.setPageSize; pp.flags |= (fv.suppressCenter | fv.suppressRotate); this.print(pp);");

                //Paragraph Title1 = new Paragraph("單位：" + Session["hp_name"], ChFont2);
                if (Session["hp_name"] == null)
                {
                    HttpCookie myhp_name_Cookie = Request.Cookies["hp_name"];
                    if (myhp_name_Cookie != null)
                    {
                        Session["hp_name"] = myhp_name_Cookie.Values["hp_name"];
                    }
                }
                Paragraph Title2 = new Paragraph(Session["hp_name"] + "\n護理紀錄", ChFont2);
                //Paragraph Title3 = new Paragraph(lblPrintReviseDate.Text, ChFont);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                //Title3.Alignment = 2;//設定左右對齊 0:左 1:置中 2:右
                PdfPTable title = new PdfPTable(5);
                title.SpacingBefore = 10f;
                title.WidthPercentage = 100;
                PdfPCell[] info = new PdfPCell[5];

                info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                info[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號：" + txtIPNo.Text, ChFont));
                info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));


                //Doc.Add(Title1);
                Doc.Add(Title2);
                //Doc.Add(Title3);
                //Paragraph Title = new Paragraph("\n", ChFont);
                //Doc.Add(Title);
                for (int i = 0; i < info.Length; i++)
                {
                    info[i].BorderWidth = 0;
                    title.AddCell(info[i]);
                }
                Doc.Add(title);
                PdfPTable table = new PdfPTable(new float[] { 1, 3, 0.6f });
                table.WidthPercentage = 100; //一整頁
                table.SpacingBefore = 5;

                PdfPCell[] cell = new PdfPCell[6];
                cell[0] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄日期/時間", ChFont3));
                cell[0].GrayFill = 0.9f;
                cell[1] = new PdfPCell(new iTextSharp.text.Paragraph("護理紀錄內容", ChFont3));
                cell[1].GrayFill = 0.9f;
                //cell[1].Colspan = 3;
                cell[2] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄人員", ChFont3));
                cell[2].GrayFill = 0.9f;
                cell[3] = new PdfPCell(new iTextSharp.text.Paragraph(p_date + " " + p_time, ChFont4));
                cell[4] = new PdfPCell(new iTextSharp.text.Paragraph(content, ChFont4));
                //cell[4].Colspan = 3;
                cell[5] = new PdfPCell(new iTextSharp.text.Paragraph(p_user, ChFont4));

                for (int i = 0; i < cell.Length; i++)
                {
                    table.AddCell(cell[i]);
                }
                Doc.Add(table);
                Doc.Close();
                Response.Clear();
                //Response.AddHeader("Content-Disposition", "attachment; filename=NursingRecordSimple_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + "_" + no +"_"+ sqlTime.time() + ".pdf");
                Response.AddHeader("Content-Disposition", "inline; filename="+ pdffilename);
                //Response.ContentType = "application/octet-stream";
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                Response.End();
            }
            catch (DocumentException de)
            {
                Response.Write(de.ToString());
            }
        }

        //清除
        protected void btnREWRITE_Click(object sender, EventArgs e)
        {
            
            //DropDownList3.SelectedIndex = -1;
            string nowdate = sqlTime.time();
            txtR_Date.Text = nowdate.Substring(0, 4) + "/" + nowdate.Substring(4, 2) + "/" + nowdate.Substring(6, 2);
            string nowtime = sqlTime.hourminute();
            txtR_Time.Text = nowtime.Substring(0, 2) + ":" + nowtime.Substring(2, 2);
            clearTXT();
            MultiView1.ActiveViewIndex = 1;
        }
        //清空文字盒與標籤內的文字
        protected void clearTXT()
        {
            txtContent.Text = "";
            //lblname.Text = "";
            //lblid.Text = "";
        }

        protected void LinkBtnGoToAddView_Click(object sender, EventArgs e)
        {
            Response.Redirect("NursingRecordSimple_A.aspx");
        }

        protected void LinkBtnGoToQUDView_Click(object sender, EventArgs e)
        {
            tab_state_NursingRecordSimple_M.Text = "20";
            Response.Redirect("NursingRecordSimple_M.aspx");
        }
        //片語:個案狀況及處置
        protected void ibtnContent_Click(object sender, ImageClickEventArgs e)
        {
            sqlPHRASE.connect(str_connection_id.Text);
            Phrase.sqlPHRASE.connect(str_connection_id.Text);
            //string phrase_user = str_account.Text;
            string pharsename = sqlPHRASE.getphrase("個案狀況及處置");
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename, "個案狀況及處置");
            string[,] phrase_item = sqlPHRASE.GetPhrase_item();
            TabPanel tabid = new TabPanel();
            //ToolTip toolTipchk = new ToolTip();
            // Set up the delays for the ToolTip.
            //toolTipchk.AutoPopDelay = 5000;
            //toolTipchk.InitialDelay = 1000;
            //toolTipchk.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            //toolTipchk.ShowAlways = true;
            tabid.HeaderText = pharsename;
            chklp[0] = new CheckBoxList();
            chklp[0].RepeatColumns = 5;
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal;
            chklp[0].RepeatLayout = RepeatLayout.Table;
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]);
                string value = SpecialCharactersTransform.strtospace(phrase_item[1, j]);
                value = SpecialCharactersTransform.strtosc(value);
                chklp[0].Items[j].Value = value;
                chklp[0].Items[j].Attributes["title"] = value;
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]);
            TabContainerPHRASE.Controls.Add(tabid);
            TabContainerPHRASE.ActiveTabIndex = selectvalue;

            btnSENT_Content.Visible = true;
            MultiView1.ActiveViewIndex = 1;
        }
        //選擇健康問題
        /*
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblname.Text = DropDownList3.SelectedItem.Text;
            lblid.Text = DropDownList3.SelectedValue;
            MultiView1.ActiveViewIndex = 1;
        }
        */
        

        //View1日期查詢顯示
        private void DateTimeQueryShow()
        {
            string nowdate = sqlTime.time();
            txtenddate.Text = sqlTime.DateAddSlash(nowdate);
            DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Startdate = Enddate.AddDays(-7);
            txtstartdate.Text = Startdate.ToString("yyyy/MM/dd");
        }
        
        //state 1的狀態
        private void State1()
        {
            btnSearch.Enabled = true;
            btnQTotalDetailView.Visible = false;
            btnPrint.Visible = false;
            lblQueryResult.Visible = false;
            GridView1.Visible = false;
            MultiView1.ActiveViewIndex = 0;
        }
        //state 2的狀態
        private void State2()
        {
            btnSearch.Enabled = true;
            btnQTotalDetailView.Visible = true;
            btnPrint.Visible = true;
            lblQueryResult.Visible = true;
            GridView1.SelectedIndex = -1;
            GridView1.Visible = true;
            MultiView1.ActiveViewIndex = 0;
        }
        //state 2-1的狀態
        private void State2_1()
        {
            btnPrint2_1.Visible = true;
            Button9.Visible = true;
            MultiView1.ActiveViewIndex = 2;
        }
        //state 3的狀態<只能查>
        private void State3()
        {
            Button5.Enabled = false;//修改這一筆
            Button6.Enabled = false;//刪除這一筆
            Button8.Enabled = false;//將這一筆紀錄帶入護理交班
            Button8.Visible = false;
            Button7.Enabled = true;//返回查詢
            Panel1.Enabled = false;//資料填寫區
            //PanelSymbolsTools.Visible = false;//符號表
            Panel5.Visible = false;//護理交班
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除
            
        }
        //state 4的狀態<查修>
        private void State4()
        {
            Button5.Enabled = true;//修改這一筆
            Button6.Enabled = false;//刪除這一筆
            //將這一筆紀錄帶入護理交班
            if (RadioButtonList1.SelectedIndex == 0)
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                Button8.Enabled = true;
                Button8.Visible = true;
            }
            else
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            Button7.Enabled = true;//返回查詢
            Panel1.Enabled = false;//資料填寫區
            //PanelSymbolsTools.Visible = false;//符號表
            Panel5.Visible = false;//護理交班
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除
            
        }
        //state 8的狀態<查刪>
        private void State8()
        {
            Button5.Enabled = false;//修改這一筆
            Button6.Enabled = true;//刪除這一筆
            Button8.Enabled = false;//將這一筆紀錄帶入護理交班
            Button8.Visible = false;
            Button7.Enabled = true;//返回查詢
            Panel1.Enabled = false;//資料填寫區
            //PanelSymbolsTools.Visible = false;//符號表
            Panel5.Visible = false;//護理交班
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除
        }
        //state 6的狀態<查修刪>
        private void State6()
        {
            Button5.Enabled = true;//修改這一筆
            Button6.Enabled = true;//刪除這一筆
            //將這一筆紀錄帶入護理交班
            if (RadioButtonList1.SelectedIndex == 0)
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                Button8.Enabled = true;
                Button8.Visible = true;
            }
            else
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            Button7.Enabled = true;//返回查詢
            Panel1.Enabled = false;//資料填寫區
            //PanelSymbolsTools.Visible = false;//符號表
            Panel5.Visible = false;//護理交班
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除
            
        }
        //state 5的狀態
        private void State5()
        {
            Button5.Enabled = false;//修改這一筆
            Button6.Enabled = false;//刪除這一筆
            //將這一筆紀錄帶入護理交班
            if (RadioButtonList1.SelectedIndex == 0)
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                Button8.Enabled = true;
                Button8.Visible = true;
            }
            else
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            Button7.Enabled = true;//返回查詢
            Panel1.Enabled = true;//資料填寫區
            //PanelSymbolsTools.Visible = false;//符號表
            Panel5.Visible = false;//護理交班
            btnSAVE.Enabled = true;//儲存
            btnPrintThisRecord.Enabled = false;//列印
            btnREWRITE.Enabled = true;//清除
            MultiView1.ActiveViewIndex = 1;
        }
        //state 7的狀態
        private void State7()
        {
            Button5.Enabled = false;//修改這一筆
            Button6.Enabled = true;//刪除這一筆
            //將這一筆紀錄帶入護理交班
            if (RadioButtonList1.SelectedIndex == 0)
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                Button8.Enabled = true;
                Button8.Visible = true;
            }
            else
            {
                Button8.Enabled = false;
                Button8.Visible = false;
            }
            Button7.Enabled = true;//返回查詢
            Panel1.Enabled = true;//資料填寫區
            //PanelSymbolsTools.Visible = false;//符號表
            Panel5.Visible = false;//護理交班
            btnSAVE.Enabled = true;//儲存
            btnPrintThisRecord.Enabled = false;//列印
            btnREWRITE.Enabled = true;//清除
            MultiView1.ActiveViewIndex = 1;
        }
        //state 9的狀態
        private void State9()
        {
            Button5.Enabled = false;//修改這一筆
            Button6.Enabled = false;//刪除這一筆
            Button8.Enabled = true;//將這一筆紀錄帶入護理交班
            Button8.Visible = true;
            Button7.Enabled = true;//返回查詢
            Panel1.Enabled = false;//資料填寫區
            //PanelSymbolsTools.Visible = false;//符號表
            Panel5.Visible = true;//護理交班
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = false;//列印
            btnREWRITE.Enabled = false;//清除
            MultiView1.ActiveViewIndex = 1;
        }
        //ShadowDB處理
        private void ShadowNursingRecordSimple(string recno, string OPing_DATE, string OPing_TIME, string OPing_USER, string OPing_STATE)
        {
            //先讀取原先資料
            DataTable dt_NursingRecordSimple = sqlNursingRecordSimple.SearchRecData(recno, str_connection_id.Text);
            string NO = dt_NursingRecordSimple.Rows[0]["NO"].ToString();
            string IP_NO = dt_NursingRecordSimple.Rows[0]["IP_NO"].ToString();
            string R_DATE = dt_NursingRecordSimple.Rows[0]["R_DATE"].ToString();
            string R_TIME = dt_NursingRecordSimple.Rows[0]["R_TIME"].ToString();
            string CONTENT = dt_NursingRecordSimple.Rows[0]["CONTENT"].ToString();
            string CREATE_DATE = dt_NursingRecordSimple.Rows[0]["CREATE_DATE"].ToString();
            string CREATE_TIME = dt_NursingRecordSimple.Rows[0]["CREATE_TIME"].ToString();
            string CREATE_USER = dt_NursingRecordSimple.Rows[0]["CREATE_USER"].ToString();
            string OP_DATE = dt_NursingRecordSimple.Rows[0]["OP_DATE"].ToString();
            string OP_TIME = dt_NursingRecordSimple.Rows[0]["OP_TIME"].ToString();
            string OP_USER = dt_NursingRecordSimple.Rows[0]["OP_USER"].ToString();
            string ShiftExchange = dt_NursingRecordSimple.Rows[0]["ShiftExchange"].ToString();
            string DIAG_ID = dt_NursingRecordSimple.Rows[0]["DIAG_ID"].ToString();
            dt_NursingRecordSimple.Dispose();
            //將資料新增至簡單版護理紀錄的Shadow
            sqlNursingRecordSimple objShadow = new sqlNursingRecordSimple();
            objShadow.addshadowNursingRecordSimple(NO, IP_NO, R_DATE, R_TIME, CONTENT, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER, ShiftExchange, DIAG_ID, OPing_DATE, OPing_TIME, OPing_USER, OPing_STATE, str_connection_id.Text);
            string warning = objShadow.GetShadowWarning();
            if (warning=="新增資料失敗")
            {
                if (OPing_STATE == "U")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "修改資料失敗" + "'); ", true);
                    //CurrentlyState();
                }
                else if (OPing_STATE == "D")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "刪除資料失敗" + "'); ", true);
                    //CurrentlyState();
                }

            }
            //string waring = sqlNursingRecordSimple.insertRecord(ipno, Rdate, Rtime, content, nowdate, nowtime, user, "N", QuesId, connection_id);


            //NO, IP_NO, R_DATE, R_TIME, CONTENT, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER, ShiftExchange, DIAG_ID

        }

        //GridView1欄位-日期格式轉換
        protected string DGFormatRIDDATE(string DATE)
        {
            return DATE.Substring(0, 4) + "/" + DATE.Substring(4, 2) + "/" + DATE.Substring(6, 2);
        }
        //GridView1欄位-時間格式轉換
        protected string DGFormatRIDTIME(string TIME)
        {
            return TIME.Substring(0, 2) + ":" + TIME.Substring(2, 2);
        }

        protected string DGFormatRIDCONTENT(string CONTENT)
        {
            return SpecialCharactersTransform.strtosc(CONTENT);
        }

        protected void btn_recent_Click(object sender, ImageClickEventArgs e)
        {
            sqlRecent.connect(str_connection_id.Text);
            int selectvalue = 1;
            TabContainer1.Visible = true;
            DataSet FormSet = sqlRecent.ReturnForm();

            TabPanel tabid = new TabPanel();
            tabid.HeaderText = "此住民最近一筆照護紀錄";
            chklp[0] = new CheckBoxList();
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Vertical;
            chklp[0].RepeatLayout = RepeatLayout.Table;

            System.Web.UI.WebControls.Label Description1 = new System.Web.UI.WebControls.Label();
            Description1.Text = "此住民目前無最近照護紀錄";
            Description1.ForeColor = System.Drawing.Color.Red;
            Description1.Font.Size = 10;
            Description1.Font.Bold = true;

            if (FormSet.Tables.Count > 0)
            {
                int total_count = 0;

                foreach (DataRow row in FormSet.Tables[0].Rows)
                {
                    DataSet Data_Set = sqlRecent.ReturnData(row["SELECT_COMM_COL"].ToString(), row["SELECT_COMM_TABLE"].ToString(), row["SELECT_COMM_ORDERBY_DATE"].ToString(), row["NO_STRING"].ToString(), ip_no, sqlTime.DateSplitSlash(txtR_Date.Text));
                    if (Data_Set.Tables[0].Rows.Count > 0)
                    {
                        string[] table_name = row["SELECT_COMM_COL_NAME"].ToString().Split(',');
                        string[] table = new string[table_name.Length];
                        string record = "●" + row["FORM_NAME"].ToString();
                        int count = 0;

                        for (int i = 0; i < table.Length; i++)
                        {
                            table[i] = Data_Set.Tables[0].Rows[0][i].ToString().Trim();
                            if (row["SELECT_COMM_ORDERBY_DATE"].ToString().Split(',').Length != 2)
                            {
                                if (i == 0)
                                {
                                    table[i] = sqlTime.DateAddSlash(table[i]);
                                    record += "【" + table_name[i] + ":" + table[i] + "】</br>";
                                }
                                else
                                {
                                    //判斷欄位是否有值
                                    if (!table[i].Trim().Equals(""))
                                    {
                                        record += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + table_name[i] + ":" + table[i] + "</br>";
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    table[i] = sqlTime.DateAddSlash(table[i]);
                                    record += "【" + table_name[i] + ":" + table[i] + ",";
                                }
                                else if (i == 1)
                                {
                                    table[i] = table[i].Substring(0, 2) + "：" + table[i].Substring(2, 2);
                                    record += table_name[i] + ":" + table[i] + "】</br>";
                                }
                                else
                                {
                                    //判斷欄位是否有值
                                    if (!table[i].Trim().Equals(""))
                                    {
                                        record += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + table_name[i] + ":" + table[i] + "</br>";
                                        count++;
                                    }
                                }

                            }
                            //if (table_name[i].Equals("紀錄日期")) { table[i]=sqlTime.DateAddSlash(table[i]); }
                            //else if (table_name[i].Equals("紀錄時間")) { table[i]=table[i].Substring(0, 2) + "：" + table[i].Substring(2, 2); }
                            //record += (!table[i].Trim().Equals("") ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + table_name[i] + ":" + table[i] + "</br>" : "");
                        }

                        if (count > 0)
                        {
                            chklp[0].Items.Add(record);
                            chklp[0].DataBind();
                            total_count++;
                        }

                    }
                    Data_Set.Dispose();
                }

                if (total_count <= 0)
                {
                    Description1.Visible = true;
                }
                else { Description1.Visible = false; }
            }
            FormSet.Dispose();

            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]);
            tabid.Controls.Add(Description1);
            TabContainer1.Controls.Add(tabid);
            TabContainer1.ActiveTabIndex = selectvalue;
            MultiView1.ActiveViewIndex = 1;
        }

        //開啟或關閉符號表
        protected void btnSymbolsTools_Click(object sender, EventArgs e)
        {
            if (PanelSymbolsTools.Visible == false)
            {
                PanelSymbolsTools.Visible = true;
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")
                {
                    State5();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "5";
                }
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")
                {
                    State7();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "7";
                }
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                PanelSymbolsTools.Visible = false;
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "201")
                {
                    State5();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "5";
                }
                if (tab_state_NursingRecordSimple_M.Text.Substring(0, 3) == "203")
                {
                    State7();
                    tab_state_NursingRecordSimple_M.Text = tab_state_NursingRecordSimple_M.Text.Substring(0, 3) + "7";
                }
                MultiView1.ActiveViewIndex = 1;
            }
        }

        

        
        /*
        //符號表內的各個按鈕按下後將按鈕的Text傳到TextBox
        protected void btn_AccPan_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button b = (System.Web.UI.WebControls.Button)sender;
            txtContent.Text += b.Text;
            MultiView1.ActiveViewIndex = 1;
        }
        */
        

        


        /*
        //返回查詢
        protected void Button4_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel3.Visible = false;
            GridView1.SelectedIndex = -1;
            GridView1.Visible = false;
            lblQueryResult.Text = "";

        }
         */
        
        
        
        
        /*
        protected void ckShiftExchange_CheckedChanged(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.CheckBox ckShiftExchange = (System.Web.UI.WebControls.CheckBox)sender;
            GridViewRow gvw = (GridViewRow)ckShiftExchange.NamingContainer;

            int ri = gvw.RowIndex;
            string rec_no = GridView1.DataKeys[ri].Value.ToString();
            string user = Session["account"].ToString();
            DataTable dt_content = sqlNursingRecordSimple.getContent(rec_no, connection_id);
            string ip_no = dt_content.Rows[0]["IP_NO"].ToString();
            string rdate = dt_content.Rows[0]["R_DATE"].ToString();
            string rtime = dt_content.Rows[0]["R_TIME"].ToString();
            string content = dt_content.Rows[0]["CONTENT"].ToString();          
           
            bool ins_success=false;
            if (ckShiftExchange.Checked)
            {
                ins_success = sqlNursingRecordSimple.updateShiftExchange("Y", rec_no, user, content, connection_id, rdate, rtime, ip_no);
            }
            else 
            {
                ins_success = sqlNursingRecordSimple.updateShiftExchange("N", rec_no, user, content, connection_id, rdate, rtime, ip_no);
            }

            //判斷是否有新增交班成功
            if (!ins_success) 
            {
                ckShiftExchange.Checked = false;
            }
        }
        
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
             foreach(GridViewRow row in GridView1.Rows)
             {
                 string rec_no = GridView1.DataKeys[row.RowIndex].Value.ToString();
                 bool check = sqlNursingRecordSimple.checkShiftExchange(rec_no, connection_id);
                 System.Web.UI.WebControls.CheckBox ckShiftExchange = (System.Web.UI.WebControls.CheckBox) row.FindControl("ckShiftExchange");
                 if (check)
                 {
                     ckShiftExchange.Checked = true;
                 }
                 else
                 {
                     ckShiftExchange.Checked = false;
                 }
             }
        }
*/
        

        

    }
}