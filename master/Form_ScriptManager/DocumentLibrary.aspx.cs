using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace longtermcare.Accreditations
{
    public partial class DocumentLibrary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void SyncMyTree(string MyTreeJson)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<G_JSTree> G_JSTreeArray = ser.Deserialize<List<G_JSTree>>(MyTreeJson);
        }

        public class G_JSTree
        {
            public G_JsTreeAttribute attr;
            public G_JSTree[] children;
            public string data
            {
                get;
                set;
            }
            public int IdServerUse
            {
                get;
                set;
            }
            public string icons
            {
                get;
                set;
            }
            public string state
            {
                get;
                set;
            }
        }

        public class G_JsTreeAttribute
        {
            public string id;
            public bool selected;
        }
    }
}