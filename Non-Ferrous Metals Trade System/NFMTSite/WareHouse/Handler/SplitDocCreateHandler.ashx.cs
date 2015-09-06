using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WareHouse.Handler
{
    /// <summary>
    /// SplitDocCreateHandler 的摘要说明
    /// </summary>
    public class SplitDocCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            int stockId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["stockId"]) || !int.TryParse(context.Request.Form["stockId"], out stockId) || stockId <= 0)
            {
                context.Response.Write("库存序号错误");
                context.Response.End();
            }

            string splitDetailStr = context.Request.Form["splitDetail"];
            if (string.IsNullOrEmpty(splitDetailStr))
            {
                context.Response.Write("拆单内容不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<NFMT.WareHouse.Model.SplitDocDetail> splitDocDetails = serializer.Deserialize<List<NFMT.WareHouse.Model.SplitDocDetail>>(splitDetailStr);
                if (splitDocDetails == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.WareHouse.BLL.SplitDocBLL bll = new NFMT.WareHouse.BLL.SplitDocBLL();
                result = bll.Create(user, stockId, splitDocDetails);
                if (result.ResultStatus == 0)
                {
                    result.Message = "拆单成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
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