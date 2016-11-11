using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using longtermcare.NursingCare.Print;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Windows.Forms;
using AjaxControlToolkit;
using System.Data;
using System.Drawing;


namespace longtermcare.NursingPlan.Shift_Exchange
{
    public partial class SHIFT_EXCHANGE_RECORD_View2 : System.Web.UI.Page
    {
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
        System.Web.UI.WebControls.Image sortImage = new System.Web.UI.WebControls.Image();
        private string ip_no;
        private string rec_no;
        CheckBoxList[] chklp = new CheckBoxList[1000];
        CheckBoxList[] chklp_p = new CheckBoxList[1000];

        string sql = "";
        private static string connection_id;

        //離開網頁時清除狀態轉換的Session
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View2");
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

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
                sqlShiftExchange.connect(connection_id);

                SqlDataSource1.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
                SqlDataSource1.SelectCommand = "SELECT Login, Name FROM EMP_LOGIN WHERE AccountEnable = 'Y' AND LOGIN_IDENTITY = 'Y'";

                SqlDataSource3.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
                SqlDataSource3.SelectCommand = "SELECT [AREA_STATE] FROM [CODE_ROOM_AREA] ORDER BY [AREA_ID] ASC";
            
                this.Form.Attributes.Add("autocomplete", "off");

                if (IsPostBack != true)
                {
                    string nowdate = sqlTime.time();
                    txtC_date_s2.Text = sqlTime.DateAddSlash(nowdate);
                    DateTime Enddate = System.DateTime.ParseExact(nowdate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime Startdate = Enddate.AddDays(-7);
                    txtC_date_s1.Text = Startdate.ToString("yyyy/MM/dd");

                    if (String.IsNullOrEmpty(GridView3.SortExpression)) GridView3.Sort("A_DATETIME", SortDirection.Descending);
                    Session.Remove("SortedView");
                }

                BindSortingImg();

            }
            else
            {
                Response.Write("<script>alert('請先點選住民以便新增'); location.href='../../FNursingPlan.aspx'; </script>");
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('請先點選住民以便新增');location.href='../../FNursingPlan.aspx';", true);
            }
            
        }

        //列印
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string ip_no = sstr_ipno.Text;
            string connection_id = sstr_hid.Text;
            string print_date = sqlTime.time();
            string print_time = sqlTime.hourminute();
            string print_user = sqlShiftExchange.SearchA_user(str_account.Text, connection_id);//(string a_user, string connection_id)
            int rec_count = GridView3.Rows.Count; //區間記錄筆數
            string ip_name;
            string ip_sex;
            int dob = 0;
            int nowyear = Convert.ToInt32(sqlTime.time_year());
            string age = "";
            string room_bed = "";
            string ip_nno = "";

            DataTable dt = BindGridView();

            try
            {
                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                string pdffilename = "NP_ShiftExchRecordList_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf";
                sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                pdfWriter.PageEvent = new sqlPDFFooter();

                //字型設定     
                //BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\mingliu.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
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
                Paragraph Title = new Paragraph(Session["hp_name"].ToString() , ChFont2);
                Title.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                Doc.Add(Title);
                Paragraph Title2 = new Paragraph("雜項交班紀錄單", ChFont2);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右               


                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    PdfPTable title = new PdfPTable(5);
                    title.SpacingBefore = 10f;
                    title.WidthPercentage = 100;

                    ip_name = SqlMain.GetIPName(ip_no);
                    ip_sex = SqlMain.GetIPSex(ip_no).ToString() == "1" ? "男" : "女";
                    dob = Convert.ToInt32(sqlTime.cut(SqlMain.GetIPBirthday(ip_no).Trim()));
                    age = (nowyear - dob).ToString();
                    age = age == "-1" ? "" : age;
                    room_bed = SqlMain.GetIPBed(ip_no);

                    //DataTable dt_ip_info = sqlShiftExchange.SearchIPInfo(ip_no, connection_id);//(string ip_no, string connection_id)
                    //ip_name = dt_ip_info.Rows[0][0].ToString();
                    //ip_sex = dt_ip_info.Rows[0][1].ToString() == "1" ? "男" : "女";
                    //dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0][2].ToString().Trim()));
                    //age = (nowyear - dob).ToString();
                    //age = age == "-1" ? "" : age;
                    //room_bed = dt_ip_info.Rows[0][3].ToString();
                    ip_nno = sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no);//dt_ip_info.Rows[0][4].ToString();
                    PdfPCell[] info = new PdfPCell[8];
                    info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                    info[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號：" + ip_nno, ChFont));
                    info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                    info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                    info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));
                    info[5] = new PdfPCell(new iTextSharp.text.Phrase("列印人員：" + print_user, ChFont));
                    info[6] = new PdfPCell(new iTextSharp.text.Phrase("列印日期：" + sqlTime.DateAddSlash(sqlTime.time()), ChFont));
                    info[7] = new PdfPCell(new iTextSharp.text.Phrase("列印時間：" + print_time.Substring(0, 2) + ":" + print_time.Substring(2, 2), ChFont));

                    Doc.Add(Title2);
                    //Paragraph Title3 = new Paragraph("\n", ChFont2);
                    //Doc.Add(Title3);
                    for (int i = 0; i < info.Length; i++)
                    {
                        info[i].BorderWidth = 0;
                        if (i == 5) info[i].Colspan = 2;
                        if (i == 6) info[i].Colspan = 2;
                        title.AddCell(info[i]);
                    }
                    Doc.Add(title);
                    //Doc.Add(new Paragraph("\n"));

                }
                else//所有住民
                {
                    PdfPTable title = new PdfPTable(3);
                    title.SpacingBefore = 10f;
                    title.WidthPercentage = 100;

                    ip_name = "";
                    ip_sex = "";
                    age = "";
                    room_bed = "";

                    PdfPCell[] info = new PdfPCell[3];
                    info[0] = new PdfPCell(new iTextSharp.text.Phrase("列印人員：" + print_user, ChFont));
                    info[1] = new PdfPCell(new iTextSharp.text.Phrase("列印日期：" + sqlTime.DateAddSlash(sqlTime.time()), ChFont));
                    info[2] = new PdfPCell(new iTextSharp.text.Phrase("列印時間：" + print_time.Substring(0, 2) + ":" + print_time.Substring(2, 2), ChFont));

                    Doc.Add(Title2);
                    //Paragraph Title3 = new Paragraph("\n", ChFont2);
                    //Doc.Add(Title3);
                    for (int i = 0; i < info.Length; i++)
                    {
                        info[i].BorderWidth = 0;
                        title.AddCell(info[i]);
                    }
                    Doc.Add(title);
                    //Doc.Add(new Paragraph("\n"));
                }

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
                /*
                 * SHIFT_EXCHANGE_RECORD.REC_NO, 0
                 * SHIFT_EXCHANGE_RECORD.NRS_NO, 1
                 * SHIFT_EXCHANGE_RECORD.IP_NO, 2 
                 * SHIFT_EXCHANGE_RECORD.NRS_CONTENT, 3 
                 * SHIFT_EXCHANGE_RECORD.A_DATE, 4
                 * SHIFT_EXCHANGE_RECORD.A_TIME, 5
                 * SHIFT_EXCHANGE_RECORD.A_USER,  6
                 * SHIFT_EXCHANGE_RECORD.CREATE_DATE, 7 
                 * SHIFT_EXCHANGE_RECORD.CREATE_TIME, 8
                 * SHIFT_EXCHANGE_RECORD.CREATE_USER, 9
                 * SHIFT_EXCHANGE_RECORD.OP_DATE, 10
                 * SHIFT_EXCHANGE_RECORD.OP_TIME, 11
                 * SHIFT_EXCHANGE_RECORD.OP_USER, 12
                 * SHIFT_EXCHANGE_RECORD.AccountEnable, 13 
                 * IP_INFORMATION.IP_NO_NEW, 14
                 * IP_INFORMATION.IP_NAME, 15
                 * ROOM_AREA, 16
                 * ROOM_BED, 17
                 * NAME 18
                 */
                PdfPCell[,] cell = new PdfPCell[rec_count, 6];
                for (int i = 0; i < rec_count; i++)
                {

                    string CDate = sqlTime.DateAddSlash(dt.Rows[i]["A_DATE"].ToString());
                    string CTime = dt.Rows[i]["A_TIME"].ToString().Substring(0, 2) + ":" + dt.Rows[i]["A_TIME"].ToString().Substring(2, 2);
                    string CUser = dt.Rows[i]["Name"].ToString();
                    string IPName = dt.Rows[i]["IP_NAME"].ToString();
                    string IPArea = dt.Rows[i]["ROOM_AREA"].ToString();
                    string content = dt.Rows[i]["NRS_CONTENT"].ToString().Replace("<br />", "\n");
                    content = content.Replace("&nbsp;", " ");
                    content = SpecialCharactersTransform.strtosc(content);
                    if (Convert.ToInt32(CTime.Substring(0, 2) + CTime.Substring(3, 2)) >= Convert.ToInt32("0000") && Convert.ToInt32(CTime.Substring(0, 2) + CTime.Substring(3, 2)) <= Convert.ToInt32("0800"))
                    {
                        content = "大夜 \n" + content + "\n";
                    }
                    if (Convert.ToInt32(CTime.Substring(0, 2) + CTime.Substring(3, 2)) >= Convert.ToInt32("0801") && Convert.ToInt32(CTime.Substring(0, 2) + CTime.Substring(3, 2)) <= Convert.ToInt32("1600"))
                    {
                        content = "白班 \n" + content + "\n";
                    }
                    if (Convert.ToInt32(CTime.Substring(0, 2) + CTime.Substring(3, 2)) >= Convert.ToInt32("1601") && Convert.ToInt32(CTime.Substring(0, 2) + CTime.Substring(3, 2)) <= Convert.ToInt32("2359"))
                    {
                        content = "小夜 \n" + content + "\n";
                    }
                    cell[i, 0] = new PdfPCell(new iTextSharp.text.Paragraph(CDate, ChFont));
                    cell[i, 1] = new PdfPCell(new iTextSharp.text.Paragraph(CTime, ChFont));
                    cell[i, 2] = new PdfPCell(new iTextSharp.text.Paragraph(CUser, ChFont));
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
                Response.Clear();
                /*
                if (DropDownListIP.SelectedValue == "0")
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=ShiftExchangeRecordInterval_NonNR_" + s_date + "_" + e_date + "_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf");
                }
                else
                {
                    if (DropDownListArea.SelectedValue == "-99")
                    {
                        Response.AddHeader("Content-Disposition", "attachment; filename=ShiftExchangeRecordInterval_NonNR_" + s_date + "_" + e_date + "_All_IP.pdf");
                    }
                    else
                    {
                        Response.AddHeader("Content-Disposition", "attachment; filename=ShiftExchangeRecordInterval_NonNR_" + s_date + "_" + e_date + "_Some_IP_in_" + DropDownListArea.SelectedValue.ToString()+ ".pdf");
                    }
                }
                */
                //Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
                ////Response.ContentType = "application/octet-stream";
                //Response.ContentType = "application/pdf";
                //Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                //Response.OutputStream.Flush();
                //Response.OutputStream.Close();
                //Response.Flush();
                //Response.End();

                //檔案下載*/
                Response.Clear();
                Response.AddHeader("Content-Disposition", "inline; filename=" + "ShiftExchangeRecord_NR.pdf");
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
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

       
        protected void ExecSearch(string rec_no)
        {
            connection_id = sstr_hid.Text;
            sqlShiftExchange.connect(connection_id);
            DataTable dt_SelSERecord = sqlShiftExchange.getSelSERecord(rec_no, connection_id);

            lbname.Text = dt_SelSERecord.Rows[0]["IP_NAME"].ToString().Trim();
            lb_rec_no.Text = rec_no;
            lb_ip_no.Text = dt_SelSERecord.Rows[0]["IP_NO"].ToString().Trim();
            string ip_no_new, ip_name, ip_sex, age, room_bed;
            SqlMain.Get_IP_Information_now(connection_id, lb_ip_no.Text, out ip_no_new, out ip_name, out ip_sex, out age, out room_bed);
            lblipnonew.Text = ip_no_new;
            lblroombed.Text = room_bed;
            lblipsex.Text = ip_sex;
            lblipage.Text = age;

            lblIP_NO.Text = dt_SelSERecord.Rows[0][2].ToString();
            string nrs_no = dt_SelSERecord.Rows[0][1].ToString();
            txtC_DATE.Text = dt_SelSERecord.Rows[0][7].ToString().Substring(0, 4) + "/" + dt_SelSERecord.Rows[0][7].ToString().Substring(4, 2) + "/" + dt_SelSERecord.Rows[0][7].ToString().Substring(6, 2);
            txtC_TIME.Text = dt_SelSERecord.Rows[0][8].ToString().Substring(0, 2) + ":" + dt_SelSERecord.Rows[0][8].ToString().Substring(2, 2);
            string nowdate = sqlTime.datetime();
            string nowtime = nowdate.Substring(8, 4);
            txtA_DATE.Text = nowdate.Substring(0, 4) + "/" + nowdate.Substring(4, 2) + "/" + nowdate.Substring(6, 2);
            txtA_TIME.Text = nowtime.Substring(0, 2) + ":" + nowtime.Substring(2, 2);
            txtC_USER.Text = sqlShiftExchange.SearchA_user(dt_SelSERecord.Rows[0][9].ToString(), connection_id);
            lblRecUserID.Text = dt_SelSERecord.Rows[0][9].ToString();
            txtA_USER.Text = sqlShiftExchange.SearchA_user(Session["account"].ToString(), connection_id);
            //txtIP_Name.Text = dt_SelSERecord.Rows[0][15].ToString();
            txtIPAREA.Text = dt_SelSERecord.Rows[0][16].ToString();
            txtContent.Text = dt_SelSERecord.Rows[0][3].ToString();
            txtContent.Text = SpecialCharactersTransform.strtosc(txtContent.Text);
            dt_SelSERecord.Dispose();
            if (str_account.Text == lblRecUserID.Text)//登入者與記錄者一致
            {
                State6();
            }
            else
            {
                State3();
            }
            MultiView1.ActiveViewIndex = 1;
        }
        //GridView1日期格式轉換
        protected string FormatDate(string Date)
        {
            return Date.Substring(0, 4) + "/" + Date.Substring(4, 2) + "/" + Date.Substring(6, 2);
        }

        protected string FormatDATETIME(string datetime)
        {
            return datetime.Substring(0, 4) + "/" + datetime.Substring(4, 2) + "/" + datetime.Substring(6, 2) + " " + datetime.Substring(8, 2) + ":" + datetime.Substring(10, 2);
        }

        //GridView1時間格式轉換
        protected string FormatTime(string Time)
        {
            return Time.Substring(0, 2) + ":" + Time.Substring(2, 2);
        }

        //GridView1交班內容格式轉換
        protected string FormatContent(string Content)
        {
            return SpecialCharactersTransform.strtosc((Content.Replace("<br />", "\n")).Replace("&nbsp;", " "));
        }

        
        //修改這一筆紀錄
        protected void Button2_Click(object sender, EventArgs e)
        {
            State7();
            //if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString().Substring(0, 3) == "201")
            //{
            //    State5();
            //    Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"] = Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString().Substring(0, 3) + "5";
            //    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_SHIFT_EXCHANGE_RECORD_View2";
            //}
            //if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString().Substring(0, 3) == "203")
            //{
            //    State7();
            //    Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"] = Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString().Substring(0, 3) + "7";
            //    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_SHIFT_EXCHANGE_RECORD_View2";
            //}
            //PanelData.Enabled = true;
            //btnSAVE.Enabled = true;
            //btnREWRITE.Enabled = true;
            //Button2.Enabled = false;
        }
        //刪除這一筆紀錄
        protected void btn_DELET_Click(object sender, EventArgs e)
        {
            rec_no = lb_rec_no.Text;
            connection_id = sstr_hid.Text;

            //Shadow處理
            ShadowShiftExchangeRecord(rec_no, sqlTime.time(), sqlTime.hourminute(), str_account.Text, "D");

            //string warning = sqlShiftExchange.delShiftContent(rec_no, connection_id, Session["account"].ToString());
            string warning = sqlShiftExchange.delShift(rec_no, connection_id, str_account.Text);
            
            if (warning == "刪除失敗")
            {
                lblShowErrMsg.Text = "刪除失敗";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
            }
            else
            {
                lblShowMsg.Text = warning;
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
            }

            State6();

            DataView sortedView = new DataView(BindGridView());
            GridView3.DataSource = sortedView;
            GridView3.DataBind();
            GridView3.SelectedIndex = -1;

            if (GridView3.Rows.Count != 0)
            {
                lblQueryResult.Text = "共" + sortedView.Count + "筆";
            }
            else
            {
                lblQueryResult.Text = "";
                lblQueryResult.Visible = false;
            }
            MultiView1.ActiveViewIndex = 0;
     
        }

        //返回查詢
        protected void btnRESEARCH_Click(object sender, EventArgs e)
        {
            State6();
            MultiView1.ActiveViewIndex = 0;
        }

        //修改
        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            try
            {
                rec_no = lb_rec_no.Text;
                connection_id = sstr_hid.Text;

                //Shadow處理
                ShadowShiftExchangeRecord(rec_no, sqlTime.time(), sqlTime.hourminute(), str_account.Text, "U");

                string warning = sqlShiftExchange.updateShiftContent(rec_no, SpecialCharactersTransform.sctostr(txtContent.Text), connection_id, str_account.Text);
                ////(string rec_no, string content, string connection_id, string op_user)

                if (warning == "修改成功")
                {

                    lblShowMsg.Text = "修改成功";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    lblShowErrMsg.Text = "修改失敗";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                    MultiView1.ActiveViewIndex = 0;
                }

                DataView sortedView = new DataView(BindGridView());
                GridView3.DataSource = sortedView;
                GridView3.DataBind();
                GridView3.SelectedIndex = -1;

                State6();
                /*
                string sdate = txtS_DATE0.Text.Substring(0, 4) + txtS_DATE0.Text.Substring(5, 2) + txtS_DATE0.Text.Substring(8, 2);
                string edate = txtE_DATE0.Text.Substring(0, 4) + txtE_DATE0.Text.Substring(5, 2) + txtE_DATE0.Text.Substring(8, 2);
                if (DropDownListIP.SelectedValue == "0")//此一住民
                {
                    SqlDataSource1.SelectCommand = "SELECT SHIFT_EXCHANGE_RECORD.REC_NO,SHIFT_EXCHANGE_RECORD.NRS_NO,SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE,SHIFT_EXCHANGE_RECORD.A_TIME,SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.OP_DATE, SHIFT_EXCHANGE_RECORD.OP_TIME, SHIFT_EXCHANGE_RECORD.OP_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED, NAME FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO  LEFT JOIN EMP_LOGIN ON EMP_LOGIN.Login = SHIFT_EXCHANGE_RECORD.A_USER WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO = '0' AND SHIFT_EXCHANGE_RECORD.A_DATE BETWEEN '" + sdate + "' AND '" + edate + "' AND SHIFT_EXCHANGE_RECORD.IP_NO = '" + Session["ipno"].ToString() + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO ";
                }
                else//全部住民
                {
                    if (DropDownListArea.SelectedValue == "-99")//不分區域或樓層
                    {
                        SqlDataSource1.SelectCommand = "SELECT SHIFT_EXCHANGE_RECORD.REC_NO,SHIFT_EXCHANGE_RECORD.NRS_NO,SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE,SHIFT_EXCHANGE_RECORD.A_TIME,SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.OP_DATE, SHIFT_EXCHANGE_RECORD.OP_TIME, SHIFT_EXCHANGE_RECORD.OP_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED, NAME FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO  LEFT JOIN EMP_LOGIN ON EMP_LOGIN.Login = SHIFT_EXCHANGE_RECORD.A_USER WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO = '0' AND SHIFT_EXCHANGE_RECORD.A_DATE BETWEEN '" + sdate + "' AND '" + edate + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO ";
                    }
                    else//有選擇特定區域和樓層
                    {
                        SqlDataSource1.SelectCommand = "SELECT SHIFT_EXCHANGE_RECORD.REC_NO,SHIFT_EXCHANGE_RECORD.NRS_NO,SHIFT_EXCHANGE_RECORD.IP_NO, SHIFT_EXCHANGE_RECORD.NRS_CONTENT, SHIFT_EXCHANGE_RECORD.A_DATE,SHIFT_EXCHANGE_RECORD.A_TIME,SHIFT_EXCHANGE_RECORD.A_USER, SHIFT_EXCHANGE_RECORD.CREATE_DATE, SHIFT_EXCHANGE_RECORD.CREATE_TIME, SHIFT_EXCHANGE_RECORD.CREATE_USER, SHIFT_EXCHANGE_RECORD.OP_DATE, SHIFT_EXCHANGE_RECORD.OP_TIME, SHIFT_EXCHANGE_RECORD.OP_USER, SHIFT_EXCHANGE_RECORD.AccountEnable, IP_INFORMATION.IP_NO_NEW, IP_INFORMATION.IP_NAME, ROOM_AREA, ROOM_BED, NAME FROM SHIFT_EXCHANGE_RECORD LEFT JOIN IP_INFORMATION ON SHIFT_EXCHANGE_RECORD.IP_NO = IP_INFORMATION.IP_NO LEFT JOIN ROOM ON SHIFT_EXCHANGE_RECORD.IP_NO = ROOM.IP_NO  LEFT JOIN EMP_LOGIN ON EMP_LOGIN.Login = SHIFT_EXCHANGE_RECORD.A_USER WHERE SHIFT_EXCHANGE_RECORD.AccountEnable = 'Y' AND SHIFT_EXCHANGE_RECORD.NRS_NO = '0' AND SHIFT_EXCHANGE_RECORD.A_DATE BETWEEN '" + sdate + "' AND '" + edate + "' AND ROOM_AREA = '" + DropDownListArea.SelectedValue.ToString() + "' ORDER BY A_DATE DESC, ROOM.ROOM_AREA, ROOM_BED, IP_NAME, A_TIME, REC_NO ";
                    }
                }
                SqlDataSource1.DataBind();
                GridView1.DataBind();
                GridView2.DataBind();
                GridView1.SelectedIndex = -1;
                State2();
                 */
            
                MultiView1.ActiveViewIndex = 1;
            }
            catch
            {
                lblShowErrMsg.Text = "修改失敗";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "更新失敗" + "');", true);
                //CurrentlyState();
                MultiView1.ActiveViewIndex = 1;
            }
        }
        //列印這筆紀錄
        protected void btnPrintThisRecord_Click(object sender, EventArgs e)
        {
            string connection_id = sstr_hid.Text;
            string ip_no = lblIP_NO.Text;
            string print_date = sqlTime.time();
            string print_time = sqlTime.hourminute();
            string print_user = sqlShiftExchange.SearchA_user(str_account.Text, connection_id);//(string a_user, string connection_id)

            string ip_name = SqlMain.GetIPName(ip_no);
            string ip_sex = SqlMain.GetIPSex(ip_no).ToString() == "1" ? "男" : "女";
            int dob = Convert.ToInt32(sqlTime.cut(SqlMain.GetIPBirthday(ip_no).Trim()));
            int nowyear = Convert.ToInt32(sqlTime.time_year());
            string age = (nowyear - dob).ToString();
            age = age == "-1" ? "" : age;
            string room_bed = SqlMain.GetIPBed(ip_no);
            
            //DataTable dt_ip_info = sqlShiftExchange.SearchIPInfo(ip_no, connection_id);//(string ip_no, string connection_id)
            //string ip_name = txtIP_Name.Text;
            //string ip_sex = dt_ip_info.Rows[0][1].ToString() == "1" ? "男" : "女";
            //int dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0][2].ToString().Trim()));
            //int nowyear = Convert.ToInt32(sqlTime.time_year());
            //string age = (nowyear - dob).ToString();
            //age = age == "-1" ? "" : age;
            //string room_bed = dt_ip_info.Rows[0][3].ToString();
            //string ip_nno = dt_ip_info.Rows[0][4].ToString();
            try
            {
                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                string pdffilename = "NP_ShiftExchRecord_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf";
                sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                pdfWriter.PageEvent = new sqlPDFFooter();

                //字型設定     
                //BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\mingliu.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
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
                Paragraph Title = new Paragraph(Session["hp_name"].ToString(), ChFont2);
                Title.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                Doc.Add(Title);
                Paragraph Title2 = new Paragraph("雜項交班紀錄單", ChFont2);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右               
                PdfPTable title = new PdfPTable(5);
                title.SpacingBefore = 10f;
                title.WidthPercentage = 100;

                PdfPCell[] info = new PdfPCell[8];
                info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                info[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號：" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no), ChFont));
                info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));
                info[5] = new PdfPCell(new iTextSharp.text.Phrase("列印人員：" + print_user, ChFont));
                info[6] = new PdfPCell(new iTextSharp.text.Phrase("列印日期：" + sqlTime.DateAddSlash(sqlTime.time()), ChFont));
                info[7] = new PdfPCell(new iTextSharp.text.Phrase("列印時間：" + print_time.Substring(0, 2) + ":" + print_time.Substring(2, 2), ChFont));

                Doc.Add(Title2);
                //Paragraph Title3 = new Paragraph("\n", ChFont2);
                //Doc.Add(Title3);
                for (int i = 0; i < info.Length; i++)
                {
                    info[i].BorderWidth = 0;
                    if (i == 5) info[i].Colspan = 2;
                    if (i == 6) info[i].Colspan = 2;
                    title.AddCell(info[i]);
                }
                Doc.Add(title);
                //Doc.Add(new Paragraph("\n"));

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
                PdfPCell[] cell = new PdfPCell[6];
                cell[0] = new PdfPCell(new iTextSharp.text.Phrase(txtC_DATE.Text, ChFont));
                cell[1] = new PdfPCell(new iTextSharp.text.Phrase(txtC_TIME.Text, ChFont));
                cell[2] = new PdfPCell(new iTextSharp.text.Phrase(txtC_USER.Text, ChFont));
                cell[3] = new PdfPCell(new iTextSharp.text.Phrase(lbname.Text, ChFont));
                cell[4] = new PdfPCell(new iTextSharp.text.Phrase(txtIPAREA.Text, ChFont));
                //cell[5] = new PdfPCell(new iTextSharp.text.Phrase(txtContent.Text, ChFont));
                if (Convert.ToInt32(txtC_TIME.Text.Substring(0, 2) + txtC_TIME.Text.Substring(3, 2)) >= Convert.ToInt32("0000") && Convert.ToInt32(txtC_TIME.Text.Substring(0, 2) + txtC_TIME.Text.Substring(3, 2)) <= Convert.ToInt32("0800"))
                {
                    cell[5] = new PdfPCell(new iTextSharp.text.Phrase("大夜 \n" + txtContent.Text, ChFont));
                    //content = "大夜 \n" + content + "\n";
                }
                if (Convert.ToInt32(txtC_TIME.Text.Substring(0, 2) + txtC_TIME.Text.Substring(3, 2)) >= Convert.ToInt32("0801") && Convert.ToInt32(txtC_TIME.Text.Substring(0, 2) + txtC_TIME.Text.Substring(3, 2)) <= Convert.ToInt32("1600"))
                {
                    cell[5] = new PdfPCell(new iTextSharp.text.Phrase("白班 \n" + txtContent.Text, ChFont));
                    //content = "白班 \n" + content + "\n";
                }
                if (Convert.ToInt32(txtC_TIME.Text.Substring(0, 2) + txtC_TIME.Text.Substring(3, 2)) >= Convert.ToInt32("1601") && Convert.ToInt32(txtC_TIME.Text.Substring(0, 2) + txtC_TIME.Text.Substring(3, 2)) <= Convert.ToInt32("2359"))
                {
                    cell[5] = new PdfPCell(new iTextSharp.text.Phrase("小夜 \n" + txtContent.Text, ChFont));
                    //content = "小夜 \n" + content + "\n";
                }
                cell[5].Colspan = 3;
                for (int j = 0; j < cell.Length; j++)
                {
                    if (cell[j] != null)
                    {
                        table.AddCell(cell[j]);
                    }
                }
                Doc.Add(table);
                Doc.Close();
                //Response.Clear();
                ////Response.AddHeader("Content-Disposition", "attachment; filename=ShiftExchangeRecord_NonNR_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf");
                //Response.AddHeader("Content-Disposition", "inline; filename=ShiftExchangeRecord_NonNR_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf");
                ////Response.ContentType = "application/octet-stream";
                //Response.ContentType = "application/pdf";
                //Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                //Response.OutputStream.Flush();
                //Response.OutputStream.Close();
                //Response.Flush();
                //Response.End();

                //檔案下載*/
                Response.Clear();
                Response.AddHeader("Content-Disposition", "inline; filename=" + "ShiftExchangeRecord_NR_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf");
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
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
            txtContent.Text = "";
            MultiView1.ActiveViewIndex = 1;
        }
           

        //片語:內容
        protected void ibtnPhraseShiftExchangeContent_Click(object sender, ImageClickEventArgs e)
        {
            sqlPHRASE.connect(sstr_hid.Text);
            Phrase.sqlPHRASE.connect(sstr_hid.Text);
            //string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("雜項交班-內容");
            int selectvalue = 1;
            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename, "雜項交班-內容");
            string[,] phrase_item = sqlPHRASE.GetPhrase_item();
            TabPanel tabid = new TabPanel();
            tabid.HeaderText = pharsename;
            chklp[0] = new CheckBoxList();
            chklp[0].RepeatColumns = 8;
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
            MultiView1.ActiveViewIndex = 1;

        }

        protected void LinkBtnGoToAddView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View2");
            Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "10";
            Response.Redirect("SHIFT_EXCHANGE_RECORD.aspx");
        }

        protected void LinkBtnGoToUView_Click(object sender, EventArgs e)
        {
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"] = "20";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View2.aspx");
        }
        protected void LinkBtnGoToAddNRView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View2");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View1"] = "10";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View1.aspx");
        }
        protected void LinkBtnGoToQView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View2");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View3"] = "20";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View3.aspx");
        }
        protected void LinkBtnGoToMQView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD_View2");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = "30";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View4.aspx");
        }
        
        //判斷Session["tab_state"]所處的狀態
        /*
        private void CurrentlyState()
        {
            if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"] != null)
            {
                if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2001")
                {
                    State1();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2011")
                {
                    State1();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2021")
                {
                    State1();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2031")
                {
                    State1();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2002")
                {
                    State2();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2012")
                {
                    State2();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2022")
                {
                    State2();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2032")
                {
                    State2();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2003")
                {
                    State3();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2014")
                {
                    State4();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2028")
                {
                    State8();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2036")
                {
                    State6();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2015")
                {
                    State5();
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"].ToString() == "2037")
                {
                    State7();
                }
            }
            else
            {
                Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"] = "";
            }
            
            if (Session["tab_state_temp"] != null)
            {
                if (Session["tab_state_temp"].ToString() == "2001_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2001";
                    State1();
                }
                else if (Session["tab_state_temp"].ToString() == "2011_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2011";
                    State1();
                }
                else if (Session["tab_state_temp"].ToString() == "2021_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2021";
                    State1();
                }
                else if (Session["tab_state_temp"].ToString() == "2031_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2031";
                    State1();
                }
                else if (Session["tab_state_temp"].ToString() == "2002_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2002";
                    State2();
                }
                else if (Session["tab_state_temp"].ToString() == "2012_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2012";
                    State2();
                }
                else if (Session["tab_state_temp"].ToString() == "2022_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2022";
                    State2();
                }
                else if (Session["tab_state_temp"].ToString() == "2032_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2032";
                    State2();
                }
                else if (Session["tab_state_temp"].ToString() == "2003_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2003";
                    State3();
                }
                else if (Session["tab_state_temp"].ToString() == "2014_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2014";
                    State4();
                }
                else if (Session["tab_state_temp"].ToString() == "2028_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2028";
                    State8();
                }
                else if (Session["tab_state_temp"].ToString() == "2036_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2036";
                    State6();
                }
                else if (Session["tab_state_temp"].ToString() == "2015_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2015";
                    State5();
                }
                else if (Session["tab_state_temp"].ToString() == "2037_SHIFT_EXCHANGE_RECORD_View2")
                {
                    Session["tab_state"] = "2037";
                    State7();
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
            btnPrint.Enabled = false;
            lblQueryResult.Visible = false;
            MultiView1.ActiveViewIndex = 0;
        }
        //state 2的狀態
        private void State2()
        {
            btnPrint.Enabled = true;
            lblQueryResult.Visible = true;
            MultiView1.ActiveViewIndex = 0;
        }
        //state 3的狀態<只能查>
        private void State3()
        {
            Button2.Enabled = false;//修改這一筆
            btn_DELET.Enabled = false;//刪除這一筆
            btnRESEARCH.Enabled = true;//返回查詢
            PanelData.Enabled = false;//資料填寫區
            PanelSymbolsTools.Visible = false;
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除 
        }
        //state 4的狀態<查修>
        private void State4()
        {
            Button2.Enabled = true;//修改這一筆
            btn_DELET.Enabled = false;//刪除這一筆
            btnRESEARCH.Enabled = true;//返回查詢
            PanelData.Enabled = false;//資料填寫區
            PanelSymbolsTools.Visible = false;
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除
        }
        //state 8的狀態<查刪>
        private void State8()
        {
            Button2.Enabled = false;//修改這一筆
            btn_DELET.Enabled = true;//刪除這一筆
            btnRESEARCH.Enabled = true;//返回查詢
            PanelData.Enabled = false;//資料填寫區
            PanelSymbolsTools.Visible = false;
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除
        }
        //state 6的狀態<查修刪>
        private void State6()
        {
            Button2.Enabled = true;//修改這一筆
            btn_DELET.Enabled = true;//刪除這一筆
            btnRESEARCH.Enabled = true;//返回查詢
            PanelData.Enabled = false;//資料填寫區
            PanelSymbolsTools.Visible = false;
            btnSAVE.Enabled = false;//儲存
            btnPrintThisRecord.Enabled = true;//列印
            btnREWRITE.Enabled = false;//清除 
        }
        //state 5的狀態
        private void State5()
        {
            Button2.Enabled = false;//修改這一筆
            btn_DELET.Enabled = false;//刪除這一筆
            btnRESEARCH.Enabled = true;//返回查詢
            PanelData.Enabled = true;//資料填寫區
            btnSAVE.Enabled = true;//儲存
            btnPrintThisRecord.Enabled = false;//列印
            btnREWRITE.Enabled = true;//清除
            MultiView1.ActiveViewIndex = 1;
        }
        //state 7的狀態
        private void State7()
        {
            Button2.Enabled = false;//修改這一筆
            btn_DELET.Enabled = true;//刪除這一筆
            btnRESEARCH.Enabled = true;//返回查詢
            PanelData.Enabled = true;//資料填寫區
            btnSAVE.Enabled = true;//儲存
            btnPrintThisRecord.Enabled = false;//列印
            btnREWRITE.Enabled = true;//清除
            MultiView1.ActiveViewIndex = 1;
        }

        private void ShadowShiftExchangeRecord(string recno, string OPing_DATE, string OPing_TIME, string OPing_USER, string OPing_STATE)
        {
            //先讀取原先資料
            DataTable dt_Shift_Exchange_Record = sqlShiftExchange.SearchRecData(recno, sstr_hid.Text);

            string REC_NO = dt_Shift_Exchange_Record.Rows[0][0].ToString();
            string NRS_NO = dt_Shift_Exchange_Record.Rows[0][1].ToString();
            string IP_NO = dt_Shift_Exchange_Record.Rows[0][2].ToString();
            string NRS_CONTENT = dt_Shift_Exchange_Record.Rows[0][3].ToString();
            string A_DATE = dt_Shift_Exchange_Record.Rows[0][4].ToString();
            string A_TIME = dt_Shift_Exchange_Record.Rows[0][5].ToString();
            string A_USER = dt_Shift_Exchange_Record.Rows[0][6].ToString();
            string CREATE_DATE = dt_Shift_Exchange_Record.Rows[0][7].ToString();
            string CREATE_TIME = dt_Shift_Exchange_Record.Rows[0][8].ToString();
            string CREATE_USER = dt_Shift_Exchange_Record.Rows[0][9].ToString();
            string OP_DATE = dt_Shift_Exchange_Record.Rows[0][10].ToString();
            string OP_TIME = dt_Shift_Exchange_Record.Rows[0][11].ToString();
            string OP_USER = dt_Shift_Exchange_Record.Rows[0][12].ToString();
            dt_Shift_Exchange_Record.Dispose();
            //將資料新增至交班的Shadow
            sqlShiftExchange objShadow = new sqlShiftExchange();
            objShadow.addshadowshiftexchangerecord(REC_NO, NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, OP_DATE, OP_TIME, OP_USER, OPing_DATE, OPing_TIME, OPing_USER, OPing_STATE, sstr_hid.Text);
            string warning = objShadow.GetShadowWarning();
            if (warning.Equals("新增資料失敗"))
            {
                if (OPing_STATE == "U")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "修改資料失敗" + "'); ", true);
                    //CurrentlyState();
                }
                else if (OPing_STATE == "D")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "刪除資料失敗" + "'); ", true);
                    //CurrentlyState();
                }

            }

            
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

        protected void btnS_Click(object sender, EventArgs e)
        {
            bool alert = true;
            txt_sqlcom.Text = "1";

            if (DateTimeCheck.SDATEVSEDATE(txtC_date_s1.Text, txtC_date_s2.Text).Equals(""))
            {
                GridView3.Visible = true;
                string sdate = sqlTime.DateSplitSlash(txtC_date_s1.Text);
                string edate = sqlTime.DateSplitSlash(txtC_date_s2.Text);

                //此一住民
                if (DropDownListIP.SelectedValue.Equals("0"))
                {
                    sql += "AND SHIFT_EXCHANGE_RECORD.IP_NO='" + Session["ipno"].ToString() + "' AND SHIFT_EXCHANGE_RECORD.A_DATE>='" + sdate + "' AND SHIFT_EXCHANGE_RECORD.A_DATE<='" + edate + "'";

                    if (dropP_USER.SelectedValue.ToString() != "-99")//特定評估人員
                    {
                        sql += "AND SHIFT_EXCHANGE_RECORD.A_USER='" + dropP_USER.SelectedValue.ToString() + "'";
                    }
                }
                else//全部住民 
                {
                    sql += "AND SHIFT_EXCHANGE_RECORD.A_DATE>='" + sdate + "' AND SHIFT_EXCHANGE_RECORD.A_DATE<='" + edate + "'";

                    if (DropDownListArea.SelectedValue != "-99")//特定區域或樓層
                    {
                        sql += "AND ROOM.ROOM_AREA='" + DropDownListArea.SelectedValue.ToString() + "'";
                    }

                    if (dropP_USER.SelectedValue.ToString() != "-99")//特定評估人員
                    {
                        sql += "AND SHIFT_EXCHANGE_RECORD.A_USER='" + dropP_USER.SelectedValue.ToString() + "'";
                    }
                }

                txt_sql.Text = sql;
                DataView sortedView = new DataView(BindGridView());
                Session["SortedView"] = sortedView;

                lblQueryResult.Visible = true;
                lblQueryResult.Text = "共" + sortedView.Count + "筆";

                GridView3.DataSource = sortedView;
                GridView3.DataBind();

                if (GridView3.Rows.Count == 0)
                {
                    btnPrint.Visible = false;
                    lblQueryResult.Visible = false;
                    lblShowMsg.Text = "查無此資料";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }
                else
                {
                    if (alert)
                    {
                        btnPrint.Visible = true;
                        BindSortingImg();
                        //msg
                        lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    }
                }


            }
            else
            {
                lblQueryResult.Visible = false;
                GridView3.Visible = false;
                btnPrint.Visible = false;
                string warning = DateTimeCheck.SDATEVSEDATE(txtC_date_s1.Text, txtC_date_s2.Text);
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                alert = false;
            }
        }

        protected void btnSearchLast1_Click(object sender, EventArgs e)
        {
            txt_sqlcom.Text = "2";
            btnSearchLastNum();
        }

        protected void btnSearchLast5_Click(object sender, EventArgs e)
        {
            txt_sqlcom.Text = "3";
            btnSearchLastNum();
        }

        private void btnSearchLastNum()
        {
            bool alert = true;
            GridView3.Visible = true;

            //此一住民
            if (DropDownListIP.SelectedValue.Equals("0"))
            {
                sql += "AND SHIFT_EXCHANGE_RECORD.IP_NO='" + sstr_ipno.Text + "'";

                if (dropP_USER.SelectedValue.ToString() != "-99")//特定評估人員
                {
                    sql += "AND SHIFT_EXCHANGE_RECORD.A_USER='" + dropP_USER.SelectedValue.ToString() + "'";
                }
            }
            else//全部住民 
            {
                if (DropDownListArea.SelectedValue != "-99")//特定區域或樓層
                {
                    sql += "AND ROOM.ROOM_AREA='" + DropDownListArea.SelectedValue.ToString() + "'";
                }

                if (dropP_USER.SelectedValue.ToString() != "-99")//特定評估人員
                {
                    sql += "AND SHIFT_EXCHANGE_RECORD.A_USER='" + dropP_USER.SelectedValue.ToString() + "'";
                }
            }

            txt_sql.Text = sql;
            DataView sortedView = new DataView(BindGridView());
            Session["SortedView"] = sortedView;

            lblQueryResult.Visible = true;
            lblQueryResult.Text = "共" + sortedView.Count + "筆";

            GridView3.DataSource = sortedView;
            GridView3.DataBind();

            if (GridView3.Rows.Count == 0)
            {
                btnPrint.Visible = false;
                lblQueryResult.Visible = false;
                lblShowMsg.Text = "查無此資料";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
            }
            else
            {
                if (alert)
                {
                    btnPrint.Visible = true;
                    BindSortingImg();
                    //msg
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }
            }



        }

        protected void btnSearchLastDay1_Click(object sender, EventArgs e)
        {
            txt_sqlcom.Text = "1";
            string e_date = sqlTime.time();
            string s_date = DateTime.Parse(sqlTime.DateAddSlash(e_date)).ToString("yyyyMMdd");
            btnSearchLastDay(s_date, e_date);          
        }

        protected void btnSearchLastDay3_Click(object sender, EventArgs e)
        {
            txt_sqlcom.Text = "3";
            string e_date = sqlTime.time();
            string s_date = DateTime.Parse(sqlTime.DateAddSlash(e_date)).AddDays(-3).ToString("yyyyMMdd");
            btnSearchLastDay(s_date, e_date);
        }

        protected void btnSearchLastDay7_Click(object sender, EventArgs e)
        {
            txt_sqlcom.Text = "4";
            string e_date = sqlTime.time();
            string s_date = DateTime.Parse(sqlTime.DateAddSlash(e_date)).AddDays(-7).ToString("yyyyMMdd");
            btnSearchLastDay(s_date, e_date);
        }

        protected void btnSearchLastDay14_Click(object sender, EventArgs e)
        {
            txt_sqlcom.Text = "5";
            string e_date = sqlTime.time();
            string s_date = DateTime.Parse(sqlTime.DateAddSlash(e_date)).AddDays(-14).ToString("yyyyMMdd");
            btnSearchLastDay(s_date, e_date);
        }

        private void btnSearchLastDay(string s_date, string e_date)
        {
            bool alert = true;
            
            GridView3.Visible = true;

            //此一住民
            if (DropDownListIP.SelectedValue.Equals("0"))
            {
                sql += "AND SHIFT_EXCHANGE_RECORD.IP_NO='" + Session["ipno"].ToString() + "' AND SHIFT_EXCHANGE_RECORD.A_DATE>='" + s_date + "' AND SHIFT_EXCHANGE_RECORD.A_DATE<='" + e_date + "'";

                if (dropP_USER.SelectedValue.ToString() != "-99")//特定評估人員
                {
                    sql += "AND SHIFT_EXCHANGE_RECORD.A_USER='" + dropP_USER.SelectedValue.ToString() + "'";
                }
            }
            else//全部住民 
            {
                sql += "AND SHIFT_EXCHANGE_RECORD.A_DATE>='" + s_date + "' AND SHIFT_EXCHANGE_RECORD.A_DATE<='" + e_date + "'";

                if (DropDownListArea.SelectedValue != "-99")//特定區域或樓層
                {
                    sql += "AND ROOM.ROOM_AREA='" + DropDownListArea.SelectedValue.ToString() + "'";
                }

                if (dropP_USER.SelectedValue.ToString() != "-99")//特定評估人員
                {
                    sql += "AND SHIFT_EXCHANGE_RECORD.A_USER='" + dropP_USER.SelectedValue.ToString() + "'";
                }
            }

            txt_sql.Text = sql;
            DataView sortedView = new DataView(BindGridView());
            Session["SortedView"] = sortedView;

            lblQueryResult.Visible = true;
            lblQueryResult.Text = "共" + sortedView.Count + "筆";

            GridView3.DataSource = sortedView;
            GridView3.DataBind();

            if (GridView3.Rows.Count == 0)
            {
                btnPrint.Visible = false;
                lblQueryResult.Visible = false;
                lblShowMsg.Text = "查無此資料";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
            }
            else
            {
                if (alert)
                {
                    btnPrint.Visible = true;
                    BindSortingImg();
                    //msg
                    lblShowMsg.Text = sqlTime.NowTimes().Substring(0, 2) + "時" + sqlTime.NowTimes().Substring(2, 2) + "分" + sqlTime.NowTimes().Substring(4, 2) + "秒查詢完成";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }
            }

        }

        private DataTable BindGridView()
        {
            DataTable dt = new DataTable();

            if (txt_sqlcom.Text.Equals("1"))
            {
                dt = sqlShiftExchange.searchSHIFTE_RECORD2(sstr_hid.Text, txt_sql.Text);
            }
            else if (txt_sqlcom.Text.Equals("2"))
            {
                dt = sqlShiftExchange.SearchLastSHIFTE_RECORD2(sstr_hid.Text, txt_sql.Text, "1");
            }
            else if (txt_sqlcom.Text.Equals("3"))
            {
                //dt = sqlShiftExchange.SearchLastSHIFTE_RECORD2(sstr_hid.Text, txt_sql.Text, "5");
                dt = sqlShiftExchange.searchSHIFTE_RECORD2(sstr_hid.Text, txt_sql.Text);
            }
            else if (txt_sqlcom.Text.Equals("4"))
            {
                dt = sqlShiftExchange.searchSHIFTE_RECORD2(sstr_hid.Text, txt_sql.Text);
            }
            else if (txt_sqlcom.Text.Equals("5"))
            {
                dt = sqlShiftExchange.searchSHIFTE_RECORD2(sstr_hid.Text, txt_sql.Text);
            }


            if (dt.Rows.Count != 0)
            {
                DataColumn newColumn = new DataColumn("A_DATETIME");
                newColumn.AllowDBNull = true;
                dt.Columns.Add(newColumn);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string A_DATE = dt.Rows[i]["A_DATE"].ToString();
                    string A_TIME = dt.Rows[i]["A_TIME"].ToString();
                    string tmp = A_DATE + A_TIME;

                    dt.Rows[i]["A_DATETIME"] = tmp;
                }
            }

            return dt;
        }

        protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
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
            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            GridView3.DataSource = sortedView;
            GridView3.DataBind();

            if (GridView3.Rows.Count > 0)
            {
                int columnIndex = 0;
                foreach (DataControlFieldHeaderCell headerCell in GridView3.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression == e.SortExpression)
                    {
                        columnIndex = GridView3.HeaderRow.Cells.GetCellIndex(headerCell);
                        txt_columnIndex.Text = columnIndex.ToString();
                    }
                }
                GridView3.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            }
        }

        private void BindSortingImg()
        {
            if (GridView3.Rows.Count != 0)
            {
                if (direction == SortDirection.Ascending)
                {
                    sortImage.ImageUrl = "~/Image/WebImage/up.png";
                    GridView3.HeaderRow.Cells[Convert.ToInt16(txt_columnIndex.Text)].Controls.Add(sortImage);
                }
                else if (direction == SortDirection.Descending)
                {
                    sortImage.ImageUrl = "~/Image/WebImage/down.png";
                    GridView3.HeaderRow.Cells[Convert.ToInt16(txt_columnIndex.Text)].Controls.Add(sortImage);
                }
            }
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView3.PageIndex = e.NewPageIndex;

            if (Session["SortedView"] != null)
            {
                GridView3.DataSource = Session["SortedView"];
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = BindGridView();
                GridView3.DataBind();
            }

            BindSortingImg();
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "search":
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow SelectedRow = GridView3.Rows[index];
                    string no = ((System.Web.UI.WebControls.Label)GridView3.Rows[index].FindControl("lab_r_id")).Text;
                    ExecSearch(no);
                    PanelData.Enabled = false;
                    MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }

        //開啟或關閉符號表
        protected void btnSymbolsTools_Click(object sender, EventArgs e)
        {
            if (PanelSymbolsTools.Visible == false)
            {
                PanelSymbolsTools.Visible = true;
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                PanelSymbolsTools.Visible = false;
                MultiView1.ActiveViewIndex = 1;
            }
        }
        /*
        //符號表內的各個按鈕按下後將按鈕的Text傳到TextBox
        protected void btn_AccPan_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button b = (System.Web.UI.WebControls.Button)sender;
            txtContent.Text += b.Text;
        }
        */


        

        

        

        
        
        
        
        
        
        

        

        

        
        

       

       
    }
}