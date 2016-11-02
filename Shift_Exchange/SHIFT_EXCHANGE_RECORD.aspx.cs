using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
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
    public partial class SHIFT_EXCHANGE_RECORD : System.Web.UI.Page
    {
        string connection_id = "";
        string ip_no = "";
        CheckBoxList[] chklp = new CheckBoxList[1000];
        CheckBoxList[] chklp_p = new CheckBoxList[1000];

        //離開網頁時清除狀態轉換的Session
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD");
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
            if (Session["account"] != null)
            {
                sstr_account.Text = Session["account"].ToString();
            }
            else
            {
                HttpCookie myaccount_Cookie = Request.Cookies["account"];
                Session["account"] = myaccount_Cookie.Values["account"];
                sstr_account.Text = myaccount_Cookie.Values["account"];
            }

            Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "10";

            if (Session["ipno"] != null)
            {
                ip_no = Session["ipno"].ToString();
                sstr_ipno.Text = Session["ipno"].ToString();
                this.Form.Attributes.Add("autocomplete", "off");
                //this.Page.Form.DefaultButton = "ContentPlaceHolder1$btnSearch";
                //權限檢查
                //查詢權限表此人在此表單的權限並寫入至session
                //只有查詢權限
                //if(權限session==only query && Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString()=="10")
                //{ 
                //Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "11"; 
                //}
                //若有新增權限
                //else if(權限session==insert && Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString()=="10")
                //{ 
                Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "12";
                //}
                //else
                //{
                //Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "10";
                //}

                if (Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString() == "10")
                {
                    State0();//最原始狀態
                }
                if (Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString() == "11")
                {
                    State1();//只有查詢權限時進入狀態1的按鈕狀態程序
                }
                if (Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString() == "12")
                {
                    State2();//有新增權限時進入狀態2的按鈕狀態程序
                }

                if (!IsPostBack)
                {
                    //setDateControl();
                    //Bind GridView
                    //GridView1.DataSource = BindGridData(txtS_DATE.Text, txtE_DATE.Text, ip_no);
                    //GridView1.DataBind();
                    DateTimeShow();

                }
            }
            else
            {
                Response.Write("<script>alert('請先點選住民以便新增'); location.href='../../FNursingPlan.aspx'; </script>");
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('請先點選住民以便新增');location.href='../../FNursingPlan.aspx';", true);
            }

            //交班紀錄新增


        }


        //日期時間驗證(javascript取代)
        protected void txtShowDate_TextChanged(object sender, EventArgs e)
        {
            txtContent.Focus();
            try
            {
                string strDate = txtShowDate.Text.Substring(0, 4) + txtShowDate.Text.Substring(5, 2) + txtShowDate.Text.Substring(8, 2);
                int vsdate = Convert.ToInt32(strDate);
                string datewarning = DateTimeCheck.datevaild(strDate);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                    //CurrentlyState();
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                //CurrentlyState();
            }
        }
        protected void txtTime_TextChanged(object sender, EventArgs e)
        {
            txtContent.Focus();
            try
            {
                string strTime = txtTime.Text.Substring(0, 2) + txtTime.Text.Substring(3, 2);
                int vtime = Convert.ToInt32(strTime);
                string timewarning = DateTimeCheck.timevaild(strTime);
                if (timewarning == "時間格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);
                    //CurrentlyState();
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);
                //CurrentlyState();
            }
        }

        //顯示最近交班紀錄
        protected void Button2_Click(object sender, EventArgs e)
        {
            string connection_id = sstr_hid.Text;
            string ipno = sstr_ipno.Text;
            string user = sstr_account.Text;
            string nowdate = sqlTime.DateHaveSlash();
            DateTime e_date = DateTime.ParseExact(nowdate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime s_date = e_date.AddDays(-1);
            string sdate = s_date.ToString("yyyy/MM/dd");
            string edate = e_date.ToString("yyyy/MM/dd");
            sdate = sdate.Substring(0, 4) + sdate.Substring(5, 2) + sdate.Substring(8, 2);
            edate = edate.Substring(0, 4) + edate.Substring(5, 2) + edate.Substring(8, 2);

            //DateTime s_date = e_date.AddDays(-1);

            DataTable dt_lastrecord = sqlShiftExchange.SearchLastSERecord(ipno, user, connection_id, sdate, edate);
            if (Convert.ToInt32(dt_lastrecord.Rows.Count.ToString()) <= 0)
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('" + "此住民目前無最近雜項交班紀錄" + "');location.href='SHIFT_EXCHANGE_RECORD.aspx';", true);
                lblShowNoSERecord.Text = "此住民目前無最近雜項交班紀錄";
                lblShowNoSERecord.Visible = true;
            }
            else
            {
                lblShowNoSERecord.Visible = false;
                lblSEDate.Text = "紀錄日期:" + dt_lastrecord.Rows[0][6].ToString().Substring(0, 4) + "/" + dt_lastrecord.Rows[0][6].ToString().Substring(4, 2) + "/" + dt_lastrecord.Rows[0][6].ToString().Substring(6, 2);
                lblSETime.Text = "紀錄時間:" + dt_lastrecord.Rows[0][7].ToString().Substring(0, 2) + ":" + dt_lastrecord.Rows[0][7].ToString().Substring(2, 2);
                lblSEContent.Text = "內容:" + SpecialCharactersTransform.strtosc((dt_lastrecord.Rows[0][2].ToString().Replace("<br />", "\n")).Replace("&nbsp;", " "));
            }
            dt_lastrecord.Dispose();
            //CurrentlyState();
        }

        //Dito
        protected void btnDito_Click(object sender, EventArgs e)
        {
            DataTable dt_ditorecord = sqlShiftExchange.SearchDitoSERecord(sstr_ipno.Text, sstr_hid.Text);
            if (Convert.ToInt32(dt_ditorecord.Rows.Count.ToString()) <= 0)
            {
                lblShowMsg.Text = "此住民目前無雜項交班紀錄";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel19, typeof(UpdatePanel), "Message", "runEffect();", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "此住民目前無雜項交班紀錄" + "');location.href='SHIFT_EXCHANGE_RECORD.aspx';", true);
            }
            else
            {
                txtContent.Text = SpecialCharactersTransform.strtosc(dt_ditorecord.Rows[0][2].ToString());
            }
            dt_ditorecord.Dispose();
        }

        //列印空白表單
        protected void btnNullFormPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../NullReport/雜項交班紀錄.docx");
            /*
            DataTable dt_ip_info = sqlShiftExchange.SearchIPInfo(ip_no, sstr_hid.Text);
            string strBody = "<html>" + "<head><meta http-equiv=Content-Type content=text/html; charset=utf-8></head>" +
            "<body>" + "<form style=\"width: 18cm\"><div style=\"font-weight: bold; font-size:larger\">單位:" + Session["hp_name"].ToString() + "</div>" +
            "<div><table style=\"font-weight: bold; font-size:larger; text-align: center; width: 18cm\"><tr><td>雜項交班紀錄</td></tr></table></div>" +
            "<div><table style=\"width: 18cm\"><tr><td>姓名:" + dt_ip_info.Rows[0][0].ToString() + "</td><td>家字號:" + "      " + "</td><td>床號:" + dt_ip_info.Rows[0][3].ToString() + "</td><td>性別:" + (dt_ip_info.Rows[0][1].ToString().Trim() == "1" ? "男" : "女") + "</td><td>年齡:" + dt_ip_info.Rows[0][2].ToString().Trim() + "</td></tr></table></div>" +
            "<div><br></div>" +
            "<div><table cellspacing=\"1\" style=\"width: 17cm; border-collapse: collapse; border: 1px solid #000000\">" +
                 "<tr><td style=\"border-style: solid; border-width: 1px; width: 2cm\">紀錄日期</td><td style=\"border-style: solid; border-width: 1px; width: 3.6cm\">&nbsp;</td>" +
                 "<td style=\"border-style: solid; border-width: 1px; width: 2cm\">紀錄時間</td><td style=\"border-style: solid; border-width: 1px; width: 3.6cm\">&nbsp;</td>" +
                 "<td style=\"border-style: solid; border-width: 1px; width: 2cm\">紀錄人員</td><td style=\"border-style: solid; border-width: 1px; width: 3.8cm\">&nbsp;</td></tr>" +
                 "<tr style=\"height: 21cm\"><td style=\"border-style: solid; border-width: 1px; width: 2cm\">交班內容</td>" +
                 "<td style=\"border-style: solid; border-width: 1px\" colspan=\"5\">&nbsp;</td></tr></table></div>" +
            "</form></body>" +
            "</html>";
            string fileName = "ShiftExchangeRecord_" + ip_no + ".doc";
            // You can add whatever you want to add as the HTML and it will be generated as Ms Word docs
            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName);
            Response.Write(strBody);
            */

        }

        //填寫規則
        protected void btnRule_Click(object sender, EventArgs e)
        {
            Response.Redirect("SHIFT_EXCHANGE_RECORD_C.docx");
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "目前施工中!" + "');", true);
            //CurrentlyState();
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
        //儲存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string content = SpecialCharactersTransform.sctostr(txtContent.Text);
            string create_date = DateCut(txtShowDate.Text);//sqlTime.DateSplitSlash(txtShowDate.Text);
            string create_time = TimeCut(txtTime.Text);//txtTime.Text.Substring(0, 2) + txtTime.Text.Substring(3, 2);
            try
            {
                int vdate = Convert.ToInt32(create_date);
                int vtime = Convert.ToInt32(create_time);
                string datewarning = DateTimeCheck.datevaild(create_date);
                string timewarning = DateTimeCheck.timevaild(create_time);
                if (datewarning == "日期格式錯誤")
                {
                    lblShowErrMsg.Text = "日期格式錯誤";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                    //CurrentlyState();
                }
                else
                {
                    if (timewarning == "時間格式錯誤")
                    {
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);
                        lblShowErrMsg.Text = "時間格式錯誤";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                        //CurrentlyState();
                    }
                    else
                    {
                        string create_user = sstr_account.Text;
                        string warning = sqlShiftExchange.addShiftExchange(sstr_ipno.Text, content, create_date, create_time, create_user, sstr_hid.Text);
                        if (warning == "新增失敗")
                        {
                            //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                            lblShowErrMsg.Text = "新增失敗";
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                            //CurrentlyState();
                        }
                        else
                        {
                            lblShowMsg.Text = "新增成功";
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel19, typeof(UpdatePanel), "Message", "runEffect();", true);
                            Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "13";
                            State3();
                            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_SHIFT_EXCHANGE_RECORD";
                        }
                        //lblShowMsg.Text = warning ;
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                        //cleanControl();
                        //btnPrint.Enabled = true;
                    }
                }
            }
            catch
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel12, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                lblShowErrMsg.Text = "日期格式錯誤";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, typeof(UpdatePanel), "Message", "runEffect1();", true);
                //CurrentlyState();
            }
        }
        //列印
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string connection_id = sstr_hid.Text;
                string nowdate = sqlTime.datetime().Substring(0, 8);
                DataTable dt_ip_thisserecord = sqlShiftExchange.SearchIPThisSERecord(sstr_ipno.Text, sstr_account.Text, nowdate, connection_id);
                string r_date = dt_ip_thisserecord.Rows[0][6].ToString().Substring(0, 4) + "/" + dt_ip_thisserecord.Rows[0][6].ToString().Substring(4, 2) + "/" + dt_ip_thisserecord.Rows[0][6].ToString().Substring(6, 2);
                string r_time = dt_ip_thisserecord.Rows[0][7].ToString();
                string r_user = sqlShiftExchange.SearchA_user(dt_ip_thisserecord.Rows[0][8].ToString(), connection_id);
                string content = dt_ip_thisserecord.Rows[0][2].ToString().Replace("<br />", "\n");
                content = content.Replace("&nbsp;", " ");
                content = SpecialCharactersTransform.strtosc(content);
                string print_time = sqlTime.hourminute();
                string p_user = sqlShiftExchange.SearchA_user(sstr_account.Text, connection_id);
                DataTable dt_ip_info = sqlShiftExchange.SearchIPInfo(sstr_ipno.Text, connection_id);
                string ip_name = dt_ip_info.Rows[0][0].ToString();
                string ip_sex = dt_ip_info.Rows[0][1].ToString() == "1" ? "男" : "女";

                int dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0][2].ToString().Trim()));
                int nowyear = Convert.ToInt32(sqlTime.time_year());
                string age = (nowyear - dob).ToString();
                age = age == "-1" ? "" : age;
                string room_bed = ip_sex = dt_ip_info.Rows[0][3].ToString();
                dt_ip_thisserecord.Dispose();

                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                //設定頁首頁尾
                //宣告檔案名稱
                string pdffilename = "NP_ShiftExchRecord_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, sstr_ipno.Text) + ".pdf";
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
                Paragraph Title2 = new Paragraph(Session["hp_name"] + "\n雜項交班記錄", ChFont2);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右               
                PdfPTable title = new PdfPTable(5);
                title.SpacingBefore = 10f;
                title.WidthPercentage = 100;
                PdfPCell[] info = new PdfPCell[8];

                info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                info[1] = new PdfPCell(new iTextSharp.text.Phrase("家字號：" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, sstr_ipno.Text), ChFont));
                info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));
                info[5] = new PdfPCell(new iTextSharp.text.Phrase("列印人員：" + p_user, ChFont));
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

                PdfPTable table = new PdfPTable(new float[] { 1, 1, 1, 1, 1, 1 });
                table.WidthPercentage = 100; //一整頁
                table.SpacingBefore = 5;
                PdfPCell[] cell = new PdfPCell[8];
                cell[0] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄日期", ChFont));
                cell[0].GrayFill = 0.9f;
                cell[1] = new PdfPCell(new iTextSharp.text.Paragraph(r_date, ChFont));
                cell[2] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄時間", ChFont));
                cell[2].GrayFill = 0.9f;
                cell[3] = new PdfPCell(new iTextSharp.text.Paragraph(r_time.Substring(0, 2) + ":" + r_time.Substring(2, 2), ChFont));
                cell[4] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄人員", ChFont));
                cell[4].GrayFill = 0.9f;
                cell[5] = new PdfPCell(new iTextSharp.text.Paragraph(r_user, ChFont));
                cell[6] = new PdfPCell(new iTextSharp.text.Paragraph("交班內容", ChFont));
                cell[6].GrayFill = 0.9f;
                cell[7] = new PdfPCell(new iTextSharp.text.Paragraph(content + "\n\n\n\n\n\n\n\n\n", ChFont));
                cell[7].Colspan = 5;

                for (int i = 0; i < cell.Length; i++)
                {
                    table.AddCell(cell[i]);
                }
                Doc.Add(table);
                Doc.Close();
                Response.Clear();
                //Response.AddHeader("Content-Disposition", "attachment; filename=ShiftExchangeRecord_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf");
                Response.AddHeader("Content-Disposition", "inline; filename=ShiftExchangeRecord_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, sstr_ipno.Text) + ".pdf");
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            cleanControl();
            Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "12";
            State0();
            State2();
            //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_SHIFT_EXCHANGE_RECORD";
        }
        //返回新增
        protected void btnGoBackNewAdd_Click(object sender, EventArgs e)
        {
            Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "10";
            Response.Redirect("SHIFT_EXCHANGE_RECORD.aspx");
        }

        private void cleanControl()
        {
            txtContent.Text = "";
            txtShowDate.Text = sqlTime.DateHaveSlash();
            txtTime.Text = sqlTime.hourminute().Substring(0, 2) + ":" + sqlTime.hourminute().Substring(2, 2);
        }

        protected void LinkBtnGoToAddView_Click(object sender, EventArgs e)
        {
            Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "10";
            Response.Redirect("SHIFT_EXCHANGE_RECORD.aspx");
        }
        protected void LinkBtnGoToUView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View2"] = "20";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View2.aspx");
        }
        protected void LinkBtnGoToAddNRView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View1"] = "10";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View1.aspx");
        }
        protected void LinkBtnGoToQView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View3"] = "20";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View3.aspx");
        }
        protected void LinkBtnGoToMQView_Click(object sender, EventArgs e)
        {
            Session.Contents.Remove("tab_state_SHIFT_EXCHANGE_RECORD");
            Session["tab_state_SHIFT_EXCHANGE_RECORD_View4"] = "30";
            Response.Redirect("SHIFT_EXCHANGE_RECORD_View4.aspx");
        }

        //片語:內容
        protected void ibtnPhraseShiftExchangeContent_Click(object sender, ImageClickEventArgs e)
        {
            sqlPHRASE.connect(sstr_hid.Text);
            Phrase.sqlPHRASE.connect(sstr_hid.Text);
            //string phrase_user = str_account.Text;
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
            //CurrentlyState();
        }

        //時間日期顯示
        private void DateTimeShow()
        {
            txtShowDate.Text = sqlTime.DateHaveSlash();
            txtTime.Text = sqlTime.hourminute().Substring(0, 2) + ":" + sqlTime.hourminute().Substring(2, 2);
        }
        //判斷Session["tab_state"]所處的狀態
        /*
        private void CurrentlyState()
        {
            if (Session["tab_state_SHIFT_EXCHANGE_RECORD"] != null)
            {
                if (Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString() == "10")
                {
                    State0();//最原始狀態
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString() == "11")
                {
                    State1();//只有查詢權限時進入狀態1的按鈕狀態程序
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString() == "12")
                {
                    State2();//有新增權限時進入狀態2的按鈕狀態程序
                }
                else if (Session["tab_state_SHIFT_EXCHANGE_RECORD"].ToString() == "13")
                {
                    State3();//新增後進入狀態3的按鈕狀態程序
                }
            }
            else
            {
                Session["tab_state_SHIFT_EXCHANGE_RECORD"] = "";
            }
            
            if (Session["tab_state_temp"] != null)
            {
                if (Session["tab_state_temp"].ToString() == "10_SHIFT_EXCHANGE_RECORD")
                {
                    Session["tab_state"] = "10";
                    State0();//最原始狀態
                }
                else if (Session["tab_state_temp"].ToString() == "11_SHIFT_EXCHANGE_RECORD")
                {
                    Session["tab_state"] = "11";
                    State1();//只有查詢權限時進入狀態1的按鈕狀態程序
                }
                else if (Session["tab_state_temp"].ToString() == "12_SHIFT_EXCHANGE_RECORD")
                {
                    Session["tab_state"] = "12";
                    State2();//有新增權限時進入狀態2的按鈕狀態程序
                }
                else if (Session["tab_state_temp"].ToString() == "13_SHIFT_EXCHANGE_RECORD")
                {
                    Session["tab_state"] = "13";
                    State3();//新增後進入狀態3的按鈕狀態程序
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
        //state 0的狀態
        private void State0()
        {
            DateTimeShow();
            txtContent.Text = "";
        }
        //state 1的狀態<only 查詢>
        private void State1()
        {
            Button2.Enabled = false;
            btnDito.Enabled = false;
            btnNullFormPrint.Enabled = true;
            btnRule.Enabled = true;
            PanelData.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnClear.Enabled = false;
            btnGoBackNewAdd.Enabled = false;
        }

        //state 2的狀態<有新增權限>
        private void State2()
        {
            Button2.Enabled = true;
            btnDito.Enabled = true;
            btnNullFormPrint.Enabled = true;
            btnRule.Enabled = true;
            PanelData.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
            btnClear.Enabled = true;
            btnGoBackNewAdd.Enabled = true;
        }
        //state 3的狀態<儲存護理紀錄成功後>
        private void State3()
        {
            Button2.Enabled = true;
            btnDito.Enabled = false;
            btnNullFormPrint.Enabled = true;
            btnRule.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = true;
            btnClear.Enabled = false;
            btnGoBackNewAdd.Enabled = true;
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
        #region 修改護理交班
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
        #region 新增護理交班
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 0;
        }
        #endregion

        //開啟或關閉符號表
        protected void btnSymbolsTools_Click(object sender, EventArgs e)
        {
            if (PanelSymbolsTools.Visible == false)
            {
                PanelSymbolsTools.Visible = true;
            }
            else
            {
                PanelSymbolsTools.Visible = false;
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


        protected void btn_recent_Click(object sender, ImageClickEventArgs e)
        {
            sqlRecent.connect(connection_id);
            int selectvalue = 1;
            TabContainer1.Visible = true;
            TabContainer1.ScrollBars = System.Web.UI.WebControls.ScrollBars.Vertical;
            DataSet FormSet = sqlRecent.ReturnForm();

            TabPanel tabid = new TabPanel();
            tabid.HeaderText = "此住民最近三筆照護記錄";
            chklp[0] = new CheckBoxList();
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Vertical;
            chklp[0].RepeatLayout = RepeatLayout.Table;

            System.Web.UI.WebControls.Label Description1 = new System.Web.UI.WebControls.Label();
            Description1.Text = "此住民目前無最近照護記錄";
            Description1.ForeColor = System.Drawing.Color.Red;
            Description1.Font.Size = 10;
            Description1.Font.Bold = true;

            if (FormSet.Tables.Count > 0)
            {
                int total_count = 0;

                foreach (DataRow row in FormSet.Tables[0].Rows)
                {
                    DataSet Data_Set = sqlRecent.Return3Data(row["SELECT_COMM_COL"].ToString(), row["SELECT_COMM_TABLE"].ToString(), row["SELECT_COMM_ORDERBY_DATE"].ToString(), row["NO_STRING"].ToString(), ip_no, sqlTime.DateSplitSlash(txtShowDate.Text));

                    for (int j = 0; j < Data_Set.Tables[0].Rows.Count; j++)
                    {
                        if (Data_Set.Tables[0].Rows.Count > 0)
                        {

                            string[] table_name = row["SELECT_COMM_COL_NAME"].ToString().Split(',');
                            string[] table = new string[table_name.Length];
                            string record = "●" + row["FORM_NAME"].ToString();
                            int count = 0;

                            for (int i = 0; i < table.Length; i++)
                            {
                                table[i] = Data_Set.Tables[0].Rows[j][i].ToString().Trim();

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
                            }


                            if (count > 0)
                            {
                                chklp[0].Items.Add(record);
                                chklp[0].DataBind();
                                total_count++;
                            }
                            
                        }
                    }
                    Data_Set.Dispose();
                }

                Description1.Visible = total_count <= 0 ? true : false;
            }
            FormSet.Dispose();

            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]);
            tabid.Controls.Add(Description1);
            TabContainer1.Controls.Add(tabid);
            TabContainer1.ActiveTabIndex = selectvalue;
        }



    }
}