using NFMT.Common;
using NFMT.DoPrice.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.DoPrice.DAL
{
    public partial class InterestDetailDAL : ExecOperate, IInterestDetailDAL
    {
        public ResultModel Load(UserModel user, int interestId,StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Pri_InterestDetail where InterestId ={0} and DetailStatus >={1}",interestId,(int)status);
            ResultModel reslut = this.Load<Model.InterestDetail>(user, System.Data.CommandType.Text, cmdText);
            return reslut;
        }
    }
}
