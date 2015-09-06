using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// CurrencyUpdateHandler 的摘要说明
    /// </summary>
    public class CurrencyUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string currencyName = context.Request.Form["currencyName"];
            string currencyFullName = context.Request.Form["currencyFullName"];
            string currencyShort = context.Request.Form["currencyShort"];
            int currencyStatus = 0;
            //string selValue = context.Request.Form["selValue"];
            int id = 0;

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


            if (!int.TryParse(context.Request.Form["currencyStatus"], out currencyStatus))
            {
                resultStr = "类型状态id为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            //if (string.IsNullOrEmpty(selValue))
            //{
            //    resultStr = "币种序号（currencyId）不能为空啊";
            //    context.Response.Write(resultStr);
            //}


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


            NFMT.Data.BLL.CurrencyBLL cyBLL = new NFMT.Data.BLL.CurrencyBLL();
            NFMT.Common.ResultModel result = cyBLL.Get(user, id);
            if (result.ResultStatus != 0)
            {
                resultStr = "获取数据错误";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            NFMT.Data.Model.Currency cy = result.ReturnValue as NFMT.Data.Model.Currency;
            if (cy != null)
            {
                cy.CurrencyName = currencyName;
                cy.CurrencyId =id;
                cy.CurrencyFullName = currencyFullName;
                cy.CurencyShort = currencyFullName;
                
                cy.Status = (NFMT.Common.StatusEnum)currencyStatus;

                result = cyBLL.Update(user, cy);
                resultStr = result.Message;
            }

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