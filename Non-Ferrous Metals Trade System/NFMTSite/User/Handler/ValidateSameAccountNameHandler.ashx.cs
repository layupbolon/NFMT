using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// ValidateSameAccountNameHandler 的摘要说明
    /// </summary>
    public class ValidateSameAccountNameHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";
            if (string.IsNullOrEmpty(context.Request["accountName"]))
            {
                result.Message = "报关序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.User.BLL.AccountBLL bll = new NFMT.User.BLL.AccountBLL();
            result = bll.ValidateAccountName(user, context.Request["accountName"].ToString());
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