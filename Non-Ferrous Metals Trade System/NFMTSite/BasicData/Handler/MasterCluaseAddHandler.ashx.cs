using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MasterCluaseAddHandler 的摘要说明
    /// </summary>
    public class MasterCluaseAddHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int clauseId = 0;
            int masterId = 0;

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                context.Response.Write("条款序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["id"], out clauseId) || clauseId <= 0)
            {
                context.Response.Write("条款序号错误");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["mid"]))
            {
                context.Response.Write("模板序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["mid"], out masterId) || masterId <= 0)
            {
                context.Response.Write("模板序号错误");
                context.Response.End();
            }
            
            bool isChose = false;
            int sort = 1;

            NFMT.Data.Model.ClauseContract insertObj = new NFMT.Data.Model.ClauseContract();
            insertObj.ClauseId = clauseId;
            insertObj.CreatorId = user.EmpId;
            insertObj.IsChose = isChose;
            insertObj.MasterId = masterId;
            insertObj.RefStatus = NFMT.Common.StatusEnum.已生效;
            insertObj.Sort = sort;

            NFMT.Data.BLL.ClauseContractBLL bll = new NFMT.Data.BLL.ClauseContractBLL();
            NFMT.Common.ResultModel result = bll.Insert(user, insertObj);

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