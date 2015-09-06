using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayApplyInvoiceListHandler 的摘要说明
    /// </summary>
    public class PayApplyInvoiceListHandler : IHttpHandler
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
                    case "InvoiceId":
                        sortDataField = "inv.InvoiceId";
                        break;
                    case "SIId":
                        sortDataField = "si.SIId";
                        break;
                    case "InvoiceDate":
                        sortDataField = "inv.InvoiceDate";
                        break;
                    case "InvoiceNo":
                        sortDataField = "inv.InvoiceNo";
                        break;
                    case "OutCorpName":
                        sortDataField = "inv.OutCorpName ";
                        break;
                    case "InCorpName":
                        sortDataField = "inv.InCorpName";
                        break;
                    case "InvoiceBala":
                        sortDataField = "inv.InvoiceBala";
                        break;
                    case "CurrencyName":
                        sortDataField = "cur.CurrencyName";
                        break;
                    case "LastBala":
                        sortDataField = "ISNULL(inv.InvoiceBala,0) - ISNULL(ipar.SumBala,0)";
                        break;
                    case "DeptName":
                        sortDataField = "dept.DeptName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            string ids = context.Request.QueryString["ids"];
            string refIds = context.Request.QueryString["refIds"];

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Funds.BLL.PayApplyBLL bll = new NFMT.Funds.BLL.PayApplyBLL();
            NFMT.Common.SelectModel select = bll.GetInvoiceListSelect(pageIndex, pageSize, orderStr,ids,refIds);

            NFMT.Authority.CorpAuth auth = new NFMT.Authority.CorpAuth();
            auth.AuthColumnNames.Add("inv.InCorpId");
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