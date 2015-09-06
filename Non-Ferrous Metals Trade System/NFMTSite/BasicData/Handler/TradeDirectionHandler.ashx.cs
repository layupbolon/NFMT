using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// TradeDirectionHandler 的摘要说明
    /// </summary>
    public class TradeDirectionHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<NFMT.Data.Model.BDStyleDetail> tradeDirections = new List<NFMT.Data.Model.BDStyleDetail>();

            try
            {
                NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
                NFMT.Data.DAL.BDStyleDetailDAL dal = new NFMT.Data.DAL.BDStyleDetailDAL();
                NFMT.Common.ResultModel result = dal.LoadTradeDirectionAuth(user);
                if (result.ResultStatus == 0)
                    tradeDirections = result.ReturnValue as List<NFMT.Data.Model.BDStyleDetail>;
            }
            catch
            {
                tradeDirections = new List<NFMT.Data.Model.BDStyleDetail>();
            }

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(tradeDirections);
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