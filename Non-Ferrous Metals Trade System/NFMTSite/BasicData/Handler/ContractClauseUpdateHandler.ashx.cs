using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractClauseUpdateHandler 的摘要说明
    /// </summary>
    public class ContractClauseUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string clauseText = context.Request.Form["tv"];
            string clauseEtext = context.Request.Form["ev"];
            int clauseStatus = 0;
            int clauseId = 0;

            if (string.IsNullOrEmpty(clauseText))
            {
                context.Response.Write("合约条款中文内容不能为空");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(clauseEtext))
            {
                context.Response.Write("合约条款英文内容不能为空");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["cs"], out clauseStatus) || clauseStatus <= 0)
            {
                context.Response.Write("必须选择数据状态");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["cid"], out clauseId) || clauseId <= 0)
            {
                context.Response.Write("合约条款序号错误");
                context.Response.End();
            }

            NFMT.Data.BLL.ContractClauseBLL bll = new NFMT.Data.BLL.ContractClauseBLL();
            NFMT.Data.Model.ContractClause clause = new NFMT.Data.Model.ContractClause();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            clause.LastModifyId = user.EmpId;
            clause.ClauseText = clauseText;
            clause.ClauseEnText = clauseEtext;
            clause.ClauseId = clauseId;
            clause.ClauseStatus = (NFMT.Common.StatusEnum)clauseStatus;
            NFMT.Common.ResultModel result = bll.Update(user, clause);

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