using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// PledgeApplyCompleteHandler 的摘要说明
    /// </summary>
    public class PledgeApplyCompleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int pledgeApplyId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out pledgeApplyId))
                {
                    context.Response.Write("参数错误");
                    context.Response.End();
                }
            }

            NFMT.WareHouse.BLL.PledgeApplyBLL bll = new NFMT.WareHouse.BLL.PledgeApplyBLL();
            result = bll.Complete(user, pledgeApplyId);
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