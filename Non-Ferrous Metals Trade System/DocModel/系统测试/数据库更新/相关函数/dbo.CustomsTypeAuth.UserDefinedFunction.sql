USE [NFMT_User]
GO
/****** Object:  UserDefinedFunction [dbo].[CustomsTypeAuth]    Script Date: 01/16/2015 11:15:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomsTypeAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CustomsTypeAuth]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomsTypeAuth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
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


' 
END
GO
