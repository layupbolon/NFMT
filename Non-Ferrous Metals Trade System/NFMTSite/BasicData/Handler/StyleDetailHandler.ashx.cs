using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// StyleDetailHandler 的摘要说明
    /// </summary>
    public class StyleDetailHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                int status = (int)NFMT.Common.StatusEnum.已生效;

                int styleId = 0;
                if (!string.IsNullOrEmpty(context.Request.QueryString["id"]))
                    int.TryParse(context.Request.QueryString["id"], out styleId);

                NFMT.Data.StyleEnum style = (NFMT.Data.StyleEnum)styleId;
                NFMT.Data.DetailCollection details = NFMT.Data.DetailProvider.Details(style);

                List<NFMT.Data.Model.BDStyleDetail> ds = details.Where(temp=>(int)temp.DetailStatus == status && temp.DetailStatus == NFMT.Common.StatusEnum.已生效).ToList();

                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(ds);
                context.Response.Write(jsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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