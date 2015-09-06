alter table Pri_Pricing
   drop constraint PK_PRI_PRICING
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_Pricing')
            and   type = 'U')
   drop table Pri_Pricing
go

/*==============================================================*/
/* Table: Pri_Pricing                                           */
/*==============================================================*/
create table Pri_Pricing (
   PricingId            int                  identity,
   PricingApplyId       int                  null,
   PricingWeight        decimal(18,4)        null,
   MUId                 int                  null,
   ExchangeId           int                  null,
   FuturesCodeId        int                  null,
   FuturesCodeEndDate   datetime             null,
   SpotQP               datetime             null,
   DelayFee             decimal(18,4)        null default 0,
   Spread               decimal(18,4)        null,
   OtherFee             decimal(18,4)        null,
   AvgPrice             decimal(18,4)        null,
   PricingTime          datetime             null,
   CurrencyId           int                  null,
   Pricinger            int                  null,
   AssertId             int                  null,
   PricingDirection     int                  null,
   PricingStatus        int                  null,
   FinalPrice           decimal(18,4)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '点价表',
   'user', @CurrentUser, 'table', 'Pri_Pricing'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价序号',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'PricingId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'PricingApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价重量',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'PricingWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价重量单位',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'MUId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价期货市场',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'ExchangeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价期货合约',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'FuturesCodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '期货合约到期日',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'FuturesCodeEndDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '延期费',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'DelayFee'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '调期费',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'Spread'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他费',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'OtherFee'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他费描述',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'AvgPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价时间',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'PricingTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价人',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'Pricinger'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价品种',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'AssertId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价方向',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'PricingDirection'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价状态',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'PricingStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最终价格',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'FinalPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_Pricing', 'column', 'LastModifyTime'
go

alter table Pri_Pricing
   add constraint PK_PRI_PRICING primary key (PricingId)
go

/****** Object:  Stored Procedure [dbo].Pri_PricingGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingUpdateStatus
// 存储过程功能描述：更新Pri_Pricing中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_Pricing'

set @str = 'update [dbo].[Pri_Pricing] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PricingId = '+ Convert(varchar,@id) 
exec(@str)

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingGoBack
// 存储过程功能描述：撤返Pri_Pricing，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_Pricing'

set @str = 'update [dbo].[Pri_Pricing] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PricingId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO









SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingGet
// 存储过程功能描述：查询指定Pri_Pricing的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingGet
    /*
	@PricingId int
    */
    @id int
AS

SELECT
	[PricingId],
	[PricingApplyId],
	[PricingWeight],
	[MUId],
	[ExchangeId],
	[FuturesCodeId],
	[FuturesCodeEndDate],
	[SpotQP],
	[DelayFee],
	[Spread],
	[OtherFee],
	[AvgPrice],
	[PricingTime],
	[CurrencyId],
	[Pricinger],
	[AssertId],
	[PricingDirection],
	[PricingStatus],
	[FinalPrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_Pricing]
WHERE
	[PricingId] = @id

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingLoad
// 存储过程功能描述：查询所有Pri_Pricing记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingLoad
AS

SELECT
	[PricingId],
	[PricingApplyId],
	[PricingWeight],
	[MUId],
	[ExchangeId],
	[FuturesCodeId],
	[FuturesCodeEndDate],
	[SpotQP],
	[DelayFee],
	[Spread],
	[OtherFee],
	[AvgPrice],
	[PricingTime],
	[CurrencyId],
	[Pricinger],
	[AssertId],
	[PricingDirection],
	[PricingStatus],
	[FinalPrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_Pricing]

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO







SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingInsert
// 存储过程功能描述：新增一条Pri_Pricing记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingInsert
	@PricingApplyId int =NULL ,
	@PricingWeight decimal(18, 4) =NULL ,
	@MUId int =NULL ,
	@ExchangeId int =NULL ,
	@FuturesCodeId int =NULL ,
	@FuturesCodeEndDate datetime =NULL ,
	@SpotQP datetime =NULL ,
	@DelayFee decimal(18, 4) =NULL ,
	@Spread decimal(18, 4) =NULL ,
	@OtherFee decimal(18, 4) =NULL ,
	@AvgPrice decimal(18, 4) =NULL ,
	@PricingTime datetime =NULL ,
	@CurrencyId int =NULL ,
	@Pricinger int =NULL ,
	@AssertId int =NULL ,
	@PricingDirection int =NULL ,
	@PricingStatus int =NULL ,
	@FinalPrice decimal(18, 4) =NULL ,
	@CreatorId int =NULL ,
	@PricingId int OUTPUT
AS

INSERT INTO [dbo].[Pri_Pricing] (
	[PricingApplyId],
	[PricingWeight],
	[MUId],
	[ExchangeId],
	[FuturesCodeId],
	[FuturesCodeEndDate],
	[SpotQP],
	[DelayFee],
	[Spread],
	[OtherFee],
	[AvgPrice],
	[PricingTime],
	[CurrencyId],
	[Pricinger],
	[AssertId],
	[PricingDirection],
	[PricingStatus],
	[FinalPrice],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PricingApplyId,
	@PricingWeight,
	@MUId,
	@ExchangeId,
	@FuturesCodeId,
	@FuturesCodeEndDate,
	@SpotQP,
	@DelayFee,
	@Spread,
	@OtherFee,
	@AvgPrice,
	@PricingTime,
	@CurrencyId,
	@Pricinger,
	@AssertId,
	@PricingDirection,
	@PricingStatus,
	@FinalPrice,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PricingId = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingUpdate
// 存储过程功能描述：更新Pri_Pricing
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingUpdate
    @PricingId int,
@PricingApplyId int = NULL,
@PricingWeight decimal(18, 4) = NULL,
@MUId int = NULL,
@ExchangeId int = NULL,
@FuturesCodeId int = NULL,
@FuturesCodeEndDate datetime = NULL,
@SpotQP datetime = NULL,
@DelayFee decimal(18, 4) = NULL,
@Spread decimal(18, 4) = NULL,
@OtherFee decimal(18, 4) = NULL,
@AvgPrice decimal(18, 4) = NULL,
@PricingTime datetime = NULL,
@CurrencyId int = NULL,
@Pricinger int = NULL,
@AssertId int = NULL,
@PricingDirection int = NULL,
@PricingStatus int = NULL,
@FinalPrice decimal(18, 4) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_Pricing] SET
	[PricingApplyId] = @PricingApplyId,
	[PricingWeight] = @PricingWeight,
	[MUId] = @MUId,
	[ExchangeId] = @ExchangeId,
	[FuturesCodeId] = @FuturesCodeId,
	[FuturesCodeEndDate] = @FuturesCodeEndDate,
	[SpotQP] = @SpotQP,
	[DelayFee] = @DelayFee,
	[Spread] = @Spread,
	[OtherFee] = @OtherFee,
	[AvgPrice] = @AvgPrice,
	[PricingTime] = @PricingTime,
	[CurrencyId] = @CurrencyId,
	[Pricinger] = @Pricinger,
	[AssertId] = @AssertId,
	[PricingDirection] = @PricingDirection,
	[PricingStatus] = @PricingStatus,
	[FinalPrice] = @FinalPrice,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PricingId] = @PricingId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



