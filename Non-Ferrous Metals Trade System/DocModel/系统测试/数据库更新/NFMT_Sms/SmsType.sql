alter table dbo.Sm_SmsType
   drop constraint PK_SM_SMSTYPE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Sm_SmsType')
            and   type = 'U')
   drop table dbo.Sm_SmsType
go

/*==============================================================*/
/* Table: Sm_SmsType                                            */
/*==============================================================*/
create table dbo.Sm_SmsType (
   SmsTypeId            int                  identity,
   TypeName             varchar(200)         null,
   ListUrl              varchar(200)         null,
   ViewUrl              varchar(200)         null,
   SmsTypeStatus        int                  null,
   SourceBaseName       varchar(50)          null,
   SourceTableName      varchar(50)          null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '消息类型',
   'user', 'dbo', 'table', 'Sm_SmsType'
go

execute sp_addextendedproperty 'MS_Description', 
   '消息类型序号',
   'user', 'dbo', 'table', 'Sm_SmsType', 'column', 'SmsTypeId'
go

execute sp_addextendedproperty 'MS_Description', 
   '类型名称',
   'user', 'dbo', 'table', 'Sm_SmsType', 'column', 'TypeName'
go

execute sp_addextendedproperty 'MS_Description', 
   '程序集名称',
   'user', 'dbo', 'table', 'Sm_SmsType', 'column', 'ListUrl'
go

execute sp_addextendedproperty 'MS_Description', 
   '类名',
   'user', 'dbo', 'table', 'Sm_SmsType', 'column', 'ViewUrl'
go

execute sp_addextendedproperty 'MS_Description', 
   '类型状态',
   'user', 'dbo', 'table', 'Sm_SmsType', 'column', 'SmsTypeStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '数据库名',
   'user', 'dbo', 'table', 'Sm_SmsType', 'column', 'SourceBaseName'
go

execute sp_addextendedproperty 'MS_Description', 
   '表名',
   'user', 'dbo', 'table', 'Sm_SmsType', 'column', 'SourceTableName'
go

alter table dbo.Sm_SmsType
   add constraint PK_SM_SMSTYPE primary key (SmsTypeId)
go


/****** Object:  Stored Procedure [dbo].Sm_SmsTypeGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsTypeGet]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsTypeLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsTypeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsTypeLoad]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsTypeInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsTypeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsTypeInsert]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsTypeUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsTypeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsTypeUpdate]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsTypeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsTypeUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsTypeUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Sm_SmsTypeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Sm_SmsTypeGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Sm_SmsTypeGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Sm_SmsTypeUpdateStatus
// 存储过程功能描述：更新Sm_SmsType中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsTypeUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Sm_SmsType'

set @str = 'update [dbo].[Sm_SmsType] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SmsTypeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Sm_SmsTypeGoBack
// 存储过程功能描述：撤返Sm_SmsType，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsTypeGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Sm_SmsType'

set @str = 'update [dbo].[Sm_SmsType] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where SmsTypeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Sm_SmsTypeGet
// 存储过程功能描述：查询指定Sm_SmsType的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsTypeGet
    /*
	@SmsTypeId int
    */
    @id int
AS

SELECT
	[SmsTypeId],
	[TypeName],
	[ListUrl],
	[ViewUrl],
	[SmsTypeStatus],
	[SourceBaseName],
	[SourceTableName]
FROM
	[dbo].[Sm_SmsType]
WHERE
	[SmsTypeId] = @id

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
// 存储过程名：[dbo].Sm_SmsTypeLoad
// 存储过程功能描述：查询所有Sm_SmsType记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsTypeLoad
AS

SELECT
	[SmsTypeId],
	[TypeName],
	[ListUrl],
	[ViewUrl],
	[SmsTypeStatus],
	[SourceBaseName],
	[SourceTableName]
FROM
	[dbo].[Sm_SmsType]

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
// 存储过程名：[dbo].Sm_SmsTypeInsert
// 存储过程功能描述：新增一条Sm_SmsType记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsTypeInsert
	@TypeName varchar(200) =NULL ,
	@ListUrl varchar(200) =NULL ,
	@ViewUrl varchar(200) =NULL ,
	@SmsTypeStatus int =NULL ,
	@SourceBaseName varchar(50) =NULL ,
	@SourceTableName varchar(50) =NULL ,
	@SmsTypeId int OUTPUT
AS

INSERT INTO [dbo].[Sm_SmsType] (
	[TypeName],
	[ListUrl],
	[ViewUrl],
	[SmsTypeStatus],
	[SourceBaseName],
	[SourceTableName]
) VALUES (
	@TypeName,
	@ListUrl,
	@ViewUrl,
	@SmsTypeStatus,
	@SourceBaseName,
	@SourceTableName
)


SET @SmsTypeId = @@IDENTITY

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
// 存储过程名：[dbo].Sm_SmsTypeUpdate
// 存储过程功能描述：更新Sm_SmsType
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Sm_SmsTypeUpdate
    @SmsTypeId int,
@TypeName varchar(200) = NULL,
@ListUrl varchar(200) = NULL,
@ViewUrl varchar(200) = NULL,
@SmsTypeStatus int = NULL,
@SourceBaseName varchar(50) = NULL,
@SourceTableName varchar(50) = NULL
AS

UPDATE [dbo].[Sm_SmsType] SET
	[TypeName] = @TypeName,
	[ListUrl] = @ListUrl,
	[ViewUrl] = @ViewUrl,
	[SmsTypeStatus] = @SmsTypeStatus,
	[SourceBaseName] = @SourceBaseName,
	[SourceTableName] = @SourceTableName
WHERE
	[SmsTypeId] = @SmsTypeId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



