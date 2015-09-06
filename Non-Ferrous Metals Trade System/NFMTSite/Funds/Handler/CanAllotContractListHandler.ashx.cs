using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CanAllotContractListHandler 的摘要说明
    /// </summary>
    public class CanAllotContractListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int outerCorp = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty;

            string contractNo = context.Request["cno"];

            DateTime beginDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (!string.IsNullOrEmpty(context.Request["fd"]))
            {
                if (!DateTime.TryParse(context.Request["fd"], out beginDate))
                    beginDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request["td"]))
            {
                if (!DateTime.TryParse(context.Request["td"], out endDate))
                    endDate = NFMT.Common.DefaultValue.DefaultTime;
                else
                    endDate.AddDays(1);
            }
            if (!string.IsNullOrEmpty(context.Request["ci"]))
                int.TryParse(context.Request["ci"], out outerCorp);

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
                        sortDataField = "c.ContractId";
                        break;
                    case "ContractDate":
                        sortDataField = "cs.ContractDate";
                        break;
                    case "ContractNo":
                        sortDataField = "cs.SubNo";
                        break;
                    case "OutContractNo":
                        sortDataField = "cs.OutContractNo";
                        break;
                    case "TradeDirectionName":
                        sortDataField = "c.TradeDirection";
                        break;
                    case "InCorpName":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "c.AssetId";
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
            NFMT.Funds.BLL.ContractReceivableBLL bll = new NFMT.Funds.BLL.ContractReceivableBLL();
            NFMT.Common.SelectModel select = bll.GetCanAllotListSelect(pageIndex, pageSize, orderStr, contractNo, outerCorp, beginDate, endDate);
            NFMT.Common.ResultModel result = bll.Load(user, select);

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