using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BrandUpdateHandler 的摘要说明
    /// </summary>
    public class BrandUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string brandName = context.Request.Form["brandName"];
            string brandFullName = context.Request.Form["brandFullName"];
            string brandInfo = context.Request.Form["brandInfo"];
            int producerName = 0;
            int id = 0;
            int brandStatusName = 0;

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
            if (!int.TryParse(context.Request.Form["producerName"], out producerName))
            {
                resultStr = "生产商名称序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (!int.TryParse(context.Request.Form["brandStatusName"], out brandStatusName))
            {
                resultStr = "品牌状态序号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(brandName))
            {
                resultStr = "品牌名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(brandFullName))
            {
                resultStr = "品牌全称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(brandInfo))
            {
                resultStr = "品牌备注不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }


            NFMT.Data.BLL.BrandBLL brandBLL = new NFMT.Data.BLL.BrandBLL();
            NFMT.Data.Model.Brand brand = new NFMT.Data.Model.Brand()
            {
                BrandName = brandName,
                BrandFullName = brandFullName,
                BrandInfo = brandInfo,
                BrandId = id,
                ProducerId = producerName,
                BrandStatus = (NFMT.Common.StatusEnum)brandStatusName
            };

            NFMT.Common.ResultModel result = brandBLL.Update(user, brand);
            if (result.ResultStatus == 0)
                context.Response.Write("修改成功");
            else
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