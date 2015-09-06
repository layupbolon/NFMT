using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// GrossProfitReportHandler 的摘要说明
    /// </summary>
    public class GrossProfitReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string refNo = context.Request.QueryString["refNo"];

            string cardNo = context.Request.QueryString["cardNo"];

            int brandId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["bid"]) || !int.TryParse(context.Request.QueryString["bid"], out brandId))
                brandId = 0;

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
                    case "StockId":
                        sortDataField = "st.StockId";
                        break;
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                    case "InBala":
                        sortDataField = "inBala.InBala";
                        break;
                    case "OutBala":
                        sortDataField = "outBala.OutBala";
                        break;
                    case "SIBala":
                        sortDataField = "siBala.SIBala";
                        break;
                    case "ProfitBala":
                        sortDataField = "outBala.OutBala-inBala.InBala-siBala.SIBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "inBala.CurrencyName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "StockDate":
                        sortDataField = "st.StockDate";
                        break;
                    case "BrandName":
                        sortDataField = "bra.BrandName";
                        break;
                    case "DPName":
                        sortDataField = "dp.DPName";
                        break;
                    case "CardNo":
                        sortDataField = "st.CardNo";
                        break;
                    case "InBalaOutCorpName":
                        sortDataField = "inBala.OutCorpName";
                        break;
                    case "InBalaInCorpName":
                        sortDataField = "inBala.InCorpName";
                        break;
                    case "OutBalaInCorpName":
                        sortDataField = "outBala.InCorpName";
                        break;
                    case "OutBalaOutCorpName":
                        sortDataField = "outBala.OutCorpName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Invoice.BLL.BusinessInvoiceDetailBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceDetailBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetReportSelect(pageIndex, pageSize, orderStr, refNo, brandId, cardNo);
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