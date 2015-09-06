using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockMoveApplyCompleteHandler 的摘要说明
    /// </summary>
    public class StockMoveApplyCompleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int stockMoveApplyId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out stockMoveApplyId))
                {
                    result.Message = "参数错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
            }

            NFMT.WareHouse.BLL.StockMoveApplyBLL bll = new NFMT.WareHouse.BLL.StockMoveApplyBLL();
            result = bll.Get(user, stockMoveApplyId);
            if (result.ResultStatus != 0)
            {
                result.Message = "获取数据错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.WareHouse.Model.StockMoveApply stockMoveApply = result.ReturnValue as NFMT.WareHouse.Model.StockMoveApply;
            result = bll.Complete(user, stockMoveApply);
            if (result.ResultStatus == 0)
                result.Message = "完成成功";

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