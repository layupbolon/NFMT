using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// GetFinStockInfo 的摘要说明
    /// </summary>
    public class GetFinStockInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string refNo = context.Request["text"];

            FinanceService.FinService service = new FinanceService.FinService();
            string json = service.GetFinStockInfo(refNo.ToLower());

            context.Response.Write(json);
            context.Response.End();
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