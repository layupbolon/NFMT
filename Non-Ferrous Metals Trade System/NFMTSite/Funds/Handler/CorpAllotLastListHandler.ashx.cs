using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CorpAllotLastListHandler 的摘要说明
    /// </summary>
    public class CorpAllotLastListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

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
                    case "CashInDate":
                        sortDataField = "ci.CashInDate";
                        break;
                    case "InCorp":
                        sortDataField = "inCorp.CorpName";
                        break;
                    case "OutCorp":
                        sortDataField = "outCorp.CorpName";
                        break;
                    case "CashInBankName":
                        sortDataField = "ban.BankName";
                        break;
                    case "CashInBala":
                        sortDataField = "ci.CashInBala";
                        break;
                    case "LastBala":
                        sortDataField = "ci.CashInBala - ISNULL(ref.SumBala,0)";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string detailIds = context.Request.QueryString["detailIds"];
            string cashInIds = context.Request.QueryString["cashInIds"];

            NFMT.Funds.BLL.CashInCorpBLL bll = new NFMT.Funds.BLL.CashInCorpBLL();
            NFMT.Common.SelectModel select = new NFMT.Common.SelectModel();// bll.GetCorpAllotLastSelect(pageIndex, pageSize, orderStr, outCorpIds, detailIds, cashInIds);
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