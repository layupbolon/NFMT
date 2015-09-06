alter table Wf_Task
   drop constraint PK_WF_TASK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_Task')
            and   type = 'U')
   drop table Wf_Task
go

/*==============================================================*/
/* Table: Wf_Task                                               */
/*==============================================================*/
create table Wf_Task (
   TaskId               int                  identity,
   MasterId             int                  null,
   TaskName              varchar(200)        null,
   TaskConnext          varchar(max)         null,
   TaskStatus           int                  null,
   DataSourceId         int                  null,
   TaskType             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '任务表',
   'user', @CurrentUser, 'table', 'Wf_Task'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务序号',
   'user', @CurrentUser, 'table', 'Wf_Task', 'column', 'TaskId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模版序号',
   'user', @CurrentUser, 'table', 'Wf_Task', 'column', 'MasterId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务名称',
   'user', @CurrentUser, 'table', 'Wf_Task', 'column', 'TaskName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务内容',
   'user', @CurrentUser, 'table', 'Wf_Task', 'column', 'TaskConnext'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务状态',
   'user', @CurrentUser, 'table', 'Wf_Task', 'column', 'TaskStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数据源序号',
   'user', @CurrentUser, 'table', 'Wf_Task', 'column', 'DataSourceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务类型',
   'user', @CurrentUser, 'table', 'Wf_Task', 'column', 'TaskType'
go

alter table Wf_Task
   add constraint PK_WF_TASK primary key (TaskId)
go


/****** Object:  Stored Procedure [dbo].Wf_TaskGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskUpdateStatus
// 存储过程功能描述：更新Wf_Task中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_Task'

set @str = 'update [dbo].[Wf_Task] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where TaskId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskGoBack
// 存储过程功能描述：撤返Wf_Task，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_Task'

set @str = 'update [dbo].[Wf_Task] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where TaskId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskGet
// 存储过程功能描述：查询指定Wf_Task的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskGet
    /*
	@TaskId int
    */
    @id int
AS

SELECT
	[TaskId],
	[MasterId],
	[TaskName],
	[TaskConnext],
	[TaskStatus],
	[DataSourceId],
	[TaskType]
FROM
	[dbo].[Wf_Task]
WHERE
	[TaskId] = @id

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
// 存储过程名：[dbo].Wf_TaskLoad
// 存储过程功能描述：查询所有Wf_Task记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskLoad
AS

SELECT
	[TaskId],
	[MasterId],
	[TaskName],
	[TaskConnext],
	[TaskStatus],
	[DataSourceId],
	[TaskType]
FROM
	[dbo].[Wf_Task]

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
// 存储过程名：[dbo].Wf_TaskInsert
// 存储过程功能描述：新增一条Wf_Task记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskInsert
	@MasterId int =NULL ,
	@TaskName varchar(200) =NULL ,
	@TaskConnext varchar(max) =NULL ,
	@TaskStatus int =NULL ,
	@DataSourceId int =NULL ,
	@TaskType int =NULL ,
	@TaskId int OUTPUT
AS

INSERT INTO [dbo].[Wf_Task] (
	[MasterId],
	[TaskName],
	[TaskConnext],
	[TaskStatus],
	[DataSourceId],
	[TaskType]
) VALUES (
	@MasterId,
	@TaskName,
	@TaskConnext,
	@TaskStatus,
	@DataSourceId,
	@TaskType
)


SET @TaskId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_TaskUpdate
// 存储过程功能描述：更新Wf_Task
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskUpdate
    @TaskId int,
@MasterId int = NULL,
@TaskName varchar(200) = NULL,
@TaskConnext varchar(max) = NULL,
@TaskStatus int = NULL,
@DataSourceId int = NULL,
@TaskType int = NULL
AS

UPDATE [dbo].[Wf_Task] SET
	[MasterId] = @MasterId,
	[TaskName] = @TaskName,
	[TaskConnext] = @TaskConnext,
	[TaskStatus] = @TaskStatus,
	[DataSourceId] = @DataSourceId,
	[TaskType] = @TaskType
WHERE
	[TaskId] = @TaskId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



