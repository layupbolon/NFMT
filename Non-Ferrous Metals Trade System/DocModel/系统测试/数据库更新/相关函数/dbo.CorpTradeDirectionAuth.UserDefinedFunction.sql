USE [NFMT_User]
GO
/****** Object:  UserDefinedFunction [dbo].[CorpTradeDirectionAuth]    Script Date: 01/16/2015 11:15:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpTradeDirectionAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CorpTradeDirectionAuth]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorpTradeDirectionAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE function [dbo].[CorpTradeDirectionAuth]
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

end' 
END
GO
