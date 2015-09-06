using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// RoleMenuListHandler 的摘要说明
    /// </summary>
    public class RoleMenuListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int roleId = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            if (!string.IsNullOrEmpty(context.Request["roleId"]))
                int.TryParse(context.Request["roleId"], out roleId);//角色查询条件

            if (!string.IsNullOrEmpty(context.Request.QueryString["page"]))
                int.TryParse(context.Request.QueryString["page"].Trim(), out pageIndex);

            if (!string.IsNullOrEmpty(context.Request.QueryString["rows"]))
                int.TryParse(context.Request.QueryString["rows"].Trim(), out pageSize);

            //jquery ui datagrid
            if (!string.IsNullOrEmpty(context.Request.QueryString["sord"]) && !string.IsNullOrEmpty(context.Request.QueryString["sidx"]))
                orderStr = string.Format("{0} {1}", context.Request.QueryString["sidx"].Trim(), context.Request.QueryString["sord"].Trim());

            NFMT.User.BLL.RoleMenuBLL roleMenuBLL = new NFMT.User.BLL.RoleMenuBLL();
            NFMT.Common.SelectModel select = roleMenuBLL.GetSelectModel(pageIndex, pageSize, orderStr, roleId);
            NFMT.Common.ResultModel result = roleMenuBLL.Load(user, select);

            context.Response.ContentType = "text/plain";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //jquery ui 
            decimal pageCount = Math.Ceiling((decimal)result.AffectCount / select.PageSize);
            dic.Add("total", pageCount);
            dic.Add("page", select.PageIndex);
            dic.Add("records", result.AffectCount);
            dic.Add("rows", dt);

            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(dic, new Newtonsoft.Json.Converters.DataTableConverter());

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