using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// GetEmpIdByRoleIdHandler 的摘要说明
    /// </summary>
    public class GetEmpIdByRoleIdHandler : IHttpHandler
    {
        NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int roleId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["roleId"]))
                int.TryParse(context.Request.Form["roleId"], out roleId);

            try
            {
                NFMT.User.BLL.EmpRoleBLL empRoleBLL = new NFMT.User.BLL.EmpRoleBLL();
                result = empRoleBLL.GetEmpIdsByRoleId(user, roleId);
                if (result.ResultStatus != 0)
                {
                    context.Response.Write(result.Message);
                    context.Response.End();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result.ReturnValue);
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