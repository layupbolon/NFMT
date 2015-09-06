using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankAccountCreateHandler 的摘要说明
    /// </summary>
    public class BankAccountCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string bankAccountStr = context.Request.Form["bankAccount"];
            
            if (string.IsNullOrEmpty(bankAccountStr))
            {
                context.Response.Write("银行账号为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Data.Model.BankAccount bankAccount = serializer.Deserialize<NFMT.Data.Model.BankAccount>(bankAccountStr);
                if (bankAccount == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.Data.BLL.BankAccountBLL bll = new NFMT.Data.BLL.BankAccountBLL();
                result = bll.Insert(user, bankAccount);
                if (result.ResultStatus == 0)
                {
                    result.Message = "新增成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(result.Message);
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