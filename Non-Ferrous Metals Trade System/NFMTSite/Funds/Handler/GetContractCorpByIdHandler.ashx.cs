using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Funds.Handler
{
    /// <summary>
    /// GetContractCorpByIdHandler 的摘要说明
    /// </summary>
    public class GetContractCorpByIdHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int subId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["subId"]) || !int.TryParse(context.Request.Form["subId"], out subId) || subId <= 0)
            {
                context.Response.Write("子合约信息错误");
                context.Response.End();
            }

            NFMT.Contract.BLL.ContractSubBLL bll = new NFMT.Contract.BLL.ContractSubBLL();
            NFMT.Common.ResultModel result = bll.GetContractOutCorp(user, subId);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
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