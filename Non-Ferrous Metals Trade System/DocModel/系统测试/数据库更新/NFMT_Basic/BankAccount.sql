alter table dbo.BankAccount
   drop constraint PK_BANKACCOUNT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BankAccount')
            and   type = 'U')
   drop table dbo.BankAccount
go

/*==============================================================*/
/* Table: BankAccount                                           */
/*==============================================================*/
create table dbo.BankAccount (
   BankAccId            int                  identity,
   CompanyId            int                  null,
   BankId               int                  null,
   AccountNo            varchar(80)          null,
   CurrencyId           int                  null,
   BankAccDesc          varchar(400)         null,
   BankAccStatus        int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '银行账号',
   'user', 'dbo', 'table', 'BankAccount'
go

execute sp_addextendedproperty 'MS_Description', 
   '银行账户序号',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'BankAccId'
go

execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'CompanyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '银行序号',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'BankId'
go

execute sp_addextendedproperty 'MS_Description', 
   '账户号',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'AccountNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '币种序号',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '账户描述',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'BankAccDesc'
go

execute sp_addextendedproperty 'MS_Description', 
   '账户状态',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'BankAccStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'BankAccount', 'column', 'LastModifyTime'
go

alter table dbo.BankAccount
   add constraint PK_BANKACCOUNT primary key (BankAccId)
go


/****** Object:  Stored Procedure [dbo].BankAccountGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankAccountGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankAccountGet]
GO

/****** Object:  Stored Procedure [dbo].BankAccountLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankAccountLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankAccountLoad]
GO

/****** Object:  Stored Procedure [dbo].BankAccountInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankAccountInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankAccountInsert]
GO

/****** Object:  Stored Procedure [dbo].BankAccountUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankAccountUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankAccountUpdate]
GO

/****** Object:  Stored Procedure [dbo].BankAccountUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankAccountUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankAccountUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].BankAccountUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankAccountGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BankAccountGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].BankAccountUpdateStatus
// 存储过程功能描述：更新BankAccount中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankAccountUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BankAccount'

set @str = 'update [dbo].[BankAccount] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BankAccId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BankAccountGoBack
// 存储过程功能描述：撤返BankAccount，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankAccountGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.BankAccount'

set @str = 'update [dbo].[BankAccount] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where BankAccId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].BankAccountGet
// 存储过程功能描述：查询指定BankAccount的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankAccountGet
    /*
	@BankAccId int
    */
    @id int
AS

SELECT
	[BankAccId],
	[CompanyId],
	[BankId],
	[AccountNo],
	[CurrencyId],
	[BankAccDesc],
	[BankAccStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BankAccount]
WHERE
	[BankAccId] = @id

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
// 存储过程名：[dbo].BankAccountLoad
// 存储过程功能描述：查询所有BankAccount记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankAccountLoad
AS

SELECT
	[BankAccId],
	[CompanyId],
	[BankId],
	[AccountNo],
	[CurrencyId],
	[BankAccDesc],
	[BankAccStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[BankAccount]

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
// 存储过程名：[dbo].BankAccountInsert
// 存储过程功能描述：新增一条BankAccount记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankAccountInsert
	@CompanyId int ,
	@BankId int ,
	@AccountNo varchar(80) ,
	@CurrencyId int ,
	@BankAccDesc varchar(400) =NULL ,
	@BankAccStatus int =NULL ,
	@CreatorId int ,
	@BankAccId int OUTPUT
AS

INSERT INTO [dbo].[BankAccount] (
	[CompanyId],
	[BankId],
	[AccountNo],
	[CurrencyId],
	[BankAccDesc],
	[BankAccStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@CompanyId,
	@BankId,
	@AccountNo,
	@CurrencyId,
	@BankAccDesc,
	@BankAccStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @BankAccId = @@IDENTITY

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
// 存储过程名：[dbo].BankAccountUpdate
// 存储过程功能描述：更新BankAccount
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].BankAccountUpdate
    @BankAccId int,
@CompanyId int,
@BankId int,
@AccountNo varchar(80),
@CurrencyId int,
@BankAccDesc varchar(400) = NULL,
@BankAccStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[BankAccount] SET
	[CompanyId] = @CompanyId,
	[BankId] = @BankId,
	[AccountNo] = @AccountNo,
	[CurrencyId] = @CurrencyId,
	[BankAccDesc] = @BankAccDesc,
	[BankAccStatus] = @BankAccStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[BankAccId] = @BankAccId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



