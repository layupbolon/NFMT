USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentUpdateStatus]    Script Date: 11/27/2014 11:19:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentUpdateStatus]    Script Date: 11/27/2014 11:19:06 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentUpdateStatus
// 存储过程功能描述：更新Doc_Document中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_Document'

set @str = 'update [dbo].[Doc_Document] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DocumentId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentUpdate]    Script Date: 11/27/2014 11:19:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentUpdate]    Script Date: 11/27/2014 11:19:02 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentUpdate
// 存储过程功能描述：更新Doc_Document
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentUpdate]
    @DocumentId int,
@OrderId int = NULL,
@DocumentDate datetime = NULL,
@DocEmpId int = NULL,
@PresentDate datetime = NULL,
@Presenter int = NULL,
@AcceptanceDate datetime = NULL,
@Acceptancer int = NULL,
@Meno varchar(4000) = NULL,
@DocumentStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_Document] SET
	[OrderId] = @OrderId,
	[DocumentDate] = @DocumentDate,
	[DocEmpId] = @DocEmpId,
	[PresentDate] = @PresentDate,
	[Presenter] = @Presenter,
	[AcceptanceDate] = @AcceptanceDate,
	[Acceptancer] = @Acceptancer,
	[Meno] = @Meno,
	[DocumentStatus] = @DocumentStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DocumentId] = @DocumentId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockUpdateStatus]    Script Date: 11/27/2014 11:18:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentStockUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentStockUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockUpdateStatus]    Script Date: 11/27/2014 11:18:58 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentStockUpdateStatus
// 存储过程功能描述：更新Doc_DocumentStock中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentStockUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentStock'

set @str = 'update [dbo].[Doc_DocumentStock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockUpdate]    Script Date: 11/27/2014 11:18:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentStockUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentStockUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockUpdate]    Script Date: 11/27/2014 11:18:54 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentStockUpdate
// 存储过程功能描述：更新Doc_DocumentStock
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentStockUpdate]
    @DetailId int,
@DocumentId int = NULL,
@OrderId int = NULL,
@OrderStockDetailId int = NULL,
@StockId int = NULL,
@StockNameId int = NULL,
@RefNo varchar(200) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentStock] SET
	[DocumentId] = @DocumentId,
	[OrderId] = @OrderId,
	[OrderStockDetailId] = @OrderStockDetailId,
	[StockId] = @StockId,
	[StockNameId] = @StockNameId,
	[RefNo] = @RefNo,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockLoad]    Script Date: 11/27/2014 11:18:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentStockLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentStockLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockLoad]    Script Date: 11/27/2014 11:18:51 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentStockLoad
// 存储过程功能描述：查询所有Doc_DocumentStock记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentStockLoad]
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[OrderStockDetailId],
	[StockId],
	[StockNameId],
	[RefNo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentStock]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockInsert]    Script Date: 11/27/2014 11:18:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentStockInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentStockInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockInsert]    Script Date: 11/27/2014 11:18:47 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentStockInsert
// 存储过程功能描述：新增一条Doc_DocumentStock记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentStockInsert]
	@DocumentId int =NULL ,
	@OrderId int =NULL ,
	@OrderStockDetailId int =NULL ,
	@StockId int =NULL ,
	@StockNameId int =NULL ,
	@RefNo varchar(200) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentStock] (
	[DocumentId],
	[OrderId],
	[OrderStockDetailId],
	[StockId],
	[StockNameId],
	[RefNo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DocumentId,
	@OrderId,
	@OrderStockDetailId,
	@StockId,
	@StockNameId,
	@RefNo,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockGoBack]    Script Date: 11/27/2014 11:18:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentStockGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentStockGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockGoBack]    Script Date: 11/27/2014 11:18:44 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentStockGoBack
// 存储过程功能描述：撤返Doc_DocumentStock，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentStockGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentStock'

set @str = 'update [dbo].[Doc_DocumentStock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockGet]    Script Date: 11/27/2014 11:18:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentStockGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentStockGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentStockGet]    Script Date: 11/27/2014 11:18:40 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentStockGet
// 存储过程功能描述：查询指定Doc_DocumentStock的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentStockGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[OrderStockDetailId],
	[StockId],
	[StockNameId],
	[RefNo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentStock]
WHERE
	[DetailId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderUpdateStatus]    Script Date: 11/27/2014 11:18:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderUpdateStatus]    Script Date: 11/27/2014 11:18:36 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrder中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrder'

set @str = 'update [dbo].[Doc_DocumentOrder] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where OrderId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderUpdate]    Script Date: 11/27/2014 11:18:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderUpdate]    Script Date: 11/27/2014 11:18:33 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderUpdate
// 存储过程功能描述：更新Doc_DocumentOrder
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderUpdate]
    @OrderId int,
@OrderNo varchar(200) = NULL,
@ApplyId int = NULL,
@ContractId int = NULL,
@ContractNo varchar(200) = NULL,
@SubId int = NULL,
@LCId int = NULL,
@LCNo varchar(200) = NULL,
@LCDay int = NULL,
@OrderType int = NULL,
@OrderDate datetime = NULL,
@ApplyCorp int = NULL,
@ApplyDept int = NULL,
@SellerCorp int = NULL,
@BuyerCorp int = NULL,
@BuyerCorpName varchar(200) = NULL,
@BuyerAddress varchar(800) = NULL,
@PaymentStyle int = NULL,
@RecBankId int = NULL,
@DiscountBase varchar(200) = NULL,
@AssetId int = NULL,
@BrandId int = NULL,
@AreaId int = NULL,
@AreaName varchar(400) = NULL,
@BankCode varchar(400) = NULL,
@GrossAmount decimal(18, 4) = NULL,
@NetAmount decimal(18, 4) = NULL,
@UnitId int = NULL,
@Currency int = NULL,
@UnitPrice decimal(18, 4) = NULL,
@InvBala decimal(18, 4) = NULL,
@InvGap decimal(18, 4) = NULL,
@Bundles int = NULL,
@Meno varchar(4000) = NULL,
@OrderStatus int = NULL,
@ApplyEmpId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrder] SET
	[OrderNo] = @OrderNo,
	[ApplyId] = @ApplyId,
	[ContractId] = @ContractId,
	[ContractNo] = @ContractNo,
	[SubId] = @SubId,
	[LCId] = @LCId,
	[LCNo] = @LCNo,
	[LCDay] = @LCDay,
	[OrderType] = @OrderType,
	[OrderDate] = @OrderDate,
	[ApplyCorp] = @ApplyCorp,
	[ApplyDept] = @ApplyDept,
	[SellerCorp] = @SellerCorp,
	[BuyerCorp] = @BuyerCorp,
	[BuyerCorpName] = @BuyerCorpName,
	[BuyerAddress] = @BuyerAddress,
	[PaymentStyle] = @PaymentStyle,
	[RecBankId] = @RecBankId,
	[DiscountBase] = @DiscountBase,
	[AssetId] = @AssetId,
	[BrandId] = @BrandId,
	[AreaId] = @AreaId,
	[AreaName] = @AreaName,
	[BankCode] = @BankCode,
	[GrossAmount] = @GrossAmount,
	[NetAmount] = @NetAmount,
	[UnitId] = @UnitId,
	[Currency] = @Currency,
	[UnitPrice] = @UnitPrice,
	[InvBala] = @InvBala,
	[InvGap] = @InvGap,
	[Bundles] = @Bundles,
	[Meno] = @Meno,
	[OrderStatus] = @OrderStatus,
	[ApplyEmpId] = @ApplyEmpId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[OrderId] = @OrderId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockUpdateStatus]    Script Date: 11/27/2014 11:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderStockUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderStockUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockUpdateStatus]    Script Date: 11/27/2014 11:18:29 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderStockUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderStock中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderStockUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderStock'

set @str = 'update [dbo].[Doc_DocumentOrderStock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockUpdate]    Script Date: 11/27/2014 11:18:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderStockUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderStockUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockUpdate]    Script Date: 11/27/2014 11:18:25 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderStockUpdate
// 存储过程功能描述：更新Doc_DocumentOrderStock
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderStockUpdate]
    @DetailId int,
@OrderId int = NULL,
@StockId int = NULL,
@StockNameId int = NULL,
@RefNo varchar(200) = NULL,
@ApplyAmount decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderStock] SET
	[OrderId] = @OrderId,
	[StockId] = @StockId,
	[StockNameId] = @StockNameId,
	[RefNo] = @RefNo,
	[ApplyAmount] = @ApplyAmount,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockLoad]    Script Date: 11/27/2014 11:18:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderStockLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderStockLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockLoad]    Script Date: 11/27/2014 11:18:20 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderStockLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderStock记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderStockLoad]
AS

SELECT
	[DetailId],
	[OrderId],
	[StockId],
	[StockNameId],
	[RefNo],
	[ApplyAmount],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderStock]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockInsert]    Script Date: 11/27/2014 11:18:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderStockInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderStockInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockInsert]    Script Date: 11/27/2014 11:18:16 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderStockInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderStock记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderStockInsert]
	@OrderId int =NULL ,
	@StockId int =NULL ,
	@StockNameId int =NULL ,
	@RefNo varchar(200) =NULL ,
	@ApplyAmount decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderStock] (
	[OrderId],
	[StockId],
	[StockNameId],
	[RefNo],
	[ApplyAmount],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderId,
	@StockId,
	@StockNameId,
	@RefNo,
	@ApplyAmount,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockGoBack]    Script Date: 11/27/2014 11:18:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderStockGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderStockGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockGoBack]    Script Date: 11/27/2014 11:18:12 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderStockGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderStock，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderStockGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderStock'

set @str = 'update [dbo].[Doc_DocumentOrderStock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockGet]    Script Date: 11/27/2014 11:18:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderStockGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderStockGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderStockGet]    Script Date: 11/27/2014 11:18:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderStockGet
// 存储过程功能描述：查询指定Doc_DocumentOrderStock的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderStockGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[OrderId],
	[StockId],
	[StockNameId],
	[RefNo],
	[ApplyAmount],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderStock]
WHERE
	[DetailId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderLoad]    Script Date: 11/27/2014 11:18:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderLoad]    Script Date: 11/27/2014 11:18:04 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderLoad
// 存储过程功能描述：查询所有Doc_DocumentOrder记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderLoad]
AS

SELECT
	[OrderId],
	[OrderNo],
	[ApplyId],
	[ContractId],
	[ContractNo],
	[SubId],
	[LCId],
	[LCNo],
	[LCDay],
	[OrderType],
	[OrderDate],
	[ApplyCorp],
	[ApplyDept],
	[SellerCorp],
	[BuyerCorp],
	[BuyerCorpName],
	[BuyerAddress],
	[PaymentStyle],
	[RecBankId],
	[DiscountBase],
	[AssetId],
	[BrandId],
	[AreaId],
	[AreaName],
	[BankCode],
	[GrossAmount],
	[NetAmount],
	[UnitId],
	[Currency],
	[UnitPrice],
	[InvBala],
	[InvGap],
	[Bundles],
	[Meno],
	[OrderStatus],
	[ApplyEmpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrder]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceUpdateStatus]    Script Date: 11/27/2014 11:18:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInvoiceUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderInvoiceUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceUpdateStatus]    Script Date: 11/27/2014 11:18:01 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderInvoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderInvoiceUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderInvoice'

set @str = 'update [dbo].[Doc_DocumentOrderInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceUpdate]    Script Date: 11/27/2014 11:17:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInvoiceUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderInvoiceUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceUpdate]    Script Date: 11/27/2014 11:17:57 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceUpdate
// 存储过程功能描述：更新Doc_DocumentOrderInvoice
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderInvoiceUpdate]
    @DetailId int,
@OrderId int = NULL,
@StockDetailId int = NULL,
@InvoiceNo varchar(200) = NULL,
@InvoiceBala decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderInvoice] SET
	[OrderId] = @OrderId,
	[StockDetailId] = @StockDetailId,
	[InvoiceNo] = @InvoiceNo,
	[InvoiceBala] = @InvoiceBala,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceLoad]    Script Date: 11/27/2014 11:17:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInvoiceLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderInvoiceLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceLoad]    Script Date: 11/27/2014 11:17:53 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderInvoiceLoad]
AS

SELECT
	[DetailId],
	[OrderId],
	[StockDetailId],
	[InvoiceNo],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderInvoice]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceInsert]    Script Date: 11/27/2014 11:17:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInvoiceInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderInvoiceInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceInsert]    Script Date: 11/27/2014 11:17:46 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderInvoiceInsert]
	@OrderId int =NULL ,
	@StockDetailId int =NULL ,
	@InvoiceNo varchar(200) =NULL ,
	@InvoiceBala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderInvoice] (
	[OrderId],
	[StockDetailId],
	[InvoiceNo],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderId,
	@StockDetailId,
	@InvoiceNo,
	@InvoiceBala,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceGoBack]    Script Date: 11/27/2014 11:17:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInvoiceGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderInvoiceGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceGoBack]    Script Date: 11/27/2014 11:17:42 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderInvoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderInvoiceGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderInvoice'

set @str = 'update [dbo].[Doc_DocumentOrderInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceGet]    Script Date: 11/27/2014 11:17:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInvoiceGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderInvoiceGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInvoiceGet]    Script Date: 11/27/2014 11:17:38 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceGet
// 存储过程功能描述：查询指定Doc_DocumentOrderInvoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderInvoiceGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[OrderId],
	[StockDetailId],
	[InvoiceNo],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderInvoice]
WHERE
	[DetailId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInsert]    Script Date: 11/27/2014 11:17:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderInsert]    Script Date: 11/27/2014 11:17:34 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInsert
// 存储过程功能描述：新增一条Doc_DocumentOrder记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderInsert]
	@OrderNo varchar(200) =NULL ,
	@ApplyId int =NULL ,
	@ContractId int =NULL ,
	@ContractNo varchar(200) =NULL ,
	@SubId int =NULL ,
	@LCId int =NULL ,
	@LCNo varchar(200) =NULL ,
	@LCDay int =NULL ,
	@OrderType int =NULL ,
	@OrderDate datetime =NULL ,
	@ApplyCorp int =NULL ,
	@ApplyDept int =NULL ,
	@SellerCorp int =NULL ,
	@BuyerCorp int =NULL ,
	@BuyerCorpName varchar(200) =NULL ,
	@BuyerAddress varchar(800) =NULL ,
	@PaymentStyle int =NULL ,
	@RecBankId int =NULL ,
	@DiscountBase varchar(200) =NULL ,
	@AssetId int =NULL ,
	@BrandId int =NULL ,
	@AreaId int =NULL ,
	@AreaName varchar(400) =NULL ,
	@BankCode varchar(400) =NULL ,
	@GrossAmount decimal(18, 4) =NULL ,
	@NetAmount decimal(18, 4) =NULL ,
	@UnitId int =NULL ,
	@Currency int =NULL ,
	@UnitPrice decimal(18, 4) =NULL ,
	@InvBala decimal(18, 4) =NULL ,
	@InvGap decimal(18, 4) =NULL ,
	@Bundles int =NULL ,
	@Meno varchar(4000) =NULL ,
	@OrderStatus int =NULL ,
	@ApplyEmpId int =NULL ,
	@CreatorId int =NULL ,
	@OrderId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrder] (
	[OrderNo],
	[ApplyId],
	[ContractId],
	[ContractNo],
	[SubId],
	[LCId],
	[LCNo],
	[LCDay],
	[OrderType],
	[OrderDate],
	[ApplyCorp],
	[ApplyDept],
	[SellerCorp],
	[BuyerCorp],
	[BuyerCorpName],
	[BuyerAddress],
	[PaymentStyle],
	[RecBankId],
	[DiscountBase],
	[AssetId],
	[BrandId],
	[AreaId],
	[AreaName],
	[BankCode],
	[GrossAmount],
	[NetAmount],
	[UnitId],
	[Currency],
	[UnitPrice],
	[InvBala],
	[InvGap],
	[Bundles],
	[Meno],
	[OrderStatus],
	[ApplyEmpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderNo,
	@ApplyId,
	@ContractId,
	@ContractNo,
	@SubId,
	@LCId,
	@LCNo,
	@LCDay,
	@OrderType,
	@OrderDate,
	@ApplyCorp,
	@ApplyDept,
	@SellerCorp,
	@BuyerCorp,
	@BuyerCorpName,
	@BuyerAddress,
	@PaymentStyle,
	@RecBankId,
	@DiscountBase,
	@AssetId,
	@BrandId,
	@AreaId,
	@AreaName,
	@BankCode,
	@GrossAmount,
	@NetAmount,
	@UnitId,
	@Currency,
	@UnitPrice,
	@InvBala,
	@InvGap,
	@Bundles,
	@Meno,
	@OrderStatus,
	@ApplyEmpId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @OrderId = @@IDENTITY

exec dbo.CreateOrderNo @identity =@OrderId

GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderGoBack]    Script Date: 11/27/2014 11:17:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderGoBack]    Script Date: 11/27/2014 11:17:31 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderGoBack
// 存储过程功能描述：撤返Doc_DocumentOrder，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrder'

set @str = 'update [dbo].[Doc_DocumentOrder] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where OrderId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderGet]    Script Date: 11/27/2014 11:17:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderGet]    Script Date: 11/27/2014 11:17:27 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderGet
// 存储过程功能描述：查询指定Doc_DocumentOrder的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderGet]
    /*
	@OrderId int
    */
    @id int
AS

SELECT
	[OrderId],
	[OrderNo],
	[ApplyId],
	[ContractId],
	[ContractNo],
	[SubId],
	[LCId],
	[LCNo],
	[LCDay],
	[OrderType],
	[OrderDate],
	[ApplyCorp],
	[ApplyDept],
	[SellerCorp],
	[BuyerCorp],
	[BuyerCorpName],
	[BuyerAddress],
	[PaymentStyle],
	[RecBankId],
	[DiscountBase],
	[AssetId],
	[BrandId],
	[AreaId],
	[AreaName],
	[BankCode],
	[GrossAmount],
	[NetAmount],
	[UnitId],
	[Currency],
	[UnitPrice],
	[InvBala],
	[InvGap],
	[Bundles],
	[Meno],
	[OrderStatus],
	[ApplyEmpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrder]
WHERE
	[OrderId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailUpdateStatus]    Script Date: 11/27/2014 11:17:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderDetailUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderDetailUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailUpdateStatus]    Script Date: 11/27/2014 11:17:24 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderDetailUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderDetailUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderDetail'

set @str = 'update [dbo].[Doc_DocumentOrderDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailUpdate]    Script Date: 11/27/2014 11:17:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderDetailUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderDetailUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailUpdate]    Script Date: 11/27/2014 11:17:19 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderDetailUpdate
// 存储过程功能描述：更新Doc_DocumentOrderDetail
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderDetailUpdate]
    @DetailId int,
@OrderId int = NULL,
@InvoiceCopies int = NULL,
@InvoiceSpecific varchar(2000) = NULL,
@QualityCopies int = NULL,
@QualitySpecific varchar(2000) = NULL,
@WeightCopies int = NULL,
@WeightSpecific varchar(2000) = NULL,
@TexCopies int = NULL,
@TexSpecific varchar(2000) = NULL,
@DeliverCopies int = NULL,
@DeliverSpecific varchar(2000) = NULL,
@TotalInvCopies int = NULL,
@TotalInvSpecific varchar(2000) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderDetail] SET
	[OrderId] = @OrderId,
	[InvoiceCopies] = @InvoiceCopies,
	[InvoiceSpecific] = @InvoiceSpecific,
	[QualityCopies] = @QualityCopies,
	[QualitySpecific] = @QualitySpecific,
	[WeightCopies] = @WeightCopies,
	[WeightSpecific] = @WeightSpecific,
	[TexCopies] = @TexCopies,
	[TexSpecific] = @TexSpecific,
	[DeliverCopies] = @DeliverCopies,
	[DeliverSpecific] = @DeliverSpecific,
	[TotalInvCopies] = @TotalInvCopies,
	[TotalInvSpecific] = @TotalInvSpecific,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailLoad]    Script Date: 11/27/2014 11:17:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderDetailLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderDetailLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailLoad]    Script Date: 11/27/2014 11:17:15 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderDetailLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderDetailLoad]
AS

SELECT
	[DetailId],
	[OrderId],
	[InvoiceCopies],
	[InvoiceSpecific],
	[QualityCopies],
	[QualitySpecific],
	[WeightCopies],
	[WeightSpecific],
	[TexCopies],
	[TexSpecific],
	[DeliverCopies],
	[DeliverSpecific],
	[TotalInvCopies],
	[TotalInvSpecific],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderDetail]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailInsert]    Script Date: 11/27/2014 11:17:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderDetailInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderDetailInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailInsert]    Script Date: 11/27/2014 11:17:12 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderDetailInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderDetailInsert]
	@OrderId int =NULL ,
	@InvoiceCopies int =NULL ,
	@InvoiceSpecific varchar(2000) =NULL ,
	@QualityCopies int =NULL ,
	@QualitySpecific varchar(2000) =NULL ,
	@WeightCopies int =NULL ,
	@WeightSpecific varchar(2000) =NULL ,
	@TexCopies int =NULL ,
	@TexSpecific varchar(2000) =NULL ,
	@DeliverCopies int =NULL ,
	@DeliverSpecific varchar(2000) =NULL ,
	@TotalInvCopies int =NULL ,
	@TotalInvSpecific varchar(2000) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderDetail] (
	[OrderId],
	[InvoiceCopies],
	[InvoiceSpecific],
	[QualityCopies],
	[QualitySpecific],
	[WeightCopies],
	[WeightSpecific],
	[TexCopies],
	[TexSpecific],
	[DeliverCopies],
	[DeliverSpecific],
	[TotalInvCopies],
	[TotalInvSpecific],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderId,
	@InvoiceCopies,
	@InvoiceSpecific,
	@QualityCopies,
	@QualitySpecific,
	@WeightCopies,
	@WeightSpecific,
	@TexCopies,
	@TexSpecific,
	@DeliverCopies,
	@DeliverSpecific,
	@TotalInvCopies,
	@TotalInvSpecific,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailGoBack]    Script Date: 11/27/2014 11:17:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderDetailGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderDetailGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailGoBack]    Script Date: 11/27/2014 11:17:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderDetailGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderDetailGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderDetail'

set @str = 'update [dbo].[Doc_DocumentOrderDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailGet]    Script Date: 11/27/2014 11:17:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderDetailGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderDetailGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderDetailGet]    Script Date: 11/27/2014 11:17:04 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderDetailGet
// 存储过程功能描述：查询指定Doc_DocumentOrderDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderDetailGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[OrderId],
	[InvoiceCopies],
	[InvoiceSpecific],
	[QualityCopies],
	[QualitySpecific],
	[WeightCopies],
	[WeightSpecific],
	[TexCopies],
	[TexSpecific],
	[DeliverCopies],
	[DeliverSpecific],
	[TotalInvCopies],
	[TotalInvSpecific],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderDetail]
WHERE
	[DetailId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachUpdateStatus]    Script Date: 11/27/2014 11:16:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderAttachUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderAttachUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachUpdateStatus]    Script Date: 11/27/2014 11:16:59 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderAttachUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderAttachUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderAttach'

set @str = 'update [dbo].[Doc_DocumentOrderAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where OrderAttachId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachUpdate]    Script Date: 11/27/2014 11:16:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderAttachUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderAttachUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachUpdate]    Script Date: 11/27/2014 11:16:56 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderAttachUpdate
// 存储过程功能描述：更新Doc_DocumentOrderAttach
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderAttachUpdate]
    @OrderAttachId int,
@OrderId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderAttach] SET
	[OrderId] = @OrderId,
	[AttachId] = @AttachId
WHERE
	[OrderAttachId] = @OrderAttachId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachLoad]    Script Date: 11/27/2014 11:16:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderAttachLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderAttachLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachLoad]    Script Date: 11/27/2014 11:16:52 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderAttachLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderAttachLoad]
AS

SELECT
	[OrderAttachId],
	[OrderId],
	[AttachId]
FROM
	[dbo].[Doc_DocumentOrderAttach]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachInsert]    Script Date: 11/27/2014 11:16:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderAttachInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderAttachInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachInsert]    Script Date: 11/27/2014 11:16:48 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderAttachInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderAttachInsert]
	@OrderId int =NULL ,
	@AttachId int =NULL ,
	@OrderAttachId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderAttach] (
	[OrderId],
	[AttachId]
) VALUES (
	@OrderId,
	@AttachId
)


SET @OrderAttachId = @@IDENTITY


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachGoBack]    Script Date: 11/27/2014 11:16:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderAttachGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderAttachGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachGoBack]    Script Date: 11/27/2014 11:16:44 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderAttachGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderAttachGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderAttach'

set @str = 'update [dbo].[Doc_DocumentOrderAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where OrderAttachId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachGet]    Script Date: 11/27/2014 11:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderAttachGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentOrderAttachGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentOrderAttachGet]    Script Date: 11/27/2014 11:16:40 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderAttachGet
// 存储过程功能描述：查询指定Doc_DocumentOrderAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentOrderAttachGet]
    /*
	@OrderAttachId int
    */
    @id int
AS

SELECT
	[OrderAttachId],
	[OrderId],
	[AttachId]
FROM
	[dbo].[Doc_DocumentOrderAttach]
WHERE
	[OrderAttachId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentLoad]    Script Date: 11/27/2014 11:16:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentLoad]    Script Date: 11/27/2014 11:16:35 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentLoad
// 存储过程功能描述：查询所有Doc_Document记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentLoad]
AS

SELECT
	[DocumentId],
	[OrderId],
	[DocumentDate],
	[DocEmpId],
	[PresentDate],
	[Presenter],
	[AcceptanceDate],
	[Acceptancer],
	[Meno],
	[DocumentStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_Document]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceUpdateStatus]    Script Date: 11/27/2014 11:16:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInvoiceUpdateStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentInvoiceUpdateStatus]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceUpdateStatus]    Script Date: 11/27/2014 11:16:32 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInvoiceUpdateStatus
// 存储过程功能描述：更新Doc_DocumentInvoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentInvoiceUpdateStatus]
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentInvoice'

set @str = 'update [dbo].[Doc_DocumentInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceUpdate]    Script Date: 11/27/2014 11:16:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInvoiceUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentInvoiceUpdate]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceUpdate]    Script Date: 11/27/2014 11:16:29 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInvoiceUpdate
// 存储过程功能描述：更新Doc_DocumentInvoice
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentInvoiceUpdate]
    @DetailId int,
@DocumentId int = NULL,
@OrderId int = NULL,
@StockDetailId int = NULL,
@OrderInvoiceDetailId int = NULL,
@InvoiceNo varchar(200) = NULL,
@InvoiceId int = NULL,
@InvoiceBala decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentInvoice] SET
	[DocumentId] = @DocumentId,
	[OrderId] = @OrderId,
	[StockDetailId] = @StockDetailId,
	[OrderInvoiceDetailId] = @OrderInvoiceDetailId,
	[InvoiceNo] = @InvoiceNo,
	[InvoiceId] = @InvoiceId,
	[InvoiceBala] = @InvoiceBala,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceLoad]    Script Date: 11/27/2014 11:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInvoiceLoad]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentInvoiceLoad]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceLoad]    Script Date: 11/27/2014 11:16:24 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInvoiceLoad
// 存储过程功能描述：查询所有Doc_DocumentInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentInvoiceLoad]
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[StockDetailId],
	[OrderInvoiceDetailId],
	[InvoiceNo],
	[InvoiceId],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentInvoice]


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceInsert]    Script Date: 11/27/2014 11:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInvoiceInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentInvoiceInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceInsert]    Script Date: 11/27/2014 11:16:20 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInvoiceInsert
// 存储过程功能描述：新增一条Doc_DocumentInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentInvoiceInsert]
	@DocumentId int =NULL ,
	@OrderId int =NULL ,
	@StockDetailId int =NULL ,
	@OrderInvoiceDetailId int =NULL ,
	@InvoiceNo varchar(200) =NULL ,
	@InvoiceId int =NULL ,
	@InvoiceBala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentInvoice] (
	[DocumentId],
	[OrderId],
	[StockDetailId],
	[OrderInvoiceDetailId],
	[InvoiceNo],
	[InvoiceId],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DocumentId,
	@OrderId,
	@StockDetailId,
	@OrderInvoiceDetailId,
	@InvoiceNo,
	@InvoiceId,
	@InvoiceBala,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceGoBack]    Script Date: 11/27/2014 11:16:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInvoiceGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentInvoiceGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceGoBack]    Script Date: 11/27/2014 11:16:16 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInvoiceGoBack
// 存储过程功能描述：撤返Doc_DocumentInvoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentInvoiceGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentInvoice'

set @str = 'update [dbo].[Doc_DocumentInvoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceGet]    Script Date: 11/27/2014 11:16:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInvoiceGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentInvoiceGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInvoiceGet]    Script Date: 11/27/2014 11:16:12 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInvoiceGet
// 存储过程功能描述：查询指定Doc_DocumentInvoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentInvoiceGet]
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[StockDetailId],
	[OrderInvoiceDetailId],
	[InvoiceNo],
	[InvoiceId],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentInvoice]
WHERE
	[DetailId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInsert]    Script Date: 11/27/2014 11:16:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentInsert]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentInsert]    Script Date: 11/27/2014 11:16:09 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInsert
// 存储过程功能描述：新增一条Doc_Document记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentInsert]
	@OrderId int =NULL ,
	@DocumentDate datetime =NULL ,
	@DocEmpId int =NULL ,
	@PresentDate datetime =NULL ,
	@Presenter int =NULL ,
	@AcceptanceDate datetime =NULL ,
	@Acceptancer int =NULL ,
	@Meno varchar(4000) =NULL ,
	@DocumentStatus int =NULL ,
	@CreatorId int =NULL ,
	@DocumentId int OUTPUT
AS

INSERT INTO [dbo].[Doc_Document] (
	[OrderId],
	[DocumentDate],
	[DocEmpId],
	[PresentDate],
	[Presenter],
	[AcceptanceDate],
	[Acceptancer],
	[Meno],
	[DocumentStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderId,
	@DocumentDate,
	@DocEmpId,
	@PresentDate,
	@Presenter,
	@AcceptanceDate,
	@Acceptancer,
	@Meno,
	@DocumentStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DocumentId = @@IDENTITY


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentGoBack]    Script Date: 11/27/2014 11:16:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentGoBack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentGoBack]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentGoBack]    Script Date: 11/27/2014 11:16:05 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentGoBack
// 存储过程功能描述：撤返Doc_Document，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentGoBack]
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_Document'

set @str = 'update [dbo].[Doc_Document] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DocumentId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentGet]    Script Date: 11/27/2014 11:16:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Doc_DocumentGet]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[Doc_DocumentGet]    Script Date: 11/27/2014 11:16:02 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentGet
// 存储过程功能描述：查询指定Doc_Document的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年11月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[Doc_DocumentGet]
    /*
	@DocumentId int
    */
    @id int
AS

SELECT
	[DocumentId],
	[OrderId],
	[DocumentDate],
	[DocEmpId],
	[PresentDate],
	[Presenter],
	[AcceptanceDate],
	[Acceptancer],
	[Meno],
	[DocumentStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_Document]
WHERE
	[DocumentId] = @id


GO


USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[CreateOrderNo]    Script Date: 11/27/2014 11:15:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateOrderNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateOrderNo]
GO

USE [NFMT]
GO

/****** Object:  StoredProcedure [dbo].[CreateOrderNo]    Script Date: 11/27/2014 11:15:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[CreateOrderNo]
(
@identity int
)
as
begin

declare @orderNo varchar(20)

set @orderNo = '批次' + CAST(@identity as varchar)

update NFMT.dbo.Doc_DocumentOrder set OrderNo=@orderNo where OrderId = @identity

end


GO


