alter table St_ContractStockIn_Ref
   drop constraint PK_ST_CONTRACTSTOCKIN_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_ContractStockIn_Ref')
            and   type = 'U')
   drop table St_ContractStockIn_Ref
go

/*==============================================================*/
/* Table: St_ContractStockIn_Ref                                */
/*==============================================================*/
create table St_ContractStockIn_Ref (
   RefId                int                  identity,
   StockInId            int                  null,
   ContractId           int                  null,
   ContractSubId        int                  null,
   RefStatus            int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '入库登记合约关联',
   'user', @CurrentUser, 'table', 'St_ContractStockIn_Ref'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'St_ContractStockIn_Ref', 'column', 'RefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '登记序号',
   'user', @CurrentUser, 'table', 'St_ContractStockIn_Ref', 'column', 'StockInId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'St_ContractStockIn_Ref', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'St_ContractStockIn_Ref', 'column', 'ContractSubId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关联状态',
   'user', @CurrentUser, 'table', 'St_ContractStockIn_Ref', 'column', 'RefStatus'
go

alter table St_ContractStockIn_Ref
   add constraint PK_ST_CONTRACTSTOCKIN_REF primary key (RefId)
go

/****** Object:  Stored Procedure [dbo].St_ContractStockIn_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_ContractStockIn_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_ContractStockIn_RefGet]
GO

/****** Object:  Stored Procedure [dbo].St_ContractStockIn_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_ContractStockIn_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_ContractStockIn_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].St_ContractStockIn_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_ContractStockIn_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_ContractStockIn_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].St_ContractStockIn_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_ContractStockIn_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_ContractStockIn_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_ContractStockIn_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_ContractStockIn_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_ContractStockIn_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_ContractStockIn_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_ContractStockIn_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_ContractStockIn_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_ContractStockIn_RefUpdateStatus
// 存储过程功能描述：更新St_ContractStockIn_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_ContractStockIn_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_ContractStockIn_Ref'

set @str = 'update [dbo].[St_ContractStockIn_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_ContractStockIn_RefGoBack
// 存储过程功能描述：撤返St_ContractStockIn_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_ContractStockIn_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_ContractStockIn_Ref'

set @str = 'update [dbo].[St_ContractStockIn_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_ContractStockIn_RefGet
// 存储过程功能描述：查询指定St_ContractStockIn_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_ContractStockIn_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[StockInId],
	[ContractId],
	[ContractSubId],
	[RefStatus]
FROM
	[dbo].[St_ContractStockIn_Ref]
WHERE
	[RefId] = @id

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
// 存储过程名：[dbo].St_ContractStockIn_RefLoad
// 存储过程功能描述：查询所有St_ContractStockIn_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_ContractStockIn_RefLoad
AS

SELECT
	[RefId],
	[StockInId],
	[ContractId],
	[ContractSubId],
	[RefStatus]
FROM
	[dbo].[St_ContractStockIn_Ref]

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
// 存储过程名：[dbo].St_ContractStockIn_RefInsert
// 存储过程功能描述：新增一条St_ContractStockIn_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_ContractStockIn_RefInsert
	@StockInId int =NULL ,
	@ContractId int =NULL ,
	@ContractSubId int =NULL ,
	@RefStatus int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[St_ContractStockIn_Ref] (
	[StockInId],
	[ContractId],
	[ContractSubId],
	[RefStatus]
) VALUES (
	@StockInId,
	@ContractId,
	@ContractSubId,
	@RefStatus
)


SET @RefId = @@IDENTITY

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
// 存储过程名：[dbo].St_ContractStockIn_RefUpdate
// 存储过程功能描述：更新St_ContractStockIn_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_ContractStockIn_RefUpdate
    @RefId int,
@StockInId int = NULL,
@ContractId int = NULL,
@ContractSubId int = NULL,
@RefStatus int = NULL
AS

UPDATE [dbo].[St_ContractStockIn_Ref] SET
	[StockInId] = @StockInId,
	[ContractId] = @ContractId,
	[ContractSubId] = @ContractSubId,
	[RefStatus] = @RefStatus
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



