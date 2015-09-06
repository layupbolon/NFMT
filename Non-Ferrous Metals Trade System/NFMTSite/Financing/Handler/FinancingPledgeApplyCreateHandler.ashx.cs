﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Financing.Handler
{
    /// <summary>
    /// FinancingPledgeApplyCreateHandler 的摘要说明
    /// </summary>
    public class FinancingPledgeApplyCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = string.Empty;

            string pledgeApplyStr = context.Request.Form["pledgeApply"];
            if (string.IsNullOrEmpty(pledgeApplyStr))
            {
                context.Response.Write("质押申请单信息不能为空");
                context.Response.End();
            }
            string rowsStr = context.Request.Form["rows"];
            if (string.IsNullOrEmpty(rowsStr))
            {
                context.Response.Write("实货信息不能为空");
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
                FinanceService.PledgeApply pledgeApply = serializer.Deserialize<FinanceService.PledgeApply>(pledgeApplyStr);
                List<FinanceService.PledgeApplyStockDetail> pledgeApplyStockDetails = serializer.Deserialize<List<FinanceService.PledgeApplyStockDetail>>(rowsStr);
                if (pledgeApply == null || pledgeApplyStockDetails == null || !pledgeApplyStockDetails.Any())
                {
                    context.Response.Write("信息错误");
                    context.Response.End();
                }

                string userJson = serializer.Serialize(user);
                FinanceService.UserModel serviceUser = serializer.Deserialize<FinanceService.UserModel>(userJson);

                FinanceService.FinService service = new FinanceService.FinService();
                resultStr = service.FinancingPledgeApplyCreate(serviceUser, pledgeApply, pledgeApplyStockDetails.ToArray(), isSubmitAudit);
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