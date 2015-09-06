alter table St_PledgeApplyDetail
   drop constraint PK_ST_PLEDGEAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_PledgeApplyDetail')
            and   type = 'U')
   drop table St_PledgeApplyDetail
go

/*==============================================================*/
/* Table: St_PledgeApplyDetail                                  */
/*==============================================================*/
create table St_PledgeApplyDetail (
   DetailId             int                  identity,
   PledgeApplyId        int                  not null,
   StockId              int                  null,
   DetailStatus         int                  null,
   ApplyQty             numeric(18,4)        null,
   UintId               int                  null,
   PledgePrice          numeric(18,4)        null,
   CurrencyId           int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '质押申请明细',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请序号',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'PledgeApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请数量',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'ApplyQty'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'UintId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押价格',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'PledgePrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'St_PledgeApplyDetail', 'column', 'CurrencyId'
go

alter table St_PledgeApplyDetail
   add constraint PK_ST_PLEDGEAPPLYDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_PledgeApplyDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeApplyDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeApplyDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeApplyDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_PledgeApplyDetailUpdateStatus
// 存储过程功能描述：更新St_PledgeApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_PledgeApplyDetail'

set @str = 'update [dbo].[St_PledgeApplyDetail] '+
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
// 存储过程名：[dbo].St_PledgeApplyDetailGoBack
// 存储过程功能描述：撤返St_PledgeApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_PledgeApplyDetail'

set @str = 'update [dbo].[St_PledgeApplyDetail] '+
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
// 存储过程名：[dbo].St_PledgeApplyDetailGet
// 存储过程功能描述：查询指定St_PledgeApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[PledgeApplyId],
	[StockId],
	[DetailStatus],
	[ApplyQty],
	[UintId],
	[PledgePrice],
	[CurrencyId]
FROM
	[dbo].[St_PledgeApplyDetail]
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
// 存储过程名：[dbo].St_PledgeApplyDetailLoad
// 存储过程功能描述：查询所有St_PledgeApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeApplyDetailLoad
AS

SELECT
	[DetailId],
	[PledgeApplyId],
	[StockId],
	[DetailStatus],
	[ApplyQty],
	[UintId],
	[PledgePrice],
	[CurrencyId]
FROM
	[dbo].[St_PledgeApplyDetail]

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
// 存储过程名：[dbo].St_PledgeApplyDetailInsert
// 存储过程功能描述：新增一条St_PledgeApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeApplyDetailInsert
	@PledgeApplyId int ,
	@StockId int =NULL ,
	@DetailStatus int =NULL ,
	@ApplyQty numeric(18, 4) =NULL ,
	@UintId int =NULL ,
	@PledgePrice numeric(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_PledgeApplyDetail] (
	[PledgeApplyId],
	[StockId],
	[DetailStatus],
	[ApplyQty],
	[UintId],
	[PledgePrice],
	[CurrencyId]
) VALUES (
	@PledgeApplyId,
	@StockId,
	@DetailStatus,
	@ApplyQty,
	@UintId,
	@PledgePrice,
	@CurrencyId
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
// 存储过程名：[dbo].St_PledgeApplyDetailUpdate
// 存储过程功能描述：更新St_PledgeApplyDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeApplyDetailUpdate
    @DetailId int,
@PledgeApplyId int,
@StockId int = NULL,
@DetailStatus int = NULL,
@ApplyQty numeric(18, 4) = NULL,
@UintId int = NULL,
@PledgePrice numeric(18, 4) = NULL,
@CurrencyId int = NULL
AS

UPDATE [dbo].[St_PledgeApplyDetail] SET
	[PledgeApplyId] = @PledgeApplyId,
	[StockId] = @StockId,
	[DetailStatus] = @DetailStatus,
	[ApplyQty] = @ApplyQty,
	[UintId] = @UintId,
	[PledgePrice] = @PledgePrice,
	[CurrencyId] = @CurrencyId
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



