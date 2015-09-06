USE [NFMT]
GO
/****** Object:  StoredProcedure [dbo].[CreateContractNo]    Script Date: 01/16/2015 09:07:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateContractNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateContractNo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateContractNo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE proc [dbo].[CreateContractNo]
(
@identity int
)
as
begin

declare @typeName varchar(20)
declare @contractNo varchar(20)

set @typeName = ''Contract''
set @contractNo = @typeName + CAST(@identity as varchar)

update dbo.Con_Contract set ContractNo=@contractNo where ContractId = @identity

end

' 
END
GO
