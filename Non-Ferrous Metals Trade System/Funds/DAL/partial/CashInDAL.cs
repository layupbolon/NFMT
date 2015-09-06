using NFMT.Common;
using NFMT.Funds.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Funds.DAL
{
    public partial class CashInDAL : ApplyOperate, ICashInDAL
    {
        #region 新增方法

        public override int MenuId
        {
            get
            {
                return 55;
            }
        }

        public ResultModel CheckContractSubCashInConfirm(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            string cmdText = "select COUNT(*) from NFMT.dbo.Fun_CashInContract_Ref cic inner join NFMT.dbo.Fun_CashIn ci on cic.CashInId = ci.CashInId where cic.SubContractId =@subId and ci.CashInStatus in (@entryStatus,@readyStatus)";

            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@subId", subId);
            paras[1] = new SqlParameter("@entryStatus", (int)NFMT.Common.StatusEnum.已录入);
            paras[1] = new SqlParameter("@readyStatus", (int)NFMT.Common.StatusEnum.已生效);

            object obj = DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras);
            int i = 0;
            if (obj == null || !int.TryParse(obj.ToString(), out i) || i <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "检验收款登记失败";
                return result;
            }
            if (i > 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约中含有未完成的收款登记，不能进行确认完成操作。";
                return result;
            }

            result.ResultStatus = 0;
            result.Message = "收款登记全部完成";

            return result;
        }

        #endregion
    }
}
