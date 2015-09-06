alter table MeasureUnit
   drop constraint PK_MEASUREUNIT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MeasureUnit')
            and   type = 'U')
   drop table MeasureUnit
go

/*==============================================================*/
/* Table: MeasureUnit                                           */
/*==============================================================*/
create table MeasureUnit (
   MUId                 int                  identity,
   MUName               varchar(20)          null,
   BaseId               int                  null,
   TransformRate        decimal(8,2)         null,
   MUStatus             int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', @CurrentUser, 'table', 'MeasureUnit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计量单位序号',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'MUId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计量单位名称',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'MUName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '基本单位序号',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'BaseId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '基本单位转换率',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'TransformRate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单位状态',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'MUStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'MeasureUnit', 'column', 'LastModifyTime'
go

alter table MeasureUnit
   add constraint PK_MEASUREUNIT primary key (MUId)
go


/****** Object:  Stored Procedure [dbo].MeasureUnitGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MeasureUnitGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MeasureUnitGet]
GO

/****** Object:  Stored Procedure [dbo].MeasureUnitLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MeasureUnitLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MeasureUnitLoad]
GO

/****** Object:  Stored Procedure [dbo].MeasureUnitInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MeasureUnitInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MeasureUnitInsert]
GO

/****** Object:  Stored Procedure [dbo].MeasureUnitUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MeasureUnitUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MeasureUnitUpdate]
GO

/****** Object:  Stored Procedure [dbo].MeasureUnitUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MeasureUnitUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MeasureUnitUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].MeasureUnitUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MeasureUnitGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[MeasureUnitGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].MeasureUnitUpdateStatus
// 存储过程功能描述：更新MeasureUnit中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MeasureUnitUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.MeasureUnit'

set @str = 'update [dbo].[MeasureUnit] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MUId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].MeasureUnitGoBack
// 存储过程功能描述：撤返MeasureUnit，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MeasureUnitGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.MeasureUnit'

set @str = 'update [dbo].[MeasureUnit] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MUId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].MeasureUnitGet
// 存储过程功能描述：查询指定MeasureUnit的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MeasureUnitGet
    /*
	@MUId int
    */
    @id int
AS

SELECT
	[MUId],
	[MUName],
	[BaseId],
	[TransformRate],
	[MUStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[MeasureUnit]
WHERE
	[MUId] = @id

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
// 存储过程名：[dbo].MeasureUnitLoad
// 存储过程功能描述：查询所有MeasureUnit记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MeasureUnitLoad
AS

SELECT
	[MUId],
	[MUName],
	[BaseId],
	[TransformRate],
	[MUStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[MeasureUnit]

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
// 存储过程名：[dbo].MeasureUnitInsert
// 存储过程功能描述：新增一条MeasureUnit记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MeasureUnitInsert
	@MUName varchar(20) ,
	@BaseId int =NULL ,
	@TransformRate decimal(8, 2) ,
	@MUStatus int ,
	@CreatorId int ,
	@MUId int OUTPUT
AS

INSERT INTO [dbo].[MeasureUnit] (
	[MUName],
	[BaseId],
	[TransformRate],
	[MUStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MUName,
	@BaseId,
	@TransformRate,
	@MUStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MUId = @@IDENTITY

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
// 存储过程名：[dbo].MeasureUnitUpdate
// 存储过程功能描述：更新MeasureUnit
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].MeasureUnitUpdate
    @MUId int,
@MUName varchar(20),
@BaseId int = NULL,
@TransformRate decimal(8, 2),
@MUStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[MeasureUnit] SET
	[MUName] = @MUName,
	[BaseId] = @BaseId,
	[TransformRate] = @TransformRate,
	[MUStatus] = @MUStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MUId] = @MUId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



