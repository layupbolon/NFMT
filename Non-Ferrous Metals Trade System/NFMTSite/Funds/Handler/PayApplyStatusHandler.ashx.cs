using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayApplyStatusHandler 的摘要说明
    /// </summary>
    public class PayApplyStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int payApplyId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["pi"], out payApplyId) || payApplyId <= 0)
            {
                context.Response.Write("付款申请序号错误");
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

            NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.Goback(user, payApplyId);                    
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, payApplyId);
                    break;
                case NFMT.Common.OperateEnum.确认完成:
                    result = bll.Confirm(user, payApplyId);
                    break;
                case NFMT.Common.OperateEnum.确认完成撤销:
                    result = bll.ConfirmCancel(user, payApplyId);
                    break;
                case NFMT.Common.OperateEnum.关闭:
                    result = bll.Close(user, payApplyId);
                    break;
            }

            if (result.ResultStatus == 0)
                result.Message = string.Format("{0}成功",operateEnum.ToString());

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