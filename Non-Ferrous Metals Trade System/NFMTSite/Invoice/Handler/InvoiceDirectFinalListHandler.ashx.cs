using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// InvoiceDirectFinalListHandler 的摘要说明
    /// </summary>
    public class InvoiceDirectFinalListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime fromDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime toDate = NFMT.Common.DefaultValue.DefaultTime;
            int status = 0;

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);
            if (!string.IsNullOrEmpty(context.Request.QueryString["fd"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["fd"], out fromDate))
                    fromDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["td"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["td"], out toDate))
                    toDate = NFMT.Common.DefaultValue.DefaultTime;
                else
                    toDate.AddDays(1);
            }

            int inCorpId = 0;
            int outCorpId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ic"]) || !int.TryParse(context.Request.QueryString["ic"].Trim(), out inCorpId))
            {
                inCorpId = 0;
            }

            if (string.IsNullOrEmpty(context.Request.QueryString["oc"]) || !int.TryParse(context.Request.QueryString["oc"].Trim(), out outCorpId))
            {
                outCorpId = 0;
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

                switch (sortDataField)
                {
                    case "InvoiceDate":
                        sortDataField = "inv.InvoiceDate";
                        break;
                    case "InvoiceNo":
                        sortDataField = "inv.InvoiceNo";
                        break;
                    case "InvoiceName":
                        sortDataField = "inv.InvoiceName";
                        break;
                    case "InvoiceBala":
                        sortDataField = "inv.InvoiceBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName ";
                        break;
                    case "OutCorpName":
                        sortDataField = "inv.OutCorpName";
                        break;
                    case "InCorpName":
                        sortDataField = "inv.InCorpName";
                        break;
                    case "VATRatio":
                        sortDataField = "bi.VATRatio";
                        break;
                    case "VATBala":
                        sortDataField = "bi.VATBala";
                        break;
                    case "StatusName":
                        sortDataField = "inv.InvoiceStatus";
                        break;
                    case "DirectionName":
                        sortDataField = "inv.InvoiceDirection";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
            NFMT.Common.SelectModel select = bll.GetDirectFinalSelect(pageIndex, pageSize, orderStr, fromDate, toDate, status, inCorpId, outCorpId);
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