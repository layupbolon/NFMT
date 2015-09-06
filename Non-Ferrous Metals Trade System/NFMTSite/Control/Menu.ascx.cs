using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Control
{
    public partial class Menu : System.Web.UI.UserControl
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(Menu));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                    if (user == null || user.EmpId <= 0)
                        Response.Redirect(string.Format("{0}Login.aspx", NFMT.Common.DefaultValue.NfmtPassPort));
                    
                    this.liUser.InnerHtml = string.Format("<span style=\"color:blue\">{0}</span>，欢迎访问{1}", user.EmpName, NFMT.Common.DefaultValue.SystemName);

                    this.aLoginOut.HRef = string.Format("{0}LoginOut.aspx", NFMT.Common.DefaultValue.NfmtPassPort);
                }
                catch (Exception ex)
                {
                    this.log.ErrorFormat(ex.Message);
                }                
            }
        }
    }
}