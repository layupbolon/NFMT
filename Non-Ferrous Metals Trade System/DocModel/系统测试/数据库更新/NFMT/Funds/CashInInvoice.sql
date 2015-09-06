alter table Fun_CashInInvoice_Ref
   drop constraint PK_FUN_CASHININVOICE_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fun_CashInInvoice_Ref')
            and   type = 'U')
   drop table Fun_CashInInvoice_Ref
go

/*==============================================================*/
/* Table: Fun_CashInInvoice_Ref                                 */
/*==============================================================*/
create table Fun_CashInInvoice_Ref (
   RefId                int                  identity,
   CashInId             int                  null,
   InvoiceId            int                  null,
   DetailId             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '收款分配至发票',
   'user', @CurrentUser, 'table', 'Fun_CashInInvoice_Ref'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fun_CashInInvoice_Ref', 'column', 'RefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收款登记序号',
   'user', @CurrentUser, 'table', 'Fun_CashInInvoice_Ref', 'column', 'CashInId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发票序号',
   'user', @CurrentUser, 'table', 'Fun_CashInInvoice_Ref', 'column', 'InvoiceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'Fun_CashInInvoice_Ref', 'column', 'DetailId'
go

alter table Fun_CashInInvoice_Ref
   add constraint PK_FUN_CASHININVOICE_REF primary key (RefId)
go


/****** Object:  Stored Procedure [dbo].Fun_CashInInvoice_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInInvoice_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInInvoice_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInInvoice_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInInvoice_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInInvoice_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInInvoice_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInInvoice_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInInvoice_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInInvoice_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInInvoice_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInInvoice_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInInvoice_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInInvoice_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInInvoice_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInInvoice_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInInvoice_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInInvoice_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_CashInInvoice_RefUpdateStatus
// 存储过程功能描述：更新Fun_CashInInvoice_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInInvoice_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInInvoice_Ref'

set @str = 'update [dbo].[Fun_CashInInvoice_Ref] '+
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
// 存储过程名：[dbo].Fun_CashInInvoice_RefGoBack
// 存储过程功能描述：撤返Fun_CashInInvoice_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInInvoice_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInInvoice_Ref'

set @str = 'update [dbo].[Fun_CashInInvoice_Ref] '+
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
// 存储过程名：[dbo].Fun_CashInInvoice_RefGet
// 存储过程功能描述：查询指定Fun_CashInInvoice_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInInvoice_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[CashInId],
	[InvoiceId],
	[DetailId]
FROM
	[dbo].[Fun_CashInInvoice_Ref]
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
// 存储过程名：[dbo].Fun_CashInInvoice_RefLoad
// 存储过程功能描述：查询所有Fun_CashInInvoice_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInInvoice_RefLoad
AS

SELECT
	[RefId],
	[CashInId],
	[InvoiceId],
	[DetailId]
FROM
	[dbo].[Fun_CashInInvoice_Ref]

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
// 存储过程名：[dbo].Fun_CashInInvoice_RefInsert
// 存储过程功能描述：新增一条Fun_CashInInvoice_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInInvoice_RefInsert
	@CashInId int =NULL ,
	@InvoiceId int =NULL ,
	@DetailId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Fun_CashInInvoice_Ref] (
	[CashInId],
	[InvoiceId],
	[DetailId]
) VALUES (
	@CashInId,
	@InvoiceId,
	@DetailId
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
// 存储过程名：[dbo].Fun_CashInInvoice_RefUpdate
// 存储过程功能描述：更新Fun_CashInInvoice_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInInvoice_RefUpdate
    @RefId int,
@CashInId int = NULL,
@InvoiceId int = NULL,
@DetailId int = NULL
AS

UPDATE [dbo].[Fun_CashInInvoice_Ref] SET
	[CashInId] = @CashInId,
	[InvoiceId] = @InvoiceId,
	[DetailId] = @DetailId
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



