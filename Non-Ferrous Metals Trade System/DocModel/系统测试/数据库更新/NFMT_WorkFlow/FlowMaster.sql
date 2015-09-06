alter table Wf_FlowMaster
   drop constraint PK_WF_FLOWMASTER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_FlowMaster')
            and   type = 'U')
   drop table Wf_FlowMaster
go

/*==============================================================*/
/* Table: Wf_FlowMaster                                         */
/*==============================================================*/
create table Wf_FlowMaster (
   MasterId             int                  identity,
   MasterName           varchar(200)         null,
   MasterStatus         int                  null,
   ViewUrl              varchar(200)         null,
   ConditionUrl         varchar(200)         null,
   SuccessUrl           varchar(200)         null,
   RefusalUrl           varchar(200)         null,
   ViewTitle            varchar(200)         null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '流程模板',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模版序号',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'MasterId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模版名称',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'MasterName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模版状态',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'MasterStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '显示页面地址',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'ViewUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '条件页面地址',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'ConditionUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通过页面地址',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'SuccessUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拒绝页面地址',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'RefusalUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '显示标题',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'ViewTitle'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Wf_FlowMaster', 'column', 'LastModifyTime'
go

alter table Wf_FlowMaster
   add constraint PK_WF_FLOWMASTER primary key (MasterId)
go


/****** Object:  Stored Procedure [dbo].Wf_FlowMasterGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterUpdateStatus
// 存储过程功能描述：更新Wf_FlowMaster中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_FlowMaster'

set @str = 'update [dbo].[Wf_FlowMaster] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MasterId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_FlowMasterGoBack
// 存储过程功能描述：撤返Wf_FlowMaster，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_FlowMaster'

set @str = 'update [dbo].[Wf_FlowMaster] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where MasterId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_FlowMasterGet
// 存储过程功能描述：查询指定Wf_FlowMaster的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterGet
    /*
	@MasterId int
    */
    @id int
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterStatus],
	[ViewUrl],
	[ConditionUrl],
	[SuccessUrl],
	[RefusalUrl],
	[ViewTitle],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Wf_FlowMaster]
WHERE
	[MasterId] = @id

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
// 存储过程名：[dbo].Wf_FlowMasterLoad
// 存储过程功能描述：查询所有Wf_FlowMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterLoad
AS

SELECT
	[MasterId],
	[MasterName],
	[MasterStatus],
	[ViewUrl],
	[ConditionUrl],
	[SuccessUrl],
	[RefusalUrl],
	[ViewTitle],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Wf_FlowMaster]

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
// 存储过程名：[dbo].Wf_FlowMasterInsert
// 存储过程功能描述：新增一条Wf_FlowMaster记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterInsert
	@MasterName varchar(200) ,
	@MasterStatus int ,
	@ViewUrl varchar(200) =NULL ,
	@ConditionUrl varchar(200) =NULL ,
	@SuccessUrl varchar(200) =NULL ,
	@RefusalUrl varchar(200) =NULL ,
	@ViewTitle varchar(200) =NULL ,
	@CreatorId int =NULL ,
	@MasterId int OUTPUT
AS

INSERT INTO [dbo].[Wf_FlowMaster] (
	[MasterName],
	[MasterStatus],
	[ViewUrl],
	[ConditionUrl],
	[SuccessUrl],
	[RefusalUrl],
	[ViewTitle],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@MasterName,
	@MasterStatus,
	@ViewUrl,
	@ConditionUrl,
	@SuccessUrl,
	@RefusalUrl,
	@ViewTitle,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @MasterId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_FlowMasterUpdate
// 存储过程功能描述：更新Wf_FlowMaster
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterUpdate
    @MasterId int,
@MasterName varchar(200),
@MasterStatus int,
@ViewUrl varchar(200) = NULL,
@ConditionUrl varchar(200) = NULL,
@SuccessUrl varchar(200) = NULL,
@RefusalUrl varchar(200) = NULL,
@ViewTitle varchar(200) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Wf_FlowMaster] SET
	[MasterName] = @MasterName,
	[MasterStatus] = @MasterStatus,
	[ViewUrl] = @ViewUrl,
	[ConditionUrl] = @ConditionUrl,
	[SuccessUrl] = @SuccessUrl,
	[RefusalUrl] = @RefusalUrl,
	[ViewTitle] = @ViewTitle,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[MasterId] = @MasterId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



