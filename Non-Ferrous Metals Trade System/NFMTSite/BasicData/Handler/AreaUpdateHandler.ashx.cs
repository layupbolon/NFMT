using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// AreaUpdateHandler 的摘要说明
    /// </summary>
    public class AreaUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            context.Response.ContentType = "text/plain";

            string areaName = context.Request.Form["areaName"];
            string areaFullName = context.Request.Form["areaFullName"];
            string areaShort = context.Request.Form["areaShort"];
            string areaCode = context.Request.Form["areaCode"];
            string areaZip = context.Request.Form["areaZip"];

            int areaStatus = 0;
            int id = 0;
            int parentId = 0;

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

            if (!int.TryParse(context.Request.Form["areaStatus"], out areaStatus))
            {
                resultStr = "类型状态id为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!string.IsNullOrEmpty(context.Request.Form["parentId"]))
                int.TryParse(context.Request.Form["parentId"], out parentId);

            if (string.IsNullOrEmpty(areaName))
            {
                resultStr = "地区名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(areaFullName))
            {
                resultStr = "地区全称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(areaShort))
            {
                resultStr = "地区缩写不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(areaCode))
            {
                resultStr = "电话区号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(areaZip))
            {
                resultStr = "邮政编号不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }


            NFMT.Data.BLL.AreaBLL bll = new NFMT.Data.BLL.AreaBLL();
            NFMT.Data.Model.Area area = new NFMT.Data.Model.Area()
            {
                AreaName = areaName,
                AreaId = id,
                AreaFullName = areaFullName,
                AreaShort = areaShort,
                AreaCode = areaCode,
                AreaZip = areaZip,
                ParentID = parentId,
                AreaStatus = (NFMT.Common.StatusEnum)areaStatus,
            };
            NFMT.Common.ResultModel result = bll.Update(user, area);
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