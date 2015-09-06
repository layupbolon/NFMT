alter table dbo.Doc_DocumentOrderDetail
   drop constraint PK_DOC_DOCUMENTORDERDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Doc_DocumentOrderDetail')
            and   type = 'U')
   drop table dbo.Doc_DocumentOrderDetail
go

/*==============================================================*/
/* Table: Doc_DocumentOrderDetail                               */
/*==============================================================*/
create table dbo.Doc_DocumentOrderDetail (
   DetailId             int                  identity,
   OrderId              int                  null,
   InvoiceCopies        int                  null,
   InvoiceSpecific      varchar(2000)        null,
   QualityCopies        int                  null,
   QualitySpecific      varchar(2000)        null,
   WeightCopies         int                  null,
   WeightSpecific       varchar(2000)        null,
   TexCopies            int                  null,
   TexSpecific          varchar(2000)        null,
   DeliverCopies        int                  null,
   DeliverSpecific      varchar(2000)        null,
   TotalInvCopies       int                  null,
   TotalInvSpecific     varchar(2000)        null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '制单指令明细',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'DetailId'
go

execute sp_addextendedproperty 'MS_Description', 
   '指令序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'OrderId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票份数',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'InvoiceCopies'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票特殊要求',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'InvoiceSpecific'
go

execute sp_addextendedproperty 'MS_Description', 
   '质量证数份数',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'QualityCopies'
go

execute sp_addextendedproperty 'MS_Description', 
   '质量证书特殊要求',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'QualitySpecific'
go

execute sp_addextendedproperty 'MS_Description', 
   '重量证份数',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'WeightCopies'
go

execute sp_addextendedproperty 'MS_Description', 
   '重量证特殊要求',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'WeightSpecific'
go

execute sp_addextendedproperty 'MS_Description', 
   '装箱单份数',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'TexCopies'
go

execute sp_addextendedproperty 'MS_Description', 
   '装箱单特殊要求',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'TexSpecific'
go

execute sp_addextendedproperty 'MS_Description', 
   '产地证明份数',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'DeliverCopies'
go

execute sp_addextendedproperty 'MS_Description', 
   '产地证明特殊要求',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'DeliverSpecific'
go

execute sp_addextendedproperty 'MS_Description', 
   '汇票份数',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'TotalInvCopies'
go

execute sp_addextendedproperty 'MS_Description', 
   '汇票特殊要求',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'TotalInvSpecific'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'DetailStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Doc_DocumentOrderDetail', 'column', 'LastModifyTime'
go

alter table dbo.Doc_DocumentOrderDetail
   add constraint PK_DOC_DOCUMENTORDERDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Doc_DocumentOrderDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Doc_DocumentOrderDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Doc_DocumentOrderDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Doc_DocumentOrderDetailUpdateStatus
// 存储过程功能描述：更新Doc_DocumentOrderDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderDetail'

set @str = 'update [dbo].[Doc_DocumentOrderDetail] '+
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
// 存储过程名：[dbo].Doc_DocumentOrderDetailGoBack
// 存储过程功能描述：撤返Doc_DocumentOrderDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Doc_DocumentOrderDetail'

set @str = 'update [dbo].[Doc_DocumentOrderDetail] '+
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
// 存储过程名：[dbo].Doc_DocumentOrderDetailGet
// 存储过程功能描述：查询指定Doc_DocumentOrderDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[OrderId],
	[InvoiceCopies],
	[InvoiceSpecific],
	[QualityCopies],
	[QualitySpecific],
	[WeightCopies],
	[WeightSpecific],
	[TexCopies],
	[TexSpecific],
	[DeliverCopies],
	[DeliverSpecific],
	[TotalInvCopies],
	[TotalInvSpecific],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderDetail]
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
// 存储过程名：[dbo].Doc_DocumentOrderDetailLoad
// 存储过程功能描述：查询所有Doc_DocumentOrderDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderDetailLoad
AS

SELECT
	[DetailId],
	[OrderId],
	[InvoiceCopies],
	[InvoiceSpecific],
	[QualityCopies],
	[QualitySpecific],
	[WeightCopies],
	[WeightSpecific],
	[TexCopies],
	[TexSpecific],
	[DeliverCopies],
	[DeliverSpecific],
	[TotalInvCopies],
	[TotalInvSpecific],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Doc_DocumentOrderDetail]

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
// 存储过程名：[dbo].Doc_DocumentOrderDetailInsert
// 存储过程功能描述：新增一条Doc_DocumentOrderDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderDetailInsert
	@OrderId int =NULL ,
	@InvoiceCopies int =NULL ,
	@InvoiceSpecific varchar(2000) =NULL ,
	@QualityCopies int =NULL ,
	@QualitySpecific varchar(2000) =NULL ,
	@WeightCopies int =NULL ,
	@WeightSpecific varchar(2000) =NULL ,
	@TexCopies int =NULL ,
	@TexSpecific varchar(2000) =NULL ,
	@DeliverCopies int =NULL ,
	@DeliverSpecific varchar(2000) =NULL ,
	@TotalInvCopies int =NULL ,
	@TotalInvSpecific varchar(2000) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Doc_DocumentOrderDetail] (
	[OrderId],
	[InvoiceCopies],
	[InvoiceSpecific],
	[QualityCopies],
	[QualitySpecific],
	[WeightCopies],
	[WeightSpecific],
	[TexCopies],
	[TexSpecific],
	[DeliverCopies],
	[DeliverSpecific],
	[TotalInvCopies],
	[TotalInvSpecific],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OrderId,
	@InvoiceCopies,
	@InvoiceSpecific,
	@QualityCopies,
	@QualitySpecific,
	@WeightCopies,
	@WeightSpecific,
	@TexCopies,
	@TexSpecific,
	@DeliverCopies,
	@DeliverSpecific,
	@TotalInvCopies,
	@TotalInvSpecific,
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
// 存储过程名：[dbo].Doc_DocumentOrderDetailUpdate
// 存储过程功能描述：更新Doc_DocumentOrderDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Doc_DocumentOrderDetailUpdate
    @DetailId int,
@OrderId int = NULL,
@InvoiceCopies int = NULL,
@InvoiceSpecific varchar(2000) = NULL,
@QualityCopies int = NULL,
@QualitySpecific varchar(2000) = NULL,
@WeightCopies int = NULL,
@WeightSpecific varchar(2000) = NULL,
@TexCopies int = NULL,
@TexSpecific varchar(2000) = NULL,
@DeliverCopies int = NULL,
@DeliverSpecific varchar(2000) = NULL,
@TotalInvCopies int = NULL,
@TotalInvSpecific varchar(2000) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Doc_DocumentOrderDetail] SET
	[OrderId] = @OrderId,
	[InvoiceCopies] = @InvoiceCopies,
	[InvoiceSpecific] = @InvoiceSpecific,
	[QualityCopies] = @QualityCopies,
	[QualitySpecific] = @QualitySpecific,
	[WeightCopies] = @WeightCopies,
	[WeightSpecific] = @WeightSpecific,
	[TexCopies] = @TexCopies,
	[TexSpecific] = @TexSpecific,
	[DeliverCopies] = @DeliverCopies,
	[DeliverSpecific] = @DeliverSpecific,
	[TotalInvCopies] = @TotalInvCopies,
	[TotalInvSpecific] = @TotalInvSpecific,
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



