using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        public string hrefStr = "#";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string title = !string.IsNullOrEmpty(Request.QueryString["t"]) ? Request.QueryString["t"].ToString() : "用户无权限或发生错误";
                this.spTitle.InnerText = title;

                if (!string.IsNullOrEmpty(Request.QueryString["r"]))
                {
                    hrefStr = Request.QueryString["r"].ToString();
                    this.redirect.HRef = hrefStr;
                }
            }
        }
    }
}