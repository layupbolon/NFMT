alter table DeptEmp
   drop constraint PK_DEPTEMP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DeptEmp')
            and   type = 'U')
   drop table DeptEmp
go

/*==============================================================*/
/* Table: DeptEmp                                               */
/*==============================================================*/
create table DeptEmp (
   DeptEmpId            int                  identity,
   DeptId               int                  null,
   EmpId                int                  null,
   RefStatus            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '员工部门表',
   'user', @CurrentUser, 'table', 'DeptEmp'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关联序号',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'DeptEmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门序号',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'DeptId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '员工序号',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'EmpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关联状态',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'RefStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'DeptEmp', 'column', 'LastModifyTime'
go

alter table DeptEmp
   add constraint PK_DEPTEMP primary key (DeptEmpId)
go

/****** Object:  Stored Procedure [dbo].DeptEmpGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeptEmpGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeptEmpGet]
GO

/****** Object:  Stored Procedure [dbo].DeptEmpLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeptEmpLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeptEmpLoad]
GO

/****** Object:  Stored Procedure [dbo].DeptEmpInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeptEmpInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeptEmpInsert]
GO

/****** Object:  Stored Procedure [dbo].DeptEmpUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeptEmpUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeptEmpUpdate]
GO

/****** Object:  Stored Procedure [dbo].DeptEmpUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeptEmpUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeptEmpUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].DeptEmpUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeptEmpGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeptEmpGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DeptEmpUpdateStatus
// 存储过程功能描述：更新DeptEmp中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeptEmpUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.DeptEmp'

set @str = 'update [dbo].[DeptEmp] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DeptEmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].DeptEmpGoBack
// 存储过程功能描述：撤返DeptEmp，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeptEmpGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.DeptEmp'

set @str = 'update [dbo].[DeptEmp] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DeptEmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].DeptEmpGet
// 存储过程功能描述：查询指定DeptEmp的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeptEmpGet
    /*
	@DeptEmpId int
    */
    @id int
AS

SELECT
	[DeptEmpId],
	[DeptId],
	[EmpId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeptEmp]
WHERE
	[DeptEmpId] = @id

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
// 存储过程名：[dbo].DeptEmpLoad
// 存储过程功能描述：查询所有DeptEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeptEmpLoad
AS

SELECT
	[DeptEmpId],
	[DeptId],
	[EmpId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[DeptEmp]

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
// 存储过程名：[dbo].DeptEmpInsert
// 存储过程功能描述：新增一条DeptEmp记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeptEmpInsert
	@DeptId int ,
	@EmpId int ,
	@RefStatus int =NULL ,
	@CreatorId int =NULL ,
	@DeptEmpId int OUTPUT
AS

INSERT INTO [dbo].[DeptEmp] (
	[DeptId],
	[EmpId],
	[RefStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DeptId,
	@EmpId,
	@RefStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DeptEmpId = @@IDENTITY

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
// 存储过程名：[dbo].DeptEmpUpdate
// 存储过程功能描述：更新DeptEmp
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DeptEmpUpdate
    @DeptEmpId int,
@DeptId int,
@EmpId int,
@RefStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[DeptEmp] SET
	[DeptId] = @DeptId,
	[EmpId] = @EmpId,
	[RefStatus] = @RefStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DeptEmpId] = @DeptEmpId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



