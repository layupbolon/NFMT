USE [NFMT]
GO
/****** Object:  StoredProcedure [dbo].[CustomBala]    Script Date: 11/27/2014 11:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[CustomBala] 
@CustomCorpId int,
@SubContractId int,
@ApplyId int,
@Amout money output,
@CurrencyName varchar(50) output
as
begin

declare 
@CurrencyId int,--折算币种
@SettleCurrency int,--子合约币种
@Price numeric(18,4),--价格
@DoPriceProfit numeric(18,4),--点价盈亏
@PriceStyle1 int,--作价方式
@CashIn numeric(18,4),--收款
@Payment numeric(18,4),--付款
@NotPayApply numeric(18,4),--为付款申请
@NetGoodValue numeric(18,4),--净收货价值
@GoodOutApplyValue numeric(18,4),--发货申请货值
@FloatRatio numeric(18,4)--合约货值浮动比例

	--统一折算到人民币
	select top 1 @CurrencyId = CurrencyId from NFMT_Basic..Currency where CurrencyName like '%人民币%'
	
	--子合约币种
	select @SettleCurrency = SettleCurrency from NFMT..Con_ContractSub where SubId = @SubContractId
	
	--合约货值浮动比例
	select @FloatRatio = case c.TradeDirection when 134 then 
															case sub.PriceMode when 43 then 1.05 --采购点价放5%
																			   else 1.01 --采购定价1%
																			   end	
											   else
													case sub.PriceMode when 43 then 1.05
																	   else 1.01 
																	   end 
											   end
	from NFMT..Con_ContractSub sub,NFMT..Con_Contract c where sub.ContractId = c.ContractId and sub.SubId = @SubContractId

	--获取定价价格
	select @Price = FixedPrice * NFMT.dbo.ExchangeRare(null,@CurrencyId,b.SettleCurrency) 
	from NFMT..Con_SubPrice a,NFMT..Con_ContractSub b  
	where a.SubId = b.SubId and a.SubId = @SubContractId
	--若合约为点价合约，则取结算价
	--to do list


	--点价盈亏
	set @DoPriceProfit = 0
	select @PriceStyle1 = PriceStyle1 from NFMT..Con_SubPrice where SubId = @SubContractId
	if @PriceStyle1 = 147 --点价
	begin
		select @DoPriceProfit = SUM(p.AvgPrice * p.PricingWeight - p.PricingWeight * @Price) * NFMT.dbo.ExchangeRare(null,@CurrencyId,pa.CurrencyId)
		from NFMT..Pri_PricingApply pa
		left join NFMT..Apply a on pa.ApplyId = a.ApplyId
		left join NFMT..Pri_Pricing p on pa.PricingApplyId = p.PricingApplyId
		where pa.SubContractId = @SubContractId and a.ApplyStatus >= 50 and p.PricingStatus = 80
		group by pa.CurrencyId
	end

	--收款
	select @CashIn = SUM(a.AllotBala) * NFMT.dbo.ExchangeRare(null,@CurrencyId,b.CurrencyId) 
	from NFMT..Fun_CashInCorp_Ref a,Fun_CashInAllot b  
	where a.AllotId = b.AllotId and a.DetailStatus = 50 and a.CorpId = @CustomCorpId
	group by b.CurrencyId
	
	--付款
	select @Payment = SUM(ApplyBala) * NFMT.dbo.ExchangeRare(null,@CurrencyId,p.CurrencyId) 
	from NFMT..Fun_PayApply p,NFMT..Apply a 
	where a.ApplyId = p.ApplyId and a.ApplyStatus = 80 and p.RecCorpId = @CustomCorpId
	group by p.CurrencyId
	
	--未付款申请
	select @NotPayApply = SUM(ApplyBala) * NFMT.dbo.ExchangeRare(null,@CurrencyId,p.CurrencyId) 
	from NFMT..Fun_PayApply p,NFMT..Apply a 
	where a.ApplyId = p.ApplyId and a.ApplyStatus >= 50 and a.ApplyStatus < 80 and p.RecCorpId = @CustomCorpId
	group by p.CurrencyId
	
	--净收货价值
	select @NetGoodValue = SUM(s.NetAmount) * NFMT.dbo.ExchangeRare(null,@CurrencyId,@SettleCurrency) 
	from NFMT..St_ContractStockIn_Ref csiRef
	left join NFMT..St_StockIn si on csiRef.StockInId = si.StockInId and csiRef.RefStatus = 50 and si.StockInStatus >= 50
	left join NFMT..St_StockInStock_Ref sisRef on si.StockInId = sisRef.StockInId and sisRef.RefStatus = 50
	left join NFMT..St_Stock s on sisRef.StockId = s.StockId 
	where csiRef.ContractSubId = @SubContractId
	
	--发货申请货值
	select @GoodOutApplyValue = SUM(ApplyWeight) * NFMT.dbo.ExchangeRare(null,@CurrencyId,@SettleCurrency) 
	from NFMT..St_StockOutApply soa,NFMT..Apply a
	where soa.ApplyId = a.ApplyId and a.ApplyStatus = 80 and soa.SubContractId = @SubContractId
	
	
	set @Amout = 
			+ ISNULL(@DoPriceProfit,0)
			- ISNULL(@CashIn,0)
			+ ISNULL(@Payment,0)
			+ ISNULL(@NotPayApply,0)
			- ISNULL(@NetGoodValue,0)
			+ ISNULL(@GoodOutApplyValue,0)
	set @Amout = @Amout * @FloatRatio
	
	select @CurrencyName = CurrencyName from NFMT_Basic..Currency where CurrencyId = @CurrencyId
	
	select 1
end