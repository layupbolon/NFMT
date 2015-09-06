USE [NFMT]
GO
/****** Object:  StoredProcedure [dbo].[ContractBalancePayment]    Script Date: 03/25/2015 16:37:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractBalancePayment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ContractBalancePayment] 
@SubContractId int,
@PayApplyId int,
@Amout money output,
@CurrencyName varchar(50) output
as
begin

declare 
@CurrencyId int,--子合约币种
@CashInLogAmount numeric(18,4),--收款流水金额
@ApplyBala numeric(18,4),--付款申请金额
@StockInLogAmount numeric(18,4),--入库流水货值
@StockOutApplyAmount numeric(18,4),--出库申请货值
@PriAmount numeric(18,4)--点价申请未对应库存的价值

	--合约尾款付款申请最大金额
	--	= 收款流水金额（状态 = 已完成）
	--	- 付款申请金额（状态 >= 已录入）
	--	- 入库流水货值（状态 = 已完成）
	--	+ 出库申请货值（状态 >= 已录入）
	--	- 点价申请未对应库存的重量 * 点价估价
		

	--定价合约入库流水货值	
	--	= 入库流水净重 * 合约定价
	--点价合约入库流水货值
	--	已完成点价部分 = 入库流水净重 * 对应点价价格
	--	未完成点价部分 = 入库流水净重 * 合约点价估价

	--定价合约出库申请货值
	--	= 出库申请净重 * 合约定价
	--点价合约出库申请货值
	--	已完成点价部分 = 出库申请净重 * 点价价格
	--	未完成点价部分 = 出库申请净重 * 点价估价

	--点价 == 价格确认


	--收款流水金额（状态 = 已完成）
	select @CashInLogAmount = SUM(ISNULL(flog.FundsBala,0))
	from NFMT..Fun_FundsLog flog
	where flog.SubId = @SubContractId and flog.LogStatus =80
	group by flog.SubId

	--付款申请金额（状态 >= 已录入）
	select @ApplyBala = SUM(ISNULL(pa.ApplyBala,0))
	from NFMT..Fun_ContractPayApply_Ref ref
	inner join NFMT..Fun_PayApply pa on ref.PayApplyId = pa.PayApplyId
	left join NFMT..Apply a on pa.ApplyId = a.ApplyId and a.ApplyStatus>=20
	where ref.ContractSubId = @SubContractId

	--入库流水货值（状态 = 已完成）
	select @StockInLogAmount = case sub.PriceMode when 44 then sp.FixedPrice * SUM(ISNULL(slog.NetAmount,0)) when 43 then SUM(ISNULL(ISNULL(pcDetail.SettlePrice,sp.AlmostPrice) * ISNULL(pcDetail.ConfirmAmount,slog.NetAmount),0)) end
	from NFMT..Con_ContractSub sub
	left join NFMT..Con_SubPrice sp on sub.SubId = sp.SubId
	left join NFMT..St_StockLog slog on sub.SubId = slog.SubContractId and slog.LogStatus = 80
	left join NFMT..Pri_PriceConfirmDetail pcDetail on slog.StockLogId = pcDetail.StockLogId and pcDetail.DetailStatus>=50
	where sub.SubId = @SubContractId 
	group by sub.SubId,sub.PriceMode,sp.FixedPrice

	--出库申请货值（状态 >= 已录入）
	select @StockOutApplyAmount = case sub.PriceMode when 44 then sp.FixedPrice * SUM(ISNULL(soad.NetAmount,0)) when 43 then stock.sumStockAmount + (soad.NetAmount - stock.sumAmount)* sp.AlmostPrice end
	from NFMT..Con_ContractSub sub
	left join NFMT..Con_SubPrice sp on sub.SubId = sp.SubId
	left join NFMT..St_StockOutApplyDetail soad on sub.SubId = soad.SubContractId and soad.DetailStatus>=50
	left join ( select pcdetail.StockId,SUM(ISNULL(pcdetail.ConfirmAmount,0)) as sumAmount,SUM(ISNULL(pcdetail.SettlePrice * pcdetail.ConfirmAmount,0)) as sumStockAmount
				from NFMT..Pri_PriceConfirm pc
				inner join NFMT..Pri_PriceConfirmDetail pcdetail on pc.PriceConfirmId = pcdetail.PriceConfirmId  
				where pc.PriceConfirmStatus>=50 and pcdetail.DetailStatus>=50 and pc.SubId = @SubContractId
				group by pcdetail.StockId
			  ) stock on soad.StockId = stock.StockId
	where sub.SubId = @SubContractId
	group by sub.PriceMode,sp.FixedPrice,stock.sumStockAmount,soad.NetAmount,stock.sumAmount,sp.AlmostPrice

	--点价申请未对应库存的重量 * 点价估价
	select @PriAmount = pri.PricingWeight * sp.AlmostPrice
	from NFMT..Con_ContractSub sub
	left join NFMT..Con_SubPrice sp on sub.SubId = sp.SubId
	left join ( select pa.SubContractId,pa.PricingWeight
				from NFMT..Pri_PricingApply pa 
				left join NFMT..Apply a on pa.ApplyId = a.ApplyId and a.ApplyStatus>=50
				left join NFMT..Pri_PricingApplyDetail pad on pa.PricingApplyId = pad.PricingApplyId and DetailStatus >=50
				where ISNULL(pad.DetailId,0)=0
			  ) pri on sub.SubId = pri.SubContractId
	where sub.SubId = @SubContractId
	
	--子合约币种
	select @CurrencyId = SettleCurrency from NFMT..Con_ContractSub where SubId = @SubContractId
	
	print(''收款流水金额'')
	print(@CashInLogAmount)
	
	print(''付款申请金额'')
	print(@ApplyBala)
	
	print(''入库流水货值'')
	print(@StockInLogAmount)
	
	print(''出库申请货值'')
	print(@StockOutApplyAmount)
	
	print(''点价申请未对应库存的价值'')
	print(@PriAmount)
	
	set @Amout = ISNULL(@CashInLogAmount,0)
			   - ISNULL(@ApplyBala,0)
			   - ISNULL(@StockInLogAmount,0)
			   + ISNULL(@StockOutApplyAmount,0)
			   - ISNULL(@PriAmount,0)
	
	select @CurrencyName = CurrencyName from NFMT_Basic..Currency where CurrencyId = @CurrencyId
	
	select 1
end' 
END
GO
