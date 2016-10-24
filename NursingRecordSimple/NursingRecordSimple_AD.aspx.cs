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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace longtermcare.NursingRecordSimple
{
    public partial class NursingRecordSimple_AD : System.Web.UI.Page
    {
        public string connection_id;//目前機構
        public string ip_no;//目前住民
        CheckBoxList[] chklp = new CheckBoxList[1000];
        CheckBoxList[] chklp_p = new CheckBoxList[1000];
        TabPanel[] tabid = new TabPanel[2];
        GridView[] Record = new GridView[2];
        protected void Page_Load(object sender, EventArgs e)
        {
            connection_id = Session["H_id"].ToString();
            this.Form.Attributes.Add("autocomplete", "off");
            string language = Session["language"].ToString();
            tab_state_NRSAD.Text = "10";
            if (Session["ipno"] != null)
            {
                ip_no = Session["ipno"].ToString();
                //權限檢查
                //查詢權限表此人在此表單的權限並寫入至session
                //只有查詢權限
                //if(權限session==only query && tab_state_NRSAD.Text=="10")
                //{ 
                //Session["tab_state"] = "11";  
                //}
                //若有新增權限
                //else if(權限session==insert && tab_state_NRSAD.Text=="10")
                //{ 
                tab_state_NRSAD.Text = "12";
                //}
                //else
                //{
                //Session["tab_state"] = "10";
                //}
                if (tab_state_NRSAD.Text == "10")
                {
                    State0();//最原始狀態
                }
                if (tab_state_NRSAD.Text == "11")
                {
                    State1();//只有查詢權限時進入狀態1的按鈕狀態程序
                }
                if (tab_state_NRSAD.Text == "12")
                {
                    if (Session["transintonrs"] != null)
                    {
                        txtContent.Text = Session["transintonrs"].ToString();
                    }
                    Session.Remove("transintonrs");
                    State2();//有新增權限時進入狀態2的按鈕狀態程序
                }
                /*
                if (Session["tab_state"].ToString() == "121")
                {
                    State21();//有選擇健康問題
                }
                 */
                if (tab_state_NRSAD.Text == "13")
                {
                    State3();//新增後進入狀態3的按鈕狀態程序
                }
                if (tab_state_NRSAD.Text == "141")
                {
                    State41();//進入只帶入護理交班的狀態程序
                }
                if (tab_state_NRSAD.Text == "1411")
                {
                    State411();//按下產生護理交班內容的狀態程序
                }
                if (tab_state_NRSAD.Text == "142")
                {
                    State42();//進入只帶入護理計畫的狀態程序
                }
                if (tab_state_NRSAD.Text == "143")
                {
                    State43();//進入帶入護理交班與護理計畫的狀態程序
                }
                if (tab_state_NRSAD.Text == "1431")
                {
                    State431();//進入帶入護理交班的狀態程序
                }
                if (tab_state_NRSAD.Text == "1432")
                {
                    State432();//進入帶入護理計畫的狀態程序
                }
                if (IsPostBack != true)
                {
                    DateTimeShow();
                    //DropDownList3.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), "-999"));
                    //DropDownList3.DataSource = sqlNursingRecordSimple.getDDL3Source(ip_no, connection_id);
                    //DropDownList3.DataBind();
                }
            }
            else
            {
                Response.Write("<script>alert('請先點選住民以便新增');</script>");
            }

        }

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
                    //CurrentlyState();
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                //CurrentlyState();
            }
        }

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
                    //CurrentlyState();
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);
                //CurrentlyState();
            }
        }
        //顯示最近一筆
        protected void Button3_Click(object sender, EventArgs e)
        {
            DataTable dt_lastrecord = sqlNursingRecordSimple.SearchLastNRecord(Session["ipno"].ToString(), Session["H_id"].ToString());
            //DataTable dt_lastfiverecord = sqlNursingRecordSimple.SearchLastFiveNRecord(Session["ipno"].ToString(), Session["H_id"].ToString());
            if (dt_lastrecord.Rows.Count == 0)
            {
                TabContainerRecord.Visible = false;
                lblShowNoSNRecord.Text = "此住民目前無最近護理紀錄";
                lblShowNoSNRecord.Visible = true;
            }
            else
            {
                TabContainerRecord.Visible = true;
                lblShowNoSNRecord.Text = "";
                lblShowNoSNRecord.Visible = false;

                for (int tabno = 0; tabno <= (1 - 1); tabno++)
                {
                    tabid[tabno] = new TabPanel();
                    Record[tabno] = new GridView();
                    tabid[tabno].HeaderText = "此住民最近1筆護理紀錄";
                    Record[tabno].DataSource = dt_lastrecord;

                    Record[tabno].AllowPaging = false;
                    Record[tabno].AutoGenerateColumns = false;
                    Record[tabno].BackColor = System.Drawing.Color.White;
                    Record[tabno].BorderColor = System.Drawing.Color.LightSlateGray;
                    Record[tabno].BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                    Record[tabno].BorderWidth = 1;
                    Record[tabno].CellPadding = 3;

                    string[] SELECT_COMM_COL = new string[] { "R_DATE", "R_TIME", "CONTENT", "CREATE_USER" };
                    string[] SELECT_COMM_COL_NAME = new string[] { "紀錄日期", "紀錄時間", "護理紀錄內容", "紀錄人員" };

                    for (int col = 0; col < 4; col++)
                    {
                        TemplateField field = new TemplateField();
                        field.HeaderTemplate = new Add_GridViewTemplate(DataControlRowType.Header, SELECT_COMM_COL[col].ToString(), SELECT_COMM_COL_NAME[col].ToString());
                        field.ItemTemplate = new Add_GridViewTemplate(DataControlRowType.DataRow, SELECT_COMM_COL[col].ToString(), SELECT_COMM_COL_NAME[col].ToString());
                        Record[tabno].Columns.Add(field);
                    }
                    Record[tabno].FooterStyle.BackColor = System.Drawing.Color.White;
                    Record[tabno].FooterStyle.ForeColor = System.Drawing.Color.MidnightBlue;
                    Record[tabno].HeaderStyle.BackColor = System.Drawing.Color.DarkBlue;
                    Record[tabno].HeaderStyle.Font.Bold = true;
                    Record[tabno].HeaderStyle.ForeColor = System.Drawing.Color.White;
                    Record[tabno].PagerStyle.BackColor = System.Drawing.Color.White;
                    Record[tabno].PagerStyle.ForeColor = System.Drawing.Color.MidnightBlue;
                    Record[tabno].PagerStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                    Record[tabno].RowStyle.ForeColor = System.Drawing.Color.MidnightBlue;
                    Record[tabno].SelectedRowStyle.BackColor = System.Drawing.Color.DarkCyan;
                    Record[tabno].SelectedRowStyle.Font.Bold = true;
                    Record[tabno].SelectedRowStyle.ForeColor = System.Drawing.Color.White;
                    Record[tabno].SortedAscendingCellStyle.BackColor = System.Drawing.Color.LightGray;
                    Record[tabno].SortedAscendingHeaderStyle.BackColor = System.Drawing.Color.Blue;
                    Record[tabno].SortedDescendingCellStyle.BackColor = System.Drawing.Color.Red;
                    Record[tabno].SortedDescendingHeaderStyle.BackColor = System.Drawing.Color.DarkBlue;
                    Record[tabno].DataBind();

                    tabid[tabno].Controls.Add(Record[tabno]);
                    TabContainerRecord.Controls.Add(tabid[tabno]);

                }
                dt_lastrecord.Dispose();
                //dt_lastfiverecord.Dispose();
                TabContainerRecord.ActiveTabIndex = 1;
                btnPreNRecordS.Visible = true;
            }
        }
        //GridView動態產生TemplateField的類別
        public class Add_GridViewTemplate : ITemplate
        {
            private DataControlRowType templateType;
            private string columnName;
            private string columnCName;

            public Add_GridViewTemplate(DataControlRowType type, string colname, string colcname)
            {
                templateType = type;
                columnName = colname;
                columnCName = colcname;
            }

            public void InstantiateIn(System.Web.UI.Control container)
            {  // ITemplate只有一個 InstantiateIn()方法，此方法需要輸入一個控制項
                // 當實作Class時，定義子控制項和樣板所屬的 Control 物件。這些子控制項依次定義在內嵌樣板內。
                switch (templateType)
                {
                    case DataControlRowType.Header:  // GridView表頭
                        Literal literal1 = new Literal();
                        literal1.Text = columnCName;
                        container.Controls.Add(literal1);
                        break;

                    case DataControlRowType.DataRow:  // Gridview資料列
                        System.Web.UI.WebControls.Label data = new System.Web.UI.WebControls.Label();//動態加入 Label
                        data.DataBinding += new EventHandler(data_DataBinding);
                        container.Controls.Add(data);
                        break;

                    default:
                        break;
                }
            }

            private void data_DataBinding(object sender, EventArgs e)
            {
                System.Web.UI.WebControls.Label l = (System.Web.UI.WebControls.Label)sender;
                GridViewRow row = (GridViewRow)l.NamingContainer;
                if (columnCName.ToString().Trim() == "紀錄日期")
                {
                    l.Text = DGFormatRIDDate(DataBinder.Eval(row.DataItem, columnName).ToString());
                }
                else if (columnCName.ToString().Trim() == "紀錄時間")
                {
                    l.Text = DGFormatRIDTime(DataBinder.Eval(row.DataItem, columnName).ToString());
                }
                else if (columnCName.ToString().Trim() == "護理紀錄內容")
                {
                    l.Text = DGFormatRIDContent(DataBinder.Eval(row.DataItem, columnName).ToString());
                }
                else
                {
                    l.Text = DataBinder.Eval(row.DataItem, columnName).ToString();
                }
                row.Dispose();
            }

            private string DGFormatRIDDate(string date)//紀錄日期格式轉換
            {
                return sqlTime.DateAddSlash(date);
            }

            private string DGFormatRIDTime(string time)//紀錄時間格式轉換
            {
                if (time == "")
                {
                    return "";
                }
                else
                {
                    return time.Substring(0, 2) + ":" + time.Substring(2, 2);
                }
            }

            private string DGFormatRIDContent(string content)//紀錄內容之特殊符號轉換
            {
                return SpecialCharactersTransform.strtosc(content);
            }


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
        protected void Add_Click(object sender, EventArgs e)
        {
            string ipno = Session["ipno"].ToString();
            string user = Session["account"].ToString();
            string Rdate = DateCut(txtR_Date.Text);
            string Rtime = TimeCut(txtR_Time.Text);
            try
            {
                int vrdate = Convert.ToInt32(Rdate);
                string datewarning = DateTimeCheck.datevaild(Rdate);
                int vrtime = Convert.ToInt32(Rtime);
                string timewarning = DateTimeCheck.timevaild(Rtime);
                if (datewarning == "日期格式錯誤")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期格式錯誤" + "');", true);
                    //CurrentlyState();
                }
                else
                {
                    if (timewarning == "時間格式錯誤")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "時間格式錯誤" + "');", true);
                        //CurrentlyState();
                    }
                    else
                    {
                        txtNurseExchangeTemp.Text = "";
                        //if (DropDownList3.SelectedItem.Text.Trim() == "請選擇")
                        //{
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "請選擇健康問題" + "');", true);
                        //CurrentlyState();
                        //}
                        //else
                        //{
                        //lblname.Text = DropDownList3.SelectedItem.Text;
                        //lblid.Text = DropDownList3.SelectedValue;
                        //string QuesId = DropDownList3.SelectedValue.ToString();
                        string ShiftExchangeContent = txtNurseExchangeTemp.Text;
                        string content = SpecialCharactersTransform.sctostr(txtContent.Text);
                        string nowdate = sqlTime.time(); //create日期
                        string nowtime = sqlTime.hourminute(); //create時間
                        //string addShiftExchange = cbAddShiftExchange.Checked ? "Y" : "N"; //加入交班
                        //txtNurseExchangeTemp.Text = "護理紀錄(簡單版)\n";
                        txtNurseExchangeTemp.Text = "";
                        /*
                        if (lblname.Text.Trim() == "" || lblname.Text.Trim() == "請選擇")
                        {
                            txtNurseExchangeTemp.Text += "";
                        }
                        else
                        {
                            txtNurseExchangeTemp.Text += "健康問題:" + lblname.Text.Trim() + "\n";
                        }
                        */
                        //txtNurseExchangeTemp.Text += "紀錄日期:" + txtR_Date.Text + "\n";
                        //txtNurseExchangeTemp.Text += "紀錄時間:" + txtR_Time.Text + "\n";
                        //txtNurseExchangeTemp.Text += "紀錄人員:" + sqlNursingRecordSimple.SearchA_user(Session["account"].ToString(), connection_id) + "\n";
                        txtNurseExchangeTemp.Text += "個案狀況及處置:" + txtContent.Text + "\n";
                        //string waring = sqlNursingRecordSimple.insertRecord(ipno, Rdate, Rtime, content, nowdate, nowtime, user, "N", QuesId, connection_id);
                        string waring = sqlNursingRecordSimple.insertRecord(ipno, Rdate, Rtime, content, nowdate, nowtime, user, "N", "", connection_id);
                        if (waring == "新增失敗")
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + waring + "');", true);
                            //CurrentlyState();
                        }
                        else
                        {
                            tab_state_NRSAD.Text = "13";
                            State3();
                            lblShowMsg.Text = "新增成功";
                            ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);

                            //匯出xml
                            //ExportXml();
                        }
                        //}

                    }
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "日期或時間格式錯誤" + "');", true);
                //CurrentlyState();
            }
        }
        //列印
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string connection_id = Session["H_id"].ToString();
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
                string p_user = sqlNursingRecordSimple.SearchA_user(Session["account"].ToString(), connection_id);
                DataTable dt_ip_info = sqlNursingRecordSimple.SearchIPInfo(ip_no, connection_id);
                string ip_name = dt_ip_info.Rows[0]["IP_NAME"].ToString();
                string ip_sex = dt_ip_info.Rows[0]["SEX"].ToString() == "1" ? "男" : "女";

                int dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0]["DOB"].ToString().Trim()));
                int nowyear = Convert.ToInt32(sqlTime.time_year());
                string age = (nowyear - dob).ToString();
                age = age == "-1" ? "" : age;
                string room_bed = dt_ip_info.Rows[0]["ROOM_BED"].ToString();

                dt_ip_info.Dispose();

                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);
                /*
                string pdffilename = "NursingRecordSimple_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()) + ".pdf";
                sqlPDFFooterV2.Headercontent(pdffilename, Session["ipno"].ToString(), Session["account"].ToString());
                pdfWriter.PageEvent = new sqlPDFFooterV2();
                */
                //設定頁首頁尾
                //宣告檔案名稱
                string pdffilename = "NursingRecordSimple_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()) + ".pdf";
                sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                pdfWriter.PageEvent = new sqlPDFFooter();

                //字型設定     
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 10);
                iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
                iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font ChFont4 = new iTextSharp.text.Font(bfChinese, 12);

                Doc.Open();

                // 加入自動列印指令碼
                pdfWriter.AddJavaScript(@"var pp = this.getPrintParams(); pp.interactive = pp.constants.interactionLevel.silent; pp.pageHandling = pp.constants.handling.none;
                var fv = pp.constants.flagValues; pp.flags |= fv.setPageSize; pp.flags |= (fv.suppressCenter | fv.suppressRotate); this.print(pp);");

                //Paragraph Title1 = new Paragraph("單位：" + , ChFont2);
                Paragraph Title2 = new Paragraph(Session["hp_name"] + "\n護理紀錄", ChFont2);
                //Paragraph Title3 = new Paragraph(lblPrintReviseDate.Text, ChFont);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                //Title3.Alignment = 2;//設定左右對齊 0:左 1:置中 2:右               
                PdfPTable title = new PdfPTable(5);
                title.SpacingBefore = 10f;
                title.WidthPercentage = 100;
                PdfPCell[] info = new PdfPCell[5];

                info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                info[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號：" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()), ChFont));
                info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));


                //Doc.Add(Title1);
                Doc.Add(Title2);
                //Doc.Add(Title3);
                //Paragraph Title = new Paragraph("\n", ChFont2);
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
                //Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                //Response.AddHeader("Content-Disposition", "attachment; filename=NursingRecordSimple_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()) + ".pdf");
                Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
                //Response.ContentType = "application/octet-stream";
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                //Response.BinaryWrite(Memory.ToArray());
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                Response.End();

            }
            catch (DocumentException de)
            {
                Response.Write(de.ToString());
            }
            /*
            try
            {
                string connection_id = Session["H_id"].ToString();
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
                string p_user = sqlNursingRecordSimple.SearchA_user(Session["account"].ToString(), connection_id);
                DataTable dt_ip_info = sqlNursingRecordSimple.SearchIPInfo(ip_no, connection_id);
                string ip_name = dt_ip_info.Rows[0]["IP_NAME"].ToString();
                string ip_sex = dt_ip_info.Rows[0]["SEX"].ToString() == "1" ? "男" : "女";

                int dob = Convert.ToInt32(sqlTime.cut(dt_ip_info.Rows[0]["DOB"].ToString().Trim()));
                int nowyear = Convert.ToInt32(sqlTime.time_year());
                string age = (nowyear - dob).ToString();
                age = age == "-1" ? "" : age;
                string room_bed = dt_ip_info.Rows[0]["ROOM_BED"].ToString();

                dt_ip_info.Dispose();

                Document Doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(Doc, Memory);

                string pdffilename = "NursingRecordSimple_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()) + ".pdf";
                sqlPDFFooterV2.Headercontent(pdffilename, Session["ipno"].ToString(), Session["account"].ToString());
                pdfWriter.PageEvent = new sqlPDFFooterV2();

                //字型設定     
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 10);
                iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
                iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font ChFont4 = new iTextSharp.text.Font(bfChinese, 12);

                Doc.Open();

                // 加入自動列印指令碼
                pdfWriter.AddJavaScript(@"var pp = this.getPrintParams(); pp.interactive = pp.constants.interactionLevel.silent; pp.pageHandling = pp.constants.handling.none;
                var fv = pp.constants.flagValues; pp.flags |= fv.setPageSize; pp.flags |= (fv.suppressCenter | fv.suppressRotate); this.print(pp);");

                //Paragraph Title1 = new Paragraph("單位：" + , ChFont2);
                Paragraph Title2 = new Paragraph(Session["hp_name"] + "--護理記錄(簡單版)", ChFont2);
                //Paragraph Title3 = new Paragraph(lblPrintReviseDate.Text, ChFont);
                Title2.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                //Title3.Alignment = 2;//設定左右對齊 0:左 1:置中 2:右               
                PdfPTable title = new PdfPTable(5);
                title.SpacingBefore = 10f;
                title.WidthPercentage = 100;
                PdfPCell[] info = new PdfPCell[5];

                info[0] = new PdfPCell(new iTextSharp.text.Phrase("姓名：" + ip_name, ChFont));
                info[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號：" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()), ChFont));
                info[2] = new PdfPCell(new iTextSharp.text.Phrase("床號：" + room_bed, ChFont));
                info[3] = new PdfPCell(new iTextSharp.text.Phrase("性別：" + ip_sex, ChFont));
                info[4] = new PdfPCell(new iTextSharp.text.Phrase("年齡：" + age, ChFont));


                //Doc.Add(Title1);
                Doc.Add(Title2);
                //Doc.Add(Title3);
                //Paragraph Title = new Paragraph("\n", ChFont2);
                //Doc.Add(Title);
                for (int i = 0; i < info.Length; i++)
                {
                    info[i].BorderWidth = 0;
                    title.AddCell(info[i]);

                }
                Doc.Add(title);

                PdfPTable table = new PdfPTable(new float[] { 1, 1, 1, 1, 1, 1 });
                table.WidthPercentage = 100; //一整頁
                table.SpacingBefore = 5;

                PdfPCell[] cell = new PdfPCell[10];
                cell[0] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄日期", ChFont3));
                cell[0].GrayFill = 0.9f;
                cell[1] = new PdfPCell(new iTextSharp.text.Paragraph(p_date, ChFont4));
                cell[2] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄時間", ChFont3));
                cell[2].GrayFill = 0.9f;
                cell[3] = new PdfPCell(new iTextSharp.text.Paragraph(p_time, ChFont4));
                cell[4] = new PdfPCell(new iTextSharp.text.Paragraph("紀錄人員", ChFont3));
                cell[4].GrayFill = 0.9f;
                cell[5] = new PdfPCell(new iTextSharp.text.Paragraph(p_user, ChFont4));
                cell[6] = new PdfPCell(new iTextSharp.text.Paragraph("健康問題", ChFont3));
                cell[6].GrayFill = 0.9f;
                cell[7] = new PdfPCell(new iTextSharp.text.Paragraph(nursin_questext, ChFont4));
                cell[7].Colspan = 5;
                cell[8] = new PdfPCell(new iTextSharp.text.Paragraph("個案狀況及處置", ChFont3));
                cell[8].GrayFill = 0.9f;
                cell[9] = new PdfPCell(new iTextSharp.text.Paragraph(content + "\n\n\n\n\n\n\n\n\n", ChFont4));
                cell[9].Colspan = 5;

                for (int i = 0; i < cell.Length; i++)
                {
                    table.AddCell(cell[i]);
                }
                Doc.Add(table);
                Doc.Close();
                //Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                //Response.AddHeader("Content-Disposition", "attachment; filename=NursingRecordSimple_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()) + ".pdf");
                Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
                //Response.ContentType = "application/octet-stream";
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                //Response.BinaryWrite(Memory.ToArray());
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                Response.End();

            }
            catch (DocumentException de)
            {
                Response.Write(de.ToString());
            }
            */
        }
        //清除
        protected void btnBack_Click(object sender, EventArgs e)
        {
            tab_state_NRSAD.Text = "12";
            State0();
            State2();
            //Session["tab_state_temp_NRSAD"] = tab_state_NRSAD.Text + "_NursingRecordSimple_AD";
        }
        //判斷要加入護理交班或照護計畫
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "0")//只將這筆護理紀錄加入護理交班
            {
                tab_state_NRSAD.Text = "141";
                State41();
            }
            /*
            if (RadioButtonList1.SelectedValue == "1")//只將這筆護理紀錄代入照護計畫
            {
                if (DropDownList3.SelectedValue.ToString() == "-999")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "此護理紀錄無健康問題,故無法帶入照護計畫" + "');", true);
                    tab_state_NRSAD.Text = "13";
                    State3();
                }
                else
                {
                    tab_state_NRSAD.Text = "142";
                    State42();
                }

            }
            */
            /*
            if (RadioButtonList1.SelectedValue == "2")//將這筆護理紀錄加入護理交班與照護計畫
            {
                if (DropDownList3.SelectedValue.ToString() == "-999")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "此護理紀錄無健康問題,故無法帶入照護計畫" + "');", true);
                    tab_state_NRSAD.Text = "13";
                    State3();
                }
                else
                {
                    tab_state_NRSAD.Text = "143";
                    State43();
                }
            }
            */
            if (RadioButtonList1.SelectedValue == "3")//只將這筆護理紀錄代入照會單
            {
                //if (DropDownList3.SelectedValue.ToString() == "-999")
                //{
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "此護理紀錄無健康問題,故無法帶入照會單" + "');", true);
                    //tab_state_NRSAD.Text = "13";
                    //State3();
                //}
                //else
                //{
                    tab_state_NRSAD.Text = "144";
                    State44();
                //}
            }
            if (RadioButtonList1.SelectedValue == "4")//將這筆護理紀錄加入護理交班與照會單
            {
                tab_state_NRSAD.Text = "145";
                State45();
            }
            /*
            if (RadioButtonList1.SelectedValue == "5")//將這筆護理紀錄加入照會單與護理計畫
            {
                if (DropDownList3.SelectedValue.ToString() == "-999")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "此護理紀錄無健康問題,故無法帶入照護計畫與照會單" + "');", true);
                    tab_state_NRSAD.Text = "13";
                    State3();
                }
                else
                {
                    tab_state_NRSAD.Text = "146";
                    State46();
                }
            }
            */
            /*
            if (RadioButtonList1.SelectedValue == "6")//將這筆護理紀錄加入護理交班與照會單與護理計畫
            {
                if (DropDownList3.SelectedValue.ToString() == "-999")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "此護理紀錄無健康問題,故無法帶入照護計畫與照會單" + "');", true);
                    tab_state_NRSAD.Text = "13";
                    State3();
                }
                else
                {
                    tab_state_NRSAD.Text = "147";
                    State47();
                }
            }
            */
            //Session["tab_state_temp_NRSAD"] = tab_state_NRSAD.Text + "_NursingRecordSimple_AD";
        }
        //加入護理交班
        protected void btnNExchange_Click(object sender, EventArgs e)
        {
            lbladdShiftExchange.Text = "Y";
            if (RadioButtonList1.SelectedValue == "0")
            {
                tab_state_NRSAD.Text = "1411";
                State411();
            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                tab_state_NRSAD.Text = "1431";
                State431();
            }
            if (RadioButtonList1.SelectedValue == "4")
            {
                tab_state_NRSAD.Text = "1451";
                State451();
            }
            if (RadioButtonList1.SelectedValue == "6")
            {
                tab_state_NRSAD.Text = "1471";
                State471();
            }
            //Session["tab_state_temp_NRSAD"] = tab_state_NRSAD.Text + "_NursingRecordSimple_AD";
        }
        //儲存護理交班
        protected void btnInsertNE_Click(object sender, EventArgs e)
        {
            connection_id = Session["H_id"].ToString();
            string user = Session["account"].ToString();
            string Rdate = txtR_Date.Text.Substring(0, 4) + txtR_Date.Text.Substring(5, 2) + txtR_Date.Text.Substring(8, 2);
            string datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            SqlConnection con = new SqlConnection(datasource);
            con.Open();
            string rec_no = "";
            string cmdsearch = "SELECT TOP 1 [NO] FROM NURSE_RECORD_SIMPLE WHERE R_DATE ='" + Rdate + "' AND CREATE_USER='" + user + "' ORDER BY [NO] DESC";
            SqlCommand search = new SqlCommand(cmdsearch, con);
            SqlDataReader searchlist = search.ExecuteReader();
            if (searchlist.Read())
            {
                rec_no = searchlist.GetValue(0).ToString();
            }
            else
            {
                rec_no = "1";
            }
            searchlist.Close();
            string updateRecord = "update NURSE_RECORD_SIMPLE set ShiftExchange='Y' where NO='" + rec_no + "'";
            SqlCommand update1 = new SqlCommand(updateRecord, con);
            update1.ExecuteNonQuery();

            string cdate = sqlTime.time().Substring(0, 8);
            string ctime = sqlTime.datetime().Substring(8, 4);
            string warning;

            try
            {
                string date = txtR_Date.Text.Substring(0, 4) + txtR_Date.Text.Substring(5, 2) + txtR_Date.Text.Substring(8, 2);
                string time = txtR_Time.Text.Substring(0, 2) + txtR_Time.Text.Substring(3, 2);
                string insertRecord1 = "insert into SHIFT_EXCHANGE_RECORD(NRS_NO, IP_NO, NRS_CONTENT, A_DATE, A_TIME, A_USER, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable) values ('" + rec_no + "','" + Session["ipno"].ToString() + "',N'" + SpecialCharactersTransform.sctostr(txtNurseExchangeTemp.Text) + "','" + date + "','" + time + "','" + Session["account"].ToString() + "','" + cdate + "','" + ctime + "','" + Session["account"].ToString() + "','Y')";
                SqlCommand insert1 = new SqlCommand(insertRecord1, con);
                insert1.ExecuteNonQuery();
                //warning = "新增護理交班成功";
                lblShowMsg.Text = "新增護理交班成功";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);

                if (RadioButtonList1.SelectedValue == "0")//只將這筆護理紀錄加入護理交班
                {
                    State0();
                    tab_state_NRSAD.Text = "12";
                    State2();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                if (RadioButtonList1.SelectedValue == "2")//將這筆護理紀錄加入護理交班與護理計畫
                {
                    tab_state_NRSAD.Text = "1432";
                    State432();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                if (RadioButtonList1.SelectedValue == "4")//將這筆護理紀錄加入護理交班與照會單
                {
                    tab_state_NRSAD.Text = "1452";
                    State452();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                if (RadioButtonList1.SelectedValue == "6")//將這筆護理紀錄加入護理交班與照會單與護理計畫
                {
                    tab_state_NRSAD.Text = "1472";
                    State472();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                lbladdShiftExchange.Text = "N";
                txtNurseExchangeTemp.Text = "";
            }
            catch
            {
                //transaction.Rollback();
                warning = "新增護理交班失敗";
                if (RadioButtonList1.SelectedValue == "0")//只將這筆護理紀錄加入護理交班
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                    //CurrentlyState();
                    tab_state_NRSAD.Text = "141";
                    State41();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                if (RadioButtonList1.SelectedValue == "2")//將這筆護理紀錄加入護理交班與護理計畫
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                    //CurrentlyState();
                    tab_state_NRSAD.Text = "143";
                    State43();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                if (RadioButtonList1.SelectedValue == "4")//將這筆護理紀錄加入護理交班與照會單
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                    //CurrentlyState();
                    tab_state_NRSAD.Text = "145";
                    State45();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                if (RadioButtonList1.SelectedValue == "6")//將這筆護理紀錄加入護理交班與照會單與護理計畫
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                    //CurrentlyState();
                    tab_state_NRSAD.Text = "147";
                    State47();
                    //Session["tab_state_temp"] = Session["tab_state"].ToString() + "_NursingRecordSimple_A";
                }
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + warning + "');", true);
                //CurrentlyState();
            }
            finally
            {
                con.Close();
            }
        }
        //放棄護理交班
        protected void btnNurseExchangeClear_Click(object sender, EventArgs e)
        {
            lbladdShiftExchange.Text = "N";
            tab_state_NRSAD.Text = "13";
            State0();
            State3();
            //Session["tab_state_temp_NRSAD"] = tab_state_NRSAD.Text + "_NursingRecordSimple_AD";
        }
        //帶入照護計畫
        protected void btnInsertIntoNP_Click(object sender, EventArgs e)
        {
            /*
            Session["diagid"] = lblid.Text;
            Session["diagname"] = lblname.Text;
            ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "確定要將此健康問題代入照護計畫" + "');window.open('../../NursingPlan/Ques_Sel_D.aspx','','menubar=no, status=no, scrollbars=yes, top=100, left=200, toolbar=no, width=450, height=300, locationbar=false');", true);//另開新視窗(可以)
            //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "確定要將此健康問題代入照護計畫" + "');location.href='../../NursingPlan/Ques_Sel.aspx';", true);
            if (RadioButtonList1.SelectedValue == "1")//只將這筆護理紀錄代入照護計畫
            {
                State0();
                tab_state_NRSAD.Text = "12";
                State2();
            }
            if (RadioButtonList1.SelectedValue == "2")//將這筆護理紀錄加入護理交班與照護計畫
            {
                State0();
                tab_state_NRSAD.Text = "12";
                State2();
            }
            if (RadioButtonList1.SelectedValue == "5")//將這筆護理紀錄加入照會單與照護計畫
            {
                tab_state_NRSAD.Text = "146";
                State46();
            }
            if (RadioButtonList1.SelectedValue == "6")//將這筆護理紀錄加入護理交班與照會單與照護計畫
            {
                tab_state_NRSAD.Text = "1472";
                State472();
            }
            */
        }
        //帶入照護會診單
        protected void btnInsertIntoCT_Click(object sender, EventArgs e)
        {
            DataTable dt_ip_info = sqlNursingRecordSimple.SearchIPInfo(Session["ipno"].ToString(), connection_id);
            //Session["CTSSubject"] = lblname.Text + "(護理記錄-" + dt_ip_info.Rows[0][0].ToString() + "[" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()) + "])";
            Session["CTSSubject"] = "(護理紀錄-" + dt_ip_info.Rows[0][0].ToString() + "[" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, Session["ipno"].ToString()) + "])";
            Session["CTSDescription"] = txtContent.Text;
            dt_ip_info.Dispose();
            //ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "確定要將此健康問題代入照會單" + "');window.open('../../CareTransfer/CT_Sheet_D.aspx','','menubar=no, status=no, scrollbars=yes, top=100, left=200, toolbar=no, width=450, height=300, locationbar=false');", true);//另開新視窗(可以)
            ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "test", "alert('" + "確定要將此護理紀錄代入照會單" + "');window.open('../../CareTransfer/CT_Sheet_D.aspx','','menubar=no, status=no, scrollbars=yes, top=100, left=200, toolbar=no, width=450, height=300, locationbar=false');", true);//另開新視窗(可以)
            if (RadioButtonList1.SelectedValue == "3")//只將這筆護理紀錄代入會診單
            {
                State0();
                tab_state_NRSAD.Text = "12";
                State2();
            }
            if (RadioButtonList1.SelectedValue == "4")//將這筆護理紀錄加入護理交班與會診單
            {
                State0();
                tab_state_NRSAD.Text = "12";
                State2();
            }
            if (RadioButtonList1.SelectedValue == "5")//將這筆護理紀錄加入照會單與照護計畫
            {
                tab_state_NRSAD.Text = "146";
                State46();
            }
            if (RadioButtonList1.SelectedValue == "6")//將這筆護理紀錄加入護理交班與照會單與照護計畫
            {
                tab_state_NRSAD.Text = "1472";
                State472();
            }
        }
        
        //片語:個案狀況及處置
        protected void ibtnContent_Click(object sender, ImageClickEventArgs e)
        {
            sqlPHRASE.connect(connection_id);
            Phrase.sqlPHRASE.connect(connection_id);
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("個案狀況及處置");
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
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
            //CurrentlyState();
        }

        //時間日期顯示
        private void DateTimeShow()
        {
            string nowdate = sqlTime.time();
            txtR_Date.Text = nowdate.Substring(0, 4) + "/" + nowdate.Substring(4, 2) + "/" + nowdate.Substring(6, 2);
            string nowtime = sqlTime.hourminute();
            txtR_Time.Text = nowtime.Substring(0, 2) + ":" + nowtime.Substring(2, 2);
            if (Session["transintonrs_date"] != null)
            {
                txtR_Date.Text = Session["transintonrs_date"].ToString();
            }
            Session.Remove("transintonrs_date");

            if (Session["transintonrs_time"] != null)
            {
                txtR_Time.Text = Session["transintonrs_time"].ToString();
            }
            Session.Remove("transintonrs_time");

        }

        //判斷Session["tab_state"]所處的狀態
        /*
        private void CurrentlyState()
        {
            if (Session["tab_state_temp_NRSAD"] != null)
            {
                if (Session["tab_state_temp_NRSAD"].ToString() == "10_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "10";
                    State0();//最原始狀態
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "11_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "11";
                    State1();//只有查詢權限時進入狀態1的按鈕狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "12_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "12";
                    State2();//有新增權限時進入狀態2的按鈕狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "13_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "13";
                    State3();//新增後進入狀態3的按鈕狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "141_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "141";
                    State41();//進入只帶入護理交班的狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "1411_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "1411";
                    State411();//按下產生護理交班內容的狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "142_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "142";
                    State42();//進入只帶入護理計畫的狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "143_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "143";
                    State43();//進入帶入護理交班與護理計畫的狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "1431_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "1431";
                    State431();//進入帶入護理交班的狀態程序
                }
                else if (Session["tab_state_temp_NRSAD"].ToString() == "1432_NursingRecordSimple_AD")
                {
                    tab_state_NRSAD.Text = "1432";
                    State432();//進入帶入護理計畫的狀態程序
                }
                else
                {
                    Session["tab_state_temp_NRSAD"] = "";
                }
            }
            else
            {
                Session["tab_state_temp_NRSAD"] = "";
            }

        }
        */
        //state 0的狀態
        private void State0()
        {
            DateTimeShow();
            //DropDownList3.SelectedIndex = -1;
            txtContent.Text = "";
        }

        //state 1的狀態<only 查詢>
        private void State1()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            //PanelSymbolsTools.Visible = false;
            //DropDownList3.SelectedIndex = -1;
            Add.Enabled = false;
            btnPrint.Enabled = false;
            btnBack.Enabled = false;
            PanelData1.Enabled = false;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 2的狀態<有新增權限>
        private void State2()
        {
            Button3.Enabled = true;
            PanelData.Enabled = true;
            //PanelSymbolsTools.Visible = false;
            txtContent.Enabled = true;
            ibtnContent.Enabled = true;
            Add.Enabled = true;
            //txtContent.Enabled = false;
            //ibtnContent.Enabled = false;
            //Add.Enabled = false;

            btnPrint.Enabled = false;
            btnBack.Enabled = true;
            PanelData1.Enabled = false;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }
        /*
        //state 2-1的狀態<有選擇健康問題>
        private void State21()
        {
            Button3.Enabled = true;
            btnDitoNR.Enabled = true;
            btnNullFormPrint.Enabled = true;
            btnRule.Enabled = true;
            PanelData.Enabled = true;
         
            txtContent.Enabled = true;
            ibtnContent.Enabled = true;
            Add.Enabled = true;
         
            btnPrint.Enabled = false;
            btnBack.Enabled = true;
            btnGoBackNewAdd.Enabled = false;
            PanelData1.Enabled = false;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            Panel5.Visible = false;
        }
         */
        //state 3的狀態<儲存護理紀錄成功後>
        private void State3()
        {
            Button3.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            RadioButtonList1.SelectedIndex = -1;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 4-1的狀態<只帶入護理交班>
        private void State41()
        {
            Button3.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = true;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 4-1-1的狀態<產生護理交班內容>
        private void State411()
        {
            Button3.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = true;
        }

        //state 4-2的狀態<只帶入照護計畫>
        private void State42()
        {
            Button3.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = true;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 4-3的狀態<帶入護理交班與照護計畫(先交班後計畫)>
        private void State43()
        {
            Button3.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = true;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 4-3-1的狀態<帶入護理交班與照護計畫(交班中)>
        private void State431()
        {
            Button3.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = true;
        }

        //state 4-3-2的狀態<帶入護理交班與照護計畫(交班完成進入計畫)>
        private void State432()
        {
            Button3.Enabled = true;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = true;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 4-4的狀態<只帶入照會單>
        private void State44()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = true;
            Panel5.Visible = false;
        }

        //state 4-5的狀態<將這筆護理紀錄加入護理交班與照會單(先交班後照會)>
        private void State45()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = true;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 4-5-1的狀態<將這筆護理紀錄加入護理交班與照會單(交班中)>
        private void State451()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = true;
        }

        //state 4-5-2的狀態<將這筆護理紀錄加入護理交班與照會單(交班完成進入照會)>
        private void State452()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = true;
            Panel5.Visible = false;
        }

        //state 4-6的狀態<將這筆護理紀錄加入照會單與照護計畫>
        private void State46()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = true;
            btnInsertIntoCT.Enabled = true;
            Panel5.Visible = false;
        }

        //state 4-7的狀態<將這筆護理紀錄加入護理交班與照會單與照護計畫(先交班後照會和計畫)>
        private void State47()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = true;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = false;
        }

        //state 4-7-1的狀態<將這筆護理紀錄加入護理交班與照會單與照護計畫(交班中)>
        private void State471()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = false;
            btnInsertIntoCT.Enabled = false;
            Panel5.Visible = true;
        }

        //state 4-7-2的狀態<將這筆護理紀錄加入護理交班與照會單與照護計畫(交班完成進入照會和計畫)>
        private void State472()
        {
            Button3.Enabled = false;
            PanelData.Enabled = false;
            PanelSymbolsTools.Visible = false;
            Add.Enabled = false;
            btnPrint.Enabled = true;
            btnBack.Enabled = false;
            PanelData1.Enabled = true;
            btnNExchange.Enabled = false;
            btnInsertIntoNP.Enabled = true;
            btnInsertIntoCT.Enabled = true;
            Panel5.Visible = false;
        }

        //匯出XML檔
        public void ExportXml()
        {
            string f_id = SqlMenu.getEMP_CODE("簡易護理紀錄");
            string datetime = sqlTime.DateSplitSlash(txtR_Date.Text) + txtR_Time.Text.Substring(0, 2) + txtR_Time.Text.Substring(3, 2);
            DataSet ds = sqlXML.GetXML_NursingRecord(f_id, "簡易護理紀錄", ip_no, datetime, txtContent.Text);

            try
            {
                string file_name = sqlTime.datetime() + "_" + ip_no + ".xml";
                string xslPath = Server.MapPath("~/XML_Export/XSL/Nursing_Care_XSLT.xsl");

                //原始XML檔
                string xmlPath = Server.MapPath("~/XML_Export/" + file_name);
                FileStream fsout = new FileStream(xmlPath, FileMode.Create, FileAccess.Write);
                XmlTextWriter xtw = new XmlTextWriter(fsout, Encoding.UTF8);
                ds.WriteXml(xtw, XmlWriteMode.WriteSchema);
                xtw.Close();

                //格式轉換
                XPathDocument doc = new XPathDocument(xmlPath);
                XslTransform transform = new XslTransform();
                System.IO.File.Delete(xmlPath);
                transform.Load(xslPath);

                //匯出標準病歷檔
                string filename = Server.MapPath("~/XML_Export/" + file_name);
                FileStream convert_fsout = new FileStream(filename, FileMode.Create, FileAccess.Write);
                XmlTextWriter convert_xtw = new XmlTextWriter(convert_fsout, Encoding.Unicode);
                transform.Transform(doc, null, convert_xtw);
                convert_xtw.Close();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('匯出病歷失敗');", true);
            }

        }

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
    }
}