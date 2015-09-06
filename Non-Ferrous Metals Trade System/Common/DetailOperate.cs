using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Common
{
    public abstract class DetailOperate : ExecOperate
    {
        public override int MenuId
        {
            get
            {
                return 0;
            }
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改 && obj.Status == StatusEnum.已生效)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }
            else if (operate == OperateEnum.作废 && obj.Status == StatusEnum.已生效)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }
    }
}
