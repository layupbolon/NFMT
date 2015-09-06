using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PaymentInvoiceUpdateHandler 的摘要说明
    /// </summary>
    public class PaymentInvoiceUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            string paymentStr = context.Request.Form["Payment"];
            string detailStr = context.Request.Form["Details"];

            if (string.IsNullOrEmpty(paymentStr))
            {
                context.Response.Write("付款不能为空");
                context.Response.End();
            }

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.Payment payment = serializer.Deserialize<NFMT.Funds.Model.Payment>(paymentStr);

                List<NFMT.Funds.Model.PaymentInvioceDetail> details = new List<NFMT.Funds.Model.PaymentInvioceDetail>();
                details = serializer.Deserialize<List<NFMT.Funds.Model.PaymentInvioceDetail>>(detailStr);

                NFMT.Funds.BLL.PaymentBLL bll = new NFMT.Funds.BLL.PaymentBLL();
                result = bll.PaymentInvoiceUpdate(user, payment, details);

                if (result.ResultStatus == 0)
                    result.Message = "付款申请修改成功";
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