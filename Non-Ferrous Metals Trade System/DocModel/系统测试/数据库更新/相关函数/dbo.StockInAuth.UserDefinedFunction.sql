USE [NFMT_User]
GO
/****** Object:  UserDefinedFunction [dbo].[StockInAuth]    Script Date: 01/16/2015 11:15:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StockInAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[StockInAuth]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StockInAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'

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



' 
END
GO
