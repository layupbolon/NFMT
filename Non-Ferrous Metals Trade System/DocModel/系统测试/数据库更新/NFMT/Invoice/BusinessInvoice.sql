alter table dbo.Inv_BusinessInvoice
   drop constraint PK_INV_BUSINESSINVOICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Inv_BusinessInvoice')
            and   type = 'U')
   drop table dbo.Inv_BusinessInvoice
go

/*==============================================================*/
/* Table: Inv_BusinessInvoice                                   */
/*==============================================================*/
create table dbo.Inv_BusinessInvoice (
   BusinessInvoiceId    int                  identity,
   InvoiceId            int                  null,
   RefInvoiceId         int                  null,
   ContractId           int                  null,
   SubContractId        int                  null,
   AssetId              int                  null,
   IntegerAmount        numeric(18,4)        null,
   NetAmount            numeric(18,4)        null,
   UnitPrice            numeric(18,4)        null,
   MUId                 int                  null,
   MarginRatio          numeric(18,4)        null,
   VATRatio             numeric(18,4)        null,
   VATBala              numeric(18,4)        null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '业务发票',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务发票序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'BusinessInvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '主表发票序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'SubContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '毛量',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'IntegerAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '净量',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '单价',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'UnitPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'MUId'
go

execute sp_addextendedproperty 'MS_Description', 
   '保证金比例',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'MarginRatio'
go

execute sp_addextendedproperty 'MS_Description', 
   '增值税率',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'VATRatio'
go

execute sp_addextendedproperty 'MS_Description', 
   '增值税',
   'user', 'dbo', 'table', 'Inv_BusinessInvoice', 'column', 'VATBala'
go

alter table dbo.Inv_BusinessInvoice
   add constraint PK_INV_BUSINESSINVOICE primary key (BusinessInvoiceId)
go


/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceUpdateStatus
// 存储过程功能描述：更新Inv_BusinessInvoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_BusinessInvoice'

set @str = 'update [dbo].[Inv_BusinessInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where BusinessInvoiceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_BusinessInvoiceGoBack
// 存储过程功能描述：撤返Inv_BusinessInvoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_BusinessInvoice'

set @str = 'update [dbo].[Inv_BusinessInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where BusinessInvoiceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_BusinessInvoiceGet
// 存储过程功能描述：查询指定Inv_BusinessInvoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceGet
    /*
	@BusinessInvoiceId int
    */
    @id int
AS

SELECT
	[BusinessInvoiceId],
	[InvoiceId],
	[RefInvoiceId],
	[ContractId],
	[SubContractId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[MUId],
	[MarginRatio],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_BusinessInvoice]
WHERE
	[BusinessInvoiceId] = @id

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
// 存储过程名：[dbo].Inv_BusinessInvoiceLoad
// 存储过程功能描述：查询所有Inv_BusinessInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceLoad
AS

SELECT
	[BusinessInvoiceId],
	[InvoiceId],
	[RefInvoiceId],
	[ContractId],
	[SubContractId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[MUId],
	[MarginRatio],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_BusinessInvoice]

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
// 存储过程名：[dbo].Inv_BusinessInvoiceInsert
// 存储过程功能描述：新增一条Inv_BusinessInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceInsert
	@InvoiceId int =NULL ,
	@RefInvoiceId int =NULL ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@AssetId int =NULL ,
	@IntegerAmount numeric(18, 4) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@UnitPrice numeric(18, 4) =NULL ,
	@MUId int =NULL ,
	@MarginRatio numeric(18, 4) =NULL ,
	@VATRatio numeric(18, 4) =NULL ,
	@VATBala numeric(18, 4) =NULL ,
	@BusinessInvoiceId int OUTPUT
AS

INSERT INTO [dbo].[Inv_BusinessInvoice] (
	[InvoiceId],
	[RefInvoiceId],
	[ContractId],
	[SubContractId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[MUId],
	[MarginRatio],
	[VATRatio],
	[VATBala]
) VALUES (
	@InvoiceId,
	@RefInvoiceId,
	@ContractId,
	@SubContractId,
	@AssetId,
	@IntegerAmount,
	@NetAmount,
	@UnitPrice,
	@MUId,
	@MarginRatio,
	@VATRatio,
	@VATBala
)


SET @BusinessInvoiceId = @@IDENTITY

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
// 存储过程名：[dbo].Inv_BusinessInvoiceUpdate
// 存储过程功能描述：更新Inv_BusinessInvoice
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceUpdate
    @BusinessInvoiceId int,
@InvoiceId int = NULL,
@RefInvoiceId int = NULL,
@ContractId int = NULL,
@SubContractId int = NULL,
@AssetId int = NULL,
@IntegerAmount numeric(18, 4) = NULL,
@NetAmount numeric(18, 4) = NULL,
@UnitPrice numeric(18, 4) = NULL,
@MUId int = NULL,
@MarginRatio numeric(18, 4) = NULL,
@VATRatio numeric(18, 4) = NULL,
@VATBala numeric(18, 4) = NULL
AS

UPDATE [dbo].[Inv_BusinessInvoice] SET
	[InvoiceId] = @InvoiceId,
	[RefInvoiceId] = @RefInvoiceId,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[AssetId] = @AssetId,
	[IntegerAmount] = @IntegerAmount,
	[NetAmount] = @NetAmount,
	[UnitPrice] = @UnitPrice,
	[MUId] = @MUId,
	[MarginRatio] = @MarginRatio,
	[VATRatio] = @VATRatio,
	[VATBala] = @VATBala
WHERE
	[BusinessInvoiceId] = @BusinessInvoiceId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



