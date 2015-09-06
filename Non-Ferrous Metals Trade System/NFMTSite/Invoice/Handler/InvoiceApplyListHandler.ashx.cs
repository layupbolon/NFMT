using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// InvoiceApplyListHandler 的摘要说明
    /// </summary>
    public class InvoiceApplyListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int empId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["e"]) || !int.TryParse(context.Request.QueryString["e"], out empId) || empId <= 0)
                empId = 0;

            int status = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["s"]) || !int.TryParse(context.Request.QueryString["s"], out status) || status <= 0)
                status = 0;

            DateTime fromDate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["f"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["f"], out fromDate))
                    fromDate = NFMT.Common.DefaultValue.DefaultTime;
            }

            DateTime toDate = NFMT.Common.DefaultValue.DefaultTime;
            if (!string.IsNullOrEmpty(context.Request.QueryString["t"]))
            {
                if (!DateTime.TryParse(context.Request.QueryString["t"], out toDate))
                    toDate = NFMT.Common.DefaultValue.DefaultTime;
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
                    case "InvoiceApplyId":
                        sortDataField = "ia.InvoiceApplyId";
                        break;
                    case "ApplyId":
                        sortDataField = "ca.ApplyId";
                        break;
                    case "ApplyNo":
                        sortDataField = "a.ApplyNo";
                        break;
                    case "Name":
                        sortDataField = "e.Name";
                        break;
                    case "ApplyTime":
                        sortDataField = "a.ApplyTime";
                        break;
                    case "DeptName":
                        sortDataField = "dept.DeptName";
                        break;
                    case "CorpName":
                        sortDataField = "corp.CorpName";
                        break;
                    case "ApplyDesc":
                        sortDataField = "a.ApplyDesc";
                        break;
                    case "StatusName":
                        sortDataField = "bd.StatusName";
                        break;
                    case "ApplyStatus":
                        sortDataField = "a.ApplyStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Invoice.BLL.InvoiceApplyBLL bll = new NFMT.Invoice.BLL.InvoiceApplyBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, fromDate, toDate, empId, status);
            NFMT.Common.ResultModel result = bll.Load(user, select);

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