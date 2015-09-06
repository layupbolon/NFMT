using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MasterClauseUpdateHandler 的摘要说明
    /// </summary>
    public class MasterClauseUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int refId = 0;
            int sort = 0;
            bool isChose = false;

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                result.Message = "序号出错";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["id"], out refId))
            {
                result.Message = "序号出错";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["s"]))
            {
                result.Message = "排序值为必填";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["s"], out sort))
            {
                result.Message = "排序值为必填";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["ic"]))
            {
                result.Message = "是否默认选中传值出错";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }
            if (!bool.TryParse(context.Request.Form["ic"], out isChose))
            {
                result.Message = "是否默认选中传值出错";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Data.BLL.ClauseContractBLL refBLL = new NFMT.Data.BLL.ClauseContractBLL();

            NFMT.Data.Model.ClauseContract masterClause = new NFMT.Data.Model.ClauseContract();
            masterClause.IsChose = isChose;
            masterClause.LastModifyId = user.EmpId;
            masterClause.RefId = refId;
            masterClause.Sort = sort;

            result = refBLL.Update(user, masterClause);
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