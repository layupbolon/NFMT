using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Funds.TaskProvider
{
    public class PayApplyTaskProvider : NFMT.WorkFlow.ITaskProvider
    {
        public Common.ResultModel Create(Common.UserModel user, Common.IModel model)
        {
            NFMT.Common.ResultModel result = new Common.ResultModel();

            try
            {
                WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                NFMT.Operate.Model.Apply apply = model as NFMT.Operate.Model.Apply;

                BLL.PayApplyBLL payApplyBLL = new BLL.PayApplyBLL();
                result = payApplyBLL.GetByApplyId(user, apply.ApplyId);
                if (result.ResultStatus != 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取收款信息失败";
                    return result;
                }
                Model.PayApply payApply = result.ReturnValue as Model.PayApply;

                task.TaskName = "付款申请审核";

                NFMT.User.Model.Corporation corp = NFMT.User.UserProvider.Corporations.SingleOrDefault(a => a.CorpId == payApply.RecCorpId);
                if (corp == null || corp.CorpId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取收款公司失败";
                    return result;
                }

                task.TaskConnext = string.Format("{0} 于 {1} 提交审核。收款公司：{2}，申请金额：{3}", user.EmpName, DateTime.Now.ToString(), corp.CorpName, payApply.ApplyBala);

                result.ReturnValue = task;
                result.ResultStatus = 0;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
