using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PricingStatusHandler 的摘要说明
    /// </summary>
    public class PricingStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int pricingId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out pricingId) || pricingId <= 0)
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

            NFMT.DoPrice.BLL.PricingBLL pricingBLL = new NFMT.DoPrice.BLL.PricingBLL();
            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = pricingBLL.GoBack(user, pricingId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = pricingBLL.Invalid(user, pricingId);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = pricingBLL.Complete(user, pricingId);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = pricingBLL.CompleteCancel(user, pricingId);
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