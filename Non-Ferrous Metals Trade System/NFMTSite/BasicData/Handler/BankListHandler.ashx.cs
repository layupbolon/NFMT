using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BankListHandler 的摘要说明
    /// </summary>
    public class BankListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int status = -1;
            int pageIndex = 1, pageSize = 10;
            int bankeStatus = 0;
            int capitalType = 0;

            string orderStr = string.Empty, whereStr = string.Empty;

            string key = context.Request["k"];//模糊搜索            

            string bankEname = context.Request["bankEname"];//模糊搜索英文名称
            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);

            if (!string.IsNullOrEmpty(context.Request["bankeStatus"]))
                int.TryParse(context.Request["bankeStatus"], out bankeStatus);

            if (!string.IsNullOrEmpty(context.Request["capitalType"]))
                int.TryParse(context.Request["capitalType"], out capitalType);


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
                    case "BankName":
                        sortDataFields = "B.BankName";
                        break;
                    case "BankEname":
                        sortDataFields = "B.BankEname";
                        break;
                    case "BankFullName":
                        sortDataFields = "B.BankFullName";
                        break;
                    case "BankShort":
                        sortDataFields = "B.BankShort";
                        break;
                    case "ParentBankName":
                        sortDataFields = "bt.BankName";
                        break;
                }

                orderStr = string.Format("{0} {1}", sortDataFields, sortOrders);
            }

            NFMT.Data.BLL.BankBLL bkBLL = new NFMT.Data.BLL.BankBLL();
            NFMT.Common.SelectModel select = bkBLL.GetSelectModel(pageIndex, pageSize, orderStr, status, key, bankeStatus, capitalType, bankEname);
            NFMT.Common.ResultModel result = bkBLL.Load(new NFMT.Common.UserModel(), select);

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