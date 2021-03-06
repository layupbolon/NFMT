﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInAllotCorpUpdateHandler 的摘要说明
    /// </summary>
    public class CashInAllotCorpUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string detailStr = context.Request.Form["Details"];
            if (string.IsNullOrEmpty(detailStr))
            {
                context.Response.Write("分配信息不能为空");
                context.Response.End();
            }

            string memo = context.Request.Form["Memo"];

            int allotId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["AllotId"]) || !int.TryParse(context.Request.Form["AllotId"], out allotId) || allotId <= 0)
            {
                context.Response.Write("公司信息错误");
                context.Response.End();
            }

            bool isShare = false;
            if (string.IsNullOrEmpty(context.Request.Form["IsShare"]) || !bool.TryParse(context.Request.Form["IsShare"], out isShare))
            {
                context.Response.Write("是否共享信息错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.Funds.Model.CashInCorp> details = serializer.Deserialize<List<NFMT.Funds.Model.CashInCorp>>(detailStr);
                if (details == null)
                {
                    context.Response.Write("分配信息错误");
                    context.Response.End();
                }

                NFMT.Funds.Model.CashInAllot allot = new NFMT.Funds.Model.CashInAllot();
                allot.AllotDesc = memo;
                allot.AllotId = allotId;

                foreach (NFMT.Funds.Model.CashInCorp corp in details)
                {
                    corp.IsShare = isShare;
                }

                NFMT.Funds.BLL.CashInAllotBLL bll = new NFMT.Funds.BLL.CashInAllotBLL();
                result = bll.UpdateCorp(user, allot, details);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            if (result.ResultStatus == 0)
                result.Message = "修改成功";

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