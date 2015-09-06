using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpAuthGroupInsertStatusHandler 的摘要说明
    /// </summary>
    public class EmpAuthGroupInsertStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            int authGroupId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["id"]) || !int.TryParse(context.Request.Form["id"], out authGroupId))
            {
                context.Response.Write("权限组序号错误");
                context.Response.End();
            }

            int empId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["empId"]) || !int.TryParse(context.Request.Form["empId"], out empId))
            {
                context.Response.Write("员工序号错误");
                context.Response.End();
            }

            NFMT.User.BLL.AuthGroupDetailBLL bll = new NFMT.User.BLL.AuthGroupDetailBLL();
            NFMT.Common.ResultModel result = bll.Insert(user, new NFMT.User.Model.AuthGroupDetail()
            {
                AuthGroupId = authGroupId,
                EmpId = empId,
                DetailStatus = NFMT.Common.StatusEnum.已生效
            });
            if (result.ResultStatus == 0)
                result.Message = "添加成功";
            else
                result.Message = "添加失败";

            context.Response.Write(result.Message);
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