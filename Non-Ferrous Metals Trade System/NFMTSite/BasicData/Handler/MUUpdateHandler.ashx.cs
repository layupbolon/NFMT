using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MUUpdateHandler 的摘要说明
    /// </summary>
    public class MUUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";
            string mu = context.Request.Form["measureUnit"];
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(mu))
            {
                result.Message = "修改数据不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                NFMT.Data.Model.MeasureUnit measureUnit = serializer.Deserialize<NFMT.Data.Model.MeasureUnit>(mu);
                if (measureUnit == null)
                {
                    result.Message = "数据错误";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
                NFMT.Data.BLL.MeasureUnitBLL bll = new NFMT.Data.BLL.MeasureUnitBLL();
                result = bll.Update(user, measureUnit);
                if (result.ResultStatus == 0)
                {
                    result.Message = "修改成功";
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