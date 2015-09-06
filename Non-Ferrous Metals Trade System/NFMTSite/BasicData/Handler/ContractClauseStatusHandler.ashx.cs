using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractClauseStatusHandler 的摘要说明
    /// </summary>
    public class ContractClauseStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int clauseId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["ci"], out clauseId) || clauseId <= 0)
            {
                context.Response.Write("模板序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Data.BLL.ContractClauseBLL bll = new NFMT.Data.BLL.ContractClauseBLL();
            NFMT.Data.Model.ContractClause clause = new NFMT.Data.Model.ContractClause();
            clause.LastModifyId = user.EmpId;
            clause.ClauseId = clauseId;

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.冻结:
                    result = bll.Freeze(user, clause);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = bll.UnFreeze(user, clause);
                    break;
            }

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