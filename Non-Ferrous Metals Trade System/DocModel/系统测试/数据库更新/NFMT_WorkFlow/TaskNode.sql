alter table Wf_TaskNode
   drop constraint PK_WF_TASKNODE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_TaskNode')
            and   type = 'U')
   drop table Wf_TaskNode
go

/*==============================================================*/
/* Table: Wf_TaskNode                                           */
/*==============================================================*/
create table Wf_TaskNode (
   TaskNodeId           int                  identity,
   NodeId               int                  null,
   TaskId               int                  null,
   NodeLevel            int                  null,
   NodeStatus           int                  null,
   EmpId                int                  null,
   AuditTime            datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '任务节点',
   'user', @CurrentUser, 'table', 'Wf_TaskNode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务节点序号',
   'user', @CurrentUser, 'table', 'Wf_TaskNode', 'column', 'TaskNodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点序号',
   'user', @CurrentUser, 'table', 'Wf_TaskNode', 'column', 'NodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务序号',
   'user', @CurrentUser, 'table', 'Wf_TaskNode', 'column', 'TaskId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点级别',
   'user', @CurrentUser, 'table', 'Wf_TaskNode', 'column', 'NodeLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点状态',
   'user', @CurrentUser, 'table', 'Wf_TaskNode', 'column', 'NodeStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核人',
   'user', @CurrentUser, 'table', 'Wf_TaskNode', 'column', 'EmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核时间',
   'user', @CurrentUser, 'table', 'Wf_TaskNode', 'column', 'AuditTime'
go

alter table Wf_TaskNode
   add constraint PK_WF_TASKNODE primary key (TaskNodeId)
go


/****** Object:  Stored Procedure [dbo].Wf_TaskNodeGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskNodeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskNodeGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskNodeLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskNodeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskNodeLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskNodeInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskNodeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskNodeInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskNodeUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskNodeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskNodeUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskNodeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskNodeUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskNodeUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskNodeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskNodeGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskNodeGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskNodeUpdateStatus
// 存储过程功能描述：更新Wf_TaskNode中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskNodeUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskNode'

set @str = 'update [dbo].[Wf_TaskNode] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where TaskNodeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskNodeGoBack
// 存储过程功能描述：撤返Wf_TaskNode，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskNodeGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskNode'

set @str = 'update [dbo].[Wf_TaskNode] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where TaskNodeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskNodeGet
// 存储过程功能描述：查询指定Wf_TaskNode的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskNodeGet
    /*
	@TaskNodeId int
    */
    @id int
AS

SELECT
	[TaskNodeId],
	[NodeId],
	[TaskId],
	[NodeLevel],
	[NodeStatus],
	[EmpId],
	[AuditTime]
FROM
	[dbo].[Wf_TaskNode]
WHERE
	[TaskNodeId] = @id

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
// 存储过程名：[dbo].Wf_TaskNodeLoad
// 存储过程功能描述：查询所有Wf_TaskNode记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskNodeLoad
AS

SELECT
	[TaskNodeId],
	[NodeId],
	[TaskId],
	[NodeLevel],
	[NodeStatus],
	[EmpId],
	[AuditTime]
FROM
	[dbo].[Wf_TaskNode]

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
// 存储过程名：[dbo].Wf_TaskNodeInsert
// 存储过程功能描述：新增一条Wf_TaskNode记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskNodeInsert
	@NodeId int ,
	@TaskId int ,
	@NodeLevel int =NULL ,
	@NodeStatus int =NULL ,
	@EmpId int =NULL ,
	@AuditTime datetime =NULL ,
	@TaskNodeId int OUTPUT
AS

INSERT INTO [dbo].[Wf_TaskNode] (
	[NodeId],
	[TaskId],
	[NodeLevel],
	[NodeStatus],
	[EmpId],
	[AuditTime]
) VALUES (
	@NodeId,
	@TaskId,
	@NodeLevel,
	@NodeStatus,
	@EmpId,
	@AuditTime
)


SET @TaskNodeId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_TaskNodeUpdate
// 存储过程功能描述：更新Wf_TaskNode
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskNodeUpdate
    @TaskNodeId int,
@NodeId int,
@TaskId int,
@NodeLevel int = NULL,
@NodeStatus int = NULL,
@EmpId int = NULL,
@AuditTime datetime = NULL
AS

UPDATE [dbo].[Wf_TaskNode] SET
	[NodeId] = @NodeId,
	[TaskId] = @TaskId,
	[NodeLevel] = @NodeLevel,
	[NodeStatus] = @NodeStatus,
	[EmpId] = @EmpId,
	[AuditTime] = @AuditTime
WHERE
	[TaskNodeId] = @TaskNodeId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



