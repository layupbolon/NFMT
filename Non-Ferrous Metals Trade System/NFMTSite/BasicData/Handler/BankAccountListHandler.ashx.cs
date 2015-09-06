using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankAccountListHandler 的摘要说明
    /// </summary>
    public class BankAccountListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                int pageIndex = 1, pageSize = 10;
                string orderStr = string.Empty, whereStr = string.Empty;

                int companyId = 0, bankId = 0, currencyId = 0, status = 0;

                string accountNo = context.Request["a"];

                if (!string.IsNullOrEmpty(context.Request["b"]))
                    int.TryParse(context.Request["b"], out bankId);

                if (!string.IsNullOrEmpty(context.Request["cu"]))
                    int.TryParse(context.Request["cu"], out currencyId);

                if (!string.IsNullOrEmpty(context.Request["c"]))
                    int.TryParse(context.Request["c"], out companyId);

                if (!string.IsNullOrEmpty(context.Request["s"]))
                    int.TryParse(context.Request["s"], out status);

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


                NFMT.Data.BLL.BankAccountBLL bll = new NFMT.Data.BLL.BankAccountBLL();
                NFMT.Common.SelectModel select = bll.GetSelectModel(pageIndex, pageSize, orderStr, companyId, accountNo, bankId, currencyId,status);
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