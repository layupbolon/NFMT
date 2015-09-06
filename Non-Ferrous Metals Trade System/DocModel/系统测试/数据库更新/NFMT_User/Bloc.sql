alter table Bloc
   drop constraint PK_BLOC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Bloc')
            and   type = 'U')
   drop table Bloc
go

/*==============================================================*/
/* Table: Bloc                                                  */
/*==============================================================*/
create table Bloc (
   BlocId               int                  identity,
   BlocName             varchar(200)         null,
   BlocFullName         varchar(400)         null,
   BlocEname            varchar(400)         null,
   IsSelf               bit                  null,
   BlocStatus           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '集团',
   'user', @CurrentUser, 'table', 'Bloc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '集团序号',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'BlocId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '集团名称',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'BlocName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '集团全称',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'BlocFullName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '集团英文名称',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'BlocEname'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否己方集团',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'IsSelf'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '集团状态',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'BlocStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Bloc', 'column', 'LastModifyTime'
go

alter table Bloc
   add constraint PK_BLOC primary key (BlocId)
go


/****** Object:  Stored Procedure [dbo].BlocGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BlocGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BlocGet]
GO

/****** Object:  Stored Procedure [dbo].BlocLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BlocLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BlocLoad]
GO

/****** Object:  Stored Procedure [dbo].BlocInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BlocInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BlocInsert]
GO

/****** Object:  Stored Procedure [dbo].BlocUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BlocUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BlocUpdate]
GO

/****** Object:  Stored Procedure [dbo].BlocUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BlocUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BlocUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].BlocUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BlocGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BlocGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BlocUpdateStatus
// 存储过程功能描述：更新Bloc中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BlocUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Bloc'

set @str = 'update [dbo].[Bloc] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BlocId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BlocGoBack
// 存储过程功能描述：撤返Bloc，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BlocGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Bloc'

set @str = 'update [dbo].[Bloc] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BlocId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BlocGet
// 存储过程功能描述：查询指定Bloc的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BlocGet
    /*
	@BlocId int
    */
    @id int
AS

SELECT
	[BlocId],
	[BlocName],
	[BlocFullName],
	[BlocEname],
	[IsSelf],
	[BlocStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bloc]
WHERE
	[BlocId] = @id

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
// 存储过程名：[dbo].BlocLoad
// 存储过程功能描述：查询所有Bloc记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BlocLoad
AS

SELECT
	[BlocId],
	[BlocName],
	[BlocFullName],
	[BlocEname],
	[IsSelf],
	[BlocStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Bloc]

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
// 存储过程名：[dbo].BlocInsert
// 存储过程功能描述：新增一条Bloc记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BlocInsert
	@BlocName varchar(200) ,
	@BlocFullName varchar(400) =NULL ,
	@BlocEname varchar(400) =NULL ,
	@IsSelf bit =NULL ,
	@BlocStatus int =NULL ,
	@CreatorId int ,
	@BlocId int OUTPUT
AS

INSERT INTO [dbo].[Bloc] (
	[BlocName],
	[BlocFullName],
	[BlocEname],
	[IsSelf],
	[BlocStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BlocName,
	@BlocFullName,
	@BlocEname,
	@IsSelf,
	@BlocStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BlocId = @@IDENTITY

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
// 存储过程名：[dbo].BlocUpdate
// 存储过程功能描述：更新Bloc
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BlocUpdate
    @BlocId int,
@BlocName varchar(200),
@BlocFullName varchar(400) = NULL,
@BlocEname varchar(400) = NULL,
@IsSelf bit = NULL,
@BlocStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Bloc] SET
	[BlocName] = @BlocName,
	[BlocFullName] = @BlocFullName,
	[BlocEname] = @BlocEname,
	[IsSelf] = @IsSelf,
	[BlocStatus] = @BlocStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BlocId] = @BlocId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



