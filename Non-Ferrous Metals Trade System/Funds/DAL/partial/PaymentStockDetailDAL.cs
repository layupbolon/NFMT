using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFMT.Common;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Funds.Model;
using System.Data;

namespace NFMT.Funds.DAL
{
    public partial class PaymentStockDetailDAL : DetailOperate, IPaymentStockDetailDAL
    {
        #region 新增方法

        public ResultModel LoadByStockPayApplyId(UserModel user, int stockPayApplyId)
        {
            int readyStatus = (int)Common.StatusEnum.已生效;
            string cmdText = string.Format("select * from dbo.Fun_PaymentStockDetail where PayApplyDetailId={0} and DetailStatus>={1}", stockPayApplyId, readyStatus);

            return Load<Model.PaymentStockDetail>(user, CommandType.Text, cmdText);
        }

        public ResultModel LoadByPaymentId(UserModel user, int paymentId, Common.StatusEnum status = StatusEnum.已生效)
        {
            string cmdText = string.Format("select * from dbo.Fun_PaymentStockDetail where PaymentId={0} and DetailStatus ={1}", paymentId, (int)status);
            return Load<Model.PaymentStockDetail>(user, CommandType.Text, cmdText);
        }

        public override ResultModel AllowOperate(UserModel user, IModel obj, OperateEnum operate)
        {
            if (operate == OperateEnum.修改)
            {
                ResultModel result = new ResultModel();
                result.ResultStatus = 0;
                return result;
            }

            return base.AllowOperate(user, obj, operate);
        }

        public ResultModel LoadByContractDetailId(UserModel user, int contractDetailId)
        {
            int readyStatus = (int)Common.StatusEnum.已生效;
            string cmdText = string.Format("select * from dbo.Fun_PaymentStockDetail where ContractDetailId={0} and DetailStatus>={1}", contractDetailId, readyStatus);

            return Load<Model.PaymentStockDetail>(user, CommandType.Text, cmdText);
        }

        public ResultModel LoadByPaymentId(UserModel user, int paymentId)
        {
            int readyStatus = (int)Common.StatusEnum.已生效;
            string cmdText = string.Format("select * from dbo.Fun_PaymentStockDetail where PaymentId={0} and DetailStatus>={1}", paymentId, readyStatus);

            return Load<Model.PaymentStockDetail>(user, CommandType.Text, cmdText);
        }

        #endregion
    }
}
