using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// CustomApplyStockListHandler 的摘要说明
    /// </summary>
    public class CustomApplyStockListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string sids = context.Request.QueryString["sids"];
            string refNo = context.Request.QueryString["refNo"];

            int corpId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["corpId"]) || !int.TryParse(context.Request.QueryString["corpId"], out corpId) || corpId <= 0)
                corpId = 0;

            int assetId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["assetId"]) || !int.TryParse(context.Request.QueryString["assetId"], out assetId) || assetId <= 0)
                assetId = 0;

            int unitId = 0;
            if (string.IsNullOrEmpty(context.Request.QueryString["unitId"]) || !int.TryParse(context.Request.QueryString["unitId"], out unitId) || unitId <= 0)
                unitId = 0;

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sortdatafield"].Trim(), context.Request.QueryString["sortorder"].Trim());

            NFMT.WareHouse.BLL.CustomsClearanceApplyBLL bll = new NFMT.WareHouse.BLL.CustomsClearanceApplyBLL();
            NFMT.Common.SelectModel select = bll.GetCanCustomStockListSelect(pageIndex, pageSize, orderStr, refNo, corpId, assetId,unitId, sids);
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