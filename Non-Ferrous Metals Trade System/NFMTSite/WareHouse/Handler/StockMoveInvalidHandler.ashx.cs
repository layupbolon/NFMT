using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockMoveInvalidHandler 的摘要说明
    /// </summary>
    public class StockMoveInvalidHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int stockMoveId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out stockMoveId))
                {
                    result.Message = "参数错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
            }

            NFMT.WareHouse.Model.StockMove stockMove = new NFMT.WareHouse.Model.StockMove()
            {
                StockMoveId = stockMoveId
            };

            NFMT.WareHouse.BLL.StockMoveBLL bll = new NFMT.WareHouse.BLL.StockMoveBLL();
            result = bll.Invalid(user, stockMove);
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