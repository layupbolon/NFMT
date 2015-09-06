using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// CashInAllotMainContractStockListHandler 的摘要说明
    /// </summary>
    public class CashInAllotMainContractStockListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int subContractId = 0;
            if (string.IsNullOrEmpty(context.Request["cid"]) || !int.TryParse(context.Request["cid"], out subContractId) || subContractId <= 0)
                subContractId = 0;

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Funds.BLL.CashInAllotBLL bll = new NFMT.Funds.BLL.CashInAllotBLL();
            NFMT.Common.ResultModel result = bll.GetContractStock(user, subContractId);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("count", result.AffectCount);
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