alter table St_SplitDocAttach
   drop constraint PK_ST_SPLITDOCATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_SplitDocAttach')
            and   type = 'U')
   drop table St_SplitDocAttach
go

/*==============================================================*/
/* Table: St_SplitDocAttach                                     */
/*==============================================================*/
create table St_SplitDocAttach (
   SplitDocAttachId     int                  identity,
   SplitDocId           int                  null,
   SplitDocDetailId     int                  null,
   AttachId             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '拆单附件',
   'user', @CurrentUser, 'table', 'St_SplitDocAttach'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单附件序号',
   'user', @CurrentUser, 'table', 'St_SplitDocAttach', 'column', 'SplitDocAttachId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单序号',
   'user', @CurrentUser, 'table', 'St_SplitDocAttach', 'column', 'SplitDocId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单明细序号',
   'user', @CurrentUser, 'table', 'St_SplitDocAttach', 'column', 'SplitDocDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_SplitDocAttach', 'column', 'AttachId'
go

alter table St_SplitDocAttach
   add constraint PK_ST_SPLITDOCATTACH primary key (SplitDocAttachId)
go

/****** Object:  Stored Procedure [dbo].St_SplitDocAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_SplitDocAttachUpdateStatus
// 存储过程功能描述：更新St_SplitDocAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDocAttach'

set @str = 'update [dbo].[St_SplitDocAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SplitDocAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_SplitDocAttachGoBack
// 存储过程功能描述：撤返St_SplitDocAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDocAttach'

set @str = 'update [dbo].[St_SplitDocAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SplitDocAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_SplitDocAttachGet
// 存储过程功能描述：查询指定St_SplitDocAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocAttachGet
    /*
	@SplitDocAttachId int
    */
    @id int
AS

SELECT
	[SplitDocAttachId],
	[SplitDocId],
	[SplitDocDetailId],
	[AttachId]
FROM
	[dbo].[St_SplitDocAttach]
WHERE
	[SplitDocAttachId] = @id

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
// 存储过程名：[dbo].St_SplitDocAttachLoad
// 存储过程功能描述：查询所有St_SplitDocAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocAttachLoad
AS

SELECT
	[SplitDocAttachId],
	[SplitDocId],
	[SplitDocDetailId],
	[AttachId]
FROM
	[dbo].[St_SplitDocAttach]

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
// 存储过程名：[dbo].St_SplitDocAttachInsert
// 存储过程功能描述：新增一条St_SplitDocAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocAttachInsert
	@SplitDocId int =NULL ,
	@SplitDocDetailId int =NULL ,
	@AttachId int =NULL ,
	@SplitDocAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_SplitDocAttach] (
	[SplitDocId],
	[SplitDocDetailId],
	[AttachId]
) VALUES (
	@SplitDocId,
	@SplitDocDetailId,
	@AttachId
)


SET @SplitDocAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_SplitDocAttachUpdate
// 存储过程功能描述：更新St_SplitDocAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocAttachUpdate
    @SplitDocAttachId int,
@SplitDocId int = NULL,
@SplitDocDetailId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_SplitDocAttach] SET
	[SplitDocId] = @SplitDocId,
	[SplitDocDetailId] = @SplitDocDetailId,
	[AttachId] = @AttachId
WHERE
	[SplitDocAttachId] = @SplitDocAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



