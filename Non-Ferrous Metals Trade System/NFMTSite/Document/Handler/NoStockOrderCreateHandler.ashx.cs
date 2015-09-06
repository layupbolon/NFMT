using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Document.Handler
{
    /// <summary>
    /// NoStockOrderCreateHandler 的摘要说明
    /// </summary>
    public class NoStockOrderCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string orderStr = context.Request.Form["order"];
            string orderStockInvoiceStr = context.Request.Form["orderStockInvoice"];
            string orderDetailStr = context.Request.Form["orderDetail"];
            string isSubmitAuditStr = context.Request.Form["IsSubmitAudit"];

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            NFMT.Document.Model.DocumentOrder order = serializer.Deserialize<NFMT.Document.Model.DocumentOrder>(orderStr);
            List<NFMT.Document.Model.OrderStockInvoice> stockInvoices = serializer.Deserialize<List<NFMT.Document.Model.OrderStockInvoice>>(orderStockInvoiceStr);
            NFMT.Document.Model.DocumentOrderDetail detail = serializer.Deserialize<NFMT.Document.Model.DocumentOrderDetail>(orderDetailStr);

            bool isSubmitAudit = false;
            if (string.IsNullOrEmpty(isSubmitAuditStr) || !bool.TryParse(isSubmitAuditStr, out isSubmitAudit))
                isSubmitAudit = false;

            NFMT.Document.BLL.DocumentOrderBLL bll = new NFMT.Document.BLL.DocumentOrderBLL();
            result = bll.Create(user, order, stockInvoices, detail, isSubmitAudit);

            if (result.ResultStatus == 0)
                result.Message = "制单指令新增成功";

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            context.Response.Write(jsonStr);
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