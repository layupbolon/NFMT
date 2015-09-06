alter table Fun_CashInCorp_Ref
   drop constraint PK_FUN_CASHINCORP_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fun_CashInCorp_Ref')
            and   type = 'U')
   drop table Fun_CashInCorp_Ref
go

/*==============================================================*/
/* Table: Fun_CashInCorp_Ref                                    */
/*==============================================================*/
create table Fun_CashInCorp_Ref (
   RefId                int                  identity,
   AllotId              int                  null,
   BlocId               int                  null,
   CorpId               int                  null,
   CashInId             int                  null,
   IsShare              bit                  null,
   AllotBala            numeric(18,4)        null,
   DetailStatus         int                  null,
   FundsLogId           int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '收款分配至公司',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'RefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配序号',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'AllotId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '集团序号',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'BlocId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司序号',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'CorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收款登记序号',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'CashInId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否集团共享资金',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'IsShare'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配金额',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'AllotBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资金流水序号',
   'user', @CurrentUser, 'table', 'Fun_CashInCorp_Ref', 'column', 'FundsLogId'
go

alter table Fun_CashInCorp_Ref
   add constraint PK_FUN_CASHINCORP_REF primary key (RefId)
go


/****** Object:  Stored Procedure [dbo].Fun_CashInCorp_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInCorp_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInCorp_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInCorp_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInCorp_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInCorp_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInCorp_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInCorp_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInCorp_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInCorp_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInCorp_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInCorp_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInCorp_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInCorp_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInCorp_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_CashInCorp_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_CashInCorp_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_CashInCorp_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_CashInCorp_RefUpdateStatus
// 存储过程功能描述：更新Fun_CashInCorp_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInCorp_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInCorp_Ref'

set @str = 'update [dbo].[Fun_CashInCorp_Ref] '+
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
// 存储过程名：[dbo].Fun_CashInCorp_RefGoBack
// 存储过程功能描述：撤返Fun_CashInCorp_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInCorp_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_CashInCorp_Ref'

set @str = 'update [dbo].[Fun_CashInCorp_Ref] '+
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
// 存储过程名：[dbo].Fun_CashInCorp_RefGet
// 存储过程功能描述：查询指定Fun_CashInCorp_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInCorp_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[AllotId],
	[BlocId],
	[CorpId],
	[CashInId],
	[IsShare],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
FROM
	[dbo].[Fun_CashInCorp_Ref]
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
// 存储过程名：[dbo].Fun_CashInCorp_RefLoad
// 存储过程功能描述：查询所有Fun_CashInCorp_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInCorp_RefLoad
AS

SELECT
	[RefId],
	[AllotId],
	[BlocId],
	[CorpId],
	[CashInId],
	[IsShare],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
FROM
	[dbo].[Fun_CashInCorp_Ref]

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
// 存储过程名：[dbo].Fun_CashInCorp_RefInsert
// 存储过程功能描述：新增一条Fun_CashInCorp_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInCorp_RefInsert
	@AllotId int =NULL ,
	@BlocId int =NULL ,
	@CorpId int =NULL ,
	@CashInId int =NULL ,
	@IsShare bit =NULL ,
	@AllotBala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@FundsLogId int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Fun_CashInCorp_Ref] (
	[AllotId],
	[BlocId],
	[CorpId],
	[CashInId],
	[IsShare],
	[AllotBala],
	[DetailStatus],
	[FundsLogId]
) VALUES (
	@AllotId,
	@BlocId,
	@CorpId,
	@CashInId,
	@IsShare,
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
// 存储过程名：[dbo].Fun_CashInCorp_RefUpdate
// 存储过程功能描述：更新Fun_CashInCorp_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_CashInCorp_RefUpdate
    @RefId int,
@AllotId int = NULL,
@BlocId int = NULL,
@CorpId int = NULL,
@CashInId int = NULL,
@IsShare bit = NULL,
@AllotBala numeric(18, 4) = NULL,
@DetailStatus int = NULL,
@FundsLogId int = NULL
AS

UPDATE [dbo].[Fun_CashInCorp_Ref] SET
	[AllotId] = @AllotId,
	[BlocId] = @BlocId,
	[CorpId] = @CorpId,
	[CashInId] = @CashInId,
	[IsShare] = @IsShare,
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



