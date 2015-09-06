alter table Wf_NodeCondition
   drop constraint PK_WF_NODECONDITION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_NodeCondition')
            and   type = 'U')
   drop table Wf_NodeCondition
go

/*==============================================================*/
/* Table: Wf_NodeCondition                                      */
/*==============================================================*/
create table Wf_NodeCondition (
   ConditionId          int                  identity,
   ConditionStatus      int                  null,
   NodeId               int                  null,
   FieldName            varchar(50)          null,
   FieldValue           varchar(50)          null,
   ConditionType        int                  null,
   LogicType            int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '节点条件表',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '条件序号',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition', 'column', 'ConditionId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '条件状态',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition', 'column', 'ConditionStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '节点序号',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition', 'column', 'NodeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '条件字段名',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition', 'column', 'FieldName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '条件值',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition', 'column', 'FieldValue'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '条件类型',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition', 'column', 'ConditionType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '逻辑类型',
   'user', @CurrentUser, 'table', 'Wf_NodeCondition', 'column', 'LogicType'
go

alter table Wf_NodeCondition
   add constraint PK_WF_NODECONDITION primary key (ConditionId)
go


/****** Object:  Stored Procedure [dbo].Wf_NodeConditionGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeConditionGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeConditionGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeConditionLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeConditionLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeConditionLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeConditionInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeConditionInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeConditionInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeConditionUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeConditionUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeConditionUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeConditionUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeConditionUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeConditionUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_NodeConditionUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_NodeConditionGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_NodeConditionGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_NodeConditionUpdateStatus
// 存储过程功能描述：更新Wf_NodeCondition中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeConditionUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_NodeCondition'

set @str = 'update [dbo].[Wf_NodeCondition] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ConditionId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_NodeConditionGoBack
// 存储过程功能描述：撤返Wf_NodeCondition，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeConditionGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_NodeCondition'

set @str = 'update [dbo].[Wf_NodeCondition] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ConditionId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_NodeConditionGet
// 存储过程功能描述：查询指定Wf_NodeCondition的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeConditionGet
    /*
	@ConditionId int
    */
    @id int
AS

SELECT
	[ConditionId],
	[ConditionStatus],
	[NodeId],
	[FieldName],
	[FieldValue],
	[ConditionType],
	[LogicType]
FROM
	[dbo].[Wf_NodeCondition]
WHERE
	[ConditionId] = @id

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
// 存储过程名：[dbo].Wf_NodeConditionLoad
// 存储过程功能描述：查询所有Wf_NodeCondition记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeConditionLoad
AS

SELECT
	[ConditionId],
	[ConditionStatus],
	[NodeId],
	[FieldName],
	[FieldValue],
	[ConditionType],
	[LogicType]
FROM
	[dbo].[Wf_NodeCondition]

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
// 存储过程名：[dbo].Wf_NodeConditionInsert
// 存储过程功能描述：新增一条Wf_NodeCondition记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeConditionInsert
	@ConditionStatus int ,
	@NodeId int ,
	@FieldName varchar(50) =NULL ,
	@FieldValue varchar(50) =NULL ,
	@ConditionType int =NULL ,
	@LogicType int =NULL ,
	@ConditionId int OUTPUT
AS

INSERT INTO [dbo].[Wf_NodeCondition] (
	[ConditionStatus],
	[NodeId],
	[FieldName],
	[FieldValue],
	[ConditionType],
	[LogicType]
) VALUES (
	@ConditionStatus,
	@NodeId,
	@FieldName,
	@FieldValue,
	@ConditionType,
	@LogicType
)


SET @ConditionId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_NodeConditionUpdate
// 存储过程功能描述：更新Wf_NodeCondition
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_NodeConditionUpdate
    @ConditionId int,
@ConditionStatus int,
@NodeId int,
@FieldName varchar(50) = NULL,
@FieldValue varchar(50) = NULL,
@ConditionType int = NULL,
@LogicType int = NULL
AS

UPDATE [dbo].[Wf_NodeCondition] SET
	[ConditionStatus] = @ConditionStatus,
	[NodeId] = @NodeId,
	[FieldName] = @FieldName,
	[FieldValue] = @FieldValue,
	[ConditionType] = @ConditionType,
	[LogicType] = @LogicType
WHERE
	[ConditionId] = @ConditionId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



