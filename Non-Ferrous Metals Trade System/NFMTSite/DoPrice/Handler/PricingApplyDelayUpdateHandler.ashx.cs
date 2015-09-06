using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PricingApplyDelayUpdateHandler 的摘要说明
    /// </summary>
    public class PricingApplyDelayUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string delayStr = context.Request.Form["delay"];
            if (string.IsNullOrEmpty(delayStr))
            {
                result.Message = "延期信息不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.DoPrice.Model.PricingApplyDelay delay = serializer.Deserialize<NFMT.DoPrice.Model.PricingApplyDelay>(delayStr);

                if (delay == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }

                NFMT.DoPrice.BLL.PricingApplyDelayBLL bll = new NFMT.DoPrice.BLL.PricingApplyDelayBLL();
                result = bll.Update(user, delay);
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