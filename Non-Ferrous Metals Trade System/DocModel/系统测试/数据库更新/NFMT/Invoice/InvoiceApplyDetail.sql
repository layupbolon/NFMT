alter table Inv_InvoiceApplyDetail
   drop constraint PK_INV_INVOICEAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Inv_InvoiceApplyDetail')
            and   type = 'U')
   drop table Inv_InvoiceApplyDetail
go

/*==============================================================*/
/* Table: Inv_InvoiceApplyDetail                                */
/*==============================================================*/
create table Inv_InvoiceApplyDetail (
   DetailId             int                  not null,
   InvoiceApplyId       int                  null,
   ApplyId              int                  null,
   InvoiceId            int                  null,
   BussinessInvoiceId   int                  null,
   ContractId           int                  null,
   SubContractId        int                  null,
   StockLogId           int                  null,
   InvoicePrice         numeric(18,4)        null,
   PaymentAmount        numeric(18,4)        null,
   InterestAmount       numeric(18,4)        null,
   OtherAmount          numeric(18,4)        null,
   InvoiceBala          numeric(18,4)        null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '开票申请明细',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开票申请序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开票申请序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'InvoiceApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'ApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主表发票序号.',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'InvoiceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '业务发票序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'BussinessInvoiceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'SubContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开票价格',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'InvoicePrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息金额',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'InterestAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他金额',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'OtherAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开票金额',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'InvoiceBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApplyDetail', 'column', 'LastModifyTime'
go

alter table Inv_InvoiceApplyDetail
   add constraint PK_INV_INVOICEAPPLYDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyDetailGet    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyDetailLoad    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyDetailInsert    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyDetailUpdate    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyDetailUpdateStatus    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyDetailUpdateStatus    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_InvoiceApplyDetailUpdateStatus
// 存储过程功能描述：更新Inv_InvoiceApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_InvoiceApplyDetail'

set @str = 'update [dbo].[Inv_InvoiceApplyDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
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
// 存储过程名：[dbo].Inv_InvoiceApplyDetailGoBack
// 存储过程功能描述：撤返Inv_InvoiceApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_InvoiceApplyDetail'

set @str = 'update [dbo].[Inv_InvoiceApplyDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
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
// 存储过程名：[dbo].Inv_InvoiceApplyDetailGet
// 存储过程功能描述：查询指定Inv_InvoiceApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[InvoiceApplyId],
	[ApplyId],
	[InvoiceId],
	[BussinessInvoiceId],
	[ContractId],
	[SubContractId],
	[StockLogId],
	[InvoicePrice],
	[PaymentAmount],
	[InterestAmount],
	[OtherAmount],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_InvoiceApplyDetail]
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
// 存储过程名：[dbo].Inv_InvoiceApplyDetailLoad
// 存储过程功能描述：查询所有Inv_InvoiceApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyDetailLoad
AS

SELECT
	[DetailId],
	[InvoiceApplyId],
	[ApplyId],
	[InvoiceId],
	[BussinessInvoiceId],
	[ContractId],
	[SubContractId],
	[StockLogId],
	[InvoicePrice],
	[PaymentAmount],
	[InterestAmount],
	[OtherAmount],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_InvoiceApplyDetail]

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
// 存储过程名：[dbo].Inv_InvoiceApplyDetailInsert
// 存储过程功能描述：新增一条Inv_InvoiceApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyDetailInsert
	@InvoiceApplyId int =NULL ,
	@ApplyId int =NULL ,
	@InvoiceId int =NULL ,
	@BussinessInvoiceId int =NULL ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@StockLogId int =NULL ,
	@InvoicePrice numeric(18, 4) =NULL ,
	@PaymentAmount numeric(18, 4) =NULL ,
	@InterestAmount numeric(18, 4) =NULL ,
	@OtherAmount numeric(18, 4) =NULL ,
	@InvoiceBala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Inv_InvoiceApplyDetail] (
	[InvoiceApplyId],
	[ApplyId],
	[InvoiceId],
	[BussinessInvoiceId],
	[ContractId],
	[SubContractId],
	[StockLogId],
	[InvoicePrice],
	[PaymentAmount],
	[InterestAmount],
	[OtherAmount],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@InvoiceApplyId,
	@ApplyId,
	@InvoiceId,
	@BussinessInvoiceId,
	@ContractId,
	@SubContractId,
	@StockLogId,
	@InvoicePrice,
	@PaymentAmount,
	@InterestAmount,
	@OtherAmount,
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
// 存储过程名：[dbo].Inv_InvoiceApplyDetailUpdate
// 存储过程功能描述：更新Inv_InvoiceApplyDetail
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyDetailUpdate
    @DetailId int,
@InvoiceApplyId int = NULL,
@ApplyId int = NULL,
@InvoiceId int = NULL,
@BussinessInvoiceId int = NULL,
@ContractId int = NULL,
@SubContractId int = NULL,
@StockLogId int = NULL,
@InvoicePrice numeric(18, 4) = NULL,
@PaymentAmount numeric(18, 4) = NULL,
@InterestAmount numeric(18, 4) = NULL,
@OtherAmount numeric(18, 4) = NULL,
@InvoiceBala numeric(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Inv_InvoiceApplyDetail] SET
	[InvoiceApplyId] = @InvoiceApplyId,
	[ApplyId] = @ApplyId,
	[InvoiceId] = @InvoiceId,
	[BussinessInvoiceId] = @BussinessInvoiceId,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[StockLogId] = @StockLogId,
	[InvoicePrice] = @InvoicePrice,
	[PaymentAmount] = @PaymentAmount,
	[InterestAmount] = @InterestAmount,
	[OtherAmount] = @OtherAmount,
	[InvoiceBala] = @InvoiceBala,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



