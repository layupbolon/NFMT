using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NFMT.Contract;

namespace NFMTSite
{
    using System.Web.UI.HtmlControls;

    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cok = Request.Cookies[NFMT.Common.DefaultValue.UrlCookieName];
            if (cok != null)
            {
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cok.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                Response.AppendCookie(cok);
            }

            Response.Redirect("Default.aspx");
        }
    }
}