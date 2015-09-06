using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockMoveStatusHandler 的摘要说明
    /// </summary>
    public class StockMoveStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";

            int stockMoveId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out stockMoveId) || stockMoveId <= 0)
            {
                result.Message = "报关序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                result.Message = "操作错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;

            NFMT.WareHouse.BLL.StockMoveBLL bll = new NFMT.WareHouse.BLL.StockMoveBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.GoBack(user, stockMoveId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, stockMoveId);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = bll.Complete(user, stockMoveId);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = bll.CompleteCancel(user, stockMoveId);
                    break;
                case NFMT.Common.OperateEnum.关闭:
                    result = bll.Close(user, stockMoveId);
                    break;
            }

            if (result.ResultStatus == 0)
                result.Message = string.Format("{0}成功", operateEnum.ToString());

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