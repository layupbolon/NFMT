alter table dbo.Employee
   drop constraint PK_EMPLOYEE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Employee')
            and   type = 'U')
   drop table dbo.Employee
go

/*==============================================================*/
/* Table: Employee                                              */
/*==============================================================*/
create table dbo.Employee (
   EmpId                int                  identity,
   DeptId               int                  null,
   EmpCode              varchar(20)          null,
   Name                 varchar(20)          null,
   Sex                  bit                  null,
   BirthDay             datetime             null,
   Telephone            varchar(20)          null,
   Phone                varchar(20)          null,
   WorkStatus           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '员工表',
   'user', 'dbo', 'table', 'Employee'
go

execute sp_addextendedproperty 'MS_Description', 
   '员工编号',
   'user', 'dbo', 'table', 'Employee', 'column', 'EmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属部门',
   'user', 'dbo', 'table', 'Employee', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '员工编号',
   'user', 'dbo', 'table', 'Employee', 'column', 'EmpCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '姓名',
   'user', 'dbo', 'table', 'Employee', 'column', 'Name'
go

execute sp_addextendedproperty 'MS_Description', 
   '性别',
   'user', 'dbo', 'table', 'Employee', 'column', 'Sex'
go

execute sp_addextendedproperty 'MS_Description', 
   '生日',
   'user', 'dbo', 'table', 'Employee', 'column', 'BirthDay'
go

execute sp_addextendedproperty 'MS_Description', 
   '手机号码',
   'user', 'dbo', 'table', 'Employee', 'column', 'Telephone'
go

execute sp_addextendedproperty 'MS_Description', 
   '座机号码',
   'user', 'dbo', 'table', 'Employee', 'column', 'Phone'
go

execute sp_addextendedproperty 'MS_Description', 
   '在职状态',
   'user', 'dbo', 'table', 'Employee', 'column', 'WorkStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Employee', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Employee', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Employee', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Employee', 'column', 'LastModifyTime'
go

alter table dbo.Employee
   add constraint PK_EMPLOYEE primary key (EmpId)
go

/****** Object:  Stored Procedure [dbo].EmployeeGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeGet]
GO

/****** Object:  Stored Procedure [dbo].EmployeeLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeLoad]
GO

/****** Object:  Stored Procedure [dbo].EmployeeInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeInsert]
GO

/****** Object:  Stored Procedure [dbo].EmployeeUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeUpdate]
GO

/****** Object:  Stored Procedure [dbo].EmployeeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].EmployeeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EmployeeGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[EmployeeGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].EmployeeUpdateStatus
// 存储过程功能描述：更新Employee中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Employee'

set @str = 'update [dbo].[Employee] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where EmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].EmployeeGoBack
// 存储过程功能描述：撤返Employee，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Employee'

set @str = 'update [dbo].[Employee] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where EmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].EmployeeGet
// 存储过程功能描述：查询指定Employee的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeGet
    /*
	@EmpId int
    */
    @id int
AS

SELECT
	[EmpId],
	[DeptId],
	[EmpCode],
	[Name],
	[Sex],
	[BirthDay],
	[Telephone],
	[Phone],
	[WorkStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Employee]
WHERE
	[EmpId] = @id

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
// 存储过程名：[dbo].EmployeeLoad
// 存储过程功能描述：查询所有Employee记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeLoad
AS

SELECT
	[EmpId],
	[DeptId],
	[EmpCode],
	[Name],
	[Sex],
	[BirthDay],
	[Telephone],
	[Phone],
	[WorkStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Employee]

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
// 存储过程名：[dbo].EmployeeInsert
// 存储过程功能描述：新增一条Employee记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeInsert
	@DeptId int ,
	@EmpCode varchar(20) =NULL ,
	@Name varchar(20) =NULL ,
	@Sex bit =NULL ,
	@BirthDay datetime =NULL ,
	@Telephone varchar(20) =NULL ,
	@Phone varchar(20) =NULL ,
	@WorkStatus int =NULL ,
	@CreatorId int =NULL ,
	@EmpId int OUTPUT
AS

INSERT INTO [dbo].[Employee] (
	[DeptId],
	[EmpCode],
	[Name],
	[Sex],
	[BirthDay],
	[Telephone],
	[Phone],
	[WorkStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DeptId,
	@EmpCode,
	@Name,
	@Sex,
	@BirthDay,
	@Telephone,
	@Phone,
	@WorkStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @EmpId = @@IDENTITY

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
// 存储过程名：[dbo].EmployeeUpdate
// 存储过程功能描述：更新Employee
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].EmployeeUpdate
    @EmpId int,
@DeptId int,
@EmpCode varchar(20) = NULL,
@Name varchar(20) = NULL,
@Sex bit = NULL,
@BirthDay datetime = NULL,
@Telephone varchar(20) = NULL,
@Phone varchar(20) = NULL,
@WorkStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Employee] SET
	[DeptId] = @DeptId,
	[EmpCode] = @EmpCode,
	[Name] = @Name,
	[Sex] = @Sex,
	[BirthDay] = @BirthDay,
	[Telephone] = @Telephone,
	[Phone] = @Phone,
	[WorkStatus] = @WorkStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[EmpId] = @EmpId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



