using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NFMT.Common;
using NFMT.WareHouse.DAL;
using NFMT.WareHouse;

namespace NFMTService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ContractInStocksService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ContractInStocksService.svc 或 ContractInStocksService.svc.cs，然后开始调试。
    public class ContractInStocksService : IContractInStocksService
    {
        public ResultModel InsertStockInContract(UserModel user,int contractId,int subId,List<int> stockLogIds)
        {
            ResultModel result = new ResultModel();

            int assetId = 0;
            int logDirection = 0;
            int customsType = 0;

            StockLogDAL stockLogDAL = new StockLogDAL();
            StockInDAL stockInDAL = new StockInDAL();
            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            StockInStockDAL stockInStockDAL = new StockInStockDAL();

            foreach (int stockLogId in stockLogIds)
            {
                result = stockLogDAL.Get(user, stockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水不存在";
                    return result;
                }

                if(stockLog.LogStatus != StatusEnum.已生效)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水状态不正确";
                    return result;
                }

                if (assetId == 0)
                    assetId = stockLog.AssetId;
                if (logDirection == 0)
                    logDirection = stockLog.LogDirection;
                if (customsType == 0)
                    customsType = stockLog.CustomsType;

                if (assetId != stockLog.AssetId)
                {
                    result.ResultStatus = -1;
                    result.Message = "选中库存存在品种不一致";
                    return result;
                }

                if (logDirection != stockLog.LogDirection)
                {
                    result.ResultStatus = -1;
                    result.Message = "选中库存存在流水方向不一致";
                    return result;
                }

                if (customsType != stockLog.CustomsType)
                {
                    result.ResultStatus = -1;
                    result.Message = "选中库存存在关境不一致";
                    return result;
                }

                result = stockInStockDAL.GetByStockLogId(user, stockLog.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockInStock stockInStock = result.ReturnValue as NFMT.WareHouse.Model.StockInStock;
                if (stockInStock == null || stockInStock.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水与入库登记关系获取失败";
                    return result;
                }

                NFMT.WareHouse.Model.ContractStockIn contractStockIn = new NFMT.WareHouse.Model.ContractStockIn();
                contractStockIn.ContractId = contractId;
                contractStockIn.ContractSubId = subId;
                contractStockIn.RefStatus = StatusEnum.已生效;
                contractStockIn.StockInId = stockInStock.StockInId;

                result = contractStockInDAL.Insert(user, contractStockIn);
                if (result.ResultStatus != 0)
                    return result;
            }

            return result;
        }

        public ResultModel InvalidStockContract(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();

            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            result = contractStockInDAL.Load(user, contractId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.WareHouse.Model.ContractStockIn> contractStockIns = result.ReturnValue as List<NFMT.WareHouse.Model.ContractStockIn>;
            if (contractStockIns == null)
            {
                result.Message = "合约入库分配获取失败";
                result.ResultStatus = -1;
                return result;
            }

            foreach (NFMT.WareHouse.Model.ContractStockIn contractStcokIn in contractStockIns)
            {
                result = contractStockInDAL.Invalid(user, contractStcokIn);
                if (result.ResultStatus != 0)
                    return result;
            }

            return result;
        }

        public ResultModel ContractStockInComplete(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();
            StockInDAL stockInDAL = new StockInDAL();
            StockLogDAL stockLogDAL = new StockLogDAL();
            StockDAL stockDAL = new StockDAL();
            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            StockInStockDAL stockInStockDAL = new StockInStockDAL();

            //using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            //{
                result = contractStockInDAL.Load(user, contractId);
                if (result.ResultStatus != 0)
                    return result;

                List<NFMT.WareHouse.Model.ContractStockIn> contractStockIns = result.ReturnValue as List<NFMT.WareHouse.Model.ContractStockIn>;
                if (contractStockIns == null)
                {
                    result.Message = "合约入库分配获取失败";
                    result.ResultStatus = -1;
                    return result;
                }

                foreach (NFMT.WareHouse.Model.ContractStockIn contractStcokIn in contractStockIns)
                {
                    //获取入库登记
                    result = stockInDAL.Get(user, contractStcokIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                    if (stockIn == null || stockIn.StockInId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据不存在，无法完成";
                        return result;
                    }

                    //入库登记完成
                    if (stockIn.StockInStatus == StatusEnum.已生效)
                    {
                        result = stockInDAL.Complete(user, stockIn);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取合约关联
                    result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.ContractStockIn contractStockIn = result.ReturnValue as NFMT.WareHouse.Model.ContractStockIn;
                    if (contractStockIn == null || contractStockIn.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "入库登记未关联合约，不允许确认完成";
                        return result;
                    }

                    //完成合约关联
                    if (contractStockIn.RefStatus == StatusEnum.已生效)
                    {
                        result = contractStockInDAL.Complete(user, contractStockIn);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取入库登记库存关联
                    result = stockInStockDAL.GetByStockIn(NFMT.Common.DefaultValue.SysUser, stockIn.StockInId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.StockInStock stockInStock = result.ReturnValue as NFMT.WareHouse.Model.StockInStock;
                    if (stockInStock == null || stockInStock.RefId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取入库登记与库存关联失败";
                        return result;
                    }

                    //完成入库登记库存关联
                    if (stockInStock.RefStatus == StatusEnum.已生效)
                    {
                        result = stockInStockDAL.Complete(user, stockInStock);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取库存流水
                    result = stockLogDAL.Get(user, stockInStock.StockLogId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                    if (stockLog == null || stockLog.StockLogId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取入库流水失败";
                        return result;
                    }

                    //判断库存流水是否关联合约
                    if (stockLog.ContractId <= 0)
                    {
                        stockLog.ContractId = contractStockIn.ContractId;
                        stockLog.SubContractId = contractStockIn.ContractSubId;

                        result = stockLogDAL.Update(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    if (stockLog.LogStatus == StatusEnum.已生效)
                    {
                        result = stockLogDAL.Complete(user, stockLog);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    //获取库存
                    result = stockDAL.Get(user, stockLog.StockId);
                    if (result.ResultStatus != 0)
                        return result;

                    NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                    if (stock == null || stock.StockId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "库存获取失败";
                        return result;
                    }

                    //更新库存表
                    if (stock.StockStatus == NFMT.WareHouse.StockStatusEnum.预入库存)
                    {
                        result = stockDAL.UpdateStockStatus(stock, NFMT.WareHouse.StockStatusEnum.在库正常);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }

            //    scope.Complete();
            //}

            return result;
        }

        public ResultModel ContractStockInCompleteCancel(UserModel user, int contractId)
        {
            ResultModel result = new ResultModel();
            StockDAL stockDAL = new StockDAL();
            StockLogDAL stockLogDAL = new StockLogDAL();
            StockInStockDAL stockInStockDAL = new StockInStockDAL();
            ContractStockInDAL contractStockInDAL = new ContractStockInDAL();
            StockInDAL stockInDAL = new StockInDAL();

            result = contractStockInDAL.Load(user, contractId);
            if (result.ResultStatus != 0)
                return result;

            List<NFMT.WareHouse.Model.ContractStockIn> contractStockIns = result.ReturnValue as List<NFMT.WareHouse.Model.ContractStockIn>;
            if (contractStockIns == null)
            {
                result.Message = "合约入库分配获取失败";
                result.ResultStatus = -1;
                return result;
            }

            foreach (NFMT.WareHouse.Model.ContractStockIn contractStcokIn in contractStockIns)
            {
                //获取入库登记
                result = stockInDAL.Get(user, contractStcokIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockIn stockIn = result.ReturnValue as NFMT.WareHouse.Model.StockIn;
                if (stockIn == null || stockIn.StockInId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "入库登记不存在";
                    return result;
                }

                //入库登记撤销完成
                result = stockInDAL.CompleteCancel(user, stockIn);
                if (result.ResultStatus != 0)
                    return result;

                //获取合约关联
                result = contractStockInDAL.GetByStockInId(user, stockIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.ContractStockIn contractStockIn = result.ReturnValue as NFMT.WareHouse.Model.ContractStockIn;
                if (contractStockIn == null || contractStockIn.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "入库登记未关联合约，不允许确认完成";
                    return result;
                }

                //完成撤销合约关联
                result = contractStockInDAL.CompleteCancel(user, contractStockIn);
                if (result.ResultStatus != 0)
                    return result;

                //获取入库登记与库存流水关联
                result = stockInStockDAL.GetByStockIn(user, stockIn.StockInId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockInStock stockInStock = result.ReturnValue as NFMT.WareHouse.Model.StockInStock;
                if (stockInStock == null || stockInStock.RefId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "关联获取失败";
                    return result;
                }

                //撤销关联
                result = stockInStockDAL.CompleteCancel(user, stockInStock);
                if (result.ResultStatus != 0)
                    return result;

                //获取库存流水
                result = stockLogDAL.Get(user, stockInStock.StockLogId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.StockLog stockLog = result.ReturnValue as NFMT.WareHouse.Model.StockLog;
                if (stockLog == null || stockLog.StockLogId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存流水获取失败";
                    return result;
                }

                //撤销库存流水
                result = stockLogDAL.CompleteCancel(user, stockLog);
                if (result.ResultStatus != 0)
                    return result;

                //获取库存
                result = stockDAL.Get(user, stockLog.StockId);
                if (result.ResultStatus != 0)
                    return result;

                NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                if (stock == null || stock.StockId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "库存获取失败";
                    return result;
                }

                //更新库存状态为预入库存
                result = stockDAL.UpdateStockStatus(stock, StockStatusEnum.预入库存);
                if (result.ResultStatus != 0)
                    return result;
            }

            return result;
        }

    }
}
