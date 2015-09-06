alter table dbo.St_Repo
   drop constraint PK_ST_REPO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_Repo')
            and   type = 'U')
   drop table dbo.St_Repo
go

/*==============================================================*/
/* Table: St_Repo                                               */
/*==============================================================*/
create table dbo.St_Repo (
   RepoId               int                  identity,
   RepoApplyId          int                  not null,
   Repoer               int                  null,
   RepoerTime           datetime             null,
   RepoStatus           int                  null,
   Memo                 varchar(4000)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '回购',
   'user', 'dbo', 'table', 'St_Repo'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购序号',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'RepoId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购申请序号',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'RepoApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购确认人',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'Repoer'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购确认时间',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'RepoerTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购状态',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'RepoStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '附言',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'St_Repo', 'column', 'LastModifyTime'
go

alter table dbo.St_Repo
   add constraint PK_ST_REPO primary key (RepoId)
go

/****** Object:  Stored Procedure [dbo].St_RepoGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoGet]
GO

/****** Object:  Stored Procedure [dbo].St_RepoLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoLoad]
GO

/****** Object:  Stored Procedure [dbo].St_RepoInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoInsert]
GO

/****** Object:  Stored Procedure [dbo].St_RepoUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_RepoUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_RepoUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_RepoUpdateStatus
// 存储过程功能描述：更新St_Repo中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_Repo'

set @str = 'update [dbo].[St_Repo] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RepoId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_RepoGoBack
// 存储过程功能描述：撤返St_Repo，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_Repo'

set @str = 'update [dbo].[St_Repo] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RepoId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_RepoGet
// 存储过程功能描述：查询指定St_Repo的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoGet
    /*
	@RepoId int
    */
    @id int
AS

SELECT
	[RepoId],
	[RepoApplyId],
	[Repoer],
	[RepoerTime],
	[RepoStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_Repo]
WHERE
	[RepoId] = @id

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
// 存储过程名：[dbo].St_RepoLoad
// 存储过程功能描述：查询所有St_Repo记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoLoad
AS

SELECT
	[RepoId],
	[RepoApplyId],
	[Repoer],
	[RepoerTime],
	[RepoStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_Repo]

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
// 存储过程名：[dbo].St_RepoInsert
// 存储过程功能描述：新增一条St_Repo记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoInsert
	@RepoApplyId int ,
	@Repoer int =NULL ,
	@RepoerTime datetime =NULL ,
	@RepoStatus int =NULL ,
	@Memo varchar(4000) =NULL ,
	@CreatorId int =NULL ,
	@RepoId int OUTPUT
AS

INSERT INTO [dbo].[St_Repo] (
	[RepoApplyId],
	[Repoer],
	[RepoerTime],
	[RepoStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@RepoApplyId,
	@Repoer,
	@RepoerTime,
	@RepoStatus,
	@Memo,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RepoId = @@IDENTITY

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
// 存储过程名：[dbo].St_RepoUpdate
// 存储过程功能描述：更新St_Repo
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoUpdate
    @RepoId int,
@RepoApplyId int,
@Repoer int = NULL,
@RepoerTime datetime = NULL,
@RepoStatus int = NULL,
@Memo varchar(4000) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_Repo] SET
	[RepoApplyId] = @RepoApplyId,
	[Repoer] = @Repoer,
	[RepoerTime] = @RepoerTime,
	[RepoStatus] = @RepoStatus,
	[Memo] = @Memo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RepoId] = @RepoId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



