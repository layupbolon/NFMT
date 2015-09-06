using NFMT.Common;
using NFMT.DoPrice.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.DoPrice.DAL
{
    public partial class InterestDAL : ExecOperate, IInterestDAL
    {
        public ResultModel LoadInterestListBySubId(UserModel user, int subId,StatusEnum status = StatusEnum.已生效)
        {
            ResultModel result = new ResultModel();

            string cmdTex = string.Format("select * from dbo.Pri_Interest where SubContractId={0} and InterestStatus >={1}", subId, (int)status);

            result = this.Load<Model.Interest>(user, System.Data.CommandType.Text, cmdTex);

            return result;
        }

        public ResultModel GetLastNetWeightBySubId(UserModel user, int subId)
        {
            //获取库存流水净重
            NFMT.WareHouse.DAL.StockLogDAL stockLogDAL = new WareHouse.DAL.StockLogDAL();
            ResultModel result = stockLogDAL.LoadStockLogBySubId(user, subId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.WareHouse.Model.StockLog> stockLogs = result.ReturnValue as List<NFMT.WareHouse.Model.StockLog>;
            decimal netWeight = stockLogs.Sum(temp => temp.NetAmount + temp.GapAmount);

            //获取当前子合约下利息结算
            result = this.LoadInterestListBySubId(user, subId);
            if (result.ResultStatus != 0)
                return result;

            List<Model.Interest> interests = result.ReturnValue as List<Model.Interest>;
            decimal interestAmount = interests.Sum(temp => temp.InterestAmount);

            result.ResultStatus = 0;
            result.ReturnValue = netWeight - interestAmount;

            return result;
        }

        public ResultModel GetLastCapitalBySubId(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            //获取当前子合约下所有大于已生效的资金流水
            NFMT.Funds.BLL.FundsLogBLL fundsLogBLL = new Funds.BLL.FundsLogBLL();
            result = fundsLogBLL.Load(user, subId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.Funds.Model.FundsLog> fundsLogs = result.ReturnValue as List<NFMT.Funds.Model.FundsLog>;
            if (fundsLogs == null)
            {
                result.ResultStatus = -1;
                result.Message = "获取剩余资金失败";
                return result;
            }

            decimal lastCapital = fundsLogs.Sum(temp => temp.FundsBala);

            //子合约价格确认单
            result = this.LoadInterestListBySubId(user, subId,StatusEnum.已录入);
            if (result.ResultStatus != 0)
                return result;

            List<Model.Interest> interests = result.ReturnValue as List<Model.Interest>;

            decimal curCapital = interests.Sum(temp => temp.CurCapital);

            result.ResultStatus = 0;
            result.ReturnValue = lastCapital - curCapital;

            return result;
        }
    }
}
