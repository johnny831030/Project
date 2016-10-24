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

namespace longtermcare.NursingPlan.Shift_Exchange
{
    public partial class SHIFT_EXCHANGE_RECORD_View4 : System.Web.UI.Page
    {
        string connection_id = "";
        string ip_no = "";

        //離開網頁時清除狀態轉換的Session
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View4");
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["H_id"] == null)
            {
                HttpCookie myH_id_Cookie = Request.Cookies["H_id"];
                Session["H_id"] = myH_id_Cookie.Values["H_id"];
                connection_id = myH_id_Cookie.Values["H_id"];
                sstr_hid.Text = myH_id_Cookie.Values["H_id"];
            }
            else
            {
                connection_id = Session["H_id"].ToString();
                sstr_hid.Text = Session["H_id"].ToString();
            }
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = "30";
            if (Session["ipno"] != null)
            {
                sstr_ipno.Text = Session["ipno"].ToString();
                ip_no = Session["ipno"].ToString();
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
                this.Form.Attributes.Add("autocomplete", "off");
                //this.Page.Form.DefaultButton = "ContentPlaceHolder1$btnSearch";
                sqlShiftExchange.connect(sstr_hid.Text);
                SqlDataSource3.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
                SqlDataSource3.SelectCommand = "SELECT [AREA_STATE] FROM [CODE_ROOM_AREA] ORDER BY [AREA_ID] ASC";
                //權限檢查
                //查詢權限表此人在此表單的權限並寫入至session
                //無查詢權限
                //if(權限session==no query && Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"].ToString()=="30")
                //{
                //Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = "300";
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel5, typeof(UpdatePanel), "test", "alert('" + "您的權限在此無查詢權限" + "'); location.href='SHIFT_EXCHANGE_RECORD.aspx';", true);
                //}
                //有查詢權限
                //else if(權限session==only query && Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"].ToString()=="20")
                //{  
                Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = "301";
                //}
                State1();
                Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"].ToString() + "1";

                if (!IsPostBack)
                {
                    setDateControl();
                    //Bind GridView
                    //GridView1.DataSource = BindGridData(txtS_DATE.Text, txtE_DATE.Text, ip_no);
                    //GridView1.DataBind();               
                }
                //lblQueryResult.Visible = false;
                //GridView1.Visible = false;
                //btnPrint.Enabled = false;
                //交班紀錄新增
                //txtShowDate.Text = sqlTime.DateHaveSlash();
                //txtTime.Text = sqlTime.hourminute();
                //lblShowDate.Text = sqlTime.DateHaveSlash();
                //lblShowTime.Text = sqlTime.hourminute();
            }
            else
            {
                Response.Write("<script>alert('請先點選住民以便新增'); location.href='../../FNursingPlan.aspx'; </script>");
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('請先點選住民以便新增');location.href='../../FNursingPlan.aspx';", true);
            }
            
        }

        private void setDateControl()
        {
            string nowdate = sqlTime.DateHaveSlash();
            DateTime e_date = DateTime.ParseExact(nowdate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime s_date = e_date.AddDays(-1);
            txtS_DATE.Text = s_date.ToString("yyyy/MM/dd");
            txtE_DATE.Text = e_date.ToString("yyyy/MM/dd");
            //txtS_DATE0.Text = s_date.ToString("yyyy/MM/dd");
            //txtE_DATE0.Text = e_date.ToString("yyyy/MM/dd");
        }


        public DataTable BindGridData(DataTable dt_shift_exchange)//, string ip_no
        {
            int rowCount = dt_shift_exchange.Rows.Count;
            int colCount = dt_shift_exchange.Columns.Count;
            DataTable dt_date = dt_shift_exchange.DefaultView.ToTable(true, new string[] { "A_DATE" });
            //DataTable dt_ip_no = dt_shift_exchange.DefaultView.ToTable(true, new string[] { "IP_NO" });
            //var result_room_bed = from a in dt_shift_exchange.AsEnumerable()
            //                      orderby a.Field<string>("ROOM_AREA"), a.Field<string>("ROOM_BED")
            //                      select a.Field<string>("IP_NO").Distinct();
                                      
                                 
          
            DataTable dt_gvw = new DataTable();
            dt_gvw.Columns.Add("A_DATE");
            dt_gvw.Columns.Add("IP_NAME");
            dt_gvw.Columns.Add("ROOM_AREA");
            dt_gvw.Columns.Add("ROOM_BED");
            dt_gvw.Columns.Add("DAY_SHIFT");
            dt_gvw.Columns.Add("NIGHT_SHIFT");
            dt_gvw.Columns.Add("GRAVEYARD_SHIFT");

            foreach (DataRow t_date in dt_date.Rows)
            {
                var result_room_bed = (from a in dt_shift_exchange.AsEnumerable()
                                      where Convert.ToInt32(a.Field<string>("A_DATE")) == Convert.ToInt32(t_date.Field<string>("A_DATE"))
                                      orderby a.Field<string>("ROOM_AREA"), a.Field<string>("ROOM_BED")
                                      select new { ip_no = a.Field<string>("IP_NO"),  ip_name = a.Field<string>("IP_NAME"),  ip_bed = a.Field<string>("ROOM_BED"), ip_room_area = a.Field<string>("ROOM_AREA") }).Distinct();
                int o = result_room_bed.Count();
                 if (result_room_bed.Count() > 0)
                 {
                     foreach (var row_ip_no in result_room_bed)
                     {
                         string room_area = "";
                         string name = "";
                         string bed = "";
                         var result_name = from a in dt_shift_exchange.AsEnumerable()
                                           where a.Field<string>("IP_NO") == row_ip_no.ip_no
                                           select new
                                           {
                                               ip_name = a.Field<string>("IP_NAME"),
                                               ip_bed = a.Field<string>("ROOM_BED"),
                                               ip_room_area = a.Field<string>("ROOM_AREA")
                                           };

                         name = row_ip_no.ip_name;
                         bed = row_ip_no.ip_bed;
                         room_area = row_ip_no.ip_room_area;
                         DataRow row_dt_gvw = dt_gvw.NewRow();

                         var result = from a in dt_shift_exchange.AsEnumerable()
                                      where (Convert.ToInt32(a.Field<string>("A_DATE")) == Convert.ToInt32(t_date.Field<string>("A_DATE"))) && (Convert.ToInt32(a.Field<string>("A_TIME")) >= Convert.ToInt32("0000") && Convert.ToInt32(a.Field<string>("A_TIME")) <= Convert.ToInt32("0800")) && a.Field<string>("IP_NO") == row_ip_no.ip_no
                                      select a.Field<string>("NRS_CONTENT");

                         var result1 = from a in dt_shift_exchange.AsEnumerable()
                                       where (Convert.ToInt32(a.Field<string>("A_DATE")) == Convert.ToInt32(t_date.Field<string>("A_DATE"))) && (Convert.ToInt32(a.Field<string>("A_TIME")) >= Convert.ToInt32("0801") && Convert.ToInt32(a.Field<string>("A_TIME")) <= Convert.ToInt32("1600")) && a.Field<string>("IP_NO") == row_ip_no.ip_no
                                       select a.Field<string>("NRS_CONTENT");


                         var result2 = from a in dt_shift_exchange.AsEnumerable()
                                       where (Convert.ToInt32(a.Field<string>("A_DATE")) == Convert.ToInt32(t_date.Field<string>("A_DATE"))) && (Convert.ToInt32(a.Field<string>("A_TIME")) >= Convert.ToInt32("1601") && Convert.ToInt32(a.Field<string>("A_TIME")) <= Convert.ToInt32("2359")) && a.Field<string>("IP_NO") == row_ip_no.ip_no
                                       select a.Field<string>("NRS_CONTENT");


                         string content_day = "";
                         string content_night = "";
                         string content_graveyard = "";
                         foreach (string s in result1)
                         {
                             content_day += "● " + SpecialCharactersTransform.strtosc(s) + "<br />";
                         }
                         foreach (string s in result2)
                         {
                             content_night += "● " + SpecialCharactersTransform.strtosc(s) + "<br />";
                         }
                         foreach (string s in result)
                         {
                             content_graveyard += "● " + SpecialCharactersTransform.strtosc(s) + "<br />";
                         }

                         if (content_day != "" || content_night != "" || content_graveyard != "")
                         {
                             row_dt_gvw["A_DATE"] = t_date.Field<string>("A_DATE");
                             row_dt_gvw["IP_NAME"] = name;
                             row_dt_gvw["ROOM_AREA"] = room_area;
                             row_dt_gvw["ROOM_BED"] = bed;
                             row_dt_gvw["DAY_SHIFT"] = content_day;
                             row_dt_gvw["NIGHT_SHIFT"] = content_night;
                             row_dt_gvw["GRAVEYARD_SHIFT"] = content_graveyard;
                             dt_gvw.Rows.Add(row_dt_gvw);
                         }
                     }
                 }
            }
            int count = dt_gvw.Rows.Count;
            return dt_gvw;

        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string NowSE = ViewState["NowSE"] != null ? ViewState["NowSE"].ToString() : string.Empty; //現在排序欄位
            SortDirection NowSD = ViewState["NowSD"] != null ? (SortDirection)ViewState["NowSD"] : SortDirection.Ascending;

            if (string.IsNullOrEmpty(NowSE))
            {
                NowSE = e.SortExpression;
                NowSD = SortDirection.Ascending;
            }
            if (NowSE != e.SortExpression)
            {
                NowSE = e.SortExpression;
                NowSD = SortDirection.Ascending;
            }
            else
            {
                if (NowSD == SortDirection.Ascending)
                {
                    NowSD = SortDirection.Descending;
                }
                else
                {
                    NowSD = SortDirection.Ascending;
                }
            }

            ViewState["NowSD"] = NowSD;
            ViewState["NowSE"] = NowSE;

            this.GVGetData(NowSD, NowSE);
            GridView1.Visible = true;
        }

        private void GVGetData(SortDirection pSortDirection, string pSortExpression)
        {
            string s_date = sqlTime.DateSplitSlash(txtS_DATE.Text);
            string e_date = sqlTime.DateSplitSlash(txtE_DATE.Text);
            DataTable dt_shift_exchange;
            if(DropDownListIP.SelectedValue == "0")//此一住民
            {
                dt_shift_exchange = sqlShiftExchange.getShiftExchange(s_date, e_date, sstr_ipno.Text, sstr_hid.Text);
            }
            else
            {
               if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
               {
                   dt_shift_exchange = sqlShiftExchange.getShiftExchange_All_IP_All_Area(s_date, e_date, sstr_hid.Text);
               }
               else
               {
                   dt_shift_exchange = sqlShiftExchange.getShiftExchange_All_IP_Some_Area(s_date, e_date, DropDownListArea.SelectedValue.ToString(), sstr_hid.Text);
               }
            }
            DataTable dt_gv_show = BindGridData(dt_shift_exchange);//, ip_no
            
            string sSort = string.Empty;

            
            if (pSortDirection == SortDirection.Ascending)
            {
                if (pSortExpression == "A_DATE")
                {
                    sSort = pSortExpression + ", ROOM_AREA ,ROOM_BED";
                }
                else
                {
                    sSort = pSortExpression + ", A_DATE DESC";
                }
            }
            else 
            {
                if (pSortExpression == "A_DATE")
                {
                    sSort = pSortExpression + " DESC, ROOM_AREA ,ROOM_BED";
                }
                else
                {
                    sSort = pSortExpression + " DESC, A_DATE";
                }
            }

            
            DataView oDV = dt_gv_show.DefaultView;
            oDV.Sort = sSort;
            GridView1.DataSource = oDV;
            GridView1.DataBind();
            dt_shift_exchange.Dispose();
            dt_gv_show.Dispose();
            oDV.Dispose();
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

        //日期驗證(javascript 取代)
        protected void txtS_DATE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strSDate = txtS_DATE.Text.Substring(0, 4) + txtS_DATE.Text.Substring(5, 2) + txtS_DATE.Text.Substring(8, 2);
                int vsdate = Convert.ToInt32(strSDate);
                string datewarning = DateTimeCheck.datevaild(strSDate);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
            }
        }
        protected void txtE_DATE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strEDate = txtE_DATE.Text.Substring(0, 4) + txtE_DATE.Text.Substring(5, 2) + txtE_DATE.Text.Substring(8, 2);
                int vedate = Convert.ToInt32(strEDate);
                string datewarning = DateTimeCheck.datevaild(strEDate);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
            }
        }

        //查詢
        protected void btnSearch_nonNRS_Click(object sender, EventArgs e)
        {
            string s_date = sqlTime.DateSplitSlash(txtS_DATE.Text);
            string e_date = sqlTime.DateSplitSlash(txtE_DATE.Text);
            try
            {
                int vsdate = Convert.ToInt32(s_date);
                string datewarning = DateTimeCheck.datevaild(s_date);
                int vedate = Convert.ToInt32(e_date);
                string datewarning1 = DateTimeCheck.datevaild(e_date);
                if (datewarning == "日期格式錯誤")
                {
                    lblShowErrMsg.Text = "日期格式錯誤";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                }
                else
                {
                  if (datewarning1 == "日期格式錯誤")
                  {
                      lblShowErrMsg.Text = "日期格式錯誤";
                      ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                      //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                  }
                  else
                  {
                      DataTable dt_shift_exchange;// 
                      if (DropDownListIP.SelectedValue == "0")//此一住民
                      {
                          dt_shift_exchange = sqlShiftExchange.getShiftExchange(s_date, e_date, sstr_ipno.Text, sstr_hid.Text);
                      }
                      else//全部住民
                      {
                          if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                          {
                              dt_shift_exchange = sqlShiftExchange.getShiftExchange_All_IP_All_Area(s_date, e_date, sstr_hid.Text);
                          }
                          else//有選擇特定區域和樓層
                          {
                              dt_shift_exchange = sqlShiftExchange.getShiftExchange_All_IP_Some_Area(s_date, e_date, DropDownListArea.SelectedValue.ToString(), sstr_hid.Text);
                          }
                      }
                      
                      GridView1.DataSource = BindGridData(dt_shift_exchange);//, ip_no
                      int count = dt_shift_exchange.Rows.Count;
                      //GridView2.DataSource = BindGridData(dt_shift_exchange);//, ip_no
                      dt_shift_exchange.Dispose();
                      GridView1.DataBind();
                      //GridView2.DataBind();
                      if (count == 0)
                      {
                          lblQueryResult.Visible = false;
                          GridView1.Visible = false;
                          btnPrint.Enabled = false;
                          lblShowMsg.Text = "查無此資料";
                          ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                          //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "查不到資料，請重新設定查詢條件值" + "');", true);
                      }
                      else
                      {
                          lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                          ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                          lblQueryResult.Text = "共" + count.ToString() + "筆";
                          State2();
                          Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"].ToString().Substring(0, 3) + "2";
                          //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_SHIFT_EXCHANGE_RECORD_View4";

                      }
                  }
                }
            }
            catch
            {
                lblShowErrMsg.Text = "日期格式錯誤";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
            } 
        }
        //列印查詢後的結果
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string s_date = sqlTime.DateSplitSlash(txtS_DATE.Text);
            string e_date = sqlTime.DateSplitSlash(txtE_DATE.Text);
            string connection_id = sstr_hid.Text;
            string print_time = sqlTime.hourminute();
            string print_user = sqlShiftExchange.SearchA_user(str_account.Text, connection_id);
            string ip_no = sstr_ipno.Text;
            string ip_name;
            string ip_sex;
            int dob = 0;
            string age;
            string room_bed;

            /*
            DataTable dt_Shift_Exchange_Record;
            if (DropDownListIP.SelectedValue == "0")//此一住民
            {
                ip_no = Session["ipno"].ToString();
                ip_name = SqlMain.GetIPName(ip_no);
                ip_sex = SqlMain.GetIPSex(ip_no).ToString() == "1" ? "男" : "女";
                dob = Convert.ToInt32(sqlTime.cut(SqlMain.GetIPBirthday(ip_no).Trim()));
                int nowyear = Convert.ToInt32(sqlTime.time_year());
                age = (nowyear - dob).ToString();
                age = age == "-1" ? "" : age;
                room_bed = SqlMain.GetIPBed(ip_no);

                //DataTable dt_ip_info = sqlShiftExchange.SearchIPInfo(ip_no, connection_id);
                //ip_name = dt_ip_info.Rows[0][0].ToString();
                //ip_sex = dt_ip_info.Rows[0][1].ToString() == "1" ? "男" : "女";
                //dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0][2].ToString().Trim()));
                //int nowyear = Convert.ToInt32(sqlTime.time_year());
                //age = (nowyear - dob).ToString();
                //age = age == "-1" ? "" : age;
                //room_bed = dt_ip_info.Rows[0][3].ToString();
                dt_Shift_Exchange_Record = sqlShiftExchange.getShiftExchange(s_date, e_date, ip_no, connection_id);
            }
            else//全部住民
            {
                ip_no = "";
                ip_name = "";
                ip_sex = "";
                age = "";
                room_bed = "";
                if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                {
                    dt_Shift_Exchange_Record = sqlShiftExchange.getShiftExchange_All_IP_All_Area(s_date, e_date, connection_id);
                }
                else//有選擇特定區域和樓層
                {
                    dt_Shift_Exchange_Record = sqlShiftExchange.getShiftExchange_All_IP_Some_Area(s_date, e_date, DropDownListArea.SelectedValue.ToString(), connection_id);
                }
            }
            int rec_count = dt_Shift_Exchange_Record.Rows.Count; //區間記錄筆數
            */

            try
            {
                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                //設定頁首頁尾
                //宣告檔案名稱
                string pdffilename = "";
                if (DropDownListIP.SelectedValue == "0")
                {
                    pdffilename = "TotalShiftExchRecord_" + s_date + "_" + e_date + "_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf";
                }
                else
                {
                    if (DropDownListArea.SelectedValue == "-99")
                    {
                        pdffilename = "TotalShiftExchRecord_All_IP_" + s_date + "_" + e_date + ".pdf";
                    }
                    else
                    {
                        pdffilename = "TotalShiftExchRecord_Some_IP_in_" + DropDownListArea.SelectedValue.ToString() +"_"+ s_date + "_" + e_date + ".pdf";
                    }
                }
                //string pdffilename = "NursingRecordInterval_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + "_" + sqlTime.time() + ".pdf";
                sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                pdfWriter.PageEvent = new sqlPDFFooter();

                //字型設定     
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 10);
                iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
                iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 8);
                iTextSharp.text.Font ChFont4 = new iTextSharp.text.Font(bfChinese, 10, iTextSharp.text.Font.BOLD);

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
                Paragraph Title2 = new Paragraph(Session["hp_name"] + "\n交班記錄", ChFont2);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右

                DataTable dt_Shift_Exchange_Record;
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    ip_name = SqlMain.GetIPName(ip_no);
                    ip_sex = SqlMain.GetIPSex(ip_no).ToString() == "1" ? "男" : "女";
                    dob = Convert.ToInt32(sqlTime.cut(SqlMain.GetIPBirthday(ip_no).Trim()));
                    int nowyear = Convert.ToInt32(sqlTime.time_year());
                    age = (nowyear - dob).ToString();
                    age = age == "-1" ? "" : age;
                    room_bed = SqlMain.GetIPBed(ip_no);

                    //DataTable dt_ip_info = sqlShiftExchange.SearchIPInfo(ip_no, connection_id);
                    //ip_name = dt_ip_info.Rows[0][0].ToString();
                    //ip_sex = dt_ip_info.Rows[0][1].ToString() == "1" ? "男" : "女";
                    //dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0][2].ToString().Trim()));
                    //int nowyear = Convert.ToInt32(sqlTime.time_year());
                    //age = (nowyear - dob).ToString();
                    //age = age == "-1" ? "" : age;
                    //room_bed = dt_ip_info.Rows[0][3].ToString();
                    dt_Shift_Exchange_Record = sqlShiftExchange.getShiftExchange(s_date, e_date, ip_no, connection_id);

                    PdfPTable title = new PdfPTable(5);
                    title.SpacingBefore = 10f;
                    title.WidthPercentage = 100;
                    PdfPCell[] info = new PdfPCell[8];
                    info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                    info[1] = new PdfPCell(new iTextSharp.text.Phrase("家字號：" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no), ChFont));
                    info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                    info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                    info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));
                    info[5] = new PdfPCell(new iTextSharp.text.Phrase("列印人員：" + print_user, ChFont));
                    info[6] = new PdfPCell(new iTextSharp.text.Phrase("列印日期：" + sqlTime.DateAddSlash(sqlTime.time()), ChFont));
                    info[7] = new PdfPCell(new iTextSharp.text.Phrase("列印時間：" + print_time.Substring(0, 2) + ":" + print_time.Substring(2, 2), ChFont));

                    Doc.Add(Title2);
                    //Paragraph Title = new Paragraph("\n", ChFont2);
                    //Doc.Add(Title);
                    for (int i = 0; i < info.Length; i++)
                    {
                        info[i].BorderWidth = 0;
                        if (i == 5) info[i].Colspan = 2;
                        if (i == 6) info[i].Colspan = 2;
                        title.AddCell(info[i]);

                    }
                    Doc.Add(title);

                }
                else//全部住民
                {
                    ip_no = "";
                    ip_name = "";
                    ip_sex = "";
                    age = "";
                    room_bed = "";
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        dt_Shift_Exchange_Record = sqlShiftExchange.getShiftExchange_All_IP_All_Area(s_date, e_date, connection_id);
                    }
                    else//有選擇特定區域和樓層
                    {
                        dt_Shift_Exchange_Record = sqlShiftExchange.getShiftExchange_All_IP_Some_Area(s_date, e_date, DropDownListArea.SelectedValue.ToString(), connection_id);
                    }

                    PdfPTable title = new PdfPTable(3);
                    title.SpacingBefore = 10f;
                    title.WidthPercentage = 100;
                    PdfPCell[] info = new PdfPCell[3];
                    info[0] = new PdfPCell(new iTextSharp.text.Phrase("列印人員：" + print_user, ChFont));
                    info[1] = new PdfPCell(new iTextSharp.text.Phrase("列印日期：" + sqlTime.DateAddSlash(sqlTime.time()), ChFont));
                    info[2] = new PdfPCell(new iTextSharp.text.Phrase("列印時間：" + print_time.Substring(0, 2) + ":" + print_time.Substring(2, 2), ChFont));

                    Doc.Add(Title2);
                    //Paragraph Title = new Paragraph("\n", ChFont2);
                    //Doc.Add(Title);
                    for (int i = 0; i < info.Length; i++)
                    {
                        info[i].BorderWidth = 0;
                        title.AddCell(info[i]);

                    }
                    Doc.Add(title);

                }
                int rec_count = dt_Shift_Exchange_Record.Rows.Count; //區間記錄筆數

                

                PdfPTable table = new PdfPTable(new float[] { 1, 1, 1, 1, 1, 1, 1, 1 });
                table.WidthPercentage = 100; //一整頁
                table.SpacingBefore = 5;
                PdfPCell[] tabheadcell = new PdfPCell[6];
                tabheadcell[0] = new PdfPCell(new iTextSharp.text.Phrase("建立日期", ChFont));
                tabheadcell[0].GrayFill = 0.9f;
                tabheadcell[0].HorizontalAlignment = Element.ALIGN_CENTER;
                tabheadcell[0].VerticalAlignment = Element.ALIGN_CENTER;
                tabheadcell[1] = new PdfPCell(new iTextSharp.text.Phrase("建立時間", ChFont));
                tabheadcell[1].GrayFill = 0.9f;
                tabheadcell[1].HorizontalAlignment = Element.ALIGN_CENTER;
                tabheadcell[1].VerticalAlignment = Element.ALIGN_CENTER;
                tabheadcell[2] = new PdfPCell(new iTextSharp.text.Phrase("建立人員", ChFont));
                tabheadcell[2].GrayFill = 0.9f;
                tabheadcell[2].HorizontalAlignment = Element.ALIGN_CENTER;
                tabheadcell[2].VerticalAlignment = Element.ALIGN_CENTER;
                tabheadcell[3] = new PdfPCell(new iTextSharp.text.Phrase("住民", ChFont));
                tabheadcell[3].GrayFill = 0.9f;
                tabheadcell[3].HorizontalAlignment = Element.ALIGN_CENTER;
                tabheadcell[3].VerticalAlignment = Element.ALIGN_CENTER;
                tabheadcell[4] = new PdfPCell(new iTextSharp.text.Phrase("區域或樓層", ChFont));
                tabheadcell[4].GrayFill = 0.9f;
                tabheadcell[4].HorizontalAlignment = Element.ALIGN_CENTER;
                tabheadcell[4].VerticalAlignment = Element.ALIGN_CENTER;
                tabheadcell[5] = new PdfPCell(new iTextSharp.text.Phrase("交班內容", ChFont));
                tabheadcell[5].GrayFill = 0.9f;
                tabheadcell[5].HorizontalAlignment = Element.ALIGN_CENTER;
                tabheadcell[5].VerticalAlignment = Element.ALIGN_CENTER;
                tabheadcell[5].Colspan = 3;
                for (int j = 0; j < tabheadcell.Length; j++)
                {
                    if (tabheadcell[j] != null)
                    {
                        table.AddCell(tabheadcell[j]);
                    }
                }
                PdfPCell[,] cell = new PdfPCell[rec_count, 6];
                for (int i = 0; i < rec_count; i++)
                {
                    /*
                     * SHIFT_EXCHANGE_RECORD.NRS_NO, 0
                     * SHIFT_EXCHANGE_RECORD.IP_NO, 1
                     * SHIFT_EXCHANGE_RECORD.NRS_CONTENT, 2
                     * SHIFT_EXCHANGE_RECORD.A_DATE, 3
                     * SHIFT_EXCHANGE_RECORD.A_TIME, 4
                     * SHIFT_EXCHANGE_RECORD.A_USER, 5
                     * SHIFT_EXCHANGE_RECORD.CREATE_DATE, 6
                     * SHIFT_EXCHANGE_RECORD.CREATE_TIME, 7
                     * SHIFT_EXCHANGE_RECORD.CREATE_USER, 8
                     * SHIFT_EXCHANGE_RECORD.AccountEnable, 9
                     * IP_INFORMATION.IP_NO_NEW, 10
                     * IP_INFORMATION.IP_NAME, 11
                     * ROOM_AREA, 12
                     * ROOM_BED, 13
                     */
                    string r_date = sqlTime.DateAddSlash(dt_Shift_Exchange_Record.Rows[i][6].ToString());
                    string r_time = dt_Shift_Exchange_Record.Rows[i][7].ToString().Substring(0, 2) + ":" + dt_Shift_Exchange_Record.Rows[i][7].ToString().Substring(2, 2);
                    string content = dt_Shift_Exchange_Record.Rows[i][2].ToString().Replace("<br />", "\n");
                    content = content.Replace("&nbsp;", " ");
                    content = SpecialCharactersTransform.strtosc(content);
                    if (Convert.ToInt32(r_time.Substring(0, 2) + r_time.Substring(3, 2)) >= Convert.ToInt32("0000") && Convert.ToInt32(r_time.Substring(0, 2) + r_time.Substring(3, 2)) <= Convert.ToInt32("0800"))
                    {
                        content = "大夜 \n" + content + "\n";
                    }
                    if (Convert.ToInt32(r_time.Substring(0, 2) + r_time.Substring(3, 2)) >= Convert.ToInt32("0801") && Convert.ToInt32(r_time.Substring(0, 2) + r_time.Substring(3, 2)) <= Convert.ToInt32("1600"))
                    {
                        content = "白班 \n" + content + "\n";
                    }
                    if (Convert.ToInt32(r_time.Substring(0, 2) + r_time.Substring(3, 2)) >= Convert.ToInt32("1601") && Convert.ToInt32(r_time.Substring(0, 2) + r_time.Substring(3, 2)) <= Convert.ToInt32("2359"))
                    {
                        content = "小夜 \n" + content + "\n";
                    }
                    string r_user = sqlShiftExchange.SearchA_user(dt_Shift_Exchange_Record.Rows[i][8].ToString(), connection_id);
                    string IPName = dt_Shift_Exchange_Record.Rows[i][11].ToString();
                    string IPArea = dt_Shift_Exchange_Record.Rows[i][12].ToString();
                    cell[i, 0] = new PdfPCell(new iTextSharp.text.Paragraph(r_date, ChFont));
                    cell[i, 1] = new PdfPCell(new iTextSharp.text.Paragraph(r_time, ChFont));
                    cell[i, 2] = new PdfPCell(new iTextSharp.text.Paragraph(r_user, ChFont));
                    cell[i, 3] = new PdfPCell(new iTextSharp.text.Paragraph(IPName, ChFont));
                    cell[i, 4] = new PdfPCell(new iTextSharp.text.Paragraph(IPArea, ChFont));
                    cell[i, 5] = new PdfPCell(new iTextSharp.text.Paragraph(content, ChFont));
                    for (int j = 0; j < 6; j++)
                    {
                        if (cell[i, j] != null)
                        {
                            if (j == 5)
                            {
                                cell[i, j].Colspan = 3;
                            }
                            table.AddCell(cell[i, j]);
                        }
                    }
                }
                
                Doc.Add(table);
                Doc.Close();
                dt_Shift_Exchange_Record.Dispose();
                Response.Clear();
                /*
                if (DropDownListIP.SelectedValue == "0")
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=TotalShiftExchangeRecordInterval_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf");
                }
                else
                {
                    if (DropDownListArea.SelectedValue == "-99")
                    {
                        Response.AddHeader("Content-Disposition", "attachment; filename=ShiftExchangeRecordInterval_" + s_date + "_" + e_date + "_All_IP.pdf");
                    }
                    else
                    {
                        Response.AddHeader("Content-Disposition", "attachment; filename=ShiftExchangeRecordInterval_" + s_date + "_" + e_date + "_Some_IP_in_" + DropDownListArea.SelectedValue.ToString() + ".pdf");
                    }
                }
                */
                //Response.AddHeader("Content-Disposition", "attachment; filename=TotalShiftExchangeRecordInterval_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf");
                Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
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


        protected void LinkBtnGoToAddView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View4");
            Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "10";
            Response.Redirect("SHIFT_EXCHANGE_RECORD.aspx");
        }

        protected void LinkBtnGoToUView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View4");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"] = "20";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View2.aspx");
        }
        protected void LinkBtnGoToAddNRView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View4");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View1"] = "10";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View1.aspx");
        }
        protected void LinkBtnGoToQView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View4");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View3"] = "20";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View3.aspx");
        }

        protected void LinkBtnGoToMQView_Click(object sender, EventArgs e)
        {
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = "30";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View4.aspx");
        }

        //判斷Session["tab_state"]所處的狀態
        /*
        private void CurrentlyState()
        {
            if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] != null)
            {
                if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"].ToString() == "3011")
                {
                    State1();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"].ToString() == "3012")
                {
                    State1();
                }
            }
            else
            {
                Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = "";
            }
            
            if (Session["tab_state_temp"] != null)
            {
                if (Session["tab_state_temp"].ToString() == "3011_SHIFT_EXCHANGE_RECORD_View4")
                {
                    Session["tab_state"] = "3011";
                    State1();
                }
                else if (Session["tab_state_temp"].ToString() == "3012_SHIFT_EXCHANGE_RECORD_View4")
                {
                    Session["tab_state"] = "3012";
                    State1();
                }
                else
                {
                    Session["tab_state_temp"] = "";
                }
            }
            else
            {
                Session["tab_state_temp"] = "";
            }
            
        }
        */
        //state 1的狀態
        private void State1()
        {
            btnSearch_nonNRS.Enabled = true;
            btnPrint.Enabled = false;
            lblQueryResult.Visible = false;
            GridView1.Visible = false;
        }
        //state 2的狀態
        private void State2()
        {
            btnSearch_nonNRS.Enabled = true;
            btnPrint.Enabled = true;
            lblQueryResult.Visible = true;
            GridView1.Visible = true;
        }


        #region 護理交班查詢
        protected void btnNRS_ShiftRec_Click(object sender, EventArgs e)
        {
            /*
            setDateControl();
            //bind data
            string s_date = sqlTime.DateSplitSlash(txtS_DATE.Text);
            string e_date = sqlTime.DateSplitSlash(txtE_DATE.Text);
            DataTable dt_shift_exchange = sqlShiftExchange.getShiftExchange(s_date, e_date, ip_no, connection_id);
            GridView1.DataSource = BindGridData(dt_shift_exchange, ip_no);
            GridView1.DataBind();

            MultiView1.ActiveViewIndex = 1;
            btnSearch.Visible = true;
            btnSearch_nonNRS.Visible = false;
             */
        }
        #endregion
        #region 雜項交班查詢
        protected void btn_Click(object sender, EventArgs e)
        {
            /*
            setDateControl();
            string s_date = sqlTime.DateSplitSlash(txtS_DATE.Text);
            string e_date = sqlTime.DateSplitSlash(txtE_DATE.Text);
            DataTable dt_shift_exchange = sqlShiftExchange.getShiftExchange_nonNRS(s_date, e_date, ip_no, connection_id);
            GridView1.DataSource = BindGridData(dt_shift_exchange, ip_no);
            GridView1.DataBind();

            MultiView1.ActiveViewIndex = 1;
            btnSearch.Visible = false;
            btnSearch_nonNRS.Visible = true;
             */
        }
        #endregion
        #region 新增交班
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 0;
        }
        #endregion
        #region 修改交班
        protected void btnModify_shift_Click(object sender, EventArgs e)
        {
            /*
            setDateControl();
            MultiView1.ActiveViewIndex = 2;
            string s_date = sqlTime.DateSplitSlash(txtS_DATE0.Text);
            string e_date = sqlTime.DateSplitSlash(txtE_DATE0.Text);
            DataTable dt_shift_exchange = sqlShiftExchange.getALLShiftExchange(s_date, e_date, connection_id, ip_no);
            GridView2.EditIndex = -1;
            GridView2.DataSource = dt_shift_exchange;
            GridView2.DataBind();
             */
        }
        #endregion

        

        

        

        

        
        
        

    }
}