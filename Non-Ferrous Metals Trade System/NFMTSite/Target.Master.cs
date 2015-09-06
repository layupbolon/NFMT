using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite
{
    public partial class Target : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Dictionary<string, string> Routes
        {
            get { return this.navigation1.Routes; }
        }
    }
}