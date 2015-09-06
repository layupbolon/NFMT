using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// InvoiceApplyUpdateHandler 的摘要说明
    /// </summary>
    public class InvoiceApplyUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string applyStr = context.Request.Form["apply"];
            if (string.IsNullOrEmpty(applyStr))
            {
                result.Message = "申请信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string invoiceApplyStr = context.Request.Form["invoiceApply"];
            if (string.IsNullOrEmpty(invoiceApplyStr))
            {
                result.Message = "发票申请信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                result.Message = "明细信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Operate.Model.Apply apply = serializer.Deserialize<NFMT.Operate.Model.Apply>(applyStr);
                NFMT.Invoice.Model.InvoiceApply invoiceApply = serializer.Deserialize<NFMT.Invoice.Model.InvoiceApply>(invoiceApplyStr);
                List<NFMT.Invoice.Model.InvoiceApplyDetail> details = serializer.Deserialize<List<NFMT.Invoice.Model.InvoiceApplyDetail>>(rowsStr);

                NFMT.Invoice.BLL.InvoiceApplyBLL bll = new NFMT.Invoice.BLL.InvoiceApplyBLL();
                result = bll.Update(user, apply, invoiceApply, details);

                if (result.ResultStatus == 0)
                {
                    result.Message = "修改成功";
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