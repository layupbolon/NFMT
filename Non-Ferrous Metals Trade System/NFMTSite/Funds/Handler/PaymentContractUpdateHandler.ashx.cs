using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PaymentContractUpdateHandler 的摘要说明
    /// </summary>
    public class PaymentContractUpdateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string paymentyStr = context.Request.Form["Payment"];
            if (string.IsNullOrEmpty(paymentyStr))
            {
                result.Message = "财务付款不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
           
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.Payment payment = serializer.Deserialize<NFMT.Funds.Model.Payment>(paymentyStr);

                NFMT.Funds.BLL.PaymentBLL bll = new NFMT.Funds.BLL.PaymentBLL();
                result = bll.PaymentContractUpdate(user, payment);

                if (result.ResultStatus == 0)
                {
                    result.Message = "付款修改成功";
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