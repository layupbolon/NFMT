using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInCanSelectContractListHandler 的摘要说明
    /// </summary>
    public class StockInCanSelectContractListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

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
                    case "StockOutId":
                        sortDataField = "so.StockOutId";
                        break;
                    case "StockOutTime":
                        sortDataField = "so.StockOutTime";
                        break;
                    case "SubNo":
                        sortDataField = "cs.SubNo";
                        break;
                    case "ApplyNo":
                        sortDataField = "app.ApplyNo";
                        break;
                    case "InCorpName":
                        sortDataField = "inCorp.CorpName ";
                        break;
                    case "OutCorpName":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "OutWeight":
                        sortDataField = "od.SumWeight";
                        break;
                    case "ExecutorName":
                        sortDataField = "emp.Name";
                        break;
                    case "StatusName":
                        sortDataField = "so.StockOutStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            int subId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["id"]) || !int.TryParse(context.Request.QueryString["id"], out subId))
                subId = 0;
            int stockInId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["stockId"]) || !int.TryParse(context.Request.QueryString["stockId"], out stockInId))
                stockInId = 0;
            
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.WareHouse.BLL.StockInBLL stockInBLL = new NFMT.WareHouse.BLL.StockInBLL();
            NFMT.Common.ResultModel result = stockInBLL.Get(user, stockInId);
            int corpId = 0;
            int assetId = 0;
            int customsType = 0;
            if (result.ResultStatus == 0)
            {
                NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                if (stockIn != null && stockIn.StockInId > 0)
                {
                    corpId = stockIn.CorpId;
                    assetId = stockIn.AssetId;
                    customsType = stockIn.CustomType;
                }
            }

            NFMT.WareHouse.BLL.ContractStockIn_BLL bll = new NFMT.WareHouse.BLL.ContractStockIn_BLL();
            NFMT.Common.SelectModel select = bll.GetStockInCanSelect(pageIndex, pageSize, orderStr, subId,assetId,corpId,customsType);
            NFMT.Common.IAuthority auth = new NFMT.Authority.ContractAuth();
            result = bll.Load(user, select,auth);

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