using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInStatusHandler 的摘要说明
    /// </summary>
    public class CashInStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int cashInId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out cashInId) || cashInId <= 0)
            {
                context.Response.Write("收款登记序号错误");
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

            NFMT.Funds.BLL.CashInBLL bll = new NFMT.Funds.BLL.CashInBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.Goback(user, cashInId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, cashInId);
                    break;
                case NFMT.Common.OperateEnum.确认完成:
                    result = bll.Confirm(user, cashInId);
                    break;
                case NFMT.Common.OperateEnum.确认完成撤销:
                    result = bll.ConfirmCancel(user, cashInId);
                    break;
                //case NFMT.Common.OperateEnum.关闭:
                //    result = bll.Close(user, payApplyId);
                //    break;
            }

            if (result.ResultStatus == 0)
                result.Message = string.Format("{0}成功", operateEnum.ToString());

            context.Response.Write(result.Message);
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