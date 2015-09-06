using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// FuturesCodeDDLHandler 的摘要说明
    /// </summary>
    public class FuturesCodeDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string jsonStr = string.Empty;

            int exchangeId = 0;
            if (!string.IsNullOrEmpty(context.Request["exId"]))
                int.TryParse(context.Request["exId"], out exchangeId);

            if (exchangeId == 0)
                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(NFMT.Data.BasicDataProvider.FuturesCodes.Where(a => a.FuturesCodeStatus == NFMT.Common.StatusEnum.已生效));
            else if (exchangeId > 0)
                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(NFMT.Data.BasicDataProvider.FuturesCodes.Where(a => a.ExchageId == exchangeId && a.FuturesCodeStatus == NFMT.Common.StatusEnum.已生效));
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