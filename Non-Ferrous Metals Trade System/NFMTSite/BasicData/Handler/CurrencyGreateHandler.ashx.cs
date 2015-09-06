using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// CurrencyGreateHandler 的摘要说明
    /// </summary>
    public class CurrencyGreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string currencyName = context.Request.Form["currencyName"];
            string currencyFullName = context.Request.Form["currencyFullName"];
            string currencyShort = context.Request.Form["currencyShort"];
            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(currencyName))
            {
                resultStr = "币种名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(currencyFullName))
            {
                resultStr = "币种全称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(currencyShort))
            {
                resultStr = "币种缩写不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            //录入币种（add）
            NFMT.Data.BLL.CurrencyBLL bll = new NFMT.Data.BLL.CurrencyBLL();
            NFMT.Data.Model.Currency master = new NFMT.Data.Model.Currency();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            //master.CreatorId = user.EmpId;
            master.CurrencyName = currencyName;
            master.CurrencyFullName = currencyFullName;
            master.CurencyShort = currencyShort;
            master.CurrencyStatus = NFMT.Common.StatusEnum.已录入;


            NFMT.Common.ResultModel result = bll.Insert(user, master);
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