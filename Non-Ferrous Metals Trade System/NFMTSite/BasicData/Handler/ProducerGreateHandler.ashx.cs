using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ProducerGreateHandler 的摘要说明
    /// </summary>
    public class ProducerGreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string producerName = context.Request.Form["producerName"];
            string producerFullName = context.Request.Form["producerFullName"];
            string producerShort = context.Request.Form["producerShort"];
            string areaName = context.Request.Form["areaName"];
            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(producerName))
            {
                resultStr = "生产商名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(producerFullName))
            {
                resultStr = "生产商全称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(producerShort))
            {
                resultStr = "生产商简称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(areaName))
            {
                resultStr = "生产商地区不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.Data.BLL.ProducerBLL pdBLL = new NFMT.Data.BLL.ProducerBLL();
            NFMT.Data.Model.Producer pd = new NFMT.Data.Model.Producer();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            pd.ProducerName = producerName;
            pd.ProducerFullName = producerFullName;
            pd.ProducerShort = producerShort;
            pd.ProducerArea = Convert.ToInt32(areaName);


            pd.ProducerStatus = NFMT.Common.StatusEnum.已录入;


            NFMT.Common.ResultModel result = pdBLL.Insert(user, pd);
            resultStr = result.Message;
            context.Response.Write(resultStr);
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