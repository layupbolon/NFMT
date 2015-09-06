using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// DeliverPlaceUpdateHandler 的摘要说明
    /// </summary>
    public class DeliverPlaceUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string deliverPlaceStr = context.Request.Form["deliverPlace"];
            if (string.IsNullOrEmpty(deliverPlaceStr))
            {
                context.Response.Write("交货地信息不能为空");
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Data.Model.DeliverPlace deliverPlace = serializer.Deserialize<NFMT.Data.Model.DeliverPlace>(deliverPlaceStr);
                if (deliverPlace == null)
                {
                    context.Response.Write("数据错误");
                    context.Response.End();
                }
                NFMT.Data.BLL.DeliverPlaceBLL bll = new NFMT.Data.BLL.DeliverPlaceBLL();
                result = bll.Update(user, deliverPlace);
                if (result.ResultStatus == 0)
                {
                    result.Message = "交货地修改成功";
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

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