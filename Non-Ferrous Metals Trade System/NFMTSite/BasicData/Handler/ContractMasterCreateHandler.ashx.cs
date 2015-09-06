using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractMasterCreateHandler 的摘要说明
    /// </summary>
    public class ContractMasterCreateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string masterName = context.Request.Form["mn"];
            string masterEname = context.Request.Form["men"];

            if (string.IsNullOrEmpty(masterName))
            {
                result.Message = "合约模板名称不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(masterEname))
            {
                result.Message = "合约模板英文名称不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            NFMT.Data.BLL.ContractMasterBLL bll = new NFMT.Data.BLL.ContractMasterBLL();
            NFMT.Data.Model.ContractMaster master = new NFMT.Data.Model.ContractMaster();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            master.CreatorId = user.EmpId;
            master.MasterName = masterName;
            master.MasterEname = masterEname;
            master.MasterStatus = NFMT.Common.StatusEnum.已录入;

            result = bll.Insert(user, master);

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