using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// ReceivableListHandler 的摘要说明
    /// </summary>
    public class ReceivableListHandler : IHttpHandler
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

            //if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            //    orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());
            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                switch (sortDataField)
                {
                    case "ReceivableId":
                        sortDataField = "r.ContractId";
                        break;
                    case "ReceiveDate":
                        sortDataField = "r.ReceiveDate";
                        break;
                    case "InnerCorp":
                        sortDataField = "c.CorpName";
                        break;
                    case "PayBala":
                        sortDataField = "r.PayBala";
                        break;
                    case "BankName":
                        sortDataField = "b.BankName";
                        break;
                    case "OutCorp":
                        sortDataField = "c2.CorpName";
                        break;
                    case "StatusName":
                        sortDataField = "bd.StatusName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Funds.BLL.ReceivableBLL bll = new NFMT.Funds.BLL.ReceivableBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, fromDate, toDate, empId, bank, status);
            NFMT.Common.ResultModel result = bll.Load(user, select);

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