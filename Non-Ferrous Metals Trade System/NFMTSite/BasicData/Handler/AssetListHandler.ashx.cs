using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// AssetList 的摘要说明
    /// </summary>
    public class AssetListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int status = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            string sortDataField = string.Empty;
            string sortOrder = string.Empty;

            string assetName = context.Request["k"];//AssetName模糊搜索                

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);//status状态查询条件

            //jqwidgets jqxGrid
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataFields = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrders = context.Request.QueryString["sortorder"].Trim();

                orderStr = string.Format("{0} {1}", sortDataFields, sortOrders);
            }

            NFMT.Data.BLL.AssetBLL assetBLL = new NFMT.Data.BLL.AssetBLL();
            NFMT.Common.SelectModel select = assetBLL.GetSelectModel(pageIndex, pageSize, orderStr, status, assetName);
            NFMT.Common.ResultModel result = assetBLL.Load(new NFMT.Common.UserModel(), select);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //jqwidgets
            dic.Add("count", result.AffectCount);
            dic.Add("data", dt);

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dic, new Newtonsoft.Json.Converters.DataTableConverter());
            context.Response.Write(jsonStr);
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