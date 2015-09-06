using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInAllotCanAllotCashInListHandler 的摘要说明
    /// </summary>
    public class CashInAllotCanAllotCashInListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int status = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["s"]))
            {
                if (!int.TryParse(context.Request.QueryString["s"], out status))
                    status = 0;
            }

            int bank = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["b"]))
            {
                if (!int.TryParse(context.Request.QueryString["b"], out bank))
                    bank = 0;
            }

            int empId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["e"]))
            {
                if (!int.TryParse(context.Request.QueryString["e"], out empId))
                    empId = 0;
            }

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
                    case "CashInId":
                        sortDataField = "ci.CashInId";
                        break;
                    case "CashInDate":
                        sortDataField = "ci.CashInDate";
                        break;
                    case "InnerCorp":
                        sortDataField = "c.CorpName";
                        break;
                    case "CashInBala":
                        sortDataField = "ci.CashInBala";
                        break;
                    case "BankName":
                        sortDataField = "b.BankName";
                        break;
                    case "OutCorp":
                        sortDataField = "ci.PayCorpName";
                        break;
                    case "StatusName":
                        sortDataField = "bd.StatusName";
                        break;
                    case "CanAllotBala":
                        sortDataField = "ci.CashInBala";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Funds.BLL.CashInBLL bll = new NFMT.Funds.BLL.CashInBLL();
            NFMT.Common.SelectModel select = bll.GetCanAllotSelectModel(pageIndex, pageSize, orderStr, fromDate, toDate, empId, bank, status);

            NFMT.Common.IAuthority authority = new NFMT.Authority.CorpAuth();
            authority.AuthColumnNames.Add("ci.CashInCorpId");

            NFMT.Common.ResultModel result = bll.Load(user, select, authority);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            System.Collections.Generic.Dictionary<string, object> dic = new System.Collections.Generic.Dictionary<string, object>();

            dic.Add("count", result.AffectCount);
            dic.Add("data", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic);

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