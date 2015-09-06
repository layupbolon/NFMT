alter table dbo.Doc_DocumentInvoice
   drop constraint PK_DOC_DOCUMENTINVOICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_DocumentInvoice')
            and   type = 'U')
   drop table dbo.Doc_DocumentInvoice
go

/*==============================================================*/
/* Table: Doc_DocumentInvoice                                   */
/*==============================================================*/
create table dbo.Doc_DocumentInvoice (
   DetailId             int                  identity,
   DocumentId           int                  null,
   OrderId              int                  null,
   StockDetailId        int                  null,
   OrderInvoiceDetailId int                  null,
   InvoiceNo            varchar(200)         null,
   InvoiceId            int                  null,
   InvoiceBala          decimal(18,4)        null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单发票明细',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单序号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'DocumentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令序号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'StockDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令发票明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'OrderInvoiceDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'InvoiceNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票序号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票金额',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'InvoiceBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Doc_DocumentInvoice', 'column', 'LastModifyTime'
go

alter table dbo.Doc_DocumentInvoice
   add constraint PK_DOC_DOCUMENTINVOICE primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentInvoiceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentInvoiceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentInvoiceGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentInvoiceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentInvoiceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentInvoiceLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentInvoiceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentInvoiceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentInvoiceInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentInvoiceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentInvoiceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentInvoiceUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentInvoiceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentInvoiceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentInvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentInvoiceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentInvoiceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentInvoiceUpdateStatus
// 存储过程功能描述：更新Doc_DocumentInvoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentInvoiceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentInvoice'

set @str = 'update [dbo].[Doc_DocumentInvoice] '+
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
// 存储过程名：[dbo].Doc_DocumentInvoiceGoBack
// 存储过程功能描述：撤返Doc_DocumentInvoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentInvoiceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentInvoice'

set @str = 'update [dbo].[Doc_DocumentInvoice] '+
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
// 存储过程名：[dbo].Doc_DocumentInvoiceGet
// 存储过程功能描述：查询指定Doc_DocumentInvoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentInvoiceGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[StockDetailId],
	[OrderInvoiceDetailId],
	[InvoiceNo],
	[InvoiceId],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentInvoice]
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
// 存储过程名：[dbo].Doc_DocumentInvoiceLoad
// 存储过程功能描述：查询所有Doc_DocumentInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentInvoiceLoad
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[StockDetailId],
	[OrderInvoiceDetailId],
	[InvoiceNo],
	[InvoiceId],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentInvoice]

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
// 存储过程名：[dbo].Doc_DocumentInvoiceInsert
// 存储过程功能描述：新增一条Doc_DocumentInvoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentInvoiceInsert
	@DocumentId int =NULL ,
	@OrderId int =NULL ,
	@StockDetailId int =NULL ,
	@OrderInvoiceDetailId int =NULL ,
	@InvoiceNo varchar(200) =NULL ,
	@InvoiceId int =NULL ,
	@InvoiceBala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentInvoice] (
	[DocumentId],
	[OrderId],
	[StockDetailId],
	[OrderInvoiceDetailId],
	[InvoiceNo],
	[InvoiceId],
	[InvoiceBala],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DocumentId,
	@OrderId,
	@StockDetailId,
	@OrderInvoiceDetailId,
	@InvoiceNo,
	@InvoiceId,
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
// 存储过程名：[dbo].Doc_DocumentInvoiceUpdate
// 存储过程功能描述：更新Doc_DocumentInvoice
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentInvoiceUpdate
    @DetailId int,
@DocumentId int = NULL,
@OrderId int = NULL,
@StockDetailId int = NULL,
@OrderInvoiceDetailId int = NULL,
@InvoiceNo varchar(200) = NULL,
@InvoiceId int = NULL,
@InvoiceBala decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentInvoice] SET
	[DocumentId] = @DocumentId,
	[OrderId] = @OrderId,
	[StockDetailId] = @StockDetailId,
	[OrderInvoiceDetailId] = @OrderInvoiceDetailId,
	[InvoiceNo] = @InvoiceNo,
	[InvoiceId] = @InvoiceId,
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



