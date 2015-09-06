alter table BDStyle
   drop constraint PK_BDSTYLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BDStyle')
            and   type = 'U')
   drop table BDStyle
go

/*==============================================================*/
/* Table: BDStyle                                               */
/*==============================================================*/
create table BDStyle (
   BDStyleId            int                  identity,
   BDStyleCode          varchar(80)          null,
   BDStyleName          varchar(80)          null,
   BDStyleStatus        int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '基础类型编码表',
   'user', @CurrentUser, 'table', 'BDStyle'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '基础类型序号',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'BDStyleId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '基础类型编号',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'BDStyleCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '基础类型名称',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'BDStyleName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '基础类型状态',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'BDStyleStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'BDStyle', 'column', 'LastModifyTime'
go

alter table BDStyle
   add constraint PK_BDSTYLE primary key (BDStyleId)
go


/****** Object:  Stored Procedure [dbo].BDStyleGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleGet]
GO

/****** Object:  Stored Procedure [dbo].BDStyleLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleLoad]
GO

/****** Object:  Stored Procedure [dbo].BDStyleInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleInsert]
GO

/****** Object:  Stored Procedure [dbo].BDStyleUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleUpdate]
GO

/****** Object:  Stored Procedure [dbo].BDStyleUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].BDStyleUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStyleGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStyleGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStyleUpdateStatus
// 存储过程功能描述：更新BDStyle中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BDStyle'

set @str = 'update [dbo].[BDStyle] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BDStyleId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BDStyleGoBack
// 存储过程功能描述：撤返BDStyle，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BDStyle'

set @str = 'update [dbo].[BDStyle] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BDStyleId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BDStyleGet
// 存储过程功能描述：查询指定BDStyle的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleGet
    /*
	@BDStyleId int
    */
    @id int
AS

SELECT
	[BDStyleId],
	[BDStyleCode],
	[BDStyleName],
	[BDStyleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyle]
WHERE
	[BDStyleId] = @id

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
// 存储过程名：[dbo].BDStyleLoad
// 存储过程功能描述：查询所有BDStyle记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleLoad
AS

SELECT
	[BDStyleId],
	[BDStyleCode],
	[BDStyleName],
	[BDStyleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStyle]

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
// 存储过程名：[dbo].BDStyleInsert
// 存储过程功能描述：新增一条BDStyle记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleInsert
	@BDStyleCode varchar(80) ,
	@BDStyleName varchar(80) ,
	@BDStyleStatus int ,
	@CreatorId int =NULL ,
	@BDStyleId int OUTPUT
AS

INSERT INTO [dbo].[BDStyle] (
	[BDStyleCode],
	[BDStyleName],
	[BDStyleStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@BDStyleCode,
	@BDStyleName,
	@BDStyleStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BDStyleId = @@IDENTITY

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
// 存储过程名：[dbo].BDStyleUpdate
// 存储过程功能描述：更新BDStyle
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStyleUpdate
    @BDStyleId int,
@BDStyleCode varchar(80),
@BDStyleName varchar(80),
@BDStyleStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BDStyle] SET
	[BDStyleCode] = @BDStyleCode,
	[BDStyleName] = @BDStyleName,
	[BDStyleStatus] = @BDStyleStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BDStyleId] = @BDStyleId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



