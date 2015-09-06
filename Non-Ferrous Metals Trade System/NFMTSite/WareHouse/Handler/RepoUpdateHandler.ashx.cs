using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// RepoUpdateHandler 的摘要说明
    /// </summary>
    public class RepoUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int repoId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out repoId))
                {
                    result.Message = "参数错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
            }

            string sids = context.Request.Form["sids"];
            string memo = context.Request.Form["memo"];

            NFMT.WareHouse.BLL.RepoBLL repoBLL = new NFMT.WareHouse.BLL.RepoBLL();
            result = repoBLL.RepoUpdateHandle(user, repoId, sids, memo);
            if (result.ResultStatus == 0)
                result.Message = "回购修改成功";

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