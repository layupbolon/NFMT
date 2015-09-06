using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// AreaGreateHandler 的摘要说明
    /// </summary>
    public class AreaGreateHandler : IHttpHandler
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

            string resultStr = "添加失败";

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
                if (string.IsNullOrEmpty(areaZip))
                {
                    resultStr = "邮政编号不能为空";
                    context.Response.Write(resultStr);
                    context.Response.End();
                }
            }

            int parentId = 0;
            if (!string.IsNullOrEmpty(context.Request.Form["parentId"]))
                int.TryParse(context.Request.Form["parentId"], out parentId);

            //录入地区信息（add）
            NFMT.Data.BLL.AreaBLL bll = new NFMT.Data.BLL.AreaBLL();
            NFMT.Data.Model.Area area = new NFMT.Data.Model.Area();
            area.AreaName = areaName;
            area.AreaFullName = areaFullName;
            area.AreaShort = areaShort;
            area.AreaCode = areaCode;
            area.AreaZip = areaZip;
            area.ParentID = parentId;

            NFMT.Common.ResultModel result = bll.Insert(user, area);
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