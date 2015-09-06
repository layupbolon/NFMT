using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// FinancingRepoApplyStatusHandler 的摘要说明
    /// </summary>
    public class FinancingRepoApplyStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
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

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            string resultStr = string.Empty;

            string userJson = serializer.Serialize(user);

            FinanceService.FinService service = new FinanceService.FinService();
            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    resultStr = service.FinancingRepoApplyInvalid(userJson, id);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    resultStr = service.FinancingRepoApplyGoBack(userJson, id);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    resultStr = service.FinancingRepoApplyIdComplete(userJson, id);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    resultStr = service.FinancingRepoApplyIdCompleteCancel(userJson, id);
                    break;
            }

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