using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// RopoCreateHandler 的摘要说明
    /// </summary>
    public class RopoCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            NFMT.WareHouse.BLL.RepoBLL repoBLL = new NFMT.WareHouse.BLL.RepoBLL();

            int repoApplyId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out repoApplyId))
                {
                    result.Message = "参数错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
            }

            string sids = context.Request.Form["sids"];

            string memo = context.Request.Form["memo"];

            result = repoBLL.RepoCreateHandle(user, repoApplyId, memo, sids);
            if (result.ResultStatus == 0)
                result.Message = "新增成功";

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