using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// FinanceInvoiceInvApplyListHandler 的摘要说明
    /// </summary>
    public class FinanceInvoiceInvApplyListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 20;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime fromdate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["fd"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["fd"], out fromdate))
                    fromdate = NFMT.Common.DefaultValue.DefaultTime;
            }

            DateTime todate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["td"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["td"], out todate))
                    todate = NFMT.Common.DefaultValue.DefaultTime;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.Invoice.BLL.InvoiceApplyBLL bll = new NFMT.Invoice.BLL.InvoiceApplyBLL();
            NFMT.Common.SelectModel select = bll.GetCanIssueApplyListSelectModel(pageIndex, pageSize, orderStr, fromdate, todate);
            NFMT.Common.ResultModel result = bll.Load(user, select);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            System.Collections.Generic.Dictionary<string, object> dic = new System.Collections.Generic.Dictionary<string, object>();

            dic.Add("count", result.AffectCount);
            dic.Add("data", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic);

            context.Response.Write(postData);
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