using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// RoleUpdateHandler 的摘要说明
    /// </summary>
    public class RoleUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            string roleName = context.Request.Form["roleName"];

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

            if (string.IsNullOrEmpty(roleName))
            {
                resultStr = "角色名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.User.BLL.RoleBLL roleBLL = new NFMT.User.BLL.RoleBLL();
            NFMT.Common.ResultModel result = roleBLL.Get(user, id);
            if (result.ResultStatus != 0)
            {
                resultStr = "获取数据错误";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            NFMT.User.Model.Role role = result.ReturnValue as NFMT.User.Model.Role;
            if (role != null)
            {
                role.RoleName = roleName;

                result = roleBLL.Update(user, role);
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