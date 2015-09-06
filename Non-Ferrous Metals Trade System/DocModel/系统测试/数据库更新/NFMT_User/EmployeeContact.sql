alter table EmployeeContact
   drop constraint PK_EMPLOYEECONTACT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmployeeContact')
            and   type = 'U')
   drop table EmployeeContact
go

/*==============================================================*/
/* Table: EmployeeContact                                       */
/*==============================================================*/
create table EmployeeContact (
   ECId                 int                  identity,
   ContactId            int                  null,
   EmpId                int                  null,
   RefStatus            int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '联系人员工关系表',
   'user', @CurrentUser, 'table', 'EmployeeContact'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'EmployeeContact', 'column', 'ECId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '联系人序号',
   'user', @CurrentUser, 'table', 'EmployeeContact', 'column', 'ContactId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '员工序号',
   'user', @CurrentUser, 'table', 'EmployeeContact', 'column', 'EmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关联状态',
   'user', @CurrentUser, 'table', 'EmployeeContact', 'column', 'RefStatus'
go

alter table EmployeeContact
   add constraint PK_EMPLOYEECONTACT primary key (ECId)
go

/****** Object:  Stored Procedure [dbo].EmployeeContactGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeContactGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeContactGet]
GO

/****** Object:  Stored Procedure [dbo].EmployeeContactLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeContactLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeContactLoad]
GO

/****** Object:  Stored Procedure [dbo].EmployeeContactInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeContactInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeContactInsert]
GO

/****** Object:  Stored Procedure [dbo].EmployeeContactUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeContactUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeContactUpdate]
GO

/****** Object:  Stored Procedure [dbo].EmployeeContactUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeContactUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeContactUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].EmployeeContactUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeContactGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeContactGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeContactUpdateStatus
// 存储过程功能描述：更新EmployeeContact中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeContactUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.EmployeeContact'

set @str = 'update [dbo].[EmployeeContact] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ECId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].EmployeeContactGoBack
// 存储过程功能描述：撤返EmployeeContact，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeContactGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.EmployeeContact'

set @str = 'update [dbo].[EmployeeContact] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where ECId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].EmployeeContactGet
// 存储过程功能描述：查询指定EmployeeContact的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeContactGet
    /*
	@ECId int
    */
    @id int
AS

SELECT
	[ECId],
	[ContactId],
	[EmpId],
	[RefStatus]
FROM
	[dbo].[EmployeeContact]
WHERE
	[ECId] = @id

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
// 存储过程名：[dbo].EmployeeContactLoad
// 存储过程功能描述：查询所有EmployeeContact记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeContactLoad
AS

SELECT
	[ECId],
	[ContactId],
	[EmpId],
	[RefStatus]
FROM
	[dbo].[EmployeeContact]

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
// 存储过程名：[dbo].EmployeeContactInsert
// 存储过程功能描述：新增一条EmployeeContact记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeContactInsert
	@ContactId int ,
	@EmpId int ,
	@RefStatus int =NULL ,
	@ECId int OUTPUT
AS

INSERT INTO [dbo].[EmployeeContact] (
	[ContactId],
	[EmpId],
	[RefStatus]
) VALUES (
	@ContactId,
	@EmpId,
	@RefStatus
)


SET @ECId = @@IDENTITY

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
// 存储过程名：[dbo].EmployeeContactUpdate
// 存储过程功能描述：更新EmployeeContact
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeContactUpdate
    @ECId int,
@ContactId int,
@EmpId int,
@RefStatus int = NULL
AS

UPDATE [dbo].[EmployeeContact] SET
	[ContactId] = @ContactId,
	[EmpId] = @EmpId,
	[RefStatus] = @RefStatus
WHERE
	[ECId] = @ECId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



