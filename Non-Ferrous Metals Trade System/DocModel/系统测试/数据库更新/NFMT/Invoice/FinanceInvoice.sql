alter table dbo.Inv_FinanceInvoice
   drop constraint PK_INV_FINANCEINVOICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Inv_FinanceInvoice')
            and   type = 'U')
   drop table dbo.Inv_FinanceInvoice
go

/*==============================================================*/
/* Table: Inv_FinanceInvoice                                    */
/*==============================================================*/
create table dbo.Inv_FinanceInvoice (
   FinanceInvoiceId     int                  identity,
   InvoiceId            int                  null,
   AssetId              int                  null,
   IntegerAmount        numeric(18,4)        null,
   NetAmount            numeric(18,4)        null,
   MUId                 int                  null,
   VATRatio             numeric(18,4)        null,
   VATBala              numeric(18,4)        null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '财务发票',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务发票序号',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'FinanceInvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '主表发票序号',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '毛量',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'IntegerAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '净量',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'MUId'
go

execute sp_addextendedproperty 'MS_Description', 
   '增值税率',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'VATRatio'
go

execute sp_addextendedproperty 'MS_Description', 
   '增值税',
   'user', 'dbo', 'table', 'Inv_FinanceInvoice', 'column', 'VATBala'
go

alter table dbo.Inv_FinanceInvoice
   add constraint PK_INV_FINANCEINVOICE primary key (FinanceInvoiceId)
go


/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_FinanceInvoiceUpdateStatus
// 存储过程功能描述：更新Inv_FinanceInvoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinanceInvoice'

set @str = 'update [dbo].[Inv_FinanceInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where FinanceInvoiceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_FinanceInvoiceGoBack
// 存储过程功能描述：撤返Inv_FinanceInvoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinanceInvoice'

set @str = 'update [dbo].[Inv_FinanceInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where FinanceInvoiceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_FinanceInvoiceGet
// 存储过程功能描述：查询指定Inv_FinanceInvoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceGet
    /*
	@FinanceInvoiceId int
    */
    @id int
AS

SELECT
	[FinanceInvoiceId],
	[InvoiceId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_FinanceInvoice]
WHERE
	[FinanceInvoiceId] = @id

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
// 存储过程名：[dbo].Inv_FinanceInvoiceLoad
// 存储过程功能描述：查询所有Inv_FinanceInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceLoad
AS

SELECT
	[FinanceInvoiceId],
	[InvoiceId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_FinanceInvoice]

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
// 存储过程名：[dbo].Inv_FinanceInvoiceInsert
// 存储过程功能描述：新增一条Inv_FinanceInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceInsert
	@InvoiceId int =NULL ,
	@AssetId int =NULL ,
	@IntegerAmount numeric(18, 4) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@MUId int =NULL ,
	@VATRatio numeric(18, 4) =NULL ,
	@VATBala numeric(18, 4) =NULL ,
	@FinanceInvoiceId int OUTPUT
AS

INSERT INTO [dbo].[Inv_FinanceInvoice] (
	[InvoiceId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[VATRatio],
	[VATBala]
) VALUES (
	@InvoiceId,
	@AssetId,
	@IntegerAmount,
	@NetAmount,
	@MUId,
	@VATRatio,
	@VATBala
)


SET @FinanceInvoiceId = @@IDENTITY

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
// 存储过程名：[dbo].Inv_FinanceInvoiceUpdate
// 存储过程功能描述：更新Inv_FinanceInvoice
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceUpdate
    @FinanceInvoiceId int,
@InvoiceId int = NULL,
@AssetId int = NULL,
@IntegerAmount numeric(18, 4) = NULL,
@NetAmount numeric(18, 4) = NULL,
@MUId int = NULL,
@VATRatio numeric(18, 4) = NULL,
@VATBala numeric(18, 4) = NULL
AS

UPDATE [dbo].[Inv_FinanceInvoice] SET
	[InvoiceId] = @InvoiceId,
	[AssetId] = @AssetId,
	[IntegerAmount] = @IntegerAmount,
	[NetAmount] = @NetAmount,
	[MUId] = @MUId,
	[VATRatio] = @VATRatio,
	[VATBala] = @VATBala
WHERE
	[FinanceInvoiceId] = @FinanceInvoiceId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



