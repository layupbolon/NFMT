using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptStatusHandler 的摘要说明
    /// </summary>
    public class DeptStatusHandler : IHttpHandler
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

            NFMT.User.BLL.DepartmentBLL bll = new NFMT.User.BLL.DepartmentBLL();
            NFMT.User.Model.Department dept = new NFMT.User.Model.Department()
            {
                DeptId = id,
                LastModifyId = user.EmpId
            };

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, dept);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.GoBack(user, dept);
                    break;
                case NFMT.Common.OperateEnum.冻结:
                    result = bll.Freeze(user, dept);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = bll.UnFreeze(user, dept);
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