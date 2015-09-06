alter table Pri_StopLoss
   drop constraint PK_PRI_STOPLOSS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_StopLoss')
            and   type = 'U')
   drop table Pri_StopLoss
go

/*==============================================================*/
/* Table: Pri_StopLoss                                          */
/*==============================================================*/
create table Pri_StopLoss (
   StopLossId           int                  identity,
   StopLossApplyId      int                  null,
   ApplyId              int                  null,
   StopLossWeight       decimal(18,4)        null,
   MUId                 int                  null,
   ExchangeId           int                  null,
   FuturesCodeId        int                  null,
   AvgPrice             decimal(18,4)        null,
   PricingTime          datetime             null,
   CurrencyId           int                  null,
   StopLosser           int                  null,
   AssertId             int                  null,
   PricingDirection     int                  null,
   StopLossStatus       int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '止损表',
   'user', @CurrentUser, 'table', 'Pri_StopLoss'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损序号',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'StopLossId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损申请序号',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'StopLossApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请主表序号',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'ApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价重量',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'StopLossWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损重量单位',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'MUId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损期货市场',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'ExchangeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损期货合约',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'FuturesCodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损均价',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'AvgPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损时间',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'PricingTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损人',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'StopLosser'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损品种',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'AssertId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价方向',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'PricingDirection'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损状态',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'StopLossStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_StopLoss', 'column', 'LastModifyTime'
go

alter table Pri_StopLoss
   add constraint PK_PRI_STOPLOSS primary key (StopLossId)
go

/****** Object:  Stored Procedure [dbo].Pri_StopLossGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_StopLossUpdateStatus
// 存储过程功能描述：更新Pri_StopLoss中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_StopLoss'

set @str = 'update [dbo].[Pri_StopLoss] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StopLossId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_StopLossGoBack
// 存储过程功能描述：撤返Pri_StopLoss，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_StopLoss'

set @str = 'update [dbo].[Pri_StopLoss] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StopLossId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_StopLossGet
// 存储过程功能描述：查询指定Pri_StopLoss的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossGet
    /*
	@StopLossId int
    */
    @id int
AS

SELECT
	[StopLossId],
	[StopLossApplyId],
	[ApplyId],
	[StopLossWeight],
	[MUId],
	[ExchangeId],
	[FuturesCodeId],
	[AvgPrice],
	[PricingTime],
	[CurrencyId],
	[StopLosser],
	[AssertId],
	[PricingDirection],
	[StopLossStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_StopLoss]
WHERE
	[StopLossId] = @id

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
// 存储过程名：[dbo].Pri_StopLossLoad
// 存储过程功能描述：查询所有Pri_StopLoss记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossLoad
AS

SELECT
	[StopLossId],
	[StopLossApplyId],
	[ApplyId],
	[StopLossWeight],
	[MUId],
	[ExchangeId],
	[FuturesCodeId],
	[AvgPrice],
	[PricingTime],
	[CurrencyId],
	[StopLosser],
	[AssertId],
	[PricingDirection],
	[StopLossStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_StopLoss]

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
// 存储过程名：[dbo].Pri_StopLossInsert
// 存储过程功能描述：新增一条Pri_StopLoss记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossInsert
	@StopLossApplyId int =NULL ,
	@ApplyId int =NULL ,
	@StopLossWeight decimal(18, 4) =NULL ,
	@MUId int =NULL ,
	@ExchangeId int =NULL ,
	@FuturesCodeId int =NULL ,
	@AvgPrice decimal(18, 4) =NULL ,
	@PricingTime datetime =NULL ,
	@CurrencyId int =NULL ,
	@StopLosser int =NULL ,
	@AssertId int =NULL ,
	@PricingDirection int =NULL ,
	@StopLossStatus int =NULL ,
	@CreatorId int =NULL ,
	@StopLossId int OUTPUT
AS

INSERT INTO [dbo].[Pri_StopLoss] (
	[StopLossApplyId],
	[ApplyId],
	[StopLossWeight],
	[MUId],
	[ExchangeId],
	[FuturesCodeId],
	[AvgPrice],
	[PricingTime],
	[CurrencyId],
	[StopLosser],
	[AssertId],
	[PricingDirection],
	[StopLossStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StopLossApplyId,
	@ApplyId,
	@StopLossWeight,
	@MUId,
	@ExchangeId,
	@FuturesCodeId,
	@AvgPrice,
	@PricingTime,
	@CurrencyId,
	@StopLosser,
	@AssertId,
	@PricingDirection,
	@StopLossStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StopLossId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_StopLossUpdate
// 存储过程功能描述：更新Pri_StopLoss
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossUpdate
    @StopLossId int,
@StopLossApplyId int = NULL,
@ApplyId int = NULL,
@StopLossWeight decimal(18, 4) = NULL,
@MUId int = NULL,
@ExchangeId int = NULL,
@FuturesCodeId int = NULL,
@AvgPrice decimal(18, 4) = NULL,
@PricingTime datetime = NULL,
@CurrencyId int = NULL,
@StopLosser int = NULL,
@AssertId int = NULL,
@PricingDirection int = NULL,
@StopLossStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_StopLoss] SET
	[StopLossApplyId] = @StopLossApplyId,
	[ApplyId] = @ApplyId,
	[StopLossWeight] = @StopLossWeight,
	[MUId] = @MUId,
	[ExchangeId] = @ExchangeId,
	[FuturesCodeId] = @FuturesCodeId,
	[AvgPrice] = @AvgPrice,
	[PricingTime] = @PricingTime,
	[CurrencyId] = @CurrencyId,
	[StopLosser] = @StopLosser,
	[AssertId] = @AssertId,
	[PricingDirection] = @PricingDirection,
	[StopLossStatus] = @StopLossStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StopLossId] = @StopLossId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



