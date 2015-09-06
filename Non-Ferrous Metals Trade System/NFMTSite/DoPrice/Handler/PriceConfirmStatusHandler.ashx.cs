using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PriceConfirmStatusHandler 的摘要说明
    /// </summary>
    public class PriceConfirmStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int priceConfirmId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out priceConfirmId) || priceConfirmId <= 0)
            {
                result.Message = "点价序号错误";
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

            NFMT.DoPrice.BLL.PriceConfirmBLL bll = new NFMT.DoPrice.BLL.PriceConfirmBLL();
            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.GoBack(user, priceConfirmId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, priceConfirmId);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = bll.Complete(user, priceConfirmId);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = bll.CompleteCancel(user, priceConfirmId);
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