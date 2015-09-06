alter table St_PledgeDetial
   drop constraint PK_ST_PLEDGEDETIAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_PledgeDetial')
            and   type = 'U')
   drop table St_PledgeDetial
go

/*==============================================================*/
/* Table: St_PledgeDetial                                       */
/*==============================================================*/
create table St_PledgeDetial (
   DetailId             int                  identity,
   PledgeId             int                  null,
   DetailStatus         int                  null,
   PledgeApplyDetailId  int                  null,
   StockId              int                  null,
   GrossAmount          numeric(18,4)        null,
   Unit                 int                  null,
   PledgePrice          numeric(18,4)        null,
   CurrencyId           int                  null,
   StockLogId           int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '质押明细',
   'user', @CurrentUser, 'table', 'St_PledgeDetial'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押序号',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'PledgeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押明细状态',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请明细序号',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'PledgeApplyDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押数量',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'GrossAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'Unit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押价格',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'PledgePrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押流水序号',
   'user', @CurrentUser, 'table', 'St_PledgeDetial', 'column', 'StockLogId'
go

alter table St_PledgeDetial
   add constraint PK_ST_PLEDGEDETIAL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_PledgeDetialGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeDetialGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeDetialGet]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeDetialLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeDetialLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeDetialLoad]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeDetialInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeDetialInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeDetialInsert]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeDetialUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeDetialUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeDetialUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeDetialUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeDetialUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeDetialUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeDetialUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeDetialGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeDetialGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_PledgeDetialUpdateStatus
// 存储过程功能描述：更新St_PledgeDetial中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeDetialUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_PledgeDetial'

set @str = 'update [dbo].[St_PledgeDetial] '+
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
// 存储过程名：[dbo].St_PledgeDetialGoBack
// 存储过程功能描述：撤返St_PledgeDetial，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeDetialGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_PledgeDetial'

set @str = 'update [dbo].[St_PledgeDetial] '+
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
// 存储过程名：[dbo].St_PledgeDetialGet
// 存储过程功能描述：查询指定St_PledgeDetial的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeDetialGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[PledgeId],
	[DetailStatus],
	[PledgeApplyDetailId],
	[StockId],
	[GrossAmount],
	[Unit],
	[PledgePrice],
	[CurrencyId],
	[StockLogId]
FROM
	[dbo].[St_PledgeDetial]
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
// 存储过程名：[dbo].St_PledgeDetialLoad
// 存储过程功能描述：查询所有St_PledgeDetial记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeDetialLoad
AS

SELECT
	[DetailId],
	[PledgeId],
	[DetailStatus],
	[PledgeApplyDetailId],
	[StockId],
	[GrossAmount],
	[Unit],
	[PledgePrice],
	[CurrencyId],
	[StockLogId]
FROM
	[dbo].[St_PledgeDetial]

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
// 存储过程名：[dbo].St_PledgeDetialInsert
// 存储过程功能描述：新增一条St_PledgeDetial记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeDetialInsert
	@PledgeId int =NULL ,
	@DetailStatus int =NULL ,
	@PledgeApplyDetailId int =NULL ,
	@StockId int =NULL ,
	@GrossAmount numeric(18, 4) =NULL ,
	@Unit int =NULL ,
	@PledgePrice numeric(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@StockLogId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_PledgeDetial] (
	[PledgeId],
	[DetailStatus],
	[PledgeApplyDetailId],
	[StockId],
	[GrossAmount],
	[Unit],
	[PledgePrice],
	[CurrencyId],
	[StockLogId]
) VALUES (
	@PledgeId,
	@DetailStatus,
	@PledgeApplyDetailId,
	@StockId,
	@GrossAmount,
	@Unit,
	@PledgePrice,
	@CurrencyId,
	@StockLogId
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
// 存储过程名：[dbo].St_PledgeDetialUpdate
// 存储过程功能描述：更新St_PledgeDetial
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeDetialUpdate
    @DetailId int,
@PledgeId int = NULL,
@DetailStatus int = NULL,
@PledgeApplyDetailId int = NULL,
@StockId int = NULL,
@GrossAmount numeric(18, 4) = NULL,
@Unit int = NULL,
@PledgePrice numeric(18, 4) = NULL,
@CurrencyId int = NULL,
@StockLogId int = NULL
AS

UPDATE [dbo].[St_PledgeDetial] SET
	[PledgeId] = @PledgeId,
	[DetailStatus] = @DetailStatus,
	[PledgeApplyDetailId] = @PledgeApplyDetailId,
	[StockId] = @StockId,
	[GrossAmount] = @GrossAmount,
	[Unit] = @Unit,
	[PledgePrice] = @PledgePrice,
	[CurrencyId] = @CurrencyId,
	[StockLogId] = @StockLogId
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



