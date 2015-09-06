using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PaymentInvoiceCreateHandler 的摘要说明
    /// </summary>
    public class PaymentInvoiceCreateHandler : IHttpHandler
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
            int payApplyId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["PayApplyId"]) || !int.TryParse(context.Request.Form["PayApplyId"], out payApplyId))
            {
                context.Response.Write("财务付款对应申请不能为空");
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
                result = bll.PaymentInvoiceCreate(user, payment, details,payApplyId);

                if (result.ResultStatus == 0)
                    result.Message = "付款申请添加成功";
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