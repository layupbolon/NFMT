create function [dbo].[TradeDirectionAuth]
(
@empId int,
@tradeDirection int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.TradeDirection = @tradeDirection or authGroup.TradeDirection = 0)
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO

create function [dbo].[TradeBorderAuth]
(
@empId int,
@tradeBorder int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.TradeBorder = @tradeBorder or authGroup.TradeBorder = 0)
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO


Create function [dbo].[StockLogAuth]
(
@empId int,
@stockLogId int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from AuthGroup authGroup,
	AuthGroupDetail authDetail,
	NFMT.dbo.St_StockLog authStockLog,
	NFMT.dbo.St_Stock authStock
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authStockLog.StockId = authStock.StockId
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.AssetId=authStock.AssetId or authGroup.AssetId =0)
	and (authGroup.TradeBorder = case when authStock.CustomsType = 21 then 136 when authStock.CustomsType =20 then 137 end or authGroup.TradeBorder =0)	
	and (authGroup.CorpId = authStock.CorpId or authGroup.CorpId =0)	
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO

CREATE function [dbo].[StockInAuth]
(
@empId int,
@stockInId int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from AuthGroup authGroup,
	AuthGroupDetail authDetail,
	NFMT.dbo.St_StockIn authStockIn
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and authStockIn.StockInId = @stockInId
	and (authGroup.AssetId=authStockIn.AssetId or authGroup.AssetId =0)
	and (authGroup.TradeBorder = case when authStockIn.CustomType = 21 then 136 when authStockIn.CustomType =20 then 137 end or authGroup.TradeBorder =0)	
	and (authGroup.CorpId = authStockIn.CorpId or authGroup.CorpId =0)	
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO

CREATE function [dbo].[StockAuth]
(
@empId int,
@stockId int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from AuthGroup authGroup,
	AuthGroupDetail authDetail,
	NFMT.dbo.St_Stock authStock
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and authStock.StockId = @stockId
	and (authGroup.AssetId=authStock.AssetId or authGroup.AssetId =0)
	and (authGroup.TradeBorder = case when authStock.CustomsType = 21 then 136 when authStock.CustomsType =20 then 137 end or authGroup.TradeBorder =0)	
	and (authGroup.CorpId = authStock.CorpId or authGroup.CorpId =0)	
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end




GO
CREATE function [dbo].[DataAuthGroup]
(
@empId int,
@corpId int,
@assetId int,
@tradeDirect int,
@tradeBorder int,
@contractInOut int,
@contractLimit int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	

	select @count = COUNT(*) from dbo.AuthGroup authGroup 
	inner join dbo.AuthGroupDetail authDetail on authGroup.AuthGroupId = authDetail.AuthGroupId and authGroup.AuthGroupStatus = 50
	where authDetail.EmpId=@empId 
	and (authGroup.CorpId=@corpId or authGroup.CorpId =0) 
	and (authGroup.AssetId=@assetId or authGroup.AssetId =0) 
	and (authGroup.TradeDirection =@tradeDirect or authGroup.TradeDirection=0)
	and (authGroup.TradeBorder = @tradeBorder or authGroup.TradeBorder =0)
	and (authGroup.ContractInOut = @contractInOut or authGroup.ContractInOut =0)
	and (authGroup.ContractLimit = @contractLimit or authGroup.ContractLimit =0)
	and authDetail.DetailStatus = 50
	
	if @count >0 
		begin
			set @retVal =1
		end

return @retVal

end


GO
create function [dbo].[CustomsTypeAuth]
(
@empId int,
@customsType int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.TradeBorder = case when @customsType = 21 then 136 when @customsType =20 then 137 end or authGroup.TradeBorder = 0)
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end



GO

CREATE function [dbo].[CorpTradeDirectionAuth]
(
@empId int,
@corpId int,
@tradeDirection int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	,dbo.Corporation authCorp
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.CorpId =@corpId or @corpId=0 or authGroup.CorpId =0) 
	and (authGroup.TradeDirection = @tradeDirection or authGroup.TradeDirection = 0)
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO
CREATE function [dbo].[CorpAuth]
(
@empId int,
@corpId int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	,dbo.Corporation authCorp 
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.CorpId= @corpId or authGroup.CorpId =0) 
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end





GO
create function [dbo].[ContractLimitAuth]
(
@empId int,
@contractLimit int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.ContractLimit = @contractLimit or authGroup.ContractLimit = 0)
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO
create function [dbo].[ContractInOutAuth]
(
@empId int,
@contractInOut int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.ContractInOut = @contractInOut or authGroup.ContractInOut = 0)
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO
CREATE function [dbo].[ContractAuth]
(
@empId int,
@contractId int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from AuthGroup authGroup,
	AuthGroupDetail authDetail,
	NFMT.dbo.Con_Contract authCon,
	NFMT.dbo.Con_ContractCorporationDetail authCorp 
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authCon.ContractId = @contractId
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.AssetId=authCon.AssetId or authGroup.AssetId =0) 
	and (authGroup.TradeDirection =authCon.TradeDirection or authGroup.TradeDirection=0)
	and (authGroup.TradeBorder = authCon.TradeBorder or authGroup.TradeBorder =0)
	and (authGroup.ContractInOut = authCon.ContractSide or authGroup.ContractInOut =0)
	and (authGroup.ContractLimit = authCon.ContractLimit or authGroup.ContractLimit =0)
	and authCon.ContractId = authCorp.ContractId and authCorp.IsInnerCorp =1 and authCorp.DetailStatus = 50
	and (authGroup.CorpId= authCorp.CorpId or authGroup.CorpId =0) 
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end




GO
create function [dbo].[AssetAuth]
(
@empId int,
@assetId int
)
returns int
as
begin

declare @retVal int
set @retVal =0 
	
declare @count int	
	
	if exists (
	select 1
	from dbo.AuthGroup authGroup
	,dbo.AuthGroupDetail authDetail
	where authGroup.AuthGroupId = authDetail.AuthGroupId 
	and authDetail.DetailStatus = 50
	and authGroup.AuthGroupStatus = 50
	and (authGroup.AssetId = @assetId or authGroup.AssetId = 0)
	and authDetail.EmpId = @empId
	)
		begin
			set @retVal =1
		end

return @retVal

end
GO