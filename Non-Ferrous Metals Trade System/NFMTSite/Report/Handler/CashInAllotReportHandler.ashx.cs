using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Report.Handler
{
    /// <summary>
    /// CashInAllotReportHandler 的摘要说明
    /// </summary>
    public class CashInAllotReportHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            //DateTime startDate = NFMT.Common.DefaultValue.DefaultTime;
            //DateTime endDate = NFMT.Common.DefaultValue.DefaultTime;

            //if (string.IsNullOrEmpty(context.Request["s"]) || !DateTime.TryParse(context.Request["s"], out startDate))
            //    startDate = NFMT.Common.DefaultValue.DefaultTime;

            //if (string.IsNullOrEmpty(context.Request["e"]) || !DateTime.TryParse(context.Request["e"], out endDate))
            //    endDate = NFMT.Common.DefaultValue.DefaultTime;
            //else
            //    endDate.AddDays(1);

            //string refNo = context.Request["r"];

            int cashInId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["id"]) || !int.TryParse(context.Request.QueryString["id"], out cashInId))
                cashInId = 0;

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
                    case "CashInId":
                        sortDataField = "ci.CashInId";
                        break;
                    case "CorpName":
                        sortDataField = "corp.CorpName";
                        break;
                    case "corpAllotBala":
                        sortDataField = "corpAllotInfo.corpAllotBala";
                        break;
                    case "ContractNo":
                        sortDataField = "con.ContractNo";
                        break;
                    case "conAllotBala":
                        sortDataField = "conAllotInfo.conAllotBala";
                        break;
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                    case "stAllotBala":
                        sortDataField = "stAllotInfo.stAllotBala";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Funds.BLL.CashInAllotBLL bll = new NFMT.Funds.BLL.CashInAllotBLL();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.SelectModel select = bll.GetCustomReportSelect(pageIndex, pageSize, orderStr, cashInId);
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