using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpRoleDeleteHandler 的摘要说明
    /// </summary>
    public class EmpRoleDeleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            int empRoleId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out empRoleId) || empRoleId <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            NFMT.User.Model.EmpRole empRole = new NFMT.User.Model.EmpRole()
            {
                  EmpRoleId = empRoleId,
                  RefStatus = NFMT.Common.StatusEnum.已录入
            };

            NFMT.User.BLL.EmpRoleBLL bll = new NFMT.User.BLL.EmpRoleBLL();
            NFMT.Common.ResultModel result = bll.Invalid(user, empRole);
            if (result.ResultStatus == 0)
                context.Response.Write("操作成功");
            else
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