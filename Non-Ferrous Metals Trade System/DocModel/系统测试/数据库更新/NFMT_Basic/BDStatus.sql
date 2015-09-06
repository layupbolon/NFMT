alter table dbo.BDStatus
   drop constraint PK_BDSTATUS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BDStatus')
            and   type = 'U')
   drop table dbo.BDStatus
go

/*==============================================================*/
/* Table: BDStatus                                              */
/*==============================================================*/
create table dbo.BDStatus (
   StatusId             int                  identity,
   StatusName           varchar(20)          null,
   StatusNameCode       varchar(20)          null,
   KeyName              varchar(20)          null,
   TableName            varchar(50)          null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '基础状态主表',
   'user', 'dbo', 'table', 'BDStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '状态序号',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'StatusId'
go

execute sp_addextendedproperty 'MS_Description', 
   '状态类型名称',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'StatusName'
go

execute sp_addextendedproperty 'MS_Description', 
   '状态类型编号',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'StatusNameCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '主键列名',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'KeyName'
go

execute sp_addextendedproperty 'MS_Description', 
   '表名',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'TableName'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'BDStatus', 'column', 'LastModifyTime'
go

alter table dbo.BDStatus
   add constraint PK_BDSTATUS primary key (StatusId)
go


/****** Object:  Stored Procedure [dbo].BDStatusGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStatusGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStatusGet]
GO

/****** Object:  Stored Procedure [dbo].BDStatusLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStatusLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStatusLoad]
GO

/****** Object:  Stored Procedure [dbo].BDStatusInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStatusInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStatusInsert]
GO

/****** Object:  Stored Procedure [dbo].BDStatusUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStatusUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStatusUpdate]
GO

/****** Object:  Stored Procedure [dbo].BDStatusUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStatusUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStatusUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].BDStatusUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BDStatusGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BDStatusGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BDStatusUpdateStatus
// 存储过程功能描述：更新BDStatus中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStatusUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BDStatus'

set @str = 'update [dbo].[BDStatus] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StatusId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BDStatusGoBack
// 存储过程功能描述：撤返BDStatus，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStatusGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BDStatus'

set @str = 'update [dbo].[BDStatus] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StatusId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BDStatusGet
// 存储过程功能描述：查询指定BDStatus的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStatusGet
    /*
	@StatusId int
    */
    @id int
AS

SELECT
	[StatusId],
	[StatusName],
	[StatusNameCode],
	[KeyName],
	[TableName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStatus]
WHERE
	[StatusId] = @id

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
// 存储过程名：[dbo].BDStatusLoad
// 存储过程功能描述：查询所有BDStatus记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStatusLoad
AS

SELECT
	[StatusId],
	[StatusName],
	[StatusNameCode],
	[KeyName],
	[TableName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BDStatus]

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
// 存储过程名：[dbo].BDStatusInsert
// 存储过程功能描述：新增一条BDStatus记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStatusInsert
	@StatusName varchar(20) =NULL ,
	@StatusNameCode varchar(20) =NULL ,
	@KeyName varchar(20) =NULL ,
	@TableName varchar(50) =NULL ,
	@CreatorId int =NULL ,
	@StatusId int OUTPUT
AS

INSERT INTO [dbo].[BDStatus] (
	[StatusName],
	[StatusNameCode],
	[KeyName],
	[TableName],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StatusName,
	@StatusNameCode,
	@KeyName,
	@TableName,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StatusId = @@IDENTITY

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
// 存储过程名：[dbo].BDStatusUpdate
// 存储过程功能描述：更新BDStatus
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BDStatusUpdate
    @StatusId int,
@StatusName varchar(20) = NULL,
@StatusNameCode varchar(20) = NULL,
@KeyName varchar(20) = NULL,
@TableName varchar(50) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BDStatus] SET
	[StatusName] = @StatusName,
	[StatusNameCode] = @StatusNameCode,
	[KeyName] = @KeyName,
	[TableName] = @TableName,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StatusId] = @StatusId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



