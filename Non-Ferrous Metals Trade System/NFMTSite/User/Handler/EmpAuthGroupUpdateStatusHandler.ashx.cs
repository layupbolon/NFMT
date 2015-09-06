using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// EmpAuthGroupUpdateStatusHandler 的摘要说明
    /// </summary>
    public class EmpAuthGroupUpdateStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            int detailId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["id"]) || !int.TryParse(context.Request.Form["id"], out detailId))
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            NFMT.User.BLL.AuthGroupDetailBLL bll = new NFMT.User.BLL.AuthGroupDetailBLL();
            NFMT.Common.ResultModel result = bll.UpdateStauts(user, detailId, NFMT.Common.StatusEnum.已作废);
            if (result.ResultStatus == 0)
                result.Message = "删除成功";
            else
                result.Message = "删除失败";

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