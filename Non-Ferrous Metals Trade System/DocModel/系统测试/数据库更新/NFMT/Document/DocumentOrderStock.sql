alter table dbo.Doc_DocumentOrderStock
   drop constraint PK_DOC_DOCUMENTORDERSTOCK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_DocumentOrderStock')
            and   type = 'U')
   drop table dbo.Doc_DocumentOrderStock
go

/*==============================================================*/
/* Table: Doc_DocumentOrderStock                                */
/*==============================================================*/
create table dbo.Doc_DocumentOrderStock (
   DetailId             int                  identity,
   ComDetailId          int                  null,
   OrderId              int                  null,
   StockId              int                  null,
   StockNameId          int                  null,
   RefNo                varchar(200)         null,
   ApplyAmount          decimal(18,4)        null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单指令库存明细',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '临票库存序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'ComDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务单序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'StockNameId'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'RefNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请重量',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'ApplyAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联数据状态',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrderStock', 'column', 'LastModifyTime'
go

alter table dbo.Doc_DocumentOrderStock
   add constraint PK_DOC_DOCUMENTORDERSTOCK primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderStockGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderStockGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderStockGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderStockLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderStockLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderStockLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderStockInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderStockInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderStockInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderStockUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderStockUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderStockUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderStockUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderStockUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderStockUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderStockUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderStockGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderStockGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderStockUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderStock中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderStockUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderStock'

set @str = 'update [dbo].[Doc_DocumentOrderStock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
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
// 存储过程名：[dbo].Doc_DocumentOrderStockGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderStock，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderStockGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderStock'

set @str = 'update [dbo].[Doc_DocumentOrderStock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
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
// 存储过程名：[dbo].Doc_DocumentOrderStockGet
// 存储过程功能描述：查询指定Doc_DocumentOrderStock的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderStockGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[ComDetailId],
	[OrderId],
	[StockId],
	[StockNameId],
	[RefNo],
	[ApplyAmount],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderStock]
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
// 存储过程名：[dbo].Doc_DocumentOrderStockLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderStock记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderStockLoad
AS

SELECT
	[DetailId],
	[ComDetailId],
	[OrderId],
	[StockId],
	[StockNameId],
	[RefNo],
	[ApplyAmount],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderStock]

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
// 存储过程名：[dbo].Doc_DocumentOrderStockInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderStock记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderStockInsert
	@ComDetailId int =NULL ,
	@OrderId int =NULL ,
	@StockId int =NULL ,
	@StockNameId int =NULL ,
	@RefNo varchar(200) =NULL ,
	@ApplyAmount decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderStock] (
	[ComDetailId],
	[OrderId],
	[StockId],
	[StockNameId],
	[RefNo],
	[ApplyAmount],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ComDetailId,
	@OrderId,
	@StockId,
	@StockNameId,
	@RefNo,
	@ApplyAmount,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

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
// 存储过程名：[dbo].Doc_DocumentOrderStockUpdate
// 存储过程功能描述：更新Doc_DocumentOrderStock
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderStockUpdate
    @DetailId int,
@ComDetailId int = NULL,
@OrderId int = NULL,
@StockId int = NULL,
@StockNameId int = NULL,
@RefNo varchar(200) = NULL,
@ApplyAmount decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderStock] SET
	[ComDetailId] = @ComDetailId,
	[OrderId] = @OrderId,
	[StockId] = @StockId,
	[StockNameId] = @StockNameId,
	[RefNo] = @RefNo,
	[ApplyAmount] = @ApplyAmount,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



