using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// LcStatusHandler 的摘要说明
    /// </summary>
    public class LcStatusHandler : IHttpHandler
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

            NFMT.Contract.BLL.LcBLL lcBLL = new NFMT.Contract.BLL.LcBLL();
            NFMT.Contract.Model.Lc lc = new NFMT.Contract.Model.Lc()
            {
                LastModifyId = user.EmpId,
                LcId = id
            };

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    result = lcBLL.Invalid(user, lc);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    result = lcBLL.GoBack(user, lc);
                    break;
                case NFMT.Common.OperateEnum.冻结:
                    result = lcBLL.Freeze(user, lc);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = lcBLL.UnFreeze(user, lc);
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