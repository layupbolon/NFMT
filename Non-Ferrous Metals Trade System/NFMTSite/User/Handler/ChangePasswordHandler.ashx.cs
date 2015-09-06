using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ChangePasswordHandler 的摘要说明
    /// </summary>
    public class ChangePasswordHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string oldPwd = context.Request.Form["o"];
            if (string.IsNullOrEmpty(oldPwd))
            {
                result.Message = "原密码不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string newPwd = context.Request.Form["n"];
            if (string.IsNullOrEmpty(newPwd))
            {
                result.Message = "新密码不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.User.BLL.AccountBLL bll = new NFMT.User.BLL.AccountBLL();
            result = bll.ChangePwd(user, oldPwd, newPwd);
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}