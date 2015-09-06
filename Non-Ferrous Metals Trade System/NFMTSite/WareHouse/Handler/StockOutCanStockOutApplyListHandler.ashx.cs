using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockOutCanStockOutApplyListHandler 的摘要说明
    /// </summary>
    public class StockOutCanStockOutApplyListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string contractCode = context.Request.QueryString["cc"];
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
                        sortDataField = string.Format("a.{0}", sortDataField);
                        break;
                    case "SubNo":
                        sortDataField = string.Format("cs.{0}", sortDataField);
                        break;
                    case "InnerCorpName":
                        sortDataField = "ccdin.CorpName";
                        break;
                    case "OutCorpName":
                        sortDataField = "ccdout.CorpName";
                        break;
                    case "Name":
                        sortDataField = "e.Name ";
                        break;
                    case "DeptName":
                        sortDataField = "dept.DeptName";
                        break;
                    case "StatusName":
                        sortDataField = "sd.StatusName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.WareHouse.BLL.StockOutApplyBLL stockOutApplyBLL = new NFMT.WareHouse.BLL.StockOutApplyBLL();
            NFMT.Common.SelectModel select = stockOutApplyBLL.GetCanStockOutApplyListSelectModel(pageIndex, pageSize, orderStr, contractCode, fromDate, toDate, status);
            NFMT.Common.IAuthority auth = new NFMT.Authority.ContractAuth();
            auth.AuthColumnNames.Add("con.ContractId");

            NFMT.Common.ResultModel result = stockOutApplyBLL.Load(user, select, auth);

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