using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ProducerUpdateHandler 的摘要说明
    /// </summary>
    public class ProducerUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string producerName = context.Request.Form["producerName"];
            string producerFullName = context.Request.Form["producerFullName"];
            string producerShort = context.Request.Form["producerShort"];
            int areaName = 0;
            int id = 0;
            int statusName = 0;

            string resultStr = "修改失败";

            if (string.IsNullOrEmpty(context.Request.Form["id"]))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["id"], out id))
            {
                resultStr = "id未知";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["areaName"], out areaName))
            {
                resultStr = "地区名序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["statusName"], out statusName))
            {
                resultStr = "生产商状态序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

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
                resultStr = "生产商缩写不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }


            NFMT.Data.BLL.ProducerBLL producerBLL = new NFMT.Data.BLL.ProducerBLL();
            NFMT.Data.Model.Producer producer = new NFMT.Data.Model.Producer()
            {
                ProducerName = producerName,
                ProducerFullName = producerFullName,
                ProducerShort = producerShort,
                ProducerId = id,
                ProducerArea = areaName,
                ProducerStatus = (NFMT.Common.StatusEnum)statusName
            };
            NFMT.Common.ResultModel result = producerBLL.Update(user, producer);
            if (result.ResultStatus == 0)
                context.Response.Write("修改成功");
            else
                context.Response.Write(result.Message);
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