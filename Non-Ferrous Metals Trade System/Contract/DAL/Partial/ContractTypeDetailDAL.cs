using NFMT.Common;
using NFMT.Contract.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.DAL
{
    public partial class ContractTypeDetailDAL : DataOperate, IContractTypeDetailDAL
    {
        public ResultModel LoadContractTypesById(UserModel user, int contractId)
        {
            string cmdText = string.Format("select * from dbo.Con_ContractTypeDetail where ContractId={0} and DetailStatus >={1}",contractId,(int)NFMT.Common.StatusEnum.已生效);
            ResultModel result = this.Load<Model.ContractTypeDetail>(user, System.Data.CommandType.Text, cmdText);
            return result;
        }
    }
}
