alter table Fun_CashInContract_Ref
   drop constraint PK_FUN_CASHINCONTRACT_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fun_CashInContract_Ref')
            and   type = 'U')
   drop table Fun_CashInContract_Ref
go

/*==============================================================*/
/* Table: Fun_CashInContract_Ref                                */
/*==============================================================*/
create table Fun_CashInContract_Ref (
   RefId                int                  identity,
   CorpRefId            int                  null,
   AllotId              int                  null,
   CashInId             int                  null,
   ContractId           int                  null,
   SubContractId        int                  null,
   AllotBala            numeric(18,4)        null,
   DetailStatus         int                  null,
   FundsLogId           int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '收款分配至合约',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约收款序号',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'RefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收款分配公司序号',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'CorpRefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配序号',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'AllotId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收款登记序号',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'CashInId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'SubContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配金额',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'AllotBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资金流水序号',
   'user', @CurrentUser, 'table', 'Fun_CashInContract_Ref', 'column', 'FundsLogId'
go

alter table Fun_CashInContract_Ref
   add constraint PK_FUN_CASHINCONTRACT_REF primary key (RefId)
go


/****** Object:  Stored Procedure [dbo].Fun_CashInContract_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInContract_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInContract_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInContract_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInContract_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInContract_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInContract_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInContract_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInContract_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInContract_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInContract_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInContract_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInContract_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInContract_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInContract_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInContract_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInContract_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInContract_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_CashInContract_RefUpdateStatus
// 存储过程功能描述：更新Fun_CashInContract_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInContract_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInContract_Ref'

set @str = 'update [dbo].[Fun_CashInContract_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_CashInContract_RefGoBack
// 存储过程功能描述：撤返Fun_CashInContract_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInContract_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInContract_Ref'

set @str = 'update [dbo].[Fun_CashInContract_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fun_CashInContract_RefGet
// 存储过程功能描述：查询指定Fun_CashInContract_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInContract_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[CorpRefId],
	[AllotId],
	[CashInId],
	[ContractId],
	[SubContractId],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
FROM
	[dbo].[Fun_CashInContract_Ref]
WHERE
	[RefId] = @id

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
// 存储过程名：[dbo].Fun_CashInContract_RefLoad
// 存储过程功能描述：查询所有Fun_CashInContract_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInContract_RefLoad
AS

SELECT
	[RefId],
	[CorpRefId],
	[AllotId],
	[CashInId],
	[ContractId],
	[SubContractId],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
FROM
	[dbo].[Fun_CashInContract_Ref]

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
// 存储过程名：[dbo].Fun_CashInContract_RefInsert
// 存储过程功能描述：新增一条Fun_CashInContract_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInContract_RefInsert
	@CorpRefId int =NULL ,
	@AllotId int =NULL ,
	@CashInId int =NULL ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@AllotBala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@FundsLogId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Fun_CashInContract_Ref] (
	[CorpRefId],
	[AllotId],
	[CashInId],
	[ContractId],
	[SubContractId],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
) VALUES (
	@CorpRefId,
	@AllotId,
	@CashInId,
	@ContractId,
	@SubContractId,
	@AllotBala,
	@DetailStatus,
	@FundsLogId
)


SET @RefId = @@IDENTITY

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
// 存储过程名：[dbo].Fun_CashInContract_RefUpdate
// 存储过程功能描述：更新Fun_CashInContract_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInContract_RefUpdate
    @RefId int,
@CorpRefId int = NULL,
@AllotId int = NULL,
@CashInId int = NULL,
@ContractId int = NULL,
@SubContractId int = NULL,
@AllotBala numeric(18, 4) = NULL,
@DetailStatus int = NULL,
@FundsLogId int = NULL
AS

UPDATE [dbo].[Fun_CashInContract_Ref] SET
	[CorpRefId] = @CorpRefId,
	[AllotId] = @AllotId,
	[CashInId] = @CashInId,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[AllotBala] = @AllotBala,
	[DetailStatus] = @DetailStatus,
	[FundsLogId] = @FundsLogId
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



