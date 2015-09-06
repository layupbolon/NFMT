alter table dbo.Doc_DocumentOrderInvoice
   drop constraint PK_DOC_DOCUMENTORDERINVOICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_DocumentOrderInvoice')
            and   type = 'U')
   drop table dbo.Doc_DocumentOrderInvoice
go

/*==============================================================*/
/* Table: Doc_DocumentOrderInvoice                              */
/*==============================================================*/
create table dbo.Doc_DocumentOrderInvoice (
   DetailId             int                  identity,
   OrderId              int                  null,
   StockDetailId        int                  null,
   InvoiceNo            varchar(200)         null,
   InvoiceBala          decimal(18,4)        null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单指令发票明细',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'StockDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'InvoiceNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票金额',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'InvoiceBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrderInvoice', 'column', 'LastModifyTime'
go

alter table dbo.Doc_DocumentOrderInvoice
   add constraint PK_DOC_DOCUMENTORDERINVOICE primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderInvoiceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderInvoiceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderInvoiceGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderInvoiceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderInvoiceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderInvoiceLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderInvoiceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderInvoiceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderInvoiceInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderInvoiceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderInvoiceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderInvoiceUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderInvoiceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderInvoiceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderInvoiceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderInvoiceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderInvoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderInvoiceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderInvoice'

set @str = 'update [dbo].[Doc_DocumentOrderInvoice] '+
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
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderInvoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderInvoiceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderInvoice'

set @str = 'update [dbo].[Doc_DocumentOrderInvoice] '+
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
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceGet
// 存储过程功能描述：查询指定Doc_DocumentOrderInvoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderInvoiceGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[OrderId],
	[StockDetailId],
	[InvoiceNo],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderInvoice]
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
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderInvoiceLoad
AS

SELECT
	[DetailId],
	[OrderId],
	[StockDetailId],
	[InvoiceNo],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderInvoice]

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
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderInvoiceInsert
	@OrderId int =NULL ,
	@StockDetailId int =NULL ,
	@InvoiceNo varchar(200) =NULL ,
	@InvoiceBala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderInvoice] (
	[OrderId],
	[StockDetailId],
	[InvoiceNo],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderId,
	@StockDetailId,
	@InvoiceNo,
	@InvoiceBala,
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
// 存储过程名：[dbo].Doc_DocumentOrderInvoiceUpdate
// 存储过程功能描述：更新Doc_DocumentOrderInvoice
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderInvoiceUpdate
    @DetailId int,
@OrderId int = NULL,
@StockDetailId int = NULL,
@InvoiceNo varchar(200) = NULL,
@InvoiceBala decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderInvoice] SET
	[OrderId] = @OrderId,
	[StockDetailId] = @StockDetailId,
	[InvoiceNo] = @InvoiceNo,
	[InvoiceBala] = @InvoiceBala,
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



