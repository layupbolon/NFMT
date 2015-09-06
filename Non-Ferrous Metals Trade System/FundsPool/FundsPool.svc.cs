using System.Collections.Generic;

namespace FundsPool
{
    public class FundsPool : IFunds
    {
        private readonly NFMT.Funds.BLL.FundsLogBLL fundsLogBll = new NFMT.Funds.BLL.FundsLogBLL();

        /// <summary>
        /// 资金收入
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="fundsLog">资金流水</param>
        /// <param name="fundsLogAttachs">资金流水附件</param>
        /// <returns></returns>
        public NFMT.Common.ResultModel FundIncome(NFMT.Common.UserModel user, NFMT.Funds.Model.FundsLog fundsLog, List<NFMT.Funds.Model.FundsLogAttach> fundsLogAttachs)
        {
            return fundsLogBll.FundHandle(user, fundsLog, fundsLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.资金流水类型)["收款"]);
        }

        /// <summary>
        /// 资金支出
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="fundsLog">资金流水</param>
        /// <param name="fundsLogAttachs">资金流水附件</param>
        /// <returns></returns>
        public NFMT.Common.ResultModel FundExpend(NFMT.Common.UserModel user, NFMT.Funds.Model.FundsLog fundsLog, List<NFMT.Funds.Model.FundsLogAttach> fundsLogAttachs)
        {
            return fundsLogBll.FundHandle(user, fundsLog, fundsLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.资金流水类型)["付款"]);
        }

        /// <summary>
        /// 资金冲销
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="fundsLog">资金流水</param>
        /// <param name="fundsLogAttachs">资金流水附件</param>
        /// <returns></returns>
        public NFMT.Common.ResultModel FundReverse(NFMT.Common.UserModel user, NFMT.Funds.Model.FundsLog fundsLog, List<NFMT.Funds.Model.FundsLogAttach> fundsLogAttachs)
        {
            return fundsLogBll.FundHandle(user, fundsLog, fundsLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.资金流水类型)["冲销"]);
        }
    }
}
