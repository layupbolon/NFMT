alter table dbo.Area
   drop constraint PK_AREA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Area')
            and   type = 'U')
   drop table dbo.Area
go

/*==============================================================*/
/* Table: Area                                                  */
/*==============================================================*/
create table dbo.Area (
   AreaId               int                  identity,
   AreaName             varchar(50)          null,
   AreaFullName         varchar(100)         null,
   AreaShort            varchar(10)          null,
   AreaCode             varchar(20)          null,
   AreaZip              varchar(20)          null,
   AreaLevel            int                  null,
   ParentID             int                  null,
   AreaStatus           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '地区表',
   'user', 'dbo', 'table', 'Area'
go

execute sp_addextendedproperty 'MS_Description', 
   '地区序号',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaId'
go

execute sp_addextendedproperty 'MS_Description', 
   '地区名称',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaName'
go

execute sp_addextendedproperty 'MS_Description', 
   '地区全称',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaFullName'
go

execute sp_addextendedproperty 'MS_Description', 
   '地区缩写',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaShort'
go

execute sp_addextendedproperty 'MS_Description', 
   '电话区号',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '邮政编号',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaZip'
go

execute sp_addextendedproperty 'MS_Description', 
   '地区级别',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaLevel'
go

execute sp_addextendedproperty 'MS_Description', 
   '上级地区序号',
   'user', 'dbo', 'table', 'Area', 'column', 'ParentID'
go

execute sp_addextendedproperty 'MS_Description', 
   '地区状态',
   'user', 'dbo', 'table', 'Area', 'column', 'AreaStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Area', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Area', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Area', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Area', 'column', 'LastModifyTime'
go

alter table dbo.Area
   add constraint PK_AREA primary key (AreaId)
go


/****** Object:  Stored Procedure [dbo].AreaGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AreaGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AreaGet]
GO

/****** Object:  Stored Procedure [dbo].AreaLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AreaLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AreaLoad]
GO

/****** Object:  Stored Procedure [dbo].AreaInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AreaInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AreaInsert]
GO

/****** Object:  Stored Procedure [dbo].AreaUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AreaUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AreaUpdate]
GO

/****** Object:  Stored Procedure [dbo].AreaUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AreaUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AreaUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].AreaUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AreaGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AreaGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AreaUpdateStatus
// 存储过程功能描述：更新Area中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AreaUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Area'

set @str = 'update [dbo].[Area] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AreaId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AreaGoBack
// 存储过程功能描述：撤返Area，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AreaGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Area'

set @str = 'update [dbo].[Area] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AreaId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AreaGet
// 存储过程功能描述：查询指定Area的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AreaGet
    /*
	@AreaId int
    */
    @id int
AS

SELECT
	[AreaId],
	[AreaName],
	[AreaFullName],
	[AreaShort],
	[AreaCode],
	[AreaZip],
	[AreaLevel],
	[ParentID],
	[AreaStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Area]
WHERE
	[AreaId] = @id

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
// 存储过程名：[dbo].AreaLoad
// 存储过程功能描述：查询所有Area记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AreaLoad
AS

SELECT
	[AreaId],
	[AreaName],
	[AreaFullName],
	[AreaShort],
	[AreaCode],
	[AreaZip],
	[AreaLevel],
	[ParentID],
	[AreaStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Area]

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
// 存储过程名：[dbo].AreaInsert
// 存储过程功能描述：新增一条Area记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AreaInsert
	@AreaName varchar(50) ,
	@AreaFullName varchar(100) ,
	@AreaShort varchar(80) ,
	@AreaCode varchar(20) =NULL ,
	@AreaZip varchar(20) =NULL ,
	@AreaLevel int =NULL ,
	@ParentID int =NULL ,
	@AreaStatus int ,
	@CreatorId int ,
	@AreaId int OUTPUT
AS

INSERT INTO [dbo].[Area] (
	[AreaName],
	[AreaFullName],
	[AreaShort],
	[AreaCode],
	[AreaZip],
	[AreaLevel],
	[ParentID],
	[AreaStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AreaName,
	@AreaFullName,
	@AreaShort,
	@AreaCode,
	@AreaZip,
	@AreaLevel,
	@ParentID,
	@AreaStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AreaId = @@IDENTITY

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
// 存储过程名：[dbo].AreaUpdate
// 存储过程功能描述：更新Area
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AreaUpdate
    @AreaId int,
@AreaName varchar(50),
@AreaFullName varchar(100),
@AreaShort varchar(80),
@AreaCode varchar(20) = NULL,
@AreaZip varchar(20) = NULL,
@AreaLevel int = NULL,
@ParentID int = NULL,
@AreaStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Area] SET
	[AreaName] = @AreaName,
	[AreaFullName] = @AreaFullName,
	[AreaShort] = @AreaShort,
	[AreaCode] = @AreaCode,
	[AreaZip] = @AreaZip,
	[AreaLevel] = @AreaLevel,
	[ParentID] = @ParentID,
	[AreaStatus] = @AreaStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AreaId] = @AreaId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



