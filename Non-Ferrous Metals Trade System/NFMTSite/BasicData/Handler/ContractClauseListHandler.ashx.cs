using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractClauseList 的摘要说明
    /// </summary>
    public class ContractClauseListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int status = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            string sortDataField = string.Empty;
            string sortOrder = string.Empty;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Data.BLL.ContractClauseBLL bll = new NFMT.Data.BLL.ContractClauseBLL();
            NFMT.Common.ResultModel result = bll.Load<NFMT.Data.Model.ContractClause>(user);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            List<NFMT.Data.Model.ContractClause> clauses = new List<NFMT.Data.Model.ContractClause>();

            clauses = result.ReturnValue as List<NFMT.Data.Model.ContractClause>;

            string masterName = context.Request["n"];//模糊搜索
            if (!string.IsNullOrEmpty(masterName))
                clauses = clauses.Where(temp => temp.ClauseText.Contains(masterName)).ToList();

            if (!string.IsNullOrEmpty(context.Request["s"]))
            {
                if (int.TryParse(context.Request["s"], out status) && status > 0)
                    clauses = clauses.Where(temp => (int)temp.ClauseStatus == status).ToList();
            }

            int count = clauses.Count;

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                sortOrder = context.Request.QueryString["sortorder"].Trim();

                if (sortDataField == "ClauseStatusName")
                {
                    if (sortOrder.ToLower() == "asc")
                        clauses = (from m in clauses orderby m.ClauseStatus ascending select m).ToList();
                    else if (sortOrder.ToLower() == "desc")
                        clauses = (from m in clauses orderby m.ClauseStatus descending select m).ToList();
                }
                else if (sortDataField == "ClauseText")
                {
                    if (sortOrder.ToLower() == "asc")
                        clauses = (from m in clauses orderby m.ClauseText ascending select m).ToList();
                    else if (sortOrder.ToLower() == "desc")
                        clauses = (from m in clauses orderby m.ClauseText descending select m).ToList();
                }
                else if (sortDataField == "ClauseEnText")
                {
                    if (sortOrder.ToLower() == "asc")
                        clauses = (from m in clauses orderby m.ClauseEnText ascending select m).ToList();
                    else if (sortOrder.ToLower() == "desc")
                        clauses = (from m in clauses orderby m.ClauseEnText descending select m).ToList();
                }
            }

            clauses = (from m in clauses orderby m.ClauseId descending select m).ToList();      

            context.Response.ContentType = "application/json; charset=utf-8";
            int pageStart = pageIndex * pageSize;
            clauses = clauses.Skip(pageStart).Take(pageSize).ToList();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("count", count);
            dic.Add("data", clauses);

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
            context.Response.Write(jsonStr);
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