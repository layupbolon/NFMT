using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// PledgeStatusHandler 的摘要说明
    /// </summary>
    public class PledgeStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";

            int pledgeId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out pledgeId) || pledgeId <= 0)
            {
                result.Message = "质押序号错误";
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

            NFMT.WareHouse.BLL.PledgeBLL bll = new NFMT.WareHouse.BLL.PledgeBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.GoBack(user, pledgeId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, pledgeId);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = bll.Complete(user, pledgeId);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = bll.CompleteCancel(user, pledgeId);
                    break;
                case NFMT.Common.OperateEnum.关闭:
                    result = bll.Close(user, pledgeId);
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