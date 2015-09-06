alter table dbo.Doc_DocumentOrder
   drop constraint PK_DOC_DOCUMENTORDER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_DocumentOrder')
            and   type = 'U')
   drop table dbo.Doc_DocumentOrder
go

/*==============================================================*/
/* Table: Doc_DocumentOrder                                     */
/*==============================================================*/
create table dbo.Doc_DocumentOrder (
   OrderId              int                  identity,
   OrderNo              varchar(200)         null,
   CommercialId         int                  null,
   ContractId           int                  null,
   ContractNo           varchar(200)         null,
   SubId                int                  null,
   LCId                 int                  null,
   LCNo                 varchar(200)         null,
   LCDay                int                  null,
   OrderType            int                  null,
   OrderDate            datetime             null,
   ApplyCorp            int                  null,
   ApplyDept            int                  null,
   SellerCorp           int                  null,
   BuyerCorp            int                  null,
   BuyerCorpName        varchar(200)         null,
   BuyerAddress         varchar(800)         null,
   PaymentStyle         int                  null,
   RecBankId            int                  null,
   DiscountBase         varchar(200)         null,
   AssetId              int                  null,
   BrandId              int                  null,
   AreaId               int                  null,
   AreaName             varchar(400)         null,
   BankCode             varchar(400)         null,
   GrossAmount          decimal(18,4)        null,
   NetAmount            decimal(18,4)        null,
   UnitId               int                  null,
   Currency             int                  null,
   UnitPrice            decimal(18,4)        null,
   InvBala              decimal(18,4)        null,
   InvGap               decimal(18,4)        null,
   Bundles              int                  null,
   Meno                 varchar(4000)        null,
   OrderStatus          int                  null,
   ApplyEmpId           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单指令',
   'user', 'dbo', 'table', 'Doc_DocumentOrder'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单指令序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '批次号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'OrderNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '临票指令序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'CommercialId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'ContractNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   'LC序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'LCId'
go

execute sp_addextendedproperty 'MS_Description', 
   'LC编号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'LCNo'
go

execute sp_addextendedproperty 'MS_Description', 
   'LC天数',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'LCDay'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单指令类型',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'OrderType'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令日期',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'OrderDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请公司',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'ApplyCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请部门',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'ApplyDept'
go

execute sp_addextendedproperty 'MS_Description', 
   '我方公司',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'SellerCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '买家公司',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'BuyerCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '买家公司名称',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'BuyerCorpName'
go

execute sp_addextendedproperty 'MS_Description', 
   '买家地址',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'BuyerAddress'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款方式',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'PaymentStyle'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款银行',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'RecBankId'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格条款',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'DiscountBase'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'BrandId'
go

execute sp_addextendedproperty 'MS_Description', 
   '产地',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'AreaId'
go

execute sp_addextendedproperty 'MS_Description', 
   '原产地',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'AreaName'
go

execute sp_addextendedproperty 'MS_Description', 
   '银行编写',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'BankCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '毛重',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'GrossAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '净重',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'UnitId'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'Currency'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'UnitPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票总价',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'InvBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '差额',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'InvGap'
go

execute sp_addextendedproperty 'MS_Description', 
   '捆数',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'Bundles'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令状态',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'OrderStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请人',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'ApplyEmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrder', 'column', 'LastModifyTime'
go

alter table dbo.Doc_DocumentOrder
   add constraint PK_DOC_DOCUMENTORDER primary key (OrderId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrder中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderUpdateStatus
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
// 存储过程名：[dbo].Doc_DocumentOrderGoBack
// 存储过程功能描述：撤返Doc_DocumentOrder，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderGoBack
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
// 存储过程名：[dbo].Doc_DocumentOrderGet
// 存储过程功能描述：查询指定Doc_DocumentOrder的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderGet
    /*
	@OrderId int
    */
    @id int
AS

SELECT
	[OrderId],
	[OrderNo],
	[CommercialId],
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
// 存储过程名：[dbo].Doc_DocumentOrderLoad
// 存储过程功能描述：查询所有Doc_DocumentOrder记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderLoad
AS

SELECT
	[OrderId],
	[OrderNo],
	[CommercialId],
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
// 存储过程名：[dbo].Doc_DocumentOrderInsert
// 存储过程功能描述：新增一条Doc_DocumentOrder记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderInsert
	@OrderNo varchar(200) =NULL ,
	@CommercialId int =NULL ,
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
	[CommercialId],
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
	@CommercialId,
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
// 存储过程名：[dbo].Doc_DocumentOrderUpdate
// 存储过程功能描述：更新Doc_DocumentOrder
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderUpdate
    @OrderId int,
@OrderNo varchar(200) = NULL,
@CommercialId int = NULL,
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
	[CommercialId] = @CommercialId,
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

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



