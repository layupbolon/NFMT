USE [NFMT_User]
GO
/****** Object:  UserDefinedFunction [dbo].[StockLogAuth]    Script Date: 01/16/2015 11:15:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StockLogAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[StockLogAuth]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StockLogAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'Create function [dbo].[StockLogAuth]
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


' 
END
GO
