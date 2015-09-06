using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// InvoiceFundsCreateHandler 的摘要说明
    /// </summary>
    public class InvoiceFundsCreateHandler : IHttpHandler
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

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Invoice invoice = serializer.Deserialize<NFMT.Operate.Model.Invoice>(invoiceStr);
                NFMT.Invoice.Model.FinanceInvoice fundsInvoice = serializer.Deserialize<NFMT.Invoice.Model.FinanceInvoice>(invoiceFunds);

                NFMT.Invoice.BLL.FinanceInvoiceBLL bll = new NFMT.Invoice.BLL.FinanceInvoiceBLL();
                result = bll.Create(user, invoice, fundsInvoice);

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