alter table dbo.Fun_Payment
   drop constraint PK_FUN_PAYMENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_Payment')
            and   type = 'U')
   drop table dbo.Fun_Payment
go

/*==============================================================*/
/* Table: Fun_Payment                                           */
/*==============================================================*/
create table dbo.Fun_Payment (
   PaymentId            int                  identity,
   PayApplyId           int                  null,
   PaymentCode          varchar(20)          null,
   PayBala              decimal(18,4)        null,
   FundsBala            decimal(18,4)        null,
   VirtualBala          decimal(18,4)        null,
   CurrencyId           int                  null,
   PayStyle             int                  null,
   PayBankId            int                  null,
   PayBankAccountId     int                  null,
   PayCorp              int                  null,
   PayDept              int                  null,
   PayEmpId             int                  null,
   PayDatetime          datetime             null,
   RecevableCorp        int                  null,
   ReceBankId           int                  null,
   ReceBankAccountId    int                  null,
   ReceBankAccount      varchar(200)         null,
   PaymentStatus        int                  null,
   FlowName             varchar(200)         null,
   Memo                 varchar(4000)        null,
   FundsLogId           int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '财务付款',
   'user', 'dbo', 'table', 'Fun_Payment'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款序号',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PaymentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款编号',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PaymentCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款金额',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '财务付款金额',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'FundsBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '虚拟付款金额',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'VirtualBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款币种',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款方式',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayStyle'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款银行',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayBankId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款账户',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayBankAccountId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款公司',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款部门',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayDept'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款操作人',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayEmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款时间',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PayDatetime'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款公司',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'RecevableCorp'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款银行',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'ReceBankId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款银行账户',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'ReceBankAccountId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款账户',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'ReceBankAccount'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款状态',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'PaymentStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '外部流水号',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'FlowName'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金流水序号',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'FundsLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Fun_Payment', 'column', 'LastModifyTime'
go

alter table dbo.Fun_Payment
   add constraint PK_FUN_PAYMENT primary key (PaymentId)
go


/****** Object:  Stored Procedure [dbo].Fun_PaymentGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PaymentUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PaymentGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PaymentGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PaymentUpdateStatus
// 存储过程功能描述：更新Fun_Payment中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_Payment'

set @str = 'update [dbo].[Fun_Payment] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PaymentId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PaymentGoBack
// 存储过程功能描述：撤返Fun_Payment，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_Payment'

set @str = 'update [dbo].[Fun_Payment] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PaymentId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PaymentGet
// 存储过程功能描述：查询指定Fun_Payment的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentGet
    /*
	@PaymentId int
    */
    @id int
AS

SELECT
	[PaymentId],
	[PayApplyId],
	[PaymentCode],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[CurrencyId],
	[PayStyle],
	[PayBankId],
	[PayBankAccountId],
	[PayCorp],
	[PayDept],
	[PayEmpId],
	[PayDatetime],
	[RecevableCorp],
	[ReceBankId],
	[ReceBankAccountId],
	[ReceBankAccount],
	[PaymentStatus],
	[FlowName],
	[Memo],
	[FundsLogId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_Payment]
WHERE
	[PaymentId] = @id

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
// 存储过程名：[dbo].Fun_PaymentLoad
// 存储过程功能描述：查询所有Fun_Payment记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentLoad
AS

SELECT
	[PaymentId],
	[PayApplyId],
	[PaymentCode],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[CurrencyId],
	[PayStyle],
	[PayBankId],
	[PayBankAccountId],
	[PayCorp],
	[PayDept],
	[PayEmpId],
	[PayDatetime],
	[RecevableCorp],
	[ReceBankId],
	[ReceBankAccountId],
	[ReceBankAccount],
	[PaymentStatus],
	[FlowName],
	[Memo],
	[FundsLogId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_Payment]

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[CreatePaymentCode]
(
@identity int
)
as
begin

declare @paymentCode varchar(20)

set @paymentCode = 'Payment' + CAST(@identity as varchar)

update dbo.Fun_Payment set PaymentCode=@paymentCode where PaymentId = @identity

end


GO




SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PaymentInsert
// 存储过程功能描述：新增一条Fun_Payment记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentInsert
	@PayApplyId int =NULL ,
	@PaymentCode varchar(20) =NULL ,
	@PayBala decimal(18, 4) =NULL ,
	@FundsBala decimal(18, 4) =NULL ,
	@VirtualBala decimal(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@PayStyle int =NULL ,
	@PayBankId int =NULL ,
	@PayBankAccountId int =NULL ,
	@PayCorp int =NULL ,
	@PayDept int =NULL ,
	@PayEmpId int =NULL ,
	@PayDatetime datetime =NULL ,
	@RecevableCorp int =NULL ,
	@ReceBankId int =NULL ,
	@ReceBankAccountId int =NULL ,
	@ReceBankAccount varchar(200) =NULL ,
	@PaymentStatus int =NULL ,
	@FlowName varchar(200) =NULL ,
	@Memo varchar(4000) =NULL ,
	@FundsLogId int =NULL ,
	@CreatorId int =NULL ,
	@PaymentId int OUTPUT
AS

INSERT INTO [dbo].[Fun_Payment] (
	[PayApplyId],
	[PaymentCode],
	[PayBala],
	[FundsBala],
	[VirtualBala],
	[CurrencyId],
	[PayStyle],
	[PayBankId],
	[PayBankAccountId],
	[PayCorp],
	[PayDept],
	[PayEmpId],
	[PayDatetime],
	[RecevableCorp],
	[ReceBankId],
	[ReceBankAccountId],
	[ReceBankAccount],
	[PaymentStatus],
	[FlowName],
	[Memo],
	[FundsLogId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PayApplyId,
	@PaymentCode,
	@PayBala,
	@FundsBala,
	@VirtualBala,
	@CurrencyId,
	@PayStyle,
	@PayBankId,
	@PayBankAccountId,
	@PayCorp,
	@PayDept,
	@PayEmpId,
	@PayDatetime,
	@RecevableCorp,
	@ReceBankId,
	@ReceBankAccountId,
	@ReceBankAccount,
	@PaymentStatus,
	@FlowName,
	@Memo,
	@FundsLogId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PaymentId = @@IDENTITY

Exec dbo.CreatePaymentCode @PaymentId

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
// 存储过程名：[dbo].Fun_PaymentUpdate
// 存储过程功能描述：更新Fun_Payment
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PaymentUpdate
    @PaymentId int,
@PayApplyId int = NULL,
@PaymentCode varchar(20) = NULL,
@PayBala decimal(18, 4) = NULL,
@FundsBala decimal(18, 4) = NULL,
@VirtualBala decimal(18, 4) = NULL,
@CurrencyId int = NULL,
@PayStyle int = NULL,
@PayBankId int = NULL,
@PayBankAccountId int = NULL,
@PayCorp int = NULL,
@PayDept int = NULL,
@PayEmpId int = NULL,
@PayDatetime datetime = NULL,
@RecevableCorp int = NULL,
@ReceBankId int = NULL,
@ReceBankAccountId int = NULL,
@ReceBankAccount varchar(200) = NULL,
@PaymentStatus int = NULL,
@FlowName varchar(200) = NULL,
@Memo varchar(4000) = NULL,
@FundsLogId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Fun_Payment] SET
	[PayApplyId] = @PayApplyId,
	[PaymentCode] = @PaymentCode,
	[PayBala] = @PayBala,
	[FundsBala] = @FundsBala,
	[VirtualBala] = @VirtualBala,
	[CurrencyId] = @CurrencyId,
	[PayStyle] = @PayStyle,
	[PayBankId] = @PayBankId,
	[PayBankAccountId] = @PayBankAccountId,
	[PayCorp] = @PayCorp,
	[PayDept] = @PayDept,
	[PayEmpId] = @PayEmpId,
	[PayDatetime] = @PayDatetime,
	[RecevableCorp] = @RecevableCorp,
	[ReceBankId] = @ReceBankId,
	[ReceBankAccountId] = @ReceBankAccountId,
	[ReceBankAccount] = @ReceBankAccount,
	[PaymentStatus] = @PaymentStatus,
	[FlowName] = @FlowName,
	[Memo] = @Memo,
	[FundsLogId] = @FundsLogId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PaymentId] = @PaymentId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



