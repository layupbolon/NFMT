using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// AreaListHandler 的摘要说明
    /// </summary>
    public class AreaListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int status = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string key = context.Request["k"];//模糊搜索                

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);//状态查询条件

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

                switch (sortDataFields)
                {
                    case "AreaId":
                        sortDataFields = "a.AreaId";
                        break;
                    case "AreaName":
                        sortDataFields = "a.AreaName";
                        break;
                    case "AreaFullName":
                        sortDataFields = "a.AreaFullName";
                        break;
                    case "AreaShort":
                        sortDataFields = "a.AreaShort";
                        break;
                    case "AreaCode":
                        sortDataFields = "a.AreaCode";
                        break;
                    case "AreaStatus":
                        sortDataFields = "a.AreaStatus";
                        break;
                    case "AreaZip":
                        sortDataFields = "a.AreaZip";
                        break;
                    case "StatusName":
                        sortDataFields = "bd.StatusName";
                        break;
                    case "atAreaName":
                        sortDataFields = "at.AreaName";
                        break;
                }

                orderStr = string.Format("{0} {1}", sortDataFields, sortOrders);
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Data.BLL.AreaBLL areaBLL = new NFMT.Data.BLL.AreaBLL();
            NFMT.Common.SelectModel select = areaBLL.GetSelectModel(pageIndex, pageSize, orderStr, status, key);
            NFMT.Common.ResultModel result = areaBLL.Load(user, select);

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