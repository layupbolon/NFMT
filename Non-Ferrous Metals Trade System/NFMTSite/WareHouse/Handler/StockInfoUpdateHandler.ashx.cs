using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInfoUpdateHandler 的摘要说明
    /// </summary>
    public class StockInfoUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int stockId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockId"]) || !int.TryParse(context.Request.Form["stockId"], out stockId) || stockId <= 0)
            {
                result.Message = "库存序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string stockNo = context.Request.Form["stockNo"];
            string cardNo = context.Request.Form["cardNo"];
            if (string.IsNullOrEmpty(cardNo) && string.IsNullOrEmpty(stockNo))
            {
                result.Message = "卡号和提单号不能全为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string refNo = context.Request.Form["refNo"];
            if (string.IsNullOrEmpty(refNo))
            {
                result.Message = "业务单号不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int stockType = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockType"]) || !int.TryParse(context.Request.Form["stockType"], out stockType) || stockType <= 0)
            {
                result.Message = "单据类型错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int stockOperateType = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockOperateType"]) || !int.TryParse(context.Request.Form["stockOperateType"], out stockOperateType) || stockOperateType <= 0)
            {
                result.Message = "入库类型错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                NFMT.WareHouse.BLL.StockBLL stockBLL = new NFMT.WareHouse.BLL.StockBLL();
                result = stockBLL.UpdateStockInfo(user, stockId, cardNo, refNo, stockType, stockOperateType, stockNo);
                if (result.ResultStatus == 0)
                    result.Message = "操作成功";
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