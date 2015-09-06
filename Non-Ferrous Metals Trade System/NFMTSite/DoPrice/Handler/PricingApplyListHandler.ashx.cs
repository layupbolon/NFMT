using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.DoPrice.Handler
{
    /// <summary>
    /// PricingApplyListHandler 的摘要说明
    /// </summary>
    public class PricingApplyListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int applyPerson = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["applyPerson"]))
            {
                if (!int.TryParse(context.Request.QueryString["applyPerson"], out applyPerson))
                    applyPerson = 0;
            }

            string subNo = context.Request.QueryString["subNo"];

            int assetId = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["assetId"]))
            {
                if (!int.TryParse(context.Request.QueryString["assetId"], out assetId))
                    assetId = 0;
            }

            int status = 0;
            if (!string.IsNullOrEmpty(context.Request.QueryString["status"]))
            {
                if (!int.TryParse(context.Request.QueryString["status"], out status))
                    status = 0;
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.DoPrice.BLL.PricingApplyBLL bll = new NFMT.DoPrice.BLL.PricingApplyBLL();
            NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, applyPerson, subNo, assetId, status);

            NFMT.Common.IAuthority auth = new NFMT.Authority.ContractAuth();
            auth.AuthColumnNames.Add("pa.ContractId");

            NFMT.Common.ResultModel result = bll.Load(user, select, auth);

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