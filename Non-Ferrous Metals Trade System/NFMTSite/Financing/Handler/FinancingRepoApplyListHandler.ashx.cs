using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// FinancingRepoApplyListHandler 的摘要说明
    /// </summary>
    public class FinancingRepoApplyListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int status = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty;

            string pledgeApplyNo = context.Request["paNo"];
            string repoApplyIdNo = context.Request["reNo"];
            string refNo = context.Request["refNo"];

            if (!string.IsNullOrEmpty(context.Request["status"]))
                int.TryParse(context.Request["status"], out status);

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
            string result = service.GetFinancingRepoApplyList(pageIndex, pageSize, orderStr, status, pledgeApplyNo, repoApplyIdNo, refNo, beginDate, endDate);
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