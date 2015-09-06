using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInStockUpdateHandler 的摘要说明
    /// </summary>
    public class CashInStockUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string detailStr = context.Request.Form["Details"];
            if (string.IsNullOrEmpty(detailStr))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }

            int stockLogId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["StockLogId"]) || !int.TryParse(context.Request.Form["StockLogId"], out stockLogId) || stockLogId <= 0)
            {
                context.Response.Write("库存信息错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.Funds.Model.CashInStcok> stocks = serializer.Deserialize<List<NFMT.Funds.Model.CashInStcok>>(detailStr);
                if (stocks == null)
                {
                    context.Response.Write("分配信息错误");
                    context.Response.End();
                }

                NFMT.Funds.BLL.CashInStcokBLL bll = new NFMT.Funds.BLL.CashInStcokBLL();
                result = bll.UpdateStock(user, stocks, stockLogId);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            if (result.ResultStatus == 0)
                result.Message = "修改成功";

            context.Response.Write(result.Message);
            context.Response.End();
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