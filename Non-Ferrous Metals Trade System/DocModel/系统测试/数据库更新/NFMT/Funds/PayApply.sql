alter table dbo.Fun_PayApply
   drop constraint PK_FUN_PAYAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_PayApply')
            and   type = 'U')
   drop table dbo.Fun_PayApply
go

/*==============================================================*/
/* Table: Fun_PayApply                                          */
/*==============================================================*/
create table dbo.Fun_PayApply (
   PayApplyId           int                  identity,
   ApplyId              int                  null,
   PayApplySource       int                  null,
   RecBlocId            int                  null,
   RecCorpId            int                  null,
   RecBankId            int                  null,
   RecBankAccountId     int                  null,
   RecBankAccount       varchar(50)          null,
   ApplyBala            numeric(18,4)        null,
   CurrencyId           int                  null,
   PayMode              int                  null,
   PayDeadline          datetime             null,
   SpecialDesc          varchar(4000)        null,
   PayMatter            int                  null,
   RealPayCorpId        int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '付款申请',
   'user', 'dbo', 'table', 'Fun_PayApply'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请主表序号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'ApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请来源
   合约付款申请、库存付款申请、发票付款申请',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'PayApplySource'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款集团序号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'RecBlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款公司序号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'RecCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款公司银行序号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'RecBankId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款公司银行账号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'RecBankAccountId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收款公司银行账号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'RecBankAccount'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请金额',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'ApplyBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款方式',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'PayMode'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款期限',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'PayDeadline'
go

execute sp_addextendedproperty 'MS_Description', 
   '特殊要求',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'SpecialDesc'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款事项',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'PayMatter'
go

execute sp_addextendedproperty 'MS_Description', 
   '实际付款公司',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'RealPayCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Fun_PayApply', 'column', 'LastModifyTime'
go

alter table dbo.Fun_PayApply
   add constraint PK_FUN_PAYAPPLY primary key (PayApplyId)
go


/****** Object:  Stored Procedure [dbo].Fun_PayApplyGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_PayApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_PayApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_PayApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_PayApplyUpdateStatus
// 存储过程功能描述：更新Fun_PayApply中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PayApply'

set @str = 'update [dbo].[Fun_PayApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PayApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PayApplyGoBack
// 存储过程功能描述：撤返Fun_PayApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_PayApply'

set @str = 'update [dbo].[Fun_PayApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PayApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_PayApplyGet
// 存储过程功能描述：查询指定Fun_PayApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyGet
    /*
	@PayApplyId int
    */
    @id int
AS

SELECT
	[PayApplyId],
	[ApplyId],
	[PayApplySource],
	[RecBlocId],
	[RecCorpId],
	[RecBankId],
	[RecBankAccountId],
	[RecBankAccount],
	[ApplyBala],
	[CurrencyId],
	[PayMode],
	[PayDeadline],
	[SpecialDesc],
	[PayMatter],
	[RealPayCorpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_PayApply]
WHERE
	[PayApplyId] = @id

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
// 存储过程名：[dbo].Fun_PayApplyLoad
// 存储过程功能描述：查询所有Fun_PayApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyLoad
AS

SELECT
	[PayApplyId],
	[ApplyId],
	[PayApplySource],
	[RecBlocId],
	[RecCorpId],
	[RecBankId],
	[RecBankAccountId],
	[RecBankAccount],
	[ApplyBala],
	[CurrencyId],
	[PayMode],
	[PayDeadline],
	[SpecialDesc],
	[PayMatter],
	[RealPayCorpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fun_PayApply]

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
// 存储过程名：[dbo].Fun_PayApplyInsert
// 存储过程功能描述：新增一条Fun_PayApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyInsert
	@ApplyId int =NULL ,
	@PayApplySource int =NULL ,
	@RecBlocId int =NULL ,
	@RecCorpId int =NULL ,
	@RecBankId int =NULL ,
	@RecBankAccountId int =NULL ,
	@RecBankAccount varchar(50) =NULL ,
	@ApplyBala numeric(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@PayMode int =NULL ,
	@PayDeadline datetime =NULL ,
	@SpecialDesc varchar(4000) =NULL ,
	@PayMatter int =NULL ,
	@RealPayCorpId int =NULL ,
	@CreatorId int =NULL ,
	@PayApplyId int OUTPUT
AS

INSERT INTO [dbo].[Fun_PayApply] (
	[ApplyId],
	[PayApplySource],
	[RecBlocId],
	[RecCorpId],
	[RecBankId],
	[RecBankAccountId],
	[RecBankAccount],
	[ApplyBala],
	[CurrencyId],
	[PayMode],
	[PayDeadline],
	[SpecialDesc],
	[PayMatter],
	[RealPayCorpId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ApplyId,
	@PayApplySource,
	@RecBlocId,
	@RecCorpId,
	@RecBankId,
	@RecBankAccountId,
	@RecBankAccount,
	@ApplyBala,
	@CurrencyId,
	@PayMode,
	@PayDeadline,
	@SpecialDesc,
	@PayMatter,
	@RealPayCorpId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PayApplyId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_PayApplyUpdate
// 存储过程功能描述：更新Fun_PayApply
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_PayApplyUpdate
    @PayApplyId int,
@ApplyId int = NULL,
@PayApplySource int = NULL,
@RecBlocId int = NULL,
@RecCorpId int = NULL,
@RecBankId int = NULL,
@RecBankAccountId int = NULL,
@RecBankAccount varchar(50) = NULL,
@ApplyBala numeric(18, 4) = NULL,
@CurrencyId int = NULL,
@PayMode int = NULL,
@PayDeadline datetime = NULL,
@SpecialDesc varchar(4000) = NULL,
@PayMatter int = NULL,
@RealPayCorpId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Fun_PayApply] SET
	[ApplyId] = @ApplyId,
	[PayApplySource] = @PayApplySource,
	[RecBlocId] = @RecBlocId,
	[RecCorpId] = @RecCorpId,
	[RecBankId] = @RecBankId,
	[RecBankAccountId] = @RecBankAccountId,
	[RecBankAccount] = @RecBankAccount,
	[ApplyBala] = @ApplyBala,
	[CurrencyId] = @CurrencyId,
	[PayMode] = @PayMode,
	[PayDeadline] = @PayDeadline,
	[SpecialDesc] = @SpecialDesc,
	[PayMatter] = @PayMatter,
	[RealPayCorpId] = @RealPayCorpId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PayApplyId] = @PayApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



