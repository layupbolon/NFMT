using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// FundsInvoiceStatusHandler 的摘要说明
    /// </summary>
    public class FundsInvoiceStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int fundsInvoiceId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["ii"], out fundsInvoiceId) || fundsInvoiceId <= 0)
            {
                context.Response.Write("发票序号错误");
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

            NFMT.Invoice.BLL.FinanceInvoiceBLL bll = new NFMT.Invoice.BLL.FinanceInvoiceBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.Goback(user, fundsInvoiceId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, fundsInvoiceId);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = bll.Complete(user, fundsInvoiceId);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = bll.CompleteCancel(user, fundsInvoiceId);
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