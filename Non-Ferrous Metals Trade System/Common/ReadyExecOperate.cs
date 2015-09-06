using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public abstract class ReadyExecOperate : ExecOperate
    {
        public override NFMT.Common.ResultModel Insert(NFMT.Common.UserModel user, NFMT.Common.IModel obj)
        {
            obj.Status = StatusEnum.已生效;
            return base.Insert(user, obj);
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改 && obj.Status == StatusEnum.已生效)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }
    }
}
