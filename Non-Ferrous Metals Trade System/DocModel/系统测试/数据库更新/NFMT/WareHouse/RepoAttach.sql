alter table dbo.St_RepoAttach
   drop constraint PK_ST_REPOATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_RepoAttach')
            and   type = 'U')
   drop table dbo.St_RepoAttach
go

/*==============================================================*/
/* Table: St_RepoAttach                                         */
/*==============================================================*/
create table dbo.St_RepoAttach (
   RepoAttachId         int                  identity,
   RepoId               int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '回购附件',
   'user', 'dbo', 'table', 'St_RepoAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购附件序号',
   'user', 'dbo', 'table', 'St_RepoAttach', 'column', 'RepoAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购序号',
   'user', 'dbo', 'table', 'St_RepoAttach', 'column', 'RepoId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'St_RepoAttach', 'column', 'AttachId'
go

alter table dbo.St_RepoAttach
   add constraint PK_ST_REPOATTACH primary key (RepoAttachId)
go

/****** Object:  Stored Procedure [dbo].St_RepoAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_RepoAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_RepoAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_RepoAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_RepoAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_RepoAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_RepoAttachUpdateStatus
// 存储过程功能描述：更新St_RepoAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoAttach'

set @str = 'update [dbo].[St_RepoAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RepoAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_RepoAttachGoBack
// 存储过程功能描述：撤返St_RepoAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoAttach'

set @str = 'update [dbo].[St_RepoAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RepoAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_RepoAttachGet
// 存储过程功能描述：查询指定St_RepoAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoAttachGet
    /*
	@RepoAttachId int
    */
    @id int
AS

SELECT
	[RepoAttachId],
	[RepoId],
	[AttachId]
FROM
	[dbo].[St_RepoAttach]
WHERE
	[RepoAttachId] = @id

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
// 存储过程名：[dbo].St_RepoAttachLoad
// 存储过程功能描述：查询所有St_RepoAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoAttachLoad
AS

SELECT
	[RepoAttachId],
	[RepoId],
	[AttachId]
FROM
	[dbo].[St_RepoAttach]

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
// 存储过程名：[dbo].St_RepoAttachInsert
// 存储过程功能描述：新增一条St_RepoAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoAttachInsert
	@RepoId int =NULL ,
	@AttachId int =NULL ,
	@RepoAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_RepoAttach] (
	[RepoId],
	[AttachId]
) VALUES (
	@RepoId,
	@AttachId
)


SET @RepoAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_RepoAttachUpdate
// 存储过程功能描述：更新St_RepoAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoAttachUpdate
    @RepoAttachId int,
@RepoId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_RepoAttach] SET
	[RepoId] = @RepoId,
	[AttachId] = @AttachId
WHERE
	[RepoAttachId] = @RepoAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



