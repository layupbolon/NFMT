using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Files.Handler
{
    /// <summary>
    /// AttachUpdateStatusHandler 的摘要说明
    /// </summary>
    public class AttachUpdateStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int attachId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["aid"]) || !int.TryParse(context.Request.Form["aid"], out attachId) || attachId <= 0)
            {
                context.Response.Write("附件序号错误");
                context.Response.End();
            }

            int status = 0;
            if (string.IsNullOrEmpty(context.Request.Form["s"]) || !int.TryParse(context.Request.Form["s"], out status) || status <= 0)
            {
                context.Response.Write("状态信息错误");
                context.Response.End();
            }

            try
            {
                NFMT.Operate.BLL.AttachBLL bll = new NFMT.Operate.BLL.AttachBLL();
                result = bll.UpdateAttachStatus(user, attachId, status);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
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