using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockReceiptContractListHandler 的摘要说明
    /// </summary>
    public class StockReceiptContractListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string subNo = context.Request.QueryString["sn"];
            DateTime fromDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime toDate = NFMT.Common.DefaultValue.DefaultTime;
            int outCorpId = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["oci"]))
            {
                if (!int.TryParse(context.Request.QueryString["oci"], out outCorpId))
                    outCorpId = 0;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["fd"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["fd"], out fromDate))
                    fromDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["td"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["td"], out toDate))
                    toDate = NFMT.Common.DefaultValue.DefaultTime;
            }

            int tradeDir = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["dir"]))
                int.TryParse(context.Request.QueryString["dir"], out tradeDir);

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
                    case "InCorpName":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "a.AssetName";
                        break;
                    case "SignWeight":
                        sortDataField = "cs.SignAmount";
                        break;
                    case "SumWeight":
                        sortDataField = "sr.SumAmount";
                        break;                 
                    case "StatusName":
                        sortDataField = "sd.StatusName";
                        break;
                    case "OutContractNo":
                        sortDataField = "cs.OutContractNo";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.WareHouse.BLL.StockReceiptBLL bll = new NFMT.WareHouse.BLL.StockReceiptBLL();
            NFMT.Common.SelectModel select = bll.GetContractListSelect(pageIndex, pageSize, orderStr, subNo, outCorpId, fromDate, toDate, tradeDir);
            NFMT.Authority.ContractAuth auth = new NFMT.Authority.ContractAuth();
            NFMT.Common.ResultModel result = bll.Load(user, select,auth);

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