using NFMT.Common;
using NFMT.Contract.IDAL;
using NFMT.Contract.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.DAL
{
    public partial class ContractPriceDAL : DataOperate, IContractPriceDAL
    {
        #region 新增方法

        public ResultModel GetPriceByContractId(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            if (contractId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@contractId", SqlDbType.Int, 4);
            para.Value = contractId;
            paras.Add(para);

            try
            {
                string cmdText = "select * from NFMT.dbo.Con_ContractPrice where ContractId =@contractId";
                result = Get(user, CommandType.Text, cmdText, paras.ToArray());
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion
    }
}
