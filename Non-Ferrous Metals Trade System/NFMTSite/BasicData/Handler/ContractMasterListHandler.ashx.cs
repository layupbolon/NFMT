using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractMasterListHandler 的摘要说明
    /// </summary>
    public class ContractMasterListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            int status = -1;
            int pageIndex = 1, pageSize = 10;
            string orderStr = string.Empty, whereStr = string.Empty;
            string sortDataField = string.Empty;
            string sortOrder = string.Empty;

            NFMT.Data.BLL.ContractMasterBLL bll = new NFMT.Data.BLL.ContractMasterBLL();
            NFMT.Common.ResultModel result = bll.Load<NFMT.Data.Model.ContractMaster>(Utility.UserUtility.CurrentUser);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            List<NFMT.Data.Model.ContractMaster> masters = result.ReturnValue as List<NFMT.Data.Model.ContractMaster>;
            //List<NFMT.Data.Model.ContractMaster> masters = NFMT.Data.BasicDataProvider.ContractMasters;
            

            string masterName = context.Request["n"];//模糊搜索
            if (!string.IsNullOrEmpty(masterName))
                masters = masters.Where(temp => temp.MasterName.Contains(masterName)).ToList();

            if (!string.IsNullOrEmpty(context.Request["s"]))
            {
                if (int.TryParse(context.Request["s"], out status) && status > 0)
                    masters = masters.Where(temp => (int)temp.MasterStatus == status).ToList();
            }

            if (!string.IsNullOrEmpty(context.Request.QueryString["pagenum"]))
                int.TryParse(context.Request.QueryString["pagenum"], out pageIndex);
            if (!string.IsNullOrEmpty(context.Request.QueryString["pagesize"]))
                int.TryParse(context.Request.QueryString["pagesize"], out pageSize);

            if (!string.IsNullOrEmpty(context.Request.QueryString["sortdatafield"]) && !string.IsNullOrEmpty(context.Request.QueryString["sortorder"]))
            {
                sortDataField = context.Request.QueryString["sortdatafield"].Trim();
                sortOrder = context.Request.QueryString["sortorder"].Trim();

                if (sortDataField == "MasterStatusName")
                {
                    if (sortOrder.ToLower() == "asc")
                        masters = (from m in masters orderby m.MasterStatus ascending select m).ToList();
                    else if (sortOrder.ToLower() == "desc")
                        masters = (from m in masters orderby m.MasterStatus descending select m).ToList();
                }
                else if (sortDataField == "MasterName")
                {
                    if (sortOrder.ToLower() == "asc")
                        masters = (from m in masters orderby m.MasterName ascending select m).ToList();
                    else if (sortOrder.ToLower() == "desc")
                        masters = (from m in masters orderby m.MasterName descending select m).ToList();
                }
                else if (sortDataField == "MasterEname")
                {
                    if (sortOrder.ToLower() == "asc")
                        masters = (from m in masters orderby m.MasterEname ascending select m).ToList();
                    else if (sortOrder.ToLower() == "desc")
                        masters = (from m in masters orderby m.MasterEname descending select m).ToList();
                }
            }

            masters = (from m in masters orderby m.MasterId descending select m).ToList();

            int count = masters.Count;

            context.Response.ContentType = "application/json; charset=utf-8";
            int pageStart = pageIndex * pageSize;
            masters = masters.Skip(pageStart).Take(pageSize).ToList();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("count", count);
            dic.Add("data", masters);

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
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