using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MasterCluaseGetHandler 的摘要说明
    /// </summary>
    public class MasterCluaseGetHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int cId = -1;            

            if (!string.IsNullOrEmpty(context.Request["refId"]))
                int.TryParse(context.Request["refId"], out cId);

            NFMT.Common.UserModel user  = Utility.UserUtility.CurrentUser;

            NFMT.Data.BLL.ClauseContractBLL bll = new NFMT.Data.BLL.ClauseContractBLL();
            NFMT.Common.ResultModel result = bll.Get(user, cId);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            context.Response.ContentType = "text/plain";
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