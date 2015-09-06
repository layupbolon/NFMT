using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// FinRepoApplyCreateHandler 的摘要说明
    /// </summary>
    public class FinRepoApplyCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = string.Empty;

            string repoApplyStr = context.Request.Form["repoApply"];
            if (string.IsNullOrEmpty(repoApplyStr))
            {
                context.Response.Write("赎回申请单信息不能为空");
                context.Response.End();
            }
            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                context.Response.Write("明细信息不能为空");
                context.Response.End();
            }

            bool isSubmitAudit = false;
            if (string.IsNullOrEmpty(context.Request.Form["isAudit"]) || !bool.TryParse(context.Request.Form["isAudit"], out isSubmitAudit))
                isSubmitAudit = false;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Finance.Model.RepoApply repoApply = serializer.Deserialize<NFMT.Finance.Model.RepoApply>(repoApplyStr);
                List<NFMT.Finance.Model.RepoApplyDetail> details = serializer.Deserialize<List<NFMT.Finance.Model.RepoApplyDetail>>(rowsStr);
                if (repoApply == null || details == null || !details.Any())
                {
                    context.Response.Write("信息错误");
                    context.Response.End();
                }

                decimal sumNetAmount = 0;
                int sumHands = 0;
                foreach (NFMT.Finance.Model.RepoApplyDetail detail in details)
                {
                    sumNetAmount += detail.NetAmount;
                    sumHands += detail.Hands;
                }

                repoApply.SumNetAmount = sumNetAmount;
                repoApply.SumHands = sumHands;

                string userJson = serializer.Serialize(user);
                string repoApplyJson = serializer.Serialize(repoApply);                

                FinanceService.FinService service = new FinanceService.FinService();
                resultStr = service.FinRepoApplyCreate(userJson, repoApplyJson, rowsStr, isSubmitAudit);
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
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