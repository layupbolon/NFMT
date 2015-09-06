alter table Fin_RepoApplyDetail
   drop constraint PK_FIN_REPOAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fin_RepoApplyDetail')
            and   type = 'U')
   drop table Fin_RepoApplyDetail
go

/*==============================================================*/
/* Table: Fin_RepoApplyDetail                                   */
/*==============================================================*/
create table Fin_RepoApplyDetail (
   DetailId             int                  identity,
   RepoApplyId          int                  null,
   StockDetailId        int                  null,
   PledgeApplyId        int                  null,
   RepoTime             datetime             null,
   ContractNo           varchar(30)          null,
   NetAmount            numeric(18,4)        null,
   StockId              int                  null,
   RefNo                varchar(30)          null,
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
   '赎回申请单明细',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请单序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'RepoApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实货明细序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'StockDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请单序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'PledgeApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日期',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'RepoTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合同号',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'ContractNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '净重',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'NetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'RefNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '匹配手数',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'Hands'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'Price'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '到期日',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'ExpiringDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '经济公司',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'AccountName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'Fin_RepoApplyDetail', 'column', 'DetailStatus'
go

alter table Fin_RepoApplyDetail
   add constraint PK_FIN_REPOAPPLYDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Fin_RepoApplyDetailGet    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyDetailLoad    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyDetailInsert    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyDetailUpdate    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyDetailUpdateStatus    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyDetailUpdateStatus    Script Date: 2015年4月21日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fin_RepoApplyDetailUpdateStatus
// 存储过程功能描述：更新Fin_RepoApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_RepoApplyDetail'

set @str = 'update [dbo].[Fin_RepoApplyDetail] '+
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
// 存储过程名：[dbo].Fin_RepoApplyDetailGoBack
// 存储过程功能描述：撤返Fin_RepoApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_RepoApplyDetail'

set @str = 'update [dbo].[Fin_RepoApplyDetail] '+
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
// 存储过程名：[dbo].Fin_RepoApplyDetailGet
// 存储过程功能描述：查询指定Fin_RepoApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[RepoApplyId],
	[StockDetailId],
	[PledgeApplyId],
	[RepoTime],
	[ContractNo],
	[NetAmount],
	[StockId],
	[RefNo],
	[Hands],
	[Price],
	[ExpiringDate],
	[AccountName],
	[Memo],
	[DetailStatus]
FROM
	[dbo].[Fin_RepoApplyDetail]
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
// 存储过程名：[dbo].Fin_RepoApplyDetailLoad
// 存储过程功能描述：查询所有Fin_RepoApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyDetailLoad
AS

SELECT
	[DetailId],
	[RepoApplyId],
	[StockDetailId],
	[PledgeApplyId],
	[RepoTime],
	[ContractNo],
	[NetAmount],
	[StockId],
	[RefNo],
	[Hands],
	[Price],
	[ExpiringDate],
	[AccountName],
	[Memo],
	[DetailStatus]
FROM
	[dbo].[Fin_RepoApplyDetail]

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
// 存储过程名：[dbo].Fin_RepoApplyDetailInsert
// 存储过程功能描述：新增一条Fin_RepoApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyDetailInsert
	@RepoApplyId int =NULL ,
	@StockDetailId int =NULL ,
	@PledgeApplyId int =NULL ,
	@RepoTime datetime =NULL ,
	@ContractNo varchar(30) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@StockId int =NULL ,
	@RefNo varchar(30) =NULL ,
	@Hands int =NULL ,
	@Price numeric(18, 4) =NULL ,
	@ExpiringDate datetime =NULL ,
	@AccountName varchar(50) =NULL ,
	@Memo varchar(4000) =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Fin_RepoApplyDetail] (
	[RepoApplyId],
	[StockDetailId],
	[PledgeApplyId],
	[RepoTime],
	[ContractNo],
	[NetAmount],
	[StockId],
	[RefNo],
	[Hands],
	[Price],
	[ExpiringDate],
	[AccountName],
	[Memo],
	[DetailStatus]
) VALUES (
	@RepoApplyId,
	@StockDetailId,
	@PledgeApplyId,
	@RepoTime,
	@ContractNo,
	@NetAmount,
	@StockId,
	@RefNo,
	@Hands,
	@Price,
	@ExpiringDate,
	@AccountName,
	@Memo,
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
// 存储过程名：[dbo].Fin_RepoApplyDetailUpdate
// 存储过程功能描述：更新Fin_RepoApplyDetail
// 创建人：CodeSmith
// 创建时间： 2015年4月21日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyDetailUpdate
    @DetailId int,
@RepoApplyId int = NULL,
@StockDetailId int = NULL,
@PledgeApplyId int = NULL,
@RepoTime datetime = NULL,
@ContractNo varchar(30) = NULL,
@NetAmount numeric(18, 4) = NULL,
@StockId int = NULL,
@RefNo varchar(30) = NULL,
@Hands int = NULL,
@Price numeric(18, 4) = NULL,
@ExpiringDate datetime = NULL,
@AccountName varchar(50) = NULL,
@Memo varchar(4000) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Fin_RepoApplyDetail] SET
	[RepoApplyId] = @RepoApplyId,
	[StockDetailId] = @StockDetailId,
	[PledgeApplyId] = @PledgeApplyId,
	[RepoTime] = @RepoTime,
	[ContractNo] = @ContractNo,
	[NetAmount] = @NetAmount,
	[StockId] = @StockId,
	[RefNo] = @RefNo,
	[Hands] = @Hands,
	[Price] = @Price,
	[ExpiringDate] = @ExpiringDate,
	[AccountName] = @AccountName,
	[Memo] = @Memo,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



