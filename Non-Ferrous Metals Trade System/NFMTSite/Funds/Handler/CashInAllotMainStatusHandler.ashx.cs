using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInAllotMainStatusHandler 的摘要说明
    /// </summary>
    public class CashInAllotMainStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int allotId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out allotId) || allotId <= 0)
            {
                context.Response.Write("收款分配序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            NFMT.Funds.BLL.CashInAllotBLL bll = new NFMT.Funds.BLL.CashInAllotBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, allotId);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.Goback(user, allotId);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = bll.Complete_NewVersion(user, allotId);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = bll.CompleteCancel_NewVersion(user, allotId);
                    break;
                case NFMT.Common.OperateEnum.关闭:
                    result = bll.Close_NewVersion(user, allotId);
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