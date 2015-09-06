alter table dbo.Asset
   drop constraint PK_ASSET
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Asset')
            and   type = 'U')
   drop table dbo.Asset
go

/*==============================================================*/
/* Table: Asset                                                 */
/*==============================================================*/
create table dbo.Asset (
   AssetId              int                  identity,
   AssetName            varchar(20)          null,
   MUId                 int                  null,
   MisTake              numeric(18,8)        null,
   AssetStatus          int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '品种表',
   'user', 'dbo', 'table', 'Asset'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种序号',
   'user', 'dbo', 'table', 'Asset', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种名称',
   'user', 'dbo', 'table', 'Asset', 'column', 'AssetName'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种主要计量单位序号',
   'user', 'dbo', 'table', 'Asset', 'column', 'MUId'
go

execute sp_addextendedproperty 'MS_Description', 
   '溢短装',
   'user', 'dbo', 'table', 'Asset', 'column', 'MisTake'
go

execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', 'dbo', 'table', 'Asset', 'column', 'AssetStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Asset', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Asset', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Asset', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Asset', 'column', 'LastModifyTime'
go

alter table dbo.Asset
   add constraint PK_ASSET primary key (AssetId)
go


/****** Object:  Stored Procedure [dbo].AssetGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AssetGet]
GO

/****** Object:  Stored Procedure [dbo].AssetLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AssetLoad]
GO

/****** Object:  Stored Procedure [dbo].AssetInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AssetInsert]
GO

/****** Object:  Stored Procedure [dbo].AssetUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AssetUpdate]
GO

/****** Object:  Stored Procedure [dbo].AssetUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AssetUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].AssetUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AssetGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AssetGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AssetUpdateStatus
// 存储过程功能描述：更新Asset中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AssetUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Asset'

set @str = 'update [dbo].[Asset] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AssetId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AssetGoBack
// 存储过程功能描述：撤返Asset，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AssetGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Asset'

set @str = 'update [dbo].[Asset] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AssetId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AssetGet
// 存储过程功能描述：查询指定Asset的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AssetGet
    /*
	@AssetId int
    */
    @id int
AS

SELECT
	[AssetId],
	[AssetName],
	[MUId],
	[MisTake],
	[AssetStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Asset]
WHERE
	[AssetId] = @id

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
// 存储过程名：[dbo].AssetLoad
// 存储过程功能描述：查询所有Asset记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AssetLoad
AS

SELECT
	[AssetId],
	[AssetName],
	[MUId],
	[MisTake],
	[AssetStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Asset]

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
// 存储过程名：[dbo].AssetInsert
// 存储过程功能描述：新增一条Asset记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AssetInsert
	@AssetName varchar(20) ,
	@MUId int ,
	@MisTake numeric(18, 8) =NULL ,
	@AssetStatus int ,
	@CreatorId int ,
	@AssetId int OUTPUT
AS

INSERT INTO [dbo].[Asset] (
	[AssetName],
	[MUId],
	[MisTake],
	[AssetStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AssetName,
	@MUId,
	@MisTake,
	@AssetStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AssetId = @@IDENTITY

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
// 存储过程名：[dbo].AssetUpdate
// 存储过程功能描述：更新Asset
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AssetUpdate
    @AssetId int,
@AssetName varchar(20),
@MUId int,
@MisTake numeric(18, 8) = NULL,
@AssetStatus int,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Asset] SET
	[AssetName] = @AssetName,
	[MUId] = @MUId,
	[MisTake] = @MisTake,
	[AssetStatus] = @AssetStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AssetId] = @AssetId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



