using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds
{
    /// <summary>
    /// CashInAllotMainCreateContractListHandler 的摘要说明
    /// </summary>
    public class CashInAllotMainCreateContractListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int status = -1, outerCorp = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty;

            string contractNo = context.Request["cn"];

            DateTime beginDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (!string.IsNullOrEmpty(context.Request["db"]))
            {
                if (!DateTime.TryParse(context.Request["db"], out beginDate))
                    beginDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request["de"]))
            {
                endDate = !DateTime.TryParse(context.Request["de"], out endDate) ? NFMT.Common.DefaultValue.DefaultTime : endDate.AddDays(1);
            }

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);

            if (!string.IsNullOrEmpty(context.Request["ci"]))
                int.TryParse(context.Request["ci"], out outerCorp);//状态查询条件

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
                    case "ContractId":
                        sortDataField = "con.ContractId";
                        break;
                    case "ContractDate":
                        sortDataField = "cs.ContractDate";
                        break;
                    case "SubNo":
                        sortDataField = "cs.SubNo";
                        break;
                    case "OutContractNo":
                        sortDataField = "cs.OutContractNo";
                        break;
                    case "TradeDirectionName":
                        sortDataField = "con.TradeDirection";
                        break;
                    case "InCorpName":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "con.AssetId";
                        break;
                    case "ContractWeight":
                        sortDataField = "cs.SignAmount";
                        break;
                    case "PriceModeName":
                        sortDataField = "cs.PriceMode";
                        break;
                    case "StatusName":
                        sortDataField = "cs.SubStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Contract.BLL.ContractSubBLL subBLL = new NFMT.Contract.BLL.ContractSubBLL();
            NFMT.Common.SelectModel select = subBLL.GetListSelectForCashInAllot(pageIndex, pageSize, orderStr, contractNo, outerCorp, status, beginDate, endDate);
            NFMT.Common.ResultModel result = subBLL.Load(user, select);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("count", result.AffectCount);
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