using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// MUGreateHandler 的摘要说明
    /// </summary>
    public class MUGreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string muName = context.Request.Form["muName"];

            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            if (string.IsNullOrEmpty(muName))
            {
                result.Message = "计量单位名称不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            int baseId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["baseId"]) || !int.TryParse(context.Request.Form["baseId"],out baseId))
                baseId = 0;

            decimal transformRate = 0;
            if (string.IsNullOrEmpty(context.Request.Form["transformRate"]) || !decimal.TryParse(context.Request.Form["transformRate"], out transformRate))
                transformRate = 0;

            //录入计量单位（add）
            NFMT.Data.BLL.MeasureUnitBLL bll = new NFMT.Data.BLL.MeasureUnitBLL();
            NFMT.Data.Model.MeasureUnit measureUnit = new NFMT.Data.Model.MeasureUnit();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            //master.CreatorId = user.EmpId;
            measureUnit.MUName = muName;
            measureUnit.BaseId = baseId;
            measureUnit.TransformRate = transformRate;
            
            result = bll.Insert(user, measureUnit);

            if (result.ResultStatus == 0)
                result.Message = "计量单位添加成功";

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