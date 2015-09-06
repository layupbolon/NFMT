using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ExchangeUpdateHandler 的摘要说明
    /// </summary>
    public class ExchangeUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string exchangeName = context.Request.Form["exchangeName"];
            string exchangeCode = context.Request.Form["exchangeCode"];
            int id = 0;
            int statusName = 0;

            string resultStr = "修改失败";

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out id))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["statusName"], out statusName))
            {
                resultStr = "交易所状态不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }


            if (string.IsNullOrEmpty(exchangeName))
            {
                resultStr = "交易所名称不可为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(exchangeCode))
            {
                resultStr = "交易所代码不可为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }


            NFMT.Data.BLL.ExchangeBLL ecBLL = new NFMT.Data.BLL.ExchangeBLL();
            NFMT.Data.Model.Exchange exchange = new NFMT.Data.Model.Exchange()
            {
                ExchangeName = exchangeName,
                ExchangeCode = exchangeCode,
                ExchangeId = id,
                ExchangeStatus = (NFMT.Common.StatusEnum)statusName
            };

            NFMT.Common.ResultModel result = ecBLL.Update(user, exchange);
            if (result.ResultStatus == 0)
                context.Response.Write("修改成功");
            else
                context.Response.Write(resultStr);
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