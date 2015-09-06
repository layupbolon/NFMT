alter table dbo.Fun_PaymentStockDetail
   drop constraint PK_FUN_PAYMENTSTOCKDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_PaymentStockDetail')
            and   type = 'U')
   drop table dbo.Fun_PaymentStockDetail
go

/*==============================================================*/
/* Table: Fun_PaymentStockDetail                                */
/*==============================================================*/
create table dbo.Fun_PaymentStockDetail (
   DetailId             int                  identity,
   ContractDetailId     int                  null,
   PaymentId            int                  not null,
   StockId              int                  null,
   StockLogId           int                  null,
   ContractId           int                  null,
   SubId                int                  null,
   PayApplyId           int                  null,
   PayApplyDetailId     int                  null,
   PayBala              decimal(18,4)        null,
   FundsBala            decimal(18,4)        null,
   VirtualBala          decimal(18,4)        null,
   SourceFrom           int                  null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '库存财务付款明细',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约付款序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'ContractDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'PaymentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'StockLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约付款申请明细序号',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'PayApplyDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'PayBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'FundsBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '虚拟付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'VirtualBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '数据来源',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'SourceFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Fun_PaymentStockDetail', 'column', 'DetailStatus'
go

alter table dbo.Fun_PaymentStockDetail
   add constraint PK_FUN_PAYMENTSTOCKDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Fun_PaymentStockDetailGet    Script Date: 2015年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentStockDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentStockDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentStockDetailLoad    Script Date: 2015年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentStockDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentStockDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentStockDetailInsert    Script Date: 2015年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentStockDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentStockDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentStockDetailUpdate    Script Date: 2015年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentStockDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentStockDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentStockDetailUpdateStatus    Script Date: 2015年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentStockDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentStockDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentStockDetailUpdateStatus    Script Date: 2015年3月10日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentStockDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentStockDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PaymentStockDetailUpdateStatus
// 存储过程功能描述：更新Fun_PaymentStockDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月10日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentStockDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentStockDetail'

set @str = 'update [dbo].[Fun_PaymentStockDetail] '+
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
// 存储过程名：[dbo].Fun_PaymentStockDetailGoBack
// 存储过程功能描述：撤返Fun_PaymentStockDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月10日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentStockDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentStockDetail'

set @str = 'update [dbo].[Fun_PaymentStockDetail] '+
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
// 存储过程名：[dbo].Fun_PaymentStockDetailGet
// 存储过程功能描述：查询指定Fun_PaymentStockDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月10日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentStockDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ContractDetailId],
	[PaymentId],
	[StockId],
	[StockLogId],
	[ContractId],
	[SubId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[SourceFrom],
	[DetailStatus]
FROM
	[dbo].[Fun_PaymentStockDetail]
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
// 存储过程名：[dbo].Fun_PaymentStockDetailLoad
// 存储过程功能描述：查询所有Fun_PaymentStockDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月10日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentStockDetailLoad
AS

SELECT
	[DetailId],
	[ContractDetailId],
	[PaymentId],
	[StockId],
	[StockLogId],
	[ContractId],
	[SubId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[SourceFrom],
	[DetailStatus]
FROM
	[dbo].[Fun_PaymentStockDetail]

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
// 存储过程名：[dbo].Fun_PaymentStockDetailInsert
// 存储过程功能描述：新增一条Fun_PaymentStockDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月10日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentStockDetailInsert
	@ContractDetailId int =NULL ,
	@PaymentId int ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@ContractId int =NULL ,
	@SubId int =NULL ,
	@PayApplyId int =NULL ,
	@PayApplyDetailId int =NULL ,
	@PayBala decimal(18, 4) =NULL ,
	@FundsBala decimal(18, 4) =NULL ,
	@VirtualBala decimal(18, 4) =NULL ,
	@SourceFrom int =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Fun_PaymentStockDetail] (
	[ContractDetailId],
	[PaymentId],
	[StockId],
	[StockLogId],
	[ContractId],
	[SubId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[SourceFrom],
	[DetailStatus]
) VALUES (
	@ContractDetailId,
	@PaymentId,
	@StockId,
	@StockLogId,
	@ContractId,
	@SubId,
	@PayApplyId,
	@PayApplyDetailId,
	@PayBala,
	@FundsBala,
	@VirtualBala,
	@SourceFrom,
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
// 存储过程名：[dbo].Fun_PaymentStockDetailUpdate
// 存储过程功能描述：更新Fun_PaymentStockDetail
// 创建人：CodeSmith
// 创建时间： 2015年3月10日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentStockDetailUpdate
    @DetailId int,
@ContractDetailId int = NULL,
@PaymentId int,
@StockId int = NULL,
@StockLogId int = NULL,
@ContractId int = NULL,
@SubId int = NULL,
@PayApplyId int = NULL,
@PayApplyDetailId int = NULL,
@PayBala decimal(18, 4) = NULL,
@FundsBala decimal(18, 4) = NULL,
@VirtualBala decimal(18, 4) = NULL,
@SourceFrom int = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Fun_PaymentStockDetail] SET
	[ContractDetailId] = @ContractDetailId,
	[PaymentId] = @PaymentId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[ContractId] = @ContractId,
	[SubId] = @SubId,
	[PayApplyId] = @PayApplyId,
	[PayApplyDetailId] = @PayApplyDetailId,
	[PayBala] = @PayBala,
	[FundsBala] = @FundsBala,
	[VirtualBala] = @VirtualBala,
	[SourceFrom] = @SourceFrom,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



