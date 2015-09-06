alter table dbo.Fun_PaymentContractDetail
   drop constraint PK_FUN_PAYMENTCONTRACTDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_PaymentContractDetail')
            and   type = 'U')
   drop table dbo.Fun_PaymentContractDetail
go

/*==============================================================*/
/* Table: Fun_PaymentContractDetail                             */
/*==============================================================*/
create table dbo.Fun_PaymentContractDetail (
   DetailId             int                  identity,
   PaymentId            int                  not null,
   ContractId           int                  null,
   ContractSubId        int                  null,
   PayApplyId           int                  null,
   PayApplyDetailId     int                  null,
   PayBala              decimal(18,4)        null,
   FundsBala            decimal(18,4)        null,
   VirtualBala          decimal(18,4)        null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约财务付款明细',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款序号',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail', 'column', 'PaymentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约付款申请明细序号',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail', 'column', 'PayApplyDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail', 'column', 'PayBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail', 'column', 'FundsBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '虚拟付款金额',
   'user', 'dbo', 'table', 'Fun_PaymentContractDetail', 'column', 'VirtualBala'
go

alter table dbo.Fun_PaymentContractDetail
   add constraint PK_FUN_PAYMENTCONTRACTDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Fun_PaymentContractDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentContractDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentContractDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentContractDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentContractDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentContractDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentContractDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentContractDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentContractDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentContractDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentContractDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentContractDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentContractDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentContractDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentContractDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentContractDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentContractDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentContractDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PaymentContractDetailUpdateStatus
// 存储过程功能描述：更新Fun_PaymentContractDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentContractDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentContractDetail'

set @str = 'update [dbo].[Fun_PaymentContractDetail] '+
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
// 存储过程名：[dbo].Fun_PaymentContractDetailGoBack
// 存储过程功能描述：撤返Fun_PaymentContractDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentContractDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PaymentContractDetail'

set @str = 'update [dbo].[Fun_PaymentContractDetail] '+
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
// 存储过程名：[dbo].Fun_PaymentContractDetailGet
// 存储过程功能描述：查询指定Fun_PaymentContractDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentContractDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[PaymentId],
	[ContractId],
	[ContractSubId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala]
FROM
	[dbo].[Fun_PaymentContractDetail]
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
// 存储过程名：[dbo].Fun_PaymentContractDetailLoad
// 存储过程功能描述：查询所有Fun_PaymentContractDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentContractDetailLoad
AS

SELECT
	[DetailId],
	[PaymentId],
	[ContractId],
	[ContractSubId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala]
FROM
	[dbo].[Fun_PaymentContractDetail]

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
// 存储过程名：[dbo].Fun_PaymentContractDetailInsert
// 存储过程功能描述：新增一条Fun_PaymentContractDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentContractDetailInsert
	@PaymentId int ,
	@ContractId int =NULL ,
	@ContractSubId int =NULL ,
	@PayApplyId int =NULL ,
	@PayApplyDetailId int =NULL ,
	@PayBala decimal(18, 4) =NULL ,
	@FundsBala decimal(18, 4) =NULL ,
	@VirtualBala decimal(18, 4) =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Fun_PaymentContractDetail] (
	[PaymentId],
	[ContractId],
	[ContractSubId],
	[PayApplyId],
	[PayApplyDetailId],
	[PayBala],
	[FundsBala],
	[VirtualBala]
) VALUES (
	@PaymentId,
	@ContractId,
	@ContractSubId,
	@PayApplyId,
	@PayApplyDetailId,
	@PayBala,
	@FundsBala,
	@VirtualBala
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
// 存储过程名：[dbo].Fun_PaymentContractDetailUpdate
// 存储过程功能描述：更新Fun_PaymentContractDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentContractDetailUpdate
    @DetailId int,
@PaymentId int,
@ContractId int = NULL,
@ContractSubId int = NULL,
@PayApplyId int = NULL,
@PayApplyDetailId int = NULL,
@PayBala decimal(18, 4) = NULL,
@FundsBala decimal(18, 4) = NULL,
@VirtualBala decimal(18, 4) = NULL
AS

UPDATE [dbo].[Fun_PaymentContractDetail] SET
	[PaymentId] = @PaymentId,
	[ContractId] = @ContractId,
	[ContractSubId] = @ContractSubId,
	[PayApplyId] = @PayApplyId,
	[PayApplyDetailId] = @PayApplyDetailId,
	[PayBala] = @PayBala,
	[FundsBala] = @FundsBala,
	[VirtualBala] = @VirtualBala
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



