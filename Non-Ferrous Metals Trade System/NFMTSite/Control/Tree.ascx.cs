using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NFMTSite.Control
{
    public partial class Tree : System.Web.UI.UserControl
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(Tree));

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetMenu()
        {
            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                if (user == null || user.EmpId <= 0)
                    Response.Redirect(string.Format("{0}Login.aspx", NFMT.Common.DefaultValue.NfmtPassPort));
                
                NFMT.User.UserSecurity userSecurity = NFMT.User.UserProvider.GetUserSecurity(user.CookieValue, user.AccountName);
                List<NFMT.User.Model.Menu> menus = userSecurity.Menus;

                if (menus == null || menus.Count < 1)
                    return string.Empty;

                string url = this.Page.Request.Url.AbsolutePath.Substring(1);
                NFMT.User.Model.Menu selectMenu = menus.FirstOrDefault(temp => temp.Url == url);
                int selectParentId = 0;
                if (selectMenu != null && selectMenu.MenuId > 0)
                    selectParentId = selectMenu.ParentId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<ul style=\"margin: 0 0 0 0\">");
                sb.Append(Environment.NewLine);
                sb.Append(GetChildMenu(0, menus, selectParentId));
                sb.Append(Environment.NewLine);
                sb.Append("</ul>");

                return sb.ToString();
            }
            catch (Exception e)
            {
                this.log.ErrorFormat(e.Message);
                return string.Empty;
            }
            
        }

        private System.Text.StringBuilder GetChildMenu(int parentId, List<NFMT.User.Model.Menu> menus,int selectParentId)
        {
            List<NFMT.User.Model.Menu> childMenus;
            var result = from m in menus
                         where m.ParentId == parentId
                         select m;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (result != null && result.Any())
            {
                childMenus = result.ToList();
                sb.Append(parentId == 0 ? "" : "<ul>");
            }
            else
                return sb;

            foreach (NFMT.User.Model.Menu menu in childMenus)
            {
                sb.Append("<li ");

                if (selectParentId == menu.MenuId)
                    sb.AppendFormat("id=\"{0}\" item-expanded=\"true\">", menu.Id);
                else
                    sb.AppendFormat("id=\"{0}\" item-expanded=\"false\">", menu.Id);
               
                sb.Append(Environment.NewLine);
                sb.AppendFormat("   <img style=\"float: left; margin-right: 5px;\" src=\"{0}images/folder.png\" />", NFMT.Common.DefaultValue.NftmSiteName);
                sb.Append(Environment.NewLine);

                if (!string.IsNullOrEmpty(menu.Url))
                    sb.AppendFormat("   <a href=\"{0}{1}\" target=\"TargeForm\" item-title=\"true\">{2}</a>", NFMT.Common.DefaultValue.NftmSiteName, menu.Url, menu.MenuName);
                else
                    sb.AppendFormat("   <span>{0}</span>", menu.MenuName);

                sb.Append(Environment.NewLine);
                System.Text.StringBuilder sb2 = GetChildMenu(menu.Id, menus,selectParentId);
                if (sb2.ToString().Length > 1)
                {
                    sb.Append(sb2);
                    sb.Append("</ul>");
                }

                sb.Append("</li>");
            }

            return sb;
        }
    }
}