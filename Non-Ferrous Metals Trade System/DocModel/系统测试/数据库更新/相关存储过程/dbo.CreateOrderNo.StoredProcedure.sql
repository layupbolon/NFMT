USE [NFMT]
GO
/****** Object:  StoredProcedure [dbo].[CreateOrderNo]    Script Date: 01/16/2015 09:07:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOrderNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOrderNo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOrderNo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE proc [dbo].[CreateOrderNo]
(
@identity int
)
as
begin

declare @orderNo varchar(20)

set @orderNo = ''批次'' + CAST(@identity as varchar)

update NFMT.dbo.Doc_DocumentOrder set OrderNo=@orderNo where OrderId = @identity

end

' 
END
GO
