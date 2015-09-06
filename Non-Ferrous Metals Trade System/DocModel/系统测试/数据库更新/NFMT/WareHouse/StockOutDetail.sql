alter table dbo.St_StockOutDetail
   drop constraint PK_ST_STOCKOUTDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockOutDetail')
            and   type = 'U')
   drop table dbo.St_StockOutDetail
go

/*==============================================================*/
/* Table: St_StockOutDetail                                     */
/*==============================================================*/
create table dbo.St_StockOutDetail (
   DetailId             int                  identity,
   StockOutId           int                  not null,
   DetailStatus         int                  null,
   StockId              int                  null,
   StockOutApplyDetailId int                  null,
   NetAmount            decimal(18,4)        null,
   StockLogId           int                  null,
   GrossAmount          decimal(18,4)        null,
   Bundles              int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '出库明细',
   'user', 'dbo', 'table', 'St_StockOutDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库序号',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'StockOutId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库明细状态',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库申请明细序号',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'StockOutApplyDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库净量',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库流水序号',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'StockLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库毛重',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'GrossAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '捆数',
   'user', 'dbo', 'table', 'St_StockOutDetail', 'column', 'Bundles'
go

alter table dbo.St_StockOutDetail
   add constraint PK_ST_STOCKOUTDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].St_StockOutDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockOutDetailUpdateStatus
// 存储过程功能描述：更新St_StockOutDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutDetail'

set @str = 'update [dbo].[St_StockOutDetail] '+
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
// 存储过程名：[dbo].St_StockOutDetailGoBack
// 存储过程功能描述：撤返St_StockOutDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOutDetail'

set @str = 'update [dbo].[St_StockOutDetail] '+
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
// 存储过程名：[dbo].St_StockOutDetailGet
// 存储过程功能描述：查询指定St_StockOutDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[StockOutId],
	[DetailStatus],
	[StockId],
	[StockOutApplyDetailId],
	[NetAmount],
	[StockLogId],
	[GrossAmount],
	[Bundles]
FROM
	[dbo].[St_StockOutDetail]
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
// 存储过程名：[dbo].St_StockOutDetailLoad
// 存储过程功能描述：查询所有St_StockOutDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutDetailLoad
AS

SELECT
	[DetailId],
	[StockOutId],
	[DetailStatus],
	[StockId],
	[StockOutApplyDetailId],
	[NetAmount],
	[StockLogId],
	[GrossAmount],
	[Bundles]
FROM
	[dbo].[St_StockOutDetail]

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
// 存储过程名：[dbo].St_StockOutDetailInsert
// 存储过程功能描述：新增一条St_StockOutDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutDetailInsert
	@StockOutId int ,
	@DetailStatus int =NULL ,
	@StockId int =NULL ,
	@StockOutApplyDetailId int =NULL ,
	@NetAmount decimal(18, 4) =NULL ,
	@StockLogId int =NULL ,
	@GrossAmount decimal(18, 4) =NULL ,
	@Bundles int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_StockOutDetail] (
	[StockOutId],
	[DetailStatus],
	[StockId],
	[StockOutApplyDetailId],
	[NetAmount],
	[StockLogId],
	[GrossAmount],
	[Bundles]
) VALUES (
	@StockOutId,
	@DetailStatus,
	@StockId,
	@StockOutApplyDetailId,
	@NetAmount,
	@StockLogId,
	@GrossAmount,
	@Bundles
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
// 存储过程名：[dbo].St_StockOutDetailUpdate
// 存储过程功能描述：更新St_StockOutDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutDetailUpdate
    @DetailId int,
@StockOutId int,
@DetailStatus int = NULL,
@StockId int = NULL,
@StockOutApplyDetailId int = NULL,
@NetAmount decimal(18, 4) = NULL,
@StockLogId int = NULL,
@GrossAmount decimal(18, 4) = NULL,
@Bundles int = NULL
AS

UPDATE [dbo].[St_StockOutDetail] SET
	[StockOutId] = @StockOutId,
	[DetailStatus] = @DetailStatus,
	[StockId] = @StockId,
	[StockOutApplyDetailId] = @StockOutApplyDetailId,
	[NetAmount] = @NetAmount,
	[StockLogId] = @StockLogId,
	[GrossAmount] = @GrossAmount,
	[Bundles] = @Bundles
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



