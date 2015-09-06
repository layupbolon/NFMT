alter table Fun_CashInStcok_Ref
   drop constraint PK_FUN_CASHINSTCOK_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fun_CashInStcok_Ref')
            and   type = 'U')
   drop table Fun_CashInStcok_Ref
go

/*==============================================================*/
/* Table: Fun_CashInStcok_Ref                                   */
/*==============================================================*/
create table Fun_CashInStcok_Ref (
   RefId                int                  identity,
   AllotId              int                  null,
   CorpRefId            int                  null,
   ContractRefId        int                  null,
   CashInId             int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   StockNameId          int                  null,
   AllotNetAmount       numeric(18,4)        null,
   AllotBala            numeric(18,4)        null,
   DetailStatus         int                  null,
   FundsLogId           int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '收款分配至库存',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'RefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配序号',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'AllotId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收款分配合约序号',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'ContractRefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收款登记序号',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'CashInId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'StockNameId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '配款净重',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'AllotNetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配金额',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'AllotBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资金流水序号',
   'user', @CurrentUser, 'table', 'Fun_CashInStcok_Ref', 'column', 'FundsLogId'
go

alter table Fun_CashInStcok_Ref
   add constraint PK_FUN_CASHINSTCOK_REF primary key (RefId)
go


/****** Object:  Stored Procedure [dbo].Fun_CashInStcok_RefGet    Script Date: 2015年3月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInStcok_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInStcok_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInStcok_RefLoad    Script Date: 2015年3月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInStcok_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInStcok_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInStcok_RefInsert    Script Date: 2015年3月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInStcok_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInStcok_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInStcok_RefUpdate    Script Date: 2015年3月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInStcok_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInStcok_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInStcok_RefUpdateStatus    Script Date: 2015年3月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInStcok_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInStcok_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInStcok_RefUpdateStatus    Script Date: 2015年3月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInStcok_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInStcok_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_CashInStcok_RefUpdateStatus
// 存储过程功能描述：更新Fun_CashInStcok_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInStcok_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInStcok_Ref'

set @str = 'update [dbo].[Fun_CashInStcok_Ref] '+
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
// 存储过程名：[dbo].Fun_CashInStcok_RefGoBack
// 存储过程功能描述：撤返Fun_CashInStcok_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInStcok_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInStcok_Ref'

set @str = 'update [dbo].[Fun_CashInStcok_Ref] '+
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
// 存储过程名：[dbo].Fun_CashInStcok_RefGet
// 存储过程功能描述：查询指定Fun_CashInStcok_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInStcok_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[AllotId],
	[CorpRefId],
	[ContractRefId],
	[CashInId],
	[StockId],
	[StockLogId],
	[StockNameId],
	[AllotNetAmount],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
FROM
	[dbo].[Fun_CashInStcok_Ref]
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
// 存储过程名：[dbo].Fun_CashInStcok_RefLoad
// 存储过程功能描述：查询所有Fun_CashInStcok_Ref记录
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInStcok_RefLoad
AS

SELECT
	[RefId],
	[AllotId],
	[CorpRefId],
	[ContractRefId],
	[CashInId],
	[StockId],
	[StockLogId],
	[StockNameId],
	[AllotNetAmount],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
FROM
	[dbo].[Fun_CashInStcok_Ref]

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
// 存储过程名：[dbo].Fun_CashInStcok_RefInsert
// 存储过程功能描述：新增一条Fun_CashInStcok_Ref记录
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInStcok_RefInsert
	@AllotId int =NULL ,
	@CorpRefId int =NULL ,
	@ContractRefId int =NULL ,
	@CashInId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@StockNameId int =NULL ,
	@AllotNetAmount numeric(18, 4) =NULL ,
	@AllotBala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@FundsLogId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Fun_CashInStcok_Ref] (
	[AllotId],
	[CorpRefId],
	[ContractRefId],
	[CashInId],
	[StockId],
	[StockLogId],
	[StockNameId],
	[AllotNetAmount],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
) VALUES (
	@AllotId,
	@CorpRefId,
	@ContractRefId,
	@CashInId,
	@StockId,
	@StockLogId,
	@StockNameId,
	@AllotNetAmount,
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
// 存储过程名：[dbo].Fun_CashInStcok_RefUpdate
// 存储过程功能描述：更新Fun_CashInStcok_Ref
// 创建人：CodeSmith
// 创建时间： 2015年3月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInStcok_RefUpdate
    @RefId int,
@AllotId int = NULL,
@CorpRefId int = NULL,
@ContractRefId int = NULL,
@CashInId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@StockNameId int = NULL,
@AllotNetAmount numeric(18, 4) = NULL,
@AllotBala numeric(18, 4) = NULL,
@DetailStatus int = NULL,
@FundsLogId int = NULL
AS

UPDATE [dbo].[Fun_CashInStcok_Ref] SET
	[AllotId] = @AllotId,
	[CorpRefId] = @CorpRefId,
	[ContractRefId] = @ContractRefId,
	[CashInId] = @CashInId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[StockNameId] = @StockNameId,
	[AllotNetAmount] = @AllotNetAmount,
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



