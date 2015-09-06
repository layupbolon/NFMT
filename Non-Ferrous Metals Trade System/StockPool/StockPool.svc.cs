using System.Collections.Generic;

namespace StockPool
{
    public class StockPool : IStock
    {
        private NFMT.WareHouse.BLL.StockLogBLL logBLL = new NFMT.WareHouse.BLL.StockLogBLL();

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="stockLog">出库流水</param>
        /// <param name="user">当前用户</param>
        /// <param name="stockLogAttachs"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel StockOut(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs)
        {
            NFMT.Common.ResultModel result = logBLL.StockHandle(user, stockLog, stockLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.流水类型)["出库"]);
            return result;
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="stockLog">入库流水</param>
        /// <param name="user">当前用户</param>
        /// <param name="stockLogAttachs"></param>
        /// <returns></returns>
        public bool StockIn(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs)
        {
            NFMT.Common.ResultModel result =
             logBLL.StockHandle(user, stockLog, stockLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.流水类型)["入库"]);

            if (result.ResultStatus == 0)
                return true;
            return false;
        }

        /// <summary>
        /// 冲销
        /// </summary>
        /// <param name="stockLog">冲销流水</param>
        /// <param name="user">当前用户</param>
        /// <param name="stockLogAttachs"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel Reverse(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs)
        {
            return logBLL.StockHandle(user, stockLog, stockLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.流水类型)["冲销"]);
        }

        /// <summary>
        /// 质押
        /// </summary>
        /// <param name="stockLog">质押流水</param>
        /// <param name="user">当前用户</param>
        /// <param name="stockLogAttachs"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel Pledge(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs)
        {
            return logBLL.StockHandle(user, stockLog, stockLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.流水类型)["质押"]);
        }

        /// <summary>
        /// 回购
        /// </summary>
        /// <param name="stockLog">回购流水</param>
        /// <param name="user">当前用户</param>
        /// <param name="stockLogAttachs"></param>
        /// <returns></returns>
        public NFMT.Common.ResultModel Repo(NFMT.Common.UserModel user, NFMT.WareHouse.Model.StockLog stockLog, List<NFMT.WareHouse.Model.StockLogAttach> stockLogAttachs)
        {
            return logBLL.StockHandle(user, stockLog, stockLogAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.流水类型)["回购"]);
        }
    }
}
