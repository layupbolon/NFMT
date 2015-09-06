USE [NFMT]
GO
/****** Object:  StoredProcedure [dbo].[CreateInvoiceNo]    Script Date: 01/16/2015 09:07:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateInvoiceNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateInvoiceNo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateInvoiceNo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE proc [dbo].[CreateInvoiceNo]
(
@identity int
)
as
begin

declare @no varchar(20)

set @no = ''Invoice'' + CAST(@identity as varchar)

update dbo.Invoice set InvoiceNo=@no where InvoiceId = @identity

end


' 
END
GO
