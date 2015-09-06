using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PaymentVirtualConfirmHandler 的摘要说明
    /// </summary>
    public class PaymentVirtualConfirmHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string paymentVirtualStr = context.Request.Form["PaymentVirtual"];
            if (string.IsNullOrEmpty(paymentVirtualStr))
            {
                result.Message = "虚拟付款不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.PaymentVirtual paymentVirtual = serializer.Deserialize<NFMT.Funds.Model.PaymentVirtual>(paymentVirtualStr);

                NFMT.Funds.BLL.PaymentVirtualBLL bll = new NFMT.Funds.BLL.PaymentVirtualBLL();
                result = bll.PaymentVirtualConfirm(user, paymentVirtual);

                if (result.ResultStatus == 0)
                {
                    result.Message = "虚拟付款确认成功";
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