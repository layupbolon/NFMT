using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PaymentCheckAuditHandler 的摘要说明
    /// </summary>
    public class PaymentCheckAuditHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int paymentId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out paymentId))
                {
                    context.Response.Write("参数错误");
                    context.Response.End();
                }
            }

            NFMT.Funds.BLL.PaymentBLL bll = new NFMT.Funds.BLL.PaymentBLL();
            result = bll.PaymentCheckAudit(user, paymentId);

            if(result.ResultStatus==0)
                context.Response.Write(result.ResultStatus);
            else
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