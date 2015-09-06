alter table dbo.St_StockMoveDetail
   drop constraint PK_ST_STOCKMOVEDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockMoveDetail')
            and   type = 'U')
   drop table dbo.St_StockMoveDetail
go

/*==============================================================*/
/* Table: St_StockMoveDetail                                    */
/*==============================================================*/
create table dbo.St_StockMoveDetail (
   DetailId             int                  identity,
   StockMoveId          int                  null,
   MoveDetailStatus     int                  null,
   StockId              int                  null,
   PaperNo              varchar(80)          null,
   DeliverPlaceId       int                  null,
   StockLogId           int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '移库明细',
   'user', 'dbo', 'table', 'St_StockMoveDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'St_StockMoveDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库序号',
   'user', 'dbo', 'table', 'St_StockMoveDetail', 'column', 'StockMoveId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库明细状态',
   'user', 'dbo', 'table', 'St_StockMoveDetail', 'column', 'MoveDetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存编号',
   'user', 'dbo', 'table', 'St_StockMoveDetail', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '权证编号',
   'user', 'dbo', 'table', 'St_StockMoveDetail', 'column', 'PaperNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地',
   'user', 'dbo', 'table', 'St_StockMoveDetail', 'column', 'DeliverPlaceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库流水序号',
   'user', 'dbo', 'table', 'St_StockMoveDetail', 'column', 'StockLogId'
go

alter table dbo.St_StockMoveDetail
   add constraint PK_ST_STOCKMOVEDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].St_StockMoveDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockMoveDetailUpdateStatus
// 存储过程功能描述：更新St_StockMoveDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMoveDetail'

set @str = 'update [dbo].[St_StockMoveDetail] '+
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
// 存储过程名：[dbo].St_StockMoveDetailGoBack
// 存储过程功能描述：撤返St_StockMoveDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMoveDetail'

set @str = 'update [dbo].[St_StockMoveDetail] '+
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
// 存储过程名：[dbo].St_StockMoveDetailGet
// 存储过程功能描述：查询指定St_StockMoveDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[StockMoveId],
	[MoveDetailStatus],
	[StockId],
	[PaperNo],
	[DeliverPlaceId],
	[StockLogId]
FROM
	[dbo].[St_StockMoveDetail]
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
// 存储过程名：[dbo].St_StockMoveDetailLoad
// 存储过程功能描述：查询所有St_StockMoveDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveDetailLoad
AS

SELECT
	[DetailId],
	[StockMoveId],
	[MoveDetailStatus],
	[StockId],
	[PaperNo],
	[DeliverPlaceId],
	[StockLogId]
FROM
	[dbo].[St_StockMoveDetail]

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
// 存储过程名：[dbo].St_StockMoveDetailInsert
// 存储过程功能描述：新增一条St_StockMoveDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveDetailInsert
	@StockMoveId int =NULL ,
	@MoveDetailStatus int =NULL ,
	@StockId int =NULL ,
	@PaperNo varchar(80) =NULL ,
	@DeliverPlaceId int =NULL ,
	@StockLogId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_StockMoveDetail] (
	[StockMoveId],
	[MoveDetailStatus],
	[StockId],
	[PaperNo],
	[DeliverPlaceId],
	[StockLogId]
) VALUES (
	@StockMoveId,
	@MoveDetailStatus,
	@StockId,
	@PaperNo,
	@DeliverPlaceId,
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
// 存储过程名：[dbo].St_StockMoveDetailUpdate
// 存储过程功能描述：更新St_StockMoveDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveDetailUpdate
    @DetailId int,
@StockMoveId int = NULL,
@MoveDetailStatus int = NULL,
@StockId int = NULL,
@PaperNo varchar(80) = NULL,
@DeliverPlaceId int = NULL,
@StockLogId int = NULL
AS

UPDATE [dbo].[St_StockMoveDetail] SET
	[StockMoveId] = @StockMoveId,
	[MoveDetailStatus] = @MoveDetailStatus,
	[StockId] = @StockId,
	[PaperNo] = @PaperNo,
	[DeliverPlaceId] = @DeliverPlaceId,
	[StockLogId] = @StockLogId
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



