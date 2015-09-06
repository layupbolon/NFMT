alter table dbo.Attach
   drop constraint PK_ATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Attach')
            and   type = 'U')
   drop table dbo.Attach
go

/*==============================================================*/
/* Table: Attach                                                */
/*==============================================================*/
create table dbo.Attach (
   AttachId             int                  identity,
   AttachName           varchar(200)         null,
   ServerAttachName     varchar(200)         null,
   AttachExt            varchar(20)          null,
   AttachType           int                  null,
   AttachInfo           varchar(4000)        null,
   AttachLength         int                  null,
   AttachPath           varchar(400)         null,
   AttachStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '附件',
   'user', 'dbo', 'table', 'Attach'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件名称，不包含扩展名',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachName'
go

execute sp_addextendedproperty 'MS_Description', 
   '服务端文件名',
   'user', 'dbo', 'table', 'Attach', 'column', 'ServerAttachName'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件扩展名',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachExt'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件类型',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachType'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件描述',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachInfo'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件大小',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachLength'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件路径',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachPath'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件状态',
   'user', 'dbo', 'table', 'Attach', 'column', 'AttachStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Attach', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Attach', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Attach', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Attach', 'column', 'LastModifyTime'
go

alter table dbo.Attach
   add constraint PK_ATTACH primary key (AttachId)
go


/****** Object:  Stored Procedure [dbo].AttachGet    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AttachGet]
GO

/****** Object:  Stored Procedure [dbo].AttachLoad    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AttachLoad]
GO

/****** Object:  Stored Procedure [dbo].AttachInsert    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AttachInsert]
GO

/****** Object:  Stored Procedure [dbo].AttachUpdate    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].AttachUpdateStatus    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].AttachUpdateStatus    Script Date: 2014年12月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AttachUpdateStatus
// 存储过程功能描述：更新Attach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Attach'

set @str = 'update [dbo].[Attach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AttachGoBack
// 存储过程功能描述：撤返Attach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Attach'

set @str = 'update [dbo].[Attach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AttachGet
// 存储过程功能描述：查询指定Attach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AttachGet
    /*
	@AttachId int
    */
    @id int
AS

SELECT
	[AttachId],
	[AttachName],
	[ServerAttachName],
	[AttachExt],
	[AttachType],
	[AttachInfo],
	[AttachLength],
	[AttachPath],
	[AttachStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Attach]
WHERE
	[AttachId] = @id

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
// 存储过程名：[dbo].AttachLoad
// 存储过程功能描述：查询所有Attach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AttachLoad
AS

SELECT
	[AttachId],
	[AttachName],
	[ServerAttachName],
	[AttachExt],
	[AttachType],
	[AttachInfo],
	[AttachLength],
	[AttachPath],
	[AttachStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Attach]

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
// 存储过程名：[dbo].AttachInsert
// 存储过程功能描述：新增一条Attach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AttachInsert
	@AttachName varchar(200) =NULL ,
	@ServerAttachName varchar(200) =NULL ,
	@AttachExt varchar(20) =NULL ,
	@AttachType int =NULL ,
	@AttachInfo varchar(4000) =NULL ,
	@AttachLength int =NULL ,
	@AttachPath varchar(400) =NULL ,
	@AttachStatus int =NULL ,
	@CreatorId int =NULL ,
	@AttachId int OUTPUT
AS

INSERT INTO [dbo].[Attach] (
	[AttachName],
	[ServerAttachName],
	[AttachExt],
	[AttachType],
	[AttachInfo],
	[AttachLength],
	[AttachPath],
	[AttachStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AttachName,
	@ServerAttachName,
	@AttachExt,
	@AttachType,
	@AttachInfo,
	@AttachLength,
	@AttachPath,
	@AttachStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AttachId = @@IDENTITY

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
// 存储过程名：[dbo].AttachUpdate
// 存储过程功能描述：更新Attach
// 创建人：CodeSmith
// 创建时间： 2014年12月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AttachUpdate
    @AttachId int,
@AttachName varchar(200) = NULL,
@ServerAttachName varchar(200) = NULL,
@AttachExt varchar(20) = NULL,
@AttachType int = NULL,
@AttachInfo varchar(4000) = NULL,
@AttachLength int = NULL,
@AttachPath varchar(400) = NULL,
@AttachStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Attach] SET
	[AttachName] = @AttachName,
	[ServerAttachName] = @ServerAttachName,
	[AttachExt] = @AttachExt,
	[AttachType] = @AttachType,
	[AttachInfo] = @AttachInfo,
	[AttachLength] = @AttachLength,
	[AttachPath] = @AttachPath,
	[AttachStatus] = @AttachStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AttachId] = @AttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



