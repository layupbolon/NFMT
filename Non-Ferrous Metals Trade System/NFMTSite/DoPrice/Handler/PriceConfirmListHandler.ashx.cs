using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PriceConfirmListHandler 的摘要说明
    /// </summary>
    public class PriceConfirmListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string subNo = context.Request.QueryString["sn"];
            int status = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["s"]))
            {
                if (!int.TryParse(context.Request.QueryString["s"], out status))
                    status = 0;
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
                    case "PriceConfirmId":
                        sortDataField = string.Format("pc.{0}", sortDataField);
                        break;
                    case "InCorpName":
                        sortDataField = "corpIn.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "corpOut.CorpName";
                        break;
                    case "SubNo":
                        sortDataField = "sub.OutContractNo";
                        break;
                    case "ContractAmount":
                        sortDataField = "pc.ContractAmount";
                        break;
                    case "SubAmount":
                        sortDataField = "pc.SubAmount";
                        break;
                    case "RealityAmount":
                        sortDataField = "pc.RealityAmount";
                        break;
                    case "PricingAvg":
                        sortDataField = "pc.PricingAvg";
                        break;
                    case "PremiumAvg":
                        sortDataField = "pc.PremiumAvg";
                        break;
                    case "InterestBala":
                        sortDataField = "pc.InterestBala";
                        break;
                    case "InterestAvg":
                        sortDataField = "pc.InterestAvg";
                        break;
                    case "SettlePrice":
                        sortDataField = "pc.SettlePrice";
                        break;
                    case "SettleBala":
                        sortDataField = "pc.SettleBala";
                        break;
                    case "PricingDate":
                        sortDataField = "pc.PricingDate";
                        break;
                    case "TakeCorpName":
                        sortDataField = "corpTake.CorpName";
                        break;
                    case "ContactPerson":
                        sortDataField = "pc.ContactPerson";
                        break;
                    case "Memo":
                        sortDataField = "pc.Memo";
                        break;
                    case "StatusName":
                        sortDataField = "bd.StatusName";
                        break;
                    default: break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.DoPrice.BLL.PriceConfirmBLL bll = new NFMT.DoPrice.BLL.PriceConfirmBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, subNo, status);
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