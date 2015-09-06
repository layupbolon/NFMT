using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// BankPledgeReportHandler 的摘要说明
    /// </summary>
    public class BankPledgeReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 20;
            string orderStr = string.Empty, whereStr = string.Empty;

            DateTime startDate = NFMT.Common.DefaultValue.DefaultTime;
            DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["s"]) || !DateTime.TryParse(context.Request["s"], out startDate))
                startDate = NFMT.Common.DefaultValue.DefaultTime;

            if (string.IsNullOrEmpty(context.Request["e"]) || !DateTime.TryParse(context.Request["e"], out endDate))
                endDate = NFMT.Common.DefaultValue.DefaultTime;
            else
                endDate = endDate.AddDays(1);

            int bankId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["bankId"]) || !int.TryParse(context.Request.QueryString["bankId"], out bankId))
                bankId = 0;

            int repoInfo = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["repoInfo"]) || !int.TryParse(context.Request.QueryString["repoInfo"], out repoInfo))
                repoInfo = 0;

            string refNo = context.Request.QueryString["refNo"];

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
                    case "BankName":
                        sortDataField = "bank.BankName";
                        break;
                    case "RefNo":
                        sortDataField = "pasd.RefNo";
                        break;
                    case "NetAmount":
                        sortDataField = "pasd.NetAmount";
                        break;
                    case "ContractNo":
                        sortDataField = "pasd.ContractNo";
                        break;
                    case "ApplyTime":
                        sortDataField = "pa.ApplyTime";
                        break;
                    case "Hands":
                        sortDataField = "pasd.Hands";
                        break;
                    case "Price":
                        sortDataField = "price.Price";
                        break;
                    case "ExpiringDate":
                        sortDataField = "price.ExpiringDate";
                        break;
                    case "nowPledgeAmount":
                        sortDataField = "pasd.NetAmount";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Finance.BLL.PledgeApplyStockDetailBLL bll = new NFMT.Finance.BLL.PledgeApplyStockDetailBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetBankPledgeReportSelect(pageIndex, pageSize, orderStr, refNo, bankId, startDate, endDate, repoInfo);
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