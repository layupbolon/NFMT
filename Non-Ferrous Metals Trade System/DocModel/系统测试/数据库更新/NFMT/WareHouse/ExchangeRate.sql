USE [NFMT]
GO
/****** Object:  UserDefinedFunction [dbo].[ExchangeRare]    Script Date: 12/26/2014 09:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER function [dbo].[ExchangeRare] 
(@TradeDate date,
@Currecny1 int,--目标币种
@Currency2 int--被折算的币种
)
RETURNS numeric(18,4)
begin

declare @RR numeric(18,4)
set @RR = 1
--if @Currecny1 = @Currency2
--begin
--  select @RR = 1
--end
--else begin
--  select top 1 @RR = case when Currency1 = @Currecny1 then Bala1/Bala2 else Bala2/Bala1 end
--  from BusinessNow..TExchangeRate
--  where ((Currency1 = @Currecny1 and Currency2 = @Currency2) or (Currency2 = @Currecny1 and Currency1 = @Currency2))
--  and ExDate <= isnull(@TradeDate,ExDate)
--  order by ExDate desc
--end
return @RR
end