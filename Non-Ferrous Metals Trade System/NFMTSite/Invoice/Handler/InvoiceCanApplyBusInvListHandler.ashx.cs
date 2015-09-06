using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// InvoiceCanApplyBusInvListHandler 的摘要说明
    /// </summary>
    public class InvoiceCanApplyBusInvListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string busInvIds = context.Request.QueryString["bids"];

            string stockLogIds = context.Request.QueryString["slIds"];

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
                    case "BusinessInvoiceId":
                        sortDataField = "bi.BusinessInvoiceId";
                        break;
                    case "InvoiceId":
                        sortDataField = "inv.InvoiceId";
                        break;
                    case "InvoiceNo":
                        sortDataField = "inv.InvoiceNo";
                        break;
                    case "InvoiceDate":
                        sortDataField = "inv.InvoiceDate";
                        break;
                    case "InvoiceBala":
                        sortDataField = "inv.InvoiceBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "SubNo":
                        sortDataField = "sub.SubNo";
                        break;
                    case "OutContractNo":
                        sortDataField = "sub.OutContractNo";
                        break;
                    case "CorpName":
                        sortDataField = "customerCorp.CorpName";
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
                    case "InvoicePrice":
                        sortDataField = "cp.FixedPrice";
                        break;
                    case "StockLogId":
                        sortDataField = "bid.StockLogId";
                        break;
                    case "ContractId":
                        sortDataField = "bi.ContractId";
                        break;
                    case "SubContractId":
                        sortDataField = "bi.SubContractId";
                        break;
                    case "CardNo":
                        sortDataField = "slog.CardNo";
                        break;
                    default: break;
                }

                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Invoice.BLL.InvoiceApplyBLL bll = new NFMT.Invoice.BLL.InvoiceApplyBLL();
            NFMT.Common.SelectModel select = bll.GetApplyBIInvInfoSelectModel(pageIndex, pageSize, orderStr, busInvIds, stockLogIds);
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