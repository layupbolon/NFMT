using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.BasicData.Handler
{
    /// <summary>
    /// ContractMasterStatusHandler 的摘要说明
    /// </summary>
    public class ContractMasterStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int masterId = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["mi"], out masterId) || masterId <= 0)
            {
                context.Response.Write( "模板序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Data.BLL.ContractMasterBLL bll = new NFMT.Data.BLL.ContractMasterBLL();
            NFMT.Data.Model.ContractMaster master = new NFMT.Data.Model.ContractMaster();
            master.LastModifyId = user.EmpId;
            master.MasterId = masterId;

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.冻结:
                    result = bll.Freeze(user, master);
                    break;
                case NFMT.Common.OperateEnum.解除冻结:
                    result = bll.UnFreeze(user, master);
                    break;
            }

            context.Response.Write( result.Message);
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