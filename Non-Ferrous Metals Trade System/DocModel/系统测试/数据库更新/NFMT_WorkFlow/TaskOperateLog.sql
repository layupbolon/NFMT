alter table Wf_TaskOperateLog
   drop constraint PK_WF_TASKOPERATELOG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_TaskOperateLog')
            and   type = 'U')
   drop table Wf_TaskOperateLog
go

/*==============================================================*/
/* Table: Wf_TaskOperateLog                                     */
/*==============================================================*/
create table Wf_TaskOperateLog (
   LogId                int                  identity,
   TaskNodeId           int                  null,
   EmpId                int                  null,
   Memo                 varchar(4000)        null,
   LogTime              datetime             null,
   LogResult            varchar(400)         null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '任务操作记录表',
   'user', @CurrentUser, 'table', 'Wf_TaskOperateLog'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '记录序号',
   'user', @CurrentUser, 'table', 'Wf_TaskOperateLog', 'column', 'LogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务节点序号',
   'user', @CurrentUser, 'table', 'Wf_TaskOperateLog', 'column', 'TaskNodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作员序号',
   'user', @CurrentUser, 'table', 'Wf_TaskOperateLog', 'column', 'EmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作附言',
   'user', @CurrentUser, 'table', 'Wf_TaskOperateLog', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作时间',
   'user', @CurrentUser, 'table', 'Wf_TaskOperateLog', 'column', 'LogTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作结果',
   'user', @CurrentUser, 'table', 'Wf_TaskOperateLog', 'column', 'LogResult'
go

alter table Wf_TaskOperateLog
   add constraint PK_WF_TASKOPERATELOG primary key (LogId)
go


/****** Object:  Stored Procedure [dbo].Wf_TaskOperateLogGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskOperateLogGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskOperateLogGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskOperateLogLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskOperateLogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskOperateLogLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskOperateLogInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskOperateLogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskOperateLogInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskOperateLogUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskOperateLogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskOperateLogUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskOperateLogUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskOperateLogUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskOperateLogUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskOperateLogUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskOperateLogGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskOperateLogGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskOperateLogUpdateStatus
// 存储过程功能描述：更新Wf_TaskOperateLog中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskOperateLogUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskOperateLog'

set @str = 'update [dbo].[Wf_TaskOperateLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where LogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskOperateLogGoBack
// 存储过程功能描述：撤返Wf_TaskOperateLog，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskOperateLogGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskOperateLog'

set @str = 'update [dbo].[Wf_TaskOperateLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where LogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskOperateLogGet
// 存储过程功能描述：查询指定Wf_TaskOperateLog的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskOperateLogGet
    /*
	@LogId int
    */
    @id int
AS

SELECT
	[LogId],
	[TaskNodeId],
	[EmpId],
	[Memo],
	[LogTime],
	[LogResult]
FROM
	[dbo].[Wf_TaskOperateLog]
WHERE
	[LogId] = @id

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
// 存储过程名：[dbo].Wf_TaskOperateLogLoad
// 存储过程功能描述：查询所有Wf_TaskOperateLog记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskOperateLogLoad
AS

SELECT
	[LogId],
	[TaskNodeId],
	[EmpId],
	[Memo],
	[LogTime],
	[LogResult]
FROM
	[dbo].[Wf_TaskOperateLog]

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
// 存储过程名：[dbo].Wf_TaskOperateLogInsert
// 存储过程功能描述：新增一条Wf_TaskOperateLog记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskOperateLogInsert
	@TaskNodeId int ,
	@EmpId int ,
	@Memo varchar(4000) =NULL ,
	@LogTime datetime =NULL ,
	@LogResult varchar(400) =NULL ,
	@LogId int OUTPUT
AS

INSERT INTO [dbo].[Wf_TaskOperateLog] (
	[TaskNodeId],
	[EmpId],
	[Memo],
	[LogTime],
	[LogResult]
) VALUES (
	@TaskNodeId,
	@EmpId,
	@Memo,
	@LogTime,
	@LogResult
)


SET @LogId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_TaskOperateLogUpdate
// 存储过程功能描述：更新Wf_TaskOperateLog
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskOperateLogUpdate
    @LogId int,
@TaskNodeId int,
@EmpId int,
@Memo varchar(4000) = NULL,
@LogTime datetime = NULL,
@LogResult varchar(400) = NULL
AS

UPDATE [dbo].[Wf_TaskOperateLog] SET
	[TaskNodeId] = @TaskNodeId,
	[EmpId] = @EmpId,
	[Memo] = @Memo,
	[LogTime] = @LogTime,
	[LogResult] = @LogResult
WHERE
	[LogId] = @LogId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



