using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// DeptListHandler 的摘要说明
    /// </summary>
    public class DeptListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int status = -1, corpId = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string deptName = context.Request["k"];//deptName模糊搜索                

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);//状态查询条件

            if (!string.IsNullOrEmpty(context.Request["corpId"]))
                int.TryParse(context.Request["corpId"], out corpId);//状态查询条件

            //jqwidgets jqxGrid
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            pageIndex++;
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                string sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                string sortOrder = context.Request.QueryString["sortorder"].Trim();

                switch (sortDataField)
                {
                    case "DeptName":
                        sortDataField = string.Format("D.{0}", sortDataField);
                        break;
                    case "DeptFullName":
                        sortDataField = string.Format("D.{0}", sortDataField);
                        break;
                    case "DeptShort":
                        sortDataField = string.Format("D.{0}", sortDataField);
                        break;
                    case "DeptType":
                        sortDataField = string.Format("D.{0}", sortDataField);
                        break;
                    case "ParentDeptName":
                        sortDataField = "D2.DeptName";
                        break;
                    case "StatusName":
                        sortDataField = "D.DeptStatus";
                        break;
                    case "DeptLevel":
                         sortDataField = string.Format("D.{0}", sortDataField);
                        break;
                    case "CorpName":
                        sortDataField = "C.CorpName";
                        break;
                    case "DeptTypeName":
                        sortDataField = "D.DeptType";
                        break;
                }

                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.User.BLL.DepartmentBLL departmentBLL = new NFMT.User.BLL.DepartmentBLL();
            NFMT.Common.SelectModel select = departmentBLL.GetSelectModel(pageIndex, pageSize, orderStr, status, deptName, corpId);
            NFMT.Common.ResultModel result = departmentBLL.Load(user, select);

            context.Response.ContentType = "application/json; charset=utf-8";
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            int totalRows = result.AffectCount;
            System.Data.DataTable dt = result.ReturnValue as System.Data.DataTable;
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //jqwidgets
            dic.Add("count", result.AffectCount);
            dic.Add("data", dt);

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dic, new Newtonsoft.Json.Converters.DataTableConverter());
            context.Response.Write(jsonStr);
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