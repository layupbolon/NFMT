alter table Wf_DataSource
   drop constraint PK_WF_DATASOURCE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_DataSource')
            and   type = 'U')
   drop table Wf_DataSource
go

/*==============================================================*/
/* Table: Wf_DataSource                                         */
/*==============================================================*/
create table Wf_DataSource (
   SourceId             int                  identity,
   BaseName             varchar(200)         null,
   TableCode            varchar(50)          null,
   DataStatus           int                  null,
   RowId                int                  null,
   DalName              varchar(80)          null,
   AssName              varchar(50)          null,
   ViewUrl              varchar(400)         null,
   RefusalUrl           varchar(800)         null,
   SuccessUrl           varchar(800)         null,
   ConditionUrl         varchar(800)         null,
   EmpId                int                  null,
   ApplyTime            datetime             null,
   ApplyTitle           varchar(400)         null,
   ApplyMemo            varchar(4000)        null,
   ApplyInfo            varchar(4000)        null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '数据源表',
   'user', @CurrentUser, 'table', 'Wf_DataSource'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '源序号',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'SourceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库名',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'BaseName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '表名',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'TableCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数据状态',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'DataStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '表序号',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'RowId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '显示页面地址',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'ViewUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拒绝页面地址',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'RefusalUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通过页面地址',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'SuccessUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '条件页面地址',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'ConditionUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请人序号',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'EmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请时间',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'ApplyTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请标题',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'ApplyTitle'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请附言',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'ApplyMemo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请内容',
   'user', @CurrentUser, 'table', 'Wf_DataSource', 'column', 'ApplyInfo'
go

alter table Wf_DataSource
   add constraint PK_WF_DATASOURCE primary key (SourceId)
go


/****** Object:  Stored Procedure [dbo].Wf_DataSourceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_DataSourceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_DataSourceGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_DataSourceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_DataSourceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_DataSourceLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_DataSourceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_DataSourceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_DataSourceInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_DataSourceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_DataSourceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_DataSourceUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_DataSourceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_DataSourceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_DataSourceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_DataSourceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_DataSourceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_DataSourceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_DataSourceUpdateStatus
// 存储过程功能描述：更新Wf_DataSource中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_DataSourceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_DataSource'

set @str = 'update [dbo].[Wf_DataSource] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SourceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_DataSourceGoBack
// 存储过程功能描述：撤返Wf_DataSource，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_DataSourceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_DataSource'

set @str = 'update [dbo].[Wf_DataSource] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SourceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_DataSourceGet
// 存储过程功能描述：查询指定Wf_DataSource的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_DataSourceGet
    /*
	@SourceId int
    */
    @id int
AS

SELECT
	[SourceId],
	[BaseName],
	[TableCode],
	[DataStatus],
	[RowId],
	[DalName],
	[AssName],
	[ViewUrl],
	[RefusalUrl],
	[SuccessUrl],
	[ConditionUrl],
	[EmpId],
	[ApplyTime],
	[ApplyTitle],
	[ApplyMemo],
	[ApplyInfo]
FROM
	[dbo].[Wf_DataSource]
WHERE
	[SourceId] = @id

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
// 存储过程名：[dbo].Wf_DataSourceLoad
// 存储过程功能描述：查询所有Wf_DataSource记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_DataSourceLoad
AS

SELECT
	[SourceId],
	[BaseName],
	[TableCode],
	[DataStatus],
	[RowId],
	[DalName],
	[AssName],
	[ViewUrl],
	[RefusalUrl],
	[SuccessUrl],
	[ConditionUrl],
	[EmpId],
	[ApplyTime],
	[ApplyTitle],
	[ApplyMemo],
	[ApplyInfo]
FROM
	[dbo].[Wf_DataSource]

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
// 存储过程名：[dbo].Wf_DataSourceInsert
// 存储过程功能描述：新增一条Wf_DataSource记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_DataSourceInsert
	@BaseName varchar(200) =NULL ,
	@TableCode varchar(50) =NULL ,
	@DataStatus int =NULL ,
	@RowId int =NULL ,
	@DalName varchar(80) =NULL ,
	@AssName varchar(50) =NULL ,
	@ViewUrl varchar(400) =NULL ,
	@RefusalUrl varchar(800) =NULL ,
	@SuccessUrl varchar(800) =NULL ,
	@ConditionUrl varchar(800) =NULL ,
	@EmpId int =NULL ,
	@ApplyTime datetime =NULL ,
	@ApplyTitle varchar(400) =NULL ,
	@ApplyMemo varchar(4000) =NULL ,
	@ApplyInfo varchar(4000) =NULL ,
	@SourceId int OUTPUT
AS

INSERT INTO [dbo].[Wf_DataSource] (
	[BaseName],
	[TableCode],
	[DataStatus],
	[RowId],
	[DalName],
	[AssName],
	[ViewUrl],
	[RefusalUrl],
	[SuccessUrl],
	[ConditionUrl],
	[EmpId],
	[ApplyTime],
	[ApplyTitle],
	[ApplyMemo],
	[ApplyInfo]
) VALUES (
	@BaseName,
	@TableCode,
	@DataStatus,
	@RowId,
	@DalName,
	@AssName,
	@ViewUrl,
	@RefusalUrl,
	@SuccessUrl,
	@ConditionUrl,
	@EmpId,
	@ApplyTime,
	@ApplyTitle,
	@ApplyMemo,
	@ApplyInfo
)


SET @SourceId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_DataSourceUpdate
// 存储过程功能描述：更新Wf_DataSource
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_DataSourceUpdate
    @SourceId int,
@BaseName varchar(200) = NULL,
@TableCode varchar(50) = NULL,
@DataStatus int = NULL,
@RowId int = NULL,
@DalName varchar(80) = NULL,
@AssName varchar(50) = NULL,
@ViewUrl varchar(400) = NULL,
@RefusalUrl varchar(800) = NULL,
@SuccessUrl varchar(800) = NULL,
@ConditionUrl varchar(800) = NULL,
@EmpId int = NULL,
@ApplyTime datetime = NULL,
@ApplyTitle varchar(400) = NULL,
@ApplyMemo varchar(4000) = NULL,
@ApplyInfo varchar(4000) = NULL
AS

UPDATE [dbo].[Wf_DataSource] SET
	[BaseName] = @BaseName,
	[TableCode] = @TableCode,
	[DataStatus] = @DataStatus,
	[RowId] = @RowId,
	[DalName] = @DalName,
	[AssName] = @AssName,
	[ViewUrl] = @ViewUrl,
	[RefusalUrl] = @RefusalUrl,
	[SuccessUrl] = @SuccessUrl,
	[ConditionUrl] = @ConditionUrl,
	[EmpId] = @EmpId,
	[ApplyTime] = @ApplyTime,
	[ApplyTitle] = @ApplyTitle,
	[ApplyMemo] = @ApplyMemo,
	[ApplyInfo] = @ApplyInfo
WHERE
	[SourceId] = @SourceId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



