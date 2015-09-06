using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// CorpListHandler 的摘要说明
    /// </summary>
    public class CorpListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            int status = -1, blocId = 0;
            int pageIndex = 0, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;

            string CorpCode = context.Request["Code"];//公司代码     
            string CorpName = context.Request["Name"];//公司名称   

            if (!string.IsNullOrEmpty(context.Request["s"]))
                int.TryParse(context.Request["s"], out status);//状态查询条件

            if (!string.IsNullOrEmpty(context.Request["blocId"]))
                int.TryParse(context.Request["blocId"], out blocId);//归属集团查询条件

            int isSelf = -1;
            if (!string.IsNullOrEmpty(context.Request["self"]))
            {
                if (!int.TryParse(context.Request["self"], out isSelf))
                    isSelf = -1;
            }

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
                    case "IsSelf":
                        sortDataField = "C.IsSelf";
                        break;
                }

                orderStr = string.Format("{0} {1}", sortDataField, sortOrder);
            }

            NFMT.User.BLL.CorporationBLL corporationBLL = new NFMT.User.BLL.CorporationBLL();
            NFMT.Common.SelectModel select = corporationBLL.GetSelectModel(pageIndex, pageSize, orderStr, status, CorpCode, CorpName, blocId, isSelf);
            NFMT.Common.ResultModel result = corporationBLL.Load(user, select);

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