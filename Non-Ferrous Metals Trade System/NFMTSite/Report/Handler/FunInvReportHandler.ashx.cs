using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// FunInvReportHandler 的摘要说明
    /// </summary>
    public class FunInvReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string invoiceNo = context.Request.QueryString["invNo"];
            string invoiceName = context.Request.QueryString["invName"];

            DateTime startDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request.QueryString["s"]) || !DateTime.TryParse(context.Request.QueryString["s"], out startDate))
                startDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request.QueryString["e"]) || !DateTime.TryParse(context.Request.QueryString["e"], out endDate))
                endDate = NFMT.Common.DefaultValue.DefaultTime;
            else
                endDate = endDate.AddDays(1);

            int invDir = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["invDir"]) || !int.TryParse(context.Request.QueryString["invDir"], out invDir))
                invDir = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                switch (sortDataField)
                {
                    case "InvoiceId":
                        sortDataField = "inv.InvoiceId";
                        break;
                    case "InvoiceDate":
                        sortDataField = "inv.InvoiceDate";
                        break;
                    case "InvoiceNo":
                        sortDataField = "inv.InvoiceNo";
                        break;
                    case "InvoiceName":
                        sortDataField = "inv.InvoiceName";
                        break;
                    case "DetailName":
                        sortDataField = "bdInvoiceDirection.DetailName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "NetAmount":
                        sortDataField = "fi.NetAmount";
                        break;
                    case "MUName":
                        sortDataField = "mu.MUName";
                        break;
                    case "InvoiceBala":
                        sortDataField = "ass.AssetName";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "innerCorp":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "outerCorp":
                        sortDataField = "outCorp.CorpName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Invoice.BLL.FinanceInvoiceBLL bll = new NFMT.Invoice.BLL.FinanceInvoiceBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetFinInvReportSelect(pageIndex, pageSize, orderStr, invoiceNo, invoiceName, invDir, startDate, endDate);
            NFMT.Common.ResultModel result = bll.Load(user, select);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("count", totalRows);
            dic.Add("data", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic, new Newtonsoft.Json.Converters.DataTableConverter());

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