alter table dbo.St_RepoDetail
   drop constraint PK_ST_REPODETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_RepoDetail')
            and   type = 'U')
   drop table dbo.St_RepoDetail
go

/*==============================================================*/
/* Table: St_RepoDetail                                         */
/*==============================================================*/
create table dbo.St_RepoDetail (
   DetailId             int                  identity,
   RepoId               int                  null,
   RepoDetailStatus     int                  null,
   StockId              int                  null,
   RepoApplyDetailId    int                  null,
   RepoWeight           numeric(18,4)        null,
   Unit                 int                  null,
   RepoPrice            numeric(18,4)        null,
   Currency             int                  null,
   StockLogId           int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '回购明细',
   'user', 'dbo', 'table', 'St_RepoDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购序号',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'RepoId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购明细状态',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'RepoDetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购申请明细序号',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'RepoApplyDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购重量',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'RepoWeight'
go

execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'Unit'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购价格',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'RepoPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'Currency'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购流水序号',
   'user', 'dbo', 'table', 'St_RepoDetail', 'column', 'StockLogId'
go

alter table dbo.St_RepoDetail
   add constraint PK_ST_REPODETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_RepoDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_RepoDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_RepoDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_RepoDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_RepoDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_RepoDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_RepoDetailUpdateStatus
// 存储过程功能描述：更新St_RepoDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoDetail'

set @str = 'update [dbo].[St_RepoDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
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
// 存储过程名：[dbo].St_RepoDetailGoBack
// 存储过程功能描述：撤返St_RepoDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoDetail'

set @str = 'update [dbo].[St_RepoDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
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
// 存储过程名：[dbo].St_RepoDetailGet
// 存储过程功能描述：查询指定St_RepoDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[RepoId],
	[RepoDetailStatus],
	[StockId],
	[RepoApplyDetailId],
	[RepoWeight],
	[Unit],
	[RepoPrice],
	[Currency],
	[StockLogId]
FROM
	[dbo].[St_RepoDetail]
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
// 存储过程名：[dbo].St_RepoDetailLoad
// 存储过程功能描述：查询所有St_RepoDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoDetailLoad
AS

SELECT
	[DetailId],
	[RepoId],
	[RepoDetailStatus],
	[StockId],
	[RepoApplyDetailId],
	[RepoWeight],
	[Unit],
	[RepoPrice],
	[Currency],
	[StockLogId]
FROM
	[dbo].[St_RepoDetail]

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
// 存储过程名：[dbo].St_RepoDetailInsert
// 存储过程功能描述：新增一条St_RepoDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoDetailInsert
	@RepoId int =NULL ,
	@RepoDetailStatus int =NULL ,
	@StockId int =NULL ,
	@RepoApplyDetailId int =NULL ,
	@RepoWeight numeric(18, 4) =NULL ,
	@Unit int =NULL ,
	@RepoPrice numeric(18, 4) =NULL ,
	@Currency int =NULL ,
	@StockLogId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_RepoDetail] (
	[RepoId],
	[RepoDetailStatus],
	[StockId],
	[RepoApplyDetailId],
	[RepoWeight],
	[Unit],
	[RepoPrice],
	[Currency],
	[StockLogId]
) VALUES (
	@RepoId,
	@RepoDetailStatus,
	@StockId,
	@RepoApplyDetailId,
	@RepoWeight,
	@Unit,
	@RepoPrice,
	@Currency,
	@StockLogId
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
// 存储过程名：[dbo].St_RepoDetailUpdate
// 存储过程功能描述：更新St_RepoDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoDetailUpdate
    @DetailId int,
@RepoId int = NULL,
@RepoDetailStatus int = NULL,
@StockId int = NULL,
@RepoApplyDetailId int = NULL,
@RepoWeight numeric(18, 4) = NULL,
@Unit int = NULL,
@RepoPrice numeric(18, 4) = NULL,
@Currency int = NULL,
@StockLogId int = NULL
AS

UPDATE [dbo].[St_RepoDetail] SET
	[RepoId] = @RepoId,
	[RepoDetailStatus] = @RepoDetailStatus,
	[StockId] = @StockId,
	[RepoApplyDetailId] = @RepoApplyDetailId,
	[RepoWeight] = @RepoWeight,
	[Unit] = @Unit,
	[RepoPrice] = @RepoPrice,
	[Currency] = @Currency,
	[StockLogId] = @StockLogId
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



