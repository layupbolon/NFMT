using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// FuturesCodeCreateHandler 的摘要说明
    /// </summary>
    public class FuturesCodeCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int codeSize = 0;
            string tradeCode = context.Request.Form["tradeCode"];
            int exchage = 0;
            DateTime firstTradeDate =Convert.ToDateTime(context.Request.Form["firstTradeDate"]);
            DateTime lastTradeDate = Convert.ToDateTime(context.Request.Form["lastTradeDate"]);
            int mU = 0;
            int currency = 0;
            int assetId = 0;

            string resultStr = "添加失败";
       
            if (string.IsNullOrEmpty(tradeCode))
            {
                resultStr = "交易代码不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["codeSize"], out codeSize))
            {
                resultStr = "交易规模不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["exchage"], out exchage))
            {
                resultStr = "交易所不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["mU"], out mU))
            {
                resultStr = "合约单位不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["currency"], out currency))
            {
                resultStr = "币种不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["assetId"]) || !int.TryParse(context.Request.Form["assetId"], out assetId) || assetId <= 0)
            {
                resultStr = "品种不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.Data.BLL.FuturesCodeBLL fcBLL = new NFMT.Data.BLL.FuturesCodeBLL();
            NFMT.Data.Model.FuturesCode fc = new NFMT.Data.Model.FuturesCode();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            fc.CodeSize = codeSize;
            fc.TradeCode = tradeCode;
            fc.FirstTradeDate = firstTradeDate;
            fc.LastTradeDate = lastTradeDate;
            fc.ExchageId = exchage;
            fc.MUId = mU;
            fc.AssetId = assetId;
            fc.CurrencyId = currency;

            fc.FuturesCodeStatus = NFMT.Common.StatusEnum.已录入;


            NFMT.Common.ResultModel result = fcBLL.Insert(user, fc);
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