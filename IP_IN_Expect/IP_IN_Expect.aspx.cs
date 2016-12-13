using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using AjaxControlToolkit;

namespace longtermcare.IPInOut
{
    public partial class IP_IN_Expect : System.Web.UI.Page
    {
        private static string datasource;
        private static string connection_id;
        //private string sql = "SELECT I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, CONVERT(VARCHAR(10), CAST(I.DOB AS DATETIME), 111) AS DOB, R.ROOM_BED FROM IP_INFORMATION as I Left Outer Join IN_RECORD as R ON I.IP_NO = R.IP_NO WHERE (R.OUT_DATE IS NULL) AND (I.AccountEnable = 'Y')";
        
        private string sql1;
        private string sql2;
        private string sql3;
        private string sql4;
        private string sql5;

        CheckBoxList[] chklp = new CheckBoxList[1000];
        CheckBoxList[] chklp_p = new CheckBoxList[1000];

        protected void Page_Load(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 0;
            connection_id = Session["H_id"].ToString();
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            SqlIPInOut.connect(connection_id);
            SqlDataSource1.ConnectionString = datasource;
            SqlPotentialList.ConnectionString = datasource;

            //this.txtinput.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) {" + this.ClientScript.GetPostBackEventReference(this.btninput, "") + "}");
            this.txtipno.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
            this.txtexindate.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");
            this.txtExpectNote.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }");

            this.txtipno.Focus(); //預設焦點

            string language = Session["language"].ToString();
            this.Form.Attributes.Add("autocomplete", "off");
            Label6.Text = Language.Language.translate("IP_NO", language);
            Label7.Text = Language.Language.translate("IP_Name", language);
            Label8.Text = Language.Language.translate("IP_ID", language);
            Label3.Text = Language.Language.translate("IP_NO", language);
            Label4.Text = Language.Language.translate("IP_ID", language);
            Label5.Text = Language.Language.translate("Birthday", language);
            Label1.Text = Language.Language.translate("IP_Name", language);
            Label9.Text = Language.Language.translate("Sex", language);
            Label19.Text = Language.Language.translate("Note", language);
            Label17.Text = Language.Language.translate("ExpectInDate", language);
            btninput.Text = Language.Language.translate("Input", language);
            btnConfirm.Text = Language.Language.translate("Confirm", language);
            Label2.Text = Language.Language.translate("ExpectINHOS", language);
        }

        //性別轉文字ConvertSex
        protected string ConvertSex(string str)
        {
            if (str.Trim() == "1")
            {
                str = "男";
                return str;
            }
            else if (str.Trim() == "2")
            {
                str = "女";
                return str;
            }
            else
            {
                str = "未填寫";
                return str;
            }
        }
        
        //日期轉文字
        protected string ConvertDate(string str)
        {
            if (str.Trim() == "" | str == null)
            {
                str = "";
                return str;
            }
            else
                return sqlTime.DateAddSlash(str);
        }

        //輸入
        protected void btnInput_Click(object sender, EventArgs e)
        {
            Label21.Text = "";

            string IDsearch = ""; //SqlIPInOut.IDselect(txtipno.Text.Trim()).Trim();

            if (txtipno.Text == "" & txtIPName.Text == "" & txtIPID.Text == "" & txtDOB.Text == "" & txtRoomBed.Text == "")
            {
                Label21.Text = "請輸入住民號碼 或姓名 或身分證號 或生日 或床號！";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + Label21.Text + "');", true);
            }
            if (txtipno.Text != "")
            {
                IDsearch = SqlIPInOut.IDselect(txtipno.Text.Trim()).Trim();
                if (IDsearch == "")
                {
                    Label21.Text = "找不到紀錄。請輸入完整的住民號碼，或輸入其他資料繼續查詢。";
                    txtipno.Text = "";
                    txtIPName.Text = "";
                    txtIPID.Text = "";
                    txtDOB.Text = "";
                    txtRoomBed.Text = "";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + Label21.Text + "');", true);
                }
                else
                {
                    sql1 = "AND I.IP_NO LIKE '%" + IDsearch + "%' ";
                }
            }
            else
            {
                sql1 = "";
            }
            if (txtIPName.Text == "")
                sql2 = "";
            else
                sql2 = "AND I.IP_NAME LIKE '%" + txtIPName.Text + "%' ";
            if (txtIPID.Text == "")
                sql3 = "";
            else
                sql3 = "AND I.IP_ID LIKE '%" + txtIPID.Text + "%' ";
            if (txtDOB.Text == "")
                sql4 = "";
            else
                sql4 = "AND I.DOB LIKE '%" + txtDOB.Text + "%' ";
            if (txtRoomBed.Text == "")
                sql5 = "";
            else
                sql5 = "AND ROOM.ROOM_BED LIKE '%" + txtRoomBed.Text + "%' ";

            SqlDataSource1.ConnectionString = datasource;
            SqlDataSource1.SelectCommand = SqlIPInOut.GetSqlcom(sql1, sql2, sql3, sql4, sql5);
            gvwIPList.DataBind();

            if (Convert.ToInt32(gvwIPList.Rows.Count.ToString()) == 0)
            {
                //string nullrec = txtipno.Text + txtIPName.Text + txtIPID.Text + txtDOB.Text + txtRoomBed.Text;
                Label21.Text = "無此住民資訊！";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + Label21.Text + "');", true);

                txtipno.Text = "";
                txtIPName.Text = "";
                txtIPID.Text = "";
                txtDOB.Text = "";
                txtRoomBed.Text = "";
                lblWARNINGP_DATE.Text = "";
                Label20.Text = "";
                Label21.Text = "";

                sql1 = "";
                sql2 = "";
                sql3 = "";
                sql4 = "";
                sql5 = "";
                SqlDataSource1.SelectCommand = SqlIPInOut.GetSqlcom(sql1, sql2, sql3, sql4, sql5);
                gvwIPList.DataBind();
                gvwIPList.SelectedIndex = -1;
            }
            else
            {
                gvwIPList.Visible = true;
            }
        }
        //清除
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtipno.Text = "";
            txtIPName.Text = "";
            txtIPID.Text = "";
            txtDOB.Text = "";
            txtRoomBed.Text = "";
            lblWARNINGP_DATE.Text = "";
            Label20.Text = "";
            Label21.Text = "";

            sql1 = "";
            sql2 = "";
            sql3 = "";
            sql4 = "";
            sql5 = "";

            string IDsearch = SqlIPInOut.IDselect(txtipno.Text.Trim()).Trim();
            SqlDataSource1.ConnectionString = datasource;
            SqlDataSource1.SelectCommand = SqlIPInOut.GetSqlcom(sql1, sql2, sql3, sql4, sql5);
            gvwIPList.Visible = false;
            gvwIPList.DataBind();
            gvwIPList.SelectedIndex = -1;
        }

        //儲存
        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            //檢查是否輸入必填欄位
            string unFillItem = "";

            if (CheckRequiredFields(out unFillItem))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "tip", "<script>alert('儲存失敗。請填寫必填欄位：\\n" + unFillItem + "');</script>", false);
            }
            else
            {
                string ipno = lblipno.Text;
                string revdate = sqlTime.DateSplitSlash(TextBox3.Text); //預約日期
                string exindate = sqlTime.DateSplitSlash(txtexindate.Text);//預期入住日期
                string exinnote = txtExpectNote.Text;
                string createuser = Session["account"].ToString();
                string nowdate = sqlTime.time();
                string creattTime = SqlIPInOut.gettime().Trim();
                SqlIPInOut.ExpectIn(ipno, revdate, exindate, exinnote, nowdate, creattTime, createuser);
                string warning = (SqlIPInOut.GetWarning());

                if (warning == "已完成預約入院")
                {
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + warning + "');", true);
                    txtipno.Text = "";
                    txtIPName.Text = "";
                    txtIPID.Text = "";
                    txtDOB.Text = "";
                    txtRoomBed.Text = "";
                    lblWARNINGP_DATE.Text = "";
                    Label20.Text = "";
                    Label21.Text = "";
                    MultiView1.ActiveViewIndex = 0;
                    lblShowMsg.Text = warning;
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('預約入住失敗');", true);
                    lblShowMsg.Text = "預約入住失敗";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                }

            }

        }

        //選取Gridview中的紀錄, 並將資料代入View2
        protected void gvwIPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlIPInOut.inputipno(gvwIPList.SelectedValue.ToString());
            lblipno.Text = SqlIPInOut.GetIp_no();
            lblipno2.Text = SqlIPInOut.IDsearch(SqlIPInOut.GetIp_no());
            lblipid.Text = SqlIPInOut.GetIp_id();
            lblipname.Text = SqlIPInOut.GetIp_name();
            lblipbirth.Text = sqlTime.DateAddSlash(SqlIPInOut.GetIp_birth());
            if (SqlIPInOut.GetIp_sex().Trim() == "1")
            {
                lblipsex.Text = "男";
            }
            else
            {
                lblipsex.Text = "女";
            }

            if (lblipno.Text != "")
            {
                SqlIPInOut.haveip(lblipno.Text);
                int checkip = SqlIPInOut.GetIp_check();
                if (checkip == 1)//此住民已經入住了
                {
                    lblWARNINGP_DATE.Text = SqlIPInOut.GetWarning() + "入住日期為：" + sqlTime.DateAddSlash(SqlIPInOut.GetIp_indate());
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNINGP_DATE.Text + "');", true);
                    lblipinstate.Text = "已經入住";
                    Label17.Text = "入住日期";
                    txtexindate.ReadOnly = true;
                    txtexindate.ForeColor = Color.Red;
                    txtexindate.Text = sqlTime.DateAddSlash(SqlIPInOut.GetIp_indate());
                    Panel1.Enabled = false;
                    btnConfirm.Enabled = false;
                }
                else
                {
                    //SqlIPInOut.CheckIPInExpect(lblipno.Text);
                    string checkipexpect = SqlIPInOut.CheckIPInExpect(lblipno.Text);//SqlIPInOut.GetIp_check();

                    if (checkipexpect == "1")//判斷住民是否已預約
                    {
                        string ExpectInIDsearch = SqlIPInOut.ExpectInIDsearch(lblipno.Text).Trim(); //查此一住民是否存在預約紀錄中及是否已入住

                        if (ExpectInIDsearch != "")//已預約且尚未入住
                        {
                            lblWARNINGP_DATE.Text = "此住民已經預約入住。預期入住日期為：" + sqlTime.DateAddSlash(SqlIPInOut.GetIp_exindate());
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNINGP_DATE.Text + "');", true);
                            lblipinstate.Text = "已經預約入住";
                            TextBox3.Text = sqlTime.DateAddSlash(SqlIPInOut.GetIp_revdate());
                            Label17.Text = "預約入住日期";
                            txtexindate.ReadOnly = true;
                            txtexindate.ForeColor = Color.Red;
                            txtexindate.Text = sqlTime.DateAddSlash(SqlIPInOut.GetIp_exindate());
                            txtExpectNote.Text = SqlIPInOut.GetIp_innote();
                            Panel1.Enabled = false;
                            btnConfirm.Enabled = false;
                        }
                        else
                        {
                            lblWARNINGP_DATE.Text = "";
                            lblipinstate.Text = "尚未預約且尚未入住";
                            Label17.Text = "預期入住日期";
                            txtexindate.ReadOnly = false;
                            txtexindate.ForeColor = Color.Black;
                            txtexindate.Text = "";
                            Panel1.Enabled = true;
                            //預約日期-預設為今日
                            string nowdatetime = sqlTime.datetime();
                            TextBox3.Text = nowdatetime.Substring(0, 4) + "/" + nowdatetime.Substring(4, 2) + "/" + nowdatetime.Substring(6, 2);
                            this.TextBox3.Focus(); //預設焦點
                            btnConfirm.Enabled = true;
                        }
                    }
                    else
                    {
                        lblWARNINGP_DATE.Text = "";
                        lblipinstate.Text = "尚未預約且尚未入住";
                        Label17.Text = "預期入住日期";
                        txtexindate.ReadOnly = false;
                        txtexindate.ForeColor = Color.Black;
                        txtexindate.Text = "";
                        Panel1.Enabled = true;
                        //預約日期-預設為今日
                        string nowdatetime = sqlTime.datetime();
                        TextBox3.Text = nowdatetime.Substring(0, 4) + "/" + nowdatetime.Substring(4, 2) + "/" + nowdatetime.Substring(6, 2);
                        this.TextBox3.Focus(); //預設焦點
                        btnConfirm.Enabled = true;
                    }
                }
                MultiView1.ActiveViewIndex = 1;
            }
        }

        //返回
        protected void Button2_Click(object sender, EventArgs e)
        {
            txtipno.Text = "";
            txtIPName.Text = "";
            txtIPID.Text = "";
            txtDOB.Text = "";
            txtRoomBed.Text = "";
            lblWARNINGP_DATE.Text = "";
            Label20.Text = "";
            Label21.Text = "";
            TextBox3.Text = "";
            txtexindate.Text = "";
            txtExpectNote.Text = "";

            sql1 = "";
            sql2 = "";
            sql3 = "";
            sql4 = "";
            sql5 = "";

            string IDsearch = SqlIPInOut.IDselect(txtipno.Text.Trim()).Trim();
            SqlDataSource1.ConnectionString = datasource;
            SqlDataSource1.SelectCommand = SqlIPInOut.GetSqlcom(sql1, sql2, sql3, sql4, sql5);
            gvwIPList.Visible = false;
            gvwIPList.DataBind();
            gvwIPList.SelectedIndex = -1;

            MultiView1.ActiveViewIndex = 0;
        }

        //Link到新增住民預約入住
        protected void LinkBtnGoToAddView_Click(object sender, EventArgs e)
        {
            Response.Redirect("IP_IN_Expect.aspx");
        }

        //Link到查詢/修改住民預約入住
        protected void LinkBtnGoToQUView_Click(object sender, EventArgs e)
        {
            Response.Redirect("IP_IN_Expect_MM.aspx");
        }

        //Linkto(區間)查詢/列印住民預約入住
        protected void LinkBtnGoToRegionQueryView_Click(object sender, EventArgs e)
        {
            Response.Redirect("IP_IN_Expect_RQ.aspx");
        }

        //GridView分頁處理
        protected void CustomersGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            //string sql = "SELECT I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, CONVERT(VARCHAR(10), CAST(I.DOB AS DATETIME), 111) AS DOB, R.ROOM_BED FROM IP_INFORMATION as I Left Outer Join IN_RECORD as R ON I.IP_NO = R.IP_NO WHERE (R.OUT_DATE IS NULL) AND (I.AccountEnable = 'Y')";
            string sql = "SELECT I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, I.DOB, R.ROOM_BED FROM IP_INFORMATION as I Left Outer Join ROOM as R ON I.IP_NO = R.IP_NO WHERE (I.AccountEnable = 'Y') order by I.IP_NO";
            SqlDataSource1.SelectCommand = sql + sql1 + sql2 + sql3 + sql4 + sql5;
            gvwIPList.DataBind();
        }
        
        //檢查日期格式
        protected void txtexindate_TextChanged(object sender, EventArgs e)
        {
            if (txtexindate.Text == "")
            {
                lblWARNINGP_DATE.Text = "請輸入日期";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNINGP_DATE.Text + "');", true);
            }
            else
            {
                try
                {
                    string exdate = txtexindate.Text.Substring(0, 4) + txtexindate.Text.Substring(5, 2) + txtexindate.Text.Substring(8, 2);
                    int vdate = Convert.ToInt32(exdate);
                    string datewarning = DateTimeCheck.datevaild(exdate);
                    if (datewarning == "日期格式錯誤")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + datewarning + "');", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('日期格式錯誤！輸入格式：西元年/月/日（yyyy/MM/dd）');", true);
                }
            }
        }

        //現有住民預約
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
        
        //非現有住民預約入住
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }
        
        //檢查預期入住日期-訪客預約
        protected void txtexpindate_TextChanged(object sender, EventArgs e)
        {
            if (txtexpindate.Text == "")
            {
                lblWARNINGP_DATE.Text = "請輸入日期";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNINGP_DATE.Text + "');", true);
            }
            else
            {
                try
                {
                    string exdate = txtexpindate.Text.Substring(0, 4) + txtexpindate.Text.Substring(5, 2) + txtexpindate.Text.Substring(8, 2);
                    int vdate = Convert.ToInt32(exdate);
                    string datewarning = DateTimeCheck.datevaild(exdate);
                    if (datewarning == "日期格式錯誤")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + datewarning + "');", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('日期格式錯誤！輸入格式：西元年/月/日（yyyy/MM/dd）');", true);
                }
            }
        }
        
        //選取訪客紀錄gvwPotentialList
        protected void gvwPotentialList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlIPInOut.GetPotentialExpectData(gvwPotentialList.SelectedDataKey.Values[0].ToString(), gvwPotentialList.SelectedDataKey.Values[1].ToString());
                
                lblpoipid.Text = SqlIPInOut.GetIp_id();
                lblpoipname.Text = SqlIPInOut.GetIp_name();
                if (SqlIPInOut.GetIp_sex() == "1")
                {
                    lblopipsex.Text = "男";
                }
                else
                {
                    lblopipsex.Text = "女";
                }
                lblpoipbirth.Text = sqlTime.DateAddSlash(SqlIPInOut.GetIp_birth());
                lblipinreason.Text = SqlIPInOut.FindIPInReason(SqlIPInOut.GetIp_inreason());

                //檢查是否已預約
                //SqlIPInOut.CheckPIPInExpect(gvwPotentialList.SelectedDataKey.Values[0].ToString());
                string checkpipexpect = SqlIPInOut.CheckPIPInExpect(gvwPotentialList.SelectedDataKey.Values[0].ToString()); ; // SqlIPInOut.GetIp_check();

                if (checkpipexpect == "1")
                {
                    SqlIPInOut.GetPIPExpectData(gvwPotentialList.SelectedDataKey.Values[0].ToString());

                    if (SqlIPInOut.GetIp_incheck() == "Y")
                    {
                        //已入住
                        lblWARNING_DATE.Text = "此潛在住民已經入住";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNING_DATE.Text + "');", true);
                    }
                    else
                    {
                        lblWARNING_DATE.Text = "此潛在住民已經預約入住。預期入住日期為：" + sqlTime.DateAddSlash(SqlIPInOut.GetIp_exindate());
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNING_DATE.Text + "');", true);
                    }
                    TextBox1.Text = sqlTime.DateAddSlash(SqlIPInOut.GetIp_revdate());
                    Label34.Text = "預約入住日期";
                    txtexpindate.ReadOnly = true;
                    txtexpindate.ForeColor = Color.Red;
                    txtexpindate.Text = sqlTime.DateAddSlash(SqlIPInOut.GetIp_exindate());
                    txtpoipinNote.Text = SqlIPInOut.GetIp_innote();
                    Panel2.Enabled = false;
                    Button4.Enabled = false;
                    
                }
                else
                {
                    lblWARNING_DATE.Text = "";
                    Label34.Text = "預期入住日期";
                    txtexpindate.ReadOnly = false;
                    txtexpindate.ForeColor = Color.Black;
                    txtexpindate.Text = "";
                    Panel2.Enabled = true;

                    //預約日期-預設為今日
                    string nowdatetime = sqlTime.datetime();
                    TextBox1.Text = nowdatetime.Substring(0, 4) + "/" + nowdatetime.Substring(4, 2) + "/" + nowdatetime.Substring(6, 2);
                    this.TextBox1.Focus(); //預設焦點
                    Button4.Enabled = true;
                }

                MultiView1.ActiveViewIndex = 3;
            }
            catch
            {
                Label37.Text = "訪客資料讀取錯誤！";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + Label37.Text + "');", true);
            }
        }
        
        //返回查詢-訪客頁面
        protected void Button5_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            txtexpindate.Text = "";
            txtpoipinNote.Text = "";
            Panel2.Enabled = true;
            gvwPotentialList.DataBind();
            gvwPotentialList.SelectedIndex = -1;

            MultiView1.ActiveViewIndex = 2;
        }
        
        //儲存訪客預約入住紀錄
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                //檢查是否輸入必填欄位
                string unFillItem = "";

                if (CheckRequiredFields2(out unFillItem))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "tip", "<script>alert('儲存失敗。請填寫必填欄位：\\n" + unFillItem + "');</script>", false);
                }
                else
                {
                    string no = gvwPotentialList.SelectedDataKey.Values[0].ToString();
                    string ipname = gvwPotentialList.SelectedDataKey.Values[1].ToString();
                    //預約日期, 預期入住日期及備註
                    string revdate = sqlTime.DateSplitSlash(TextBox1.Text);
                    string expindate = sqlTime.DateSplitSlash(txtexpindate.Text);
                    string expinnote = txtpoipinNote.Text;

                    string createuser = Session["account"].ToString();
                    string createdate = sqlTime.time();
                    string createTime = SqlIPInOut.gettime().Trim();
                    string warning1;
                    string warning2;

                    //更新訪客資料表
                    SqlIPInOut.UpdatePotentialIPInfo(no, ipname, expindate, expinnote, createdate, createTime, createuser);
                    warning1 = SqlIPInOut.GetWarning();

                    //存入預約入住表PotentExpectIn(string tempipno, string exindate, string exinnote, string createdate, string createtime, string createuser)
                    SqlIPInOut.PotentExpectIn(no, revdate, expindate, expinnote, createdate, createTime, createuser);
                    warning2 = SqlIPInOut.GetWarning();

                    if (warning1 == "訪客預約完成" & warning2 == "已完成預約入住")
                    {
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('完成訪客預約入住！');location.href='IP_IN_Expect.aspx';", true);
                        lblShowMsg.Text = warning2;
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                        Panel2.Enabled = false;
                        Button4.Enabled = false;
                    
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('預約失敗！');", true);
                        lblShowMsg.Text = "預約失敗！";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    }
                }

            }
            catch
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('預約失敗！請確認資料填寫正確。');", true);
                lblShowMsg.Text = "預約失敗！請確認資料填寫正確！";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
            }

        }

        //檢查預約日期
        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (TextBox3.Text == "")
            {
                lblWARNINGP_DATE.Text = "請輸入預約日期";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNINGP_DATE.Text + "');", true);
            }
            else
            {
                try
                {
                    string exdate = TextBox3.Text.Substring(0, 4) + TextBox3.Text.Substring(5, 2) + TextBox3.Text.Substring(8, 2);
                    int vdate = Convert.ToInt32(exdate);
                    string datewarning = DateTimeCheck.datevaild(exdate);
                    if (datewarning == "日期格式錯誤")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + datewarning + "');", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('日期格式錯誤！輸入格式：西元年/月/日（yyyy/MM/dd）');", true);
                }
            }
        }

        //檢查預約日期-訪客
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                lblWARNINGP_DATE.Text = "請輸入預約日期";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + lblWARNINGP_DATE.Text + "');", true);
            }
            else
            {
                try
                {
                    string exdate = TextBox1.Text.Substring(0, 4) + TextBox1.Text.Substring(5, 2) + TextBox1.Text.Substring(8, 2);
                    int vdate = Convert.ToInt32(exdate);
                    string datewarning = DateTimeCheck.datevaild(exdate);
                    if (datewarning == "日期格式錯誤")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + datewarning + "');", true);
                    }
                }
                catch
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('日期格式錯誤！輸入格式：西元年/月/日（yyyy/MM/dd）');", true);
                }
            }
        }

        //核對必填欄位是否已填寫
        private bool CheckRequiredFields(out string unFillItem)
        {
            StringBuilder sb_unFillItem = new StringBuilder();
            bool unFill = false;

            //預約日期
            if (TextBox3.Text == "")
            {
                sb_unFillItem.Append("預約日期, ");
                unFill = true;
            }

            //預期入住日期
            if (txtexindate.Text == "")
            {
                sb_unFillItem.Append("預期入住日期, ");
                unFill = true;
            }



            if (sb_unFillItem.Length > 0)
            {
                unFillItem = sb_unFillItem.ToString().Substring(0, sb_unFillItem.Length);
            }
            else
            {
                unFillItem = "";
            }
            return unFill;
        }

        //核對必填欄位是否已填寫
        private bool CheckRequiredFields2(out string unFillItem)
        {
            StringBuilder sb_unFillItem = new StringBuilder();
            bool unFill = false;

            //預約日期
            if (TextBox1.Text == "")
            {
                sb_unFillItem.Append("預約日期, ");
                unFill = true;
            }

            //預期入住日期
            if (txtexpindate.Text == "")
            {
                sb_unFillItem.Append("預期入住日期, ");
                unFill = true;
            }



            if (sb_unFillItem.Length > 0)
            {
                unFillItem = sb_unFillItem.ToString().Substring(0, sb_unFillItem.Length);
            }
            else
            {
                unFillItem = "";
            }
            return unFill;
        }
    }
}