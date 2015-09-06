alter table St_StockReceipt
   drop constraint PK_ST_STOCKRECEIPT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_StockReceipt')
            and   type = 'U')
   drop table St_StockReceipt
go

/*==============================================================*/
/* Table: St_StockReceipt                                       */
/*==============================================================*/
create table St_StockReceipt (
   ReceiptId            int                  identity,
   ContractId           int                  null,
   ContractSubId        int                  null,
   PreNetAmount         numeric(18,4)        null,
   ReceiptAmount        numeric(18,4)        null,
   UnitId               int                  null,
   QtyMiss              numeric(18,4)        null,
   QtyRate              numeric(18,8)        null,
   ReceiptDate          datetime             null,
   Receipter            int                  null,
   Memo                 varchar(4000)        null,
   ReceiptType          int                  null,
   ReceiptStatus        int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '仓库库存净重确认回执，磅差回执',
   'user', @CurrentUser, 'table', 'St_StockReceipt'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执序号',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'ReceiptId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约号',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约号',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'ContractSubId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执前净重',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'PreNetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执库存净重',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'ReceiptAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'UnitId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '磅差重量',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'QtyMiss'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '磅差比例',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'QtyRate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执日期',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'ReceiptDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执人',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'Receipter'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执备注',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执方向，（入库回执，出库回执）',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'ReceiptType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执状态',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'ReceiptStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'St_StockReceipt', 'column', 'LastModifyTime'
go

alter table St_StockReceipt
   add constraint PK_ST_STOCKRECEIPT primary key (ReceiptId)
go

/****** Object:  Stored Procedure [dbo].St_StockReceiptGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockReceiptUpdateStatus
// 存储过程功能描述：更新St_StockReceipt中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockReceipt'

set @str = 'update [dbo].[St_StockReceipt] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ReceiptId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockReceiptGoBack
// 存储过程功能描述：撤返St_StockReceipt，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockReceipt'

set @str = 'update [dbo].[St_StockReceipt] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ReceiptId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockReceiptGet
// 存储过程功能描述：查询指定St_StockReceipt的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptGet
    /*
	@ReceiptId int
    */
    @id int
AS

SELECT
	[ReceiptId],
	[ContractId],
	[ContractSubId],
	[PreNetAmount],
	[ReceiptAmount],
	[UnitId],
	[QtyMiss],
	[QtyRate],
	[ReceiptDate],
	[Receipter],
	[Memo],
	[ReceiptType],
	[ReceiptStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockReceipt]
WHERE
	[ReceiptId] = @id

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
// 存储过程名：[dbo].St_StockReceiptLoad
// 存储过程功能描述：查询所有St_StockReceipt记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptLoad
AS

SELECT
	[ReceiptId],
	[ContractId],
	[ContractSubId],
	[PreNetAmount],
	[ReceiptAmount],
	[UnitId],
	[QtyMiss],
	[QtyRate],
	[ReceiptDate],
	[Receipter],
	[Memo],
	[ReceiptType],
	[ReceiptStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockReceipt]

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
// 存储过程名：[dbo].St_StockReceiptInsert
// 存储过程功能描述：新增一条St_StockReceipt记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptInsert
	@ContractId int =NULL ,
	@ContractSubId int =NULL ,
	@PreNetAmount numeric(18, 4) =NULL ,
	@ReceiptAmount numeric(18, 4) =NULL ,
	@UnitId int =NULL ,
	@QtyMiss numeric(18, 4) =NULL ,
	@QtyRate numeric(18, 8) =NULL ,
	@ReceiptDate datetime =NULL ,
	@Receipter int =NULL ,
	@Memo varchar(4000) =NULL ,
	@ReceiptType int =NULL ,
	@ReceiptStatus int =NULL ,
	@CreatorId int =NULL ,
	@ReceiptId int OUTPUT
AS

INSERT INTO [dbo].[St_StockReceipt] (
	[ContractId],
	[ContractSubId],
	[PreNetAmount],
	[ReceiptAmount],
	[UnitId],
	[QtyMiss],
	[QtyRate],
	[ReceiptDate],
	[Receipter],
	[Memo],
	[ReceiptType],
	[ReceiptStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractId,
	@ContractSubId,
	@PreNetAmount,
	@ReceiptAmount,
	@UnitId,
	@QtyMiss,
	@QtyRate,
	@ReceiptDate,
	@Receipter,
	@Memo,
	@ReceiptType,
	@ReceiptStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ReceiptId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockReceiptUpdate
// 存储过程功能描述：更新St_StockReceipt
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptUpdate
    @ReceiptId int,
@ContractId int = NULL,
@ContractSubId int = NULL,
@PreNetAmount numeric(18, 4) = NULL,
@ReceiptAmount numeric(18, 4) = NULL,
@UnitId int = NULL,
@QtyMiss numeric(18, 4) = NULL,
@QtyRate numeric(18, 8) = NULL,
@ReceiptDate datetime = NULL,
@Receipter int = NULL,
@Memo varchar(4000) = NULL,
@ReceiptType int = NULL,
@ReceiptStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_StockReceipt] SET
	[ContractId] = @ContractId,
	[ContractSubId] = @ContractSubId,
	[PreNetAmount] = @PreNetAmount,
	[ReceiptAmount] = @ReceiptAmount,
	[UnitId] = @UnitId,
	[QtyMiss] = @QtyMiss,
	[QtyRate] = @QtyRate,
	[ReceiptDate] = @ReceiptDate,
	[Receipter] = @Receipter,
	[Memo] = @Memo,
	[ReceiptType] = @ReceiptType,
	[ReceiptStatus] = @ReceiptStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ReceiptId] = @ReceiptId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



