alter table dbo.Department
   drop constraint PK_DEPARTMENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Department')
            and   type = 'U')
   drop table dbo.Department
go

/*==============================================================*/
/* Table: Department                                            */
/*==============================================================*/
create table dbo.Department (
   DeptId               int                  identity,
   CorpId               int                  null,
   DeptCode             varchar(80)          null,
   DeptName             varchar(80)          null,
   DeptFullName         varchar(80)          null,
   DeptShort            varchar(80)          null,
   DeptType             int                  null,
   ParentLeve           int                  null,
   DeptStatus           int                  null,
   DeptLevel            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '部门',
   'user', 'dbo', 'table', 'Department'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门序号',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属公司',
   'user', 'dbo', 'table', 'Department', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门编号',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门名称',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptName'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门全称',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptFullName'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门缩写',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptShort'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门类型',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptType'
go

execute sp_addextendedproperty 'MS_Description', 
   '上级部门序号',
   'user', 'dbo', 'table', 'Department', 'column', 'ParentLeve'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门状态',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门级别',
   'user', 'dbo', 'table', 'Department', 'column', 'DeptLevel'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Department', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Department', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Department', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Department', 'column', 'LastModifyTime'
go

alter table dbo.Department
   add constraint PK_DEPARTMENT primary key (DeptId)
go

/****** Object:  Stored Procedure [dbo].DepartmentGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DepartmentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DepartmentGet]
GO

/****** Object:  Stored Procedure [dbo].DepartmentLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DepartmentLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DepartmentLoad]
GO

/****** Object:  Stored Procedure [dbo].DepartmentInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DepartmentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DepartmentInsert]
GO

/****** Object:  Stored Procedure [dbo].DepartmentUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DepartmentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DepartmentUpdate]
GO

/****** Object:  Stored Procedure [dbo].DepartmentUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DepartmentUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DepartmentUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].DepartmentUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DepartmentGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DepartmentGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].DepartmentUpdateStatus
// 存储过程功能描述：更新Department中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DepartmentUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Department'

set @str = 'update [dbo].[Department] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DeptId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].DepartmentGoBack
// 存储过程功能描述：撤返Department，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DepartmentGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Department'

set @str = 'update [dbo].[Department] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DeptId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].DepartmentGet
// 存储过程功能描述：查询指定Department的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DepartmentGet
    /*
	@DeptId int
    */
    @id int
AS

SELECT
	[DeptId],
	[CorpId],
	[DeptCode],
	[DeptName],
	[DeptFullName],
	[DeptShort],
	[DeptType],
	[ParentLeve],
	[DeptStatus],
	[DeptLevel],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Department]
WHERE
	[DeptId] = @id

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
// 存储过程名：[dbo].DepartmentLoad
// 存储过程功能描述：查询所有Department记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DepartmentLoad
AS

SELECT
	[DeptId],
	[CorpId],
	[DeptCode],
	[DeptName],
	[DeptFullName],
	[DeptShort],
	[DeptType],
	[ParentLeve],
	[DeptStatus],
	[DeptLevel],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Department]

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
// 存储过程名：[dbo].DepartmentInsert
// 存储过程功能描述：新增一条Department记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DepartmentInsert
	@CorpId int ,
	@DeptCode varchar(80) =NULL ,
	@DeptName varchar(80) ,
	@DeptFullName varchar(80) =NULL ,
	@DeptShort varchar(80) =NULL ,
	@DeptType int =NULL ,
	@ParentLeve int =NULL ,
	@DeptStatus int ,
	@DeptLevel int =NULL ,
	@CreatorId int ,
	@DeptId int OUTPUT
AS

INSERT INTO [dbo].[Department] (
	[CorpId],
	[DeptCode],
	[DeptName],
	[DeptFullName],
	[DeptShort],
	[DeptType],
	[ParentLeve],
	[DeptStatus],
	[DeptLevel],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CorpId,
	@DeptCode,
	@DeptName,
	@DeptFullName,
	@DeptShort,
	@DeptType,
	@ParentLeve,
	@DeptStatus,
	@DeptLevel,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DeptId = @@IDENTITY

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
// 存储过程名：[dbo].DepartmentUpdate
// 存储过程功能描述：更新Department
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].DepartmentUpdate
    @DeptId int,
@CorpId int,
@DeptCode varchar(80) = NULL,
@DeptName varchar(80),
@DeptFullName varchar(80) = NULL,
@DeptShort varchar(80) = NULL,
@DeptType int = NULL,
@ParentLeve int = NULL,
@DeptStatus int,
@DeptLevel int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Department] SET
	[CorpId] = @CorpId,
	[DeptCode] = @DeptCode,
	[DeptName] = @DeptName,
	[DeptFullName] = @DeptFullName,
	[DeptShort] = @DeptShort,
	[DeptType] = @DeptType,
	[ParentLeve] = @ParentLeve,
	[DeptStatus] = @DeptStatus,
	[DeptLevel] = @DeptLevel,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DeptId] = @DeptId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



