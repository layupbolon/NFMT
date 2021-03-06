USE [NFMT]
GO
/****** Object:  StoredProcedure [dbo].[CreatePaymentCode]    Script Date: 01/16/2015 09:07:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreatePaymentCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreatePaymentCode]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreatePaymentCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE proc [dbo].[CreatePaymentCode]
(
@identity int
)
as
begin

declare @paymentCode varchar(20)

set @paymentCode = ''Payment'' + CAST(@identity as varchar)

update dbo.Fun_Payment set PaymentCode=@paymentCode where PaymentId = @identity

end

' 
END
GO
