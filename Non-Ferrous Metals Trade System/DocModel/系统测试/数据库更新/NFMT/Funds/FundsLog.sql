alter table dbo.Fun_FundsLog
   drop constraint PK_FUN_FUNDSLOG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_FundsLog')
            and   type = 'U')
   drop table dbo.Fun_FundsLog
go

/*==============================================================*/
/* Table: Fun_FundsLog                                          */
/*==============================================================*/
create table dbo.Fun_FundsLog (
   FundsLogId           int                  identity,
   ContractId           int                  null,
   SubId                int                  null,
   InvoiceId            int                  null,
   LogDate              datetime             null,
   InBlocId             int                  null,
   InCorpId             int                  null,
   InBankId             int                  null,
   InAccountId          int                  null,
   OutBlocId            int                  null,
   OutCorpId            int                  null,
   OutBankId            int                  null,
   OutBank              varchar(200)         null,
   OutAccountId         int                  null,
   OutAccount           varchar(200)         null,
   FundsBala            decimal(18,4)        null,
   FundsType            int                  null,
   CurrencyId           int                  null,
   LogDirection         int                  null,
   LogType              int                  not null default -1,
   PayMode              int                  null,
   IsVirtualPay         bit                  null,
   FundsDesc            varchar(800)         null,
   OpPerson             int                  null,
   LogSourceBase        varchar(50)          null,
   LogSource            varchar(200)         null,
   SourceId             int                  null,
   LogStatus            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '资金流水',
   'user', 'dbo', 'table', 'Fun_FundsLog'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金流水序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'FundsLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水日期',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LogDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '集团序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'InBlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'InCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '我方银行账户',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'InAccountId'
go

execute sp_addextendedproperty 'MS_Description', 
   '对方集团',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'OutBlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '对方公司',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'OutCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '对方银行序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'OutBankId'
go

execute sp_addextendedproperty 'MS_Description', 
   '对方银行',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'OutBank'
go

execute sp_addextendedproperty 'MS_Description', 
   '对方银行账户',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'OutAccount'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金数量',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'FundsBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '资金类型',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'FundsType'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水方向',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LogDirection'
go

execute sp_addextendedproperty 'MS_Description', 
   '操作类型/收款，收款冲销，付款，付款冲销',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LogType'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款方式',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'PayMode'
go

execute sp_addextendedproperty 'MS_Description', 
   '是否虚拟付款',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'IsVirtualPay'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'FundsDesc'
go

execute sp_addextendedproperty 'MS_Description', 
   '操作人',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'OpPerson'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水来源库名',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LogSourceBase'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水来源/表名记录',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LogSource'
go

execute sp_addextendedproperty 'MS_Description', 
   '来源编号/表序号记录',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'SourceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水状态',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LogStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Fun_FundsLog', 'column', 'LastModifyTime'
go

alter table dbo.Fun_FundsLog
   add constraint PK_FUN_FUNDSLOG primary key (FundsLogId)
go


/****** Object:  Stored Procedure [dbo].Fun_FundsLogGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_FundsLogUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_FundsLogGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_FundsLogGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_FundsLogUpdateStatus
// 存储过程功能描述：更新Fun_FundsLog中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_FundsLog'

set @str = 'update [dbo].[Fun_FundsLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where FundsLogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_FundsLogGoBack
// 存储过程功能描述：撤返Fun_FundsLog，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_FundsLog'

set @str = 'update [dbo].[Fun_FundsLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where FundsLogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_FundsLogGet
// 存储过程功能描述：查询指定Fun_FundsLog的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogGet
    /*
	@FundsLogId int
    */
    @id int
AS

SELECT
	[FundsLogId],
	[ContractId],
	[SubId],
	[InvoiceId],
	[LogDate],
	[InBlocId],
	[InCorpId],
	[InBankId],
	[InAccountId],
	[OutBlocId],
	[OutCorpId],
	[OutBankId],
	[OutBank],
	[OutAccountId],
	[OutAccount],
	[FundsBala],
	[FundsType],
	[CurrencyId],
	[LogDirection],
	[LogType],
	[PayMode],
	[IsVirtualPay],
	[FundsDesc],
	[OpPerson],
	[LogSourceBase],
	[LogSource],
	[SourceId],
	[LogStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_FundsLog]
WHERE
	[FundsLogId] = @id

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
// 存储过程名：[dbo].Fun_FundsLogLoad
// 存储过程功能描述：查询所有Fun_FundsLog记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogLoad
AS

SELECT
	[FundsLogId],
	[ContractId],
	[SubId],
	[InvoiceId],
	[LogDate],
	[InBlocId],
	[InCorpId],
	[InBankId],
	[InAccountId],
	[OutBlocId],
	[OutCorpId],
	[OutBankId],
	[OutBank],
	[OutAccountId],
	[OutAccount],
	[FundsBala],
	[FundsType],
	[CurrencyId],
	[LogDirection],
	[LogType],
	[PayMode],
	[IsVirtualPay],
	[FundsDesc],
	[OpPerson],
	[LogSourceBase],
	[LogSource],
	[SourceId],
	[LogStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_FundsLog]

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
// 存储过程名：[dbo].Fun_FundsLogInsert
// 存储过程功能描述：新增一条Fun_FundsLog记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogInsert
	@ContractId int =NULL ,
	@SubId int =NULL ,
	@InvoiceId int =NULL ,
	@LogDate datetime =NULL ,
	@InBlocId int =NULL ,
	@InCorpId int =NULL ,
	@InBankId int =NULL ,
	@InAccountId int =NULL ,
	@OutBlocId int =NULL ,
	@OutCorpId int =NULL ,
	@OutBankId int =NULL ,
	@OutBank varchar(200) =NULL ,
	@OutAccountId int =NULL ,
	@OutAccount varchar(200) =NULL ,
	@FundsBala decimal(18, 4) =NULL ,
	@FundsType int =NULL ,
	@CurrencyId int =NULL ,
	@LogDirection int =NULL ,
	@LogType int ,
	@PayMode int =NULL ,
	@IsVirtualPay bit =NULL ,
	@FundsDesc varchar(800) =NULL ,
	@OpPerson int =NULL ,
	@LogSourceBase varchar(50) =NULL ,
	@LogSource varchar(200) =NULL ,
	@SourceId int =NULL ,
	@LogStatus int =NULL ,
	@CreatorId int =NULL ,
	@FundsLogId int OUTPUT
AS

INSERT INTO [dbo].[Fun_FundsLog] (
	[ContractId],
	[SubId],
	[InvoiceId],
	[LogDate],
	[InBlocId],
	[InCorpId],
	[InBankId],
	[InAccountId],
	[OutBlocId],
	[OutCorpId],
	[OutBankId],
	[OutBank],
	[OutAccountId],
	[OutAccount],
	[FundsBala],
	[FundsType],
	[CurrencyId],
	[LogDirection],
	[LogType],
	[PayMode],
	[IsVirtualPay],
	[FundsDesc],
	[OpPerson],
	[LogSourceBase],
	[LogSource],
	[SourceId],
	[LogStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractId,
	@SubId,
	@InvoiceId,
	@LogDate,
	@InBlocId,
	@InCorpId,
	@InBankId,
	@InAccountId,
	@OutBlocId,
	@OutCorpId,
	@OutBankId,
	@OutBank,
	@OutAccountId,
	@OutAccount,
	@FundsBala,
	@FundsType,
	@CurrencyId,
	@LogDirection,
	@LogType,
	@PayMode,
	@IsVirtualPay,
	@FundsDesc,
	@OpPerson,
	@LogSourceBase,
	@LogSource,
	@SourceId,
	@LogStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @FundsLogId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_FundsLogUpdate
// 存储过程功能描述：更新Fun_FundsLog
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_FundsLogUpdate
    @FundsLogId int,
@ContractId int = NULL,
@SubId int = NULL,
@InvoiceId int = NULL,
@LogDate datetime = NULL,
@InBlocId int = NULL,
@InCorpId int = NULL,
@InBankId int = NULL,
@InAccountId int = NULL,
@OutBlocId int = NULL,
@OutCorpId int = NULL,
@OutBankId int = NULL,
@OutBank varchar(200) = NULL,
@OutAccountId int = NULL,
@OutAccount varchar(200) = NULL,
@FundsBala decimal(18, 4) = NULL,
@FundsType int = NULL,
@CurrencyId int = NULL,
@LogDirection int = NULL,
@LogType int,
@PayMode int = NULL,
@IsVirtualPay bit = NULL,
@FundsDesc varchar(800) = NULL,
@OpPerson int = NULL,
@LogSourceBase varchar(50) = NULL,
@LogSource varchar(200) = NULL,
@SourceId int = NULL,
@LogStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Fun_FundsLog] SET
	[ContractId] = @ContractId,
	[SubId] = @SubId,
	[InvoiceId] = @InvoiceId,
	[LogDate] = @LogDate,
	[InBlocId] = @InBlocId,
	[InCorpId] = @InCorpId,
	[InBankId] = @InBankId,
	[InAccountId] = @InAccountId,
	[OutBlocId] = @OutBlocId,
	[OutCorpId] = @OutCorpId,
	[OutBankId] = @OutBankId,
	[OutBank] = @OutBank,
	[OutAccountId] = @OutAccountId,
	[OutAccount] = @OutAccount,
	[FundsBala] = @FundsBala,
	[FundsType] = @FundsType,
	[CurrencyId] = @CurrencyId,
	[LogDirection] = @LogDirection,
	[LogType] = @LogType,
	[PayMode] = @PayMode,
	[IsVirtualPay] = @IsVirtualPay,
	[FundsDesc] = @FundsDesc,
	[OpPerson] = @OpPerson,
	[LogSourceBase] = @LogSourceBase,
	[LogSource] = @LogSource,
	[SourceId] = @SourceId,
	[LogStatus] = @LogStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[FundsLogId] = @FundsLogId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



