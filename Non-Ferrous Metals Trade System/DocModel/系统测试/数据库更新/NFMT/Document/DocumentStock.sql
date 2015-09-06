alter table dbo.Doc_DocumentStock
   drop constraint PK_DOC_DOCUMENTSTOCK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_DocumentStock')
            and   type = 'U')
   drop table dbo.Doc_DocumentStock
go

/*==============================================================*/
/* Table: Doc_DocumentStock                                     */
/*==============================================================*/
create table dbo.Doc_DocumentStock (
   DetailId             int                  identity,
   DocumentId           int                  null,
   OrderId              int                  null,
   OrderStockDetailId   int                  null,
   StockId              int                  null,
   StockNameId          int                  null,
   RefNo                varchar(200)         null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单库存明细',
   'user', 'dbo', 'table', 'Doc_DocumentStock'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '制单序号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'DocumentId'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令序号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令库存明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'OrderStockDetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务单序号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'StockNameId'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'RefNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '关联数据状态',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Doc_DocumentStock', 'column', 'LastModifyTime'
go

alter table dbo.Doc_DocumentStock
   add constraint PK_DOC_DOCUMENTSTOCK primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentStockGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentStockGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentStockGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentStockLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentStockLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentStockLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentStockInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentStockInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentStockInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentStockUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentStockUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentStockUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentStockUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentStockUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentStockUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentStockUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentStockGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentStockGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentStockUpdateStatus
// 存储过程功能描述：更新Doc_DocumentStock中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentStockUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentStock'

set @str = 'update [dbo].[Doc_DocumentStock] '+
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
// 存储过程名：[dbo].Doc_DocumentStockGoBack
// 存储过程功能描述：撤返Doc_DocumentStock，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentStockGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentStock'

set @str = 'update [dbo].[Doc_DocumentStock] '+
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
// 存储过程名：[dbo].Doc_DocumentStockGet
// 存储过程功能描述：查询指定Doc_DocumentStock的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentStockGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[OrderStockDetailId],
	[StockId],
	[StockNameId],
	[RefNo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentStock]
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
// 存储过程名：[dbo].Doc_DocumentStockLoad
// 存储过程功能描述：查询所有Doc_DocumentStock记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentStockLoad
AS

SELECT
	[DetailId],
	[DocumentId],
	[OrderId],
	[OrderStockDetailId],
	[StockId],
	[StockNameId],
	[RefNo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentStock]

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
// 存储过程名：[dbo].Doc_DocumentStockInsert
// 存储过程功能描述：新增一条Doc_DocumentStock记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentStockInsert
	@DocumentId int =NULL ,
	@OrderId int =NULL ,
	@OrderStockDetailId int =NULL ,
	@StockId int =NULL ,
	@StockNameId int =NULL ,
	@RefNo varchar(200) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentStock] (
	[DocumentId],
	[OrderId],
	[OrderStockDetailId],
	[StockId],
	[StockNameId],
	[RefNo],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@DocumentId,
	@OrderId,
	@OrderStockDetailId,
	@StockId,
	@StockNameId,
	@RefNo,
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
// 存储过程名：[dbo].Doc_DocumentStockUpdate
// 存储过程功能描述：更新Doc_DocumentStock
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentStockUpdate
    @DetailId int,
@DocumentId int = NULL,
@OrderId int = NULL,
@OrderStockDetailId int = NULL,
@StockId int = NULL,
@StockNameId int = NULL,
@RefNo varchar(200) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentStock] SET
	[DocumentId] = @DocumentId,
	[OrderId] = @OrderId,
	[OrderStockDetailId] = @OrderStockDetailId,
	[StockId] = @StockId,
	[StockNameId] = @StockNameId,
	[RefNo] = @RefNo,
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



