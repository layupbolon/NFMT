using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayContractAllotStockUpdateHandler 的摘要说明
    /// </summary>
    public class PayContractAllotStockUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string itemStr = context.Request.Form["item"];
            if (string.IsNullOrEmpty(itemStr))
            {
                context.Response.Write("修改信息不能为空");
                context.Response.End();
            }
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Funds.Model.PaymentStockDetail detail = serializer.Deserialize<NFMT.Funds.Model.PaymentStockDetail>(itemStr);

                NFMT.Funds.BLL.PaymentStockDetailBLL bll = new NFMT.Funds.BLL.PaymentStockDetailBLL();
                result = bll.Update(user, detail);

                if (result.ResultStatus == 0)
                    result.Message = "修改成功";
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            context.Response.End();
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