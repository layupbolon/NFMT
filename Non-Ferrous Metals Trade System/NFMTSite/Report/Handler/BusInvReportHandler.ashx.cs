using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// BusInvReportHandler 的摘要说明
    /// </summary>
    public class BusInvReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime startDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["s"]) || !DateTime.TryParse(context.Request["s"], out startDate))
                startDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["e"]) || !DateTime.TryParse(context.Request["e"], out endDate))
                endDate = NFMT.Common.DefaultValue.DefaultTime;
            else
                endDate = endDate.AddDays(1);

            int innerCorpId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["inner"]) || !int.TryParse(context.Request.QueryString["inner"], out innerCorpId))
                innerCorpId = 0;

            int outerCorpId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["outer"]) || !int.TryParse(context.Request.QueryString["outer"], out outerCorpId))
                outerCorpId = 0;

            int invType = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["invType"]) || !int.TryParse(context.Request.QueryString["invType"], out invType))
                invType = 0;

            int assetId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ass"]) || !int.TryParse(context.Request.QueryString["ass"], out assetId))
                assetId = 0;

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
                    case "innerCorp":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "outerCorp":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "InvoiceDirection":
                        sortDataField = "bdInvoiceDirection.DetailName";
                        break;
                    case "InvoiceType":
                        sortDataField = "bdInvoiceType.DetailName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "NetAmount":
                        sortDataField = "biDetail.NetAmount";
                        break;
                    case "MUName":
                        sortDataField = "mu.MUName";
                        break;
                    case "Bala":
                        sortDataField = "biDetail.Bala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetBusInvReportSelect(pageIndex, pageSize, orderStr, innerCorpId, outerCorpId, invType, assetId, startDate, endDate);
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