using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// StockContractCreateHandler 的摘要说明
    /// </summary>
    public class StockContractCreateHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string stockLogIds = context.Request.Form["lgs"];

            NFMT.WareHouse.BLL.ContractStockIn_BLL bll = new NFMT.WareHouse.BLL.ContractStockIn_BLL();

            //result = bll.Create(user,

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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