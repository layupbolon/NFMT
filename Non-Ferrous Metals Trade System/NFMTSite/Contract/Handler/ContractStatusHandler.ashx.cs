using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.Contract.Handler
{
    /// <summary>
    /// ContractStatusHandler 的摘要说明
    /// </summary>
    public class ContractStatusHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";
            int id = 0;
            int operateId = 0;

            if (!int.TryParse(context.Request.Form["id"], out id) || id <= 0)
            {
                context.Response.Write("序号错误");
                context.Response.End();
            }

            if (!int.TryParse(context.Request.Form["oi"], out operateId) || operateId <= 0)
            {
                context.Response.Write("操作错误");
                context.Response.End();
            }

            NFMT.Contract.BLL.ContractBLL contractBLL = new NFMT.Contract.BLL.ContractBLL();
            NFMT.Contract.Model.Contract contract = new NFMT.Contract.Model.Contract()
            {
                LastModifyId = user.EmpId,
                ContractId = id
            };

            NFMT.Common.OperateEnum operateEnum = (NFMT.Common.OperateEnum)operateId;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            switch (operateEnum)
            {
                case NFMT.Common.OperateEnum.作废:
                    result = contractBLL.Invalid(user, contract);
                    break;
                case NFMT.Common.OperateEnum.撤返:
                    result = contractBLL.GoBack(user, contract);
                    break;
                case NFMT.Common.OperateEnum.执行完成:
                    result = contractBLL.Complete(user, id);
                    break;
                case NFMT.Common.OperateEnum.执行完成撤销:
                    result = contractBLL.CompleteCancel(user, id);
                    break;
            }

            if (result.ResultStatus == 0)
                result.Message = string.Format("{0}成功",operateEnum.ToString());

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