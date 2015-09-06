alter table AuthGroup
   drop constraint PK_AUTHGROUP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AuthGroup')
            and   type = 'U')
   drop table AuthGroup
go

/*==============================================================*/
/* Table: AuthGroup                                             */
/*==============================================================*/
create table AuthGroup (
   AuthGroupId          int                  identity,
   AuthGroupName        varchar(800)         null,
   AssetId              int                  null,
   TradeDirection       int                  null,
   TradeBorder          int                  null,
   ContractInOut        int                  null,
   ContractLimit        int                  null,
   CorpId               int                  null,
   AuthGroupStatus      int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '权限组',
   'user', @CurrentUser, 'table', 'AuthGroup'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限组序号',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'AuthGroupId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限组名称',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'AuthGroupName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'AssetId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '贸易方向（进口，出口）',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'TradeDirection'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '贸易方向（外贸，内贸）',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'TradeBorder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '内外部合约',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'ContractInOut'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约长零单',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'ContractLimit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'CorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限组状态',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'AuthGroupStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'AuthGroup', 'column', 'LastModifyTime'
go

alter table AuthGroup
   add constraint PK_AUTHGROUP primary key (AuthGroupId)
go


/****** Object:  Stored Procedure [dbo].AuthGroupGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupGet]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupLoad]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupInsert]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupUpdate]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].AuthGroupUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthGroupGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthGroupGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthGroupUpdateStatus
// 存储过程功能描述：更新AuthGroup中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.AuthGroup'

set @str = 'update [dbo].[AuthGroup] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AuthGroupId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AuthGroupGoBack
// 存储过程功能描述：撤返AuthGroup，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.AuthGroup'

set @str = 'update [dbo].[AuthGroup] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AuthGroupId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AuthGroupGet
// 存储过程功能描述：查询指定AuthGroup的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupGet
    /*
	@AuthGroupId int
    */
    @id int
AS

SELECT
	[AuthGroupId],
	[AuthGroupName],
	[AssetId],
	[TradeDirection],
	[TradeBorder],
	[ContractInOut],
	[ContractLimit],
	[CorpId],
	[AuthGroupStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroup]
WHERE
	[AuthGroupId] = @id

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
// 存储过程名：[dbo].AuthGroupLoad
// 存储过程功能描述：查询所有AuthGroup记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupLoad
AS

SELECT
	[AuthGroupId],
	[AuthGroupName],
	[AssetId],
	[TradeDirection],
	[TradeBorder],
	[ContractInOut],
	[ContractLimit],
	[CorpId],
	[AuthGroupStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthGroup]

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
// 存储过程名：[dbo].AuthGroupInsert
// 存储过程功能描述：新增一条AuthGroup记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupInsert
	@AuthGroupName varchar(800) =NULL ,
	@AssetId int =NULL ,
	@TradeDirection int =NULL ,
	@TradeBorder int =NULL ,
	@ContractInOut int =NULL ,
	@ContractLimit int =NULL ,
	@CorpId int =NULL ,
	@AuthGroupStatus int =NULL ,
	@CreatorId int =NULL ,
	@AuthGroupId int OUTPUT
AS

INSERT INTO [dbo].[AuthGroup] (
	[AuthGroupName],
	[AssetId],
	[TradeDirection],
	[TradeBorder],
	[ContractInOut],
	[ContractLimit],
	[CorpId],
	[AuthGroupStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AuthGroupName,
	@AssetId,
	@TradeDirection,
	@TradeBorder,
	@ContractInOut,
	@ContractLimit,
	@CorpId,
	@AuthGroupStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AuthGroupId = @@IDENTITY

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
// 存储过程名：[dbo].AuthGroupUpdate
// 存储过程功能描述：更新AuthGroup
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthGroupUpdate
    @AuthGroupId int,
@AuthGroupName varchar(800) = NULL,
@AssetId int = NULL,
@TradeDirection int = NULL,
@TradeBorder int = NULL,
@ContractInOut int = NULL,
@ContractLimit int = NULL,
@CorpId int = NULL,
@AuthGroupStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthGroup] SET
	[AuthGroupName] = @AuthGroupName,
	[AssetId] = @AssetId,
	[TradeDirection] = @TradeDirection,
	[TradeBorder] = @TradeBorder,
	[ContractInOut] = @ContractInOut,
	[ContractLimit] = @ContractLimit,
	[CorpId] = @CorpId,
	[AuthGroupStatus] = @AuthGroupStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AuthGroupId] = @AuthGroupId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



