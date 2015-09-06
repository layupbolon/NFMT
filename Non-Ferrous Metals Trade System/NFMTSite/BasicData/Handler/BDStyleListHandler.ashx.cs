using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BDStyleListHandler 的摘要说明
    /// </summary>
    public class BDStyleListHandler : IHttpHandler
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

                List<NFMT.Data.Model.BDStyle> styles = NFMT.Data.BasicDataProvider.BDStyles;

                string styleName = context.Request["n"];//模糊搜索
                if (!string.IsNullOrEmpty(styleName))
                {
                    styles = styles.Where(temp => temp.BDStyleName.Contains(styleName)).ToList();
                }

                if (!string.IsNullOrEmpty(context.Request["s"]))
                {
                    if (int.TryParse(context.Request["s"], out status) && status>0)
                    {
                        styles = styles.Where(temp => (int)temp.BDStyleStatus == status).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                    int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
                if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                    int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

                if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
                {
                    sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                    sortOrder = context.Request.QueryString["sortorder"].Trim();

                    if (sortDataField == "BDStyleStatusName")
                    {
                        if (sortOrder.ToLower() == "asc")
                            styles = (from u in styles orderby u.BDStyleStatus ascending select u).ToList();
                        else if (sortOrder.ToLower() == "desc")
                            styles = (from u in styles orderby u.BDStyleStatus descending select u).ToList();
                    }
                    else if (sortDataField == "BDStyleCode")
                    {
                        if (sortOrder.ToLower() == "asc")
                            styles = (from u in styles orderby u.BDStyleCode ascending select u).ToList();
                        else if (sortOrder.ToLower() == "desc")
                            styles = (from u in styles orderby u.BDStyleCode descending select u).ToList();
                    }
                    else if (sortDataField == "BDStyleName")
                    {
                        if (sortOrder.ToLower() == "asc")
                            styles = (from u in styles orderby u.BDStyleName ascending select u).ToList();
                        else if (sortOrder.ToLower() == "desc")
                            styles = (from u in styles orderby u.BDStyleName descending select u).ToList();
                    }
                }

                context.Response.ContentType = "application/json; charset=utf-8";

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("count", styles.Count);

                List<NFMT.Data.Model.BDStyle> ss = new List<NFMT.Data.Model.BDStyle>();
                
                int pageEnd = (pageIndex + 1) * pageSize;
                if (pageEnd > styles.Count)
                    pageEnd = styles.Count;

                for (int pageStart = pageIndex * pageSize; pageStart < pageEnd; pageStart++)
                {
                    ss.Add(styles[pageStart]);
                }

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