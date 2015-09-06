using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// PledgeGoBackHandler 的摘要说明
    /// </summary>
    public class PledgeGoBackHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int pledgeId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out pledgeId))
                {
                    context.Response.Write("参数错误");
                    context.Response.End();
                }
            }

            //NFMT.WareHouse.BLL.PledgeBLL bll = new NFMT.WareHouse.BLL.PledgeBLL();
            //result = bll.Goback(user, pledgeId);
            //if (result.ResultStatus == 0)
            //    context.Response.Write("撤返成功");
            //else
            //    context.Response.Write(result.Message);

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