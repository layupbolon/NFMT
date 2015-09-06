alter table St_CustomsDetail
   drop constraint PK_ST_CUSTOMSDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_CustomsDetail')
            and   type = 'U')
   drop table St_CustomsDetail
go

/*==============================================================*/
/* Table: St_CustomsDetail                                      */
/*==============================================================*/
create table St_CustomsDetail (
   DetailId             int                  identity,
   CustomsId            int                  null,
   CustomsApplyId       int                  null,
   CustomsApplyDetailId int                  null,
   StockId              int                  null,
   GrossWeight          decimal(18,4)        null,
   NetWeight            decimal(18,4)        null,
   CustomsPrice         decimal(18,4)        null,
   DeliverPlaceId       int                  null,
   CardNo               varchar(50)          null,
   DetailStatus         int                  null,
   StockLogId           int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '报关明细',
   'user', @CurrentUser, 'table', 'St_CustomsDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关明细序号',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关序号',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'CustomsId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请序号',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'CustomsApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请明细序号',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'CustomsApplyDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关毛重',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'GrossWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关净重',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'NetWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实际报关单价',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'CustomsPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '交货地',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'DeliverPlaceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '卡号',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'CardNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关流水序号',
   'user', @CurrentUser, 'table', 'St_CustomsDetail', 'column', 'StockLogId'
go

alter table St_CustomsDetail
   add constraint PK_ST_CUSTOMSDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_CustomsDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_CustomsDetailUpdateStatus
// 存储过程功能描述：更新St_CustomsDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsDetail'

set @str = 'update [dbo].[St_CustomsDetail] '+
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
// 存储过程名：[dbo].St_CustomsDetailGoBack
// 存储过程功能描述：撤返St_CustomsDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsDetail'

set @str = 'update [dbo].[St_CustomsDetail] '+
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
// 存储过程名：[dbo].St_CustomsDetailGet
// 存储过程功能描述：查询指定St_CustomsDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[CustomsId],
	[CustomsApplyId],
	[CustomsApplyDetailId],
	[StockId],
	[GrossWeight],
	[NetWeight],
	[CustomsPrice],
	[DeliverPlaceId],
	[CardNo],
	[DetailStatus],
	[StockLogId]
FROM
	[dbo].[St_CustomsDetail]
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
// 存储过程名：[dbo].St_CustomsDetailLoad
// 存储过程功能描述：查询所有St_CustomsDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsDetailLoad
AS

SELECT
	[DetailId],
	[CustomsId],
	[CustomsApplyId],
	[CustomsApplyDetailId],
	[StockId],
	[GrossWeight],
	[NetWeight],
	[CustomsPrice],
	[DeliverPlaceId],
	[CardNo],
	[DetailStatus],
	[StockLogId]
FROM
	[dbo].[St_CustomsDetail]

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
// 存储过程名：[dbo].St_CustomsDetailInsert
// 存储过程功能描述：新增一条St_CustomsDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsDetailInsert
	@CustomsId int =NULL ,
	@CustomsApplyId int =NULL ,
	@CustomsApplyDetailId int =NULL ,
	@StockId int =NULL ,
	@GrossWeight decimal(18, 4) =NULL ,
	@NetWeight decimal(18, 4) =NULL ,
	@CustomsPrice decimal(18, 4) =NULL ,
	@DeliverPlaceId int =NULL ,
	@CardNo varchar(50) =NULL ,
	@DetailStatus int =NULL ,
	@StockLogId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_CustomsDetail] (
	[CustomsId],
	[CustomsApplyId],
	[CustomsApplyDetailId],
	[StockId],
	[GrossWeight],
	[NetWeight],
	[CustomsPrice],
	[DeliverPlaceId],
	[CardNo],
	[DetailStatus],
	[StockLogId]
) VALUES (
	@CustomsId,
	@CustomsApplyId,
	@CustomsApplyDetailId,
	@StockId,
	@GrossWeight,
	@NetWeight,
	@CustomsPrice,
	@DeliverPlaceId,
	@CardNo,
	@DetailStatus,
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
// 存储过程名：[dbo].St_CustomsDetailUpdate
// 存储过程功能描述：更新St_CustomsDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsDetailUpdate
    @DetailId int,
@CustomsId int = NULL,
@CustomsApplyId int = NULL,
@CustomsApplyDetailId int = NULL,
@StockId int = NULL,
@GrossWeight decimal(18, 4) = NULL,
@NetWeight decimal(18, 4) = NULL,
@CustomsPrice decimal(18, 4) = NULL,
@DeliverPlaceId int = NULL,
@CardNo varchar(50) = NULL,
@DetailStatus int = NULL,
@StockLogId int = NULL
AS

UPDATE [dbo].[St_CustomsDetail] SET
	[CustomsId] = @CustomsId,
	[CustomsApplyId] = @CustomsApplyId,
	[CustomsApplyDetailId] = @CustomsApplyDetailId,
	[StockId] = @StockId,
	[GrossWeight] = @GrossWeight,
	[NetWeight] = @NetWeight,
	[CustomsPrice] = @CustomsPrice,
	[DeliverPlaceId] = @DeliverPlaceId,
	[CardNo] = @CardNo,
	[DetailStatus] = @DetailStatus,
	[StockLogId] = @StockLogId
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



