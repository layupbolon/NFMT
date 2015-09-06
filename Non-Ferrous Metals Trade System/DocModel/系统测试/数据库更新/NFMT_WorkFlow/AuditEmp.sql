alter table Wf_AuditEmp
   drop constraint PK_WF_AUDITEMP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Wf_AuditEmp')
            and   type = 'U')
   drop table Wf_AuditEmp
go

/*==============================================================*/
/* Table: Wf_AuditEmp                                           */
/*==============================================================*/
create table Wf_AuditEmp (
   AuditEmpId           int                  identity,
   AuditEmpType         int                  null,
   ValueId              int                  null,
   AuditEmpStatus       int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '审核人表',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'AuditEmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核人类型',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'AuditEmpType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核类型内容',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'ValueId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核类型状态',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'AuditEmpStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Wf_AuditEmp', 'column', 'LastModifyTime'
go

alter table Wf_AuditEmp
   add constraint PK_WF_AUDITEMP primary key (AuditEmpId)
go


/****** Object:  Stored Procedure [dbo].Wf_AuditEmpGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_AuditEmpGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_AuditEmpGet]
GO

/****** Object:  Stored Procedure [dbo].Wf_AuditEmpLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_AuditEmpLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_AuditEmpLoad]
GO

/****** Object:  Stored Procedure [dbo].Wf_AuditEmpInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_AuditEmpInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_AuditEmpInsert]
GO

/****** Object:  Stored Procedure [dbo].Wf_AuditEmpUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_AuditEmpUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_AuditEmpUpdate]
GO

/****** Object:  Stored Procedure [dbo].Wf_AuditEmpUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_AuditEmpUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_AuditEmpUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Wf_AuditEmpUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Wf_AuditEmpGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Wf_AuditEmpGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Wf_AuditEmpUpdateStatus
// 存储过程功能描述：更新Wf_AuditEmp中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_AuditEmpUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_AuditEmp'

set @str = 'update [dbo].[Wf_AuditEmp] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AuditEmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_AuditEmpGoBack
// 存储过程功能描述：撤返Wf_AuditEmp，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_AuditEmpGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Wf_AuditEmp'

set @str = 'update [dbo].[Wf_AuditEmp] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where AuditEmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Wf_AuditEmpGet
// 存储过程功能描述：查询指定Wf_AuditEmp的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_AuditEmpGet
    /*
	@AuditEmpId int
    */
    @id int
AS

SELECT
	[AuditEmpId],
	[AuditEmpType],
	[ValueId],
	[AuditEmpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Wf_AuditEmp]
WHERE
	[AuditEmpId] = @id

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
// 存储过程名：[dbo].Wf_AuditEmpLoad
// 存储过程功能描述：查询所有Wf_AuditEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_AuditEmpLoad
AS

SELECT
	[AuditEmpId],
	[AuditEmpType],
	[ValueId],
	[AuditEmpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Wf_AuditEmp]

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
// 存储过程名：[dbo].Wf_AuditEmpInsert
// 存储过程功能描述：新增一条Wf_AuditEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_AuditEmpInsert
	@AuditEmpType int =NULL ,
	@ValueId int =NULL ,
	@AuditEmpStatus int =NULL ,
	@CreatorId int =NULL ,
	@AuditEmpId int OUTPUT
AS

INSERT INTO [dbo].[Wf_AuditEmp] (
	[AuditEmpType],
	[ValueId],
	[AuditEmpStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@AuditEmpType,
	@ValueId,
	@AuditEmpStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @AuditEmpId = @@IDENTITY

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
// 存储过程名：[dbo].Wf_AuditEmpUpdate
// 存储过程功能描述：更新Wf_AuditEmp
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Wf_AuditEmpUpdate
    @AuditEmpId int,
@AuditEmpType int = NULL,
@ValueId int = NULL,
@AuditEmpStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Wf_AuditEmp] SET
	[AuditEmpType] = @AuditEmpType,
	[ValueId] = @ValueId,
	[AuditEmpStatus] = @AuditEmpStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[AuditEmpId] = @AuditEmpId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



