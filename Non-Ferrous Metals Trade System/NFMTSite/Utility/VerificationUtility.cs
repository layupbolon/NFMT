using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using log4net;
using NFMT.Common;
using NFMT.User.BLL;
using NFMT.User.Model;

namespace NFMTSite.Utility
{
    public class VerificationUtility
    {
        private ILog log = LogManager.GetLogger(typeof(VerificationUtility));

        public void JudgeOperate(Page page, int menuId, List<OperateEnum> operateTypes)
        {
            UserModel user = UserUtility.CurrentUser;

            try
            {
                AuthOperateBLL bll = new AuthOperateBLL();
                ResultModel result = bll.JudgeOperate(user, menuId, operateTypes);
                if (result.ResultStatus != 0)
                {
                    string oids = operateTypes.Aggregate(string.Empty, (current, operate) => current + (operate.ToString() + ","));

                    if (!string.IsNullOrEmpty(oids) && oids.IndexOf(',') > -1)
                        oids = oids.Substring(0, oids.Length - 1);

                    MenuBLL menuBLL = new MenuBLL();
                    result = menuBLL.Get(user, menuId);
                    if (result.ResultStatus != 0)
                        throw new Exception("获取菜单失败");

                    Menu menu = result.ReturnValue as Menu;

                    if (menu != null)
                    {
                        string redirectUrl = string.Format("{0}/ErrorPage.aspx?t={1}&r={2}", DefaultValue.NfmtSiteName, string.Format("用户无{0}-{1}权限", menu.MenuName, oids), string.Format("{0}MainForm.aspx",NFMT.Common.DefaultValue.NfmtSiteName));
                        page.Response.Redirect(redirectUrl,false);
                    }
                }
            }
            catch (Exception e)
            {
                log.ErrorFormat("用户{0},错误:{1}", user.EmpName, e.Message);
                page.Response.Redirect("/MainForm.aspx");
            }
        }

        public List<OperateEnum> JudgeUserOperate(Page page, UserModel user, int menuId)
        {
            try
            {
                AuthOperateBLL authOperateBLL = new AuthOperateBLL();
                ResultModel result = authOperateBLL.GetUserMenuOperate(user, menuId);
                if (result.ResultStatus != 0)
                    return null;

                return result.ReturnValue as List<OperateEnum>;
            }
            catch (Exception e)
            {
                log.ErrorFormat("用户{0},错误:{1}", user.EmpName, e.Message);
                return null;
            }
        }
    }
}