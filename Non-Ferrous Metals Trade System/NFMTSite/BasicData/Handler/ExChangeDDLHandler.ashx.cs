using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NFMT.Common;
using NFMT.Data;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ExChangeDDLHandler 的摘要说明
    /// </summary>
    public class ExChangeDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string jsonStr = JsonConvert.SerializeObject(BasicDataProvider.Exchanges.Where(a => a.ExchangeStatus == StatusEnum.已生效));
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