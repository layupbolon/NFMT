using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BrandDDLHandler 的摘要说明
    /// </summary>
    public class BrandDDLHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string jsonStr = string.Empty;

            int pid = 0;
            if (!string.IsNullOrEmpty(context.Request["pid"]))
                int.TryParse(context.Request["pid"], out pid);

            var brands = NFMT.Data.BasicDataProvider.Brands.Where(temp => temp.BrandStatus == NFMT.Common.StatusEnum.已生效);

            if (pid > 0)
                brands = NFMT.Data.BasicDataProvider.Brands.Where(a => a.ProducerId == pid);

            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(brands.ToList());
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