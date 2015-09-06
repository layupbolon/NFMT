using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ProducerDDLHandler 的摘要说明
    /// </summary>
    public class ProducerDDLHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            //NFMT.Data.BLL.ProducerBLL bll = new NFMT.Data.BLL.ProducerBLL();
            //var result = bll.Load(user);

            //context.Response.ContentType = "application/json; charset=utf-8";
            //if (result.ResultStatus != 0)
            //{
            //    context.Response.Write(result.Message);
            //    context.Response.End();
            //}

            //List<NFMT.Data.Model.Producer> dt = result.ReturnValue as List<NFMT.Data.Model.Producer>;

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(NFMT.Data.BasicDataProvider.Producers.Where(a => a.ProducerStatus == NFMT.Common.StatusEnum.已生效));
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