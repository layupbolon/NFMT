using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// RateListHandler 的摘要说明
    /// </summary>
    public class RateListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //int status = -1;
                int pageIndex = 1, pageSize = 10;
                string orderStr = string.Empty, whereStr = string.Empty;

                int currency1 =0;
                if (string.IsNullOrEmpty(context.Request["c1"]) || !int.TryParse(context.Request["c1"], out currency1))
                    currency1 = 0;

                int currency2 = 0;
                if (string.IsNullOrEmpty(context.Request["c2"]) || !int.TryParse(context.Request["c2"], out currency2))
                    currency2 = 0;

                DateTime rateDate = NFMT.Common.DefaultValue.DefaultTime;
                if (string.IsNullOrEmpty(context.Request["rd"]) || !DateTime.TryParse(context.Request["rd"], out rateDate))
                    rateDate = NFMT.Common.DefaultValue.DefaultTime;

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
                        case "CreateTime":
                            sortDataField = "r.CreateTime";
                            break;
                        case "CurrencyName_1":
                            sortDataField = "c1.CurrencyName";
                            break;
                        case "CurrencyName_2":
                            sortDataField = "c2.CurrencyName";
                            break;
                    }

                    orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
                }

                NFMT.Data.BLL.RateBLL bll = new NFMT.Data.BLL.RateBLL();
                NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, rateDate, currency1, currency2);
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