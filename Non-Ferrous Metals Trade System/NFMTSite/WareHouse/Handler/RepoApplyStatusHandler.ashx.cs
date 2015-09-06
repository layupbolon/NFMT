using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// RepoApplyStatusHandler 的摘要说明
    /// </summary>
    public class RepoApplyStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int repoApplyId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["ri"], out repoApplyId) || repoApplyId <= 0)
            {
                result.Message = "出库申请序号错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                result.Message = "操作错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;

            NFMT.WareHouse.BLL.RepoApplyBLL bll = new NFMT.WareHouse.BLL.RepoApplyBLL();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.撤返:
                    result = bll.Goback(user, repoApplyId);
                    break;
                case NFMT.Common.OperateEnum.作废:
                    result = bll.Invalid(user, repoApplyId);
                    break;
                case NFMT.Common.OperateEnum.确认完成:
                    result = bll.Confirm(user, repoApplyId);
                    break;
                case NFMT.Common.OperateEnum.确认完成撤销:
                    result = bll.ConfirmCancel(user, repoApplyId);
                    break;
            }

            if (result.ResultStatus == 0)
                result.Message = string.Format("{0}成功", operateEnum.ToString());

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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