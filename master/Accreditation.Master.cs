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
    public partial class Accreditation : System.Web.UI.MasterPage
    {
        string url = HttpContext.Current.Request.CurrentExecutionFilePath;
        int init = 0;
        private static string choosedate;
        private static string datasource;
            
        protected void Page_Load(object sender, EventArgs e)
        {
            string language = Session["language"].ToString();            
            string connection_id = Session["H_id"].ToString();
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);

            SqlMenu.connect(connection_id);  
            English.Text = Language.Language.translate("En_Button", language);
            Chinese.Text = Language.Language.translate("Ch_Button", language);
            Tailand.Text = Language.Language.translate("Thai", language);
            Vietnamese.Text = Language.Language.translate("Vietnamese", language);
            HyperLink1.Text = Language.Language.translate("IpManagement", language);
            HyperLink2.Text = Language.Language.translate("IpNursing", language);
            HyperLink6.Text = Language.Language.translate("MMManagement", language);
            HyperLink3.Text = Language.Language.translate("Administration", language);
            HyperLink4.Text = Language.Language.translate("BasicManagement", language);
            HyperLink5.Text = Language.Language.translate("AuthorityManagement", language);
            HyperLink7.Text = Language.Language.translate("NursingPlan", language);
            HyperLink9.Text = Language.Language.translate("TrainingVideo", language);
            /*
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
                string master = "9";
                makemenu(group, master, language);
            }
            if (!IsPostBack)
            {
                string login = Session["account"].ToString();
                string nowdate = sqlTime.time();
               // SqlDataSource1.SelectCommand = "";
               // SqlDataSource1.ConnectionString = datasource;
                //SqlDataSource1.SelectCommand = SqlMain.GetEMPNewsCom(login, nowdate);
               // gvwEMPNews.DataBind();
                SqlMain.getselectt(login);
                string[] a = SqlMain.Getselectnewss();

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

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href='../../FrontNews.aspx'</script>");
        }
     
    }
}