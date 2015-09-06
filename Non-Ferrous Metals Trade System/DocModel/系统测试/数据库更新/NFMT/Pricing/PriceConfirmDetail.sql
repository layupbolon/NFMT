alter table Pri_PriceConfirmDetail
   drop constraint PK_PRI_PRICECONFIRMDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_PriceConfirmDetail')
            and   type = 'U')
   drop table Pri_PriceConfirmDetail
go

/*==============================================================*/
/* Table: Pri_PriceConfirmDetail                                */
/*==============================================================*/
create table Pri_PriceConfirmDetail (
   DetailId             int                  identity,
   PriceConfirmId       int                  null,
   InterestDetailId     int                  null,
   InterestId           int                  null,
   StockLogId           int                  null,
   StockId              int                  null,
   ConfirmAmount        decimal(18,4)        null,
   SettlePrice          decimal(18,4)        null,
   SettleBala           decimal(18,4)        null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '价格确认明细',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格确认单序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'PriceConfirmId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息明细序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'InterestDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'InterestId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结算重量',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'ConfirmAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结算单价',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'SettlePrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结算金额',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'SettleBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirmDetail', 'column', 'LastModifyTime'
go

alter table Pri_PriceConfirmDetail
   add constraint PK_PRI_PRICECONFIRMDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmDetailGet    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmDetailLoad    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmDetailInsert    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmDetailUpdate    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmDetailUpdateStatus    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmDetailUpdateStatus    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PriceConfirmDetailUpdateStatus
// 存储过程功能描述：更新Pri_PriceConfirmDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PriceConfirmDetail'

set @str = 'update [dbo].[Pri_PriceConfirmDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PriceConfirmDetailGoBack
// 存储过程功能描述：撤返Pri_PriceConfirmDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PriceConfirmDetail'

set @str = 'update [dbo].[Pri_PriceConfirmDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PriceConfirmDetailGet
// 存储过程功能描述：查询指定Pri_PriceConfirmDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[PriceConfirmId],
	[InterestDetailId],
	[InterestId],
	[StockLogId],
	[StockId],
	[ConfirmAmount],
	[SettlePrice],
	[SettleBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PriceConfirmDetail]
WHERE
	[DetailId] = @id

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
// 存储过程名：[dbo].Pri_PriceConfirmDetailLoad
// 存储过程功能描述：查询所有Pri_PriceConfirmDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmDetailLoad
AS

SELECT
	[DetailId],
	[PriceConfirmId],
	[InterestDetailId],
	[InterestId],
	[StockLogId],
	[StockId],
	[ConfirmAmount],
	[SettlePrice],
	[SettleBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PriceConfirmDetail]

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
// 存储过程名：[dbo].Pri_PriceConfirmDetailInsert
// 存储过程功能描述：新增一条Pri_PriceConfirmDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmDetailInsert
	@PriceConfirmId int =NULL ,
	@InterestDetailId int =NULL ,
	@InterestId int =NULL ,
	@StockLogId int =NULL ,
	@StockId int =NULL ,
	@ConfirmAmount decimal(18, 4) =NULL ,
	@SettlePrice decimal(18, 4) =NULL ,
	@SettleBala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Pri_PriceConfirmDetail] (
	[PriceConfirmId],
	[InterestDetailId],
	[InterestId],
	[StockLogId],
	[StockId],
	[ConfirmAmount],
	[SettlePrice],
	[SettleBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PriceConfirmId,
	@InterestDetailId,
	@InterestId,
	@StockLogId,
	@StockId,
	@ConfirmAmount,
	@SettlePrice,
	@SettleBala,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_PriceConfirmDetailUpdate
// 存储过程功能描述：更新Pri_PriceConfirmDetail
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmDetailUpdate
    @DetailId int,
@PriceConfirmId int = NULL,
@InterestDetailId int = NULL,
@InterestId int = NULL,
@StockLogId int = NULL,
@StockId int = NULL,
@ConfirmAmount decimal(18, 4) = NULL,
@SettlePrice decimal(18, 4) = NULL,
@SettleBala decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_PriceConfirmDetail] SET
	[PriceConfirmId] = @PriceConfirmId,
	[InterestDetailId] = @InterestDetailId,
	[InterestId] = @InterestId,
	[StockLogId] = @StockLogId,
	[StockId] = @StockId,
	[ConfirmAmount] = @ConfirmAmount,
	[SettlePrice] = @SettlePrice,
	[SettleBala] = @SettleBala,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



