using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Message.Handler
{
    /// <summary>
    /// MessageReadHandler 的摘要说明
    /// </summary>
    public class MessageReadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string smsId = context.Request.Form["id"];

            if (string.IsNullOrEmpty(smsId))
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }
            //if (!int.TryParse(context.Request.Form["id"], out smsId) || smsId <= 0)
            //{
            //    context.Response.Write("序号错误");
            //    context.Response.End();
            //}

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.Sms.BLL.SmsBLL bll = new NFMT.Sms.BLL.SmsBLL();
            result = bll.ReadSms(user, smsId);
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