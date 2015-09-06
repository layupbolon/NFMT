using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractLimitHandler 的摘要说明
    /// </summary>
    public class ContractLimitHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<NFMT.Data.Model.BDStyleDetail> contractLimits = new List<NFMT.Data.Model.BDStyleDetail>();

            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Data.DAL.BDStyleDetailDAL dal = new NFMT.Data.DAL.BDStyleDetailDAL();
                NFMT.Common.ResultModel result = dal.LoadContractLimitAuth(user);
                if (result.ResultStatus == 0)
                    contractLimits = result.ReturnValue as List<NFMT.Data.Model.BDStyleDetail>;
            }
            catch
            {
                contractLimits = new List<NFMT.Data.Model.BDStyleDetail>();
            }

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(contractLimits);
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