alter table dbo.Fun_CashIn
   drop constraint PK_FUN_CASHIN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_CashIn')
            and   type = 'U')
   drop table dbo.Fun_CashIn
go

/*==============================================================*/
/* Table: Fun_CashIn                                            */
/*==============================================================*/
create table dbo.Fun_CashIn (
   CashInId             int                  identity,
   CashInEmpId          int                  null,
   CashInDate           datetime             null,
   CashInBlocId         int                  null,
   CashInCorpId         int                  null,
   CurrencyId           int                  null,
   CashInBala           numeric(18,4)        null,
   CashInBank           int                  null,
   CashInAccoontId      int                  null,
   PayBlocId            int                  null,
   PayCorpId            int                  null,
   PayCorpName          varchar(50)          null,
   PayBankId            int                  null,
   PayBank              varchar(50)          null,
   PayAccountId         int                  null,
   PayAccount           varchar(50)          null,
   PayWord              varchar(400)         null,
   BankLog              varchar(4000)        null,
   CashInStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '收款',
   'user', 'dbo', 'table', 'Fun_CashIn'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款登记人',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInEmpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款日期',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款集团序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInBlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款公司序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款币种',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款金额',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款银行',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInBank'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款银行账户序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInAccoontId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款集团序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayBlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款公司序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款公司名称',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayCorpName'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款银行序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayBankId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款银行',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayBank'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款银行账户序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayAccountId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款银行账户',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayAccount'
go

execute sp_addextendedproperty 'MS_Description', 
   '简短附言',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'PayWord'
go

execute sp_addextendedproperty 'MS_Description', 
   '外部流水备注',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'BankLog'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款状态',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CashInStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Fun_CashIn', 'column', 'LastModifyTime'
go

alter table dbo.Fun_CashIn
   add constraint PK_FUN_CASHIN primary key (CashInId)
go


/****** Object:  Stored Procedure [dbo].Fun_CashInGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_CashInUpdateStatus
// 存储过程功能描述：更新Fun_CashIn中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashIn'

set @str = 'update [dbo].[Fun_CashIn] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CashInId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_CashInGoBack
// 存储过程功能描述：撤返Fun_CashIn，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashIn'

set @str = 'update [dbo].[Fun_CashIn] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where CashInId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_CashInGet
// 存储过程功能描述：查询指定Fun_CashIn的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInGet
    /*
	@CashInId int
    */
    @id int
AS

SELECT
	[CashInId],
	[CashInEmpId],
	[CashInDate],
	[CashInBlocId],
	[CashInCorpId],
	[CurrencyId],
	[CashInBala],
	[CashInBank],
	[CashInAccoontId],
	[PayBlocId],
	[PayCorpId],
	[PayCorpName],
	[PayBankId],
	[PayBank],
	[PayAccountId],
	[PayAccount],
	[PayWord],
	[BankLog],
	[CashInStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_CashIn]
WHERE
	[CashInId] = @id

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
// 存储过程名：[dbo].Fun_CashInLoad
// 存储过程功能描述：查询所有Fun_CashIn记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInLoad
AS

SELECT
	[CashInId],
	[CashInEmpId],
	[CashInDate],
	[CashInBlocId],
	[CashInCorpId],
	[CurrencyId],
	[CashInBala],
	[CashInBank],
	[CashInAccoontId],
	[PayBlocId],
	[PayCorpId],
	[PayCorpName],
	[PayBankId],
	[PayBank],
	[PayAccountId],
	[PayAccount],
	[PayWord],
	[BankLog],
	[CashInStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_CashIn]

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
// 存储过程名：[dbo].Fun_CashInInsert
// 存储过程功能描述：新增一条Fun_CashIn记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInInsert
	@CashInEmpId int =NULL ,
	@CashInDate datetime =NULL ,
	@CashInBlocId int =NULL ,
	@CashInCorpId int =NULL ,
	@CurrencyId int =NULL ,
	@CashInBala numeric(18, 4) =NULL ,
	@CashInBank int =NULL ,
	@CashInAccoontId int =NULL ,
	@PayBlocId int =NULL ,
	@PayCorpId int =NULL ,
	@PayCorpName varchar(50) =NULL ,
	@PayBankId int =NULL ,
	@PayBank varchar(50) =NULL ,
	@PayAccountId int =NULL ,
	@PayAccount varchar(50) =NULL ,
	@PayWord varchar(400) =NULL ,
	@BankLog varchar(4000) =NULL ,
	@CashInStatus int =NULL ,
	@CreatorId int =NULL ,
	@CashInId int OUTPUT
AS

INSERT INTO [dbo].[Fun_CashIn] (
	[CashInEmpId],
	[CashInDate],
	[CashInBlocId],
	[CashInCorpId],
	[CurrencyId],
	[CashInBala],
	[CashInBank],
	[CashInAccoontId],
	[PayBlocId],
	[PayCorpId],
	[PayCorpName],
	[PayBankId],
	[PayBank],
	[PayAccountId],
	[PayAccount],
	[PayWord],
	[BankLog],
	[CashInStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CashInEmpId,
	@CashInDate,
	@CashInBlocId,
	@CashInCorpId,
	@CurrencyId,
	@CashInBala,
	@CashInBank,
	@CashInAccoontId,
	@PayBlocId,
	@PayCorpId,
	@PayCorpName,
	@PayBankId,
	@PayBank,
	@PayAccountId,
	@PayAccount,
	@PayWord,
	@BankLog,
	@CashInStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @CashInId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_CashInUpdate
// 存储过程功能描述：更新Fun_CashIn
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInUpdate
    @CashInId int,
@CashInEmpId int = NULL,
@CashInDate datetime = NULL,
@CashInBlocId int = NULL,
@CashInCorpId int = NULL,
@CurrencyId int = NULL,
@CashInBala numeric(18, 4) = NULL,
@CashInBank int = NULL,
@CashInAccoontId int = NULL,
@PayBlocId int = NULL,
@PayCorpId int = NULL,
@PayCorpName varchar(50) = NULL,
@PayBankId int = NULL,
@PayBank varchar(50) = NULL,
@PayAccountId int = NULL,
@PayAccount varchar(50) = NULL,
@PayWord varchar(400) = NULL,
@BankLog varchar(4000) = NULL,
@CashInStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Fun_CashIn] SET
	[CashInEmpId] = @CashInEmpId,
	[CashInDate] = @CashInDate,
	[CashInBlocId] = @CashInBlocId,
	[CashInCorpId] = @CashInCorpId,
	[CurrencyId] = @CurrencyId,
	[CashInBala] = @CashInBala,
	[CashInBank] = @CashInBank,
	[CashInAccoontId] = @CashInAccoontId,
	[PayBlocId] = @PayBlocId,
	[PayCorpId] = @PayCorpId,
	[PayCorpName] = @PayCorpName,
	[PayBankId] = @PayBankId,
	[PayBank] = @PayBank,
	[PayAccountId] = @PayAccountId,
	[PayAccount] = @PayAccount,
	[PayWord] = @PayWord,
	[BankLog] = @BankLog,
	[CashInStatus] = @CashInStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[CashInId] = @CashInId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



