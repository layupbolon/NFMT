alter table dbo.Con_ContractDept
   drop constraint PK_CON_CONTRACTDEPT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractDept')
            and   type = 'U')
   drop table dbo.Con_ContractDept
go

/*==============================================================*/
/* Table: Con_ContractDept                                      */
/*==============================================================*/
create table dbo.Con_ContractDept (
   DetailId             int                  identity,
   ContractId           int                  null,
   DeptId               int                  null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约执行部门明细',
   'user', 'dbo', 'table', 'Con_ContractDept'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Con_ContractDept', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractDept', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '部门序号',
   'user', 'dbo', 'table', 'Con_ContractDept', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Con_ContractDept', 'column', 'DetailStatus'
go

alter table dbo.Con_ContractDept
   add constraint PK_CON_CONTRACTDEPT primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Con_ContractDeptGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDeptGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDeptGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDeptLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDeptLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDeptLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDeptInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDeptInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDeptInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDeptUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDeptUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDeptUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDeptUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDeptUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDeptUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractDeptUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractDeptGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractDeptGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractDeptUpdateStatus
// 存储过程功能描述：更新Con_ContractDept中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDeptUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractDept'

set @str = 'update [dbo].[Con_ContractDept] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractDeptGoBack
// 存储过程功能描述：撤返Con_ContractDept，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDeptGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractDept'

set @str = 'update [dbo].[Con_ContractDept] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractDeptGet
// 存储过程功能描述：查询指定Con_ContractDept的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDeptGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ContractId],
	[DeptId],
	[DetailStatus]
FROM
	[dbo].[Con_ContractDept]
WHERE
	[DetailId] = @id

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
// 存储过程名：[dbo].Con_ContractDeptLoad
// 存储过程功能描述：查询所有Con_ContractDept记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDeptLoad
AS

SELECT
	[DetailId],
	[ContractId],
	[DeptId],
	[DetailStatus]
FROM
	[dbo].[Con_ContractDept]

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
// 存储过程名：[dbo].Con_ContractDeptInsert
// 存储过程功能描述：新增一条Con_ContractDept记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDeptInsert
	@ContractId int =NULL ,
	@DeptId int =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractDept] (
	[ContractId],
	[DeptId],
	[DetailStatus]
) VALUES (
	@ContractId,
	@DeptId,
	@DetailStatus
)


SET @DetailId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractDeptUpdate
// 存储过程功能描述：更新Con_ContractDept
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractDeptUpdate
    @DetailId int,
@ContractId int = NULL,
@DeptId int = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Con_ContractDept] SET
	[ContractId] = @ContractId,
	[DeptId] = @DeptId,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



