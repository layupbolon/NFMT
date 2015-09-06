using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// RoleCreateHandler 的摘要说明
    /// </summary>
    public class RoleCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            string roleName = context.Request.Form["roleName"];

            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(roleName))
            {
                resultStr = "角色名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.User.Model.Role role = new NFMT.User.Model.Role()
            {
                 RoleName = roleName
            };

            NFMT.User.BLL.RoleBLL roleBLL = new NFMT.User.BLL.RoleBLL();
            var result = roleBLL.Insert(user, role);
            resultStr = result.Message;

            context.Response.Write(resultStr);
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