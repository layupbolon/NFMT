using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PaymentListHandler 的摘要说明
    /// </summary>
    public class PaymentListHandler : IHttpHandler
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
                if (!DateTime.TryParse(context.Request.QueryString["td"], out toDate))
                    toDate = NFMT.Common.DefaultValue.DefaultTime;
                else
                    toDate.AddDays(1);
            }

            int recCorpId = 0;
            int empId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["ei"]) || !int.TryParse(context.Request.QueryString["ei"].Trim(), out empId))
            {
                empId = 0;
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
                    case "PaymentCode":
                        sortDataField = "pay.PaymentCode";
                        break;
                    case "ApplyNo":
                        sortDataField = "app.ApplyNo";
                        break;
                    case "PayDatetime":
                        sortDataField = "pay.PayDatetime";
                        break;
                    case "RecevableCorpName":
                        sortDataField = "recCorp.CorpName";
                        break;
                    case "PayStyleName":
                        sortDataField = "pay.PayStyle ";
                        break;
                    case "PayBala":
                        sortDataField = "pay.PayBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "PayEmpName":
                        sortDataField = "emp.Name";
                        break;
                    case "PaymentStatusName":
                        sortDataField = "pay.PaymentStatus";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Funds.BLL.PaymentBLL bll = new NFMT.Funds.BLL.PaymentBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, fromDate, toDate, empId, recCorpId,status);
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