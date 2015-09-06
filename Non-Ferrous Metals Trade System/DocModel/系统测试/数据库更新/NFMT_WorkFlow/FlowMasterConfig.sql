
alter table Wf_FlowMasterConfig
   drop constraint PK_WF_FLOWMASTERCONFIG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_FlowMasterConfig')
            and   type = 'U')
   drop table Wf_FlowMasterConfig
go

/*==============================================================*/
/* Table: Wf_FlowMasterConfig                                   */
/*==============================================================*/
create table Wf_FlowMasterConfig (
   ConfigId             int                  identity,
   MasterId             int                  null,
   IsSeries             bit                  null,
   ConfigStatus         int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '流程模版配置表',
   'user', @CurrentUser, 'table', 'Wf_FlowMasterConfig'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Wf_FlowMasterConfig', 'column', 'ConfigId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模版序号',
   'user', @CurrentUser, 'table', 'Wf_FlowMasterConfig', 'column', 'MasterId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '同级节点全部审核通过 还是只要一个审核通过',
   'user', @CurrentUser, 'table', 'Wf_FlowMasterConfig', 'column', 'IsSeries'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'Wf_FlowMasterConfig', 'column', 'ConfigStatus'
go

alter table Wf_FlowMasterConfig
   add constraint PK_WF_FLOWMASTERCONFIG primary key (ConfigId)
go

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterConfigGet    Script Date: 2015年4月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterConfigGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterConfigGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterConfigLoad    Script Date: 2015年4月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterConfigLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterConfigLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterConfigInsert    Script Date: 2015年4月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterConfigInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterConfigInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterConfigUpdate    Script Date: 2015年4月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterConfigUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterConfigUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterConfigUpdateStatus    Script Date: 2015年4月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterConfigUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterConfigUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_FlowMasterConfigUpdateStatus    Script Date: 2015年4月23日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_FlowMasterConfigGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_FlowMasterConfigGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_FlowMasterConfigUpdateStatus
// 存储过程功能描述：更新Wf_FlowMasterConfig中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterConfigUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_FlowMasterConfig'

set @str = 'update [dbo].[Wf_FlowMasterConfig] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ConfigId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_FlowMasterConfigGoBack
// 存储过程功能描述：撤返Wf_FlowMasterConfig，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterConfigGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_FlowMasterConfig'

set @str = 'update [dbo].[Wf_FlowMasterConfig] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ConfigId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_FlowMasterConfigGet
// 存储过程功能描述：查询指定Wf_FlowMasterConfig的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterConfigGet
    /*
	@ConfigId int
    */
    @id int
AS

SELECT
	[ConfigId],
	[MasterId],
	[IsSeries],
	[ConfigStatus]
FROM
	[dbo].[Wf_FlowMasterConfig]
WHERE
	[ConfigId] = @id

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
// 存储过程名：[dbo].Wf_FlowMasterConfigLoad
// 存储过程功能描述：查询所有Wf_FlowMasterConfig记录
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterConfigLoad
AS

SELECT
	[ConfigId],
	[MasterId],
	[IsSeries],
	[ConfigStatus]
FROM
	[dbo].[Wf_FlowMasterConfig]

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
// 存储过程名：[dbo].Wf_FlowMasterConfigInsert
// 存储过程功能描述：新增一条Wf_FlowMasterConfig记录
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterConfigInsert
	@MasterId int =NULL ,
	@IsSeries bit =NULL ,
	@ConfigStatus int =NULL ,
	@ConfigId int OUTPUT
AS

INSERT INTO [dbo].[Wf_FlowMasterConfig] (
	[MasterId],
	[IsSeries],
	[ConfigStatus]
) VALUES (
	@MasterId,
	@IsSeries,
	@ConfigStatus
)


SET @ConfigId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_FlowMasterConfigUpdate
// 存储过程功能描述：更新Wf_FlowMasterConfig
// 创建人：CodeSmith
// 创建时间： 2015年4月23日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_FlowMasterConfigUpdate
    @ConfigId int,
@MasterId int = NULL,
@IsSeries bit = NULL,
@ConfigStatus int = NULL
AS

UPDATE [dbo].[Wf_FlowMasterConfig] SET
	[MasterId] = @MasterId,
	[IsSeries] = @IsSeries,
	[ConfigStatus] = @ConfigStatus
WHERE
	[ConfigId] = @ConfigId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



