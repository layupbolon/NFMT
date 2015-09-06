using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpRoleAddHandler 的摘要说明
    /// </summary>
    public class EmpRoleAddHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int empId = 0;
            int roleId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                context.Response.Write("员工序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["id"], out empId) || empId <= 0)
            {
                context.Response.Write("员工序号错误");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["did"]))
            {
                context.Response.Write("角色序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["did"], out roleId) || roleId <= 0)
            {
                context.Response.Write("角色序号错误");
                context.Response.End();
            }

            NFMT.User.Model.EmpRole empRole = new NFMT.User.Model.EmpRole()
            {
                RoleId = roleId,
                EmpId = empId,
                RefStatus = NFMT.Common.StatusEnum.已生效,
                CreatorId = user.EmpId,
            };

            NFMT.User.BLL.EmpRoleBLL bll = new NFMT.User.BLL.EmpRoleBLL();
            NFMT.Common.ResultModel result = bll.Insert(user, empRole);

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