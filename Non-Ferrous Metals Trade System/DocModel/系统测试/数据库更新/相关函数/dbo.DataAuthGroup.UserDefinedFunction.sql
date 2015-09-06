USE [NFMT_User]
GO
/****** Object:  UserDefinedFunction [dbo].[DataAuthGroup]    Script Date: 01/16/2015 11:15:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAuthGroup]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[DataAuthGroup]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAuthGroup]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'

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

' 
END
GO
