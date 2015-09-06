alter table Inv_FinBusInvAllotDetail
   drop constraint PK_INV_FINBUSINVALLOTDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Inv_FinBusInvAllotDetail')
            and   type = 'U')
   drop table Inv_FinBusInvAllotDetail
go

/*==============================================================*/
/* Table: Inv_FinBusInvAllotDetail                              */
/*==============================================================*/
create table Inv_FinBusInvAllotDetail (
   DetailId             int                  identity,
   AllotId              int                  null,
   BusinessInvoiceId    int                  null,
   FinanceInvoiceId     int                  null,
   AllotBala            numeric(18,4)        null,
   DetailStatus         int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '业务发票财务发票分配明细',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllotDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllotDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配序号',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllotDetail', 'column', 'AllotId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '业务发票序号',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllotDetail', 'column', 'BusinessInvoiceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '财务发票序号',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllotDetail', 'column', 'FinanceInvoiceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '金额',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllotDetail', 'column', 'AllotBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Inv_FinBusInvAllotDetail', 'column', 'DetailStatus'
go

alter table Inv_FinBusInvAllotDetail
   add constraint PK_INV_FINBUSINVALLOTDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_FinBusInvAllotDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_FinBusInvAllotDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_FinBusInvAllotDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_FinBusInvAllotDetailUpdateStatus
// 存储过程功能描述：更新Inv_FinBusInvAllotDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinBusInvAllotDetail'

set @str = 'update [dbo].[Inv_FinBusInvAllotDetail] '+
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
// 存储过程名：[dbo].Inv_FinBusInvAllotDetailGoBack
// 存储过程功能描述：撤返Inv_FinBusInvAllotDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_FinBusInvAllotDetail'

set @str = 'update [dbo].[Inv_FinBusInvAllotDetail] '+
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
// 存储过程名：[dbo].Inv_FinBusInvAllotDetailGet
// 存储过程功能描述：查询指定Inv_FinBusInvAllotDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[AllotId],
	[BusinessInvoiceId],
	[FinanceInvoiceId],
	[AllotBala],
	[DetailStatus]
FROM
	[dbo].[Inv_FinBusInvAllotDetail]
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
// 存储过程名：[dbo].Inv_FinBusInvAllotDetailLoad
// 存储过程功能描述：查询所有Inv_FinBusInvAllotDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotDetailLoad
AS

SELECT
	[DetailId],
	[AllotId],
	[BusinessInvoiceId],
	[FinanceInvoiceId],
	[AllotBala],
	[DetailStatus]
FROM
	[dbo].[Inv_FinBusInvAllotDetail]

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
// 存储过程名：[dbo].Inv_FinBusInvAllotDetailInsert
// 存储过程功能描述：新增一条Inv_FinBusInvAllotDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotDetailInsert
	@AllotId int =NULL ,
	@BusinessInvoiceId int =NULL ,
	@FinanceInvoiceId int =NULL ,
	@AllotBala numeric(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Inv_FinBusInvAllotDetail] (
	[AllotId],
	[BusinessInvoiceId],
	[FinanceInvoiceId],
	[AllotBala],
	[DetailStatus]
) VALUES (
	@AllotId,
	@BusinessInvoiceId,
	@FinanceInvoiceId,
	@AllotBala,
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
// 存储过程名：[dbo].Inv_FinBusInvAllotDetailUpdate
// 存储过程功能描述：更新Inv_FinBusInvAllotDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_FinBusInvAllotDetailUpdate
    @DetailId int,
@AllotId int = NULL,
@BusinessInvoiceId int = NULL,
@FinanceInvoiceId int = NULL,
@AllotBala numeric(18, 4) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Inv_FinBusInvAllotDetail] SET
	[AllotId] = @AllotId,
	[BusinessInvoiceId] = @BusinessInvoiceId,
	[FinanceInvoiceId] = @FinanceInvoiceId,
	[AllotBala] = @AllotBala,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



