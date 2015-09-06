alter table dbo.St_StockMoveApplyDetail
   drop constraint PK_ST_STOCKMOVEAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockMoveApplyDetail')
            and   type = 'U')
   drop table dbo.St_StockMoveApplyDetail
go

/*==============================================================*/
/* Table: St_StockMoveApplyDetail                               */
/*==============================================================*/
create table dbo.St_StockMoveApplyDetail (
   DetailId             int                  identity,
   StockMoveApplyId     int                  not null,
   StockId              int                  null,
   PaperNo              varchar(80)          null,
   DeliverPlaceId       int                  null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '回购申请库存明细',
   'user', 'dbo', 'table', 'St_StockMoveApplyDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'St_StockMoveApplyDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库申请序号',
   'user', 'dbo', 'table', 'St_StockMoveApplyDetail', 'column', 'StockMoveApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'St_StockMoveApplyDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '权证编号',
   'user', 'dbo', 'table', 'St_StockMoveApplyDetail', 'column', 'PaperNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地',
   'user', 'dbo', 'table', 'St_StockMoveApplyDetail', 'column', 'DeliverPlaceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库申请明细状态',
   'user', 'dbo', 'table', 'St_StockMoveApplyDetail', 'column', 'DetailStatus'
go

alter table dbo.St_StockMoveApplyDetail
   add constraint PK_ST_STOCKMOVEAPPLYDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_StockMoveApplyDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveApplyDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveApplyDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveApplyDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockMoveApplyDetailUpdateStatus
// 存储过程功能描述：更新St_StockMoveApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMoveApplyDetail'

set @str = 'update [dbo].[St_StockMoveApplyDetail] '+
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
// 存储过程名：[dbo].St_StockMoveApplyDetailGoBack
// 存储过程功能描述：撤返St_StockMoveApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMoveApplyDetail'

set @str = 'update [dbo].[St_StockMoveApplyDetail] '+
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
// 存储过程名：[dbo].St_StockMoveApplyDetailGet
// 存储过程功能描述：查询指定St_StockMoveApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[StockMoveApplyId],
	[StockId],
	[PaperNo],
	[DeliverPlaceId],
	[DetailStatus]
FROM
	[dbo].[St_StockMoveApplyDetail]
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
// 存储过程名：[dbo].St_StockMoveApplyDetailLoad
// 存储过程功能描述：查询所有St_StockMoveApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveApplyDetailLoad
AS

SELECT
	[DetailId],
	[StockMoveApplyId],
	[StockId],
	[PaperNo],
	[DeliverPlaceId],
	[DetailStatus]
FROM
	[dbo].[St_StockMoveApplyDetail]

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
// 存储过程名：[dbo].St_StockMoveApplyDetailInsert
// 存储过程功能描述：新增一条St_StockMoveApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveApplyDetailInsert
	@StockMoveApplyId int ,
	@StockId int =NULL ,
	@PaperNo varchar(80) =NULL ,
	@DeliverPlaceId int =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_StockMoveApplyDetail] (
	[StockMoveApplyId],
	[StockId],
	[PaperNo],
	[DeliverPlaceId],
	[DetailStatus]
) VALUES (
	@StockMoveApplyId,
	@StockId,
	@PaperNo,
	@DeliverPlaceId,
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
// 存储过程名：[dbo].St_StockMoveApplyDetailUpdate
// 存储过程功能描述：更新St_StockMoveApplyDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveApplyDetailUpdate
    @DetailId int,
@StockMoveApplyId int,
@StockId int = NULL,
@PaperNo varchar(80) = NULL,
@DeliverPlaceId int = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[St_StockMoveApplyDetail] SET
	[StockMoveApplyId] = @StockMoveApplyId,
	[StockId] = @StockId,
	[PaperNo] = @PaperNo,
	[DeliverPlaceId] = @DeliverPlaceId,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



