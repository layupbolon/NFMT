using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ProducerListHandler 的摘要说明
    /// </summary>
    public class ProducerListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int status = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            int producerArea = 0;

            string key = context.Request["producerName"];//模糊搜索                

            if (!string.IsNullOrEmpty(context.Request["producerStatus"]))
                int.TryParse(context.Request["producerStatus"], out status);//状态查询条件

            if (!string.IsNullOrEmpty(context.Request["producerArea"]))
                int.TryParse(context.Request["producerArea"], out producerArea);//地区查询条件

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
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.Data.BLL.ProducerBLL producerBLL = new NFMT.Data.BLL.ProducerBLL();
            NFMT.Common.SelectModel select = producerBLL.GetSelectModel(pageIndex,pageSize,orderStr,status,key,producerArea);
            NFMT.Common.ResultModel result = producerBLL.Load(new NFMT.Common.UserModel(), select);

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