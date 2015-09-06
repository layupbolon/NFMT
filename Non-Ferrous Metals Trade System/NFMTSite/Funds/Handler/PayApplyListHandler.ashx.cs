using System;
using System.Collections.Generic;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayApplyListHandler 的摘要说明
    /// </summary>
    public class PayApplyListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime fromDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime toDate = NFMT.Common.DefaultValue.DefaultTime;
            int status = 0;

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);
            if (!string.IsNullOrEmpty(context.Request.QueryString["fd"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["fd"], out fromDate))
                    fromDate = NFMT.Common.DefaultValue.DefaultTime;
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["td"]))
            {
                toDate = !DateTime.TryParse(context.Request.QueryString["td"], out toDate) ? NFMT.Common.DefaultValue.DefaultTime : toDate.AddDays(1);
            }

            int recCorpId = 0;
            int applyDeptId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ad"]) || !int.TryParse(context.Request.QueryString["ad"].Trim(), out applyDeptId))
            {
                applyDeptId = 0;
            }

            if (string.IsNullOrEmpty(context.Request.QueryString["rc"]) || !int.TryParse(context.Request.QueryString["rc"].Trim(), out recCorpId))
            {
                recCorpId = 0;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                switch (sortDataField)
                {
                    case "ApplyTime":
                        sortDataField = "app.ApplyTime";
                        break;
                    case "ApplyNo":
                        sortDataField = "app.ApplyNo";
                        break;
                    case "PayMatterName":
                        sortDataField = "pa.PayMatter";
                        break;
                    case "PayModeName":
                        sortDataField = "pa.PayMode";
                        break;
                    case "RecCorpName":
                        sortDataField = "recCor.CorpName ";
                        break;
                    case "ApplyBala":
                        sortDataField = "pa.ApplyBala";
                        break;
                    case "Name":
                        sortDataField = "emp.Name";
                        break;
                    case "DeptName":
                        sortDataField = "dep.DeptName";
                        break;
                    case "StatusName":
                        sortDataField = "app.ApplyStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, fromDate, toDate, status, applyDeptId, recCorpId);

            NFMT.Authority.CorpAuth auth = new NFMT.Authority.CorpAuth();
            auth.AuthColumnNames.Add("app.ApplyCorp");
            NFMT.Common.ResultModel result = bll.Load(user, select,auth);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("count", totalRows);
            dic.Add("data", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic, new Newtonsoft.Json.Converters.DataTableConverter());

            context.Response.Write(postData);
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