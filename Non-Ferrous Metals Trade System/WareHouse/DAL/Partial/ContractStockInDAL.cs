using NFMT.Common;
using NFMT.WareHouse.IDAL;
using NFMT.WareHouse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.DAL
{
    public partial class ContractStockInDAL : ExecOperate, IContractStockInDAL
    {
        #region 新增方法

        public ResultModel GetByStockInId(UserModel user, int stockInId, Common.StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            if (stockInId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@stockInId", SqlDbType.Int, 4);
            para.Value = stockInId;
            paras.Add(para);

            para = new SqlParameter("@status", SqlDbType.Int, 4);
            para.Value = status;
            paras.Add(para);

            string cmdText = "select * from dbo.St_ContractStockIn_Ref where StockInId = @stockInId and RefStatus >=@status";
            result = Get(user, CommandType.Text, cmdText, paras.ToArray());

            return result;
        }

        public override IAuthority Authority
        {
            get
            {
                IAuthority auth = new NFMT.Authority.StockInAuth();
                auth.AuthColumnNames.Add("si.StockInId");
                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 42;
            }
        }

        public ResultModel Load(UserModel user, int contractId, Common.StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.St_ContractStockIn_Ref where ContractId = {0} and RefStatus >= {1}",contractId,(int)status);

            return Load<Model.ContractStockIn>(user, CommandType.Text, cmdText);
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            ResultModel result = new ResultModel();

            if (operate == OperateEnum.作废)
            {
                result.ResultStatus = 0;
                return result;
            }
            return base.AllowOperate(user, obj, operate);
        }


        #endregion
    }
}
