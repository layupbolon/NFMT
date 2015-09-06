using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ExchangeCreateHandler 的摘要说明
    /// </summary>
    public class ExchangeCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string exchangeName = context.Request.Form["exchangeName"];
            string exchangeCode = context.Request.Form["exchangeCode"];
            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(exchangeName))
            {
                resultStr = "交易所名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(exchangeCode))
            {
                resultStr = "交易所代码不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            NFMT.Data.BLL.ExchangeBLL ecBLL = new NFMT.Data.BLL.ExchangeBLL();
            NFMT.Data.Model.Exchange ec = new NFMT.Data.Model.Exchange();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            ec.ExchangeName = exchangeName;
            ec.ExchangeCode = exchangeCode;


            ec.ExchangeStatus = NFMT.Common.StatusEnum.已录入;


            NFMT.Common.ResultModel result = ecBLL.Insert(user, ec);
            resultStr = result.Message;
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