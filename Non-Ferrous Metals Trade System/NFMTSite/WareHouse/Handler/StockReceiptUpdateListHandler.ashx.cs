using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockReceiptUpdateListHandler 的摘要说明
    /// </summary>
    public class StockReceiptUpdateListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string refNo = context.Request.QueryString["refno"];

            int receiptType = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["type"]) || !int.TryParse(context.Request.QueryString["type"], out receiptType))
                receiptType = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.WareHouse.BLL.StockReceiptBLL bll = new NFMT.WareHouse.BLL.StockReceiptBLL();
            NFMT.Common.SelectModel select = bll.GetStockReceiptUpdateListSelectModel(pageIndex, pageSize, orderStr, refNo, receiptType);
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