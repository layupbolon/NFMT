alter table dbo.CorpDept
   drop constraint PK_CORPDEPT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.CorpDept')
            and   type = 'U')
   drop table dbo.CorpDept
go

/*==============================================================*/
/* Table: CorpDept                                              */
/*==============================================================*/
create table dbo.CorpDept (
   CorpEmpId            int                  identity,
   DeptId               int                  null,
   CorpId               int                  null,
   RefStatus            int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '公司部门关联表',
   'user', 'dbo', 'table', 'CorpDept'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联表序号',
   'user', 'dbo', 'table', 'CorpDept', 'column', 'CorpEmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门序号',
   'user', 'dbo', 'table', 'CorpDept', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', 'dbo', 'table', 'CorpDept', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联状态',
   'user', 'dbo', 'table', 'CorpDept', 'column', 'RefStatus'
go

alter table dbo.CorpDept
   add constraint PK_CORPDEPT primary key (CorpEmpId)
go

/****** Object:  Stored Procedure [dbo].CorpDeptGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDeptGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDeptGet]
GO

/****** Object:  Stored Procedure [dbo].CorpDeptLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDeptLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDeptLoad]
GO

/****** Object:  Stored Procedure [dbo].CorpDeptInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDeptInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDeptInsert]
GO

/****** Object:  Stored Procedure [dbo].CorpDeptUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDeptUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDeptUpdate]
GO

/****** Object:  Stored Procedure [dbo].CorpDeptUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDeptUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDeptUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].CorpDeptUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CorpDeptGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CorpDeptGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].CorpDeptUpdateStatus
// 存储过程功能描述：更新CorpDept中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDeptUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.CorpDept'

set @str = 'update [dbo].[CorpDept] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CorpEmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CorpDeptGoBack
// 存储过程功能描述：撤返CorpDept，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDeptGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.CorpDept'

set @str = 'update [dbo].[CorpDept] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CorpEmpId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].CorpDeptGet
// 存储过程功能描述：查询指定CorpDept的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDeptGet
    /*
	@CorpEmpId int
    */
    @id int
AS

SELECT
	[CorpEmpId],
	[DeptId],
	[CorpId],
	[RefStatus]
FROM
	[dbo].[CorpDept]
WHERE
	[CorpEmpId] = @id

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
// 存储过程名：[dbo].CorpDeptLoad
// 存储过程功能描述：查询所有CorpDept记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDeptLoad
AS

SELECT
	[CorpEmpId],
	[DeptId],
	[CorpId],
	[RefStatus]
FROM
	[dbo].[CorpDept]

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
// 存储过程名：[dbo].CorpDeptInsert
// 存储过程功能描述：新增一条CorpDept记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDeptInsert
	@DeptId int =NULL ,
	@CorpId int =NULL ,
	@RefStatus int =NULL ,
	@CorpEmpId int OUTPUT
AS

INSERT INTO [dbo].[CorpDept] (
	[DeptId],
	[CorpId],
	[RefStatus]
) VALUES (
	@DeptId,
	@CorpId,
	@RefStatus
)


SET @CorpEmpId = @@IDENTITY

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
// 存储过程名：[dbo].CorpDeptUpdate
// 存储过程功能描述：更新CorpDept
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].CorpDeptUpdate
    @CorpEmpId int,
@DeptId int = NULL,
@CorpId int = NULL,
@RefStatus int = NULL
AS

UPDATE [dbo].[CorpDept] SET
	[DeptId] = @DeptId,
	[CorpId] = @CorpId,
	[RefStatus] = @RefStatus
WHERE
	[CorpEmpId] = @CorpEmpId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



