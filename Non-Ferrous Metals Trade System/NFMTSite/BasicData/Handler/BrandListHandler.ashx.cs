using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BrandListHandler 的摘要说明
    /// </summary>
    public class BrandListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int brandStatus = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            int producerName = 0;

            string key = context.Request["brandName"];//品牌名称模糊搜索                

            if (!string.IsNullOrEmpty(context.Request["brandStatus"]))
                int.TryParse(context.Request["brandStatus"], out brandStatus);//状态查询条件

            if (!string.IsNullOrEmpty(context.Request["producerName"]))
                int.TryParse(context.Request["producerName"], out producerName);//生产商查询条件

            if (!string.IsNullOrEmpty(context.Request.QueryString["page"]))
                int.TryParse(context.Request.QueryString["page"].Trim(), out pageIndex);

            if (!string.IsNullOrEmpty(context.Request.QueryString["rows"]))
                int.TryParse(context.Request.QueryString["rows"].Trim(), out pageSize);

            //jqwidgets jqxGrid
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
                    case "BrandStatusName":
                        sortDataField = "bd.StatusName";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Data.BLL.BrandBLL brandBLL = new NFMT.Data.BLL.BrandBLL();
            NFMT.Common.SelectModel select = brandBLL.GetSelectModel(pageIndex, pageSize, orderStr, brandStatus, key, producerName);
            NFMT.Common.ResultModel result = brandBLL.Load(new NFMT.Common.UserModel(), select);

            context.Response.ContentType = "text/plain";
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