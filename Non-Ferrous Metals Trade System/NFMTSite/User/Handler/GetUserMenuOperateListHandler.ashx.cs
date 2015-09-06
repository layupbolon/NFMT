using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// GetUserMenuOperateListHandler 的摘要说明
    /// </summary>
    public class GetUserMenuOperateListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            string postData = string.Empty;

            int empId = 0;
            if (string.IsNullOrEmpty(context.Request["empId"]) || !int.TryParse(context.Request["empId"], out empId))
                empId = 0;

            string menuIds = context.Request["menuIds"].ToString();

            NFMT.User.BLL.MenuBLL bll = new NFMT.User.BLL.MenuBLL();
            NFMT.Common.ResultModel result = bll.GetMenuOperateList(empId, menuIds);
            if (result.ResultStatus == 0)
                postData = Newtonsoft.Json.JsonConvert.SerializeObject(result.ReturnValue as System.Data.DataTable);

            context.Response.Write(postData);
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