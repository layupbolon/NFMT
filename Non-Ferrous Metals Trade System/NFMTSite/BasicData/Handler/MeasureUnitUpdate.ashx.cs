using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MeasureUnitUpdate 的摘要说明
    /// </summary>
    public class MeasureUnitUpdate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            string muName = context.Request.Form["muName"];
            string transformRate = context.Request.Form["transformRate"];
            string baseId = context.Request.Form["baseId"];
            int corpId = 0;
            int id = 0;

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

            if (string.IsNullOrEmpty(muName))
            {
                resultStr = "联系人姓名不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["corpId"]))
            {
                if (!int.TryParse(context.Request.Form["corpId"], out corpId))
                {
                    resultStr = "联系人公司转换错误";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            NFMT.Data.BLL.MeasureUnitBLL muBLL = new NFMT.Data.BLL.MeasureUnitBLL();
            NFMT.Common.ResultModel result = muBLL.Get(user, id);
            if (result.ResultStatus != 0)
            {
                resultStr = "获取数据错误";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            NFMT.Data.Model.MeasureUnit contact = result.ReturnValue as NFMT.Data.Model.MeasureUnit;
            if (contact != null)
            {
                contact.MUName = muName;
                //contact.TransformRate = transformRate;
                //contact.BaseId = baseId;
                //contact.CompanyId = corpId;

                result = muBLL.Update(user, contact);
                resultStr = result.Message;
            }

            context.Response.Write(resultStr);
            context.Response.End();
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