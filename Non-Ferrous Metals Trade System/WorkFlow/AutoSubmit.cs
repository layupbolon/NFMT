using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFMT.Common;

namespace NFMT.WorkFlow
{
    public class AutoSubmit
    {
        public NFMT.Common.ResultModel Submit(UserModel user, IModel obj, ITaskProvider taskProvider, MasterEnum masterEnum)
        {
            ResultModel result = new ResultModel();

            //提交审核
            NFMT.WorkFlow.BLL.FlowMasterBLL flowMasterBLL = new NFMT.WorkFlow.BLL.FlowMasterBLL();
            result = flowMasterBLL.Get(user, (int)masterEnum);
            if (result.ResultStatus != 0)
                return result;
            NFMT.WorkFlow.Model.FlowMaster flowMaster = result.ReturnValue as NFMT.WorkFlow.Model.FlowMaster;

            NFMT.WorkFlow.Model.DataSource source = new NFMT.WorkFlow.Model.DataSource()
            {
                BaseName = obj.DataBaseName,
                TableCode = obj.TableName,
                DataStatus = NFMT.Common.StatusEnum.待审核,
                RowId = obj.Id,
                ViewUrl = flowMaster.ViewUrl,
                EmpId = user.EmpId,
                ApplyTime = DateTime.Now,
                ApplyTitle = string.Empty,
                ApplyMemo = string.Empty,
                ApplyInfo = string.Empty,
                ConditionUrl = flowMaster.ConditionUrl,
                RefusalUrl = flowMaster.RefusalUrl,
                SuccessUrl = flowMaster.SuccessUrl,
                DalName = obj.DalName,
                AssName = obj.AssName
            };

            result = taskProvider.Create(user,obj);
            if (result.ResultStatus != 0)
                return result;

            NFMT.WorkFlow.Model.Task task = result.ReturnValue as NFMT.WorkFlow.Model.Task;
            if (task == null)
            {
                result.ResultStatus = -1;
                result.Message = "创建任务失败";
                return result;
            }
            task.MasterId = flowMaster.MasterId;

            NFMT.WorkFlow.FlowOperate flowOperate = new NFMT.WorkFlow.FlowOperate();
            result = flowOperate.AuditAndCreateTask(user, obj, flowMaster, source, task);

            return result;
        }
    }
}
