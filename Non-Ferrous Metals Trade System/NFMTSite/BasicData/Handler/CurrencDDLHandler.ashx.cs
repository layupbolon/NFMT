using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// CurrencDDLHandler 的摘要说明
    /// </summary>
    public class CurrencDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            //NFMT.Data.BLL.CurrencyBLL bll = new NFMT.Data.BLL.CurrencyBLL();
            //var result = bll.Load(user);

            //context.Response.ContentType = "application/json; charset=utf-8";
            //if (result.ResultStatus != 0)
            //{
            //    context.Response.Write(result.Message);
            //    context.Response.End();
            //}

            //List<NFMT.Data.Model.Currency> dt = result.ReturnValue as List<NFMT.Data.Model.Currency>;

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(NFMT.Data.BasicDataProvider.Currencies.Where(a => a.CurrencyStatus == NFMT.Common.StatusEnum.已生效));
            context.Response.Write(jsonStr);
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