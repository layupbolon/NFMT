using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockReceiptUpdateCreateHandler 的摘要说明
    /// </summary>
    public class StockReceiptUpdateCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int detailId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["detailId"]) || !int.TryParse(context.Request.Form["detailId"], out detailId) || detailId <= 0)
            {
                result.Message = "参数错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int stockId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockId"]) || !int.TryParse(context.Request.Form["stockId"], out stockId) || stockId <= 0)
            {
                result.Message = "参数错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int stockLogId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockLogId"]) || !int.TryParse(context.Request.Form["stockLogId"], out stockLogId) || stockLogId <= 0)
            {
                result.Message = "参数错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            decimal receiptAmount = 0;
            if (string.IsNullOrEmpty(context.Request.Form["receiptAmount"]) || !decimal.TryParse(context.Request.Form["receiptAmount"], out receiptAmount) || receiptAmount <= 0)
            {
                result.Message = "参数错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                NFMT.WareHouse.BLL.StockReceiptBLL bll = new NFMT.WareHouse.BLL.StockReceiptBLL();
                result = bll.StockReceiptUpdate(user, detailId, stockId, stockLogId, receiptAmount);
                if (result.ResultStatus == 0)
                {
                    result.Message = "库存回执修改成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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