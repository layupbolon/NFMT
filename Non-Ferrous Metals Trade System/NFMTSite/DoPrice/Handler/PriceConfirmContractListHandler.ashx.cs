using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PriceConfirmContractListHandler 的摘要说明
    /// </summary>
    public class PriceConfirmContractListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string subNo = context.Request.QueryString["sn"];

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
                    case "ContractDate":
                        sortDataField = string.Format("cs.{0}", sortDataField);
                        break;
                    case "ContractNo":
                        sortDataField = string.Format("c.{0}", sortDataField);
                        break;
                    case "SubNo":
                        sortDataField = "cs.SubNo";
                        break;
                    case "TradeDirectionName":
                        sortDataField = "c.TradeDirection";
                        break;
                    case "InCorpName":
                        sortDataField = "inccd.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "outccd.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "a.AssetName";
                        break;
                    case "SignWeight":
                        sortDataField = "cs.SignAmount";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.DoPrice.BLL.PriceConfirmBLL bll = new NFMT.DoPrice.BLL.PriceConfirmBLL();
            NFMT.Common.SelectModel select = bll.GetPriceConfirmContractListSelect(pageIndex, pageSize, orderStr, subNo);
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