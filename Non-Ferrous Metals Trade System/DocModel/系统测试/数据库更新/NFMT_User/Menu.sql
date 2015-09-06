alter table dbo.Menu
   drop constraint PK_MENU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Menu')
            and   type = 'U')
   drop table dbo.Menu
go

/*==============================================================*/
/* Table: Menu                                                  */
/*==============================================================*/
create table dbo.Menu (
   MenuId               int                  identity,
   MenuName             varchar(80)          null,
   MenuDesc             varchar(400)         null,
   ParentId             int                  null,
   FirstId              int                  null,
   Url                  varchar(400)         null,
   MenuStatus           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '功能菜单表',
   'user', 'dbo', 'table', 'Menu'
go

execute sp_addextendedproperty 'MS_Description', 
   '菜单序号',
   'user', 'dbo', 'table', 'Menu', 'column', 'MenuId'
go

execute sp_addextendedproperty 'MS_Description', 
   '功能菜单名称',
   'user', 'dbo', 'table', 'Menu', 'column', 'MenuName'
go

execute sp_addextendedproperty 'MS_Description', 
   '功能菜单描述',
   'user', 'dbo', 'table', 'Menu', 'column', 'MenuDesc'
go

execute sp_addextendedproperty 'MS_Description', 
   '上级菜单',
   'user', 'dbo', 'table', 'Menu', 'column', 'ParentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '一级菜单',
   'user', 'dbo', 'table', 'Menu', 'column', 'FirstId'
go

execute sp_addextendedproperty 'MS_Description', 
   '路径',
   'user', 'dbo', 'table', 'Menu', 'column', 'Url'
go

execute sp_addextendedproperty 'MS_Description', 
   '菜单状态',
   'user', 'dbo', 'table', 'Menu', 'column', 'MenuStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Menu', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Menu', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Menu', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Menu', 'column', 'LastModifyTime'
go

alter table dbo.Menu
   add constraint PK_MENU primary key (MenuId)
go

/****** Object:  Stored Procedure [dbo].MenuGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MenuGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MenuGet]
GO

/****** Object:  Stored Procedure [dbo].MenuLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MenuLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MenuLoad]
GO

/****** Object:  Stored Procedure [dbo].MenuInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MenuInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MenuInsert]
GO

/****** Object:  Stored Procedure [dbo].MenuUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MenuUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MenuUpdate]
GO

/****** Object:  Stored Procedure [dbo].MenuUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MenuUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MenuUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].MenuUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MenuGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MenuGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MenuUpdateStatus
// 存储过程功能描述：更新Menu中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MenuUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Menu'

set @str = 'update [dbo].[Menu] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MenuId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].MenuGoBack
// 存储过程功能描述：撤返Menu，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MenuGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Menu'

set @str = 'update [dbo].[Menu] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MenuId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].MenuGet
// 存储过程功能描述：查询指定Menu的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MenuGet
    /*
	@MenuId int
    */
    @id int
AS

SELECT
	[MenuId],
	[MenuName],
	[MenuDesc],
	[ParentId],
	[FirstId],
	[Url],
	[MenuStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Menu]
WHERE
	[MenuId] = @id

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
// 存储过程名：[dbo].MenuLoad
// 存储过程功能描述：查询所有Menu记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MenuLoad
AS

SELECT
	[MenuId],
	[MenuName],
	[MenuDesc],
	[ParentId],
	[FirstId],
	[Url],
	[MenuStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Menu]

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
// 存储过程名：[dbo].MenuInsert
// 存储过程功能描述：新增一条Menu记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MenuInsert
	@MenuName varchar(80) =NULL ,
	@MenuDesc varchar(400) =NULL ,
	@ParentId int =NULL ,
	@FirstId int =NULL ,
	@Url varchar(400) =NULL ,
	@MenuStatus int =NULL ,
	@CreatorId int =NULL ,
	@MenuId int OUTPUT
AS

INSERT INTO [dbo].[Menu] (
	[MenuName],
	[MenuDesc],
	[ParentId],
	[FirstId],
	[Url],
	[MenuStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MenuName,
	@MenuDesc,
	@ParentId,
	@FirstId,
	@Url,
	@MenuStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MenuId = @@IDENTITY

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
// 存储过程名：[dbo].MenuUpdate
// 存储过程功能描述：更新Menu
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MenuUpdate
    @MenuId int,
@MenuName varchar(80) = NULL,
@MenuDesc varchar(400) = NULL,
@ParentId int = NULL,
@FirstId int = NULL,
@Url varchar(400) = NULL,
@MenuStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Menu] SET
	[MenuName] = @MenuName,
	[MenuDesc] = @MenuDesc,
	[ParentId] = @ParentId,
	[FirstId] = @FirstId,
	[Url] = @Url,
	[MenuStatus] = @MenuStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MenuId] = @MenuId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



