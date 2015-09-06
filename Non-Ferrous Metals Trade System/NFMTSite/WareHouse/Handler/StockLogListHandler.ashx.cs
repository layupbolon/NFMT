using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// StockLogListHandler 的摘要说明
    /// </summary>
    public class StockLogListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            int stockId = 0;
            if(string.IsNullOrEmpty(context.Request["si"]))
            {
                context.Response.Write("库存序号错误");
                context.Response.End();
            }
            if (!int.TryParse(context.Request["si"], out stockId))
            {
                context.Response.Write("库存序号错误");
                context.Response.End();
            }

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
                    case "LogDate":
                        sortDataField = string.Format("sl.{0}", sortDataField);
                        break;
                    case "DetailName":
                        sortDataField = "sl.LogType";
                        break;
                    case "Name":
                        sortDataField = "emp.Name";
                        break;
                    case "Memo":
                        sortDataField = "sl.Memo";
                        break;
                }
                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }           

            NFMT.WareHouse.BLL.StockLogBLL stockLogBLL = new NFMT.WareHouse.BLL.StockLogBLL();
            NFMT.Common.SelectModel select = stockLogBLL.GetLogListByStockIdSelect(pageIndex, pageSize, orderStr,stockId);
            NFMT.Common.ResultModel result = stockLogBLL.Load(new NFMT.Common.UserModel(), select);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("count", totalRows);
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