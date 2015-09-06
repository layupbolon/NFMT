using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractClauseCreateHandler 的摘要说明
    /// </summary>
    public class ContractClauseCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string clauseText = context.Request.Form["tv"];
            string clauseEtext = context.Request.Form["ev"];

            if (string.IsNullOrEmpty(clauseText))
            {
                result.Message = "合约条款中文内容不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(clauseEtext))
            {
                result.Message = "合约条款英文内容不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Data.Model.ContractClause clause = new NFMT.Data.Model.ContractClause();
            clause.ClauseEnText = clauseEtext;
            clause.ClauseStatus = NFMT.Common.StatusEnum.已录入;
            clause.ClauseText = clauseText;
            clause.CreatorId = user.EmpId;

            NFMT.Data.BLL.ContractClauseBLL clauseBLL = new NFMT.Data.BLL.ContractClauseBLL();
            result = clauseBLL.Insert(user, clause);

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