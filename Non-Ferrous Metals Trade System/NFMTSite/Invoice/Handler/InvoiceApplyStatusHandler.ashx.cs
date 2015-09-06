using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// InvoiceApplyStatusHandler 的摘要说明
    /// </summary>
    public class InvoiceApplyStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";

            int invoiceApplyId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out invoiceApplyId) || invoiceApplyId <= 0)
            {
                result.Message = "发票申请序号错误";
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

            NFMT.Invoice.BLL.InvoiceApplyBLL bll = new NFMT.Invoice.BLL.InvoiceApplyBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.Goback(user, invoiceApplyId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, invoiceApplyId);
                    break;
                case NFMT.Common.OperateEnum.确认完成:
                    result = bll.Confirm(user, invoiceApplyId);
                    break;
                case NFMT.Common.OperateEnum.确认完成撤销:
                    result = bll.ConfirmCancel(user, invoiceApplyId);
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