using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockOutingListHandler 的摘要说明
    /// </summary>
    public class StockOutingListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int stockOutApplyId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["aid"]) || !int.TryParse(context.Request.QueryString["aid"], out stockOutApplyId))
            {
                context.Response.Write("出库申请信息错误");
                context.Response.End();
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
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                    case "StockWeight":
                        sortDataField = "sto.GrossAmount";
                        break;
                    case "StockStatusName":
                        sortDataField = "sto.StockStatus";
                        break;
                    case "CorpName":
                        sortDataField = "cor.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName ";
                        break;
                    case "BrandName":
                        sortDataField = "bra.BrandName";
                        break;
                    case "DetailId":
                        sortDataField = "soad.DetailId";
                        break;
                    case "ApplyWeight":
                        sortDataField = "soad.ApplyWeight";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            string sids = context.Request.QueryString["sids"];

            int stockOutId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["oid"]) || !int.TryParse(context.Request.QueryString["oid"], out stockOutId))
            {
                stockOutId = 0;
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.WareHouse.BLL.StockOutBLL stockOutBLL = new NFMT.WareHouse.BLL.StockOutBLL();

            NFMT.Common.SelectModel select = stockOutBLL.GetStockOutingSelect(pageIndex, pageSize, orderStr, stockOutApplyId,sids,stockOutId);
            NFMT.Common.ResultModel result = stockOutBLL.Load(user, select);

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