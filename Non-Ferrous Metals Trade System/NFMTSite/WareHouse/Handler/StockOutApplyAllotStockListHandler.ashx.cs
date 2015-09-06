using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockOutApplyAllotStockListHandler 的摘要说明
    /// </summary>
    public class StockOutApplyAllotStockListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int stockOutApplyId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["id"]))
            {
                context.Response.Write("子合约序号未知");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.QueryString["id"], out stockOutApplyId) || stockOutApplyId <= 0)
            {
                context.Response.Write("子合约序号错误");
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
                    case "StockName":
                        sortDataField = string.Format("sn.{0}", sortDataField);
                        break;
                    case "StockWeight":
                        sortDataField = "s.GrossAmount";
                        break;
                    case "StatusName":
                        sortDataField = "sd.StatusName";
                        break;
                    case "CorpName":
                        sortDataField = "cor.CorpName";
                        break;
                    case "AssetName":
                        sortDataField = "ass.AssetName";
                        break;
                    case "BrandName":
                        sortDataField = "bra.BrandName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.WareHouse.BLL.StockOutApplyBLL stockOutApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
            NFMT.Common.SelectModel select = stockOutApplyBLL.GetAllotStockListByApplyIdSelect(pageIndex, pageSize, orderStr, stockOutApplyId);
            NFMT.Common.ResultModel result = stockOutApplyBLL.Load(new NFMT.Common.UserModel(), select);

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