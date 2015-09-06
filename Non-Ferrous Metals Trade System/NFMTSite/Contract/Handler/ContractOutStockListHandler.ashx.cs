using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractOutStockListHandler 的摘要说明
    /// </summary>
    public class ContractOutStockListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string stockName = context.Request.QueryString["stockName"];
            DateTime fromDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime toDate = NFMT.Common.DefaultValue.DefaultTime;

            if (!string.IsNullOrEmpty(context.Request.QueryString["fromDate"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["fromDate"], out fromDate))
                    fromDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["toDate"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["toDate"], out toDate))
                    toDate = NFMT.Common.DefaultValue.DefaultTime;
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
                    case "CustomsTypeName":
                        sortDataField = "sto.CustomsType";
                        break;
                    default:
                        sortDataField = "sto.StockId";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.WareHouse.BLL.StockLogBLL bll = new NFMT.WareHouse.BLL.StockLogBLL();
            NFMT.Common.SelectModel select = bll.GetContractOutStockSelect(pageIndex, pageSize, orderStr, stockName, fromDate, toDate);
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