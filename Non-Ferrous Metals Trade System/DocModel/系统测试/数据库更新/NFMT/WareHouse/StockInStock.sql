alter table St_StockInStock_Ref
   drop constraint PK_ST_STOCKINSTOCK_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_StockInStock_Ref')
            and   type = 'U')
   drop table St_StockInStock_Ref
go

/*==============================================================*/
/* Table: St_StockInStock_Ref                                   */
/*==============================================================*/
create table St_StockInStock_Ref (
   RefId                int                  identity,
   StockInId            int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   RefStatus            int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '入库登记库存关联',
   'user', @CurrentUser, 'table', 'St_StockInStock_Ref'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'St_StockInStock_Ref', 'column', 'RefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '登记序号',
   'user', @CurrentUser, 'table', 'St_StockInStock_Ref', 'column', 'StockInId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_StockInStock_Ref', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'St_StockInStock_Ref', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关联状态',
   'user', @CurrentUser, 'table', 'St_StockInStock_Ref', 'column', 'RefStatus'
go

alter table St_StockInStock_Ref
   add constraint PK_ST_STOCKINSTOCK_REF primary key (RefId)
go

/****** Object:  Stored Procedure [dbo].St_StockInStock_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInStock_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInStock_RefGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockInStock_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInStock_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInStock_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockInStock_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInStock_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInStock_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockInStock_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInStock_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInStock_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockInStock_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInStock_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInStock_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockInStock_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInStock_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInStock_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockInStock_RefUpdateStatus
// 存储过程功能描述：更新St_StockInStock_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInStock_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockInStock_Ref'

set @str = 'update [dbo].[St_StockInStock_Ref] '+
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
// 存储过程名：[dbo].St_StockInStock_RefGoBack
// 存储过程功能描述：撤返St_StockInStock_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInStock_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockInStock_Ref'

set @str = 'update [dbo].[St_StockInStock_Ref] '+
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
// 存储过程名：[dbo].St_StockInStock_RefGet
// 存储过程功能描述：查询指定St_StockInStock_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInStock_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[StockInId],
	[StockId],
	[StockLogId],
	[RefStatus]
FROM
	[dbo].[St_StockInStock_Ref]
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
// 存储过程名：[dbo].St_StockInStock_RefLoad
// 存储过程功能描述：查询所有St_StockInStock_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInStock_RefLoad
AS

SELECT
	[RefId],
	[StockInId],
	[StockId],
	[StockLogId],
	[RefStatus]
FROM
	[dbo].[St_StockInStock_Ref]

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
// 存储过程名：[dbo].St_StockInStock_RefInsert
// 存储过程功能描述：新增一条St_StockInStock_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInStock_RefInsert
	@StockInId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@RefStatus int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[St_StockInStock_Ref] (
	[StockInId],
	[StockId],
	[StockLogId],
	[RefStatus]
) VALUES (
	@StockInId,
	@StockId,
	@StockLogId,
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
// 存储过程名：[dbo].St_StockInStock_RefUpdate
// 存储过程功能描述：更新St_StockInStock_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInStock_RefUpdate
    @RefId int,
@StockInId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@RefStatus int = NULL
AS

UPDATE [dbo].[St_StockInStock_Ref] SET
	[StockInId] = @StockInId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[RefStatus] = @RefStatus
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



