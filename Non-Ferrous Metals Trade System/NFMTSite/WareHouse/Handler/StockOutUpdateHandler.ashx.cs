using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockOutUpdateHandler 的摘要说明
    /// </summary>
    public class StockOutUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int stockOutId = 0;
            string sids = string.Empty;

            if (string.IsNullOrEmpty(context.Request.Form["stockOutId"]))
            {
                result.Message = "出库信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["stockOutId"], out stockOutId))
            {
                result.Message = "出库信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["sids"]))
            {
                result.Message = "未选中任务库存";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            sids = context.Request.Form["sids"].Trim();

            char[] cs = new char[1];
            cs[0] = ',';
            string[] ids = sids.Split(cs, StringSplitOptions.RemoveEmptyEntries);
            List<int> detailIds = new List<int>();
            foreach (string id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    result.Message = "库存信息错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
                int detailId = 0;
                if (!int.TryParse(id, out detailId))
                {
                    result.Message = "库存信息错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                detailIds.Add(detailId);
            }

            string memo = context.Request.Form["memo"];

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.WareHouse.BLL.StockOutBLL bll = new NFMT.WareHouse.BLL.StockOutBLL();
            result = bll.UpdateStockOut(user, stockOutId, detailIds, memo);

            if (result.ResultStatus == 0)
                result.Message = "出库修改成功";

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