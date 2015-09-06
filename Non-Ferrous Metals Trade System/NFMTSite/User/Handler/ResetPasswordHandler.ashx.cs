using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ResetPasswordHandler 的摘要说明
    /// </summary>
    public class ResetPasswordHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string idStr = context.Request.Form["id"];
            int empId = 0;
            if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out empId) || empId <= 0)
            {
                context.Response.Write("参数错误");
                context.Response.End();
            }

            try
            {
                NFMT.User.BLL.AccountBLL bll = new NFMT.User.BLL.AccountBLL();
                result = bll.ResetPassword(user, empId);
                if (result.ResultStatus == 0)
                {
                    result.Message = "重置密码成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(result.Message);
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