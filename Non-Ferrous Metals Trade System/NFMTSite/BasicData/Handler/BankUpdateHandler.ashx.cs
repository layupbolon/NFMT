using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankUpdateHandler 的摘要说明
    /// </summary>
    public class BankUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            context.Response.ContentType = "text/plain";

            string bankStr = context.Request.Form["bank"];
            if (string.IsNullOrEmpty(bankStr))
            {
                context.Response.Write("银行信息为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Data.Model.Bank bank = serializer.Deserialize<NFMT.Data.Model.Bank>(bankStr);
                if (bank == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.Data.BLL.BankBLL bll = new NFMT.Data.BLL.BankBLL();
                result = bll.Update(user, bank);
                if (result.ResultStatus == 0)
                {
                    result.Message = "修改成功";
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