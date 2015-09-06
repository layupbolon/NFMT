using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockInInvalidHandler 的摘要说明
    /// </summary>
    public class StockInInvalidHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int stockInId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out stockInId))
                {
                    result.Message = "参数错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
            }

            NFMT.WareHouse.Model.StockIn stockIn = new NFMT.WareHouse.Model.StockIn()
            {
                StockInId = stockInId
            };

            NFMT.WareHouse.BLL.StockInBLL bll = new NFMT.WareHouse.BLL.StockInBLL();
            result = bll.Invalid(user, stockIn);
            if (result.ResultStatus == 0)
                result.Message = "作废成功";

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