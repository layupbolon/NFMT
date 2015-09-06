using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractMasterUpdateHandler 的摘要说明
    /// </summary>
    public class ContractMasterUpdateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string masterName = context.Request.Form["mn"];
            string masterEname = context.Request.Form["men"];
            int masterStatus = 0;
            int masterId = 0;

            string resultStr = "添加失败";

            if (string.IsNullOrEmpty(masterName))
            {
                resultStr = "合约模板名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (string.IsNullOrEmpty(masterEname))
            {
                resultStr = "合约模板英文名称不能为空";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["ms"], out masterStatus) || masterStatus <= 0)
            {
                resultStr = "必须选择数据状态";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["mid"], out masterId) || masterId <= 0)
            {
                resultStr = "模板序号错误";
                context.Response.Write(resultStr);
                context.Response.End();
            }

            NFMT.Data.BLL.ContractMasterBLL bll = new NFMT.Data.BLL.ContractMasterBLL();
            NFMT.Data.Model.ContractMaster master = new NFMT.Data.Model.ContractMaster();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            master.LastModifyId = user.EmpId;
            master.MasterName = masterName;
            master.MasterEname = masterEname;
            master.MasterId = masterId;
            master.MasterStatus = (NFMT.Common.StatusEnum)masterStatus;

            NFMT.Common.ResultModel result = bll.Update(user, master);
            resultStr = result.Message;

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