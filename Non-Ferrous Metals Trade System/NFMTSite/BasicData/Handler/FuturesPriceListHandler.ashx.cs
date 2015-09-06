using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// FuturesPriceListHandler 的摘要说明
    /// </summary>
    public class FuturesPriceListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //int status = -1;
                int pageIndex = 1, pageSize = 10;
                string orderStr = string.Empty, whereStr = string.Empty;
                string tradeCode = context.Request["tradeCode"];//交易代码    

                DateTime tradeDate = NFMT.Common.DefaultValue.DefaultTime; 
                DateTime deliverDate = NFMT.Common.DefaultValue.DefaultTime;

                if (!string.IsNullOrEmpty(context.Request["tradeDate"]))//交易日      
                {
                    if (!DateTime.TryParse(context.Request["tradeDate"], out tradeDate))
                        tradeDate = NFMT.Common.DefaultValue.DefaultTime;
                }
                if (!string.IsNullOrEmpty(context.Request["deliverDate"]))//交割日
                {
                    if (!DateTime.TryParse(context.Request["deliverDate"], out deliverDate))
                        deliverDate = NFMT.Common.DefaultValue.DefaultTime;
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


                NFMT.Data.BLL.FuturesPriceBLL bll = new NFMT.Data.BLL.FuturesPriceBLL();
                NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, tradeDate, deliverDate, tradeCode);
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