using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// BussinessInvoiceAllotListHandler 的摘要说明
    /// </summary>
    public class BussinessInvoiceAllotListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int inCorpId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["inCorpId"]))
            {
                if (!int.TryParse(context.Request.QueryString["inCorpId"], out inCorpId))
                    inCorpId = 0;
            }

            int outCorpId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["outCorpId"]))
            {
                if (!int.TryParse(context.Request.QueryString["outCorpId"], out outCorpId))
                    outCorpId = 0;
            }

            int currencyId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["cur"]))
            {
                if (!int.TryParse(context.Request.QueryString["cur"], out currencyId))
                    currencyId = 0;
            }

            int assetId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["ass"]))
            {
                if (!int.TryParse(context.Request.QueryString["ass"], out assetId))
                    assetId = 0;
            }

            int dir = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["dir"]))
            {
                if (!int.TryParse(context.Request.QueryString["dir"], out dir))
                    dir = 0;
            }

            string iids = context.Request.QueryString["iids"];

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.Invoice.BLL.BusinessInvoiceBLL bll = new NFMT.Invoice.BLL.BusinessInvoiceBLL();
            NFMT.Common.SelectModel select = bll.GetCanAllotBISelectModel(pageIndex, pageSize, orderStr,dir, currencyId, assetId, outCorpId, inCorpId, iids);
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