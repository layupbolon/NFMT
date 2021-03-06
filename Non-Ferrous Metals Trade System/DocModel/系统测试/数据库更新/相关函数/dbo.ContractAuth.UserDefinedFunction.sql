USE [NFMT_User]
GO
/****** Object:  UserDefinedFunction [dbo].[ContractAuth]    Script Date: 01/16/2015 11:15:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ContractAuth]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContractAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
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



' 
END
GO
