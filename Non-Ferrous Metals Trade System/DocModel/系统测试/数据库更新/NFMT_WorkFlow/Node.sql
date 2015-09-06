alter table Wf_Node
   drop constraint PK_WF_NODE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_Node')
            and   type = 'U')
   drop table Wf_Node
go

/*==============================================================*/
/* Table: Wf_Node                                               */
/*==============================================================*/
create table Wf_Node (
   NodeId               int                  identity,
   MasterId             int                  null,
   NodeStatus           int                  null,
   NodeName             varchar(20)          null,
   NodeType             int                  null,
   IsFirst              bit                  null,
   IsLast               bit                  null,
   PreNodeId            int                  null,
   AuditEmpId           int                  null,
   AuthGroupId          int                  null,
   NodeLevel            int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '节点表',
   'user', @CurrentUser, 'table', 'Wf_Node'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点序号',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'NodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模版序号',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'MasterId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点状态',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'NodeStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点名称',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'NodeName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点类型（审核，知会）',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'NodeType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否第一节点',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'IsFirst'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否最终节点',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'IsLast'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上一节点序号',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'PreNodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核角色部门序号',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'AuditEmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限组序号',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'AuthGroupId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点级别',
   'user', @CurrentUser, 'table', 'Wf_Node', 'column', 'NodeLevel'
go

alter table Wf_Node
   add constraint PK_WF_NODE primary key (NodeId)
go


/****** Object:  Stored Procedure [dbo].Wf_NodeGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeUpdateStatus
// 存储过程功能描述：更新Wf_Node中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_Node'

set @str = 'update [dbo].[Wf_Node] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where NodeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_NodeGoBack
// 存储过程功能描述：撤返Wf_Node，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_Node'

set @str = 'update [dbo].[Wf_Node] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where NodeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_NodeGet
// 存储过程功能描述：查询指定Wf_Node的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeGet
    /*
	@NodeId int
    */
    @id int
AS

SELECT
	[NodeId],
	[MasterId],
	[NodeStatus],
	[NodeName],
	[NodeType],
	[IsFirst],
	[IsLast],
	[PreNodeId],
	[AuditEmpId],
	[AuthGroupId],
	[NodeLevel]
FROM
	[dbo].[Wf_Node]
WHERE
	[NodeId] = @id

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
// 存储过程名：[dbo].Wf_NodeLoad
// 存储过程功能描述：查询所有Wf_Node记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeLoad
AS

SELECT
	[NodeId],
	[MasterId],
	[NodeStatus],
	[NodeName],
	[NodeType],
	[IsFirst],
	[IsLast],
	[PreNodeId],
	[AuditEmpId],
	[AuthGroupId],
	[NodeLevel]
FROM
	[dbo].[Wf_Node]

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
// 存储过程名：[dbo].Wf_NodeInsert
// 存储过程功能描述：新增一条Wf_Node记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeInsert
	@MasterId int ,
	@NodeStatus int ,
	@NodeName varchar(20) ,
	@NodeType int =NULL ,
	@IsFirst bit =NULL ,
	@IsLast bit =NULL ,
	@PreNodeId int =NULL ,
	@AuditEmpId int =NULL ,
	@AuthGroupId int =NULL ,
	@NodeLevel int =NULL ,
	@NodeId int OUTPUT
AS

INSERT INTO [dbo].[Wf_Node] (
	[MasterId],
	[NodeStatus],
	[NodeName],
	[NodeType],
	[IsFirst],
	[IsLast],
	[PreNodeId],
	[AuditEmpId],
	[AuthGroupId],
	[NodeLevel]
) VALUES (
	@MasterId,
	@NodeStatus,
	@NodeName,
	@NodeType,
	@IsFirst,
	@IsLast,
	@PreNodeId,
	@AuditEmpId,
	@AuthGroupId,
	@NodeLevel
)


SET @NodeId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_NodeUpdate
// 存储过程功能描述：更新Wf_Node
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeUpdate
    @NodeId int,
@MasterId int,
@NodeStatus int,
@NodeName varchar(20),
@NodeType int = NULL,
@IsFirst bit = NULL,
@IsLast bit = NULL,
@PreNodeId int = NULL,
@AuditEmpId int = NULL,
@AuthGroupId int = NULL,
@NodeLevel int = NULL
AS

UPDATE [dbo].[Wf_Node] SET
	[MasterId] = @MasterId,
	[NodeStatus] = @NodeStatus,
	[NodeName] = @NodeName,
	[NodeType] = @NodeType,
	[IsFirst] = @IsFirst,
	[IsLast] = @IsLast,
	[PreNodeId] = @PreNodeId,
	[AuditEmpId] = @AuditEmpId,
	[AuthGroupId] = @AuthGroupId,
	[NodeLevel] = @NodeLevel
WHERE
	[NodeId] = @NodeId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



