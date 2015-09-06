alter table St_StockName
   drop constraint PK_ST_STOCKNAME
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_StockName')
            and   type = 'U')
   drop table St_StockName
go

/*==============================================================*/
/* Table: St_StockName                                          */
/*==============================================================*/
create table St_StockName (
   StockNameId          int                  identity,
   RefNo                varchar(50)          null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '业务单号表',
   'user', @CurrentUser, 'table', 'St_StockName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存名称序号',
   'user', @CurrentUser, 'table', 'St_StockName', 'column', 'StockNameId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', @CurrentUser, 'table', 'St_StockName', 'column', 'RefNo'
go

alter table St_StockName
   add constraint PK_ST_STOCKNAME primary key (StockNameId)
go

/****** Object:  Stored Procedure [dbo].St_StockNameGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockNameGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockNameGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockNameLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockNameLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockNameLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockNameInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockNameInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockNameInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockNameUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockNameUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockNameUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockNameUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockNameUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockNameUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockNameUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockNameGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockNameGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockNameUpdateStatus
// 存储过程功能描述：更新St_StockName中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockNameUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockName'

set @str = 'update [dbo].[St_StockName] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockNameId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockNameGoBack
// 存储过程功能描述：撤返St_StockName，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockNameGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockName'

set @str = 'update [dbo].[St_StockName] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockNameId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockNameGet
// 存储过程功能描述：查询指定St_StockName的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockNameGet
    /*
	@StockNameId int
    */
    @id int
AS

SELECT
	[StockNameId],
	[RefNo]
FROM
	[dbo].[St_StockName]
WHERE
	[StockNameId] = @id

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
// 存储过程名：[dbo].St_StockNameLoad
// 存储过程功能描述：查询所有St_StockName记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockNameLoad
AS

SELECT
	[StockNameId],
	[RefNo]
FROM
	[dbo].[St_StockName]

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
// 存储过程名：[dbo].St_StockNameInsert
// 存储过程功能描述：新增一条St_StockName记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockNameInsert
	@RefNo varchar(50) =NULL ,
	@StockNameId int OUTPUT
AS

INSERT INTO [dbo].[St_StockName] (
	[RefNo]
) VALUES (
	@RefNo
)


SET @StockNameId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockNameUpdate
// 存储过程功能描述：更新St_StockName
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockNameUpdate
    @StockNameId int,
@RefNo varchar(50) = NULL
AS

UPDATE [dbo].[St_StockName] SET
	[RefNo] = @RefNo
WHERE
	[StockNameId] = @StockNameId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



