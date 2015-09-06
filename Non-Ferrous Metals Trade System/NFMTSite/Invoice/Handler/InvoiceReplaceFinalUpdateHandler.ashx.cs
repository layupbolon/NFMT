using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// InvoiceReplaceFinalUpdateHandler 的摘要说明
    /// </summary>
    public class InvoiceReplaceFinalUpdateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string invoiceStr = context.Request.Form["Invoice"];
            if (string.IsNullOrEmpty(invoiceStr))
            {
                result.Message = "发票不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string invoiceBusinessStr = context.Request.Form["InvoiceBusiness"];
            if (string.IsNullOrEmpty(invoiceBusinessStr))
            {
                result.Message = "发票不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string detailStr = context.Request.Form["Details"];
            if (string.IsNullOrEmpty(detailStr))
            {
                result.Message = "发票明细不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                NFMT.Operate.Model.Invoice invoice = serializer.Deserialize<NFMT.Operate.Model.Invoice>(invoiceStr);
                NFMT.Invoice.Model.BusinessInvoice invoiceBusiness = serializer.Deserialize<NFMT.Invoice.Model.BusinessInvoice>(invoiceBusinessStr);
                List<NFMT.Invoice.Model.BusinessInvoiceDetail> details = new List<NFMT.Invoice.Model.BusinessInvoiceDetail>();
                details = serializer.Deserialize<List<NFMT.Invoice.Model.BusinessInvoiceDetail>>(detailStr);

                NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
                result = bll.UpdateReplaceFinal(user, invoice, invoiceBusiness, details);

                if (result.ResultStatus == 0)
                {
                    result.Message = "替临终票修改成功";
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