alter table dbo.Fun_PaymentInvioceDetail
   drop constraint PK_FUN_PAYMENTINVIOCEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_PaymentInvioceDetail')
            and   type = 'U')
   drop table dbo.Fun_PaymentInvioceDetail
go

/*==============================================================*/
/* Table: Fun_PaymentInvioceDetail                              */
/*==============================================================*/
create table dbo.Fun_PaymentInvioceDetail (
   DetailId             int                  identity,
   PaymentId            int                  not null,
   InvoiceId            int                  null,
   PayApplyId           int                  null,
   PayApplyDetailId     int                  null,
   PayBala              numeric(18,4)        null,
   FundsBala            decimal(18,4)        null,
   VirtualBala          decimal(18,4)        null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '发票财务付款明细',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款序号',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'PaymentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约付款申请明细序号',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'PayApplyDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'PayBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'FundsBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '虚拟付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'VirtualBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Fun_PaymentInvioceDetail', 'column', 'DetailStatus'
go

alter table dbo.Fun_PaymentInvioceDetail
   add constraint PK_FUN_PAYMENTINVIOCEDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Fun_PaymentInvioceDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentInvioceDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentInvioceDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentInvioceDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentInvioceDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentInvioceDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentInvioceDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentInvioceDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentInvioceDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentInvioceDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentInvioceDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentInvioceDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentInvioceDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentInvioceDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentInvioceDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentInvioceDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentInvioceDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentInvioceDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PaymentInvioceDetailUpdateStatus
// 存储过程功能描述：更新Fun_PaymentInvioceDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentInvioceDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentInvioceDetail'

set @str = 'update [dbo].[Fun_PaymentInvioceDetail] '+
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
// 存储过程名：[dbo].Fun_PaymentInvioceDetailGoBack
// 存储过程功能描述：撤返Fun_PaymentInvioceDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentInvioceDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentInvioceDetail'

set @str = 'update [dbo].[Fun_PaymentInvioceDetail] '+
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
// 存储过程名：[dbo].Fun_PaymentInvioceDetailGet
// 存储过程功能描述：查询指定Fun_PaymentInvioceDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentInvioceDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[PaymentId],
	[InvoiceId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[DetailStatus]
FROM
	[dbo].[Fun_PaymentInvioceDetail]
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
// 存储过程名：[dbo].Fun_PaymentInvioceDetailLoad
// 存储过程功能描述：查询所有Fun_PaymentInvioceDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentInvioceDetailLoad
AS

SELECT
	[DetailId],
	[PaymentId],
	[InvoiceId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[DetailStatus]
FROM
	[dbo].[Fun_PaymentInvioceDetail]

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
// 存储过程名：[dbo].Fun_PaymentInvioceDetailInsert
// 存储过程功能描述：新增一条Fun_PaymentInvioceDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentInvioceDetailInsert
	@PaymentId int ,
	@InvoiceId int =NULL ,
	@PayApplyId int =NULL ,
	@PayApplyDetailId int =NULL ,
	@PayBala numeric(18, 4) =NULL ,
	@FundsBala decimal(18, 4) =NULL ,
	@VirtualBala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Fun_PaymentInvioceDetail] (
	[PaymentId],
	[InvoiceId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[DetailStatus]
) VALUES (
	@PaymentId,
	@InvoiceId,
	@PayApplyId,
	@PayApplyDetailId,
	@PayBala,
	@FundsBala,
	@VirtualBala,
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
// 存储过程名：[dbo].Fun_PaymentInvioceDetailUpdate
// 存储过程功能描述：更新Fun_PaymentInvioceDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentInvioceDetailUpdate
    @DetailId int,
@PaymentId int,
@InvoiceId int = NULL,
@PayApplyId int = NULL,
@PayApplyDetailId int = NULL,
@PayBala numeric(18, 4) = NULL,
@FundsBala decimal(18, 4) = NULL,
@VirtualBala decimal(18, 4) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Fun_PaymentInvioceDetail] SET
	[PaymentId] = @PaymentId,
	[InvoiceId] = @InvoiceId,
	[PayApplyId] = @PayApplyId,
	[PayApplyDetailId] = @PayApplyDetailId,
	[PayBala] = @PayBala,
	[FundsBala] = @FundsBala,
	[VirtualBala] = @VirtualBala,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



