using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// FuturesCodeUpdateHandler 的摘要说明
    /// </summary>
    public class FuturesCodeUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";
            DateTime firstTradeDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime lastTradeDate = NFMT.Common.DefaultValue.DefaultTime;
            
            decimal codeSize =Convert.ToDecimal(context.Request.Form["codeSize"]);
            string tradeCode = context.Request.Form["tradeCode"];
            int exchage = 0;
            int id = 0;
            int mU = 0;
            int currency = 0;
            int futuresCodeStatus = 0;
            int assetId = 0;

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

            if (!string.IsNullOrEmpty(context.Request.Form["firstTradeDate"]))
            {
                if (!DateTime.TryParse(context.Request.Form["firstTradeDate"], out firstTradeDate))
                {
                    resultStr = "交易起始日期不能为空";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            if (!string.IsNullOrEmpty(context.Request.Form["lastTradeDate"]))
            {
                if (!DateTime.TryParse(context.Request.Form["lastTradeDate"], out lastTradeDate))
                {
                    resultStr = "交易结束日期不能为空";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            if (!int.TryParse(context.Request.Form["exchage"], out exchage))
            {
                resultStr = "交易所序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["futuresCodeStatus"], out futuresCodeStatus))
            {
                resultStr = "合约状态序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["currency"], out currency))
            {
                resultStr = "币种序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["mU"], out mU))
            {
                resultStr = "计量单位序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(tradeCode))
            {
                resultStr = "交易代码不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["assetId"]) || !int.TryParse(context.Request.Form["assetId"], out assetId) || assetId <= 0)
            {
                resultStr = "品种不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.Data.BLL.FuturesCodeBLL bll = new NFMT.Data.BLL.FuturesCodeBLL();
            NFMT.Data.Model.FuturesCode futuresCode = new NFMT.Data.Model.FuturesCode()
            {
                FirstTradeDate = firstTradeDate,
                LastTradeDate = lastTradeDate,
                MUId = mU,
                CurrencyId = currency,
                FuturesCodeId = id,
                ExchageId = exchage,
                CodeSize = codeSize,
                TradeCode = tradeCode,
                AssetId = assetId,
                FuturesCodeStatus = (NFMT.Common.StatusEnum)futuresCodeStatus
            };
            NFMT.Common.ResultModel result = bll.Update(user, futuresCode);
            if (result.ResultStatus == 0)
                context.Response.Write("更新成功");
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