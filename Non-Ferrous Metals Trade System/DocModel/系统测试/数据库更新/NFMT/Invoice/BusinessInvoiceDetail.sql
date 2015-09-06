alter table dbo.Inv_BusinessInvoiceDetail
   drop constraint PK_INV_BUSINESSINVOICEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Inv_BusinessInvoiceDetail')
            and   type = 'U')
   drop table dbo.Inv_BusinessInvoiceDetail
go

/*==============================================================*/
/* Table: Inv_BusinessInvoiceDetail                             */
/*==============================================================*/
create table dbo.Inv_BusinessInvoiceDetail (
   DetailId             int                  identity,
   BusinessInvoiceId    int                  null,
   InvoiceId            int                  null,
   RefDetailId          int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   ConfirmPriceId       int                  null,
   ConfirmDetailId      int                  null,
   PricingId            int                  null,
   PricingDetailId      int                  null,
   FeeType              int                  null,
   IntegerAmount        decimal(18,4)        null,
   NetAmount            decimal(18,4)        null,
   UnitPrice            decimal(18,4)        null,
   CalculateDay         decimal(18,4)        null,
   Bala                 decimal(18,4)        null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '业务发票明细',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务发票序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'BusinessInvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '主表发票序号.',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联明细序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'RefDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'StockLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格确认序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'ConfirmPriceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格确认明细序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'ConfirmDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'PricingId'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价明细序号',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'PricingDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票内容',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'FeeType'
go

execute sp_addextendedproperty 'MS_Description', 
   '毛吨',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'IntegerAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '净吨',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '单价',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'UnitPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '计价天数',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'CalculateDay'
go

execute sp_addextendedproperty 'MS_Description', 
   '金额',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'Bala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Inv_BusinessInvoiceDetail', 'column', 'DetailStatus'
go

alter table dbo.Inv_BusinessInvoiceDetail
   add constraint PK_INV_BUSINESSINVOICEDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceDetailGet    Script Date: 2015年3月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceDetailLoad    Script Date: 2015年3月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceDetailInsert    Script Date: 2015年3月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceDetailUpdate    Script Date: 2015年3月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceDetailUpdateStatus    Script Date: 2015年3月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_BusinessInvoiceDetailUpdateStatus    Script Date: 2015年3月26日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_BusinessInvoiceDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_BusinessInvoiceDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailUpdateStatus
// 存储过程功能描述：更新Inv_BusinessInvoiceDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_BusinessInvoiceDetail'

set @str = 'update [dbo].[Inv_BusinessInvoiceDetail] '+
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
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailGoBack
// 存储过程功能描述：撤返Inv_BusinessInvoiceDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_BusinessInvoiceDetail'

set @str = 'update [dbo].[Inv_BusinessInvoiceDetail] '+
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
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailGet
// 存储过程功能描述：查询指定Inv_BusinessInvoiceDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[BusinessInvoiceId],
	[InvoiceId],
	[RefDetailId],
	[StockId],
	[StockLogId],
	[ConfirmPriceId],
	[ConfirmDetailId],
	[PricingId],
	[PricingDetailId],
	[FeeType],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[CalculateDay],
	[Bala],
	[DetailStatus]
FROM
	[dbo].[Inv_BusinessInvoiceDetail]
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
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailLoad
// 存储过程功能描述：查询所有Inv_BusinessInvoiceDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceDetailLoad
AS

SELECT
	[DetailId],
	[BusinessInvoiceId],
	[InvoiceId],
	[RefDetailId],
	[StockId],
	[StockLogId],
	[ConfirmPriceId],
	[ConfirmDetailId],
	[PricingId],
	[PricingDetailId],
	[FeeType],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[CalculateDay],
	[Bala],
	[DetailStatus]
FROM
	[dbo].[Inv_BusinessInvoiceDetail]

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
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailInsert
// 存储过程功能描述：新增一条Inv_BusinessInvoiceDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceDetailInsert
	@BusinessInvoiceId int =NULL ,
	@InvoiceId int =NULL ,
	@RefDetailId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@ConfirmPriceId int =NULL ,
	@ConfirmDetailId int =NULL ,
	@PricingId int =NULL ,
	@PricingDetailId int =NULL ,
	@FeeType int =NULL ,
	@IntegerAmount decimal(18, 4) =NULL ,
	@NetAmount decimal(18, 4) =NULL ,
	@UnitPrice decimal(18, 4) =NULL ,
	@CalculateDay decimal(18, 4) =NULL ,
	@Bala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Inv_BusinessInvoiceDetail] (
	[BusinessInvoiceId],
	[InvoiceId],
	[RefDetailId],
	[StockId],
	[StockLogId],
	[ConfirmPriceId],
	[ConfirmDetailId],
	[PricingId],
	[PricingDetailId],
	[FeeType],
	[IntegerAmount],
	[NetAmount],
	[UnitPrice],
	[CalculateDay],
	[Bala],
	[DetailStatus]
) VALUES (
	@BusinessInvoiceId,
	@InvoiceId,
	@RefDetailId,
	@StockId,
	@StockLogId,
	@ConfirmPriceId,
	@ConfirmDetailId,
	@PricingId,
	@PricingDetailId,
	@FeeType,
	@IntegerAmount,
	@NetAmount,
	@UnitPrice,
	@CalculateDay,
	@Bala,
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
// 存储过程名：[dbo].Inv_BusinessInvoiceDetailUpdate
// 存储过程功能描述：更新Inv_BusinessInvoiceDetail
// 创建人：CodeSmith
// 创建时间： 2015年3月26日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_BusinessInvoiceDetailUpdate
    @DetailId int,
@BusinessInvoiceId int = NULL,
@InvoiceId int = NULL,
@RefDetailId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@ConfirmPriceId int = NULL,
@ConfirmDetailId int = NULL,
@PricingId int = NULL,
@PricingDetailId int = NULL,
@FeeType int = NULL,
@IntegerAmount decimal(18, 4) = NULL,
@NetAmount decimal(18, 4) = NULL,
@UnitPrice decimal(18, 4) = NULL,
@CalculateDay decimal(18, 4) = NULL,
@Bala decimal(18, 4) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Inv_BusinessInvoiceDetail] SET
	[BusinessInvoiceId] = @BusinessInvoiceId,
	[InvoiceId] = @InvoiceId,
	[RefDetailId] = @RefDetailId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[ConfirmPriceId] = @ConfirmPriceId,
	[ConfirmDetailId] = @ConfirmDetailId,
	[PricingId] = @PricingId,
	[PricingDetailId] = @PricingDetailId,
	[FeeType] = @FeeType,
	[IntegerAmount] = @IntegerAmount,
	[NetAmount] = @NetAmount,
	[UnitPrice] = @UnitPrice,
	[CalculateDay] = @CalculateDay,
	[Bala] = @Bala,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



