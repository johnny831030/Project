using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRole1;
using System.Configuration;

namespace longtermcare
{
    public partial class BasicManagement : System.Web.UI.MasterPage
    {
        string url = HttpContext.Current.Request.CurrentExecutionFilePath;
        int init = 0;
        private static string datasource;
        private static string choosedate;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.SelectParameters.Clear();
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
            string connection_id = "";
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
            datasource =HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            SqlDataSource1.ConnectionString = datasource;
            SqlMenu.connect(connection_id);  
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
            
            gvwEMPNews.Columns[0].HeaderText = Language.Language.translate("EmpNewsDate", language);
            gvwEMPNews.Columns[1].HeaderText = Language.Language.translate("EmpNews", language);
            */
            string empname = Session["name"].ToString();
            string group = Session["group"].ToString();
            string degree = Session["degree"].ToString();
            // string ip = Session["ipno"].ToString();
           // int igroup = System.Convert.ToInt32(group);
            SqlMain.position(degree);
            string posname = SqlMain.Getposname();

            emp_name.Text = "：" + empname;
            emp_position.Text = "：" + posname;
            txtipname.Text = "：" + Session["hp_sname"].ToString();
            Page.Title = Session["hp_sname"].ToString();
            lblBNO.Text = Session["BNO"].ToString();
            /*
            lblempname.Text = Language.Language.translate("Emp_Name", language);
            lblemppos.Text = Language.Language.translate("Emp_Title", language);
            ButtonLogout.Text = Language.Language.translate("Logout_Button", language);
            */
            if (!IsPostBack)
            {
                string master = "5";
                makemenu(group, master, language);
            }
            if (!IsPostBack)
            {
                string login = "";
                if (Session["account"] != null)
                {
                    login = Session["account"].ToString();
                }
                else
                {
                    HttpCookie myaccount_Cookie = Request.Cookies["account"];
                    Session["account"] = myaccount_Cookie.Values["account"];
                    login = myaccount_Cookie.Values["account"];
                }
                string nowdate = sqlTime.time();
               // SqlDataSource1.SelectCommand = "";
               // SqlDataSource1.ConnectionString = datasource;
               // SqlDataSource1.SelectCommand = SqlMain.GetEMPNewsCom(login, nowdate);
               // gvwEMPNews.DataBind();
                SqlMain.getselectt(login);
                string[] a = SqlMain.Getselectnewss();

                for (int i = 0; i < a.Length; i++)
                {
                    if (SqlMain.GetisTrue(a[i], connection_id))
                        SqlDataSource1.SelectCommand += " OR NO ='" + a[i] + "'";
                    // Label2.Text = SqlDataSource1.SelectCommand;
                }
                SqlDataSource1.SelectCommand += " ORDER BY R_DATE DESC, NO DESC";
                SqlDataSource1.SelectParameters.Add("BELONG", login);
                SqlDataSource1.SelectParameters.Add("BELONG2", "All2");
                SqlDataSource1.SelectParameters.Add("DATE", nowdate);
                gvwEMPNews.DataBind();
                if (gvwEMPNews.Rows.Count == 0)
                {
                    lblNews.Text = "今天沒有安排個人行程";
                }
                else
                    lblNews.Text = "";
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
        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Write("<script>window.location.href='../../H_P_Login.aspx'</script>");
            SqlMain.logout();
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
                    }

        protected void SChinese_Click(object sender, EventArgs e)
        {
            Session["language"] = "sch";
            Response.Redirect("~" + url);
        }


        protected string DGFormatRID2(string datetime)
        {
            //DGFormatRID(Convert.ToString(DataBinder.Eval(Container.DataItem,"P_USER")))

            return sqlTime.DateAddSlash(datetime);
        }

        protected void calEMPNews_SelectionChanged(object sender, EventArgs e)
        {
            string login = "";
            if (Session["account"] != null)
            {
                login = Session["account"].ToString();
            }
            else
            {
                HttpCookie myaccount_Cookie = Request.Cookies["account"];
                Session["account"] = myaccount_Cookie.Values["account"];
                login = myaccount_Cookie.Values["account"];
            }
            choosedate = calEMPNews.SelectedDate.ToString("yyyyMMdd");
            string connection_id = "";
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
            SqlMain.getselectt(login);
            string[] a = SqlMain.Getselectnewss();

            for (int i = 0; i < a.Length; i++)
            {
                if (SqlMain.GetisTrue(a[i], connection_id))
                    SqlDataSource1.SelectCommand += "OR NO ='" + a[i] + "'";
                //Label2.Text = SqlDataSource1.SelectCommand + "OR NO ='" + a[i] + "'";
            }
            SqlDataSource1.SelectCommand += "ORDER BY R_DATE DESC,NO DESC";
            SqlDataSource1.SelectParameters.Add("BELONG", login);
            SqlDataSource1.SelectParameters.Add("BELONG2", "All2");
            SqlDataSource1.SelectParameters.Add("DATE", choosedate);
            gvwEMPNews.DataBind();

            if (gvwEMPNews.PageIndex >= 0)
                gvwEMPNews.PageIndex = 0;
            //   SqlDataSource1.SelectCommand = "";
            //  SqlDataSource1.ConnectionString = datasource;
            //    SqlDataSource1.SelectCommand = SqlMain.GetEMPNewsCom(login, choosedate);


            //  gvwEMPNews.DataBind();
            if (gvwEMPNews.Rows.Count == 0)
            {
                lblNews.Text = "沒有安排個人行程";
            }
            else
                lblNews.Text = "";

        }

        protected void CustomersGridView_PageIndexChanged(Object sender, EventArgs e)
        {
            string login = "";
            if (Session["account"] != null)
            {
                login = Session["account"].ToString();
            }
            else
            {
                HttpCookie myaccount_Cookie = Request.Cookies["account"];
                Session["account"] = myaccount_Cookie.Values["account"];
                login = myaccount_Cookie.Values["account"];
            }
            string connection_id = "";
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
            if (choosedate == null)
                choosedate = sqlTime.time();
            string nowdate = choosedate;
            SqlMain.getselectt(login);
            string[] a = SqlMain.Getselectnewss();

            for (int i = 0; i < a.Length; i++)
            {
                if (SqlMain.GetisTrue(a[i], connection_id))
                    SqlDataSource1.SelectCommand += "OR NO ='" + a[i] + "'";
                // Label2.Text = SqlDataSource1.SelectCommand + "OR NO ='" + a[i] + "'";
            }
            SqlDataSource1.SelectCommand += "ORDER BY R_DATE DESC,NO DESC";
            SqlDataSource1.SelectParameters.Add("BELONG", login);
            SqlDataSource1.SelectParameters.Add("BELONG2", "All2");
            SqlDataSource1.SelectParameters.Add("DATE", nowdate);
            gvwEMPNews.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
            for (int i = 0; i < gvwEMPNews.Rows.Count; i++)
            {
                GridViewRow row = gvwEMPNews.Rows[i];
                {
                    string a = ((HiddenField)row.Cells[0].FindControl("HiddenField1")).Value.ToString();
                    string coder = Convert.ToBase64String(Mail_Check.encrypt1.encrypt(a, "D$KtjS*)ch$ej^o&%%$y"));
                    coder = Mail_Check.encrypt1.EnCode(coder);
                    ((HyperLink)row.Cells[0].FindControl("HyperLink8")).NavigateUrl = "~/ShowNews.aspx?no=" + coder;
                    bool tag = SqlMain.GetTransfer(a);
                    if (tag)
                    {
                        ((HyperLink)row.Cells[0].FindControl("HyperLink8")).NavigateUrl = SqlMain.GetContent(a);
                        ((HyperLink)row.Cells[0].FindControl("HyperLink8")).Attributes.Add("onclick", "");
                    }
                }

            }

        }

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href='../../FrontNews.aspx'</script>");
        }

        public void EMPNewsDataBind()
        {
            string login = "";
            if (Session["account"] != null)
            {
                login = Session["account"].ToString();
            }
            else
            {
                HttpCookie myaccount_Cookie = Request.Cookies["account"];
                Session["account"] = myaccount_Cookie.Values["account"];
                login = myaccount_Cookie.Values["account"];
            }
            string connection_id = "";
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
            string nowdate = sqlTime.time();

            // SqlDataSource1.SelectCommand = "";
            // SqlDataSource1.ConnectionString = datasource;
            // SqlDataSource1.SelectCommand = SqlMain.GetEMPNewsCom(login, nowdate);
            // gvwEMPNews.DataBind();
            SqlMain.getselectt(login);
            string[] a = SqlMain.Getselectnewss();


            for (int i = 0; i < a.Length; i++)
            {
                if (SqlMain.GetisTrue(a[i], connection_id))
                    SqlDataSource1.SelectCommand += "OR NO ='" + a[i] + "'";
                // Label2.Text = SqlDataSource1.SelectCommand;
            }
            SqlDataSource1.SelectCommand += "ORDER BY R_DATE DESC,NO DESC";
            SqlDataSource1.SelectParameters.Add("BELONG", login);
            SqlDataSource1.SelectParameters.Add("BELONG2", "All2");
            SqlDataSource1.SelectParameters.Add("DATE", nowdate);
            gvwEMPNews.DataBind();
            if (gvwEMPNews.Rows.Count == 0)
            {
                lblNews.Text = "今天沒有安排個人行程";
            }
            else
                lblNews.Text = "";

        }
    }
}