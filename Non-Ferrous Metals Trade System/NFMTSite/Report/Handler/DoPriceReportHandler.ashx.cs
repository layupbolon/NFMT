using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// DoPriceReportHandler 的摘要说明
    /// </summary>
    public class DoPriceReportHandler : IHttpHandler
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

            string contractNo = context.Request["c"];

            int assetId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["a"]) || !int.TryParse(context.Request.QueryString["a"], out assetId))
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
                    case "PricingId":
                        sortDataField = "p.PricingId";
                        break;
                    case "ContractNo":
                        sortDataField = "con.ContractNo";
                        break;
                    case "OutContractNo":
                        sortDataField = "con.OutContractNo";
                        break;
                    case "CorpName":
                        sortDataField = "CustCorp.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "asset.AssetName";
                        break;
                    case "TradeCode":
                        sortDataField = "fc.TradeCode";
                        break;
                    case "ExchangeCode":
                        sortDataField = "ex.ExchangeCode";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "TurnoverHand":
                        sortDataField = "p.PricingWeight";
                        break;
                    case "Turnover":
                        sortDataField = "p.PricingWeight";
                        break;
                    case "PricingStyle":
                        sortDataField = "bdPricingStyle.DetailName";
                        break;
                    case "SpotQP":
                        sortDataField = "p.SpotQP";
                        break;
                    case "Spread":
                        sortDataField = "p.Spread";
                        break;
                    case "Premium":
                        sortDataField = "con.Premium";
                        break;
                    case "DelayFee":
                        sortDataField = "p.DelayFee";
                        break;
                    case "OtherFee":
                        sortDataField = "p.OtherFee";
                        break;
                    case "FinalPrice":
                        sortDataField = "p.FinalPrice";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.DoPrice.BLL.PricingBLL bll = new NFMT.DoPrice.BLL.PricingBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetDoPriceReportSelect(pageIndex, pageSize, orderStr, contractNo, assetId, startDate, endDate);
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