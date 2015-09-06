using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// BrandGreateHandler 的摘要说明
    /// </summary>
    public class BrandGreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string brandName = context.Request.Form["brandName"];
            string brandFullName = context.Request.Form["brandFullName"];
            string brandInfo = context.Request.Form["brandInfo"];
            string producerName = context.Request.Form["producerName"];
            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(brandName))
            {
                resultStr = "品牌名称不可为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(brandFullName))
            {
                resultStr = "品牌全称不可为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(brandInfo))
            {
                resultStr = "品牌备注不可为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(producerName))
            {
                resultStr = "生产商名称";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            NFMT.Data.BLL.BrandBLL bdBLL = new NFMT.Data.BLL.BrandBLL();
            NFMT.Data.Model.Brand bd = new NFMT.Data.Model.Brand();
            bd.BrandName = brandName;
            bd.BrandFullName = brandFullName;
            bd.BrandInfo = brandInfo;
            bd.ProducerId = Convert.ToInt32(producerName);
            bd.BrandStatus = NFMT.Common.StatusEnum.已录入;

            NFMT.Common.ResultModel result = bdBLL.Insert(user, bd);
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