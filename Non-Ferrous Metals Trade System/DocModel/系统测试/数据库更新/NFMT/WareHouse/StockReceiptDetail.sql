alter table St_StockReceiptDetail
   drop constraint PK_ST_STOCKRECEIPTDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_StockReceiptDetail')
            and   type = 'U')
   drop table St_StockReceiptDetail
go

/*==============================================================*/
/* Table: St_StockReceiptDetail                                 */
/*==============================================================*/
create table St_StockReceiptDetail (
   DetailId             int                  identity,
   ReceiptId            int                  not null,
   ContractId           int                  null,
   ContractSubId        int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   PreNetAmount         numeric(18,4)        null,
   ReceiptAmount        numeric(18,4)        null,
   QtyMiss              numeric(18,4)        null,
   QtyRate              numeric(18,8)        null,
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
   '回执明细',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执序号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'ReceiptId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'ContractSubId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执前净重',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'PreNetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回执库存净重',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'ReceiptAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '磅差重量',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'QtyMiss'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '磅差比例',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'QtyRate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'St_StockReceiptDetail', 'column', 'LastModifyTime'
go

alter table St_StockReceiptDetail
   add constraint PK_ST_STOCKRECEIPTDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].St_StockReceiptDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockReceiptDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockReceiptDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockReceiptDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockReceiptDetailUpdateStatus
// 存储过程功能描述：更新St_StockReceiptDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockReceiptDetail'

set @str = 'update [dbo].[St_StockReceiptDetail] '+
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
// 存储过程名：[dbo].St_StockReceiptDetailGoBack
// 存储过程功能描述：撤返St_StockReceiptDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockReceiptDetail'

set @str = 'update [dbo].[St_StockReceiptDetail] '+
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
// 存储过程名：[dbo].St_StockReceiptDetailGet
// 存储过程功能描述：查询指定St_StockReceiptDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ReceiptId],
	[ContractId],
	[ContractSubId],
	[StockId],
	[StockLogId],
	[PreNetAmount],
	[ReceiptAmount],
	[QtyMiss],
	[QtyRate],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockReceiptDetail]
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
// 存储过程名：[dbo].St_StockReceiptDetailLoad
// 存储过程功能描述：查询所有St_StockReceiptDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptDetailLoad
AS

SELECT
	[DetailId],
	[ReceiptId],
	[ContractId],
	[ContractSubId],
	[StockId],
	[StockLogId],
	[PreNetAmount],
	[ReceiptAmount],
	[QtyMiss],
	[QtyRate],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockReceiptDetail]

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
// 存储过程名：[dbo].St_StockReceiptDetailInsert
// 存储过程功能描述：新增一条St_StockReceiptDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptDetailInsert
	@ReceiptId int ,
	@ContractId int =NULL ,
	@ContractSubId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@PreNetAmount numeric(18, 4) =NULL ,
	@ReceiptAmount numeric(18, 4) =NULL ,
	@QtyMiss numeric(18, 4) =NULL ,
	@QtyRate numeric(18, 8) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_StockReceiptDetail] (
	[ReceiptId],
	[ContractId],
	[ContractSubId],
	[StockId],
	[StockLogId],
	[PreNetAmount],
	[ReceiptAmount],
	[QtyMiss],
	[QtyRate],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ReceiptId,
	@ContractId,
	@ContractSubId,
	@StockId,
	@StockLogId,
	@PreNetAmount,
	@ReceiptAmount,
	@QtyMiss,
	@QtyRate,
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
// 存储过程名：[dbo].St_StockReceiptDetailUpdate
// 存储过程功能描述：更新St_StockReceiptDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockReceiptDetailUpdate
    @DetailId int,
@ReceiptId int,
@ContractId int = NULL,
@ContractSubId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@PreNetAmount numeric(18, 4) = NULL,
@ReceiptAmount numeric(18, 4) = NULL,
@QtyMiss numeric(18, 4) = NULL,
@QtyRate numeric(18, 8) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_StockReceiptDetail] SET
	[ReceiptId] = @ReceiptId,
	[ContractId] = @ContractId,
	[ContractSubId] = @ContractSubId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[PreNetAmount] = @PreNetAmount,
	[ReceiptAmount] = @ReceiptAmount,
	[QtyMiss] = @QtyMiss,
	[QtyRate] = @QtyRate,
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



