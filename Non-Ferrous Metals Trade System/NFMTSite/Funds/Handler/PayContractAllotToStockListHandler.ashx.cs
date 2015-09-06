using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// PayContractAllotToStockListHandler 的摘要说明
    /// </summary>
    public class PayContractAllotToStockListHandler : IHttpHandler
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
                    case "DetailId":
                        sortDataField = "psd.DetailId";
                        break;
                    case "ApplyNo":
                        sortDataField = "a.ApplyNo";
                        break;
                    case "SubNo":
                        sortDataField = "sub.SubNo";
                        break;
                    case "RefNo":
                        sortDataField = "sn.RefNo";
                        break;
                    case "PayBala":
                        sortDataField = "psd.PayBala";
                        break;
                    case "FundsBala":
                        sortDataField = "psd.FundsBala";
                        break;
                    case "VirtualBala":
                        sortDataField = "psd.VirtualBala";
                        break;
                    case "StatusName":
                        sortDataField = "bd.StatusName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Funds.BLL.PaymentStockDetailBLL bll = new NFMT.Funds.BLL.PaymentStockDetailBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, status);
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