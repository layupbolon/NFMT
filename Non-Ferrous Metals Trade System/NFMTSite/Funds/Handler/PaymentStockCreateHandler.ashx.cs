using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PaymentStockCreateHandler 的摘要说明
    /// </summary>
    public class PaymentStockCreateHandler : IHttpHandler
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

            string detailStr = context.Request.Form["Detail"];
            if (string.IsNullOrEmpty(detailStr))
            {
                result.Message = "财务付款明细不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int payApplyId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["PayApplyId"]) || !int.TryParse(context.Request.Form["PayApplyId"], out payApplyId))
            {
                result.Message = "财务付款对应申请不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }              

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.Payment payment = serializer.Deserialize<NFMT.Funds.Model.Payment>(paymentyStr);

             
                List<NFMT.Funds.Model.PaymentStockDetail> details = new List<NFMT.Funds.Model.PaymentStockDetail>();
                details = serializer.Deserialize<List<NFMT.Funds.Model.PaymentStockDetail>>(detailStr);

                NFMT.Funds.BLL.PaymentBLL bll = new NFMT.Funds.BLL.PaymentBLL();
                result = bll.PaymentStockCreate(user, payment, details, payApplyId);

                if (result.ResultStatus == 0)
                {
                    result.Message = "付款添加成功";
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