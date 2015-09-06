using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// RepoGoBackHandler 的摘要说明
    /// </summary>
    public class RepoGoBackHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();
            string resultStr = "撤返失败";

            int repoId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                if (!int.TryParse(context.Request.Form["id"], out repoId))
                {
                    context.Response.Write("参数错误");
                    context.Response.End();
                }
            }

            NFMT.WareHouse.BLL.RepoBLL bll = new NFMT.WareHouse.BLL.RepoBLL();
            result = bll.Get(user, repoId);
            if (result.ResultStatus != 0)
            {
                context.Response.Write("获取数据错误");
                context.Response.End();
            }

            NFMT.WareHouse.Model.Repo repo = result.ReturnValue as NFMT.WareHouse.Model.Repo;

            result = bll.GoBack(user, repo);
            resultStr = result.Message;

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