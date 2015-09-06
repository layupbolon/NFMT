alter table AuthOperate
   drop constraint PK_AUTHOPERATE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AuthOperate')
            and   type = 'U')
   drop table AuthOperate
go

/*==============================================================*/
/* Table: AuthOperate                                           */
/*==============================================================*/
create table AuthOperate (
   AuthOperateId        int                  identity,
   OperateCode          varchar(50)          null,
   OperateName          varchar(50)          null,
   OperateType          int                  null,
   MenuId               int                  null,
   EmpId                int                  null,
   AuthOperateStatus	int					 null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '操作权限表',
   'user', @CurrentUser, 'table', 'AuthOperate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作权限序号',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'AuthOperateId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作权限编号',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'OperateCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作权限名称',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'OperateName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作类型',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'OperateType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单序号',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'MenuId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'AuthOperate', 'column', 'LastModifyTime'
go

alter table AuthOperate
   add constraint PK_AUTHOPERATE primary key (AuthOperateId)
go

/****** Object:  Stored Procedure [dbo].AuthOperateGet    Script Date: 2015年1月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthOperateGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthOperateGet]
GO

/****** Object:  Stored Procedure [dbo].AuthOperateLoad    Script Date: 2015年1月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthOperateLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthOperateLoad]
GO

/****** Object:  Stored Procedure [dbo].AuthOperateInsert    Script Date: 2015年1月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthOperateInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthOperateInsert]
GO

/****** Object:  Stored Procedure [dbo].AuthOperateUpdate    Script Date: 2015年1月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthOperateUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthOperateUpdate]
GO

/****** Object:  Stored Procedure [dbo].AuthOperateUpdateStatus    Script Date: 2015年1月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthOperateUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthOperateUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].AuthOperateUpdateStatus    Script Date: 2015年1月16日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AuthOperateGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AuthOperateGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].AuthOperateUpdateStatus
// 存储过程功能描述：更新AuthOperate中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthOperateUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.AuthOperate'

set @str = 'update [dbo].[AuthOperate] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AuthOperateId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AuthOperateGoBack
// 存储过程功能描述：撤返AuthOperate，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthOperateGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.AuthOperate'

set @str = 'update [dbo].[AuthOperate] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AuthOperateId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].AuthOperateGet
// 存储过程功能描述：查询指定AuthOperate的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthOperateGet
    /*
	@AuthOperateId int
    */
    @id int
AS

SELECT
	[AuthOperateId],
	[OperateCode],
	[OperateName],
	[OperateType],
	[MenuId],
	[EmpId],
	[AuthOperateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOperate]
WHERE
	[AuthOperateId] = @id

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
// 存储过程名：[dbo].AuthOperateLoad
// 存储过程功能描述：查询所有AuthOperate记录
// 创建人：CodeSmith
// 创建时间： 2015年1月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthOperateLoad
AS

SELECT
	[AuthOperateId],
	[OperateCode],
	[OperateName],
	[OperateType],
	[MenuId],
	[EmpId],
	[AuthOperateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[AuthOperate]

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
// 存储过程名：[dbo].AuthOperateInsert
// 存储过程功能描述：新增一条AuthOperate记录
// 创建人：CodeSmith
// 创建时间： 2015年1月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthOperateInsert
	@OperateCode varchar(50) =NULL ,
	@OperateName varchar(50) =NULL ,
	@OperateType int =NULL ,
	@MenuId int =NULL ,
	@EmpId int =NULL ,
	@AuthOperateStatus int =NULL ,
	@CreatorId int =NULL ,
	@AuthOperateId int OUTPUT
AS

INSERT INTO [dbo].[AuthOperate] (
	[OperateCode],
	[OperateName],
	[OperateType],
	[MenuId],
	[EmpId],
	[AuthOperateStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OperateCode,
	@OperateName,
	@OperateType,
	@MenuId,
	@EmpId,
	@AuthOperateStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AuthOperateId = @@IDENTITY

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
// 存储过程名：[dbo].AuthOperateUpdate
// 存储过程功能描述：更新AuthOperate
// 创建人：CodeSmith
// 创建时间： 2015年1月16日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].AuthOperateUpdate
    @AuthOperateId int,
@OperateCode varchar(50) = NULL,
@OperateName varchar(50) = NULL,
@OperateType int = NULL,
@MenuId int = NULL,
@EmpId int = NULL,
@AuthOperateStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[AuthOperate] SET
	[OperateCode] = @OperateCode,
	[OperateName] = @OperateName,
	[OperateType] = @OperateType,
	[MenuId] = @MenuId,
	[EmpId] = @EmpId,
	[AuthOperateStatus] = @AuthOperateStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AuthOperateId] = @AuthOperateId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



