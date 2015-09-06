using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteUtility
{
    public class VerificationUtility
    {
        public void JudgeOperate(System.Web.UI.Page page, int menuId, List<NFMT.Common.OperateEnum> operateTypes)
        {
            NFMT.Common.UserModel user = new SiteUtility.UserUtility().CurrentUser;

            try
            {
                NFMT.User.BLL.AuthOperateBLL bll = new NFMT.User.BLL.AuthOperateBLL();
                NFMT.Common.ResultModel result = bll.JudgeOperate(user, menuId, operateTypes);
                if (result.ResultStatus != 0)
                {
                    string oids = string.Empty;
                    foreach (NFMT.Common.OperateEnum operate in operateTypes)
                    {
                        oids += operate.ToString() + ",";
                    }

                    if (!string.IsNullOrEmpty(oids) && oids.IndexOf(',') > -1)
                        oids = oids.Substring(0, oids.Length - 1);

                    NFMT.User.BLL.MenuBLL menuBLL = new NFMT.User.BLL.MenuBLL();
                    result = menuBLL.Get(user, menuId);
                    if (result.ResultStatus != 0)
                        throw new Exception("获取菜单失败");

                    NFMT.User.Model.Menu menu = result.ReturnValue as NFMT.User.Model.Menu;

                    string redirectUrl = string.Format("{0}/ErrorPage.aspx?t={1}&r={2}", NFMT.Common.DefaultValue.NfmtSiteName, string.Format("用户无{0}-{1}权限", menu.MenuName, oids), "/MainForm.aspx");
                    page.Response.Redirect(redirectUrl, false);
                }
            }
            catch
            {
                page.Response.Redirect("/MainForm.aspx");
            }
        }
    }
}