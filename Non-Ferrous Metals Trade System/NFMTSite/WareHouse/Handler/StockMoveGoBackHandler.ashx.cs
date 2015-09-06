using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockMoveGoBackHandler 的摘要说明
    /// </summary>
    public class StockMoveGoBackHandler : IHttpHandler
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

            NFMT.WareHouse.BLL.StockMoveBLL bll = new NFMT.WareHouse.BLL.StockMoveBLL();
            result = bll.Get(user, stockMoveId);
            if (result.ResultStatus != 0)
            {
                result.Message = "获取数据错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.WareHouse.Model.StockMove stockMove = result.ReturnValue as NFMT.WareHouse.Model.StockMove;
            result = bll.GoBack(user, stockMove);
            if (result.ResultStatus == 0)
                result.Message = "撤返成功";

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