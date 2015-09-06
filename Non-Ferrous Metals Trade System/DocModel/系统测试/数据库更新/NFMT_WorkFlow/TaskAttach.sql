alter table Wf_TaskAttach
   drop constraint PK_WF_TASKATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_TaskAttach')
            and   type = 'U')
   drop table Wf_TaskAttach
go

/*==============================================================*/
/* Table: Wf_TaskAttach                                         */
/*==============================================================*/
create table Wf_TaskAttach (
   TaskAttachId         int                  identity,
   TaskId               int                  null,
   AttachId             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '任务附件',
   'user', @CurrentUser, 'table', 'Wf_TaskAttach'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务附件序号',
   'user', @CurrentUser, 'table', 'Wf_TaskAttach', 'column', 'TaskAttachId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务序号',
   'user', @CurrentUser, 'table', 'Wf_TaskAttach', 'column', 'TaskId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件主表序号',
   'user', @CurrentUser, 'table', 'Wf_TaskAttach', 'column', 'AttachId'
go

alter table Wf_TaskAttach
   add constraint PK_WF_TASKATTACH primary key (TaskAttachId)
go

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachGet    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachLoad    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachInsert    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachUpdate    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachUpdateStatus    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachUpdateStatus    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskAttachUpdateStatus
// 存储过程功能描述：更新Wf_TaskAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskAttach'

set @str = 'update [dbo].[Wf_TaskAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where TaskAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskAttachGoBack
// 存储过程功能描述：撤返Wf_TaskAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskAttach'

set @str = 'update [dbo].[Wf_TaskAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where TaskAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskAttachGet
// 存储过程功能描述：查询指定Wf_TaskAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachGet
    /*
	@TaskAttachId int
    */
    @id int
AS

SELECT
	[TaskAttachId],
	[TaskId],
	[AttachId]
FROM
	[dbo].[Wf_TaskAttach]
WHERE
	[TaskAttachId] = @id

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
// 存储过程名：[dbo].Wf_TaskAttachLoad
// 存储过程功能描述：查询所有Wf_TaskAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachLoad
AS

SELECT
	[TaskAttachId],
	[TaskId],
	[AttachId]
FROM
	[dbo].[Wf_TaskAttach]

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
// 存储过程名：[dbo].Wf_TaskAttachInsert
// 存储过程功能描述：新增一条Wf_TaskAttach记录
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachInsert
	@TaskId int =NULL ,
	@AttachId int =NULL ,
	@TaskAttachId int OUTPUT
AS

INSERT INTO [dbo].[Wf_TaskAttach] (
	[TaskId],
	[AttachId]
) VALUES (
	@TaskId,
	@AttachId
)


SET @TaskAttachId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_TaskAttachUpdate
// 存储过程功能描述：更新Wf_TaskAttach
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachUpdate
    @TaskAttachId int,
@TaskId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[Wf_TaskAttach] SET
	[TaskId] = @TaskId,
	[AttachId] = @AttachId
WHERE
	[TaskAttachId] = @TaskAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



