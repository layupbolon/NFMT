using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Invoice.TaskProvider
{
    public class InvoiceApplyTaskProvider : WorkFlow.ITaskProvider
    {
        public Common.ResultModel Create(Common.UserModel user, Common.IModel model)
        {
            NFMT.Common.ResultModel result = new Common.ResultModel();

            try
            {
                WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                NFMT.Operate.Model.Apply apply = model as NFMT.Operate.Model.Apply;

                task.TaskName = "发票申请审核";

                NFMT.Invoice.BLL.InvoiceApplyBLL invoiceApplyBLL = new BLL.InvoiceApplyBLL();
                result = invoiceApplyBLL.GetByApplyId(user, apply.ApplyId);
                if (result.ResultStatus != 0)
                    return result;

                Model.InvoiceApply invoiceApply = result.ReturnValue as Model.InvoiceApply;
                if (invoiceApply == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取发票申请失败";
                    return result;
                }

                task.TaskConnext = string.Format("{0} 于 {1} 提交审核。开票总金额：{2}", user.EmpName, DateTime.Now.ToString(), invoiceApply.TotalBala);

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
