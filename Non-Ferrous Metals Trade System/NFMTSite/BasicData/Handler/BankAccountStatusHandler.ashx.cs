using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankAccountStatusHandler 的摘要说明
    /// </summary>
    public class BankAccountStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";
            int id = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Data.BLL.BankAccountBLL baBLL = new NFMT.Data.BLL.BankAccountBLL();
            NFMT.Data.Model.BankAccount ba = new NFMT.Data.Model.BankAccount()
            {
                BankAccId = id,
                LastModifyId = user.EmpId
            };

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.冻结:
                    result = baBLL.Freeze(user, ba);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = baBLL.UnFreeze(user, ba);
                    break;
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