alter table dbo.St_RepoApply
   drop constraint PK_ST_REPOAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_RepoApply')
            and   type = 'U')
   drop table dbo.St_RepoApply
go

/*==============================================================*/
/* Table: St_RepoApply                                          */
/*==============================================================*/
create table dbo.St_RepoApply (
   RepoApplyId          int                  identity,
   ApplyId              int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '回购申请',
   'user', 'dbo', 'table', 'St_RepoApply'
go

execute sp_addextendedproperty 'MS_Description', 
   '回购申请序号',
   'user', 'dbo', 'table', 'St_RepoApply', 'column', 'RepoApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请主表序号',
   'user', 'dbo', 'table', 'St_RepoApply', 'column', 'ApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'St_RepoApply', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'St_RepoApply', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'St_RepoApply', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'St_RepoApply', 'column', 'LastModifyTime'
go

alter table dbo.St_RepoApply
   add constraint PK_ST_REPOAPPLY primary key (RepoApplyId)
go

/****** Object:  Stored Procedure [dbo].St_RepoApplyGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyGet]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_RepoApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_RepoApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_RepoApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_RepoApplyUpdateStatus
// 存储过程功能描述：更新St_RepoApply中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoApply'

set @str = 'update [dbo].[St_RepoApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RepoApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_RepoApplyGoBack
// 存储过程功能描述：撤返St_RepoApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_RepoApply'

set @str = 'update [dbo].[St_RepoApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RepoApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_RepoApplyGet
// 存储过程功能描述：查询指定St_RepoApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyGet
    /*
	@RepoApplyId int
    */
    @id int
AS

SELECT
	[RepoApplyId],
	[ApplyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_RepoApply]
WHERE
	[RepoApplyId] = @id

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
// 存储过程名：[dbo].St_RepoApplyLoad
// 存储过程功能描述：查询所有St_RepoApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyLoad
AS

SELECT
	[RepoApplyId],
	[ApplyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_RepoApply]

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
// 存储过程名：[dbo].St_RepoApplyInsert
// 存储过程功能描述：新增一条St_RepoApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyInsert
	@ApplyId int =NULL ,
	@CreatorId int =NULL ,
	@RepoApplyId int OUTPUT
AS

INSERT INTO [dbo].[St_RepoApply] (
	[ApplyId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ApplyId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RepoApplyId = @@IDENTITY

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
// 存储过程名：[dbo].St_RepoApplyUpdate
// 存储过程功能描述：更新St_RepoApply
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_RepoApplyUpdate
    @RepoApplyId int,
@ApplyId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_RepoApply] SET
	[ApplyId] = @ApplyId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RepoApplyId] = @RepoApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



