alter table Fin_PledgeApplyStockDetail
   drop constraint PK_FIN_PLEDGEAPPLYSTOCKDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fin_PledgeApplyStockDetail')
            and   type = 'U')
   drop table Fin_PledgeApplyStockDetail
go

/*==============================================================*/
/* Table: Fin_PledgeApplyStockDetail                            */
/*==============================================================*/
create table Fin_PledgeApplyStockDetail (
   StockDetailId        int                  identity,
   PledgeApplyId        int                  null,
   ContractNo           varchar(30)          null,
   NetAmount            numeric(18,4)        null,
   StockId              int                  null,
   RefNo                varchar(30)          null,
   Deadline             varchar(20)          null,
   Hands                int                  null,
   Memo                 varchar(4000)        null,
   DetailStatus         int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '质押申请单实货明细',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'StockDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请单序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'PledgeApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合同号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'ContractNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '净重',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'NetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'RefNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '期限',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'Deadline'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '匹配手数',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'Hands'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyStockDetail', 'column', 'DetailStatus'
go

alter table Fin_PledgeApplyStockDetail
   add constraint PK_FIN_PLEDGEAPPLYSTOCKDETAIL primary key (StockDetailId)
go

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyStockDetailGet    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyStockDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyStockDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyStockDetailLoad    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyStockDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyStockDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyStockDetailInsert    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyStockDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyStockDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyStockDetailUpdate    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyStockDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyStockDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyStockDetailUpdateStatus    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyStockDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyStockDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyStockDetailUpdateStatus    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyStockDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyStockDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fin_PledgeApplyStockDetailUpdateStatus
// 存储过程功能描述：更新Fin_PledgeApplyStockDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyStockDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_PledgeApplyStockDetail'

set @str = 'update [dbo].[Fin_PledgeApplyStockDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_PledgeApplyStockDetailGoBack
// 存储过程功能描述：撤返Fin_PledgeApplyStockDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyStockDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_PledgeApplyStockDetail'

set @str = 'update [dbo].[Fin_PledgeApplyStockDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_PledgeApplyStockDetailGet
// 存储过程功能描述：查询指定Fin_PledgeApplyStockDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyStockDetailGet
    /*
	@StockDetailId int
    */
    @id int
AS

SELECT
	[StockDetailId],
	[PledgeApplyId],
	[ContractNo],
	[NetAmount],
	[StockId],
	[RefNo],
	[Deadline],
	[Hands],
	[Memo],
	[DetailStatus]
FROM
	[dbo].[Fin_PledgeApplyStockDetail]
WHERE
	[StockDetailId] = @id

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
// 存储过程名：[dbo].Fin_PledgeApplyStockDetailLoad
// 存储过程功能描述：查询所有Fin_PledgeApplyStockDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyStockDetailLoad
AS

SELECT
	[StockDetailId],
	[PledgeApplyId],
	[ContractNo],
	[NetAmount],
	[StockId],
	[RefNo],
	[Deadline],
	[Hands],
	[Memo],
	[DetailStatus]
FROM
	[dbo].[Fin_PledgeApplyStockDetail]

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
// 存储过程名：[dbo].Fin_PledgeApplyStockDetailInsert
// 存储过程功能描述：新增一条Fin_PledgeApplyStockDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyStockDetailInsert
	@PledgeApplyId int =NULL ,
	@ContractNo varchar(30) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@StockId int =NULL ,
	@RefNo varchar(30) =NULL ,
	@Deadline varchar(20) =NULL ,
	@Hands int =NULL ,
	@Memo varchar(4000) =NULL ,
	@DetailStatus int =NULL ,
	@StockDetailId int OUTPUT
AS

INSERT INTO [dbo].[Fin_PledgeApplyStockDetail] (
	[PledgeApplyId],
	[ContractNo],
	[NetAmount],
	[StockId],
	[RefNo],
	[Deadline],
	[Hands],
	[Memo],
	[DetailStatus]
) VALUES (
	@PledgeApplyId,
	@ContractNo,
	@NetAmount,
	@StockId,
	@RefNo,
	@Deadline,
	@Hands,
	@Memo,
	@DetailStatus
)


SET @StockDetailId = @@IDENTITY

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
// 存储过程名：[dbo].Fin_PledgeApplyStockDetailUpdate
// 存储过程功能描述：更新Fin_PledgeApplyStockDetail
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyStockDetailUpdate
    @StockDetailId int,
@PledgeApplyId int = NULL,
@ContractNo varchar(30) = NULL,
@NetAmount numeric(18, 4) = NULL,
@StockId int = NULL,
@RefNo varchar(30) = NULL,
@Deadline varchar(20) = NULL,
@Hands int = NULL,
@Memo varchar(4000) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Fin_PledgeApplyStockDetail] SET
	[PledgeApplyId] = @PledgeApplyId,
	[ContractNo] = @ContractNo,
	[NetAmount] = @NetAmount,
	[StockId] = @StockId,
	[RefNo] = @RefNo,
	[Deadline] = @Deadline,
	[Hands] = @Hands,
	[Memo] = @Memo,
	[DetailStatus] = @DetailStatus
WHERE
	[StockDetailId] = @StockDetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



