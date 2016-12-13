using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using AjaxControlToolkit;
using longtermcare.IPSchedule;

namespace longtermcare.IPBasic
{
    public partial class IP_Basic_A : System.Web.UI.Page
    {
        private static string connection_id;
        private string imgfolder;
        private string allowancefolder;
        private string url;
        private static string datasource;
        private static int ChangeCount = 0;
        private static string NewNO;
        public string choice;
        CheckBoxList[] chklp = new CheckBoxList[1000];
        RadioButtonList[] rdblp = new RadioButtonList[1000];

        protected void Page_Load(object sender, EventArgs e)
        {
            connection_id = Session["H_id"].ToString();
            if (connection_id == "JINGDE")
            {
                Response.Redirect("IP_Basic_AJ.aspx");
            }
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            SqlIPBasic.connect(connection_id);
            SqlMarry.ConnectionString = datasource;
            SqlInReason.ConnectionString = datasource;
            SqlTransfer.ConnectionString = datasource;
            SqlMateDuty.ConnectionString = datasource;
            SqlStatues.ConnectionString = datasource;
            SqlBelief.ConnectionString = datasource;
            SqlCheckMethod.ConnectionString = datasource;
            SqlTalkAbility.ConnectionString = datasource;
            SqlEducation.ConnectionString = datasource;
            SqlIPLanguage.ConnectionString = datasource;
            SqlCareer.ConnectionString = datasource;
            SqlMoney.ConnectionString = datasource;
            SqlInsurance.ConnectionString = datasource;
            SqlFamilyProblem.ConnectionString = datasource;
            SqlDataSource_IP_IDENTITY.ConnectionString = datasource;
            vege1.ConnectionString = datasource;
            vege2.ConnectionString = datasource;
            vege2_1.ConnectionString = datasource;
            vege3.ConnectionString = datasource;

            this.Page.Form.DefaultButton = "ContentPlaceHolder1$btnAddIPBasic";
            string language = Session["language"].ToString();

            this.TextBoxNO.Focus(); //預設焦點

            //解決FileUpload第一次無法抓值的問題
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            //設定台灣日曆
            /*
            System.Globalization.CultureInfo cag = new System.Globalization.CultureInfo("zh-TW");
            cag.DateTimeFormat.Calendar = new System.Globalization.TaiwanCalendar();
            System.Threading.Thread.CurrentThread.CurrentCulture = cag;
            */
            if (IsPostBack != true)
            {
                dropIPStatues.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropBelief.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropEducation.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropCheckMethod.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropCareer.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropMainLanguage.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropSecLanguage.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropThirdLanguage.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropTalkAbility.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropMoney.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                //dropInsurance.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), "")); 改成複選了
                dropFamilyProblem.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropMarry.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropMateDuty.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropInReason.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropTransfer.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                ddl_IP_IDENTITY.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), ""));
                dropInsurance.DataBind();
                dropInsurance.Items[1].Selected = true; //住民保險>全民健保預設
                ddl_IP_IDENTITY.SelectedValue = "1"; //優免身分類別>一般住民預設
            }


            Label1.Text = Language.Language.translate("IP_NO", language);
            Label3.Text = Language.Language.translate("IP_ID", language);
            Label4.Text = Language.Language.translate("Birthday", language);
            Label2.Text = Language.Language.translate("IP_Name", language);
            Label121.Text = Language.Language.translate("Sex", language);
            //Label6.Text = Language.Language.translate("Blood_Type", language);
            //Label7.Text = Language.Language.translate("Health_Card", language);
            Label8.Text = Language.Language.translate("Height", language);
            Label9.Text = Language.Language.translate("Weight", language);
            Label125.Text = Language.Language.translate("CM", language);
            Label126.Text = Language.Language.translate("KG", language);
            //Label116.Text = Language.Language.translate("Birth_Place", language) + " / 出生地";
            Label10.Text = Language.Language.translate("Residence_Address", language);
            Label33.Text = Language.Language.translate("Present_Address", language);
            Label44.Text = Language.Language.translate("Status_Category", language);
            //Label45.Text = Language.Language.translate("YNAllowance", language);
            Label46.Text = Language.Language.translate("Check_Method", language);
            Label47.Text = Language.Language.translate("Religion", language);
            Label48.Text = Language.Language.translate("Education", language);
            Label49.Text = Language.Language.translate("IP_Career", language);
            Label50.Text = Language.Language.translate("Language_Communication", language);
            Label51.Text = Language.Language.translate("Talk_Ability", language);
            Label52.Text = Language.Language.translate("IP_Money", language);
            Label53.Text = Language.Language.translate("IP_Insurance", language);
            Label54.Text = Language.Language.translate("Family_Assessment", language);
            Label55.Text = Language.Language.translate("Margin", language);
            Label56.Text = Language.Language.translate("Statutory_Infectious_Disease", language);
            Label5.Text = Language.Language.translate("IP_Photo", language);
            Label58.Text = Language.Language.translate("Marital_Status", language);
            Label59.Text = Language.Language.translate("Spouse_Name", language);
            Label60.Text = Language.Language.translate("Spouse_Responsibility", language);
            Label61.Text = Language.Language.translate("IP_Son", language);
            Label62.Text = Language.Language.translate("IP_Daughter", language);
            Label65.Text = Language.Language.translate("IN_Reason", language);
            Label66.Text = Language.Language.translate("Referral_Source", language);
            Label67.Text = Language.Language.translate("Referral_Name", language);
            Label68.Text = Language.Language.translate("Contractor", language);
            Label80.Text = Language.Language.translate("Contractor_Relationship", language);
            Label94.Text = Language.Language.translate("Contractor_TEL", language);
            Label69.Text = Language.Language.translate("Contractor_Address", language);
            Label81.Text = Language.Language.translate("Guarantor", language);
            Label82.Text = Language.Language.translate("Guarantor_Releationship", language);
            Label95.Text = Language.Language.translate("Guarantor_TEL", language);
            Label83.Text = Language.Language.translate("Guarantor_Address", language);
            Label96.Text = Language.Language.translate("Emergency_Contact", language);
            Label97.Text = Language.Language.translate("Emergency_Contact_Phone", language);
            Label98.Text = Language.Language.translate("Payer", language);
            Label99.Text = Language.Language.translate("Payer_TEL", language);
            Label100.Text = Language.Language.translate("Payer_Status_Assessment", language);
            //dropIPBlood.Items[0].Text = Language.Language.translate("Choose", language);
            rbSex.Items[0].Text = Language.Language.translate("Male", language);
            rbSex.Items[1].Text = Language.Language.translate("Female", language);

            //btnAddIPBasic.Text = Language.Language.translate("Save", language);//Add
            btnIPPicAdd.Text = Language.Language.translate("Add_In", language);
            btnIPPicDelete.Text = Language.Language.translate("Delete", language);
            btnIPBaic.Text = Language.Language.translate("IP_Basic", language);
            btnAllowance.Text = Language.Language.translate("IP_Allowance", language);


            //txtIPBirthday.Attributes.Add("onClick", "javascript:setYearRange();");
            this.Form.Attributes.Add("autocomplete", "off");
            imgfolder = Server.MapPath("~/Image/" + connection_id + "/IPImage");
            if (!Directory.Exists(imgfolder))
                Directory.CreateDirectory(imgfolder);
            allowancefolder = Server.MapPath("~/Image/" + connection_id + "/IPAllowance");
            if (!Directory.Exists(allowancefolder))
                Directory.CreateDirectory(allowancefolder);

            if (SqlIPBasic.NSETSELECT() == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "alert('請先設定住民編號格式以便新增');location.href='../../FIPManagement.aspx';", true);
            }
            else
            {
                lblIPNO.Text = GetMaxIPNO();
                SqlIPBasic.NSETSELECT2();
                string s = SqlIPBasic.GetN1().Trim();
                string c = SqlIPBasic.GetN2().Trim();
                string f = SqlIPBasic.GetN3().Trim();

                if (s == "1")
                {
                    lblNO2.Visible = false;
                    TextBoxNO.Visible = true;
                    NewNO = TextBoxNO.Text.Trim();
                }
                else if (s == "2")
                {
                    lblNO2.Text = lblIPNO.Text;
                    NewNO = lblIPNO.Text;
                }
                else
                {
                    lblNO2.Text = f;
                    NewNO = f;
                }

            }

            SqlIPNOInList.ConnectionString = datasource;
            //SqlIPNOInList.SelectCommand = SqlIPBasic.SearchLastIPB_RECORD("5");
            gvwIPNOInList.DataBind();
            gvwIPNOInList.Visible = true;

            if (radStat_Infc_HXY.Checked)
            {
                Div2.Style["display"] = "inline";
                TextBox5.Enabled = true;
            }
            if (radStat_Infc_HXN.Checked)
            {
                Div2.Style["display"] = "none";
                TextBox5.Enabled = false;
            }
            if (radAfter_Adms_HXY.Checked)
            {
                Div1.Style["display"] = "inline";
                TextBox6.Enabled = true;
            }
            if (radAfter_Adms_HXN.Checked)
            {
                Div1.Style["display"] = "none";
                TextBox6.Enabled = false;
            }

            /*if (radvege.Checked)
            {
                pnlvege.Enabled = true;
                chklvege.SelectedIndex = -1;
                txtNvege.Text = string.Empty;
            }
            else
                pnlvege.Enabled = false;
            if (radNvege.Checked)
            {
                pnlNvege.Enabled = true;
                chblvege.SelectedIndex = -1;
                chklvege1.SelectedIndex = -1;
                txtvege.Text = string.Empty;
            }
            else
                pnlNvege.Enabled = false;*/


            //判斷葷素
            if (radvege.Checked)
            {
                chblvege.Enabled = true;
                chklvege1.Enabled = true;
                txtvege.Enabled = true;
                chklvege.SelectedIndex = -1;
                txtNvege.Text = string.Empty;
                //讀取被選擇的項目，送到後端
                string[] chklblvege = lblvege.Text.TrimEnd(',').Split(',');
                for (int j = 0; j < chklvege1.Items.Count; j++)
                {
                    foreach (string a in chklblvege)
                    {
                        if (chklvege1.Items[j].Value == a)
                        {
                            chklvege1.Items[j].Selected = true;
                        }
                    }
                }
            }
            else
            {
                chblvege.Enabled = false;
                chklvege1.Enabled = false;
                txtvege.Enabled = false; ;
            }
            if (radNvege.Checked)
            {
                chklvege.Enabled = true;
                txtNvege.Enabled = true;
                chblvege.SelectedIndex = -1;
                chklvege1.SelectedIndex = -1;
                txtvege.Text = string.Empty;
                //讀取被選擇的項目，送到後端
                string[] chklblNvege = lblNvege.Text.TrimEnd(',').Split(',');
                for (int j = 0; j < chklvege.Items.Count; j++)
                {
                    foreach (string a in chklblNvege)
                    {
                        if (chklvege.Items[j].Value == a)
                        {
                            chklvege.Items[j].Selected = true;
                        }
                    }
                }
            }
            else
            {
                chklvege.Enabled = false;
                txtNvege.Enabled = false;
            }

            //戶籍地址
            if (TextBox3.Text != "")
            {
                TextBox3.Style["display"] = "inline";
                Panel1.Style["display"] = "none";
                Button1.Style["display"] = "none";
                Button2.Style["display"] = "inline";
            }
            else
            {
                TextBox3.Style["display"] = "none";
                Panel1.Style["display"] = "inline";
                Button1.Style["display"] = "inline";
                Button2.Style["display"] = "none";
            }
            if (TextBox7.Text != "")
            {
                TextBox7.Style["display"] = "inline";
                Panel2.Style["display"] = "none";
                Button3.Style["display"] = "none";
                Button7.Style["display"] = "inline";
            }
            else
            {
                TextBox7.Style["display"] = "none";
                Panel2.Style["display"] = "inline";
                Button3.Style["display"] = "inline";
                Button7.Style["display"] = "none";
            }
            if (TextBox8.Text != "")
            {
                TextBox8.Style["display"] = "inline";
                Panel3.Style["display"] = "none";
                Button8.Style["display"] = "none";
                Button9.Style["display"] = "inline";
            }
            else
            {
                TextBox8.Style["display"] = "none";
                Panel3.Style["display"] = "inline";
                Button8.Style["display"] = "inline";
                Button9.Style["display"] = "none";
            }
            //本國
            if (radTaiwan.Checked == true)
            {
                pnlTaiwanadr.Style["display"] = "inline";
                pnlTaiwan.Style["display"] = "inline";
                txtIPID.Focus();
                pnlForeign.Style["display"] = "none";
                pnlForeignadr.Style["display"] = "none";
                radTaiwan.Style["display"] = "inline";

                //rbSex.Enabled = false;
            }
            //外國
            if (radForeign.Checked == true)
            {
                pnlForeign.Style["display"] = "inline";
                pnlForeignadr.Style["display"] = "inline";
                pnlTaiwanadr.Style["display"] = "none";
                pnlTaiwan.Style["display"] = "none";
                radForeign.Style["display"] = "inline";
                //rbSex.Enabled = true;
            }
        }


        //建立人員代碼轉姓名
        protected string DGFormatRID3(string id)
        {
            try
            {
                connection_id = Session["H_id"].ToString();
                string name = SqlSchedule.EmpConvert(id, connection_id);
                return name;
            }
            catch
            {
                return "";
            }
        }

        private string FixBase64FromQueryString(string b)
        {
            return b.Replace(" ", "+");
        }


        //確定Button-寫入住民基本資料表
        protected void btnAddIPBasic_Click(object sender, EventArgs e)
        {
            try
            {
                //檢查是否輸入必填欄位
                string unFillItem = "";

                if (CheckRequiredFields(out unFillItem))
                {
                    lblShowErr.Text = "儲存失敗。請填寫必填欄位：" + unFillItem;
                    lblShowErr.Text.TrimEnd(",".ToCharArray());
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel6.GetType(), "tip", "<script>alert('儲存失敗。請填寫必填欄位：\\n" + unFillItem + "');</script>", false);
                }
                else
                {
                    bool b1 = SqlIPBasic.CheckIPNO1(TextBoxNO.Text);//在AccountEnable = 'N'的部份 是否存在相同的住民編號
                    if (b1)
                    {
                        //更新模式
                        string ipno = SqlIPBasic.Search_ip_no_by_ip_no_new1(TextBoxNO.Text, Session["H_id"].ToString());
                        ModifyFun(ipno);
                    }
                    else
                    {
                        //新增模式
                        SaveFun();
                    }
                }
            }
            catch
            {
                lblShowErr.Text = "住民資料新增失敗！請確認資料填寫正確。";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
            }

        }

        private void SaveFun()
        {
            string ipno = lblIPNO.Text;
            This_IP_NO.Text = ipno;
            string ipname = txtIPName.Text;
            // string birthday = DateTime.Parse(txtIPBirthday.Text).ToString("yyyyMMdd");//生日
            int year = sqlTime.rocytoady(Convert.ToInt16(tb_year.Text));
            string birthday = year.ToString() + ddl_month.SelectedValue.ToString().PadLeft(2, '0') + tb_day.Text.PadLeft(2, '0');
            string sex = rbSex.SelectedValue;//性別
            if (sex.Trim() == "")
                sex = "0";

            string ipid = "";//身份證字號 txtIPID.Text;
            string arc = "";//外國居留證
            string passport = "";//護照號碼
            string residedate = "";//居留期限
            string country = "";//國籍
            string permadr = "";//戶籍地址
            //本國
            if (radTaiwan.Checked == true)
            {
                ipid = txtIPID.Text;
                arc = "";
                passport = "";
                residedate = "";
                country = "台灣";

                //戶籍地址
                permadr = txtpermadr.Text;
            }
            //外國
            if (radForeign.Checked == true)
            {
                ipid = "";
                arc = txtForeign.Text;
                passport = txtPassportID.Text;
                country = txtCountry.Text;

                if (txtResidence.Text == "")
                {
                    residedate = "";
                }
                else
                {
                    residedate = sqlTime.DateSplitSlash(txtResidence.Text);
                    string residedatewarning = DateTimeCheck.datevaild(residedate);
                    if (residedatewarning == "日期格式錯誤")
                    {
                        lblShowErr.Text = "居留期限日期格式錯誤";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                    }
                }

                permadr = txtForeignAdr.Text;
            }

            //string dnr = txtDNR.Text;//DNR註記
            string dnr = rbDNR.SelectedValue;
            //string healthcard = txtHealthCard.Text;//健保卡號
            //string blood = dropIPBlood.SelectedValue;//血型
            string height = txtHeight.Text;//身高
            string weight = txtWeight.Text;//體重
            //string born = txtBorn.Text;//籍貫
            //string bornplace = TextBox4.Text;//出生地
            //string margin = txtMargin.Text;//保證金=>入住時才填寫
            string matename = txtMateName.Text;//配偶姓名
            string son = txtSon.Text;//住民兒子數
            string daughter = txtDaughter.Text;//住民女兒數
            string transfername = txtTransferName.Text;//轉介者姓名

            string signname = txtSignName.Text;//主簽約者
            string signtel = txtSignTel.Text;//主簽約者電話
            string sphone2 = txtSignTel0.Text.Trim();//主簽約者電話2
            string sphone3 = txtSignTel1.Text.Trim();//主簽約者電話3
            string semail = txtSIGN_EMAIL.Text;//主簽約者EMAIL
            string scutalkid = txtSIGN_CUTalk_MId.Text;//主簽約者CUTalk
            string signrelate = txtSignRelate.Text;//主簽約者關係

            string rename = txtREName.Text;//連帶保證者
            string retel = txtRETel.Text;//連帶保證者電話
            string rphone2 = txtRETel0.Text.Trim();//連帶保證者電話2
            string rphone3 = txtRETel1.Text.Trim();//連帶保證者電話3
            string remail = txtINVO1_EMAIL.Text;//連帶保證者EMAIL
            string rcutalkid = txtINVO1_CUTalk_MId.Text;//連帶保證者CUTalk
            string rerelate = txtRERelate.Text;//連帶保證者關係

            //緊急聯絡人
            string hurryname = txtHurryName.Text;
            //緊急聯絡人電話
            string hurrytel = txtHurryTel.Text;
            string hurrytel_2 = txtHurryTel1.Text;
            string hurrytel_3 = txtHurryTel0.Text;
            string hurryemail = txtEMR_EMAIL.Text;//緊急聯絡人1EMAIL
            string hurrycutalkid = txtEMR_CUTalk_MId.Text;//緊急聯絡人1CUTalk
            string erelate = txtRERelate0.Text.Trim();//緊急聯絡人關係
            //緊急聯絡人2
            string hurryname1 = txtHurryName1.Text;
            //緊急聯絡人2電話
            string hurrytel1 = txtHurryTel2.Text;
            string hurrytel1_2 = txtHurryTel3.Text;
            string hurrytel1_3 = txtHurryTel4.Text;
            string hurryemail1 = txtEMR1_EMAIL.Text;//緊急聯絡人2EMAIL
            string hurrycutalkid1 = txtEMR1_CUTalk_MId.Text;//緊急聯絡人2CUTalk
            string erelate1 = txtRERelate1.Text.Trim();//緊急聯絡人2關係
            //緊急聯絡人3
            string hurryname2 = txtHurryName_Em.Text;
            //緊急聯絡人3電話 
            string hurryte2 = txtHurryTel_Em3_1.Text;
            string hurryte2_2 = txtHurryTel_Em3_2.Text;
            string hurryte2_3 = txtHurryTel_Em3_3.Text;
            string hurryemail2 = txtEMR2_EMAIL.Text;//緊急聯絡人3EMAIL
            string hurrycutalkid2 = txtEMR2_CUTalk_MId.Text;//緊急聯絡人3CUTalk
            string erelate2 = txtRERelate_Re3.Text.Trim();//緊急聯絡人3關係
            //主要繳款人
            string payname = txtPayName.Text;
            string paytel = txtPayTel.Text;
            string payemail1 = txtPAY_EMAIL.Text;//主要繳款人EMAIL
            string paycutalkid = txtPAY_CUTalk_MId.Text;//主要繳款人CUTalk
            string payevalute = txtPatEvalute.Text;
            //semail, scutalkid, remail, rcutalkid, hurryemail, hurrycutalkid, hurryemail1, hurrycutalkid1, hurryemail2, hurrycutalkid2, payemail1, paycutalkid


            //身分類別
            string statues = dropIPStatues.SelectedValue;
            string statuesex = "";
            if (dropIPStatues.SelectedValue == "16")
            {
                statuesex = txtIPStatuesOther.Text;
            }
            //入住方式
            string checkmethod = dropCheckMethod.SelectedValue;
            //宗教信仰
            string belief = dropBelief.SelectedValue;
            string beliefex = "";
            if (dropBelief.SelectedValue == "9")
            {
                beliefex = txtBeliefOther.Text;
            }
            //教育程度
            string education = dropEducation.SelectedValue;
            //住民職業
            string career = dropCareer.SelectedValue;
            string careerex = "";
            if (dropCareer.SelectedValue == "9")
            {
                careerex = txtCareerOther.Text;
            }

            //主要溝通語言
            string ipMainlanguage = dropMainLanguage.SelectedValue;
            string ipMainlanguageex = "";
            if (dropMainLanguage.SelectedValue == "12")
            {
                ipMainlanguageex = txtMainLanguage.Text;
            }
            //次要溝通語言
            string ipSeclanguage = dropSecLanguage.SelectedValue;
            string ipSeclanguageex = "";
            if (dropSecLanguage.SelectedValue == "12")
            {
                ipSeclanguageex = txtSecLanguage.Text;
            }
            //第三溝通語言
            string ipThirdlanguage = dropThirdLanguage.SelectedValue;
            string ipThirdlanguageex = "";
            if (dropThirdLanguage.SelectedValue == "12")
            {
                ipThirdlanguageex = txtThirdLanguage.Text;
            }
            //語言能力 txtTalkAbility
            string talkability = dropTalkAbility.SelectedValue;
            string talkabilityex = "";
            if (dropTalkAbility.SelectedValue == "5")
            {
                talkabilityex = txtTalkAbility.Text;
            }
            //主要經濟來源
            string money = dropMoney.SelectedValue;
            string moneyex = "";
            if (dropMoney.SelectedValue == "11")
            {
                moneyex = txtMoneyOther.Text;
            }
            //住民保險 txtInsuranceOther
            string insurance = "";
            string insuranceex = "";
            for (int a = 0; a < dropInsurance.Items.Count; a++)
            {
                if (dropInsurance.Items[a].Selected)
                {
                    if (dropInsurance.Items[a].Text == "其他")
                        insuranceex = txtInsuranceOther.Text;
                    else
                        insurance += dropInsurance.Items[a].Value + ",";
                }
            }
            //家庭問題評估
            string familyproblem = dropFamilyProblem.SelectedValue;
            //婚姻狀況
            string marry = dropMarry.SelectedValue;
            string marryex = "";
            if (dropMarry.SelectedValue == "8")
            {
                marryex = txtMarryOther.Text;
            }
            //配偶責任感
            string mateduty = dropMateDuty.SelectedValue;
            //入住原因
            string inreason = dropInReason.SelectedValue;
            string inreasonex = "";
            if (dropInReason.SelectedValue == "14")
            {
                inreasonex = txtInReasonOther.Text;
            }
            //轉介來源
            string transfer = dropTransfer.SelectedValue;
            string transferex = "";
            if (dropTransfer.SelectedValue == "99")
            {
                transferex = txtTransferOther.Text;
            }

            //身心障礙
            string barrier = "";
            if (radBarrierY.Checked == true)
            {
                barrier = "1";
            }
            else
                barrier = "0";
            //有無法定傳染性疾病史
            string statinfchx = "";
            if (radStat_Infc_HXY.Checked == true)
            {
                statinfchx = "1";
            }
            else
                statinfchx = "0";
            string statinfc_ex = "";//法定傳染性疾病史
            if (TextBox5.Style["display"] == "inline")
            {
                statinfc_ex = TextBox5.Text;
            }

            //有無初入院後有無追蹤疾病史
            string afteradmshx = "";
            if (radAfter_Adms_HXY.Checked == true)
            {
                afteradmshx = "1";
            }
            else
                afteradmshx = "0";

            string afteradm_ex = "";//追蹤疾病史
            if (TextBox6.Style["display"] == "inline")
            {
                afteradm_ex = TextBox6.Text;
            }

            #region 處理地址-現居地址, 主簽約者地址, 連帶保證者地址, 緊急連絡人地址
            //現居地址
            string nowadr;
            if (TextBox3.Style["display"] == "inline")
            {
                nowadr = TextBox3.Text;
            }
            else
            {
                nowadr = txtnowadr.Text;
            }

            //主簽約者地址 TextBox7
            string signadr = "";
            if (TextBox7.Style["display"] == "inline")
            {
                signadr = TextBox7.Text;
            }
            else
            {
                signadr = txtsignadr.Text;
            }

            //連帶保證者地址 TextBox8
            string readr = "";
            if (TextBox8.Style["display"] == "inline")
            {
                readr = TextBox8.Text;
            }
            else
            {
                readr = txtreadr.Text;
            }
            //緊急連絡人1地址
            string emr1_add = txtec1adr.Text;

            //緊急連絡人2地址
            string emr2_add = txtec2adr.Text;

            //緊急連絡人3地址
            string emr3_add = txtec3adr.Text;
            #endregion

            //照片檔名
            string ipphoto = "";
            if (IPPic.Src != "")
            {
                string url = IPPic.Src;
                if (File.Exists(Server.MapPath(IPPic.Src)))//有上傳照片且照片存在
                {
                    ipphoto = url.Split('/')[4];
                }
                else
                {
                    ipphoto = "";
                }
            }
            else
            {
                ipphoto = "";
            }

            string strip_identity_no = ddl_IP_IDENTITY.SelectedValue;

            string createuser = Session["account"].ToString();
            string createdate = sqlTime.time();
            string creatTime = SqlIPBasic.gettime().Trim();//sqlTime.hourminute();

            string ipyear = ipno.Substring(0, 3);
            string ipmaxno = ipno.Substring(3, 4);

            int cc;
            string ccc = "";
            SqlIPBasic.NSETSELECT2();
            string nn1 = SqlIPBasic.GetN1().Trim();
            int Count = Convert.ToInt32(SqlIPBasic.GetN2());
            if (nn1 == "3")
            {
                cc = Convert.ToInt32(lblNO2.Text) + 1;
                ccc = cc.ToString();

                int a = cc;
                int i = 1;
                //if (a < 0) i++; // 如果負號不計,這行去除
                for (;;)
                {
                    a /= 10;
                    if (a == 0) break;
                    i++;
                }

                for (int j = i; j < Convert.ToInt32(Count); j++)
                {
                    ccc = 0 + ccc;
                }
            }


            //膳食偏好
            //葷食者 不吃食物
            string chklvege_content = "";
            for (int a = 0; a < chklvege.Items.Count; a++)
            {
                if (chklvege.Items[a].Selected)
                {
                    chklvege_content += chklvege.Items[a].Value + ",";
                }
            }

            //素食者 種類
            string chblvege_content = chblvege.SelectedValue;
            //素食者 不吃食物
            string chklvege1_content = "";
            for (int a = 0; a < chklvege1.Items.Count; a++)
            {
                if (chklvege1.Items[a].Selected)
                {
                    chklvege1_content += chklvege1.Items[a].Value + ",";
                }
            }
            //其他不吃食物
            string pickyfood = "";
            if (radvege.Checked)
                pickyfood = txtvege.Text;
            else if (radNvege.Checked)
                pickyfood = txtNvege.Text;
            //素食時節
            string chklvegetime_content = "";
            for (int a = 0; a < chklvegetime.Items.Count; a++)
            {
                if (chklvegetime.Items[a].Selected)
                {
                    chklvegetime_content += chklvegetime.Items[a].Value + ",";
                }
            }
            //主食偏好
            string favorbreak = txtfavorbreak.Text;
            string favorlunch = txtfavorlunch.Text;
            string favordinner = txtfavordinner.Text;
            //禁忌
            string forbid = txtforbid.Text;
            //特殊指示
            string specorder = txtspecorder.Text;
            //各餐之要求
            string mealbreak = txtmealbreak.Text;
            string mealsnack = txtmealsnack.Text;
            string meallunch = txtmeallunch.Text;
            string mealtea = txtmealtea.Text;
            string mealdinner = txtmealdinner.Text;
            string mealmnsnack = txtmealmnsnack.Text;

            SqlIPBasic.UpdateMaxNo(ipyear, ipmaxno);
            //住民基本資料
            SqlIPBasic.AddIPInfo(ipno, ipname, sex, ipid, birthday, country, arc, passport, residedate, height, weight, permadr, nowadr, dnr, statues, statuesex, barrier, checkmethod, career, careerex, belief, beliefex, education, ipMainlanguage, ipMainlanguageex, ipSeclanguage, ipSeclanguageex, ipThirdlanguage, ipThirdlanguageex, talkability, talkabilityex, money, moneyex, insurance, insuranceex, familyproblem, statinfchx, statinfc_ex, afteradmshx, afteradm_ex, ipphoto, createuser, createdate, creatTime, NewNO, strip_identity_no);
            //住民家庭基本資料  
            SqlIPBasic.AddIPFamily(ipno, marry, marryex, matename, mateduty, son, daughter, inreason, inreasonex, transfer, transferex, signname, signrelate, signadr, signtel, rename, rerelate, readr, retel, hurryname, hurrytel, payname, paytel, payevalute, transfername, createuser, createdate, creatTime, sphone2, sphone3, rphone2, rphone3, erelate, hurrytel_2, hurrytel_3, hurryname1, hurrytel1, hurrytel1_2, hurrytel1_3, erelate1, emr1_add, emr2_add, hurryname2, hurryte2, hurryte2_2, hurryte2_3, erelate2, emr3_add, semail, scutalkid, remail, rcutalkid, hurryemail, hurrycutalkid, hurryemail1, hurrycutalkid1, hurryemail2, hurrycutalkid2, payemail1, paycutalkid);
            //住民膳食偏好
            if (radvege.Checked) //素
                SqlIPBasic.AddIPMealfavor(ipno, chklvege1_content, chblvege_content, chklvegetime_content, favorbreak, favorlunch, favordinner, forbid, specorder, mealbreak, mealsnack, meallunch, mealtea, mealdinner, mealmnsnack, createuser, createdate, creatTime, pickyfood);
            else if (radNvege.Checked)//葷
                SqlIPBasic.AddIPMealfavor(ipno, chklvege_content, chblvege_content, chklvegetime_content, favorbreak, favorlunch, favordinner, forbid, specorder, mealbreak, mealsnack, meallunch, mealtea, mealdinner, mealmnsnack, createuser, createdate, creatTime, pickyfood);
            SqlIPBasic.UpdateNfrom(ccc);

            string warning = SqlIPBasic.GetWarning();
            warning = "新增成功";
            if (warning == "新增成功")
            {
                disablePrint.Style["display"] = "none";
                btnPrint.Style["display"] = "inline";
                //btnPrint.Enabled = true;
                disableNext.Style["display"] = "none";
                Button10.Style["display"] = "inline";
                //Button10.Enabled = true; //繼續新增
                pnlIPList.Enabled = false;
                disableAddIPBasic.Style["display"] = "inline";
                btnAddIPBasic.Style["display"] = "none";
                //btnAddIPBasic.Enabled = false;
                Panel6.Enabled = true;//請選擇是否繼續填寫補助資料，障礙手冊
                disableButton4.Style["display"] = "none";
                Button4.Style["display"] = "inline";
                disableButton6.Style["display"] = "none";
                Button6.Style["display"] = "inline";
            }
            else
            {
                disablePrint.Style["display"] = "inline";
                btnPrint.Style["display"] = "none";
                //btnPrint.Enabled = false;
                disableNext.Style["display"] = "inline";
                Button10.Style["display"] = "none";
                //Button10.Enabled = false; //繼續新增
                pnlIPList.Enabled = true;
                disableAddIPBasic.Style["display"] = "none";
                btnAddIPBasic.Style["display"] = "inline";
                //btnAddIPBasic.Enabled = true;
                Panel6.Enabled = false;//請選擇是否繼續填寫補助資料，障礙手冊
                disableButton4.Style["display"] = "inline";
                Button4.Style["display"] = "none";
                disableButton6.Style["display"] = "inline";
                Button6.Style["display"] = "none";
            }

            lblShowMsg.Text = warning;
            ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
        }
        private void ModifyFun(string ipno)
        {
            //string ipno = lblIPNO.Text;
            This_IP_NO.Text = ipno;
            string ipname = txtIPName.Text;
            // string birthday = DateTime.Parse(txtIPBirthday.Text).ToString("yyyyMMdd");//生日
            int year = sqlTime.rocytoady(Convert.ToInt16(tb_year.Text));
            string birthday = year.ToString() + ddl_month.SelectedValue.ToString().PadLeft(2, '0') + tb_day.Text.PadLeft(2, '0');
            string sex = rbSex.SelectedValue;//性別
            if (sex.Trim() == "")
                sex = "0";

            string ipid = "";//身份證字號 txtIPID.Text;
            string arc = "";//外國居留證
            string passport = "";//護照號碼
            string residedate = "";//居留期限
            string country = "";//國籍
            string permadr = "";//戶籍地址
            //本國
            if (radTaiwan.Checked == true)
            {
                ipid = txtIPID.Text;
                arc = "";
                passport = "";
                residedate = "";
                country = "台灣";

                //戶籍地址
                permadr = txtpermadr.Text;
            }
            //外國
            if (radForeign.Checked == true)
            {
                ipid = "";
                arc = txtForeign.Text;
                passport = txtPassportID.Text;
                country = txtCountry.Text;

                if (txtResidence.Text == "")
                {
                    residedate = "";
                }
                else
                {
                    residedate = sqlTime.DateSplitSlash(txtResidence.Text);
                    string residedatewarning = DateTimeCheck.datevaild(residedate);
                    if (residedatewarning == "日期格式錯誤")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + "居留期限日期格式錯誤" + "');", true);
                    }
                }

                permadr = txtForeignAdr.Text;
            }

            string dnr = txtDNR.Text;//DNR註記
            //string healthcard = txtHealthCard.Text;//健保卡號
            //string blood = dropIPBlood.SelectedValue;//血型
            string height = txtHeight.Text;//身高
            string weight = txtWeight.Text;//體重
            //string born = txtBorn.Text;//籍貫
            //string bornplace = TextBox4.Text;//出生地
            //string margin = txtMargin.Text;//保證金=>入住時才填寫
            string matename = txtMateName.Text;//配偶姓名
            string son = txtSon.Text;//住民兒子數
            string daughter = txtDaughter.Text;//住民女兒數
            string transfername = txtTransferName.Text;//轉介者姓名

            string signname = txtSignName.Text;//主簽約者
            string signtel = txtSignTel.Text;//主簽約者電話
            string sphone2 = txtSignTel0.Text.Trim();//主簽約者電話2
            string sphone3 = txtSignTel1.Text.Trim();//主簽約者電話3
            string semail = txtSIGN_EMAIL.Text;//主簽約者EMAIL
            string scutalkid = txtSIGN_CUTalk_MId.Text;//主簽約者CUTalk
            string signrelate = txtSignRelate.Text;//主簽約者關係

            string rename = txtREName.Text;//連帶保證者
            string retel = txtRETel.Text;//連帶保證者電話
            string rphone2 = txtRETel0.Text.Trim();//連帶保證者電話2
            string rphone3 = txtRETel1.Text.Trim();//連帶保證者電話3
            string remail = txtINVO1_EMAIL.Text;//連帶保證者EMAIL
            string rcutalkid = txtINVO1_CUTalk_MId.Text;//連帶保證者CUTalk
            string rerelate = txtRERelate.Text;//連帶保證者關係

            //緊急聯絡人
            string hurryname = txtHurryName.Text;
            //緊急聯絡人電話
            string hurrytel = txtHurryTel.Text;
            string hurrytel_2 = txtHurryTel1.Text;
            string hurrytel_3 = txtHurryTel0.Text;
            string hurryemail = txtEMR_EMAIL.Text;//緊急聯絡人1EMAIL
            string hurrycutalkid = txtEMR_CUTalk_MId.Text;//緊急聯絡人1CUTalk
            string erelate = txtRERelate0.Text.Trim();//緊急聯絡人關係
            //緊急聯絡人2
            string hurryname1 = txtHurryName1.Text;
            //緊急聯絡人2電話
            string hurrytel1 = txtHurryTel2.Text;
            string hurrytel1_2 = txtHurryTel3.Text;
            string hurrytel1_3 = txtHurryTel4.Text;
            string hurryemail1 = txtEMR1_EMAIL.Text;//緊急聯絡人2EMAIL
            string hurrycutalkid1 = txtEMR1_CUTalk_MId.Text;//緊急聯絡人2CUTalk
            string erelate1 = txtRERelate1.Text.Trim();//緊急聯絡人2關係
            //緊急聯絡人3
            string hurryname2 = txtHurryName_Em.Text;
            //緊急聯絡人3電話 
            string hurryte2 = txtHurryTel_Em3_1.Text;
            string hurryte2_2 = txtHurryTel_Em3_2.Text;
            string hurryte2_3 = txtHurryTel_Em3_3.Text;
            string hurryemail2 = txtEMR2_EMAIL.Text;//緊急聯絡人3EMAIL
            string hurrycutalkid2 = txtEMR2_CUTalk_MId.Text;//緊急聯絡人3CUTalk
            string erelate2 = txtRERelate_Re3.Text.Trim();//緊急聯絡人3關係
            //主要繳款人
            string payname = txtPayName.Text;
            string paytel = txtPayTel.Text;
            string payemail1 = txtPAY_EMAIL.Text;//主要繳款人EMAIL
            string paycutalkid = txtPAY_CUTalk_MId.Text;//主要繳款人CUTalk
            string payevalute = txtPatEvalute.Text;
            //semail, scutalkid, remail, rcutalkid, hurryemail, hurrycutalkid, hurryemail1, hurrycutalkid1, hurryemail2, hurrycutalkid2, payemail1, paycutalkid


            //身分類別
            string statues = dropIPStatues.SelectedValue;
            string statuesex = "";
            if (dropIPStatues.SelectedValue == "16")
            {
                statuesex = txtIPStatuesOther.Text;
            }
            //入住方式
            string checkmethod = dropCheckMethod.SelectedValue;
            //宗教信仰
            string belief = dropBelief.SelectedValue;
            string beliefex = "";
            if (dropBelief.SelectedValue == "9")
            {
                beliefex = txtBeliefOther.Text;
            }
            //教育程度
            string education = dropEducation.SelectedValue;
            //住民職業
            string career = dropCareer.SelectedValue;
            string careerex = "";
            if (dropCareer.SelectedValue == "9")
            {
                careerex = txtCareerOther.Text;
            }

            //主要溝通語言
            string ipMainlanguage = dropMainLanguage.SelectedValue;
            string ipMainlanguageex = "";
            if (dropMainLanguage.SelectedValue == "12")
            {
                ipMainlanguageex = txtMainLanguage.Text;
            }
            //次要溝通語言
            string ipSeclanguage = dropSecLanguage.SelectedValue;
            string ipSeclanguageex = "";
            if (dropSecLanguage.SelectedValue == "12")
            {
                ipSeclanguageex = txtSecLanguage.Text;
            }
            //第三溝通語言
            string ipThirdlanguage = dropThirdLanguage.SelectedValue;
            string ipThirdlanguageex = "";
            if (dropThirdLanguage.SelectedValue == "12")
            {
                ipThirdlanguageex = txtThirdLanguage.Text;
            }
            //語言能力 txtTalkAbility
            string talkability = dropTalkAbility.SelectedValue;
            string talkabilityex = "";
            if (dropTalkAbility.SelectedValue == "5")
            {
                talkabilityex = txtTalkAbility.Text;
            }
            //主要經濟來源
            string money = dropMoney.SelectedValue;
            string moneyex = "";
            if (dropMoney.SelectedValue == "11")
            {
                moneyex = txtMoneyOther.Text;
            }
            //住民保險 txtInsuranceOther
            string insurance = "";
            string insuranceex = "";
            for (int a = 0; a < dropInsurance.Items.Count; a++)
            {
                if (dropInsurance.Items[a].Selected)
                {
                    if (dropInsurance.Items[a].Text == "其他")
                        insuranceex = txtInsuranceOther.Text;
                    else
                        insurance += dropInsurance.Items[a].Value + ",";
                }
            }
            //家庭問題評估
            string familyproblem = dropFamilyProblem.SelectedValue;
            //婚姻狀況
            string marry = dropMarry.SelectedValue;
            string marryex = "";
            if (dropMarry.SelectedValue == "8")
            {
                marryex = txtMarryOther.Text;
            }
            //配偶責任感
            string mateduty = dropMateDuty.SelectedValue;
            //入住原因
            string inreason = dropInReason.SelectedValue;
            string inreasonex = "";
            if (dropInReason.SelectedValue == "14")
            {
                inreasonex = txtInReasonOther.Text;
            }
            //轉介來源
            string transfer = dropTransfer.SelectedValue;
            string transferex = "";
            if (dropTransfer.SelectedValue == "5")
            {
                transferex = txtTransferOther.Text;
            }

            //身心障礙
            string barrier = "";
            if (radBarrierY.Checked == true)
            {
                barrier = "1";
            }
            else
                barrier = "0";
            //有無法定傳染性疾病史
            string statinfchx = "";
            if (radStat_Infc_HXY.Checked == true)
            {
                statinfchx = "1";
            }
            else
                statinfchx = "0";
            string statinfc_ex = "";//法定傳染性疾病史
            if (TextBox5.Style["display"] == "inline")
            {
                statinfc_ex = TextBox5.Text;
            }

            //有無初入院後有無追蹤疾病史
            string afteradmshx = "";
            if (radAfter_Adms_HXY.Checked == true)
            {
                afteradmshx = "1";
            }
            else
                afteradmshx = "0";

            string afteradm_ex = "";//追蹤疾病史
            if (TextBox6.Style["display"] == "inline")
            {
                afteradm_ex = TextBox6.Text;
            }

            #region 處理地址-現居地址, 主簽約者地址, 連帶保證者地址, 緊急連絡人地址
            //現居地址
            string nowadr;
            if (TextBox3.Style["display"] == "inline")
            {
                nowadr = TextBox3.Text;
            }
            else
            {
                nowadr = txtnowadr.Text;
            }

            //主簽約者地址 TextBox7
            string signadr = "";
            if (TextBox7.Style["display"] == "inline")
            {
                signadr = TextBox7.Text;
            }
            else
            {
                signadr = txtsignadr.Text;
            }

            //連帶保證者地址 TextBox8
            string readr = "";
            if (TextBox8.Style["display"] == "inline")
            {
                readr = TextBox8.Text;
            }
            else
            {
                readr = txtreadr.Text;
            }

            //緊急連絡人1地址
            string emr1_add = txtec1adr.Text;

            //緊急連絡人2地址
            string emr2_add = txtec2adr.Text;

            //緊急連絡人3地址
            string emr3_add = txtec3adr.Text;
            #endregion

            //照片檔名
            string ipphoto = "";
            if (IPPic.Src != "")
            {
                string url = IPPic.Src;
                if (File.Exists(Server.MapPath(IPPic.Src)))//有上傳照片且照片存在
                {
                    ipphoto = url.Split('/')[4];
                }
                else
                {
                    ipphoto = "";
                }
            }
            else
            {
                ipphoto = "";
            }

            string strip_identity_no = ddl_IP_IDENTITY.SelectedValue;

            string createuser = Session["account"].ToString();
            string createdate = sqlTime.time();
            string creatTime = SqlIPBasic.gettime().Trim();//sqlTime.hourminute();

            string ipyear = ipno.Substring(0, 3);
            string ipmaxno = ipno.Substring(3, 4);

            //膳食偏好
            //葷食者
            string chklvege_content = "";

            for (int a = 0; a < chklvege.Items.Count; a++)
            {
                if (chklvege.Items[a].Selected)
                {
                    chklvege_content += chklvege.Items[a].Value + ",";
                }
            }

            //素食者
            string chblvege_content = chblvege.SelectedValue;
            //素食者 不吃食物
            string chklvege1_content = "";
            for (int a = 0; a < chklvege1.Items.Count; a++)
            {
                if (chklvege1.Items[a].Selected)
                {
                    chklvege1_content += chklvege1.Items[a].Value + ",";
                }
            }
            //其他不吃食物
            string pickyfood = "";
            if (txtvege.Text != "")
                pickyfood = txtvege.Text;
            else
                pickyfood = txtNvege.Text;

            //素食時節
            string chklvegetime_content = "";
            for (int a = 0; a < chklvegetime.Items.Count; a++)
            {
                if (chklvegetime.Items[a].Selected)
                {
                    chklvegetime_content += chklvegetime.Items[a].Value + ",";
                }
            }
            //主食偏好
            string favorbreak = txtfavorbreak.Text;
            string favorlunch = txtfavorlunch.Text;
            string favordinner = txtfavordinner.Text;
            //禁忌
            string forbid = txtforbid.Text;
            //特殊指示
            string specorder = txtspecorder.Text;
            //各餐之要求
            string mealbreak = txtmealbreak.Text;
            string mealsnack = txtmealsnack.Text;
            string meallunch = txtmeallunch.Text;
            string mealtea = txtmealtea.Text;
            string mealdinner = txtmealdinner.Text;
            string mealmnsnack = txtmealmnsnack.Text;

            if (radNvege.Checked) //葷
            {
                SqlIPBasic.UpdateIPInformation1(ipno, ipname, sex, ipid, birthday, country, arc, passport, residedate, height, weight, permadr, nowadr, dnr, statues, statuesex, barrier, checkmethod, career, careerex, belief, beliefex, education, ipMainlanguage, ipMainlanguageex, ipSeclanguage, ipSeclanguageex, ipThirdlanguage, ipThirdlanguageex, talkability, talkabilityex, money, moneyex, insurance, insuranceex, familyproblem, statinfchx, statinfc_ex, afteradmshx, afteradm_ex, ipphoto, createuser, createdate, creatTime, NewNO, strip_identity_no, marry, marryex, matename, mateduty, son, daughter, inreason, inreasonex, transfer, transferex, signname, signrelate, signadr, signtel, rename, rerelate, readr, retel, hurryname, hurrytel, payname, paytel, payevalute, transfername, sphone2, sphone3, rphone2, rphone3, erelate, hurrytel_2, hurrytel_3, hurryname1, hurrytel1, hurrytel1_2, hurrytel1_3, erelate1, emr1_add, emr2_add, hurryname2, hurryte2, hurryte2_2, hurryte2_3, erelate2, emr3_add, semail, scutalkid, remail, rcutalkid, hurryemail, hurrycutalkid, hurryemail1, hurrycutalkid1, hurryemail2, hurrycutalkid2, payemail1, paycutalkid, chklvege_content, chblvege_content, chklvegetime_content, favorbreak, favorlunch, favordinner, forbid, specorder, mealbreak, mealsnack, meallunch, mealtea, mealdinner, mealmnsnack, pickyfood);}

            else if (radvege.Checked)//素
            {
                SqlIPBasic.UpdateIPInformation1(ipno, ipname, sex, ipid, birthday, country, arc, passport, residedate, height, weight, permadr, nowadr, dnr, statues, statuesex, barrier, checkmethod, career, careerex, belief, beliefex, education, ipMainlanguage, ipMainlanguageex, ipSeclanguage, ipSeclanguageex, ipThirdlanguage, ipThirdlanguageex, talkability, talkabilityex, money, moneyex, insurance, insuranceex, familyproblem, statinfchx, statinfc_ex, afteradmshx, afteradm_ex, ipphoto, createuser, createdate, creatTime, NewNO, strip_identity_no, marry, marryex, matename, mateduty, son, daughter, inreason, inreasonex, transfer, transferex, signname, signrelate, signadr, signtel, rename, rerelate, readr, retel, hurryname, hurrytel, payname, paytel, payevalute, transfername, sphone2, sphone3, rphone2, rphone3, erelate, hurrytel_2, hurrytel_3, hurryname1, hurrytel1, hurrytel1_2, hurrytel1_3, erelate1, emr1_add, emr2_add, hurryname2, hurryte2, hurryte2_2, hurryte2_3, erelate2, emr3_add, semail, scutalkid, remail, rcutalkid, hurryemail, hurrycutalkid, hurryemail1, hurrycutalkid1, hurryemail2, hurrycutalkid2, payemail1, paycutalkid, chklvege1_content, chblvege_content, chklvegetime_content, favorbreak, favorlunch, favordinner, forbid, specorder, mealbreak, mealsnack, meallunch, mealtea, mealdinner, mealmnsnack, pickyfood);
            }
            else {
                SqlIPBasic.UpdateIPInformation1(ipno, ipname, sex, ipid, birthday, country, arc, passport, residedate, height, weight, permadr, nowadr, dnr, statues, statuesex, barrier, checkmethod, career, careerex, belief, beliefex, education, ipMainlanguage, ipMainlanguageex, ipSeclanguage, ipSeclanguageex, ipThirdlanguage, ipThirdlanguageex, talkability, talkabilityex, money, moneyex, insurance, insuranceex, familyproblem, statinfchx, statinfc_ex, afteradmshx, afteradm_ex, ipphoto, createuser, createdate, creatTime, NewNO, strip_identity_no, marry, marryex, matename, mateduty, son, daughter, inreason, inreasonex, transfer, transferex, signname, signrelate, signadr, signtel, rename, rerelate, readr, retel, hurryname, hurrytel, payname, paytel, payevalute, transfername, sphone2, sphone3, rphone2, rphone3, erelate, hurrytel_2, hurrytel_3, hurryname1, hurrytel1, hurrytel1_2, hurrytel1_3, erelate1, emr1_add, emr2_add, hurryname2, hurryte2, hurryte2_2, hurryte2_3, erelate2, emr3_add, semail, scutalkid, remail, rcutalkid, hurryemail, hurrycutalkid, hurryemail1, hurrycutalkid1, hurryemail2, hurrycutalkid2, payemail1, paycutalkid, chklvege_content, chblvege_content, chklvegetime_content, favorbreak, favorlunch, favordinner, forbid, specorder, mealbreak, mealsnack, meallunch, mealtea, mealdinner, mealmnsnack, pickyfood);
            }

            string warning = SqlIPBasic.GetWarning();
            //warning = "修改成功";
            if (warning == "修改成功")
            {
                disablePrint.Style["display"] = "none";
                btnPrint.Style["display"] = "inline";
                //btnPrint.Enabled = true;
                disableNext.Style["display"] = "none";
                Button10.Style["display"] = "inline";
                //Button10.Enabled = true; //繼續新增
                pnlIPList.Enabled = false;
                disableAddIPBasic.Style["display"] = "inline";
                btnAddIPBasic.Style["display"] = "none";
                //btnAddIPBasic.Enabled = false;
                Panel6.Enabled = true;//請選擇是否繼續填寫補助資料，障礙手冊
                disableButton4.Style["display"] = "none";
                Button4.Style["display"] = "inline";
                disableButton6.Style["display"] = "none";
                Button6.Style["display"] = "inline";
                lblShowMsg.Text = "新增成功";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
            }
            else
            {
                disablePrint.Style["display"] = "inline";
                btnPrint.Style["display"] = "none";
                //btnPrint.Enabled = false;
                disableNext.Style["display"] = "inline";
                Button10.Style["display"] = "none";
                //Button10.Enabled = false; //繼續新增
                pnlIPList.Enabled = true;
                disableAddIPBasic.Style["display"] = "none";
                btnAddIPBasic.Style["display"] = "inline";
                //btnAddIPBasic.Enabled = true;
                Panel6.Enabled = false;//請選擇是否繼續填寫補助資料，障礙手冊
                disableButton4.Style["display"] = "inline";
                Button4.Style["display"] = "none";
                disableButton6.Style["display"] = "inline";
                Button6.Style["display"] = "none";
                lblShowErr.Text = "新增失敗";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
            }

            
        }

        //新增照片
        protected void btnIPPicAdd_Click(object sender, EventArgs e)
        {
            string connection_id = Session["H_id"].ToString();
            Boolean fileOk = false;

            if (fupIPPic.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(fupIPPic.FileName).ToLower();
                string[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOk = true;
                    }
                }
                if (fileOk)
                {
                    string photodate = sqlTime.datetimes().ToString();
                    string newFileName = Server.MapPath("~/Image/" + connection_id + "/IPImage/") + lblIPNO.Text + "_" + photodate + Path.GetExtension(fupIPPic.FileName);
                    string newFileName_Update = imgfolder + "\\" + lblIPNO.Text + "_" + photodate + Path.GetExtension(fupIPPic.FileName);
                    url = Page.ResolveUrl("~") + "Image/" + connection_id + "/IPImage/" + lblIPNO.Text + "_" + photodate + Path.GetExtension(fupIPPic.FileName);
                    try
                    {
                        int fileLength = Convert.ToInt32(fupIPPic.PostedFile.InputStream.Length);
                        byte[] imageBuffer = new byte[fileLength];
                        Request.Files[0].InputStream.Read(imageBuffer, 0, fileLength);
                        MemoryStream imageMS = new MemoryStream(imageBuffer);   //將使用者上傳的圖片塞進 MemoryStream
                        System.Drawing.Image inputImage = System.Drawing.Image.FromStream(imageMS);
                        inputImage.Save(newFileName);
                        IPPic.Style["display"] = "";
                        Preview.Style["display"] = "none";
                        IPPic.Src = url;
                        Label122.Text = "";
                        fupIPPic.Enabled = false;
                        btnIPPicAdd.Style["display"] = "none";
                        disableAdd.Style["display"] = "inline";
                        btnIPPicDelete.Style["display"] = "inline";
                        disableDelete.Style["display"] = "none";
                        //btnIPPicAdd.Enabled = false;
                        //btnIPPicDelete.Enabled = true;
                        Label122.Text = "上傳成功！記得按儲存，照片才會存";
                        //  lblShowMsg.Text = "記得按儲存，照片才會存"; postback後會消失
                        //  ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect();", true);
                    }
                    catch (Exception)
                    {
                        lblShowErr.Text = "上傳失敗";
                        ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                        fupIPPic.Enabled = true;
                        btnIPPicAdd.Style["display"] = "inline";
                        disableAdd.Style["display"] = "none";
                        btnIPPicDelete.Style["display"] = "none";
                        disableDelete.Style["display"] = "inline";
                        //btnIPPicAdd.Enabled = true;
                        //btnIPPicDelete.Enabled = false;
                    }
                }
                else
                {
                    lblShowErr.Text = "圖片格式錯誤";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                    fupIPPic.Enabled = true;
                    btnIPPicAdd.Style["display"] = "inline";
                    disableAdd.Style["display"] = "none";
                    btnIPPicDelete.Style["display"] = "none";
                    disableDelete.Style["display"] = "inline";
                    //btnIPPicAdd.Enabled = true;
                    //btnIPPicDelete.Enabled = false;
                }
            }
        }

        //照片刪除
        protected void btnIPPicDelete_Click(object sender, EventArgs e)
        {
            string url = Server.MapPath(IPPic.Src);//string url = Server.MapPath(EMPPic.Src);
            //  Label64.Text = url;
            if (File.Exists(url))
            {
                File.Delete(url);
                IPPic.Src = "";
                IPPic.Style["display"] = "none";
                Preview.Style["display"] = "none";
                url = "";
                fupIPPic.Enabled = true;
                btnIPPicAdd.Style["display"] = "inline";//inline
                disableAdd.Style["display"] = "none";//none
                btnIPPicDelete.Style["display"] = "none";
                disableDelete.Style["display"] = "inline";
                //btnIPPicAdd.Enabled = true;
                //btnIPPicDelete.Enabled = false;
                Label122.Text = "完成刪除！";
            }
        }

        protected void btnIPBaic_Click(object sender, EventArgs e)
        {
            //  pnlIPBasic.Visible = true;
            //   pnlAllowance.Visible = false;
        }

        protected void btnAllowance_Click(object sender, EventArgs e)
        {
            //  pnlAllowance.Visible = true;
            //   pnlIPBasic.Visible = false;
        }

        public string GetMaxIPNO()
        {
            int ipyear;
            ipyear = Convert.ToInt32(sqlTime.time_year()) - 1911;
            string getmaxno = SqlIPBasic.FindMaxIpno(ipyear.ToString());
            int countmaxno = 0;
            if (getmaxno.Trim() == "")
            {
                SqlIPBasic.addYearMaxNo(ipyear.ToString());
                countmaxno = Convert.ToInt32(SqlIPBasic.FindMaxIpno(ipyear.ToString())) + 1;
            }
            else
            {
                countmaxno = Convert.ToInt32(getmaxno.Trim()) + 1;
            }
            string thousand = Convert.ToString((countmaxno / 1000) % 10);
            string hundred = Convert.ToString((countmaxno / 100) % 10);
            string ten = Convert.ToString((countmaxno / 10) % 10);
            string single = Convert.ToString(countmaxno % 10);
            string ipmaxno = ipyear.ToString() + thousand + hundred + ten + single;

            return ipmaxno;
        }

        //飲食習慣片語
        protected void ibtnhabitPhrase_Click(object sender, ImageClickEventArgs e)
        {
            string ch = "";
            int num = 0;
            List<TextBox> fh = new List<TextBox>();
            foreach (Object a in ((ImageButton)sender).Parent.Controls)
            {
                if (a.GetType().Name == "TextBox")
                {
                    fh.Add((TextBox)a);
                }
            }
            switch (((ImageButton)sender).ID)
            {
                case "ibtnSignRelatePhrase_fb":
                    num = 0;
                    break;
                case "ibtnSignRelatePhrase_fl":
                    num = 1;
                    break;
                case "ibtnSignRelatePhrase_fd":
                    num = 2;
                    break;
                case "ibtnSignRelatePhrase_forb":
                    num = 3;
                    break;
                case "ibtnSignRelatePhrase_spc":
                    num = 4;
                    break;
                case "ibtnSignRelatePhrase_mb":
                    num = 5;
                    break;
                case "ibtnSignRelatePhrase_ms":
                    num = 6;
                    break;
                case "ibtnSignRelatePhrase_ml":
                    num = 7;
                    break;
                case "ibtnSignRelatePhrase_mt":
                    num = 8;
                    break;
                case "ibtnSignRelatePhrase_md":
                    num = 9;
                    break;
                case "ibtnSignRelatePhrase_mmn":
                    num = 10;
                    break;
            }
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();

            string pharsename = sqlPHRASE.getphrase(((ImageButton)sender).ToolTip.Trim()); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            chklp[0].ID = "chkhabit";
            chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            chklp[0].RepeatLayout = RepeatLayout.Table;
            //將片語陣列內的值逐一放置CheckBoxList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = false;
            btnSENT_Sign.Visible = false;
            btnSENT_Re.Visible = false;
            btnSENT_Re0.Visible = false;
            btnSENT_Re1.Visible = false;
            btnSENT_Re2.Visible = false;
            btnSENT_PAY.Visible = false;
            btnSENT_habit.Visible = true;

            ScriptManager1.SetFocus(fh[num]);

        }



        protected void ibtnSignRelatePhrase_Click(object sender, ImageClickEventArgs e)
        {
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("親戚關係"); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            //chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            //chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            //chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            //chklp[0].RepeatLayout = RepeatLayout.Table;

            rdblp[0] = new RadioButtonList();//建立RadioButtonList物件
            rdblp[0].RepeatColumns = 5; // 此RadioButtonList物件每一行有5個item
            rdblp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此RadioButtonList物件的item排列方式是水平
            rdblp[0].RepeatLayout = RepeatLayout.Table;

            //將片語陣列內的值逐一放置RadioButtonList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                //chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                //chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                //chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                //chklp[0].DataBind();

                rdblp[0].Items.Add(phrase_item[0, j]); //建立RadioButtonList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                rdblp[0].Items[j].Value = phrase_item[1, j]; //RadioButtonList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                rdblp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                rdblp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            //tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            tabid.Controls.Add(rdblp[0]); //把RadioButtonList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = false;
            btnSENT_Sign.Visible = true;
            btnSENT_Re.Visible = false;
            btnSENT_Re0.Visible = false;
            btnSENT_Re1.Visible = false;
            btnSENT_Re2.Visible = false;
            btnSENT_habit.Visible = false;
            btnSENT_PAY.Visible = false;
            ScriptManager1.SetFocus(txtSignRelate);

            /*
            string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(txtSignRelate.Text, "D$KtjS*)ch$ej^o&%%$y"));
            coder = Mail_Check.encrypt1.EnCode(coder);
            string value = compare_item(txtSignRelate);
            //value = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            value = Server.UrlEncode(value); value = Server.UrlEncode(value);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "ShowSubWin('../Blockui.aspx?day=" + coder + "&val=" + value + "&s=1');", true);
            */
        }
        protected void ibtnReRelatePhrase_Click(object sender, ImageClickEventArgs e)
        {
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("親戚關係"); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            chklp[0].RepeatLayout = RepeatLayout.Table;
            //將片語陣列內的值逐一放置CheckBoxList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = false;
            btnSENT_Sign.Visible = false;
            btnSENT_Re.Visible = true;
            btnSENT_Re0.Visible = false;
            btnSENT_Re1.Visible = false;
            btnSENT_Re2.Visible = false;
            btnSENT_habit.Visible = false;
            btnSENT_PAY.Visible = false;
            ScriptManager1.SetFocus(txtRERelate);
            /*
            string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(txtRERelate.Text, "D$KtjS*)ch$ej^o&%%$y"));
            coder = Mail_Check.encrypt1.EnCode(coder);
            string value = compare_item(txtRERelate);
            //value = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            value = Server.UrlEncode(value); value = Server.UrlEncode(value);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "ShowSubWin('../Blockui.aspx?day=" + coder + "&val=" + value + "&s=2');", true);
            */
        }
        protected void ibtnReRelatePhrase0_Click(object sender, ImageClickEventArgs e)
        {
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("親戚關係"); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            chklp[0].RepeatLayout = RepeatLayout.Table;
            //將片語陣列內的值逐一放置CheckBoxList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = false;
            btnSENT_Sign.Visible = false;
            btnSENT_Re.Visible = false;
            btnSENT_Re0.Visible = true;
            btnSENT_Re1.Visible = false;
            btnSENT_Re2.Visible = false;
            btnSENT_habit.Visible = false;
            btnSENT_PAY.Visible = false;
            ScriptManager1.SetFocus(txtRERelate0);
            /*
            string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(txtRERelate0.Text, "D$KtjS*)ch$ej^o&%%$y"));
            coder = Mail_Check.encrypt1.EnCode(coder);
            string value = compare_item(txtRERelate0);
            //value = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            value = Server.UrlEncode(value); value = Server.UrlEncode(value);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "ShowSubWin('../Blockui.aspx?day=" + coder + "&val=" + value + "&s=3');", true);
             */
        }
        protected void ibtnReRelatePhrase1_Click(object sender, ImageClickEventArgs e)
        {
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("親戚關係"); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            chklp[0].RepeatLayout = RepeatLayout.Table;
            //將片語陣列內的值逐一放置CheckBoxList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = false;
            btnSENT_Sign.Visible = false;
            btnSENT_Re.Visible = false;
            btnSENT_Re0.Visible = false;
            btnSENT_Re1.Visible = true;
            btnSENT_Re2.Visible = false;
            btnSENT_habit.Visible = false;
            btnSENT_PAY.Visible = false;
            ScriptManager1.SetFocus(txtRERelate1);
            /*
            string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(txtRERelate1.Text, "D$KtjS*)ch$ej^o&%%$y"));
            coder = Mail_Check.encrypt1.EnCode(coder);
            string value = compare_item(txtRERelate1);
            //value = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            value = Server.UrlEncode(value); value = Server.UrlEncode(value);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "ShowSubWin('../Blockui.aspx?day=" + coder + "&val=" + value + "&s=3');", true);
             */
        }

        protected void ibtnReRelatePhrase2_Click(object sender, ImageClickEventArgs e)
        {
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("親戚關係"); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            chklp[0].RepeatLayout = RepeatLayout.Table;
            //將片語陣列內的值逐一放置CheckBoxList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = false;
            btnSENT_Sign.Visible = false;
            btnSENT_Re.Visible = false;
            btnSENT_Re0.Visible = false;
            btnSENT_Re1.Visible = false;
            btnSENT_Re2.Visible = true;
            btnSENT_habit.Visible = false;
            btnSENT_PAY.Visible = false;
            ScriptManager1.SetFocus(txtRERelate_Re3);
            /*
            string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(txtRERelate1.Text, "D$KtjS*)ch$ej^o&%%$y"));
            coder = Mail_Check.encrypt1.EnCode(coder);
            string value = compare_item(txtRERelate1);
            //value = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            value = Server.UrlEncode(value); value = Server.UrlEncode(value);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "ShowSubWin('../Blockui.aspx?day=" + coder + "&val=" + value + "&s=3');", true);
             */
        }

        protected string compare_item(TextBox textbox)
        {
            string[] relatephrase = IPBasic.SqlIPBasic.GetRelatePharse();
            char[] delimiterChars = { ',' };
            string value = "";
            if (textbox.Text.Trim() != "")
            {
                string[] sArray = textbox.Text.Split(delimiterChars);
                for (int i = 0; i < sArray.Length; i++)
                {
                    if (sArray[i].Trim() != "")
                    {
                        int count = Array.IndexOf(relatephrase, sArray[i]);
                        if (count == -1)
                            value += sArray[i] + ",";
                    }
                }
            }
            return value;
        }

        protected void btnCutPic_Click(object sender, EventArgs e)
        {
            //pnlIPList.Visible = false;
            //pnlCutPicture.Visible = true;
            //ImgCutPic.Src = "";
            //ImgCutPic.Src = IPPic.Src;
        }

        protected void btnCutConfirm_Click(object sender, EventArgs e)
        {
            string connection_id = Session["H_id"].ToString();
            int x = int.Parse(txtX.Text);
            int y = int.Parse(txtY.Text);
            int w = int.Parse(txtWidth.Text);
            int h = int.Parse(TextHeight.Text);
            string photodate = sqlTime.time().ToString();
            string f = ImgCutPic.Src.Split('.')[1];
            string cutPath = Server.MapPath("../") + "Image/" + connection_id + "/IPImage/" + lblIPNO.Text + "_" + photodate + "c" + "." + f;
            string CutPathR = Page.ResolveUrl("~") + "Image/" + connection_id + "/IPImage/" + lblIPNO.Text + "_" + photodate + "c" + "." + f;
            DrawImage(Server.MapPath(ImgCutPic.Src), cutPath, x, y, w, h);
            IPPic.Src = "";
            IPPic.Src = CutPathR;
            pnlIPList.Visible = true;
            pnlCutPicture.Visible = false;
        }
        protected void DrawImage(string srcImage, string destImage, int x, int y, int width, int height)
        {
            using (System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(srcImage))
            {
                using (System.Drawing.Image templateImage = new System.Drawing.Bitmap(width, height))
                {
                    using (System.Drawing.Graphics templateGraphics = System.Drawing.Graphics.FromImage(templateImage))
                    {
                        templateGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        templateGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        templateGraphics.DrawImage(sourceImage, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(x, y, width, height), System.Drawing.GraphicsUnit.Pixel);
                        templateImage.Save(destImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
        }

        //protected void txtIPBirthday_TextChanged(object sender, EventArgs e)
        //{
        //    //ChangeCount++;
        //    try
        //    {
        //        string dobdate = txtIPBirthday.Text.Substring(0, 4) + txtIPBirthday.Text.Substring(5, 2) + txtIPBirthday.Text.Substring(8, 2);
        //        int vdate = Convert.ToInt32(dobdate);
        //        string datewarning = DateTimeCheck.datevaild(dobdate);
        //        if (datewarning == "日期格式錯誤")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('" + datewarning + "');", true);
        //        }
        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "alert('日期格式錯誤！輸入格式：西元年/月/日（yyyy/MM/dd）');", true);
        //    }
        //}

        //現居地址同戶籍地址
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string[] a = new string[11];
        //    a[0] = txtLining.Text;
        //    a[1] = txtVillage.Text;
        //    a[2] = txtNeighbor.Text;
        //    a[3] = txtRoad.Text;
        //    a[4] = txtStreet.Text;
        //    a[5] = txtSegment.Text;
        //    a[6] = txtAlley.Text;
        //    a[7] = txtLane.Text;
        //    a[8] = txtNumber.Text;
        //    a[9] = txtFloor.Text;
        //    a[10] = txtRoom.Text;

        //    string[] b = new string[12];
        //    b[0] = "里";
        //    b[1] = "村";
        //    b[2] = "鄰";
        //    b[3] = "路";
        //    b[4] = "街";
        //    b[5] = "段";
        //    b[6] = "巷";
        //    b[7] = "弄";
        //    if (txtNumberNo.Text != "")
        //        b[8] = "號之" + txtNumberNo.Text + " ";
        //    else
        //        b[8] = "號 ";
        //    if (txtFloorNo.Text != "")
        //        b[9] = "樓之" + txtFloorNo.Text + "--";
        //    else
        //        b[9] = "樓";

        //    b[10] = "室";

        //    TextBox3.Text = dropTown.SelectedValue + dropCountryname.SelectedValue + dropTown.SelectedItem;

        //    for (int i = 0; i < a.Count(); i++)
        //    {
        //        TextBox3.Text += a[i];
        //        if (a[i].Trim() != "")
        //        {
        //            TextBox3.Text += b[i];
        //        }
        //    }

        //    Button1.Visible = false;
        //    TextBox3.Visible = true;
        //    Panel1.Visible = false;
        //    Button2.Visible = true;
        //}
        ////變更現居地址
        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    Panel1.Visible = true;
        //    TextBox3.Visible = false;
        //    Button1.Visible = true;
        //    Button2.Visible = false;
        //}
        //現居地址同醫療機構地址
        protected void Button5_Click(object sender, EventArgs e)
        {
            TextBox3.Text = SqlIPBasic.SearchHPInfo(Session["hp_name"].ToString().Trim());
            Button1.Style["display"] = "none";
            TextBox3.Style["display"] = "inline";
            Panel1.Style["display"] = "none";
            Button2.Style["display"] = "inline";
        }

        protected void TextBoxNO_TextChanged(object sender, EventArgs e)
        {
            bool b = SqlIPBasic.CheckIPNO(TextBoxNO.Text.Trim());
            if (b)
            {
                lblShowErr.Text = "住民編號:" + TextBoxNO.Text + " 已存在！";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                TextBoxNO.Text = "";
            }
            else
            {
                Label187.Text = "";
                fupIPPic.Enabled = true;
                //btnIPPicAdd.Enabled = true;
                //btnIPPicDelete.Enabled = false;
                this.txtIPName.Focus(); //預設焦點
                btnIPPicAdd.Style["display"] = "inline";
                disableAdd.Style["display"] = "none";
            }

        }

        //Link到新增住民資料
        protected void LinkBtnGoToAddView_Click(object sender, EventArgs e)
        {
            Response.Redirect("IP_Basic_A.aspx");
        }

        //Link到查詢/修改住民資料
        protected void LinkBtnGoToQUView_Click(object sender, EventArgs e)
        {
            Response.Redirect("IP_Basic_MM.aspx");
        }

        ////將戶籍地址代入主簽約者地址
        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    string[] a = new string[11];
        //    a[0] = txtLining.Text;
        //    a[1] = txtVillage.Text;
        //    a[2] = txtNeighbor.Text;
        //    a[3] = txtRoad.Text;
        //    a[4] = txtStreet.Text;
        //    a[5] = txtSegment.Text;
        //    a[6] = txtAlley.Text;
        //    a[7] = txtLane.Text;
        //    a[8] = txtNumber.Text;
        //    a[9] = txtFloor.Text;
        //    a[10] = txtRoom.Text;

        //    string[] b = new string[12];
        //    b[0] = "里";
        //    b[1] = "村";
        //    b[2] = "鄰";
        //    b[3] = "路";
        //    b[4] = "街";
        //    b[5] = "段";
        //    b[6] = "巷";
        //    b[7] = "弄";
        //    if (txtNumberNo.Text != "")
        //        b[8] = "號之" + txtNumberNo.Text + " ";
        //    else
        //        b[8] = "號 ";
        //    if (txtFloorNo.Text != "")
        //        b[9] = "樓之" + txtFloorNo.Text + "--";
        //    else
        //        b[9] = "樓";
        //    b[10] = "室";

        //    TextBox7.Text = dropTown.SelectedValue + dropCountryname.SelectedValue + dropTown.SelectedItem;

        //    for (int i = 0; i < a.Count(); i++)
        //    {
        //        TextBox7.Text += a[i];
        //        if (a[i].Trim() != "")
        //        {
        //            TextBox7.Text += b[i];
        //        }
        //    }

        //    Button3.Visible = false;
        //    TextBox7.Visible = true;
        //    Panel2.Visible = false;
        //    Button7.Visible = true;
        //}

        ////變更主簽約者地址
        //protected void Button7_Click(object sender, EventArgs e)
        //{
        //    Panel2.Visible = true;
        //    TextBox7.Visible = false;
        //    Button3.Visible = true;
        //    Button7.Visible = false;
        //}

        ////將戶籍地址代入連帶保證者地址
        //protected void Button8_Click(object sender, EventArgs e)
        //{
        //    string[] a = new string[11];
        //    a[0] = txtLining.Text;
        //    a[1] = txtVillage.Text;
        //    a[2] = txtNeighbor.Text;
        //    a[3] = txtRoad.Text;
        //    a[4] = txtStreet.Text;
        //    a[5] = txtSegment.Text;
        //    a[6] = txtAlley.Text;
        //    a[7] = txtLane.Text;
        //    a[8] = txtNumber.Text;
        //    a[9] = txtFloor.Text;
        //    a[10] = txtRoom.Text;

        //    string[] b = new string[12];
        //    b[0] = "里";
        //    b[1] = "村";
        //    b[2] = "鄰";
        //    b[3] = "路";
        //    b[4] = "街";
        //    b[5] = "段";
        //    b[6] = "巷";
        //    b[7] = "弄";
        //    if (txtNumberNo.Text != "")
        //        b[8] = "號之" + txtNumberNo.Text + " ";
        //    else
        //        b[8] = "號 ";
        //    if (txtFloorNo.Text != "")
        //        b[9] = "樓之" + txtFloorNo.Text + "--";
        //    else
        //        b[9] = "樓";

        //    b[10] = "室";

        //    TextBox8.Text = dropTown.SelectedValue + dropCountryname.SelectedValue + dropTown.SelectedItem;

        //    for (int i = 0; i < a.Count(); i++)
        //    {
        //        TextBox8.Text += a[i];
        //        if (a[i].Trim() != "")
        //        {
        //            TextBox8.Text += b[i];
        //        }
        //    }

        //    Button8.Visible = false;
        //    TextBox8.Visible = true;
        //    Panel3.Visible = false;
        //    Button9.Visible = true;
        //}

        ////變更連帶保證者地址
        //protected void Button9_Click(object sender, EventArgs e)
        //{
        //    Panel3.Visible = true;
        //    TextBox8.Visible = false;
        //    Button8.Visible = true;
        //    Button9.Visible = false;
        //}


        //身份證字號驗證
        protected void txtIPID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string idChar = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
                int checksum = 0, idpos = 0;
                string id;
                Label189.Text = "";

                id = txtIPID.Text;
                idpos = idChar.IndexOf(id.Substring(0, 1).ToUpper());

                if (id.Substring(1, 1) == "1")
                {
                    rbSex.SelectedValue = "1"; //男性
                }
                else if (id.Substring(1, 1) == "2")
                {
                    rbSex.SelectedValue = "2"; //女性
                }
                else
                {
                    lblShowErr.Text = "無此性別";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                }

                //檢查
                idpos += 10;

                for (int i = 1; i < 9; i++)
                {
                    checksum += Convert.ToInt32(id.Substring(i, 1)) * (9 - i);
                }
                idpos = (idpos / 10) * 1 + (idpos % 10) * 9;
                checksum += idpos + Convert.ToInt32(id.Substring(9, 1));

                if (checksum % 10 != 0)
                {
                    lblShowErr.Text = "身份證輸入錯誤！請再核對";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                }
            }
            catch
            {
                lblShowErr.Text = "身份證字號是必填資料！請輸入";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
            }
        }

        //列印
        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    string connection_id = Session["H_id"].ToString();
        //    string language = Session["language"].ToString();
        //    string hp_name = Session["hp_name"].ToString();
        //    string ip_no = SqlIPBasic.IDselect(TextBoxNO.Text);

        //    DataTable dt_Basic_Record = sqlPrint_Basic.getRecord_Basic(connection_id, ip_no);

        //    #region 欄位名稱翻譯
        //    string txt_ip_no = Language.Language.translate("IP_NO", language);//病歷號碼
        //    string txt_ip_name = Language.Language.translate("IP_Name", language);//住民姓名
        //    string txt_ip_birth = Language.Language.translate("Birthday", language);//住民生日
        //    string txt_ip_id = Language.Language.translate("IP_ID", language);//身分證號
        //    //string txt_ip_healthCard = Language.Language.translate("Health_Card", language);//健保代號
        //    //string txt_ip_bloodType = Language.Language.translate("Blood_Type", language);//血型
        //    string txt_ip_sex = Language.Language.translate("Sex", language);//性別
        //    string txt_ip_height = Language.Language.translate("Height", language);//身高
        //    string txt_ip_weight = Language.Language.translate("Weight", language);//體重
        //    //string txt_ip_birthPlace = Language.Language.translate("Birth_Place", language)+" / 出生地";//籍貫
        //    string txt_nowADD = Language.Language.translate("Residence_Address", language);//現居地址
        //    string txt_preADD = Language.Language.translate("Present_Address", language);//戶籍地址
        //    string txt_maritalStatus = Language.Language.translate("Marital_Status", language);//婚姻狀況
        //    string txt_inReason = Language.Language.translate("IN_Reason", language);//入住原因
        //    string txt_spouseName = Language.Language.translate("Spouse_Name", language);//配偶姓名
        //    string txt_referralSource = Language.Language.translate("Referral_Source", language); //轉借介源
        //    string txt_spouse_Responsibility = Language.Language.translate("Spouse_Responsibility", language);//配偶責任感
        //    string txt_referralName = Language.Language.translate("Referral_Name", language);//轉介者姓名
        //    string txt_IP_Son = Language.Language.translate("IP_Son", language); //住民兒子數
        //    string txt_IP_Daughter = Language.Language.translate("IP_Daughter", language); //住民女兒數
        //    string txt_contractor = Language.Language.translate("Contractor", language);//主簽約者
        //    string txt_contractorTEL = Language.Language.translate("Contractor_TEL", language);//主簽約者電話
        //    string txt_ContractorRelationship = Language.Language.translate("Contractor_Relationship", language);//主簽約者關係
        //    string txt_ContractorAddress = Language.Language.translate("Contractor_Address", language);//主簽約者地址
        //    string txt_Guarantor = Language.Language.translate("Guarantor", language); //連帶保證人
        //    string txt_GuarantorTEL = Language.Language.translate("Guarantor_TEL", language);//連帶保證人電話
        //    string txt_GuarantorRelationship = Language.Language.translate("Guarantor_Relationship", language);//連帶保證人關係
        //    string txt_GuarantorAddress = Language.Language.translate("Guarantor_Address", language); //連帶保證者地址
        //    string txt_emergencyContact = Language.Language.translate("Emergency_Contact", language);//緊急聯絡人
        //    string txt_emergencyContactPhone = Language.Language.translate("Emergency_Contact_Phone", language);//緊急連絡人電話
        //    string txt_EmergencyContactRelationship = Language.Language.translate("Emergency_Contact_Relationship", language);//緊急聯絡人關係
        //    string txt_Payer = Language.Language.translate("Payer", language);//主要繳款人
        //    string txt_PayerTEL = Language.Language.translate("Payer_TEL", language); //主要繳款人電話
        //    string txt_PayerStatusAssessment = Language.Language.translate("Payer_Status_Assessment", language);//繳款人現況評估
        //    string txt_statusCategory = Language.Language.translate("Status_Category", language);//身分類別
        //    string txt_Religion = Language.Language.translate("Religion", language);//宗教信仰
        //    string txt_YNAllowance = Language.Language.translate("YNAllowance", language);//是否補助
        //    string txt_checkMethod = Language.Language.translate("Check_Method", language);//入住方式
        //    string txt_talkAbility = Language.Language.translate("Talk_Ability", language); //語言能力
        //    string txt_Education = Language.Language.translate("Education", language);//教育程度
        //    string txt_languageCommunication = Language.Language.translate("Language_Communication", language);//溝通語言
        //    string txt_IP_Career = Language.Language.translate("IP_Career", language);//住民職業
        //    string txt_IP_Money = Language.Language.translate("IP_Money", language);//主要經濟來源
        //    string txt_IP_Insurance = Language.Language.translate("IP_Insurance", language);//住民保險
        //    string txt_Margin = Language.Language.translate("Margin", language); //保證金金額
        //    string txt_Family_Assessment = Language.Language.translate("Family_Assessment", language);//家庭問題評估
        //    string txt_Statutory_Infectious_Disease = Language.Language.translate("Statutory_Infectious_Disease", language);//法定傳染性疾病
        //    string txt_After_Adms_HX = Language.Language.translate("After_Adms_HX", language);//初入院後有無追蹤疾病史
        //    #endregion

        //    try
        //    {
        //        Document doc = new Document();
        //        MemoryStream Memory = new MemoryStream();
        //        PdfWriter pdfWriter = PdfWriter.GetInstance(doc, Memory);

        //        //設定頁首頁尾
        //        string pdffilename = "IPBasic_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf";
        //        sqlPDFFooter.Headercontent(pdffilename);

        //        pdfWriter.PageEvent = new sqlPDFFooter();

        //        // Font conf
        //        BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
        //        iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 14);
        //        iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
        //        iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 10);

        //        doc.Open();

        //        // 加入自動列印指令碼
        //        pdfWriter.AddJavaScript(@"var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.silent;pp.pageHandling = pp.constants.handling.none;var fv = pp.constants.flagValues;pp.flags |= fv.setPageSize;pp.flags |= (fv.suppressCenter | fv.suppressRotate);this.print(pp);");

        //        Paragraph Title = new Paragraph(hp_name, ChFont2);
        //        Paragraph Title2 = new Paragraph("住民基本資料", ChFont);
        //        Paragraph Title4 = new Paragraph("住民資料檔", ChFont2);
        //        Title.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
        //        Title4.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右

        //        doc.Add(Title);
        //        doc.Add(Title4);
        //        doc.Add(Title2);

        //        PdfPTable table = new PdfPTable(6);
        //        PdfPTable table_family = new PdfPTable(6);
        //        PdfPTable table_identity = new PdfPTable(2);
        //        PdfPTable table_habit = new PdfPTable(new float[] { 2, 1, 5 });
        //        table.WidthPercentage = 100; //一整頁
        //        table.SpacingBefore = 5;
        //        table.KeepTogether = true;
        //        table_family.WidthPercentage = 100; //一整頁
        //        table_family.SpacingBefore = 5;
        //        //table_family.KeepTogether = true;
        //        table_identity.WidthPercentage = 100; //一整頁
        //        table_identity.SpacingBefore = 5;
        //        //table_identity.KeepTogether = true;
        //        table_habit.WidthPercentage = 100; //一整頁
        //        table_habit.SpacingBefore = 5;
        //        //table_habit.KeepTogether = true;

        //        PdfPCell[] cell = new PdfPCell[147];

        //        string path = Server.MapPath("~/Image/" + connection_id + "/IPImage/" + dt_Basic_Record.Rows[0]["PHOTO"].ToString());

        //        if (File.Exists(path))
        //        {
        //            iTextSharp.text.Image ip_jpg = iTextSharp.text.Image.GetInstance(new Uri(path));
        //            ip_jpg.ScaleAbsolute(150f, 150f);//ip_jpg.ScaleToFit(150f, 150f);
        //            //ip_jpg.ScalePercent(25f);
        //            //Image img = Image.GetInstance(imagePath);
        //            //img.ScaleAbsolute(159f, 159f);

        //            cell[0] = new PdfPCell(ip_jpg);
        //        }
        //        else
        //        {
        //            cell[0] = new PdfPCell(new iTextSharp.text.Phrase("未上傳住民照片", ChFont3));
        //        }
        //        cell[0].Rowspan = 8;
        //        cell[0].Colspan = 2;
        //        cell[0].HorizontalAlignment = Element.ALIGN_CENTER;
        //        cell[0].VerticalAlignment = Element.ALIGN_MIDDLE;

        //        cell[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號", ChFont3));
        //        cell[1].GrayFill = 0.9f;
        //        cell[2] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_NO_NEW"].ToString(), ChFont3));

        //        cell[3] = new PdfPCell(new iTextSharp.text.Phrase("住民姓名", ChFont3));
        //        cell[3].GrayFill = 0.9f;
        //        cell[4] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_NAME"].ToString(), ChFont3));

        //        cell[5] = new PdfPCell(new iTextSharp.text.Phrase("出生年月日", ChFont3));
        //        cell[5].GrayFill = 0.9f;

        //        string ip_dob = dt_Basic_Record.Rows[0]["DOB"].ToString().Trim();
        //        if (ip_dob != "" && ip_dob.Length == 8)
        //            ip_dob = ip_dob.Substring(0, 4) + "/" + ip_dob.Substring(4, 2) + "/" + ip_dob.Substring(6, 2);
        //        cell[6] = new PdfPCell(new iTextSharp.text.Phrase(ip_dob, ChFont3));

        //        cell[7] = new PdfPCell(new iTextSharp.text.Phrase("性別", ChFont3));
        //        cell[7].GrayFill = 0.9f;
        //        cell[8] = new PdfPCell(new iTextSharp.text.Phrase(transToItem_SEX(dt_Basic_Record.Rows[0]["SEX"].ToString()), ChFont3));

        //        cell[9] = new PdfPCell(new iTextSharp.text.Phrase("國別", ChFont3));
        //        cell[9].GrayFill = 0.9f;
        //        cell[10] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_CNRY"].ToString(), ChFont3));

        //        cell[11] = new PdfPCell(new iTextSharp.text.Phrase("身分證號碼", ChFont3));
        //        cell[11].GrayFill = 0.9f;
        //        cell[12] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_ID"].ToString(), ChFont3));

        //        cell[13] = new PdfPCell(new iTextSharp.text.Phrase("外國居留證", ChFont3));
        //        cell[13].GrayFill = 0.9f;
        //        cell[14] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["ARC"].ToString(), ChFont3));

        //        cell[15] = new PdfPCell(new iTextSharp.text.Phrase("護照號碼", ChFont3));
        //        cell[15].GrayFill = 0.9f;
        //        cell[16] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PASSPORT"].ToString(), ChFont3));

        //        cell[17] = new PdfPCell(new iTextSharp.text.Phrase("居留期限", ChFont3));
        //        cell[17].GrayFill = 0.9f;
        //        cell[18] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["RESIDE_DATE"].ToString(), ChFont3));

        //        cell[19] = new PdfPCell(new iTextSharp.text.Phrase("DNR註記", ChFont3));
        //        cell[19].GrayFill = 0.9f;
        //        cell[20] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["DNR"].ToString(), ChFont3));

        //        cell[21] = new PdfPCell(new iTextSharp.text.Phrase("身高", ChFont3));
        //        cell[21].GrayFill = 0.9f;
        //        cell[22] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["HEIGHT"].ToString(), ChFont3));

        //        cell[23] = new PdfPCell(new iTextSharp.text.Phrase("體重", ChFont3));
        //        cell[23].GrayFill = 0.9f;
        //        cell[24] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["WEIGHT"].ToString(), ChFont3));

        //        cell[25] = new PdfPCell(new iTextSharp.text.Phrase("戶籍地址", ChFont3));
        //        cell[25].GrayFill = 0.9f;
        //        cell[25].Colspan = 1;
        //        cell[26] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_PERM_ADR"].ToString(), ChFont3));
        //        cell[26].Colspan = 3;

        //        cell[27] = new PdfPCell(new iTextSharp.text.Phrase("現居地址", ChFont3));
        //        cell[27].GrayFill = 0.9f;
        //        cell[27].Colspan = 1;
        //        cell[28] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["NOW_ADR"].ToString(), ChFont3));
        //        cell[28].Colspan = 3;

        //        string tmp = "";
        //        if (dt_Basic_Record.Rows[0]["STATUES_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["STATUES"].ToString() == "16")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["STATUES_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["STATUES_STATE"].ToString();
        //        }
        //        cell[29] = new PdfPCell(new iTextSharp.text.Phrase("身分類別", ChFont3));
        //        cell[29].GrayFill = 0.9f;
        //        cell[30] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        cell[31] = new PdfPCell(new iTextSharp.text.Phrase("身心障礙", ChFont3));
        //        cell[31].GrayFill = 0.9f;
        //        cell[32] = new PdfPCell(new iTextSharp.text.Phrase(transToBARRIER(dt_Basic_Record.Rows[0]["BARRIER"].ToString()), ChFont3));

        //        cell[33] = new PdfPCell(new iTextSharp.text.Phrase("入住方式", ChFont3));
        //        cell[33].GrayFill = 0.9f;
        //        cell[34] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["ENTER_STATE"].ToString(), ChFont3));

        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["BELIEF_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["BELIEF"].ToString() == "9")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["BELIEF_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["BELIEF_STATE"].ToString();
        //        }
        //        cell[35] = new PdfPCell(new iTextSharp.text.Phrase("宗教信仰", ChFont3));
        //        cell[35].GrayFill = 0.9f;
        //        cell[36] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        cell[37] = new PdfPCell(new iTextSharp.text.Phrase("教育程度", ChFont3));
        //        cell[37].GrayFill = 0.9f;
        //        cell[38] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EDUCATION_STATE"].ToString(), ChFont3));

        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["CAREER_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["CAREER"].ToString() == "9")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["CAREER_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["CAREER_STATE"].ToString();
        //        }
        //        cell[39] = new PdfPCell(new iTextSharp.text.Phrase("住民職業", ChFont3));
        //        cell[39].GrayFill = 0.9f;
        //        cell[40] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        cell[41] = new PdfPCell(new iTextSharp.text.Phrase("溝通語言", ChFont3));
        //        cell[41].GrayFill = 0.9f;
        //        tmp = "";
        //        tmp = transToItem_Languaqge(dt_Basic_Record.Rows[0]["LANGUAGE"].ToString(), dt_Basic_Record.Rows[0]["LANGUAGE2"].ToString(), dt_Basic_Record.Rows[0]["LANGUAGE3"].ToString(), dt_Basic_Record.Rows[0]["LANGUAGE_EX"].ToString(), connection_id);
        //        cell[42] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["TALK_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["TALK_ABILITY"].ToString() == "5")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["TALK_ABILITY_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["TALK_STATE"].ToString();
        //        }
        //        cell[43] = new PdfPCell(new iTextSharp.text.Phrase("語言能力", ChFont3));
        //        cell[43].GrayFill = 0.9f;
        //        cell[44] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["ECO_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["ECO_SOURCE"].ToString() == "11")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["ECO_SOURCE_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["ECO_STATE"].ToString();
        //        }
        //        cell[45] = new PdfPCell(new iTextSharp.text.Phrase("主要經濟來源", ChFont3));
        //        cell[45].GrayFill = 0.9f;
        //        cell[46] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["INSURANCE_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["INSURANCE"].ToString() == "6")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["INSURANCE_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["INSURANCE_STATE"].ToString();
        //        }
        //        cell[47] = new PdfPCell(new iTextSharp.text.Phrase("住民保險", ChFont3));
        //        cell[47].GrayFill = 0.9f;
        //        cell[48] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        cell[49] = new PdfPCell(new iTextSharp.text.Phrase("家庭問題評估", ChFont3));
        //        cell[49].GrayFill = 0.9f;
        //        cell[50] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PROBLEM_STATE"].ToString(), ChFont3));

        //        cell[51] = new PdfPCell(new iTextSharp.text.Phrase("保證金", ChFont3));
        //        cell[51].GrayFill = 0.9f;
        //        cell[52] = new PdfPCell(new iTextSharp.text.Phrase("請於入住時填寫", ChFont3));//dt_Basic_Record.Rows[0]["GUAR_MONEY"].ToString()

        //        //有無法定傳染性疾病史
        //        cell[53] = new PdfPCell(new iTextSharp.text.Phrase("有無法定傳染性疾病史", ChFont3));
        //        cell[53].GrayFill = 0.9f;
        //        string statinfchx_Y = dt_Basic_Record.Rows[0]["STAT_INFC_HX"].ToString() == "1" ? "●有　" : "○有　";
        //        string statinfchx_N = dt_Basic_Record.Rows[0]["STAT_INFC_HX"].ToString() == "0" ? "●無　" : "○無　";
        //        if (dt_Basic_Record.Rows[0]["STAT_INFC_HX"].ToString() == "1")
        //            statinfchx_Y += "，" + dt_Basic_Record.Rows[0]["STAT_INFC_HX_EX"].ToString().Trim();
        //        cell[54] = new PdfPCell(new iTextSharp.text.Phrase(statinfchx_Y + statinfchx_N, ChFont3));
        //        cell[54].Colspan = 2;
        //        //初入院後有無追蹤疾病史
        //        cell[55] = new PdfPCell(new iTextSharp.text.Phrase("初入院後有無追蹤疾病史", ChFont3));
        //        cell[55].GrayFill = 0.9f;
        //        string afteradmshx_Y = dt_Basic_Record.Rows[0]["AFTER_ADMS_HX"].ToString() == "1" ? "●有　" : "○有　";
        //        string afteradmshx_N = dt_Basic_Record.Rows[0]["AFTER_ADMS_HX"].ToString() == "0" ? "●無　" : "○無　";
        //        if (dt_Basic_Record.Rows[0]["AFTER_ADMS_HX"].ToString() == "1")
        //            afteradmshx_Y += "，" + dt_Basic_Record.Rows[0]["AFTER_ADMS_HX_EX"].ToString().Trim();
        //        cell[56] = new PdfPCell(new iTextSharp.text.Phrase(afteradmshx_Y + afteradmshx_N, ChFont3));
        //        cell[56].Colspan = 2;

        //        //--家庭基本資料
        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["MARRY_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["MARRY"].ToString() == "8")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["MARRY_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["MARRY_STATE"].ToString();
        //        }
        //        cell[57] = new PdfPCell(new iTextSharp.text.Phrase("婚姻狀況", ChFont3));
        //        cell[57].GrayFill = 0.9f;
        //        cell[58] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        cell[59] = new PdfPCell(new iTextSharp.text.Phrase("配偶姓名", ChFont3));
        //        cell[59].GrayFill = 0.9f;
        //        cell[60] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["MATE_NAME"].ToString(), ChFont3));

        //        cell[61] = new PdfPCell(new iTextSharp.text.Phrase("配偶責任感", ChFont3));
        //        cell[61].GrayFill = 0.9f;
        //        cell[62] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["DUTY_STATE"].ToString(), ChFont3));

        //        cell[63] = new PdfPCell(new iTextSharp.text.Phrase("住民兒子數", ChFont3));
        //        cell[63].GrayFill = 0.9f;
        //        cell[64] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SON"].ToString(), ChFont3));

        //        cell[65] = new PdfPCell(new iTextSharp.text.Phrase("住民女兒數", ChFont3));
        //        cell[65].GrayFill = 0.9f;
        //        cell[66] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["DAUGHTER"].ToString(), ChFont3));

        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["INREASON_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["IN_REASON"].ToString() == "14")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["IN_REASON_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["INREASON_STATE"].ToString();
        //        }
        //        cell[67] = new PdfPCell(new iTextSharp.text.Phrase("入住原因", ChFont3));
        //        cell[67].GrayFill = 0.9f;
        //        cell[68] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

        //        tmp = "";
        //        if (dt_Basic_Record.Rows[0]["TRANSFER_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["TRANSFER_UNT"].ToString() == "5")
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["TRANSFER_EX"].ToString();
        //        }
        //        else
        //        {
        //            tmp = dt_Basic_Record.Rows[0]["TRANSFER_STATE"].ToString();
        //        }
        //        cell[69] = new PdfPCell(new iTextSharp.text.Phrase("轉介來源", ChFont3));
        //        cell[69].GrayFill = 0.9f;
        //        cell[70] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));
        //        cell[70].Colspan = 3;

        //        cell[71] = new PdfPCell(new iTextSharp.text.Phrase("轉介者姓名", ChFont3));
        //        cell[71].GrayFill = 0.9f;
        //        cell[72] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["TRANSFER_NAME"].ToString(), ChFont3));

        //        cell[73] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者", ChFont3));
        //        cell[73].GrayFill = 0.9f;
        //        cell[74] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_NAME"].ToString(), ChFont3));

        //        cell[75] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
        //        cell[75].GrayFill = 0.9f;
        //        cell[76] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_RELATE"].ToString(), ChFont3));

        //        cell[77] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者電話", ChFont3));
        //        cell[77].GrayFill = 0.9f;

        //        string signtel = dt_Basic_Record.Rows[0]["SIGN_TEL"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["SIGN_TEL2"].ToString().Trim() != "")
        //            signtel += ", " + "\n" + dt_Basic_Record.Rows[0]["SIGN_TEL2"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["SIGN_TEL3"].ToString().Trim() != "")
        //            signtel += ", " + "\n" + dt_Basic_Record.Rows[0]["SIGN_TEL3"].ToString().Trim();
        //        cell[78] = new PdfPCell(new iTextSharp.text.Phrase(signtel, ChFont3));

        //        cell[79] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者聯絡e-mail", ChFont3));
        //        cell[79].GrayFill = 0.9f;
        //        cell[80] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_EMAIL"].ToString(), ChFont3));
        //        cell[80].Colspan = 2;

        //        cell[81] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));//主簽約者CUTalk帳號
        //        cell[81].GrayFill = 0.9f;
        //        cell[82] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_CUTALK_MID"].ToString(), ChFont3));
        //        cell[82].Colspan = 2;

        //        cell[83] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者地址", ChFont3));
        //        cell[83].GrayFill = 0.9f;
        //        cell[84] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_ADD"].ToString(), ChFont3));
        //        cell[84].Colspan = 5;

        //        cell[85] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者", ChFont3));
        //        cell[85].GrayFill = 0.9f;
        //        cell[86] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO_NAME"].ToString(), ChFont3));
        //        cell[87] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
        //        cell[87].GrayFill = 0.9f;
        //        cell[88] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO_RELATE"].ToString(), ChFont3));

        //        cell[89] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者電話", ChFont3));
        //        cell[89].GrayFill = 0.9f;

        //        string invotel = dt_Basic_Record.Rows[0]["INVO_TEL"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["INVO_TEL2"].ToString().Trim() != "")
        //            invotel += ", " + "\n" + dt_Basic_Record.Rows[0]["INVO_TEL2"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["INVO_TEL3"].ToString().Trim() != "")
        //            invotel += ", " + "\n" + dt_Basic_Record.Rows[0]["INVO_TEL3"].ToString().Trim();
        //        cell[90] = new PdfPCell(new iTextSharp.text.Phrase(invotel, ChFont3));
        //        cell[91] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者聯絡e-mail", ChFont3));
        //        cell[91].GrayFill = 0.9f;
        //        cell[92] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO1_EMAIL"].ToString(), ChFont3));
        //        cell[92].Colspan = 2;

        //        cell[93] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));//連帶保證者CUTalk帳號
        //        cell[93].GrayFill = 0.9f;
        //        cell[94] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO1_CUTALK_MID"].ToString(), ChFont3));
        //        cell[94].Colspan = 2;

        //        cell[95] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者地址", ChFont3));
        //        cell[95].GrayFill = 0.9f;
        //        cell[96] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO_ADD"].ToString(), ChFont3));
        //        cell[96].Colspan = 5;

        //        cell[97] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1", ChFont3));
        //        cell[97].GrayFill = 0.9f;
        //        cell[98] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_NAME"].ToString(), ChFont3));

        //        cell[99] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
        //        cell[99].GrayFill = 0.9f;
        //        cell[100] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_RELATE"].ToString(), ChFont3));

        //        cell[101] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1電話", ChFont3));
        //        cell[101].GrayFill = 0.9f;
        //        string emrtel = dt_Basic_Record.Rows[0]["EMR_TEL"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["EMR_TEL2"].ToString().Trim() != "")
        //            emrtel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR_TEL2"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["EMR_TEL3"].ToString().Trim() != "")
        //            emrtel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR_TEL3"].ToString().Trim();
        //        cell[102] = new PdfPCell(new iTextSharp.text.Phrase(emrtel, ChFont3));
        //        cell[103] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1聯絡e-mail", ChFont3));
        //        cell[103].GrayFill = 0.9f;
        //        cell[104] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_EMAIL"].ToString(), ChFont3));
        //        cell[104].Colspan = 2;

        //        cell[105] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));//緊急聯絡人1 CUTalk帳號
        //        cell[105].GrayFill = 0.9f;
        //        cell[106] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_CUTALK_MID"].ToString(), ChFont3));
        //        cell[106].Colspan = 2;

        //        cell[107] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1地址", ChFont3));
        //        cell[107].GrayFill = 0.9f;
        //        cell[108] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_ADD"].ToString(), ChFont3));
        //        cell[108].Colspan = 5;

        //        cell[109] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2", ChFont3));
        //        cell[109].GrayFill = 0.9f;
        //        cell[110] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_NAME"].ToString(), ChFont3));

        //        cell[111] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
        //        cell[111].GrayFill = 0.9f;
        //        cell[112] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_RELATE"].ToString(), ChFont3));
        //        cell[113] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2電話", ChFont3));
        //        cell[113].GrayFill = 0.9f;
        //        string emr2tel = dt_Basic_Record.Rows[0]["EMR1_TEL"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["EMR1_TEL2"].ToString().Trim() != "")
        //            emr2tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR1_TEL2"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["EMR1_TEL3"].ToString().Trim() != "")
        //            emr2tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR1_TEL3"].ToString().Trim();
        //        cell[114] = new PdfPCell(new iTextSharp.text.Phrase(emr2tel, ChFont3));
        //        cell[115] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2聯絡e-mail", ChFont3));
        //        cell[115].GrayFill = 0.9f;
        //        cell[116] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_EMAIL"].ToString(), ChFont3));
        //        cell[116].Colspan = 2;

        //        cell[117] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));//緊急聯絡人2 CUTalk帳號
        //        cell[117].GrayFill = 0.9f;
        //        cell[118] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_CUTALK_MID"].ToString(), ChFont3));
        //        cell[118].Colspan = 2;

        //        cell[119] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2地址", ChFont3));
        //        cell[119].GrayFill = 0.9f;
        //        cell[120] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_ADD"].ToString(), ChFont3));
        //        cell[120].Colspan = 5;

        //        //緊急聯絡人3
        //        cell[121] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3", ChFont3));
        //        cell[121].GrayFill = 0.9f;
        //        cell[122] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_NAME"].ToString(), ChFont3));
        //        cell[123] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
        //        cell[123].GrayFill = 0.9f;
        //        cell[124] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_RELATE"].ToString(), ChFont3));
        //        cell[125] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3電話", ChFont3));
        //        cell[125].GrayFill = 0.9f;
        //        string emr3tel = dt_Basic_Record.Rows[0]["EMR2_TEL"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["EMR2_TEL2"].ToString().Trim() != "")
        //            emr3tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR2_TEL2"].ToString().Trim();
        //        if (dt_Basic_Record.Rows[0]["EMR2_TEL3"].ToString().Trim() != "")
        //            emr3tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR2_TEL3"].ToString().Trim();
        //        cell[126] = new PdfPCell(new iTextSharp.text.Phrase(emr3tel, ChFont3));
        //        cell[127] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3聯絡e-mail", ChFont3));
        //        cell[127].GrayFill = 0.9f;
        //        cell[128] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_EMAIL"].ToString(), ChFont3));
        //        cell[128].Colspan = 2;

        //        cell[129] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));//緊急聯絡人3 CUTalk帳號
        //        cell[129].GrayFill = 0.9f;
        //        cell[130] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_CUTALK_MID"].ToString(), ChFont3));
        //        cell[130].Colspan = 2;

        //        cell[131] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3地址", ChFont3));
        //        cell[131].GrayFill = 0.9f;
        //        cell[132] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_ADD"].ToString(), ChFont3));
        //        cell[132].Colspan = 5;

        //        //繳款人
        //        cell[133] = new PdfPCell(new iTextSharp.text.Phrase("主要繳款人", ChFont3));
        //        cell[133].GrayFill = 0.9f;
        //        cell[134] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_NAME"].ToString(), ChFont3));
        //        cell[135] = new PdfPCell(new iTextSharp.text.Phrase("主要繳款人電話", ChFont3));
        //        cell[135].GrayFill = 0.9f;
        //        cell[136] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_TEL"].ToString(), ChFont3));
        //        cell[137] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));
        //        cell[138] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));

        //        cell[139] = new PdfPCell(new iTextSharp.text.Phrase("主要繳款人聯絡e-mail", ChFont3));
        //        cell[139].GrayFill = 0.9f;
        //        cell[140] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_EMAIL"].ToString(), ChFont3));
        //        cell[140].Colspan = 2;

        //        cell[141] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));//主要繳款人 CUTalk帳號
        //        cell[141].GrayFill = 0.9f;
        //        cell[142] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_CUTALK_MID"].ToString(), ChFont3));
        //        cell[142].Colspan = 2;

        //        cell[143] = new PdfPCell(new iTextSharp.text.Phrase("繳款人現況評估", ChFont3));
        //        cell[143].GrayFill = 0.9f;
        //        cell[144] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_EVALUTE"].ToString(), ChFont3));
        //        cell[144].Colspan = 5;

        //        cell[145] = new PdfPCell(new iTextSharp.text.Phrase("住民優免身份", ChFont3));
        //        cell[145].GrayFill = 0.9f;
        //        cell[146] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_IDENTITY"].ToString(), ChFont3));

        //        for (int i = 0; i < cell.Length; i++)
        //        {
        //            cell[i].Padding = 5f;
        //            cell[i].VerticalAlignment = Element.ALIGN_MIDDLE;
        //            if (i <= 56)
        //            {
        //                table.AddCell(cell[i]);
        //            }
        //            else if (i >= 145)
        //            {
        //                table_identity.AddCell(cell[i]);
        //            }
        //            else
        //            {
        //                table_family.AddCell(cell[i]);
        //            }
        //        }

        //        doc.Add(table);
        //        Paragraph Title3 = new Paragraph("家庭基本資料", ChFont);
        //        doc.Add(Title3);
        //        doc.Add(table_family);
        //        Paragraph Title5 = new Paragraph("住民費用優免設定", ChFont);
        //        doc.Add(Title5);
        //        doc.Add(table_identity);


        //        string[][] habit = new string[][] { new string[] { "葷素習慣", "葷素者" , WebC_to_PS(chklvege),
        //                                                                       "素食者" , WebC_to_PS(chblvege) 
        //                                                         },
        //                                            new string[] {"素食時節限定",WebC_to_PS(chklvegetime)},
        //                                            new string[] {"主食偏好","早餐",txtfavorbreak.Text,
        //                                                                     "午餐",txtfavorlunch.Text,
        //                                                                     "晚餐",txtfavordinner.Text
        //                                                         },
        //                                            new string[] {"禁忌",txtforbid.Text},
        //                                            new string[] {"特殊指示",txtspecorder.Text},
        //                                            new string[] {"各餐之要求","早餐",txtmealbreak.Text,
        //                                                                       "點心",txtmealsnack.Text,
        //                                                                       "午餐",txtmeallunch.Text,
        //                                                                       "下午茶",txtmealtea.Text,
        //                                                                       "晚餐",txtmealdinner.Text,
        //                                                                       "宵夜",txtmealmnsnack.Text
        //                                                         }
        //                                            };

        //        for (int i = 0; i < habit.Length; i++)
        //        {
        //            PdfPCell[] habit_cell = new PdfPCell[Convert.ToInt32(habit[i].Length)];
        //            for (int j = 0; j < habit[i].Length; j++)
        //            {
        //                habit_cell[j] = new PdfPCell(new iTextSharp.text.Phrase(habit[i][j], ChFont3));
        //                if (j == 0)
        //                {
        //                    habit_cell[j].GrayFill = 0.9f;
        //                    if (i == 0)
        //                        habit_cell[j].Rowspan = 2;
        //                    if (i == 2)
        //                        habit_cell[j].Rowspan = 3;
        //                    if (i == 5)
        //                        habit_cell[j].Rowspan = 6;
        //                    if (i == 1 || i == 3 || i == 4)
        //                        habit_cell[j].Colspan = 2;
        //                }
        //                if (j > 0 && j % 2 == 1)
        //                {
        //                    if (i == 0 || i == 2 || i == 5)
        //                    {
        //                        habit_cell[j].GrayFill = 0.9f;
        //                    }
        //                }

        //                table_habit.AddCell(habit_cell[j]);
        //            }
        //        }
        //        Paragraph Title6 = new Paragraph("住民膳食習慣", ChFont);
        //        doc.Add(Title6);
        //        doc.Add(table_habit);
        //        //文件關閉
        //        doc.Close();
        //        dt_Basic_Record.Dispose();
        //        //檔案下載
        //        Response.Clear();
        //        Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
        //        Response.ContentType = "application/pdf";
        //        Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
        //        Response.OutputStream.Flush();
        //        Response.OutputStream.Close();
        //        Response.Flush();
        //        Response.End();

        //    }
        //    catch (DocumentException de)
        //    {
        //        Response.Write(de.ToString());
        //    }

        //}

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string connection_id = Session["H_id"].ToString();
            string language = Session["language"].ToString();
            string hp_name = Session["hp_name"].ToString();
            string ip_no = SqlIPBasic.IDselect(TextBoxNO.Text);

            DataTable dt_Basic_Record = sqlPrint_Basic.getRecord_Basic(connection_id, ip_no);
            DataTable dt_Basic_Record_IPInsurance = sqlPrint_Basic.getRecord_Basic_IPInsurance(connection_id, ip_no);

            #region 欄位名稱翻譯
            string txt_ip_no = Language.Language.translate("IP_NO", language);//病歷號碼
            string txt_ip_name = Language.Language.translate("IP_Name", language);//住民姓名
            string txt_ip_birth = Language.Language.translate("Birthday", language);//住民生日
            string txt_ip_id = Language.Language.translate("IP_ID", language);//身分證號
            string txt_ip_healthCard = Language.Language.translate("Health_Card", language);//健保代號
            string txt_ip_bloodType = Language.Language.translate("Blood_Type", language);//血型
            string txt_ip_sex = Language.Language.translate("Sex", language);//性別
            string txt_ip_height = Language.Language.translate("Height", language);//身高
            string txt_ip_weight = Language.Language.translate("Weight", language);//體重
            string txt_ip_birthPlace = Language.Language.translate("Birth_Place", language) + " / 出生地";//籍貫
            string txt_nowADD = Language.Language.translate("Residence_Address", language);//現居地址
            string txt_preADD = Language.Language.translate("Present_Address", language);//戶籍地址
            string txt_maritalStatus = Language.Language.translate("Marital_Status", language);//婚姻狀況
            string txt_inReason = Language.Language.translate("IN_Reason", language);//入住原因
            string txt_spouseName = Language.Language.translate("Spouse_Name", language);//配偶姓名
            string txt_referralSource = Language.Language.translate("Referral_Source", language); //轉借介源
            string txt_spouse_Responsibility = Language.Language.translate("Spouse_Responsibility", language);//配偶責任感
            string txt_referralName = Language.Language.translate("Referral_Name", language);//轉介者姓名
            string txt_IP_Son = Language.Language.translate("IP_Son", language); //住民兒子數
            string txt_IP_Daughter = Language.Language.translate("IP_Daughter", language); //住民女兒數
            string txt_contractor = Language.Language.translate("Contractor", language);//主簽約者
            string txt_contractorTEL = Language.Language.translate("Contractor_TEL", language);//主簽約者電話
            string txt_ContractorRelationship = Language.Language.translate("Contractor_Relationship", language);//主簽約者關係
            string txt_ContractorAddress = Language.Language.translate("Contractor_Address", language);//主簽約者地址
            string txt_Guarantor = Language.Language.translate("Guarantor", language); //連帶保證人
            string txt_GuarantorTEL = Language.Language.translate("Guarantor_TEL", language);//連帶保證人電話
            string txt_GuarantorRelationship = Language.Language.translate("Guarantor_Relationship", language);//連帶保證人關係
            string txt_GuarantorAddress = Language.Language.translate("Guarantor_Address", language); //連帶保證者地址
            string txt_emergencyContact = Language.Language.translate("Emergency_Contact", language);//緊急聯絡人
            string txt_emergencyContactPhone = Language.Language.translate("Emergency_Contact_Phone", language);//緊急連絡人電話
            string txt_EmergencyContactRelationship = Language.Language.translate("Emergency_Contact_Relationship", language);//緊急聯絡人關係
            string txt_Payer = Language.Language.translate("Payer", language);//主要繳款人
            string txt_PayerTEL = Language.Language.translate("Payer_TEL", language); //主要繳款人電話
            string txt_PayerStatusAssessment = Language.Language.translate("Payer_Status_Assessment", language);//繳款人現況評估
            string txt_statusCategory = Language.Language.translate("Status_Category", language);//身分類別
            string txt_Religion = Language.Language.translate("Religion", language);//宗教信仰
            string txt_YNAllowance = Language.Language.translate("YNAllowance", language);//是否補助
            string txt_checkMethod = Language.Language.translate("Check_Method", language);//入住方式
            string txt_talkAbility = Language.Language.translate("Talk_Ability", language); //語言能力
            string txt_Education = Language.Language.translate("Education", language);//教育程度
            string txt_languageCommunication = Language.Language.translate("Language_Communication", language);//溝通語言
            string txt_IP_Career = Language.Language.translate("IP_Career", language);//住民職業
            string txt_IP_Money = Language.Language.translate("IP_Money", language);//主要經濟來源
            string txt_IP_Insurance = Language.Language.translate("IP_Insurance", language);//住民保險
            string txt_Margin = Language.Language.translate("Margin", language); //保證金金額
            string txt_Family_Assessment = Language.Language.translate("Family_Assessment", language);//家庭問題評估
            string txt_Statutory_Infectious_Disease = Language.Language.translate("Statutory_Infectious_Disease", language);//法定傳染性疾病
            string txt_After_Adms_HX = Language.Language.translate("After_Adms_HX", language);//初入院後有無追蹤疾病史
            #endregion

            try
            {
                Document doc = new Document();
                MemoryStream Memory = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(doc, Memory);

                //設定頁首頁尾
                string pdffilename = "IPBasic_" + sqlTransToIP_NO_NEW.getIP_NO_NEW(connection_id, ip_no) + ".pdf";
                sqlPDFFooter.Headercontent(pdffilename);

                //calling PDFFooter class to Include in document
                pdfWriter.PageEvent = new sqlPDFFooter();

                // Font conf
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//橫式中文
                iTextSharp.text.Font ChFont = new iTextSharp.text.Font(bfChinese, 14);
                iTextSharp.text.Font ChFont2 = new iTextSharp.text.Font(bfChinese, 16);
                iTextSharp.text.Font ChFont3 = new iTextSharp.text.Font(bfChinese, 10);
                iTextSharp.text.Font ChFont4 = new iTextSharp.text.Font(bfChinese, 12);

                doc.Open();

                // 加入自動列印指令碼
                pdfWriter.AddJavaScript(@"var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.silent;pp.pageHandling = pp.constants.handling.none;var fv = pp.constants.flagValues;pp.flags |= fv.setPageSize;pp.flags |= (fv.suppressCenter | fv.suppressRotate);this.print(pp);");

                Paragraph Title = new Paragraph(hp_name, ChFont2);
                Paragraph Title2 = new Paragraph("住民基本資料", ChFont);
                Paragraph Title4 = new Paragraph("住民資料檔", ChFont2);
                Title.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右
                Title4.Alignment = 1;//設定左右對齊 0:左 1:置中 2:右

                doc.Add(Title);
                doc.Add(Title4);
                doc.Add(Title2);

                PdfPTable table = new PdfPTable(6);
                PdfPTable table_family = new PdfPTable(6);
                PdfPTable table_identity = new PdfPTable(2);
                PdfPTable table_habit = new PdfPTable(new float[] { 2, 1, 5 });
                table.WidthPercentage = 100; //一整頁
                table.SpacingBefore = 5;
                table.KeepTogether = true;
                table_family.WidthPercentage = 100; //一整頁
                table_family.SpacingBefore = 5;
                //table_family.KeepTogether = true;
                table_identity.WidthPercentage = 100; //一整頁
                table_identity.SpacingBefore = 5;
                //table_identity.KeepTogether = true;
                table_habit.WidthPercentage = 100; //一整頁
                table_habit.SpacingBefore = 5;
                //table_habit.KeepTogether = true;

                PdfPCell[] cell = new PdfPCell[147];

                string path = Server.MapPath("~/Image/" + connection_id + "/IPImage/" + dt_Basic_Record.Rows[0]["PHOTO"].ToString());

                if (File.Exists(path))
                {
                    iTextSharp.text.Image ip_jpg = iTextSharp.text.Image.GetInstance(new Uri(path));
                    ip_jpg.ScaleAbsolute(150f, 150f);//ip_jpg.ScaleToFit(300f, 150f);
                    cell[0] = new PdfPCell(ip_jpg);
                }
                else
                {
                    cell[0] = new PdfPCell(new iTextSharp.text.Phrase("未上傳住民照片", ChFont3));
                }
                cell[0].Rowspan = 8;
                cell[0].Colspan = 2;
                cell[0].HorizontalAlignment = Element.ALIGN_CENTER;
                cell[0].VerticalAlignment = Element.ALIGN_MIDDLE;

                cell[1] = new PdfPCell(new iTextSharp.text.Phrase("住民編號", ChFont3));
                cell[1].GrayFill = 0.9f;
                cell[2] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_NO_NEW"].ToString(), ChFont3));

                cell[3] = new PdfPCell(new iTextSharp.text.Phrase("住民姓名", ChFont3));
                cell[3].GrayFill = 0.9f;
                cell[4] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_NAME"].ToString(), ChFont3));

                cell[5] = new PdfPCell(new iTextSharp.text.Phrase("出生年月日", ChFont3));
                cell[5].GrayFill = 0.9f;

                string ip_dob = dt_Basic_Record.Rows[0]["DOB"].ToString().Trim();
                if (ip_dob != "" && ip_dob.Length == 8)
                    ip_dob = ip_dob.Substring(0, 4) + "/" + ip_dob.Substring(4, 2) + "/" + ip_dob.Substring(6, 2);
                cell[6] = new PdfPCell(new iTextSharp.text.Phrase(ip_dob, ChFont3));

                cell[7] = new PdfPCell(new iTextSharp.text.Phrase("性別", ChFont3));
                cell[7].GrayFill = 0.9f;
                cell[8] = new PdfPCell(new iTextSharp.text.Phrase(transToItem_SEX(dt_Basic_Record.Rows[0]["SEX"].ToString()), ChFont3));

                cell[9] = new PdfPCell(new iTextSharp.text.Phrase("國別", ChFont3));
                cell[9].GrayFill = 0.9f;
                cell[10] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_CNRY"].ToString(), ChFont3));

                cell[11] = new PdfPCell(new iTextSharp.text.Phrase("身分證號碼", ChFont3));
                cell[11].GrayFill = 0.9f;
                cell[12] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_ID"].ToString(), ChFont3));

                cell[13] = new PdfPCell(new iTextSharp.text.Phrase("外國居留證", ChFont3));
                cell[13].GrayFill = 0.9f;
                cell[14] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["ARC"].ToString(), ChFont3));

                cell[15] = new PdfPCell(new iTextSharp.text.Phrase("護照號碼", ChFont3));
                cell[15].GrayFill = 0.9f;
                cell[16] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PASSPORT"].ToString(), ChFont3));

                cell[17] = new PdfPCell(new iTextSharp.text.Phrase("居留期限", ChFont3));
                cell[17].GrayFill = 0.9f;
                cell[18] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["RESIDE_DATE"].ToString(), ChFont3));

                cell[19] = new PdfPCell(new iTextSharp.text.Phrase("DNR註記", ChFont3));
                cell[19].GrayFill = 0.9f;
                cell[20] = new PdfPCell(new iTextSharp.text.Phrase((dt_Basic_Record.Rows[0]["DNR"].ToString() == "1" ? "是" : "否"), ChFont3));

                cell[21] = new PdfPCell(new iTextSharp.text.Phrase("身高", ChFont3));
                cell[21].GrayFill = 0.9f;
                cell[22] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["HEIGHT"].ToString(), ChFont3));

                cell[23] = new PdfPCell(new iTextSharp.text.Phrase("體重", ChFont3));
                cell[23].GrayFill = 0.9f;
                cell[24] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["WEIGHT"].ToString(), ChFont3));

                cell[25] = new PdfPCell(new iTextSharp.text.Phrase("戶籍地址", ChFont3));
                cell[25].GrayFill = 0.9f;
                cell[25].Colspan = 1;
                cell[26] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_PERM_ADR"].ToString(), ChFont3));
                cell[26].Colspan = 3;

                cell[27] = new PdfPCell(new iTextSharp.text.Phrase("現居地址", ChFont3));
                cell[27].GrayFill = 0.9f;
                cell[27].Colspan = 1;
                cell[28] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["NOW_ADR"].ToString(), ChFont3));
                cell[28].Colspan = 3;

                string tmp = "";
                if (dt_Basic_Record.Rows[0]["STATUES_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["STATUES"].ToString() == "16")
                {
                    tmp = dt_Basic_Record.Rows[0]["STATUES_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["STATUES_STATE"].ToString();
                }
                cell[29] = new PdfPCell(new iTextSharp.text.Phrase("身分類別", ChFont3));
                cell[29].GrayFill = 0.9f;
                cell[30] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                cell[31] = new PdfPCell(new iTextSharp.text.Phrase("身心障礙", ChFont3));
                cell[31].GrayFill = 0.9f;
                cell[32] = new PdfPCell(new iTextSharp.text.Phrase(transToBARRIER(dt_Basic_Record.Rows[0]["BARRIER"].ToString()), ChFont3));

                cell[33] = new PdfPCell(new iTextSharp.text.Phrase("入住方式", ChFont3));
                cell[33].GrayFill = 0.9f;
                cell[34] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["ENTER_STATE"].ToString(), ChFont3));

                tmp = "";
                if (dt_Basic_Record.Rows[0]["BELIEF_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["BELIEF"].ToString() == "9")
                {
                    tmp = dt_Basic_Record.Rows[0]["BELIEF_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["BELIEF_STATE"].ToString();
                }
                cell[35] = new PdfPCell(new iTextSharp.text.Phrase("宗教信仰", ChFont3));
                cell[35].GrayFill = 0.9f;
                cell[36] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                cell[37] = new PdfPCell(new iTextSharp.text.Phrase("教育程度", ChFont3));
                cell[37].GrayFill = 0.9f;
                cell[38] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EDUCATION_STATE"].ToString(), ChFont3));

                tmp = "";
                if (dt_Basic_Record.Rows[0]["CAREER_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["CAREER"].ToString() == "9")
                {
                    tmp = dt_Basic_Record.Rows[0]["CAREER_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["CAREER_STATE"].ToString();
                }
                cell[39] = new PdfPCell(new iTextSharp.text.Phrase("住民職業", ChFont3));
                cell[39].GrayFill = 0.9f;
                cell[40] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                cell[41] = new PdfPCell(new iTextSharp.text.Phrase("溝通語言", ChFont3));
                cell[41].GrayFill = 0.9f;
                tmp = "";
                tmp = transToItem_Languaqge(dt_Basic_Record.Rows[0]["LANGUAGE"].ToString().Trim(), dt_Basic_Record.Rows[0]["LANGUAGE2"].ToString().Trim(), dt_Basic_Record.Rows[0]["LANGUAGE3"].ToString().Trim(), dt_Basic_Record.Rows[0]["LANGUAGE_EX"].ToString().Trim(), connection_id);
                cell[42] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                tmp = "";
                if (dt_Basic_Record.Rows[0]["TALK_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["TALK_ABILITY"].ToString() == "5")
                {
                    tmp = dt_Basic_Record.Rows[0]["TALK_ABILITY_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["TALK_STATE"].ToString();
                }
                cell[43] = new PdfPCell(new iTextSharp.text.Phrase("語言能力", ChFont3));
                cell[43].GrayFill = 0.9f;
                cell[44] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                tmp = "";
                if (dt_Basic_Record.Rows[0]["ECO_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["ECO_SOURCE"].ToString() == "11")
                {
                    tmp = dt_Basic_Record.Rows[0]["ECO_SOURCE_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["ECO_STATE"].ToString();
                }
                cell[45] = new PdfPCell(new iTextSharp.text.Phrase("主要主要經濟來源", ChFont3));
                cell[45].GrayFill = 0.9f;
                cell[46] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                tmp = "";
                //if (dt_Basic_Record.Rows[0]["INSURANCE_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["INSURANCE"].ToString() == "6")
                //{
                //    tmp = dt_Basic_Record.Rows[0]["INSURANCE_EX"].ToString();
                //}
                //else
                //{
                //    tmp = dt_Basic_Record.Rows[0]["INSURANCE_STATE"].ToString();
                //}
                if (SqlIPBasic.GetIPInsurance().Trim() != "")
                {
                    SqlIPBasic.SearchIPInfomation(ip_no);
                    string[] IPInsurance_content = SqlIPBasic.GetIPInsurance().Split(',');
                    for (int i = 0; i < IPInsurance_content.Length - 1; i++)
                    {
                        for (int j = 0; j < dt_Basic_Record_IPInsurance.Rows.Count; j++)
                        {
                            if (IPInsurance_content[i] == dt_Basic_Record_IPInsurance.Rows[j]["INSURANCE_ID"].ToString())
                            {
                                tmp += dt_Basic_Record_IPInsurance.Rows[j]["INSURANCE_STATE"].ToString() + ",";
                                break;
                            }
                        }
                    }
                    if (SqlIPBasic.GetIPInsuranceEx() != "")
                    {
                        tmp += SqlIPBasic.GetIPInsuranceEx();
                        //dropInsurance.Items[5].Selected = true;
                    }
                    else
                        tmp = tmp.TrimEnd(',');
                }
                cell[47] = new PdfPCell(new iTextSharp.text.Phrase("住民保險", ChFont3));
                cell[47].GrayFill = 0.9f;
                cell[48] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                cell[49] = new PdfPCell(new iTextSharp.text.Phrase("家庭問題評估", ChFont3));
                cell[49].GrayFill = 0.9f;
                cell[50] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PROBLEM_STATE"].ToString(), ChFont3));

                cell[51] = new PdfPCell(new iTextSharp.text.Phrase("保證金", ChFont3));
                cell[51].GrayFill = 0.9f;
                cell[52] = new PdfPCell(new iTextSharp.text.Phrase(lblMargin.Text, ChFont3));

                //有無法定傳染性疾病史
                cell[53] = new PdfPCell(new iTextSharp.text.Phrase("有無法定傳染性疾病史", ChFont3));
                cell[53].GrayFill = 0.9f;
                string statinfchx_Y = dt_Basic_Record.Rows[0]["STAT_INFC_HX"].ToString() == "1" ? "●有　" : "○有　";
                string statinfchx_N = dt_Basic_Record.Rows[0]["STAT_INFC_HX"].ToString() == "0" ? "●無　" : "○無　";
                if (dt_Basic_Record.Rows[0]["STAT_INFC_HX"].ToString() == "1")
                    statinfchx_Y += "，" + dt_Basic_Record.Rows[0]["STAT_INFC_HX_EX"].ToString().Trim();
                cell[54] = new PdfPCell(new iTextSharp.text.Phrase(statinfchx_Y + statinfchx_N, ChFont3));
                cell[54].Colspan = 2;
                //初入院後有無追蹤疾病史
                cell[55] = new PdfPCell(new iTextSharp.text.Phrase("初入院後有無追蹤疾病史", ChFont3));
                cell[55].GrayFill = 0.9f;
                string afteradmshx_Y = dt_Basic_Record.Rows[0]["AFTER_ADMS_HX"].ToString() == "1" ? "●有　" : "○有　";
                string afteradmshx_N = dt_Basic_Record.Rows[0]["AFTER_ADMS_HX"].ToString() == "0" ? "●無　" : "○無　";
                if (dt_Basic_Record.Rows[0]["AFTER_ADMS_HX"].ToString() == "1")
                    afteradmshx_Y += "，" + dt_Basic_Record.Rows[0]["AFTER_ADMS_HX_EX"].ToString().Trim();
                cell[56] = new PdfPCell(new iTextSharp.text.Phrase(afteradmshx_Y + afteradmshx_N, ChFont3));
                cell[56].Colspan = 2;

                //--家庭基本資料
                tmp = "";
                if (dt_Basic_Record.Rows[0]["MARRY_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["MARRY"].ToString() == "8")
                {
                    tmp = dt_Basic_Record.Rows[0]["MARRY_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["MARRY_STATE"].ToString();
                }
                cell[57] = new PdfPCell(new iTextSharp.text.Phrase("婚姻狀況", ChFont3));
                cell[57].GrayFill = 0.9f;
                cell[58] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                cell[59] = new PdfPCell(new iTextSharp.text.Phrase("配偶姓名", ChFont3));
                cell[59].GrayFill = 0.9f;
                cell[60] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["MATE_NAME"].ToString(), ChFont3));

                cell[61] = new PdfPCell(new iTextSharp.text.Phrase("配偶責任感", ChFont3));
                cell[61].GrayFill = 0.9f;
                cell[62] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["DUTY_STATE"].ToString(), ChFont3));

                cell[63] = new PdfPCell(new iTextSharp.text.Phrase("住民兒子數", ChFont3));
                cell[63].GrayFill = 0.9f;
                cell[64] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SON"].ToString(), ChFont3));

                cell[65] = new PdfPCell(new iTextSharp.text.Phrase("住民女兒數", ChFont3));
                cell[65].GrayFill = 0.9f;
                cell[66] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["DAUGHTER"].ToString(), ChFont3));

                tmp = "";
                if (dt_Basic_Record.Rows[0]["INREASON_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["IN_REASON"].ToString() == "14")
                {
                    tmp = dt_Basic_Record.Rows[0]["IN_REASON_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["INREASON_STATE"].ToString();
                }
                cell[67] = new PdfPCell(new iTextSharp.text.Phrase("入住原因", ChFont3));
                cell[67].GrayFill = 0.9f;
                cell[68] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));

                tmp = "";
                if (dt_Basic_Record.Rows[0]["TRANSFER_STATE"].ToString() == "其他" || dt_Basic_Record.Rows[0]["TRANSFER_UNT"].ToString() == "99")
                {
                    tmp = dt_Basic_Record.Rows[0]["TRANSFER_EX"].ToString();
                }
                else
                {
                    tmp = dt_Basic_Record.Rows[0]["TRANSFER_STATE"].ToString();
                }
                cell[69] = new PdfPCell(new iTextSharp.text.Phrase("轉介來源", ChFont3));
                cell[69].GrayFill = 0.9f;
                cell[70] = new PdfPCell(new iTextSharp.text.Phrase(tmp, ChFont3));
                cell[70].Colspan = 3;

                cell[71] = new PdfPCell(new iTextSharp.text.Phrase("轉介者姓名", ChFont3));
                cell[71].GrayFill = 0.9f;
                cell[72] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["TRANSFER_NAME"].ToString(), ChFont3));

                cell[73] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者", ChFont3));
                cell[73].GrayFill = 0.9f;
                cell[74] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_NAME"].ToString(), ChFont3));

                cell[75] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
                cell[75].GrayFill = 0.9f;
                cell[76] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_RELATE"].ToString(), ChFont3));

                cell[77] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者電話", ChFont3));
                cell[77].GrayFill = 0.9f;

                string signtel = dt_Basic_Record.Rows[0]["SIGN_TEL"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["SIGN_TEL2"].ToString().Trim() != "")
                    signtel += ", " + "\n" + dt_Basic_Record.Rows[0]["SIGN_TEL2"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["SIGN_TEL3"].ToString().Trim() != "")
                    signtel += ", " + "\n" + dt_Basic_Record.Rows[0]["SIGN_TEL3"].ToString().Trim();
                cell[78] = new PdfPCell(new iTextSharp.text.Phrase(signtel, ChFont3));

                cell[79] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者聯絡e-mail", ChFont3));
                cell[79].GrayFill = 0.9f;
                cell[80] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_EMAIL"].ToString(), ChFont3));
                cell[80].Colspan = 2;

                cell[81] = new PdfPCell(new iTextSharp.text.Phrase("Cu-Talk", ChFont3));//主簽約者CUTalk帳號
                cell[81].GrayFill = 0.9f;
                cell[82] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_CUTALK_MID"].ToString(), ChFont3));
                cell[82].Colspan = 2;

                cell[83] = new PdfPCell(new iTextSharp.text.Phrase("主簽約者地址", ChFont3));
                cell[83].GrayFill = 0.9f;
                cell[84] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["SIGN_ADD"].ToString(), ChFont3));
                cell[84].Colspan = 5;

                cell[85] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者", ChFont3));
                cell[85].GrayFill = 0.9f;
                cell[86] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO_NAME"].ToString(), ChFont3));
                cell[87] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
                cell[87].GrayFill = 0.9f;
                cell[88] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO_RELATE"].ToString(), ChFont3));
                cell[89] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者電話", ChFont3));
                cell[89].GrayFill = 0.9f;
                string invotel = dt_Basic_Record.Rows[0]["INVO_TEL"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["INVO_TEL2"].ToString().Trim() != "")
                    invotel += ", " + "\n" + dt_Basic_Record.Rows[0]["INVO_TEL2"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["INVO_TEL3"].ToString().Trim() != "")
                    invotel += ", " + "\n" + dt_Basic_Record.Rows[0]["INVO_TEL3"].ToString().Trim();
                cell[90] = new PdfPCell(new iTextSharp.text.Phrase(invotel, ChFont3));
                cell[91] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者聯絡e-mail", ChFont3));
                cell[91].GrayFill = 0.9f;
                cell[92] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO1_EMAIL"].ToString(), ChFont3));
                cell[92].Colspan = 2;

                cell[93] = new PdfPCell(new iTextSharp.text.Phrase("Cu-Talk", ChFont3));//連帶保證者CUTalk帳號
                cell[93].GrayFill = 0.9f;
                cell[94] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO1_CUTALK_MID"].ToString(), ChFont3));
                cell[94].Colspan = 2;

                cell[95] = new PdfPCell(new iTextSharp.text.Phrase("連帶保證者地址", ChFont3));
                cell[95].GrayFill = 0.9f;
                cell[96] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["INVO_ADD"].ToString(), ChFont3));
                cell[96].Colspan = 5;

                cell[97] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1", ChFont3));
                cell[97].GrayFill = 0.9f;
                cell[98] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_NAME"].ToString(), ChFont3));
                cell[99] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
                cell[99].GrayFill = 0.9f;
                cell[100] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_RELATE"].ToString(), ChFont3));
                cell[101] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1電話", ChFont3));
                cell[101].GrayFill = 0.9f;
                string emrtel = dt_Basic_Record.Rows[0]["EMR_TEL"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["EMR_TEL2"].ToString().Trim() != "")
                    emrtel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR_TEL2"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["EMR_TEL3"].ToString().Trim() != "")
                    emrtel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR_TEL3"].ToString().Trim();
                cell[102] = new PdfPCell(new iTextSharp.text.Phrase(emrtel, ChFont3));
                cell[103] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1聯絡e-mail", ChFont3));
                cell[103].GrayFill = 0.9f;
                cell[104] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_EMAIL"].ToString(), ChFont3));
                cell[104].Colspan = 2;

                cell[105] = new PdfPCell(new iTextSharp.text.Phrase("Cu-Talk", ChFont3));//緊急聯絡人1 CUTalk帳號
                cell[105].GrayFill = 0.9f;
                cell[106] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_CUTALK_MID"].ToString(), ChFont3));
                cell[106].Colspan = 2;

                cell[107] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人1地址", ChFont3));
                cell[107].GrayFill = 0.9f;
                cell[108] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR_ADD"].ToString(), ChFont3));
                cell[108].Colspan = 5;

                cell[109] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2", ChFont3));
                cell[109].GrayFill = 0.9f;
                cell[110] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_NAME"].ToString(), ChFont3));
                cell[111] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
                cell[111].GrayFill = 0.9f;
                cell[112] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_RELATE"].ToString(), ChFont3));
                cell[113] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2電話", ChFont3));
                cell[113].GrayFill = 0.9f;
                string emr2tel = dt_Basic_Record.Rows[0]["EMR1_TEL"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["EMR1_TEL2"].ToString().Trim() != "")
                    emr2tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR1_TEL2"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["EMR1_TEL3"].ToString().Trim() != "")
                    emr2tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR1_TEL3"].ToString().Trim();
                cell[114] = new PdfPCell(new iTextSharp.text.Phrase(emr2tel, ChFont3));
                cell[115] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2聯絡e-mail", ChFont3));
                cell[115].GrayFill = 0.9f;
                cell[116] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_EMAIL"].ToString(), ChFont3));
                cell[116].Colspan = 2;

                cell[117] = new PdfPCell(new iTextSharp.text.Phrase("Cu-Talk", ChFont3));//緊急聯絡人2 CUTalk帳號
                cell[117].GrayFill = 0.9f;
                cell[118] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_CUTALK_MID"].ToString(), ChFont3));
                cell[118].Colspan = 2;

                cell[119] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人2地址", ChFont3));
                cell[119].GrayFill = 0.9f;
                cell[120] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR1_ADD"].ToString(), ChFont3));
                cell[120].Colspan = 5;


                //緊急聯絡人3
                cell[121] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3", ChFont3));
                cell[121].GrayFill = 0.9f;
                cell[122] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_NAME"].ToString(), ChFont3));
                cell[123] = new PdfPCell(new iTextSharp.text.Phrase("關係", ChFont3));
                cell[123].GrayFill = 0.9f;
                cell[124] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_RELATE"].ToString(), ChFont3));
                cell[125] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3電話", ChFont3));
                cell[125].GrayFill = 0.9f;
                string emr3tel = dt_Basic_Record.Rows[0]["EMR2_TEL"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["EMR2_TEL2"].ToString().Trim() != "")
                    emr3tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR2_TEL2"].ToString().Trim();
                if (dt_Basic_Record.Rows[0]["EMR2_TEL3"].ToString().Trim() != "")
                    emr3tel += ", " + "\n" + dt_Basic_Record.Rows[0]["EMR2_TEL3"].ToString().Trim();
                cell[126] = new PdfPCell(new iTextSharp.text.Phrase(emr3tel, ChFont3));
                cell[127] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3聯絡e-mail", ChFont3));
                cell[127].GrayFill = 0.9f;
                cell[128] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_EMAIL"].ToString(), ChFont3));
                cell[128].Colspan = 2;

                cell[129] = new PdfPCell(new iTextSharp.text.Phrase("Cu-Talk", ChFont3));//緊急聯絡人3 CUTalk帳號
                cell[129].GrayFill = 0.9f;
                cell[130] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_CUTALK_MID"].ToString(), ChFont3));
                cell[130].Colspan = 2;

                cell[131] = new PdfPCell(new iTextSharp.text.Phrase("緊急聯絡人3地址", ChFont3));
                cell[131].GrayFill = 0.9f;
                cell[132] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["EMR2_ADD"].ToString(), ChFont3));
                cell[132].Colspan = 5;

                //繳款人
                cell[133] = new PdfPCell(new iTextSharp.text.Phrase("主要繳款人", ChFont3));
                cell[133].GrayFill = 0.9f;
                cell[134] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_NAME"].ToString(), ChFont3));
                cell[135] = new PdfPCell(new iTextSharp.text.Phrase("主要繳款人電話", ChFont3));
                cell[135].GrayFill = 0.9f;
                cell[136] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_TEL"].ToString(), ChFont3));
                cell[137] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));
                cell[138] = new PdfPCell(new iTextSharp.text.Phrase("", ChFont3));

                cell[139] = new PdfPCell(new iTextSharp.text.Phrase("主要繳款人聯絡e-mail", ChFont3));
                cell[139].GrayFill = 0.9f;
                cell[140] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_EMAIL"].ToString(), ChFont3));
                cell[140].Colspan = 2;

                cell[141] = new PdfPCell(new iTextSharp.text.Phrase("Cu-Talk", ChFont3));//主要繳款人 CUTalk帳號
                cell[141].GrayFill = 0.9f;
                cell[142] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_CUTALK_MID"].ToString(), ChFont3));
                cell[142].Colspan = 2;

                cell[143] = new PdfPCell(new iTextSharp.text.Phrase("繳款人現況評估", ChFont3));
                cell[143].GrayFill = 0.9f;
                cell[144] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["PAY_EVALUTE"].ToString(), ChFont3));
                cell[144].Colspan = 5;

                cell[145] = new PdfPCell(new iTextSharp.text.Phrase("住民優免身份", ChFont3));
                cell[145].GrayFill = 0.9f;
                cell[146] = new PdfPCell(new iTextSharp.text.Phrase(dt_Basic_Record.Rows[0]["IP_IDENTITY"].ToString(), ChFont3));

                for (int i = 0; i < cell.Length; i++)
                {
                    cell[i].Padding = 5f;
                    cell[i].VerticalAlignment = Element.ALIGN_MIDDLE;
                    if (i <= 56)
                    {
                        table.AddCell(cell[i]);
                    }
                    else if (i >= 145)
                    {
                        table_identity.AddCell(cell[i]);
                    }
                    else
                    {
                        table_family.AddCell(cell[i]);
                    }
                }

                doc.Add(table);
                Paragraph Title3 = new Paragraph("家庭基本資料", ChFont);
                doc.Add(Title3);
                doc.Add(table_family);
                Paragraph Title5 = new Paragraph("住民費用優免設定", ChFont);
                doc.Add(Title5);
                doc.Add(table_identity);

                string[][] habit = new string[][] { new string[] { "葷素習慣", "素食者"  , WebC_to_PS(chblvege)+ "\n"+
                                                                               "不吃食物: "+WebC_to_PS(chklvege1)+"  其他: "+txtvege.Text,
                                                                               "葷素者"  , "不吃食物: "+WebC_to_PS(chklvege)+"\n"+
                                                                               "其他: "+txtNvege.Text
                                                                 },
                                                    new string[] {"素食時節限定",WebC_to_PS(chklvegetime)},
                                                    new string[] {"主食偏好","早餐",txtfavorbreak.Text,
                                                                             "午餐",txtfavorlunch.Text,
                                                                             "晚餐",txtfavordinner.Text
                                                                 },
                                                    new string[] {"禁忌",txtforbid.Text},
                                                    new string[] {"特殊指示",txtspecorder.Text},
                                                    new string[] {"各餐之要求","早餐",txtmealbreak.Text,
                                                                               "點心",txtmealsnack.Text,
                                                                               "午餐",txtmeallunch.Text,
                                                                               "下午茶",txtmealtea.Text,
                                                                               "晚餐",txtmealdinner.Text,
                                                                               "宵夜",txtmealmnsnack.Text
                                                                 }
                                                    };

                for (int i = 0; i < habit.Length; i++)
                {
                    PdfPCell[] habit_cell = new PdfPCell[Convert.ToInt32(habit[i].Length)];
                    for (int j = 0; j < habit[i].Length; j++)
                    {
                        habit_cell[j] = new PdfPCell(new iTextSharp.text.Phrase(habit[i][j], ChFont3));
                        if (j == 0)
                        {
                            habit_cell[j].GrayFill = 0.9f;
                            if (i == 0)
                                habit_cell[j].Rowspan = 2; //葷素習慣
                            if (i == 2)
                                habit_cell[j].Rowspan = 3; //各餐偏好
                            if (i == 5)
                                habit_cell[j].Rowspan = 6; //各餐之要求
                            if (i == 1 || i == 3 || i == 4)
                                habit_cell[j].Colspan = 2;
                        }
                        if (j > 0 && j % 2 == 1)
                        {
                            if (i == 0 || i == 2 || i == 5)
                            {
                                habit_cell[j].GrayFill = 0.9f;
                            }
                        }

                        table_habit.AddCell(habit_cell[j]);
                    }
                }
                Paragraph Title6 = new Paragraph("住民膳食習慣", ChFont);
                doc.Add(Title6);
                doc.Add(table_habit);



                //文件關閉
                doc.Close();
                dt_Basic_Record.Dispose();
                //檔案下載*/
                Response.Clear();
                Response.AddHeader("Content-Disposition", "inline; filename=" + pdffilename);
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

        private static string transToItem_SEX(string value)
        {
            string item_text;
            switch (value)
            {
                case "1":
                    item_text = "男";
                    break;
                case "2":
                    item_text = "女";
                    break;
                default:
                    item_text = "";
                    break;
            }
            return item_text;
        }

        private static string transToItem_Languaqge(string L1, string L2, string L3, string L_EX, string connection_id)
        {
            string txt = "";
            StringBuilder sb_language = new StringBuilder();
            string strSQL = "SELECT * FROM CODE_IP_LANGUAGE";
            DataTable dt = sqlData.getDataTable(strSQL, connection_id);

            var result1 = from a in dt.AsEnumerable()
                          where a.Field<Int32>("LANGUAGE_ID").ToString() == L1
                          select a.Field<string>("LANGUAGE_STATE");

            var result2 = from a in dt.AsEnumerable()
                          where a.Field<Int32>("LANGUAGE_ID").ToString() == L2
                          select a.Field<string>("LANGUAGE_STATE");

            var result3 = from a in dt.AsEnumerable()
                          where a.Field<Int32>("LANGUAGE_ID").ToString() == L3
                          select a.Field<string>("LANGUAGE_STATE");
            dt.Dispose();
            if (result1.Count() > 0 && result1.ElementAt(0).ToString() != "")
            {
                sb_language.Append(result1.ElementAt(0) + "\n");
            }
            if (result2.Count() > 0 && result2.ElementAt(0).ToString() != "")
            {
                sb_language.Append(result2.ElementAt(0) + "\n");
            }
            if (result3.Count() > 0 && result3.ElementAt(0).ToString() != "")
            {
                sb_language.Append(result3.ElementAt(0));
            }

            if (L_EX != "")
            {
                sb_language.Append("\n" + L_EX);
                txt = sb_language.ToString();
            }
            else if (sb_language.Length > 0)
            {
                txt = sb_language.ToString().Substring(0, sb_language.Length - 1);
            }
            else
            {
                txt = "";
            }
            return txt;
        }

        private static string transToBARRIER(string value)
        {
            string item_text;
            switch (value)
            {
                case "0":
                    item_text = "○是 ●否";
                    break;
                case "1":
                    item_text = "●是 ○否";
                    break;
                default:
                    item_text = "○是 ○否";
                    break;
            }
            return item_text;
        }

        //繼續新增
        protected void Button10_Click(object sender, EventArgs e)
        {
            disableAddIPBasic.Style["display"] = "inline";
            btnAddIPBasic.Style["display"] = "none";
            //btnAddIPBasic.Enabled = true;
            disablePrint.Style["display"] = "inline";
            btnPrint.Style["display"] = "none";
            //btnPrint.Enabled = false;
            disableNext.Style["display"] = "inline";
            Button10.Style["display"] = "none";
            //Button10.Enabled = false;
            Panel6.Visible = false;
            Response.Redirect("IP_Basic_A.aspx");
        }

        //新增住民補助資料RadioButton
        protected void rbAllowance_CheckedChanged(object sender, EventArgs e)
        {
            disableButton4.Style["display"] = "none";
            Button4.Style["display"] = "inline";
            //Button4.Enabled = true;
            rbHandicap.Checked = false;
            disableButton6.Style["display"] = "inline";
            Button6.Style["display"] = "none";
            //Button6.Enabled = false;
        }

        //新增住民障礙手冊RadioButton
        protected void rbHandicap_CheckedChanged(object sender, EventArgs e)
        {
            rbAllowance.Checked = false;
            disableButton4.Style["display"] = "inline";
            Button4.Style["display"] = "none";
            //Button4.Enabled = false;
            disableButton6.Style["display"] = "none";
            Button6.Style["display"] = "inline";
            //Button6.Enabled = true;
        }

        //呼叫新增住民補助資料子視窗
        protected void Button4_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "window.open('../../IPBasic/IP_Allowance_AD.aspx?ipno=" + This_IP_NO.Text + "','新增住民補助資料','height=550,width=860,top=0,left=0,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no'); ", true);//另開新視窗(可以)
        }

        //呼叫新增住民障礙手冊子視窗
        protected void Button6_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "window.open('../../IPBasic/IP_HandicapBook_AD.aspx?ipno=" + This_IP_NO.Text + "&recno=0','新增住民障礙手冊','height=700,width=1000,top=0,left=0,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no'); ", true);//另開新視窗(可以)
        }

        //核對必填欄位是否已填寫
        private bool CheckRequiredFields(out string unFillItem)
        {
            StringBuilder sb_unFillItem = new StringBuilder();
            bool unFill = false;

            //住民編號
            if (TextBoxNO.Text == "")
            {
                sb_unFillItem.Append("住民編號, ");
                unFill = true;
            }

            //住民姓名
            if (txtIPName.Text == "")
            {
                sb_unFillItem.Append("住民姓名, ");
                unFill = true;
            }

            ////出生年月日
            //if (txtIPBirthday.Text == "")
            //{
            //    sb_unFillItem.Append("出生年月日, ");
            //    unFill = true;
            //}
            if (string.IsNullOrEmpty(tb_year.Text) || ddl_month.SelectedValue.Equals("-99") || string.IsNullOrEmpty(tb_day.Text))
            {
                sb_unFillItem.Append("出生年月日, ");
                unFill = true;
            }

            /*
            if (txtBirthday_M.Text == "")
            {
                sb_unFillItem.Append("出生月, ");
                unFill = true;
            }
            if (txtBirthday_D.Text == "")
            {
                sb_unFillItem.Append("出生日, ");
                unFill = true;
            }
            */

            if (sb_unFillItem.Length > 0)
            {
                //unFillItem = sb_unFillItem.ToString().Substring(0, sb_unFillItem.Length - 2);
                unFillItem = sb_unFillItem.ToString().Substring(0, sb_unFillItem.Length);
            }
            else
            {
                unFillItem = "";
            }
            return unFill;
        }

        //顯示行業細項
        protected void btnJobDetail_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel6, typeof(UpdatePanel), "Message", "window.open('../../IPBasic/JobDetail.aspx','行業細項','height=300,width=900,top=0,left=0,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no'); ", true);//另開新視窗(可以)
        }

        ////國別核選選單的事件處理-本國
        //protected void radTaiwan_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (radTaiwan.Checked == true)
        //    {
        //        pnlTaiwanadr.Style["display"] = "inline";
        //        pnlTaiwan.Style["display"] = "inline";
        //        txtIPID.Focus();
        //        pnlForeign.Style["display"] = "none";
        //        pnlForeignadr.Style["display"] = "none";
        //    }
        //    else
        //    {
        //        pnlTaiwanadr.Style["display"] = "none";
        //        pnlTaiwan.Style["display"] = "none";
        //        pnlForeign.Style["display"] = "inline";
        //        pnlForeignadr.Style["display"] = "inline";
        //        radForeign.Focus();
        //        radForeign.Style["display"] = "inline";
        //    }
        //}
        ////國別核選選單的事件處理-外國
        //protected void radForeign_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (radForeign.Checked == true)
        //    {
        //        pnlForeign.Style["display"] = "inline";
        //        pnlForeignadr.Style["display"] = "inline";
        //        pnlTaiwanadr.Style["display"] = "none";
        //        pnlTaiwan.Style["display"] = "none";
        //        txtForeign.Focus();
        //        rbSex.Enabled = true;
        //    }
        //    else
        //    {
        //        pnlForeign.Style["display"] = "none";
        //        pnlForeignadr.Style["display"] = "none";
        //        pnlTaiwanadr.Style["display"] = "inline";
        //        pnlTaiwan.Style["display"] = "inline";
        //        radTaiwan.Focus();
        //        radTaiwan.Style["display"] = "inline";
        //        rbSex.Enabled = false;
        //    }
        //}
        //國外簽證期限日期驗證
        protected void txtResidence_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strResidence = txtResidence.Text.Substring(0, 4) + txtResidence.Text.Substring(5, 2) + txtResidence.Text.Substring(8, 2);
                int vsdate = Convert.ToInt32(strResidence);
                string datewarning = DateTimeCheck.datevaild(strResidence);
                if (datewarning == "國外簽證期限日期格式錯誤")
                {
                    lblShowErr.Text = "國外簽證期限日期格式錯誤";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                }
            }
            catch
            {
                lblShowErr.Text = "國外簽證期限日期格式錯誤";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
            }
        }

        //住民生日
        protected void tb_day_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tb_year.Text) && !ddl_month.SelectedValue.Equals("-99") && !string.IsNullOrEmpty(tb_day.Text))
                {
                    int year = sqlTime.rocytoady(Convert.ToInt16(tb_year.Text));
                    string date = sqlTime.DateAddSlash(year.ToString() + ddl_month.SelectedValue.ToString().PadLeft(2, '0') + tb_day.Text.PadLeft(2, '0'));
                    DateTime.Parse(date);
                }

                string id = ((Control)sender).ID;
                if (id.Equals("tb_year"))
                {
                    ScriptManager1.SetFocus(ddl_month);
                }
                else if (id.Equals("ddl_month"))
                {
                    ScriptManager1.SetFocus(tb_day);
                }
                else
                {
                    ScriptManager1.SetFocus(txtIPID);
                }
            }
            catch (Exception)
            {
                lblShowErr.Text = "日期格式錯誤";
                ScriptManager.RegisterClientScriptBlock(UpdatePanelErrMsg, typeof(UpdatePanel), "Message", "runEffect1();", true);
                tb_day.Text = "";
            }

        }

        //For Print
        protected string WebC_to_PS(WebControl rb)
        {
            string bg = "";

            switch (rb.GetType().ToString())
            {
                case "System.Web.UI.WebControls.RadioButtonList":
                    foreach (System.Web.UI.WebControls.ListItem it in ((RadioButtonList)rb).Items)
                    {
                        if (it.Selected)
                            bg += "●";
                        else
                            bg += "○";
                        bg += it.Text;
                    };
                    break;
                case "System.Web.UI.WebControls.CheckBoxList":
                    foreach (System.Web.UI.WebControls.ListItem it in ((CheckBoxList)rb).Items)
                    {
                        if (it.Selected)
                            bg += "■";
                        else
                            bg += "□";
                        bg += it.Text;
                    };
                    break;
                case "System.Web.UI.WebControls.RadioButton":
                    if (((RadioButton)rb).Checked)
                        bg += "●";
                    else
                        bg += "○";
                    bg += ((RadioButton)rb).Text;
                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    if (((CheckBox)rb).Checked)
                        bg += "■";
                    else
                        bg += "□";
                    bg += ((CheckBox)rb).Text;
                    break;
                default:
                    break;
            }
            return bg;
        }

        protected void ibtnradAfter_AdmsPhrase_Click(object sender, ImageClickEventArgs e)
        {
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("追蹤疾病史"); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            chklp[0].RepeatLayout = RepeatLayout.Table;
            //將片語陣列內的值逐一放置CheckBoxList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = true;
            btnSENT_Sign.Visible = false;
            btnSENT_Re.Visible = false;
            btnSENT_Re0.Visible = false;
            btnSENT_Re1.Visible = false;
            btnSENT_Re2.Visible = false;
            btnSENT_habit.Visible = false;
            btnSENT_PAY.Visible = false;
            ScriptManager1.SetFocus(TextBox6);

            /*
            string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(txtSignRelate.Text, "D$KtjS*)ch$ej^o&%%$y"));
            coder = Mail_Check.encrypt1.EnCode(coder);
            string value = compare_item(txtSignRelate);
            //value = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            value = Server.UrlEncode(value); value = Server.UrlEncode(value);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "ShowSubWin('../Blockui.aspx?day=" + coder + "&val=" + value + "&s=1');", true);
            */
        }

        protected void ibtnPAY_Click(object sender, ImageClickEventArgs e)
        {
            connection_id = Session["H_id"].ToString(); //連結資料庫
            sqlPHRASE.connect(connection_id); //連結資料庫
            Phrase.sqlPHRASE.connect(connection_id); //連結資料庫
            string phrase_user = Session["account"].ToString();
            string pharsename = sqlPHRASE.getphrase("繳款人評估"); //篩選有關補助說明這類別的片語
            int selectvalue = 1;

            TabContainerPHRASE.Visible = true;
            sqlPHRASE.searchphrase(pharsename);
            string[,] phrase_item = sqlPHRASE.GetPhrase_item(); //篩選有關補助說明類別的片語至陣列中
            TabPanel tabid = new TabPanel();//建立Tab物件

            tabid.HeaderText = pharsename; // 指定片語類別名稱為Tab標籤
            chklp[0] = new CheckBoxList();//建立CheckBoxList物件
            chklp[0].RepeatColumns = 5; // 此CheckBoxList物件每一行有5個item
            chklp[0].RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal; // 此CheckBoxList物件的item排列方式是水平
            chklp[0].RepeatLayout = RepeatLayout.Table;
            //將片語陣列內的值逐一放置CheckBoxList物件的item內
            for (int j = 0; j < (Convert.ToInt32(phrase_item.Length) / 2); j++)
            {
                chklp[0].Items.Add(phrase_item[0, j]); //建立CheckBoxList物件的item與item顯示文字(此部分是用片語關鍵字來呈現…片語關鍵字是存在片語陣列的第一個行[index=0])
                chklp[0].Items[j].Value = phrase_item[1, j]; //CheckBoxList物件item 的值(此部分是用片語內容…片語內容是存在片語陣列的第二個行[index=1])
                chklp[0].Items[j].Attributes["title"] = phrase_item[1, j];//這是讓使用者移到這item時會顯示的hint(使用者看到關鍵字不見得知道其內容,因此這hint要顯示這關鍵字內的片語內容)
                chklp[0].DataBind();
            }
            tabid.Controls.Add(new LiteralControl("<" + "br" + ">"));
            tabid.Controls.Add(chklp[0]); //把CheckBoxList物件建立在Tab內
            TabContainerPHRASE.Controls.Add(tabid); //把Tab放置在Tab的控制容器內
            TabContainerPHRASE.ActiveTabIndex = selectvalue; //顯示預設的Tab(可能Tab的控制容器內友好幾個Tab…要選定哪一個Tab為預設要顯示的Tab)

            btnSENT_IPB.Visible = false;
            btnSENT_Sign.Visible = false;
            btnSENT_Re.Visible = false;
            btnSENT_Re0.Visible = false;
            btnSENT_Re1.Visible = false;
            btnSENT_Re2.Visible = false;
            btnSENT_habit.Visible = false;
            btnSENT_PAY.Visible = true;
            ScriptManager1.SetFocus(txtPatEvalute);

            /*
            string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(txtSignRelate.Text, "D$KtjS*)ch$ej^o&%%$y"));
            coder = Mail_Check.encrypt1.EnCode(coder);
            string value = compare_item(txtSignRelate);
            //value = HttpUtility.UrlEncode(value, System.Text.Encoding.UTF8);
            value = Server.UrlEncode(value); value = Server.UrlEncode(value);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", "ShowSubWin('../Blockui.aspx?day=" + coder + "&val=" + value + "&s=1');", true);
            */
        }
    }
}