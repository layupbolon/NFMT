using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockSaleCanReceiptListHandler 的摘要说明
    /// </summary>
    public class StockSaleCanReceiptListHandler : IHttpHandler
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
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                    case "StockWeight":
                        sortDataField = "sto.NetAmount";
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
                        sortDataField = "sod.DetailId";
                        break;
                    case "ReceiptAmount":
                        sortDataField = "sr.SumAmount";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            int subId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["si"]) || !int.TryParse(context.Request.QueryString["si"], out subId))
                subId = 0;
            int receiptId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ri"]) || !int.TryParse(context.Request.QueryString["ri"], out receiptId))
                receiptId = 0;

            string sids = context.Request.QueryString["sids"];
            if (string.IsNullOrEmpty(sids))
                sids = "0";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.WareHouse.BLL.StockReceiptBLL bll = new NFMT.WareHouse.BLL.StockReceiptBLL();

            NFMT.Common.SelectModel select = bll.GetSaleCanReceiptStockListSelect(pageIndex, pageSize, orderStr, subId, sids, receiptId);
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