using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// FinBusInvAllotReportHandler 的摘要说明
    /// </summary>
    public class FinBusInvAllotReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int fIId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["id"]) || !int.TryParse(context.Request.QueryString["id"], out fIId))
                fIId = 0;

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
                    case "BusinessInvoiceId":
                        sortDataField = "bi.BusinessInvoiceId";
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
                        sortDataField = "bi.NetAmount";
                        break;
                    case "MUName":
                        sortDataField = "mu.MUName";
                        break;
                    case "InvoiceBala":
                        sortDataField = "inv.InvoiceBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Invoice.BLL.FinBusInvAllotBLL bll = new NFMT.Invoice.BLL.FinBusInvAllotBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetFinBusAllotReportSelect(pageIndex, pageSize, orderStr, fIId);
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