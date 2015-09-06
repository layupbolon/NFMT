using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Invoice.Handler
{
    /// <summary>
    /// SICreateStockListHandler 的摘要说明
    /// </summary>
    public class SICreateStockListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string refNo = context.Request.QueryString["r"];

            string outContractNO = context.Request.QueryString["outNo"];

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.Invoice.BLL.SIBLL bll = new NFMT.Invoice.BLL.SIBLL();
            NFMT.Common.SelectModel select = bll.GetCanAllotStockSelectModel(pageIndex, pageSize, orderStr, refNo, outContractNO);

            NFMT.Authority.StockAuth auth = new NFMT.Authority.StockAuth();
            NFMT.Common.ResultModel result = bll.Load(user, select,auth);

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