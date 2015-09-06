alter table St_StockExclusive
   drop constraint PK_ST_STOCKEXCLUSIVE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_StockExclusive')
            and   type = 'U')
   drop table St_StockExclusive
go

/*==============================================================*/
/* Table: St_StockExclusive                                     */
/*==============================================================*/
create table St_StockExclusive (
   ExclusiveId          int                  identity,
   ApplyId              int                  null,
   StockApplyId         int                  null,
   DetailApplyId        int                  null,
   StockId              int                  null,
   ExclusiveAmount      decimal(18,4)        null,
   ExclusiveStatus      int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '库存申请库存排他表',
   'user', @CurrentUser, 'table', 'St_StockExclusive'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排它序号',
   'user', @CurrentUser, 'table', 'St_StockExclusive', 'column', 'ExclusiveId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请序号',
   'user', @CurrentUser, 'table', 'St_StockExclusive', 'column', 'ApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存申请序号',
   'user', @CurrentUser, 'table', 'St_StockExclusive', 'column', 'StockApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存申请明细序号',
   'user', @CurrentUser, 'table', 'St_StockExclusive', 'column', 'DetailApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存明细',
   'user', @CurrentUser, 'table', 'St_StockExclusive', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排他重量',
   'user', @CurrentUser, 'table', 'St_StockExclusive', 'column', 'ExclusiveAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排他状态',
   'user', @CurrentUser, 'table', 'St_StockExclusive', 'column', 'ExclusiveStatus'
go

alter table St_StockExclusive
   add constraint PK_ST_STOCKEXCLUSIVE primary key (ExclusiveId)
go

/****** Object:  Stored Procedure [dbo].St_StockExclusiveGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockExclusiveGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockExclusiveGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockExclusiveLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockExclusiveLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockExclusiveLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockExclusiveInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockExclusiveInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockExclusiveInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockExclusiveUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockExclusiveUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockExclusiveUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockExclusiveUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockExclusiveUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockExclusiveUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockExclusiveUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockExclusiveGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockExclusiveGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockExclusiveUpdateStatus
// 存储过程功能描述：更新St_StockExclusive中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockExclusiveUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockExclusive'

set @str = 'update [dbo].[St_StockExclusive] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ExclusiveId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockExclusiveGoBack
// 存储过程功能描述：撤返St_StockExclusive，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockExclusiveGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockExclusive'

set @str = 'update [dbo].[St_StockExclusive] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ExclusiveId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockExclusiveGet
// 存储过程功能描述：查询指定St_StockExclusive的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockExclusiveGet
    /*
	@ExclusiveId int
    */
    @id int
AS

SELECT
	[ExclusiveId],
	[ApplyId],
	[StockApplyId],
	[DetailApplyId],
	[StockId],
	[ExclusiveAmount],
	[ExclusiveStatus]
FROM
	[dbo].[St_StockExclusive]
WHERE
	[ExclusiveId] = @id

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
// 存储过程名：[dbo].St_StockExclusiveLoad
// 存储过程功能描述：查询所有St_StockExclusive记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockExclusiveLoad
AS

SELECT
	[ExclusiveId],
	[ApplyId],
	[StockApplyId],
	[DetailApplyId],
	[StockId],
	[ExclusiveAmount],
	[ExclusiveStatus]
FROM
	[dbo].[St_StockExclusive]

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
// 存储过程名：[dbo].St_StockExclusiveInsert
// 存储过程功能描述：新增一条St_StockExclusive记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockExclusiveInsert
	@ApplyId int =NULL ,
	@StockApplyId int =NULL ,
	@DetailApplyId int =NULL ,
	@StockId int =NULL ,
	@ExclusiveAmount decimal(18, 4) =NULL ,
	@ExclusiveStatus int =NULL ,
	@ExclusiveId int OUTPUT
AS

INSERT INTO [dbo].[St_StockExclusive] (
	[ApplyId],
	[StockApplyId],
	[DetailApplyId],
	[StockId],
	[ExclusiveAmount],
	[ExclusiveStatus]
) VALUES (
	@ApplyId,
	@StockApplyId,
	@DetailApplyId,
	@StockId,
	@ExclusiveAmount,
	@ExclusiveStatus
)


SET @ExclusiveId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockExclusiveUpdate
// 存储过程功能描述：更新St_StockExclusive
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockExclusiveUpdate
    @ExclusiveId int,
@ApplyId int = NULL,
@StockApplyId int = NULL,
@DetailApplyId int = NULL,
@StockId int = NULL,
@ExclusiveAmount decimal(18, 4) = NULL,
@ExclusiveStatus int = NULL
AS

UPDATE [dbo].[St_StockExclusive] SET
	[ApplyId] = @ApplyId,
	[StockApplyId] = @StockApplyId,
	[DetailApplyId] = @DetailApplyId,
	[StockId] = @StockId,
	[ExclusiveAmount] = @ExclusiveAmount,
	[ExclusiveStatus] = @ExclusiveStatus
WHERE
	[ExclusiveId] = @ExclusiveId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



