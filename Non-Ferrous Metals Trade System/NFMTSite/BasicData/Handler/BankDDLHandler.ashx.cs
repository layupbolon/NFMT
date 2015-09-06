using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankDDLHandler (银行类型的绑定)
    /// </summary>
    public class BankDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            //NFMT.Data.BLL.BankBLL bank = new NFMT.Data.BLL.BankBLL();
            //var result = bank.Load(user);

            //context.Response.ContentType = "application/json; charset=utf-8";
            //if (result.ResultStatus != 0)
            //{
            //    context.Response.Write(result.Message);
            //    context.Response.End();
            //}

            //List<NFMT.Data.Model.Bank> dt = result.ReturnValue as List<NFMT.Data.Model.Bank>;

            string jsonStr = string.Empty;
            int bankId = 0;
            if (!string.IsNullOrEmpty(context.Request["b"]))
                int.TryParse(context.Request["b"], out bankId);

            if (bankId > 0)
                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(NFMT.Data.BasicDataProvider.Banks.Where(a => a.BankStatus == NFMT.Common.StatusEnum.已生效 && a.BankId == bankId));
            else
                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(NFMT.Data.BasicDataProvider.Banks.Where(a => a.BankStatus == NFMT.Common.StatusEnum.已生效));
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