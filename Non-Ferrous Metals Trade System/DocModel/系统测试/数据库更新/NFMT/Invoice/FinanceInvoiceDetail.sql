alter table dbo.Inv_FinanceInvoiceDetail
   drop constraint PK_INV_FINANCEINVOICEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Inv_FinanceInvoiceDetail')
            and   type = 'U')
   drop table dbo.Inv_FinanceInvoiceDetail
go

/*==============================================================*/
/* Table: Inv_FinanceInvoiceDetail                              */
/*==============================================================*/
create table dbo.Inv_FinanceInvoiceDetail (
   DetailId             int                  identity,
   FinanceInvoiceId     int                  null,
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
   '财务发票明细',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务发票序号',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'FinanceInvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '主表发票序号',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '毛量',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'IntegerAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '净量',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'MUId'
go

execute sp_addextendedproperty 'MS_Description', 
   '增值税率',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'VATRatio'
go

execute sp_addextendedproperty 'MS_Description', 
   '增值税',
   'user', 'dbo', 'table', 'Inv_FinanceInvoiceDetail', 'column', 'VATBala'
go

alter table dbo.Inv_FinanceInvoiceDetail
   add constraint PK_INV_FINANCEINVOICEDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinanceInvoiceDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinanceInvoiceDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinanceInvoiceDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_FinanceInvoiceDetailUpdateStatus
// 存储过程功能描述：更新Inv_FinanceInvoiceDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinanceInvoiceDetail'

set @str = 'update [dbo].[Inv_FinanceInvoiceDetail] '+
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
// 存储过程名：[dbo].Inv_FinanceInvoiceDetailGoBack
// 存储过程功能描述：撤返Inv_FinanceInvoiceDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinanceInvoiceDetail'

set @str = 'update [dbo].[Inv_FinanceInvoiceDetail] '+
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
// 存储过程名：[dbo].Inv_FinanceInvoiceDetailGet
// 存储过程功能描述：查询指定Inv_FinanceInvoiceDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[FinanceInvoiceId],
	[InvoiceId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_FinanceInvoiceDetail]
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
// 存储过程名：[dbo].Inv_FinanceInvoiceDetailLoad
// 存储过程功能描述：查询所有Inv_FinanceInvoiceDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceDetailLoad
AS

SELECT
	[DetailId],
	[FinanceInvoiceId],
	[InvoiceId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[VATRatio],
	[VATBala]
FROM
	[dbo].[Inv_FinanceInvoiceDetail]

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
// 存储过程名：[dbo].Inv_FinanceInvoiceDetailInsert
// 存储过程功能描述：新增一条Inv_FinanceInvoiceDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceDetailInsert
	@FinanceInvoiceId int =NULL ,
	@InvoiceId int =NULL ,
	@AssetId int =NULL ,
	@IntegerAmount numeric(18, 4) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@MUId int =NULL ,
	@VATRatio numeric(18, 4) =NULL ,
	@VATBala numeric(18, 4) =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Inv_FinanceInvoiceDetail] (
	[FinanceInvoiceId],
	[InvoiceId],
	[AssetId],
	[IntegerAmount],
	[NetAmount],
	[MUId],
	[VATRatio],
	[VATBala]
) VALUES (
	@FinanceInvoiceId,
	@InvoiceId,
	@AssetId,
	@IntegerAmount,
	@NetAmount,
	@MUId,
	@VATRatio,
	@VATBala
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
// 存储过程名：[dbo].Inv_FinanceInvoiceDetailUpdate
// 存储过程功能描述：更新Inv_FinanceInvoiceDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinanceInvoiceDetailUpdate
    @DetailId int,
@FinanceInvoiceId int = NULL,
@InvoiceId int = NULL,
@AssetId int = NULL,
@IntegerAmount numeric(18, 4) = NULL,
@NetAmount numeric(18, 4) = NULL,
@MUId int = NULL,
@VATRatio numeric(18, 4) = NULL,
@VATBala numeric(18, 4) = NULL
AS

UPDATE [dbo].[Inv_FinanceInvoiceDetail] SET
	[FinanceInvoiceId] = @FinanceInvoiceId,
	[InvoiceId] = @InvoiceId,
	[AssetId] = @AssetId,
	[IntegerAmount] = @IntegerAmount,
	[NetAmount] = @NetAmount,
	[MUId] = @MUId,
	[VATRatio] = @VATRatio,
	[VATBala] = @VATBala
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



