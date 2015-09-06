using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MasterClauseDeleteHandler 的摘要说明
    /// </summary>
    public class MasterClauseDeleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int refId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out refId) || refId <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Data.Model.ClauseContract deleteObj = new NFMT.Data.Model.ClauseContract();
            deleteObj.RefId = refId;
            deleteObj.RefStatus = NFMT.Common.StatusEnum.已作废;
            deleteObj.LastModifyId = user.EmpId;

            NFMT.Data.BLL.ClauseContractBLL bll = new NFMT.Data.BLL.ClauseContractBLL();
            NFMT.Common.ResultModel result = bll.Invalid(user, deleteObj);

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