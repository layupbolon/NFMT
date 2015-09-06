using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BDStyleDtlListHandler 的摘要说明
    /// </summary>
    public class BDStyleDtlListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                int status = -1;
                int pageIndex = 1, pageSize = 10;
                string orderStr = string.Empty, whereStr = string.Empty;
                string sortDataField = string.Empty;
                string sortOrder = string.Empty;
                int styleId = 0;
                if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
                    int.TryParse(context.Request.QueryString["id"], out styleId);

                NFMT.Data.StyleEnum style = (NFMT.Data.StyleEnum)styleId;
                NFMT.Data.DetailCollection details = NFMT.Data.DetailProvider.Details(style);

                List<NFMT.Data.Model.BDStyleDetail> ds = details.ToList();

                string detailName = context.Request["n"];//模糊搜索
                if (!string.IsNullOrEmpty(detailName))
                    ds = details.Where(temp => temp.DetailName.Contains(detailName)).ToList();

                if (!string.IsNullOrEmpty(context.Request["s"]))
                {
                    if (int.TryParse(context.Request["s"], out status) && status > 0)
                        ds = ds.Where(temp => (int)temp.DetailStatus == status).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                    int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
                if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                    int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

                if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                {
                    sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                    sortOrder = context.Request.QueryString["sortorder"].Trim();

                    if (sortDataField == "DetailStatusName")
                    {
                        if (sortOrder.ToLower() == "asc")
                            ds = (from d in ds orderby d.DetailStatus ascending select d).ToList();
                        else if (sortOrder.ToLower() == "desc")
                            ds = (from d in ds orderby d.DetailStatus descending select d).ToList();
                    }
                    else if (sortDataField == "DetailCode")
                    {
                        if (sortOrder.ToLower() == "asc")
                            ds = (from d in ds orderby d.DetailCode ascending select d).ToList();
                        else if (sortOrder.ToLower() == "desc")
                            ds = (from d in ds orderby d.DetailCode descending select d).ToList();
                    }
                    else if (sortDataField == "DetailName")
                    {
                        if (sortOrder.ToLower() == "asc")
                            ds = (from d in ds orderby d.DetailName ascending select d).ToList();
                        else if (sortOrder.ToLower() == "desc")
                            ds = (from d in ds orderby d.DetailName descending select d).ToList();
                    }
                }

                context.Response.ContentType = "application/json; charset=utf-8";
                int pageStart = pageIndex * pageSize;
                List<NFMT.Data.Model.BDStyleDetail> ss = ds.Skip(pageStart).Take(pageSize).ToList();

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("count", ds.Count);
                dic.Add("data", ss);

                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
                context.Response.Write(jsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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