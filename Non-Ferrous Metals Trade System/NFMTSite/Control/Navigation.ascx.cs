using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Control
{
    public partial class Navigation : System.Web.UI.UserControl
    {
        public string CreateRoute()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("<a href=\"{0}MainForm.aspx\">首页</a>",NFMT.Common.DefaultValue.NfmtSiteName);

            foreach (KeyValuePair<string, string> kvp in this.Routes)
            {
                if (!string.IsNullOrEmpty(kvp.Value))
                    sb.AppendFormat(">><a href=\"{0}\">{1}</a>", kvp.Value, kvp.Key);
                else
                    sb.AppendFormat(">>{0}", kvp.Key);
            }

            return sb.ToString();
        }

        private Dictionary<string, string> routes = new Dictionary<string, string>();
        public Dictionary<string, string> Routes
        {
            get { return this.routes; }
            set { this.routes = value; }
        }
    }
}