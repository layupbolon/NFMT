alter table St_SplitDoc
   drop constraint PK_ST_SPLITDOC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_SplitDoc')
            and   type = 'U')
   drop table St_SplitDoc
go

/*==============================================================*/
/* Table: St_SplitDoc                                           */
/*==============================================================*/
create table St_SplitDoc (
   SplitDocId           int                  identity,
   Spliter              int                  null,
   SplitDocTime         datetime             null,
   SplitDocStatus       int                  null,
   OldRefNoId           int                  null,
   OldRefNo             varchar(50)          null,
   OldStockId           int                  null,
   StockLogId           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '拆单',
   'user', @CurrentUser, 'table', 'St_SplitDoc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单序号',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'SplitDocId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单人',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'Spliter'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单时间',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'SplitDocTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单状态',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'SplitDocStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '原业务单号',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'OldRefNoId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '原业务单号',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'OldRefNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '原库存号',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'OldStockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单流水序号',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'St_SplitDoc', 'column', 'LastModifyTime'
go

alter table St_SplitDoc
   add constraint PK_ST_SPLITDOC primary key (SplitDocId)
go

/****** Object:  Stored Procedure [dbo].St_SplitDocGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocGet]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocLoad]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocInsert]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_SplitDocUpdateStatus
// 存储过程功能描述：更新St_SplitDoc中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDoc'

set @str = 'update [dbo].[St_SplitDoc] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SplitDocId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_SplitDocGoBack
// 存储过程功能描述：撤返St_SplitDoc，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDoc'

set @str = 'update [dbo].[St_SplitDoc] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SplitDocId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_SplitDocGet
// 存储过程功能描述：查询指定St_SplitDoc的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocGet
    /*
	@SplitDocId int
    */
    @id int
AS

SELECT
	[SplitDocId],
	[Spliter],
	[SplitDocTime],
	[SplitDocStatus],
	[OldRefNoId],
	[OldRefNo],
	[OldStockId],
	[StockLogId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_SplitDoc]
WHERE
	[SplitDocId] = @id

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
// 存储过程名：[dbo].St_SplitDocLoad
// 存储过程功能描述：查询所有St_SplitDoc记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocLoad
AS

SELECT
	[SplitDocId],
	[Spliter],
	[SplitDocTime],
	[SplitDocStatus],
	[OldRefNoId],
	[OldRefNo],
	[OldStockId],
	[StockLogId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_SplitDoc]

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
// 存储过程名：[dbo].St_SplitDocInsert
// 存储过程功能描述：新增一条St_SplitDoc记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocInsert
	@Spliter int =NULL ,
	@SplitDocTime datetime =NULL ,
	@SplitDocStatus int =NULL ,
	@OldRefNoId int =NULL ,
	@OldRefNo varchar(50) =NULL ,
	@OldStockId int =NULL ,
	@StockLogId int =NULL ,
	@CreatorId int =NULL ,
	@SplitDocId int OUTPUT
AS

INSERT INTO [dbo].[St_SplitDoc] (
	[Spliter],
	[SplitDocTime],
	[SplitDocStatus],
	[OldRefNoId],
	[OldRefNo],
	[OldStockId],
	[StockLogId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@Spliter,
	@SplitDocTime,
	@SplitDocStatus,
	@OldRefNoId,
	@OldRefNo,
	@OldStockId,
	@StockLogId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @SplitDocId = @@IDENTITY

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
// 存储过程名：[dbo].St_SplitDocUpdate
// 存储过程功能描述：更新St_SplitDoc
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocUpdate
    @SplitDocId int,
@Spliter int = NULL,
@SplitDocTime datetime = NULL,
@SplitDocStatus int = NULL,
@OldRefNoId int = NULL,
@OldRefNo varchar(50) = NULL,
@OldStockId int = NULL,
@StockLogId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_SplitDoc] SET
	[Spliter] = @Spliter,
	[SplitDocTime] = @SplitDocTime,
	[SplitDocStatus] = @SplitDocStatus,
	[OldRefNoId] = @OldRefNoId,
	[OldRefNo] = @OldRefNo,
	[OldStockId] = @OldStockId,
	[StockLogId] = @StockLogId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[SplitDocId] = @SplitDocId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



