using NFMT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NFMTService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ContractOutService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ContractOutService.svc 或 ContractOutService.svc.cs，然后开始调试。
    public class ContractOutService : IContractOutService
    {
        public ResultModel ContractOutCreateStockOperate(UserModel user, int contractId, int subId, int assetId, int unitId, int outCorpId, int tradeBorder, List<NFMT.WareHouse.Model.StockOutApplyDetail> details, bool isSubmitAudit)
        {
            ResultModel result = new ResultModel();

            NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
            NFMT.WareHouse.DAL.StockOutApplyDAL outApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
            NFMT.WareHouse.DAL.StockOutApplyDetailDAL detailDAL = new NFMT.WareHouse.DAL.StockOutApplyDetailDAL();
            NFMT.WareHouse.DAL.StockExclusiveDAL exclusiveDAL = new NFMT.WareHouse.DAL.StockExclusiveDAL();
            NFMT.WareHouse.DAL.StockDAL stockDAL = new NFMT.WareHouse.DAL.StockDAL();   

            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //验证分配库存
                    if (details == null || details.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "未分配任务库存";
                        return result;
                    }

                    //添加主申请表
                    NFMT.Operate.Model.Apply apply = new NFMT.Operate.Model.Apply();
                    apply.ApplyDept = user.DeptId;
                    apply.ApplyCorp = user.CorpId;
                    apply.ApplyTime = DateTime.Now;
                    apply.ApplyDesc = string.Empty;
                    apply.ApplyType = NFMT.Operate.ApplyType.出库申请;
                    apply.EmpId = user.EmpId;
                    apply.ApplyStatus = NFMT.Common.StatusEnum.已录入;
                    if(isSubmitAudit)
                        apply.ApplyStatus = NFMT.Common.StatusEnum.待审核;

                    result = applyDAL.Insert(user, apply);
                    if (result.ResultStatus != 0)
                        return result;

                    int applyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out applyId) || applyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "申请主表添加失败";
                        return result;
                    }

                    decimal sumNetAmount = details.Sum(temp => temp.NetAmount);
                    int sumBundles = details.Sum(temp => temp.Bundles);

                    //添加出库申请主表
                    NFMT.WareHouse.Model.StockOutApply outApply = new NFMT.WareHouse.Model.StockOutApply();
                    outApply.ApplyId = applyId;
                    outApply.ContractId = contractId;
                    outApply.SubContractId = subId;
                    outApply.NetAmount = sumNetAmount;
                    outApply.Bundles = sumBundles;
                    outApply.UnitId = unitId;
                    outApply.BuyCorpId = outCorpId;

                    foreach (NFMT.WareHouse.Model.StockOutApplyDetail applyDetail in details)
                    {
                        //验证库存
                        result = stockDAL.Get(user, applyDetail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不存在";
                            return result;
                        }

                        //判断库存品种是否与合约品种相同
                        if (stock.AssetId != assetId)
                        {
                            result.ResultStatus = -1;
                            result.Message = "分配库存的品种与合约品种不一致";
                            return result;
                        }

                        //验证关境
                        if (tradeBorder == (int)NFMT.Contract.TradeBorderEnum.外贸 && stock.CustomsType != (int)NFMT.WareHouse.CustomTypeEnum.关外)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存关境与合约不一致";
                            return result;
                        }
                        else if (tradeBorder == (int)NFMT.Contract.TradeBorderEnum.内贸 && stock.CustomsType != (int)NFMT.WareHouse.CustomTypeEnum.关内)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存关境与合约不一致";
                            return result;
                        }

                        //不允许配货库存
                        int planStockInStatus = (int)NFMT.WareHouse.StockStatusEnum.预入库存;
                        int planCustomsStatus = (int)NFMT.WareHouse.StockStatusEnum.预报关库存;
                        int stockStatus = (int)stock.StockStatus;
                        if (stockStatus > planCustomsStatus || stockStatus < planStockInStatus)
                        {
                            result.ResultStatus = -1;
                            result.Message = "访笔库存不能进行出库申请分配";
                            return result;
                        }

                        applyDetail.ContractId = contractId;
                        applyDetail.DetailStatus = StatusEnum.已生效;
                        applyDetail.SubContractId = subId;

                        if (tradeBorder == (int)NFMT.Contract.TradeBorderEnum.ForeignTrade)
                            applyDetail.GrossAmount = stock.GrossAmount;
                        else
                            applyDetail.GrossAmount = applyDetail.NetAmount;
                    }

                    decimal sumGrossAmount = details.Sum(temp => temp.GrossAmount);
                    outApply.GrossAmount = sumGrossAmount;
                    outApply.CreateFrom = (int)NFMT.Common.CreateFromEnum.销售合约库存创建;
                    result = outApplyDAL.Insert(user, outApply);

                    if (result.ResultStatus != 0)
                        return result;

                    int outApplyId = 0;
                    if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out outApplyId) || outApplyId <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "出库申请添加失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApplyDetail applyDetail in details)
                    {
                        NFMT.WareHouse.Model.StockOutApplyDetail appDetail = new NFMT.WareHouse.Model.StockOutApplyDetail();
                        appDetail.StockOutApplyId = outApplyId;
                        appDetail.Bundles = applyDetail.Bundles;
                        appDetail.ContractId = applyDetail.ContractId;
                        appDetail.DetailStatus = StatusEnum.已生效;
                        appDetail.GrossAmount = applyDetail.GrossAmount;
                        appDetail.NetAmount = applyDetail.NetAmount;
                        appDetail.StockId = applyDetail.StockId;
                        appDetail.SubContractId = applyDetail.SubContractId;

                        result = detailDAL.Insert(user, appDetail);
                        if (result.ResultStatus != 0)
                            return result;

                        int detailApplyId = 0;
                        if (result.ReturnValue == null || !int.TryParse(result.ReturnValue.ToString(), out detailApplyId) || detailApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存明细添加失败";
                            return result;
                        }

                        //验证库存
                        result = stockDAL.Get(user, applyDetail.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.WareHouse.Model.Stock stock = result.ReturnValue as NFMT.WareHouse.Model.Stock;
                        if (stock == null || stock.StockId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "库存不存在";
                            return result;
                        }

                        //排他表校验
                        result = exclusiveDAL.LoadByStockId(user, stock.StockId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<NFMT.WareHouse.Model.StockExclusive> excs = result.ReturnValue as List<NFMT.WareHouse.Model.StockExclusive>;
                        if (excs == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取排他库存信息失败";
                            return result;
                        }

                        decimal excAmount = excs.Sum(temp => temp.ExclusiveAmount);
                        if (excAmount + applyDetail.NetAmount > stock.CurNetAmount)
                        {
                            result.ResultStatus = -1;
                            result.Message = "该笔库存剩余净重不足，配货失败";
                            return result;
                        }

                        //排他表新增                        
                        NFMT.WareHouse.Model.StockExclusive exculsive = new NFMT.WareHouse.Model.StockExclusive();
                        exculsive.ApplyId = applyId;
                        exculsive.DetailApplyId = detailApplyId;
                        exculsive.ExclusiveStatus = StatusEnum.已生效;
                        exculsive.StockApplyId = outApplyId;
                        exculsive.StockId = stock.StockId;
                        exculsive.ExclusiveAmount = applyDetail.NetAmount;

                        result = exclusiveDAL.Insert(user, exculsive);
                        if (result.ResultStatus != 0)
                            return result;
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutGoBackStockOperate(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //获取子合约出库申请
                    result = stockOutApplyDAL.LoadBySubId(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                    if (outApplies == null || outApplies.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                    {
                        result = applyDAL.Get(user, outApply.ApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                        if (apply == null || apply.ApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取主申请失败";
                            return result;
                        }

                        //申请撤返
                        result = applyDAL.Goback(user, apply);
                        if (result.ResultStatus != 0)
                            return result;
                    }                    

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutInvalidStockOperate(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.WareHouse.DAL.StockOutApplyDetailDAL stockOutApplyDetailDAL = new NFMT.WareHouse.DAL.StockOutApplyDetailDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                NFMT.WareHouse.DAL.StockExclusiveDAL stockExclusiveDAL = new NFMT.WareHouse.DAL.StockExclusiveDAL();

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //获取子合约出库申请
                    result = stockOutApplyDAL.LoadBySubId(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                    if (outApplies == null || outApplies.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                    {
                        result = applyDAL.Get(user, outApply.ApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                        if (apply == null || apply.ApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取主申请失败";
                            return result;
                        }

                        //申请作废
                        result = applyDAL.Invalid(user, apply);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取出库申请明细
                        result = stockOutApplyDetailDAL.Load(user, outApply.StockOutApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<NFMT.WareHouse.Model.StockOutApplyDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApplyDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取出库申请明细失败";
                            return result;
                        }

                        foreach (NFMT.WareHouse.Model.StockOutApplyDetail detail in details)
                        {
                            detail.DetailStatus = StatusEnum.已录入;
                            result = stockOutApplyDetailDAL.Invalid(user, detail);
                            if (result.ResultStatus != 0)
                                return result;

                            //获取排他明细
                            result = stockExclusiveDAL.Get(user, apply.ApplyId, outApply.StockOutApplyId, detail.DetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            NFMT.WareHouse.Model.StockExclusive stockExclusive = result.ReturnValue as NFMT.WareHouse.Model.StockExclusive;
                            if (stockExclusive == null || stockExclusive.ExclusiveId<=0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取排他明细失败";
                                return result;
                            }

                            //作废排他明细
                            stockExclusive.ExclusiveStatus = StatusEnum.已录入;
                            result = stockExclusiveDAL.Invalid(user, stockExclusive);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutSubmitStockOperate(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //获取子合约出库申请
                    result = stockOutApplyDAL.LoadBySubId(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                    if (outApplies == null || outApplies.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                    {
                        result = applyDAL.Get(user, outApply.ApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                        if (apply == null || apply.ApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取主申请失败";
                            return result;
                        }

                        //申请提交
                        result = applyDAL.Submit(user, apply);
                        if (result.ResultStatus != 0)
                            return result;                        
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutCompleteStockOperate(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.WareHouse.DAL.StockOutApplyDetailDAL stockOutApplyDetailDAL = new NFMT.WareHouse.DAL.StockOutApplyDetailDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();
                NFMT.WareHouse.DAL.StockExclusiveDAL stockExclusiveDAL = new NFMT.WareHouse.DAL.StockExclusiveDAL();

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //获取子合约出库申请
                    result = stockOutApplyDAL.LoadBySubId(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                    if (outApplies == null || outApplies.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                    {
                        //验证是否执行完成
                        result = stockOutApplyDAL.CheckStockOutCanConfirm(user, outApply.StockOutApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        result = applyDAL.Get(user, outApply.ApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                        if (apply == null || apply.ApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取主申请失败";
                            return result;
                        }

                        //主申请完成
                        result = applyDAL.Confirm(user, apply);
                        if (result.ResultStatus != 0)
                            return result;

                        //获取出库申请明细
                        result = stockOutApplyDetailDAL.Load(user, outApply.StockOutApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        List<NFMT.WareHouse.Model.StockOutApplyDetail> details = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApplyDetail>;
                        if (details == null)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取出库申请明细失败";
                            return result;
                        }

                        foreach (NFMT.WareHouse.Model.StockOutApplyDetail detail in details)
                        {
                            //出库申请明细完成
                            result = stockOutApplyDetailDAL.Confirm(user, detail);
                            if (result.ResultStatus != 0)
                                return result;

                            //获取排他明细
                            result = stockExclusiveDAL.Get(user, apply.ApplyId, outApply.StockOutApplyId, detail.DetailId);
                            if (result.ResultStatus != 0)
                                return result;

                            NFMT.WareHouse.Model.StockExclusive stockExclusive = result.ReturnValue as NFMT.WareHouse.Model.StockExclusive;
                            if (stockExclusive == null || stockExclusive.ExclusiveId <= 0)
                            {
                                result.ResultStatus = -1;
                                result.Message = "获取排他明细失败";
                                return result;
                            }

                            //排他明细完成
                            result = stockExclusiveDAL.Confirm(user, stockExclusive);
                            if (result.ResultStatus != 0)
                                return result;
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel ContractOutAuditStockOperate(UserModel user, int subId,bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                NFMT.WareHouse.DAL.StockOutApplyDAL stockOutApplyDAL = new NFMT.WareHouse.DAL.StockOutApplyDAL();
                NFMT.Operate.DAL.ApplyDAL applyDAL = new NFMT.Operate.DAL.ApplyDAL();

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //获取子合约出库申请
                    result = stockOutApplyDAL.LoadBySubId(user, subId);
                    if (result.ResultStatus != 0)
                        return result;

                    List<NFMT.WareHouse.Model.StockOutApply> outApplies = result.ReturnValue as List<NFMT.WareHouse.Model.StockOutApply>;
                    if (outApplies == null || outApplies.Count == 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "获取出库申请失败";
                        return result;
                    }

                    foreach (NFMT.WareHouse.Model.StockOutApply outApply in outApplies)
                    {
                        result = applyDAL.Get(user, outApply.ApplyId);
                        if (result.ResultStatus != 0)
                            return result;

                        NFMT.Operate.Model.Apply apply = result.ReturnValue as NFMT.Operate.Model.Apply;
                        if (apply == null || apply.ApplyId <= 0)
                        {
                            result.ResultStatus = -1;
                            result.Message = "获取主申请失败";
                            return result;
                        }

                        //审核出库申请
                        result = applyDAL.Audit(user, apply,isPass);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
