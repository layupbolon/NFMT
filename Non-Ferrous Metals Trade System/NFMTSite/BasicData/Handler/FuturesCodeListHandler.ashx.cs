using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// FuturesCodeListHandler 的摘要说明
    /// </summary>
    public class FuturesCodeListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                int selFuturesCodeStatus = 0;
                int exchageId = 0;
               if (!string.IsNullOrEmpty(context.Request.QueryString["selFuturesCodeStatus"]))//合约状态
                int.TryParse(context.Request.QueryString["selFuturesCodeStatus"].Trim(), out selFuturesCodeStatus);
             
               if (!string.IsNullOrEmpty(context.Request.QueryString["exchageId"]))//交易所
                int.TryParse(context.Request.QueryString["exchageId"].Trim(), out exchageId);

                int pageIndex = 1, pageSize = 10;
                string orderStr = string.Empty, whereStr = string.Empty;

                DateTime firstTradeDate = NFMT.Common.DefaultValue.DefaultTime;
                DateTime lastTradeDate = NFMT.Common.DefaultValue.DefaultTime;

                if (!string.IsNullOrEmpty(context.Request["firstTradeDate"]))//交易起始日期      
                {
                    if (!DateTime.TryParse(context.Request["firstTradeDate"], out firstTradeDate))
                        firstTradeDate = NFMT.Common.DefaultValue.DefaultTime;
                }
                if (!string.IsNullOrEmpty(context.Request["lastTradeDate"]))//交易终止日期
                {
                    if (!DateTime.TryParse(context.Request["lastTradeDate"], out lastTradeDate))
                        lastTradeDate = NFMT.Common.DefaultValue.DefaultTime;
                }

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

                NFMT.Data.BLL.FuturesCodeBLL bll = new NFMT.Data.BLL.FuturesCodeBLL();
                NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, firstTradeDate, lastTradeDate, exchageId, selFuturesCodeStatus);
                NFMT.Common.ResultModel result = bll.Load(new NFMT.Common.UserModel(), select);

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