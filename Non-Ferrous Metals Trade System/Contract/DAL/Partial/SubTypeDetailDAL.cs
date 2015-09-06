using NFMT.Common;
using NFMT.Contract.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.DAL
{
    /// <summary>
    /// 子合约类型明细dbo.Con_SubTypeDetail数据交互类。
    /// </summary>
    public partial class SubTypeDetailDAL : DataOperate, ISubTypeDetailDAL
    {
        #region 新增方法

        public ResultModel LoadSubTypesById(UserModel user, int subId)
        {
            string cmdText = string.Format("select * from dbo.Con_SubTypeDetail where SubId={0} and DetailStatus >={1}", subId, (int)NFMT.Common.StatusEnum.已生效);

            return this.Load<Model.SubTypeDetail>(user, System.Data.CommandType.Text, cmdText);
        }

        #endregion
    }
}
