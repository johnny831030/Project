using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using AjaxControlToolkit;

namespace longtermcare
{
    public partial class MMManagement : System.Web.UI.MasterPage
    {
        string url = HttpContext.Current.Request.CurrentExecutionFilePath;
        int init = 0;
        private static string datasource;
        private static string command;
        private static string connection_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            string language = "";
            if (Session["language"] == null)
            {
                Session["language"] = "ch";
                language = Session["language"].ToString();
            }
            else
            {
                language = Session["language"].ToString();
            }

            if (Session["H_id"] == null)
            {
                HttpCookie myH_id_Cookie = Request.Cookies["H_id"];
                Session["H_id"] = myH_id_Cookie.Values["H_id"];
                connection_id = myH_id_Cookie.Values["H_id"];
            }
            else
            {
                connection_id = Session["H_id"].ToString();
            }
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);    
            //SqlRoomType.ConnectionString = datasource;
            SqlDataSource1.ConnectionString = datasource;
            SqlDataSource2.ConnectionString = datasource;
            SqlDataSource3.ConnectionString = datasource;
            SqlMain.connect(connection_id);
            SqlMenu.connect(connection_id);
            TextBox1.Attributes.Add("autocomplete", "off");
            English.Text = Language.Language.translate("En_Button", language);
            Chinese.Text = Language.Language.translate("Ch_Button", language);
            Tailand.Text = Language.Language.translate("Thai", language);
            Vietnamese.Text = Language.Language.translate("Vietnamese", language);
/*
            HyperLink1.Text = Language.Language.translate("IpManagement", language);
            HyperLink2.Text = Language.Language.translate("IpNursing", language);
            HyperLink6.Text = Language.Language.translate("MMManagement", language);
            HyperLink3.Text = Language.Language.translate("Administration", language);
            HyperLink4.Text = Language.Language.translate("BasicManagement", language);
            HyperLink5.Text = Language.Language.translate("AuthorityManagement", language);
            HyperLink7.Text = Language.Language.translate("NursingPlan", language);
            HyperLink9.Text = Language.Language.translate("TrainingVideo", language);
            
            ipnoinput.Text = Language.Language.translate("IP_Search", language);
            ipnoinput1.Text = Language.Language.translate("IP_Search", language);
            Label11.Text = Language.Language.translate("CareType", language);
            LabelAreano.Text = Language.Language.translate("Areano", language);
            LabeIP_NO.Text = Language.Language.translate("IP_NO", language);
            LabeBedno.Text = Language.Language.translate("Bedno", language);
            LabelIP_Name.Text = Language.Language.translate("IP_Name", language);
            LabelIP_ID.Text = Language.Language.translate("IP_ID", language);
            Label146.Text = Language.Language.translate("Birthday", language);
            LabelAge.Text = Language.Language.translate("Age", language);
            LabelSex.Text = Language.Language.translate("Sex", language);
            LabelIn_D.Text = Language.Language.translate("Initail_In_Date", language);
            gvwIPlist.Columns[0].HeaderText = Language.Language.translate("Roomno", language);
            gvwIPlist.Columns[1].HeaderText = Language.Language.translate("Bedno", language);
            gvwIPlist.Columns[2].HeaderText = Language.Language.translate("Name", language);
            */
            string empname = Session["name"].ToString();
            string group = Session["group"].ToString();
            string degree = Session["degree"].ToString();
            SqlMain.position(degree);
            string posname = SqlMain.Getposname();

            emp_name.Text = "：" + empname;
            emp_position.Text = "：" + posname;
            txtipname.Text = "：" + Session["hp_sname"].ToString();
            lblBNO.Text = Session["BNO"].ToString();
            Page.Title = Session["hp_sname"].ToString();
            /*
            lblempname.Text = Language.Language.translate("Emp_Name", language);
            lblemppos.Text = Language.Language.translate("Emp_Title", language);
            ButtonLogout.Text = Language.Language.translate("Logout_Button", language);
            */
            if (!IsPostBack)
            {
                if (Session["lbltabindx2"] != null && Session["lbltabindx2"].ToString() == "1")
                {
                    LinkBtnTabView1.Font.Bold = false;
                    LinkBtnTabView2.Font.Bold = true;
                    multiTabs.ActiveViewIndex = 1;
                }
                else if (Session["lbltabindx2"] != null && Session["lbltabindx2"].ToString() == "0")
                {
                    LinkBtnTabView1.Font.Bold = true;
                    LinkBtnTabView2.Font.Bold = false;
                    multiTabs.ActiveViewIndex = 0;
                }
                else
                {
                    Session["lbltabindx2"] = "0";
                    LinkBtnTabView1.Font.Bold = true;
                    LinkBtnTabView2.Font.Bold = false;
                    multiTabs.ActiveViewIndex = 0;
                }
                //Session.Contents.Remove("lbltabindx2");
                string master = "3";
                makemenu(group, master, language);
            }
            if (ipno.Text == "")
            {
                ipno.Text = "";
                ipnonew.Text = "";
                iproom.Text = "";
                ipid.Text = "";
                ipidvisible.Text = "";
                ipname.Text = "";
                ipbirth.Text = "";
                ipage.Text = "";
                ipsex.Text = "";
                ipfir.Text = "";
            }

            if (Session["ipno"] != null)
            {
                ifhaveipno();
                IBtn_Inventory.Visible = true;
            }
            if (!IsPostBack)
            {
                //if (Session["RoomType"] != null)
                //{
                    //dropRoomtype.SelectedValue = Session["RoomType"].ToString();
                //}
                if (Session["Floor"] != null)
                {
                    dropArea.SelectedValue = Session["Floor"].ToString();
                }
                SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                gvwOutIPList.DataBind();
            }
            //TextBox1.Focus();
        }
      
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            string pass, group;
            pass = Session["account"].ToString();
            group = Session["group"].ToString();
            Session.Remove("account");
            Session.Remove("group");
            Session["account"] = pass;
            Session["group"] = group;
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Write("<script>window.location.href='../../H_P_Login.aspx'</script>");
            SqlMain.logout();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("Floor");
            Session["Floor"] = dropArea.SelectedValue;
            command = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION FROM ROOM INNER JOIN IP_INFORMATION ON ROOM.IP_NO = IP_INFORMATION.IP_NO WHERE (ROOM.ROOM_AREA = @ROOM_AREA) AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
            
            SqlDataSource2.SelectParameters.Clear();
            if (TextBox1.Text.Trim() == "")
            {
                SqlDataSource2.SelectParameters.Add("ROOM_AREA", dropArea.SelectedValue);
                //SqlDataSource2.SelectParameters.Add("ROOM_TYPE", dropRoomtype.SelectedValue);
                SqlDataSource2.SelectCommand = command;
                gvwIPlist.DataBind();
                gvwIPlist.SelectedIndex = -1;
            }
        }
        
        //製作Menu選單上項目
        public void makemenu(string group, string master, string language)
        {

            var main_tuple = SqlMenu.getpno(group, master);


            string[] main = main_tuple.Item2;
            string[] mainno = main_tuple.Item1;
            string menuname = "";
            string menuitem = "";
            int maincount = SqlMenu.getmenucount(group, master);
            int childcountnew = SqlMenu.getmenuitemcount(group, master);

            string[] child = new string[childcountnew];
            string[] url = new string[childcountnew];
            int counter_child = 0;

            for (int i = 0; i < maincount; i++)
            {
                string getno = Convert.ToString(mainno[i]);
                var child_tuple = SqlMenu.getmenuitemname(group, getno, master, counter_child, child, url);
                counter_child = child_tuple.Item1;
                child = child_tuple.Item2;
                url = child_tuple.Item3;
            }

            for (int i = 0; i < maincount; i++)
            {
                menuname = menuname + main[i] + " ";
            }
            for (int i = 0; i < childcountnew; i++)
            {
                menuitem = menuitem + child[i] + " ";
            }

            MenuItem[] mi = new MenuItem[maincount];

            for (int i = 0; i < maincount; i++)
            {
                mi[i] = new MenuItem();
                //string translatemain = Language.Language.translatechinese(main[i].ToString(), language);
                string translatemain = main[i].ToString();
                if (translatemain != "")
                {
                    string redesign = "";
                    char[] delimiterChars = { ' ' };
                    string[] sArray = translatemain.Split(delimiterChars);

                    int length = 0;
                    for (int count = 0; count < sArray.Length; count++)
                    {
                        length += sArray[count].Length;
                        if (length <= 20)
                        {
                            if (count == 0)
                                redesign = sArray[count];
                            else
                                redesign += " " + sArray[count];
                        }
                        length += 1;
                    }

                    if (translatemain.Length >= 20)
                    {
                        if (redesign.Trim() != "")
                        {
                            mi[i].Text = redesign;
                            mi[i].Selectable = false;
                        }
                        else
                        {
                            for (int count = 0; count < 20; count++)
                                redesign += translatemain[count].ToString();
                            mi[i].Text = redesign;
                            mi[i].Selectable = false;
                        }
                    }
                    else
                    {
                        mi[i].Text = translatemain;
                        mi[i].Selectable = false;
                    }
                    mi[i].ToolTip = translatemain;
                }
                else
                {
                    mi[i].Text = main[i].ToString();
                    mi[i].Selectable = false;
                    mi[i].ToolTip = main[i].ToString();
                }
                int childcount = Array.IndexOf(child, "/");

                MenuItem[] inner = new MenuItem[childcount + 1];
                for (int j = init; j < childcount; j++)
                {
                    inner[j] = new MenuItem();
                    //string translateitem = Language.Language.translatechinese(child[j].ToString(), language);
                    string translateitem = child[j].ToString();
                    if (translateitem != "")
                    {
                        string redesign = "";
                        char[] delimiterChars = { ' ' };
                        string[] sArray = translateitem.Split(delimiterChars);

                        int length = 0;
                        for (int count = 0; count < sArray.Length; count++)
                        {
                            length += sArray[count].Length;
                            if (length <= 20)
                            {
                                if (count == 0)
                                    redesign = sArray[count];
                                else
                                    redesign += " " + sArray[count];
                            }
                            length += 1;
                        }

                        if (translateitem.Length >= 20)
                        {
                            if (redesign.Trim() != "")
                                inner[j].Text = redesign;
                            else
                            {
                                for (int count = 0; count < 20; count++)
                                    redesign += translateitem[count].ToString();
                                inner[j].Text = redesign;
                            }
                        }
                        else
                        {
                            inner[j].Text = translateitem;
                        }
                        inner[j].ToolTip = translateitem;
                    }
                    else
                    {
                        inner[j].Text = child[j];
                        inner[j].ToolTip = child[j];
                    }
                    inner[j].NavigateUrl = url[j];
                    mi[i].ChildItems.Add(inner[j]);
                }
                init = childcount + 1;
                child[childcount] = "";
                url[childcount] = "";
                this.Menu1.Items.Add(mi[i]);
            }
        }
        protected void English_Click(object sender, EventArgs e)
        {
            Session["language"] = "en";
            Response.Redirect("~" + url);
        }

        protected void Chinese_Click(object sender, EventArgs e)
        {
            Session["language"] = "ch";
            Response.Redirect("~" + url);
        }
        protected void Tailand_Click(object sender, EventArgs e)
        {
            Session["language"] = "thai";
            Response.Redirect("~" + url);
        }

        protected void Vietnamese_Click(object sender, EventArgs e)
        {
            Session["language"] = "vie";
            Response.Redirect("~" + url);
        }
        protected void dropRoomtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Session.Remove("RoomType");
            //Session["RoomType"] = dropRoomtype.SelectedValue;
            command = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION FROM ROOM INNER JOIN IP_INFORMATION ON ROOM.IP_NO = IP_INFORMATION.IP_NO WHERE (ROOM.ROOM_AREA = @ROOM_AREA) AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
            
            SqlDataSource2.SelectParameters.Clear();
            if (TextBox1.Text.Trim() == "")
            {
                SqlDataSource2.SelectParameters.Add("ROOM_AREA", dropArea.SelectedValue);
                //SqlDataSource2.SelectParameters.Add("ROOM_TYPE", dropRoomtype.SelectedValue);
                SqlDataSource2.SelectCommand = command;
                gvwIPlist.DataBind();
                gvwIPlist.SelectedIndex = -1;
            }
        }

        protected void SChinese_Click(object sender, EventArgs e)
        {
            Session["language"] = "sch";
            Response.Redirect("~" + url);
        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string newipno = SqlMain.IDselect(TextBox1.Text.Trim());
            if (SqlMain.CheckIpno(newipno) == 1)
            {
                Session["ipno"] = newipno.Trim();
                ifhaveipno();
                dropdownlist();
                SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM ROOM INNER JOIN IP_INFORMATION ON ROOM.IP_NO = IP_INFORMATION.IP_NO WHERE (IP_INFORMATION.IP_NO = '" + newipno.Trim() + "') AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                gvwIPlist.DataBind();
                gvwIPlist.SelectedIndex = -1;
                TextBox1.Text = "";
                Response.Redirect("~" + url);
            }
            else
            {
                command = SqlMain.LikeNameSearch(TextBox1.Text.Trim());
                int count = SqlMain.GetCount();
                if (command != "" && count != 0)
                {
                    if (count > 1)
                    {
                        SqlDataSource2.SelectParameters.Clear();
                        SqlDataSource2.SelectCommand = command;
                        gvwIPlist.DataBind();
                        dropdownlist();
                    }
                    else if (count == 1)
                    {
                        Session["ipno"] = command.Trim();
                        ifhaveipno();
                        dropdownlist();
                        SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM ROOM INNER JOIN IP_INFORMATION ON ROOM.IP_NO = IP_INFORMATION.IP_NO WHERE (IP_INFORMATION.IP_NO = '" + command.Trim() + "') AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                        gvwIPlist.DataBind();
                        Response.Redirect("~" + url);
                    }
                    gvwIPlist.SelectedIndex = -1;
                }
                else
                {
                    string id = SqlMain.SearchPatientID(TextBox1.Text.Trim());
                    if (SqlMain.CheckIpno(id) == 1)
                    {
                        Session["ipno"] = id.Trim();
                        ifhaveipno();
                        dropdownlist();
                        SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM ROOM INNER JOIN IP_INFORMATION ON ROOM.IP_NO = IP_INFORMATION.IP_NO WHERE (IP_INFORMATION.IP_NO = '" + id.Trim() + "') AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                        gvwIPlist.DataBind();
                        gvwIPlist.SelectedIndex = -1;
                        Response.Redirect("~" + url);
                    }
                    else
                    {
                        string redirect = url;
                        Response.Write("<script>alert('查詢不到此住民或此住民無入住'); location.href='" + redirect + "'; </script>");
                    }
                }
                TextBox1.Text = "";
                multiTabs.ActiveViewIndex = 0;
                LinkBtnTabView1.Font.Bold = true;
                LinkBtnTabView2.Font.Bold = false;
                Session["lbltabindx2"] = "0";
            }
        }
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            DataTable dt_newipno = SqlMain.getdtIDselect(TextBox2.Text.Trim(), Session["H_id"].ToString());
            if (dt_newipno.Rows.Count >= 1)//用住民編號查詢有資料
            {
                //查詢此編號住民是否有退住紀錄
                DataTable dt_outipbynewipno = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                if (dt_outipbynewipno.Rows.Count == 1)
                {
                    Session["ipno"] = dt_outipbynewipno.Rows[0]["IP_NO"].ToString();
                    ifhaveipno();
                    //dropdownlist();
                    lblOutIPListInfo.Text = "";
                    SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                    gvwOutIPList.DataBind();
                    gvwOutIPList.SelectedIndex = -1;
                    TextBox2.Text = "";
                    Session["lbltabindx2"] = "1";
                    //Response.Redirect("~" + url);
                    IBtn_Inventory.Visible = true;
                }
                else if (dt_outipbynewipno.Rows.Count > 1)
                {
                    //dropdownlist();
                    lblOutIPListInfo.Text = "";
                    SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                    gvwOutIPList.DataBind();
                    gvwOutIPList.SelectedIndex = -1;
                    TextBox2.Text = "";
                    Session["lbltabindx2"] = "1";
                    //Response.Redirect("~" + url);
                    IBtn_Inventory.Visible = true;
                }
                else
                {
                    Session["lbltabindx2"] = "1";
                    string redirect = url;
                    Response.Write("<script>alert('此住民尚無退住紀錄'); location.href='" + redirect + "'; </script>");
                }
                dt_outipbynewipno.Dispose();
            }
            else//無此編號的住民(可能用其他資料[身分證或姓名]輸入查詢)
            {
                DataTable dt_ipname = SqlMain.getdtLikeNameSearch(TextBox2.Text.Trim(), Session["H_id"].ToString());
                if (dt_ipname.Rows.Count >= 1)//用住民姓名查詢有資料
                {
                    //查詢此姓名住民是否有退住紀錄
                    DataTable dt_outipbyipname = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NAME = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                    if (dt_outipbyipname.Rows.Count == 1)
                    {
                        Session["ipno"] = dt_outipbyipname.Rows[0]["IP_NO"].ToString();
                        ifhaveipno();
                        //dropdownlist();
                        lblOutIPListInfo.Text = "";
                        SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NAME = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                        gvwOutIPList.DataBind();
                        gvwOutIPList.SelectedIndex = -1;
                        TextBox2.Text = "";
                        Session["lbltabindx2"] = "1";
                        //Response.Redirect("~" + url);
                        IBtn_Inventory.Visible = true;
                    }
                    else if (dt_outipbyipname.Rows.Count > 1)
                    {
                        //dropdownlist();
                        lblOutIPListInfo.Text = "";
                        SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NAME = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                        gvwOutIPList.DataBind();
                        gvwOutIPList.SelectedIndex = -1;
                        TextBox2.Text = "";
                        Session["lbltabindx2"] = "1";
                        //Response.Redirect("~" + url);
                        IBtn_Inventory.Visible = true;
                    }
                    else
                    {
                        Session["lbltabindx2"] = "1";
                        string redirect = url;
                        Response.Write("<script>alert('此住民尚無退住紀錄'); location.href='" + redirect + "'; </script>");
                    }
                }
                else
                {
                    DataTable dt_ipid = SqlMain.getdtSearchPatientID(TextBox2.Text.Trim(), Session["H_id"].ToString());
                    if (dt_ipid.Rows.Count >= 1)//用住民身分證查詢有資料
                    {
                        //詢此身分證住民是否有退住紀錄
                        DataTable dt_outipbyipid = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                        if (dt_outipbyipid.Rows.Count == 1)
                        {
                            Session["ipno"] = dt_outipbyipid.Rows[0]["IP_NO"].ToString();
                            ifhaveipno();
                            //dropdownlist();
                            lblOutIPListInfo.Text = "";
                            SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                            gvwOutIPList.DataBind();
                            gvwOutIPList.SelectedIndex = -1;
                            TextBox2.Text = "";
                            Session["lbltabindx2"] = "1";
                            //Response.Redirect("~" + url);
                            IBtn_Inventory.Visible = true;
                        }
                        else if (dt_outipbyipid.Rows.Count > 1)
                        {
                            //dropdownlist();
                            lblOutIPListInfo.Text = "";
                            SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID = '" + TextBox2.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                            gvwOutIPList.DataBind();
                            gvwOutIPList.SelectedIndex = -1;
                            TextBox2.Text = "";
                            Session["lbltabindx2"] = "1";
                            //Response.Redirect("~" + url);
                            IBtn_Inventory.Visible = true;
                        }
                        else
                        {
                            Session["lbltabindx2"] = "1";
                            string redirect = url;
                            Response.Write("<script>alert('此住民尚無退住紀錄'); location.href='" + redirect + "'; </script>");
                        }
                    }
                    else
                    {
                        Session["lbltabindx2"] = "1";
                        string redirect = url;
                        Response.Write("<script>alert('查詢不到此住民或此住民無退住紀錄'); location.href='" + redirect + "'; </script>");
                    }
                    dt_ipid.Dispose();
                }
                dt_ipname.Dispose();
            }
            dt_newipno.Dispose();

            #region 參考寫法
            /*
            DataTable dt_newipno = SqlMain.getdtIDselect(TextBox1.Text.Trim(), Session["H_id"].ToString());
            if (dt_newipno.Rows.Count >= 1)//用住民編號查詢有資料
            {
                //先查詢此編號住民是否目前是入住的住民
                DataTable dt_inipbynewipno = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC", Session["H_id"].ToString());
                if (dt_inipbynewipno.Rows.Count == 0)//目前未入住
                {
                    //再查詢此編號住民是否有退住紀錄
                    DataTable dt_outipbynewipno = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                    if(dt_outipbynewipno.Rows.Count == 1)
                    {
                        Session["ipno"] = dt_outipbynewipno.Rows[0]["IP_NO"].ToString();
                        ifhaveipno();
                        dropdownlist();
                        //Response.Redirect("~" + url);
                        lblIPListInfo.Text = "此查詢資料之住民目前非入住中";
                        SqlDataSource2.SelectCommand = "";
                        gvwIPlist.DataBind();
                        gvwIPlist.SelectedIndex = -1;
                        lblOutIPListInfo.Text = "";
                        SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                        gvwOutIPList.DataBind();
                        gvwOutIPList.SelectedIndex = -1;
                        TextBox1.Text = "";
                        TabIPList.ActiveTabIndex = 1;
                        IBtn_Inventory.Visible = true;
                    }
                    else if(dt_outipbynewipno.Rows.Count > 1)
                    {
                        dropdownlist();
                        //Response.Redirect("~" + url);
                        lblIPListInfo.Text = "此查詢資料之住民目前非入住中";
                        SqlDataSource2.SelectCommand = "";
                        gvwIPlist.DataBind();
                        gvwIPlist.SelectedIndex = -1;
                        lblOutIPListInfo.Text = "";
                        SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                        gvwOutIPList.DataBind();
                        gvwOutIPList.SelectedIndex = -1;
                        TextBox1.Text = "";
                        TabIPList.ActiveTabIndex = 1;
                        IBtn_Inventory.Visible = true;
                    }
                    else//==0:都無入住退住資訊, 但住民資料有紀錄[應該是先輸入尚未辦入住]
                    {
                        string redirect = url;
                        Response.Write("<script>alert('此住民尚未辦過入住'); location.href='" + redirect + "'; </script>");
                    }
                    dt_outipbynewipno.Dispose();
                }
                else//==1：目前是入住
                {
                    Session["ipno"] = dt_inipbynewipno.Rows[0]["IP_NO"].ToString();
                    ifhaveipno();
                    //再查詢此編號住民是否有退住紀錄
                    DataTable dt_outipbynewipno = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                    if (dt_outipbynewipno.Rows.Count >= 1)
                    {
                        dropdownlist();
                        //Response.Redirect("~" + url);
                        lblIPListInfo.Text = "";
                        SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                        gvwIPlist.DataBind();
                        gvwIPlist.SelectedIndex = -1;
                        lblOutIPListInfo.Text = "";
                        SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                        gvwOutIPList.DataBind();
                        gvwOutIPList.SelectedIndex = -1;
                        TextBox1.Text = "";
                        TabIPList.ActiveTabIndex = 0;
                        IBtn_Inventory.Visible = true;
                    }
                    else//==0:此住民尚未退住過
                    {
                        dropdownlist();
                        //Response.Redirect("~" + url);
                        lblIPListInfo.Text = "";
                        SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                        gvwIPlist.DataBind();
                        gvwIPlist.SelectedIndex = -1;
                        lblOutIPListInfo.Text = "此查詢資料之住民尚未退住過";
                        SqlDataSource3.SelectCommand = "";
                        gvwOutIPList.DataBind();
                        gvwOutIPList.SelectedIndex = -1;
                        TextBox1.Text = "";
                        TabIPList.ActiveTabIndex = 0;
                        IBtn_Inventory.Visible = true;
                    }
                    dt_outipbynewipno.Dispose();
                }
                dt_inipbynewipno.Dispose();
            }
            else//無此編號的住民(可能用其他資料[身分證或姓名]輸入查詢)
            {
                DataTable dt_ipname = SqlMain.getdtLikeNameSearch(TextBox1.Text.Trim(), Session["H_id"].ToString());
                if (dt_ipname.Rows.Count >= 1)//用住民姓名查詢有資料
                {
                    //先查詢此姓名住民是否目前是入住的住民
                    DataTable dt_inipbyipname = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_NAME = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC", Session["H_id"].ToString());
                    if (dt_inipbyipname.Rows.Count == 0)//目前未入住
                    {
                        //再查詢此姓名住民是否有退住紀錄
                        DataTable dt_outipbyipname = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NAME = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                        if (dt_outipbyipname.Rows.Count == 1)
                        {
                            Session["ipno"] = dt_outipbyipname.Rows[0]["IP_NO"].ToString();
                            ifhaveipno();
                            dropdownlist();
                            //Response.Redirect("~" + url);
                            lblIPListInfo.Text = "此查詢資料之住民目前非入住中";
                            SqlDataSource2.SelectCommand = "";
                            gvwIPlist.DataBind();
                            gvwIPlist.SelectedIndex = -1;
                            lblOutIPListInfo.Text = "";
                            SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NO_NEW = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                            gvwOutIPList.DataBind();
                            gvwOutIPList.SelectedIndex = -1;
                            TextBox1.Text = "";
                            TabIPList.ActiveTabIndex = 1;
                            IBtn_Inventory.Visible = true;
                        }
                        else if (dt_outipbyipname.Rows.Count > 1)
                        {
                            dropdownlist();
                            //Response.Redirect("~" + url);
                            lblIPListInfo.Text = "此查詢資料之住民目前非入住中";
                            SqlDataSource2.SelectCommand = "";
                            gvwIPlist.DataBind();
                            gvwIPlist.SelectedIndex = -1;
                            lblOutIPListInfo.Text = "";
                            SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NAME LIKE '%" + TextBox1.Text.Trim() + "%') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                            gvwOutIPList.DataBind();
                            gvwOutIPList.SelectedIndex = -1;
                            TextBox1.Text = "";
                            TabIPList.ActiveTabIndex = 1;
                            IBtn_Inventory.Visible = true;
                        }
                        else//==0:都無入住退住資訊, 但住民資料有紀錄[應該是先輸入尚未辦入住]
                        {
                            string redirect = url;
                            Response.Write("<script>alert('此住民尚未辦過入住'); location.href='" + redirect + "'; </script>");
                        }
                        dt_outipbyipname.Dispose();
                    }
                    else//==1：目前是入住
                    {
                        Session["ipno"] = dt_inipbyipname.Rows[0]["IP_NO"].ToString();
                        ifhaveipno();
                        //再查詢此姓名住民是否有退住紀錄
                        DataTable dt_outipbyipname = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NAME = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                        if (dt_outipbyipname.Rows.Count >= 1)
                        {
                            dropdownlist();
                            //Response.Redirect("~" + url);
                            lblIPListInfo.Text = "";
                            SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_NAME = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                            gvwIPlist.DataBind();
                            gvwIPlist.SelectedIndex = -1;
                            lblOutIPListInfo.Text = "";
                            SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_NAME = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                            gvwOutIPList.DataBind();
                            gvwOutIPList.SelectedIndex = -1;
                            TextBox1.Text = "";
                            TabIPList.ActiveTabIndex = 0;
                            IBtn_Inventory.Visible = true;
                        }
                        else//==0:此住民尚未退住過
                        {
                            dropdownlist();
                            //Response.Redirect("~" + url);
                            lblIPListInfo.Text = "";
                            SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_NAME = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                            gvwIPlist.DataBind();
                            gvwIPlist.SelectedIndex = -1;
                            lblOutIPListInfo.Text = "此查詢資料之住民尚未退住過";
                            SqlDataSource3.SelectCommand = "";
                            gvwOutIPList.DataBind();
                            gvwOutIPList.SelectedIndex = -1;
                            TextBox1.Text = "";
                            TabIPList.ActiveTabIndex = 0;
                            IBtn_Inventory.Visible = true;
                        }
                        dt_outipbyipname.Dispose();
                    }
                    dt_inipbyipname.Dispose();
                }
                else//無此姓名的住民(可能用其他資料[身分證]輸入查詢)
                {
                    DataTable dt_ipid = SqlMain.getdtSearchPatientID(TextBox1.Text.Trim(), Session["H_id"].ToString());
                    if (dt_ipid.Rows.Count >= 1)//用住民身分證查詢有資料
                    {
                        //先查詢此身分證住民是否目前是入住的住民
                        DataTable dt_inipbyipid = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_ID = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC", Session["H_id"].ToString());
                        if (dt_inipbyipid.Rows.Count == 0)//目前未入住
                        {
                            //再查詢此身分證住民是否有退住紀錄
                            DataTable dt_outipbyipid = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                            if (dt_outipbyipid.Rows.Count == 1)
                            {
                                Session["ipno"] = dt_outipbyipid.Rows[0]["IP_NO"].ToString();
                                ifhaveipno();
                                dropdownlist();
                                //Response.Redirect("~" + url);
                                lblIPListInfo.Text = "此查詢資料之住民目前非入住中";
                                SqlDataSource2.SelectCommand = "";
                                gvwIPlist.DataBind();
                                gvwIPlist.SelectedIndex = -1;
                                lblOutIPListInfo.Text = "";
                                SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                                gvwOutIPList.DataBind();
                                gvwOutIPList.SelectedIndex = -1;
                                TextBox1.Text = "";
                                TabIPList.ActiveTabIndex = 1;
                                IBtn_Inventory.Visible = true;
                            }
                            else if (dt_outipbyipid.Rows.Count > 1)
                            {
                                dropdownlist();
                                //Response.Redirect("~" + url);
                                lblIPListInfo.Text = "此查詢資料之住民目前非入住中";
                                SqlDataSource2.SelectCommand = "";
                                gvwIPlist.DataBind();
                                gvwIPlist.SelectedIndex = -1;
                                lblOutIPListInfo.Text = "";
                                SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID ='" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                                gvwOutIPList.DataBind();
                                gvwOutIPList.SelectedIndex = -1;
                                TextBox1.Text = "";
                                TabIPList.ActiveTabIndex = 1;
                                IBtn_Inventory.Visible = true;
                            }
                            else//==0:都無入住退住資訊, 但住民資料有紀錄[應該是先輸入尚未辦入住]
                            {
                                string redirect = url;
                                Response.Write("<script>alert('此住民尚未辦過入住'); location.href='" + redirect + "'; </script>");
                            }
                            dt_inipbyipid.Dispose();
                        }
                        else//==1：目前是入住
                        {
                            Session["ipno"] = dt_inipbyipid.Rows[0]["IP_NO"].ToString();
                            ifhaveipno();
                            //再查詢此身分證住民是否有退住紀錄
                            DataTable dt_outipbyipid = sqlData.getDataTable("SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC", Session["H_id"].ToString());
                            if (dt_outipbyipid.Rows.Count >= 1)
                            {
                                dropdownlist();
                                //Response.Redirect("~" + url);
                                lblIPListInfo.Text = "";
                                SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_ID = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                                gvwIPlist.DataBind();
                                gvwIPlist.SelectedIndex = -1;
                                lblIPListInfo.Text = "";
                                SqlDataSource3.SelectCommand = "SELECT IP_INFORMATION.IP_NO AS IP_NO, IP_INFORMATION.IP_NAME AS IP_NAME, (IN_RECORD.IN_DATE+'|'+IN_RECORD.OUT_DATE) AS INDATE_RANGE, IN_RECORD.NO AS NO FROM IP_INFORMATION INNER JOIN IN_RECORD ON IP_INFORMATION.IP_NO = IN_RECORD.IP_NO WHERE (IN_RECORD.OUT_DATE IS NOT NULL) AND (IP_INFORMATION.IP_ID = '" + TextBox1.Text.Trim() + "') ORDER BY IP_INFORMATION.IP_NAME ASC, IP_INFORMATION.IP_NO ASC";
                                gvwOutIPList.DataBind();
                                gvwOutIPList.SelectedIndex = -1;
                                TextBox1.Text = "";
                                TabIPList.ActiveTabIndex = 0;
                                IBtn_Inventory.Visible = true;
                            }
                            else
                            {
                                dropdownlist();
                                //Response.Redirect("~" + url);
                                lblIPListInfo.Text = "";
                                SqlDataSource2.SelectCommand = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, ROOM.ROOM_STATION, IP_INFORMATION.IP_NO FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE (IP_INFORMATION.IP_ID = '" + TextBox1.Text.Trim() + "') ORDER BY ROOM.ROOM_BED ASC, ROOM.ROOM_STATION ASC";
                                gvwIPlist.DataBind();
                                gvwIPlist.SelectedIndex = -1;
                                lblOutIPListInfo.Text = "此查詢資料之住民尚未退住過";
                                SqlDataSource3.SelectCommand = "";
                                gvwOutIPList.DataBind();
                                gvwOutIPList.SelectedIndex = -1;
                                TextBox1.Text = "";
                                TabIPList.ActiveTabIndex = 0;
                                IBtn_Inventory.Visible = true;
                            }
                            dt_inipbyipid.Dispose();
                        }
                        dt_inipbyipid.Dispose();
                    }
                    else
                    {
                        string redirect = url;
                        Response.Write("<script>alert('查詢不到此住民或此住民無入住'); location.href='" + redirect + "'; </script>");
                    }
                    dt_ipid.Dispose();
                }
                dt_ipname.Dispose();
                TextBox1.Text = "";
            }
            dt_newipno.Dispose();
            */
            #endregion

        }


        protected void gvwIPlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string room = Convert.ToString(gvwIPlist.SelectedValue);
            SqlMain.fixiplist(room);
            ipno.Text = (SqlMain.GetIp_no());
            string IDs = SqlMain.IDsearch(SqlMain.GetIp_no());
            ipnonew.Text = IDs;
            iproom.Text = (SqlMain.GetR_bed());
            ipid.Text = (SqlMain.GetIp_id());
            int numOfipid = ipid.Text.Length;
            //ipidvisible.Text = numOfipid >= 6 ? ipid.Text.Substring(0, 6).PadRight(10, '*') : ipid.Text.Substring(0, numOfipid).PadRight(10, '*');
            //ipidvisible.Text = ipid.Text.Substring(0, 6).PadRight(10, '*');
            ipidvisible.Text = numOfipid >= 6 ? ipid.Text.Substring(0, 6).PadRight(10, '*') : "未填寫";

            ipname.Text = (SqlMain.GetIp_name());
            ipbirth.Text = sqlTime.DateAddSlash(SqlMain.GetIp_birth());
            ipage.Text = Convert.ToInt32(SqlMain.GetIp_age()) == -1 ? "" : SqlMain.GetIp_age();
            if (SqlMain.GetIp_sex().Trim() == "0")
            {
                ipsex.Text = "";
            }
            if (SqlMain.GetIp_sex().Trim() == "1")
            {
                ipsex.Text = "男";
            }
            if (SqlMain.GetIp_sex().Trim() == "2")
            {
                ipsex.Text = "女";
            }

            //photo
            if (!SqlMain.Get_PHOTO().Trim().Equals(""))
            {
                string photo_url = Page.ResolveUrl("~") + "Image/" + connection_id + "/IPImage/" + SqlMain.Get_PHOTO();
                img_photo.ImageUrl = photo_url;
                img_photo.Visible = true;
            }
            else if (SqlMain.GetIp_sex().Trim() == "1")
            {
                string photo_url = Page.ResolveUrl("~") + "Image/WebImage/male.jpg";
                img_photo.ImageUrl = photo_url;
                img_photo.Visible = true;
            }
            else if (SqlMain.GetIp_sex().Trim() == "2")
            {
                string photo_url = Page.ResolveUrl("~") + "Image/WebImage/female.jpg";
                img_photo.ImageUrl = photo_url;
                img_photo.Visible = true;
            }
            else
            {
                img_photo.Visible = false;
            }

            ipfir.Text = sqlTime.DateAddSlash(SqlMain.GetIp_fir());

            Session["ipno"] = ipno.Text;
            HttpCookie myIpno_Cookie = Request.Cookies["ipno"];
            if (myIpno_Cookie == null)
            {
                HttpCookie myipno_Cookie = new HttpCookie("ipno");
                myipno_Cookie.Values.Add("ipno", ipno.Text);
            }
            else
            {
                myIpno_Cookie.Values.Add("ipno", ipno.Text);
            }
            Session["ipsex"] = ipsex.Text;
            HttpCookie myIpsex_Cookie = Request.Cookies["ipsex"];
            if (myIpsex_Cookie == null)
            {
                HttpCookie myipsex_Cookie = new HttpCookie("ipsex");
                myipsex_Cookie.Values.Add("ipsex", ipsex.Text);
            }
            else
            {
                myIpsex_Cookie.Values.Add("ipsex", ipsex.Text);
            }

            Session["lbltabindx2"] = "0";
            Response.Redirect("~" + url);
        }

        protected void gvwOutIPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string no = Convert.ToString(gvwOutIPList.SelectedValue);
            DataTable dt_IP_InRec = SqlMain.get_dt_IP_InRec(no, Session["H_id"].ToString());
            ipno.Text = dt_IP_InRec.Rows[0]["IP_NO"].ToString();
            //Session["ipno"] = ipno.Text;
            ipnonew.Text = SqlMain.IDsearch(ipno.Text);
            iproom.Text = "";
            //iproom.Text = (SqlMain.GetR_bed());
            ipid.Text = SqlMain.GetIPID(ipno.Text);
            int numOfipid = ipid.Text.Length;
            //ipidvisible.Text = numOfipid >= 6 ? ipid.Text.Substring(0, 6).PadRight(10, '*') : ipid.Text.Substring(0, numOfipid).PadRight(10, '*');
            ipidvisible.Text = numOfipid >= 6 ? ipid.Text.Substring(0, 6).PadRight(10, '*') : "未填寫";

            ipname.Text = SqlMain.GetIPName(ipno.Text);
            ipbirth.Text = sqlTime.DateAddSlash(SqlMain.GetIPBirthday(ipno.Text));
            ipage.Text = Convert.ToInt32(SqlMain.GetIPAge(ipno.Text)) == -1 ? "" : SqlMain.GetIPAge(ipno.Text);
            if (SqlMain.GetIPSex(ipno.Text).Trim() == "0")
            {
                ipsex.Text = "";
            }
            if (SqlMain.GetIPSex(ipno.Text).Trim() == "1")
            {
                ipsex.Text = "男";
            }
            if (SqlMain.GetIPSex(ipno.Text).Trim() == "2")
            {
                ipsex.Text = "女";
            }
            //Session["ipsex"] = ipsex.Text;
            Session["ipno"] = ipno.Text;
            HttpCookie myIpno_Cookie = Request.Cookies["ipno"];
            if (myIpno_Cookie == null)
            {
                HttpCookie myipno_Cookie = new HttpCookie("ipno");
                myipno_Cookie.Values.Add("ipno", ipno.Text);
            }
            else
            {
                myIpno_Cookie.Values.Add("ipno", ipno.Text);
            }
            Session["ipsex"] = ipsex.Text;
            HttpCookie myIpsex_Cookie = Request.Cookies["ipsex"];
            if (myIpsex_Cookie == null)
            {
                HttpCookie myipsex_Cookie = new HttpCookie("ipsex");
                myipsex_Cookie.Values.Add("ipsex", ipsex.Text);
            }
            else
            {
                myIpsex_Cookie.Values.Add("ipsex", ipsex.Text);
            }
            //DataTable dt_IP_InOutDate = SqlMain.get_dt_IP_InOutDate(ipno.Text, Session["H_id"].ToString());
            ipfir.Text = DGFormatRIDDATERange(dt_IP_InRec.Rows[0]["IN_DATE"].ToString() + "~" + dt_IP_InRec.Rows[0]["OUT_DATE"].ToString());
            //ipfir.Text = gvwOutIPList.Rows[gvwOutIPList.SelectedIndex].Cells[1].ToString();
            //ipfir.Text = sqlTime.DateAddSlash(SqlMain.GetIp_fir());
            Session["lbltabindx2"] = "1";
            /*
            if (url == "/FNursingDashboard_MIPN.aspx" || url == "/FNursingList_MIPN.aspx")
            {
               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test", "location.href='../../FNursingRemind.aspx';", true);
            }
            else
            {
               Response.Redirect("~" + url);
            }
            */
            //photo
            if (!SqlMain.GetPHOTO(ipno.Text).Trim().Equals(""))
            {
                string photo_url = Page.ResolveUrl("~") + "Image/" + connection_id + "/IPImage/" + SqlMain.GetPHOTO(ipno.Text);
                img_photo.ImageUrl = photo_url;
                img_photo.Visible = true;
            }
            else if (SqlMain.GetIPSex(ipno.Text).Trim() == "1")
            {
                string photo_url = Page.ResolveUrl("~") + "Image/WebImage/male.jpg";
                img_photo.ImageUrl = photo_url;
                img_photo.Visible = true;
            }
            else if (SqlMain.GetIPSex(ipno.Text).Trim() == "2")
            {
                string photo_url = Page.ResolveUrl("~") + "Image/WebImage/female.jpg";
                img_photo.ImageUrl = photo_url;
                img_photo.Visible = true;
            }
            else
            {
                img_photo.Visible = false;
            }
            dt_IP_InRec.Dispose();

        }

        protected string DGFormatRIDDATERange(string strDATERange)
        {
            if (strDATERange.Length == 9)
            {
                return strDATERange.Substring(0, 4) + "/" + strDATERange.Substring(4, 2) + "/" + strDATERange.Substring(6, 2);
            }
            else if (strDATERange.Length > 9)
            {
                return strDATERange.Substring(0, 4) + "/" + strDATERange.Substring(4, 2) + "/" + strDATERange.Substring(6, 2) + "<br/>|<br/>" + strDATERange.Substring(9, 4) + "/" + strDATERange.Substring(13, 2) + "/" + strDATERange.Substring(15, 2);
            }
            else
            {
                return "";
            }
        }

        protected void ifhaveipno()
        {
            string reipno = "";
            if (Session["ipno"] != null)
            {
                reipno = Session["ipno"].ToString();
            }
            else
            {
                HttpCookie myipno_Cookie = Request.Cookies["ipno"];
                if (myipno_Cookie != null)
                {
                    Session["ipno"] = myipno_Cookie.Values["ipno"];
                    reipno = myipno_Cookie.Values["ipno"];
                }
            }
            ipno.Text = reipno;
            ipnonew.Text = SqlMain.IDsearch(reipno);
            iproom.Text = SqlMain.GetIPBed(reipno);
            ipid.Text = SqlMain.GetIPID(reipno);
            int numOfipid = ipid.Text.Length;
            //ipidvisible.Text = numOfipid >= 6 ? ipid.Text.Substring(0, 6).PadRight(10, '*') : ipid.Text.Substring(0, numOfipid).PadRight(10, '*');
            //ipidvisible.Text = ipid.Text.Substring(0, 6).PadRight(10, '*');
            ipidvisible.Text = numOfipid >= 6 ? ipid.Text.Substring(0, 6).PadRight(10, '*') : "未填寫";

            ipname.Text = SqlMain.GetIPName(reipno);
            ipbirth.Text = sqlTime.DateAddSlash(SqlMain.GetIPBirthday(reipno));
            ipage.Text = Convert.ToInt32(SqlMain.GetIPAge(reipno)) == -1 ? "" : SqlMain.GetIPAge(reipno);
            if (SqlMain.GetIPSex(reipno).Trim() == "1")
            {
                ipsex.Text = "男";
            }
            else if (SqlMain.GetIPSex(reipno).Trim() == "2")
            {
                ipsex.Text = "女";
            }
            else { ipsex.Text = ""; }

            Session["ipno"] = ipno.Text;
            HttpCookie myIpno_Cookie = Request.Cookies["ipno"];
            if (myIpno_Cookie == null)
            {
                HttpCookie myipno_Cookie = new HttpCookie("ipno");
                myipno_Cookie.Values.Add("ipno", ipno.Text);
            }
            else
            {
                myIpno_Cookie.Values.Add("ipno", ipno.Text);
            }
            Session["ipsex"] = ipsex.Text;
            HttpCookie myIpsex_Cookie = Request.Cookies["ipsex"];
            if (myIpsex_Cookie == null)
            {
                HttpCookie myipsex_Cookie = new HttpCookie("ipsex");
                myipsex_Cookie.Values.Add("ipsex", ipsex.Text);
            }
            else
            {
                myIpsex_Cookie.Values.Add("ipsex", ipsex.Text);
            }

            //photo
            if (!SqlMain.GetPHOTO(reipno).Trim().Equals(""))
            {
                string url = Page.ResolveUrl("~") + "Image/" + connection_id + "/IPImage/" + SqlMain.GetPHOTO(reipno);
                img_photo.ImageUrl = url;
                img_photo.Visible = true;
            }
            else if (SqlMain.GetIPSex(reipno).Trim() == "1")
            {
                string url = Page.ResolveUrl("~") + "Image/WebImage/male.jpg";
                img_photo.ImageUrl = url;
                img_photo.Visible = true;
            }
            else if (SqlMain.GetIPSex(reipno).Trim() == "2")
            {
                string url = Page.ResolveUrl("~") + "Image/WebImage/female.jpg";
                img_photo.ImageUrl = url;
                img_photo.Visible = true;
            }
            else
            {
                img_photo.Visible = false;
            }

            if (Session["lbltabindx2"].ToString() == "0")
            {
                ipfir.Text = sqlTime.DateAddSlash(SqlMain.GetIPFIR(reipno));
            }
            else
            {
                DataTable dt_IP_InRec = SqlMain.get_dt_IP_InRec1(reipno, Session["H_id"].ToString());
                ipfir.Text = DGFormatRIDDATERange(dt_IP_InRec.Rows[0]["IN_DATE"].ToString() + "~" + dt_IP_InRec.Rows[0]["OUT_DATE"].ToString());
                dt_IP_InRec.Dispose();
            }
        }
        protected void dropdownlist()
        {
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
            //dropRoomtype.Items.Clear();
            dropArea.Items.Clear();
            dropArea.DataBind();
            //dropRoomtype.DataBind();
            //dropRoomtype.Items.Add(new ListItem(Language.Language.translate("Choose", language), ""));
            dropArea.Items.Add(new ListItem(Language.Language.translate("Choose", language), ""));
            //reverselist(dropRoomtype);
            reverselist(dropArea);
        }
        protected void reverselist(DropDownList list)
        {
            string text = list.Items[list.Items.Count - 1].Text;
            string value = list.Items[list.Items.Count - 1].Value;
            for (int i = list.Items.Count - 1; i >= 1; i--)
            {
                list.Items[i].Text = list.Items[i - 1].Text;
                list.Items[i].Value = list.Items[i - 1].Value;
            }
            list.Items[0].Text = text;
            list.Items[0].Value = value;
        }

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href='../../FrontNews.aspx'</script>");
        }

        protected void LinkBtnTabView1_Click(object sender, EventArgs e)
        {
            multiTabs.ActiveViewIndex = 0;
            LinkBtnTabView1.Font.Bold = true;
            LinkBtnTabView2.Font.Bold = false;
            Session["lbltabindx2"] = "0";
        }

        protected void LinkBtnTabView2_Click(object sender, EventArgs e)
        {
            multiTabs.ActiveViewIndex = 1;
            LinkBtnTabView1.Font.Bold = false;
            LinkBtnTabView2.Font.Bold = true;
            Session["lbltabindx2"] = "1";
        }

        protected void IBtn_Inventory_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test", "location.href='../../FNursingList_MIPN.aspx';", true);
        }
    }
}