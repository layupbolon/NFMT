using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MUDDLHandler（计量名称的绑定）
    /// </summary>
    public class MUDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(NFMT.Data.BasicDataProvider.MeasureUnits.Where(a => a.MUStatus == NFMT.Common.StatusEnum.已生效));
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