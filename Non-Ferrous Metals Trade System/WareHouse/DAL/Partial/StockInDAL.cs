using NFMT.Common;
using NFMT.WareHouse.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.DAL
{
    public partial class StockInDAL : ExecOperate, IStockInDAL
    {
        #region 新增方法

        public override IAuthority Authority
        {
            get
            {
                IAuthority auth = new NFMT.Authority.StockInAuth();
                auth.AuthColumnNames.Add("st.StockInId");
                return auth;
            }
        }

        public ResultModel Load(UserModel user, int subId, StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = string.Format("select si.* from dbo.St_ContractStockIn_Ref csir inner join dbo.St_StockIn si on csir.StockInId = si.StockInId and si.StockInStatus>={0} where csir.RefStatus >= {1} and csir.ContractSubId = {2} ", (int)status, (int)status, subId);


                result = Load<Model.StockIn>(user, CommandType.Text, cmdText);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public override int MenuId
        {
            get
            {
                return 41;
            }
        }

        public ResultModel CheckContractSubStockInConfirm(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            string cmdText = "select COUNT(*) from dbo.St_StockIn si inner join dbo.St_ContractStockIn_Ref csi on csi.StockInId = si.StockInId where csi.ContractSubId=@subId and si.StockInStatus in (@entryStatus,@readyStatus)";

            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@subId", subId);
            paras[1] = new SqlParameter("@entryStatus", (int)NFMT.Common.StatusEnum.已录入);
            paras[1] = new SqlParameter("@readyStatus", (int)NFMT.Common.StatusEnum.已生效);

            object obj = DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras);
            int i = 0;
            if (obj == null || !int.TryParse(obj.ToString(), out i) || i <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "检验入库登记失败";
                return result;
            }
            if (i > 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约中含有未完成的入库登记，不能进行确认完成操作。";
                return result;
            }

            result.ResultStatus = 0;
            result.Message = "入库登记全部完成";

            return result;
        }

        #endregion
    }
}
