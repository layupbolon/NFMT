using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// FinancingPledgeApplyListForRepoCreateHandler 的摘要说明
    /// </summary>
    public class FinancingPledgeApplyListForRepoCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int status = -1, assetId = -1, bankId = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty;

            string pledgeApplyNo = context.Request["paNo"];
            string refNo = context.Request["refNo"];

            DateTime beginDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (!string.IsNullOrEmpty(context.Request["fromDate"]))
            {
                if (!DateTime.TryParse(context.Request["fromDate"], out beginDate))
                    beginDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request["toDate"]))
            {
                if (!DateTime.TryParse(context.Request["toDate"], out endDate))
                    endDate = NFMT.Common.DefaultValue.DefaultTime;
                else
                    endDate = endDate.AddDays(1);
            }

            if (!string.IsNullOrEmpty(context.Request["status"]))
                int.TryParse(context.Request["status"], out status);

            if (!string.IsNullOrEmpty(context.Request["assetId"]))
                int.TryParse(context.Request["assetId"], out assetId);

            if (!string.IsNullOrEmpty(context.Request["bankId"]))
                int.TryParse(context.Request["bankId"], out bankId);

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            FinanceService.FinService service = new FinanceService.FinService();
            string result = service.GetFinancingPledgeApplyListForRepoCreate(pageIndex, pageSize, orderStr, beginDate, endDate, bankId, assetId, status, pledgeApplyNo, refNo);
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