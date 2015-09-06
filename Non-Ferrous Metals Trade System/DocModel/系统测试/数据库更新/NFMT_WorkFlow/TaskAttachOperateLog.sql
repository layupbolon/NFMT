alter table Wf_TaskAttachOperateLog
   drop constraint PK_WF_TASKATTACHOPERATELOG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_TaskAttachOperateLog')
            and   type = 'U')
   drop table Wf_TaskAttachOperateLog
go

/*==============================================================*/
/* Table: Wf_TaskAttachOperateLog                               */
/*==============================================================*/
create table Wf_TaskAttachOperateLog (
   OperateLogId         int                  identity,
   LogId                int                  null,
   AttachId             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '任务附件操作记录表',
   'user', @CurrentUser, 'table', 'Wf_TaskAttachOperateLog'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Wf_TaskAttachOperateLog', 'column', 'OperateLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '记录序号',
   'user', @CurrentUser, 'table', 'Wf_TaskAttachOperateLog', 'column', 'LogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', @CurrentUser, 'table', 'Wf_TaskAttachOperateLog', 'column', 'AttachId'
go

alter table Wf_TaskAttachOperateLog
   add constraint PK_WF_TASKATTACHOPERATELOG primary key (OperateLogId)
go

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachOperateLogGet    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachOperateLogGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachOperateLogGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachOperateLogLoad    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachOperateLogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachOperateLogLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachOperateLogInsert    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachOperateLogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachOperateLogInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachOperateLogUpdate    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachOperateLogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachOperateLogUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachOperateLogUpdateStatus    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachOperateLogUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachOperateLogUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_TaskAttachOperateLogUpdateStatus    Script Date: 2015年1月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_TaskAttachOperateLogGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_TaskAttachOperateLogGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_TaskAttachOperateLogUpdateStatus
// 存储过程功能描述：更新Wf_TaskAttachOperateLog中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachOperateLogUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskAttachOperateLog'

set @str = 'update [dbo].[Wf_TaskAttachOperateLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where OperateLogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskAttachOperateLogGoBack
// 存储过程功能描述：撤返Wf_TaskAttachOperateLog，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachOperateLogGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_TaskAttachOperateLog'

set @str = 'update [dbo].[Wf_TaskAttachOperateLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where OperateLogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_TaskAttachOperateLogGet
// 存储过程功能描述：查询指定Wf_TaskAttachOperateLog的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachOperateLogGet
    /*
	@OperateLogId int
    */
    @id int
AS

SELECT
	[OperateLogId],
	[LogId],
	[AttachId]
FROM
	[dbo].[Wf_TaskAttachOperateLog]
WHERE
	[OperateLogId] = @id

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
// 存储过程名：[dbo].Wf_TaskAttachOperateLogLoad
// 存储过程功能描述：查询所有Wf_TaskAttachOperateLog记录
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachOperateLogLoad
AS

SELECT
	[OperateLogId],
	[LogId],
	[AttachId]
FROM
	[dbo].[Wf_TaskAttachOperateLog]

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
// 存储过程名：[dbo].Wf_TaskAttachOperateLogInsert
// 存储过程功能描述：新增一条Wf_TaskAttachOperateLog记录
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachOperateLogInsert
	@LogId int ,
	@AttachId int =NULL ,
	@OperateLogId int OUTPUT
AS

INSERT INTO [dbo].[Wf_TaskAttachOperateLog] (
	[LogId],
	[AttachId]
) VALUES (
	@LogId,
	@AttachId
)


SET @OperateLogId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_TaskAttachOperateLogUpdate
// 存储过程功能描述：更新Wf_TaskAttachOperateLog
// 创建人：CodeSmith
// 创建时间： 2015年1月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_TaskAttachOperateLogUpdate
    @OperateLogId int,
@LogId int,
@AttachId int = NULL
AS

UPDATE [dbo].[Wf_TaskAttachOperateLog] SET
	[LogId] = @LogId,
	[AttachId] = @AttachId
WHERE
	[OperateLogId] = @OperateLogId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



