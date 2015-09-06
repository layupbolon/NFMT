alter table dbo.St_RepoApplyDetail
   drop constraint PK_ST_REPOAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_RepoApplyDetail')
            and   type = 'U')
   drop table dbo.St_RepoApplyDetail
go

/*==============================================================*/
/* Table: St_RepoApplyDetail                                    */
/*==============================================================*/
create table dbo.St_RepoApplyDetail (
   DetailId             int                  identity,
   RepoApplyId          int                  not null,
   StockId              int                  null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '回购申请库存明细',
   'user', 'dbo', 'table', 'St_RepoApplyDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'St_RepoApplyDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购申请序号',
   'user', 'dbo', 'table', 'St_RepoApplyDetail', 'column', 'RepoApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'St_RepoApplyDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购明细状态',
   'user', 'dbo', 'table', 'St_RepoApplyDetail', 'column', 'DetailStatus'
go

alter table dbo.St_RepoApplyDetail
   add constraint PK_ST_REPOAPPLYDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_RepoApplyDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_RepoApplyDetailUpdateStatus
// 存储过程功能描述：更新St_RepoApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoApplyDetail'

set @str = 'update [dbo].[St_RepoApplyDetail] '+
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
// 存储过程名：[dbo].St_RepoApplyDetailGoBack
// 存储过程功能描述：撤返St_RepoApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoApplyDetail'

set @str = 'update [dbo].[St_RepoApplyDetail] '+
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
// 存储过程名：[dbo].St_RepoApplyDetailGet
// 存储过程功能描述：查询指定St_RepoApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[RepoApplyId],
	[StockId],
	[DetailStatus]
FROM
	[dbo].[St_RepoApplyDetail]
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
// 存储过程名：[dbo].St_RepoApplyDetailLoad
// 存储过程功能描述：查询所有St_RepoApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyDetailLoad
AS

SELECT
	[DetailId],
	[RepoApplyId],
	[StockId],
	[DetailStatus]
FROM
	[dbo].[St_RepoApplyDetail]

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
// 存储过程名：[dbo].St_RepoApplyDetailInsert
// 存储过程功能描述：新增一条St_RepoApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyDetailInsert
	@RepoApplyId int ,
	@StockId int =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_RepoApplyDetail] (
	[RepoApplyId],
	[StockId],
	[DetailStatus]
) VALUES (
	@RepoApplyId,
	@StockId,
	@DetailStatus
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
// 存储过程名：[dbo].St_RepoApplyDetailUpdate
// 存储过程功能描述：更新St_RepoApplyDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyDetailUpdate
    @DetailId int,
@RepoApplyId int,
@StockId int = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[St_RepoApplyDetail] SET
	[RepoApplyId] = @RepoApplyId,
	[StockId] = @StockId,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



