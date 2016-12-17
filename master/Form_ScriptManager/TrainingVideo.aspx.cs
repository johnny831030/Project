using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace longtermcare
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        public string wmvurl = "";
        private static string datasource;

        protected void Page_Load(object sender, EventArgs e)
        {
            string language = Session["language"].ToString();
            string connection_id = Session["H_id"].ToString();
            string user = Session["account"].ToString();
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);

            SqlDataSource1.ConnectionString = datasource;
            SqlDataSource1.SelectCommand = "SELECT MM_NO, MM_NAME FROM MainMenu";
            SqlDataSource2.ConnectionString = datasource;
            SqlDataSource2.SelectCommand = "SELECT MM_NO, MM_NAME FROM MainMenu";
            SqlDataSource3.ConnectionString = datasource;

            if (IsPostBack != true)
            {
                DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), "-99"));
                DropDownList2.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), "-99"));
                DropDownList3.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), "-99"));
                DropDownList4.Items.Add(new System.Web.UI.WebControls.ListItem(Language.Language.translate("Choose", language), "-99"));
            }

        }
        
        //舊方法
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            Label3.Text = "影片名稱：";
            if (e.Item.Value.Length > 3)
            {
                 if (e.Item.Value.Substring(0, 4) == "http")
                 {
                     wmvurl = e.Item.Value;
                     //Label4.Text = wmvurl;

                     Panel1.Visible = true;
                     Label3.Text += e.Item.Text;
                 }

            }
            else if (e.Item.Value == "0")
            {
                Panel1.Visible = false;
                Response.Write("<script>alert('本功能暫無教學影片，敬請期待'); </script>");
            }
            else
            {
                Panel1.Visible = false;
                Response.Write("<script>alert('此為目錄，請選擇其下的功能'); </script>");
            }
        }
        
        //主選單
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //先清空DropDownList2所有項目
            DropDownList2.Items.Clear();
            if (DropDownList1.SelectedValue == "-99")
            {
                DropDownList2.Enabled = false;
                DropDownList3.Enabled = false;
                DropDownList4.Enabled = false;
            }
            else
            {
                SqlDataSource2.SelectCommand = "SELECT SFM_NO, SFM_NAME FROM SubFunctionMenu WHERE MM_NO = '" + DropDownList1.SelectedValue.ToString() + "'";
                SqlDataSource2.DataBind();
                GridView1.DataBind();
                DropDownList2.Items.Add(new ListItem("請選擇", "-99"));//DropDownList.Items.Add(new ListItem("text","value"))；
                for (int i = 0; i < Convert.ToInt32(GridView1.Rows.Count.ToString()); i++)
                {
                    DropDownList2.Items.Add(new ListItem(GridView1.Rows[i].Cells[1].Text, GridView1.Rows[i].Cells[0].Text));
                }
                DropDownList2.Enabled = true;

                /*
                int P_MasterPage = 0;
                ListItem li = new ListItem();
                DropDownList2.Items.Clear();
                DropDownList2.Items.Add(li);
                //8是最頂部的目錄 1是住民管理作業 2是住民照護管理
                if (DropDownList1.SelectedValue != "1" || DropDownList1.SelectedValue != "2" || DropDownList1.SelectedValue != "8")
                {
                    switch (DropDownList1.SelectedValue)
                    {
                        case "3":
                            P_MasterPage = 7;
                            break;
                        case "4":
                            P_MasterPage = 3;
                            break;
                        case "5":
                            P_MasterPage = 4;
                            break;
                        case "6":
                            P_MasterPage = 5;
                            break;
                        case "7":
                            P_MasterPage = 6;
                            break;
                        default:
                            P_MasterPage = Int32.Parse(DropDownList1.SelectedValue);
                            break;
                    }
                }
                SqlDataSource2.SelectCommand = "SELECT P_No, P_Name, P_URL, P_MasterPage, P_Order, P_Main, P_Main_No FROM EMP_CODE WHERE (P_MasterPage = '" + P_MasterPage + "') AND (P_Main = 'y') ORDER BY P_Order";
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add(li);
                SqlDataSource3.SelectCommand = "";
                Panel1.Visible = false;

                DropDownList4.Items.Clear();
                DropDownList2.Enabled = true;
                */
            }

        }
        //子選單
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            if (DropDownList2.SelectedValue == "-99")
            {
                DropDownList2.Enabled = false;
            }
            else
            {
                SqlDataSource3.SelectCommand = "SELECT F_NO, F_NAME FROM FunctionItem WHERE SFM_NO = '" + DropDownList2.SelectedValue.ToString() + "'";
                SqlDataSource3.DataBind();
                GridView2.DataBind();
                DropDownList3.Items.Add(new ListItem("請選擇", "-99"));
                for (int i = 0; i < Convert.ToInt32(GridView2.Rows.Count.ToString()); i++)
                {
                    DropDownList3.Items.Add(new ListItem(GridView2.Rows[i].Cells[1].Text, GridView2.Rows[i].Cells[0].Text));
                }
                DropDownList3.Enabled = true;
                

                /*
                ListItem li = new ListItem();
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add(li);
                SqlDataSource3.SelectCommand = "SELECT P_No, P_Name, P_URL, P_MasterPage, P_Order, P_Main, P_Main_No FROM EMP_CODE WHERE (P_Main_No = '" + DropDownList2.SelectedValue + "') ORDER BY P_Order";
                Panel1.Visible = false;
                DropDownList4.Items.Clear();
                DropDownList2.Enabled = true;
                */

            }

        }
        //功　能
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.Items.Clear();
            if (DropDownList3.SelectedValue == "-99")
            {
                DropDownList4.Enabled = false;
            }
            else
            {
                ListItem L0 = new ListItem();
                ListItem l1 = new ListItem();
                ListItem l2 = new ListItem();
                ListItem l3 = new ListItem();
                ListItem l4 = new ListItem();
                ListItem l5 = new ListItem();
                DropDownList4.Items.Clear();
                //DropDownList4.Items.Add(L0);
                DropDownList4.Items.Add(new ListItem("請選擇", "-99"));
                switch (DropDownList3.SelectedItem.Text.Trim())
                {
                    #region 住民管理作業
                    #region 住民資料管理
                    case "住民基本資料":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Basic_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Basic_MM.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    case "住民補助資料":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Allowance.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Allowance_MM.mp4");
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "區間查詢列印", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_Allowance_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        DropDownList4.Items.Add(l3);
                        break;
                    case "住民障礙手冊":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_HandicapBook.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_HandicapBook_M.mp4");
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "區間查詢列印", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/IP_HandicapBook_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        DropDownList4.Items.Add(l3);
                        break;
                    case "潛在住民資料管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/Potential_IP.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/Potential_IP_M.mp4");
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "區間查詢列印", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPBasic/Potential_IP_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        DropDownList4.Items.Add(l3);
                        break;
                    #endregion
                    #region 住民入出院管理
                    case "住民預約入住":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_Expect_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_Expect_MM.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    case "住民入住管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_In_MM.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    case "住民轉床管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_ChBed_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_ChBed_MM.mp4");
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "區間查詢列印", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_ChBed_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        DropDownList4.Items.Add(l3);
                        break;
                    case "住民退住管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Out_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Out_M.mp4");
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "區間查詢列印", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Out_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        DropDownList4.Items.Add(l3);
                        break;
                    case "住民一覽表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "清單", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/PeopleLook_S1.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "簡易列表+住民床位一覽+住民聯絡電話一覽", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/PeopleLook_S235.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    case "歷次入住退住查修":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/HistoryInOut.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "辦理住民請假":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Leave_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/IPMangement/IPInOut/IP_Leave_M.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    #endregion
                    #region 會辦事項 未完全
                    case "查/修會辦事項":
                        l1 = new ListItem("本頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "管理者會辦事項查詢":
                        l1 = new ListItem("本頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #endregion
                    #region 住民照護管理
                    #region 護理照護評估
                    case "巴氏量表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_ADL_A_P.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_ADL_M.mp4");
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "區間查詢列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_ADL_A_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        DropDownList4.Items.Add(l3);
                        break;
                    case "失智量表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_MMSE_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_MMSE_M.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    case "憂鬱量表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_GDS_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "憂鬱量表2":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/GDS_LI3_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/GDS_LI3_M.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    case "柯氏量表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Karnofsky_Scale_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "入院評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Admission_Assess_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "Gordon評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/GORDON/Admission_Assess_New_A.mp4");
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/GORDON/Admission_Assess_New_M.mp4");
                        DropDownList4.Items.Add(l1);
                        DropDownList4.Items.Add(l2);
                        break;
                    case "護理評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_ADMSA_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "TPR登錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_TPR_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "InOut登錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_IO_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "健康評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/HA_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "生活需要表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_LifeCareList_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "跌倒評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_HighRiskFall_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "簡易跌倒評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_HighRiskFall8_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "壓瘡評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PS_ASSEMENT_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "壓瘡監測表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PS_MONITOR_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "壓瘡換藥紀錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PS_DRESS_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "TPR-2":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_TPR2_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民約束登錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Constraint_A_Form_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民約束評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_Constraint_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "意外事件登錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_AccidentRecord_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "TAI分級評量表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_TAI_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "初步疼痛評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_Pain_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "疼痛狀態評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_Pain_State_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "職能治療評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_OTHerapy_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "物理治療評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_PhysicalTherapy_A_M");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "日常生活評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_BodyEvaluate_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "身體評估單":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/NC_BodyEvaluate_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民入住院狀態表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_STATE_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "簡易精神狀態檢查量表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/SPMSQ_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民診療紀錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_MEDICAL_RECORD_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "每日輸出/輸入紀錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/IP_DAY_IO_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "定期身體評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/REG_BodyEvaluate_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "皮膚危險因子":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/SKIN_DANGER_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "復健綜合評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NursingCare/PhysicalTherapy_sws_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 醫事藥事管理
                    case "檢驗報告":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/MedicalManagement/LabRecord/MM_LabRecord_A_M_Year.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "血糖紀錄單":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/MedicalManagement/IP_BLOOD_SUGAR/IP_BLOOD_SUGAR_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "生命徵象紀錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/MedicalManagement/VitalSigns/VitalSignsRecord_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "疫苗紀錄表":
                        l1 = new ListItem("本頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 感染監測評估表
                    case "呼吸道感染評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Breath_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "眼耳鼻口感染評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Eye_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "皮膚感染評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Skin_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "腸胃炎感染評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Stomach_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "系統性感染評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Systemic_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "泌尿道感染評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/Infection/NC_Infection_Urinary_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 住民轉介管理
                    case "住民轉介單":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/TransferList/IP_TransferList_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 表單批次輸入
                    case "住民每月體重輸入表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/BatchInput/IPWeight_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民每月壓瘡輸入表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/BatchInput/IP_Maklebust_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 營養評估 未完全
                    case "個案入住營養評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/IP_IN_NA_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "72小時營養篩檢表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/MM_72NA_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/MM_72NA_M.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    case "迷你營養評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/MM_MNA_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "營養評估":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/IP_NA_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "營養師紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/MM_Dietitian_Record_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民生化測量記錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/IP_NA_Biochemical_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除(含區間查詢)", "http://140.123.174.27:618/TrainingVideo/NursingRemind/NutritionAssess/IP_NA_Biochemical_M.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    case "住民營養照護紀錄表":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "營養師計畫":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "每日出餐表":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 社工管理
                    case "個案適應評估表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/SocialManagement/IPAdapt_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民個別活動紀錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingRemind/SocialManagement/NC_Activity_Record_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingRemind/SocialManagement/NC_Activity_Record_M_RQ.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    case "個案服務計畫表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/SocialManagement/IP_Service_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "社工輔導紀錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/SocialManagement/Rounsel_Record_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "社會工作個案紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/SocialManagement/Social_Work_A_M_RQ.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "團體活動紀錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingRemind/SocialManagement/IP_GAR_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #endregion
                    #region 護理計畫作業(護理計畫作業改為照護計畫作業)
                    #region 護理作業(護理作業改為照護作業)
                    case "照護計畫":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingPlan/NursingJobs/Ques_Sel.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingPlan/NursingJobs/Ques_Sel_M.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    case "護理紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingPlan/NursingJobs/NursingRecord_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingPlan/NursingJobs/NursingRecord_M.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    case "簡易護理紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingPlan/NursingJobs/NursingRecordSimple_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除+區間列印", "http://140.123.174.27:618/TrainingVideo/NursingPlan/NursingJobs/NursingRecordSimple_M.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    #endregion
                    #region 管理作業
                    case "照護計畫總表查詢":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/QuesReport_List.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "待評值目標查詢":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/Achieve_List.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "健康問題表列印":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/QuesReport.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "照護計畫列印":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/NP_Print.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "護理紀錄列印":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/NR_Print.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "交班紀錄查詢":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "雜項交班新增+查詢", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/SHIFT_EXCHANGE_RECORD_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "護理交班新增+查詢", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/SHIFT_EXCHANGE_RECORD_View1_A_M.mp4");
                        DropDownList4.Items.Add(l2);
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/NursingPlan/ManagementJobs/SHIFT_EXCHANGE_RECORD_View4.mp4");
                        DropDownList4.Items.Add(l3);
                        break;
                    #endregion
                    #region 病歷摘要
                    case "病歷摘要":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "查詢", "http://140.123.174.27:618/TrainingVideo/NursingPlan/MedicalSummary/Medical_Summary.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 照護會診單
                    case "住民照護會診單":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/NursingPlan/CareTransfer/CareTransfer_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/NursingPlan/CareTransfer/CareTransfer_M.mp4");
                        DropDownList4.Items.Add(l2);
                        l3 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/列印已結案照護會診單", "http://140.123.174.27:618/TrainingVideo/NursingPlan/CareTransfer/CareTransfer_RQ.mp4");
                        DropDownList4.Items.Add(l3);
                        break;
                    #endregion
                    #endregion
                    #region 住民醫事管理
                    #region 行程管理
                    case "行程設定":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/MMManagement/ScheduleManage/IP_Schedule_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "行程確認":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/MMManagement/ScheduleManage/IP_Schedule_C_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/MMManagement/ScheduleManage/IP_Schedule_C_M.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    case "住民返回":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/MMManagement/ScheduleManage/IP_Schedule_B_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 就醫登錄
                    case "就醫紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/MMManagement/MedicalSign/IP_Visit_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 領藥紀錄
                    case "查/修領藥紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/ReceiveRecord/IP_HMed_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "目前用藥檢核表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/MMManagement/ReceiveRecord/CheckMed.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 給藥紀錄
                    case "今日給藥紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/MMManagement/GiveRecord/GiveMed_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "查詢給藥紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/MMManagement/GiveRecord/GiveMed_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 統計報表
                    case "六項指標-月份明細":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/StatisticalReports/ARrecord.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "六項指標-月份":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/StatisticalReports/ARmonth.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "六項指標-年份":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/StatisticalReports/ARyear.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "六項指標-統計圖表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/StatisticalReports/IPchart_6.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "統計圖表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/StatisticalReports/IPchart.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民體重統計圖":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/StatisticalReports/IPWeight_Chart.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "用餐狀況討計圖表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/MMManagement/StatisticalReports/IP_LifeCareList_G.mp4");
                        DropDownList4.Items.Add(l1);
                        break;

                    #endregion
                    #endregion
                    #region 行政管理作業 未完全
                    #region 住民使用耗材管理
                    case "耗材使用紀錄":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/Administration/ConsumablesManage/CareProduct.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "耗材撥補住民":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/Administration/ConsumablesManage/CareItemToPeople.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "住民耗材查詢":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "(區間查詢)", "http://140.123.174.27:618/TrainingVideo/Administration/ConsumablesManage/CareItemSearch.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 住民轉帳管理
                    case "收費轉帳":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "應收帳款查詢":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "", "http://140.123.174.27:618/TrainingVideo/Administration/TransferManage/Bill_Report.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 住民款項管理
                    case "零用金管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢", "http://140.123.174.27:618/TrainingVideo/Administration/PaymentsManage/IP_Cash_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "預收款管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增+查詢/修改", "http://140.123.174.27:618/TrainingVideo/Administration/PaymentsManage/PRE_Cash_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 收費管理
                    case "住民繳費":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/Administration/BillManage/IP_Bill_Pay.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "繳費查詢":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/Administration/BillManage/IP_Bill_S.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 物品管理
                    case "物品管理單":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/Administration/ThingManage/IP_Thing.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region SOP文件管理
                    case "SOP文件上傳":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/Administration/SOP/UploadDocument.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "SOP文件下載":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/Administration/SOP/DownloadShareFile.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "SOP文件刪除":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/Administration/SOP/UploadDocument_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "SOP目錄管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/Administration/SOP/Document_Path.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #endregion
                    #region 基本資料管理
                    #region 基本檔設定
                    case "床位基本檔":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/Basic/Bed_Basic_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "閥值基本檔":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/Basic/ReportGoal_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "補助單位資料維護檔":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/Basic/IP_Allowance_Maintenance.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 耗材基本檔管理
                    case "耗材品項管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/CareItem/Care_Item_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "常見衛耗材管理":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/CareItem/Common_Item_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "耗材成本管理":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "連帶檔管理":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 藥品檔管理
                    case "藥品檔設定":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/Medicine/Medicine_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "醫院常見藥品檔":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/BasicManagement/Medicine/CommonMed_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/BasicManagement/Medicine/CommonMed_M.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    case "藥品交互作用":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/BasicManagement/Medicine/MED_INTERACTION_A.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 片語管理
                    case "片語管理表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/Phrase/Phrase_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 公告提醒管理
                    case "全院公告":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/EMPNEW/AllEMPNEW.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "提醒設定":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "-個人提醒", "http://140.123.174.27:618/TrainingVideo/BasicManagement/EMPNEW/PerEMPNEW1.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "-他人提醒", "http://140.123.174.27:618/TrainingVideo/BasicManagement/EMPNEW/PerEMPNEW2.mp4");
                        DropDownList4.Items.Add(l2);
                        break;
                    #endregion
                    #region 護理計畫項目管理(護理計畫項目管理改為照護計畫項目管理, 護理目標改為照護目標, 護理措施改為照護措施)
                    case "系統別項目":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/NursingPlan/System_Item.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "醫學診斷項目":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/NursingPlan/Diagnosis_Item.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "健康問題項目":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/NursingPlan/Ques_Item.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "醫學診斷與健康問題連結":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "定義性特徵項目":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "相關因素項目":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "照護目標項目":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "照護措施項目":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 條碼產生作業
                    case "條碼產生":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/BasicManagement/BarCode/BarCode_Produce.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #endregion
                    #region 權限管理作業
                    #region 員工基本檔
                    case "員工基本資料":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增", "http://140.123.174.27:618/TrainingVideo/FAuthority/EMPBasic/EMPBasic_A.mp4");
                        DropDownList4.Items.Add(l1);
                        l2 = new ListItem(DropDownList3.SelectedItem.Text + "查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/FAuthority/EMPBasic/EMPBasic_M.mp4");
                        DropDownList4.Items.Add(l2);
                        l3 = new ListItem("員工證照資料", "http://140.123.174.27:618/TrainingVideo/FAuthority/EMPBasic/EMPLicense_A.mp4");
                        DropDownList4.Items.Add(l3);
                        l4 = new ListItem("員工總表列印", "http://140.123.174.27:618/TrainingVideo/FAuthority/EMPBasic/EMPPrint.mp4");
                        DropDownList4.Items.Add(l4);
                        break;
                    case "訓練時數登錄表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text + "新增/查詢/修改/刪除", "http://140.123.174.27:618/TrainingVideo/FAuthority/EMPBasic/NC_NETHC_A_M.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "員工一覽表":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/FAuthority/EMPBasic/EMP_ALL.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 權限設定
                    case "人員權限設定":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/FAuthority/Authority/EMP_Authority_A.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "群組權限設定":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/FAuthority/Authority/Group_A.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 修改密碼
                    case "密碼變更":
                        l1 = new ListItem(DropDownList3.SelectedItem.Text, "http://140.123.174.27:618/TrainingVideo/FAuthority/Authority/EMP_ChPassword.mp4");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #region 員工排班與責任分配管理
                    case "班表規則設定":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "班表建立":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    case "員工責任分配":
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                    #endregion
                    #endregion
                    default:
                        l1 = new ListItem("此頁面尚未建立教學影片，敬請期待", "0");
                        DropDownList4.Items.Add(l1);
                        break;
                }
                Panel1.Visible = false;
                DropDownList4.Enabled = true;
            }

        }
        //子功能
        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            if (DropDownList4.SelectedValue == "0")
                Response.Write("<script>alert('此頁面暫無教學影片，敬請期待'); </script>");
            else
            {
                if (DropDownList4.SelectedIndex != 0)
                {
                    if (DropDownList4.SelectedValue != "")
                    {
                        Label3.Text = "影片名稱：";
                        wmvurl = DropDownList4.SelectedValue;
                        Panel1.Visible = true;
                        Label3.Text += DropDownList4.SelectedItem.Text;
                    }
                }
            }
        }

    }
}