alter table Fin_PledgeApplyCashDetail
   drop constraint PK_FIN_PLEDGEAPPLYCASHDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fin_PledgeApplyCashDetail')
            and   type = 'U')
   drop table Fin_PledgeApplyCashDetail
go

/*==============================================================*/
/* Table: Fin_PledgeApplyCashDetail                             */
/*==============================================================*/
create table Fin_PledgeApplyCashDetail (
   CashDetailId         int                  identity,
   PledgeApplyId        int                  null,
   StockContractNo      varchar(30)          null,
   Hands                int                  null,
   Price                numeric(18,4)        null,
   ExpiringDate         datetime             null,
   AccountName          varchar(50)          null,
   Memo                 varchar(4000)        null,
   DetailStatus         int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '质押申请单期货头寸明细',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'CashDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请单序号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'PledgeApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实货合同号',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'StockContractNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量（手）',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'Hands'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'Price'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '到期日',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'ExpiringDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '经济公司账户名',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'AccountName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'Fin_PledgeApplyCashDetail', 'column', 'DetailStatus'
go

alter table Fin_PledgeApplyCashDetail
   add constraint PK_FIN_PLEDGEAPPLYCASHDETAIL primary key (CashDetailId)
go

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyCashDetailGet    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyCashDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyCashDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyCashDetailLoad    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyCashDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyCashDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyCashDetailInsert    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyCashDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyCashDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyCashDetailUpdate    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyCashDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyCashDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyCashDetailUpdateStatus    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyCashDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyCashDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fin_PledgeApplyCashDetailUpdateStatus    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_PledgeApplyCashDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_PledgeApplyCashDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fin_PledgeApplyCashDetailUpdateStatus
// 存储过程功能描述：更新Fin_PledgeApplyCashDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyCashDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_PledgeApplyCashDetail'

set @str = 'update [dbo].[Fin_PledgeApplyCashDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CashDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_PledgeApplyCashDetailGoBack
// 存储过程功能描述：撤返Fin_PledgeApplyCashDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyCashDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_PledgeApplyCashDetail'

set @str = 'update [dbo].[Fin_PledgeApplyCashDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where CashDetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_PledgeApplyCashDetailGet
// 存储过程功能描述：查询指定Fin_PledgeApplyCashDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyCashDetailGet
    /*
	@CashDetailId int
    */
    @id int
AS

SELECT
	[CashDetailId],
	[PledgeApplyId],
	[StockContractNo],
	[Hands],
	[Price],
	[ExpiringDate],
	[AccountName],
	[Memo],
	[DetailStatus]
FROM
	[dbo].[Fin_PledgeApplyCashDetail]
WHERE
	[CashDetailId] = @id

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
// 存储过程名：[dbo].Fin_PledgeApplyCashDetailLoad
// 存储过程功能描述：查询所有Fin_PledgeApplyCashDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyCashDetailLoad
AS

SELECT
	[CashDetailId],
	[PledgeApplyId],
	[StockContractNo],
	[Hands],
	[Price],
	[ExpiringDate],
	[AccountName],
	[Memo],
	[DetailStatus]
FROM
	[dbo].[Fin_PledgeApplyCashDetail]

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
// 存储过程名：[dbo].Fin_PledgeApplyCashDetailInsert
// 存储过程功能描述：新增一条Fin_PledgeApplyCashDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyCashDetailInsert
	@PledgeApplyId int =NULL ,
	@StockContractNo varchar(30) =NULL ,
	@Hands int =NULL ,
	@Price numeric(18, 4) =NULL ,
	@ExpiringDate datetime =NULL ,
	@AccountName varchar(50) =NULL ,
	@Memo varchar(4000) =NULL ,
	@DetailStatus int =NULL ,
	@CashDetailId int OUTPUT
AS

INSERT INTO [dbo].[Fin_PledgeApplyCashDetail] (
	[PledgeApplyId],
	[StockContractNo],
	[Hands],
	[Price],
	[ExpiringDate],
	[AccountName],
	[Memo],
	[DetailStatus]
) VALUES (
	@PledgeApplyId,
	@StockContractNo,
	@Hands,
	@Price,
	@ExpiringDate,
	@AccountName,
	@Memo,
	@DetailStatus
)


SET @CashDetailId = @@IDENTITY

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
// 存储过程名：[dbo].Fin_PledgeApplyCashDetailUpdate
// 存储过程功能描述：更新Fin_PledgeApplyCashDetail
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_PledgeApplyCashDetailUpdate
    @CashDetailId int,
@PledgeApplyId int = NULL,
@StockContractNo varchar(30) = NULL,
@Hands int = NULL,
@Price numeric(18, 4) = NULL,
@ExpiringDate datetime = NULL,
@AccountName varchar(50) = NULL,
@Memo varchar(4000) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Fin_PledgeApplyCashDetail] SET
	[PledgeApplyId] = @PledgeApplyId,
	[StockContractNo] = @StockContractNo,
	[Hands] = @Hands,
	[Price] = @Price,
	[ExpiringDate] = @ExpiringDate,
	[AccountName] = @AccountName,
	[Memo] = @Memo,
	[DetailStatus] = @DetailStatus
WHERE
	[CashDetailId] = @CashDetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



