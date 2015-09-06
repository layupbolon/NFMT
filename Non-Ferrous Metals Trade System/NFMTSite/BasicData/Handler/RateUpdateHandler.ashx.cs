using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// RateUpdateHandler 的摘要说明
    /// </summary>
    public class RateUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";

            string rateStr = context.Request.Form["rate"];

            if (string.IsNullOrEmpty(rateStr))
            {
                context.Response.Write("汇率不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Data.Model.Rate rate = serializer.Deserialize<NFMT.Data.Model.Rate>(rateStr);
                if (rate == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.Data.BLL.RateBLL rateBLL = new NFMT.Data.BLL.RateBLL();
                result = rateBLL.Update(user, rate);
                if (result.ResultStatus == 0)
                {
                    result.Message = "更新成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(result.Message);
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