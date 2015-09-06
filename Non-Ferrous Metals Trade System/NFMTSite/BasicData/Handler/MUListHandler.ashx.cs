using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MUListHandler 的摘要说明
    /// </summary>
    public class MUListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
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
                    string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                    string sortOrder = context.Request.QueryString["sortorder"].Trim();

                    switch(sortDataField)
                    {
                        case "MUName":
                            sortDataField = "mu.MUName";
                            break;
                        case "BaseName":
                            sortDataField = "bmu.MUName";
                            break;
                        case "MUStatusName":
                            sortDataField = "mu.MUStatus";
                            break;
                        case "TransformRate":
                            sortDataField = "mu.TransformRate";
                            break;
                        default:
                            sortDataField = "mu.MUId";
                            break;
                    }

                    orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
                }

                NFMT.Data.BLL.MeasureUnitBLL bll = new NFMT.Data.BLL.MeasureUnitBLL();
                NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, status, key);
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