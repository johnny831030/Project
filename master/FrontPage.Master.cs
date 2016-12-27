using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml;
using System.IO;

namespace longtermcare
{
    public partial class FrontPage : System.Web.UI.MasterPage
    {
        string url = HttpContext.Current.Request.CurrentExecutionFilePath;
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
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            SqlDataSource1.ConnectionString = datasource;
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
            string degree = Session["degree"].ToString();
            //  int igroup = System.Convert.ToInt32(group);
            SqlMain.position(degree);
            string posname = SqlMain.Getposname();

            emp_name.Text = "：" + empname;
            emp_position.Text = "：" + posname;
            txtipname.Text = "：" + Session["hp_sname"].ToString();
            lblBNO.Text = Session["BNO"].ToString();
            Page.Title = Session["hp_sname"].ToString();

            string ServerMapPath = Server.MapPath("~/").ToString();
            XmlDataDocument xmldoc = new XmlDataDocument();
            //string ServerMapPath = Server.MapPath("~/").ToString();
            FileStream fs = new FileStream(ServerMapPath + "xmlLinkOutSys.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            XmlNodeList xmlnodeOutLinkYN;
            xmlnodeOutLinkYN = xmldoc.GetElementsByTagName("OutLinkYN");
            string str_outlinkyn = xmlnodeOutLinkYN[0].ChildNodes.Item(0).InnerText.Trim();
            if (str_outlinkyn == "Y")
            {
                IBtn_CUTalkMan.Visible = true;
                IBtn_CUTalkMeetingRec.Visible = true;
            }
            else
            {
                IBtn_CUTalkMan.Visible = false;
                IBtn_CUTalkMeetingRec.Visible = false;
            }

            /*
            lblempname.Text = Language.Language.translate("Emp_Name", language);
            lblemppos.Text = Language.Language.translate("Emp_Title", language);
            ButtonLogout.Text = Language.Language.translate("Logout_Button", language);
            */
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
                //SqlDataSource1.SelectCommand = "";
                // SqlDataSource1.SelectCommand = SqlMain.GetEMPNewsCom(login, nowdate);
                // gvwEMPNews.DataBind();

                SqlMain.getselectt(login);
                string[] a = SqlMain.Getselectnewss();


                for (int i = 0; i < a.Length; i++)
                {
                    if (SqlMain.GetisTrue2(a[i], connection_id, nowdate))
                        SqlDataSource1.SelectCommand += "OR NEWS.NO ='" + a[i] + "'";
                    // Label2.Text = SqlDataSource1.SelectCommand;
                }
                SqlDataSource1.SelectCommand += ")ORDER BY R_DATE DESC,NO DESC";
                SqlDataSource1.SelectParameters.Add("BELONG", login);
                SqlDataSource1.SelectParameters.Add("BELONG2", "All2");
                SqlDataSource1.SelectParameters.Add("DATE", nowdate);
                gvwEMPNews.DataBind();

                if (gvwEMPNews.Rows.Count == 0)
                {
                    lblNews.Text = "沒有安排個人行程";
                }
                else
                    lblNews.Text = "";
            }

        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Write("<script>window.location.href='../H_P_Login.aspx'</script>");
            SqlMain.logout();
        }

        protected void Warning(object sender,EventArgs e)
        {
            ShowErr.Text = "施工中，敬請見諒";
            ScriptManager.RegisterStartupScript(Page, GetType(), "Warning", "runEffect2();", true);
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

        protected void SChinese_Click(object sender, EventArgs e)
        {
            Session["language"] = "sch";
            Response.Redirect("~" + url);
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
            choosedate = calEMPNews.SelectedDate.ToString("yyyyMMdd");

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
            //string language = Session["language"].ToString();
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
        protected string DGFormatRID2(string datetime)
        {
            DateTime NewDate = DateTime.ParseExact(datetime, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            return NewDate.ToString("yyyy/MM/dd"); //HH:mm:ss
        }

        protected void ButtonHome_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href='../../FrontNews.aspx'</script>");
        }

        protected void IBtn_CUTalkMan_Click(object sender, ImageClickEventArgs e)
        {
            DateTime nx = new DateTime(1970, 1, 1);
            System.TimeSpan st = new TimeSpan();
            st = DateTime.UtcNow - nx;
            Random rnd = new Random();
            string str_Rnd = (rnd.Next(10000000, 99999999)).ToString();
            string plaintext = st.TotalSeconds + "_CU^Talk#CCU_" + str_Rnd;
            string str_mdf = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(plaintext, "MD5").ToLower();
            XmlDataDocument xmldoc = new XmlDataDocument();
            string ServerMapPath = Server.MapPath("~/").ToString();
            FileStream fs = new FileStream(ServerMapPath + "xmlLinkOutSys.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            XmlNodeList xmlnodeOutLink;
            xmlnodeOutLink = xmldoc.GetElementsByTagName("CUTalkManSys");
            string str_outlink = xmlnodeOutLink[0].ChildNodes.Item(0).InnerText.Trim();
            Response.Write("<script>window.open('" + str_outlink + "?t=" + st.TotalSeconds + "&k=" + str_Rnd + "&h=" + str_mdf + "','','menubar=no, status=no, scrollbars=yes, top=100, left=200, toolbar=no,width=1200, height=500, locationbar=false');</script>");
        }

        protected void IBtn_CUTalkMeetingRec_Click(object sender, ImageClickEventArgs e)
        {
            DateTime nx = new DateTime(1970, 1, 1);
            System.TimeSpan st = new TimeSpan();
            st = DateTime.UtcNow - nx;
            Random rnd = new Random();
            string str_Rnd = (rnd.Next(10000000, 99999999)).ToString();
            string plaintext = "cutalk@" + (st.TotalSeconds).ToString() + "@CCUMIS";
            string str_mdf = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(plaintext, "MD5").ToLower();
            XmlDataDocument xmldoc = new XmlDataDocument();
            string ServerMapPath = Server.MapPath("~/").ToString();
            FileStream fs = new FileStream(ServerMapPath + "xmlLinkOutSys.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            XmlNodeList xmlnodeOutLink;
            xmlnodeOutLink = xmldoc.GetElementsByTagName("CUTalkMeetingSys");
            string str_outlink = xmlnodeOutLink[0].ChildNodes.Item(0).InnerText.Trim();
            Response.Write("<script>window.open('" + str_outlink + "?hash=" + str_mdf + "&time=" + (st.TotalSeconds).ToString() + "','','menubar=no, status=no, scrollbars=yes, top=100, left=200, toolbar=no,width=1200, height=500, locationbar=false');</script>");
        }
    }
}