using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// RateGreateHandler 的摘要说明
    /// </summary>
    public class RateGreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string reatDate = context.Request.Form["reatDate"];
            int currency1 = 0;
            string rateValue = context.Request.Form["rateValue"];
            int currency2 = 0;
            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(reatDate))
            {
                resultStr = "汇率日期不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["currency1"], out currency1))
            {
                resultStr = "币种1不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(rateValue))
            {
                resultStr = "汇率不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["currency2"], out currency2))
            {
                resultStr = "币种2不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.Data.BLL.RateBLL bll = new NFMT.Data.BLL.RateBLL();
            NFMT.Data.Model.Rate rate = new NFMT.Data.Model.Rate();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            //master.BankName = bankName;
            //master.ParentId = Convert.ToInt32(parentId);

            rate.CreateTime =Convert.ToDateTime(reatDate);
            rate.FromCurrencyId = Convert.ToInt32(currency1);
            rate.ToCurrencyId = Convert.ToInt32(currency2);
            rate.RateValue =Convert.ToDecimal(rateValue);

            rate.RateStatus = NFMT.Common.StatusEnum.已录入;


            NFMT.Common.ResultModel result = bll.Insert(user, rate);
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