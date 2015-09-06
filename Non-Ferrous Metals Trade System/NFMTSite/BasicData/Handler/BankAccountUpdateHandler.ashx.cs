using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankAccountUpdateHandler 的摘要说明
    /// </summary>
    public class BankAccountUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string accountNo = context.Request.Form["accountNo"];
            string bankAccDesc = context.Request.Form["bankAccDesc"];
            string resultStr = "修改失败";

            int currencyId = 0;
            int bankId = 0;
            int companyId = 0;
            int id = 0;
            int status = 0;

            if (string.IsNullOrEmpty(accountNo))
            {
                resultStr = "账户号码不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["currencyId"], out currencyId))
            {
                resultStr = "币种不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["bankId"], out bankId))
            {
                resultStr = "银行不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["companyId"], out companyId))
            {
                resultStr = "公司不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["id"], out id))
            {
                resultStr = "页面id为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["status"], out status))
            {
                resultStr = "状态出错";
                context.Response.Write(resultStr);
                context.Response.End();
            }


            NFMT.Data.BLL.BankAccountBLL bll = new NFMT.Data.BLL.BankAccountBLL();
            NFMT.Data.Model.BankAccount ba = new NFMT.Data.Model.BankAccount();
            ba.BankAccId = id;
            ba.CompanyId = companyId;
            ba.BankId = bankId;
            ba.CurrencyId = currencyId;
            ba.BankAccDesc = bankAccDesc;
            ba.AccountNo = accountNo;
            ba.BankAccStatus = (NFMT.Common.StatusEnum)status;

            NFMT.Common.ResultModel result = bll.Update(user, ba);
            resultStr = result.Message;

            context.Response.Write(resultStr);
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