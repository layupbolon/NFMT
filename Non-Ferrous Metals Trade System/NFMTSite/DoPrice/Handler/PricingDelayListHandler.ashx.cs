using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PricingDelayListHandler 的摘要说明
    /// </summary>
    public class PricingDelayListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int pricingApplyId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["p"]))
            {
                if (!int.TryParse(context.Request.QueryString["p"], out pricingApplyId))
                    pricingApplyId = 0;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.DoPrice.BLL.PricingApplyDelayBLL bll = new NFMT.DoPrice.BLL.PricingApplyDelayBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModelByPricingApplyId(pageIndex, pageSize, orderStr, pricingApplyId);
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