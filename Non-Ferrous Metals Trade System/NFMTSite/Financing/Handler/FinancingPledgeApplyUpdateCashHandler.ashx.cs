using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// FinancingPledgeApplyUpdateCashHandler 的摘要说明
    /// </summary>
    public class FinancingPledgeApplyUpdateCashHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = string.Empty;

            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                context.Response.Write("期货头寸信息不能为空");
                context.Response.End();
            }

            int pledgeApplyId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["pid"]) || !int.TryParse(context.Request.Form["pid"], out pledgeApplyId) || pledgeApplyId <= 0)
            {
                context.Response.Write("参数错误");
                context.Response.End();
            }


            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.Finance.Model.PledgeApplyCashDetail> pledgeApplyCashDetails = serializer.Deserialize<List<NFMT.Finance.Model.PledgeApplyCashDetail>>(rowsStr);
                if (pledgeApplyCashDetails == null || !pledgeApplyCashDetails.Any())
                {
                    context.Response.Write("信息错误");
                    context.Response.End();
                }

                string userJson = serializer.Serialize(user);

                FinanceService.FinService service = new FinanceService.FinService();
                resultStr = service.FinancingPledgeApplyUpdateCash(userJson, rowsStr, pledgeApplyId);
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