using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// FinPledgeApplyStockDetailForRepoHandler 的摘要说明
    /// </summary>
    public class FinPledgeApplyStockDetailForRepoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int pledgeApplyId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pid"]))
            {
                if (!int.TryParse(context.Request.QueryString["pid"], out pledgeApplyId))
                    pledgeApplyId = 0;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            FinanceService.FinService service = new FinanceService.FinService();
            string result = service.GetFinPledgeApplyStockDetailForRepoList(pageIndex, pageSize, orderStr, pledgeApplyId);
            context.Response.Write(result);            
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