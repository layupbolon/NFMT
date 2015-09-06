using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// PaymentReportHandler 的摘要说明
    /// </summary>
    public class PaymentReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime startDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["s"]) || !DateTime.TryParse(context.Request["s"], out startDate))
                startDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["e"]) || !DateTime.TryParse(context.Request["e"], out endDate))
                endDate = NFMT.Common.DefaultValue.DefaultTime;
            else
                endDate = endDate.AddDays(1);

            int applyCorp = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["corp"]) || !int.TryParse(context.Request.QueryString["corp"], out applyCorp))
                applyCorp = 0;

            int applyDept = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["dept"]) || !int.TryParse(context.Request.QueryString["dept"], out applyDept))
                applyDept = 0;

            int currency = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["cur"]) || !int.TryParse(context.Request.QueryString["cur"], out currency))
                currency = 0;

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
                    case "PayApplyId":
                        sortDataField = "pa.PayApplyId";
                        break;
                    case "ApplyTime":
                        sortDataField = "a.ApplyTime";
                        break;
                    case "ApplyNo":
                        sortDataField = "a.ApplyNo";
                        break;
                    case "ApplyBala":
                        sortDataField = "pa.ApplyBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "ApplyCorpName":
                        sortDataField = "corp.CorpName";
                        break;
                    case "DeptName":
                        sortDataField = "dept.DeptName";
                        break;
                    case "RecCorpName":
                        sortDataField = "recCorp.CorpName";
                        break;
                    case "BankName":
                        sortDataField = "bank.BankName";
                        break;
                    case "RecBankAccount":
                        sortDataField = "pa.RecBankAccount";
                        break;
                    case "DetailName":
                        sortDataField = "bdPayMode.DetailName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetPayApplyReportSelect(pageIndex, pageSize, orderStr, applyCorp, applyDept, currency, startDate, endDate);
            NFMT.Common.ResultModel result = bll.Load(user, select, new NFMT.Common.BasicAuth());

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