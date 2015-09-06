using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// FinanceInvoicebyInvApplyCreateHandler 的摘要说明
    /// </summary>
    public class FinanceInvoicebyInvApplyCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string invoiceStr = context.Request.Form["Invoice"];
            if (string.IsNullOrEmpty(invoiceStr))
            {
                context.Response.Write("发票不能为空");
                context.Response.End();
            }

            string invoiceFunds = context.Request.Form["InvoiceFunds"];
            if (string.IsNullOrEmpty(invoiceFunds))
            {
                context.Response.Write("发票不能为空");
                context.Response.End();
            }

            string iids = context.Request.Form["iids"];

            int invoiceApplyId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["invApplyId"]) || !int.TryParse(context.Request.Form["invApplyId"], out invoiceApplyId) || invoiceApplyId <= 0)
            {
                context.Response.Write("参数错误");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Invoice invoice = serializer.Deserialize<NFMT.Operate.Model.Invoice>(invoiceStr);
                NFMT.Invoice.Model.FinanceInvoice fundsInvoice = serializer.Deserialize<NFMT.Invoice.Model.FinanceInvoice>(invoiceFunds);

                fundsInvoice.VATRatio = fundsInvoice.VATRatio / 100;

                NFMT.Invoice.BLL.FinanceInvoiceBLL bll = new NFMT.Invoice.BLL.FinanceInvoiceBLL();
                result = bll.CreateByInvApply(user, invoice, fundsInvoice, iids, invoiceApplyId);

                if (result.ResultStatus == 0)
                {
                    result.Message = "财务发票新增成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

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