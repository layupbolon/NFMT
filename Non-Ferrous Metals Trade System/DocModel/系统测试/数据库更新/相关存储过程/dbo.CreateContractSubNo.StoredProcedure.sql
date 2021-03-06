USE [NFMT]
GO
/****** Object:  StoredProcedure [dbo].[CreateContractSubNo]    Script Date: 01/16/2015 09:07:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateContractSubNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateContractSubNo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateContractSubNo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE proc [dbo].[CreateContractSubNo]
(
@identity int
)
as
begin

declare @typeName varchar(20)
declare @contractSubNo varchar(20)

set @typeName = ''ContractSub''
set @contractSubNo = @typeName + CAST(@identity as varchar)

update dbo.Con_ContractSub set SubNo=@contractSubNo where SubId = @identity

end


' 
END
GO
