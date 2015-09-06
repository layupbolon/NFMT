alter table dbo.Invoice
   drop constraint PK_INVOICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Invoice')
            and   type = 'U')
   drop table dbo.Invoice
go

/*==============================================================*/
/* Table: Invoice                                               */
/*==============================================================*/
create table dbo.Invoice (
   InvoiceId            int                  identity,
   InvoiceDate          datetime             null,
   InvoiceNo            varchar(200)         null,
   InvoiceName          varchar(80)          null,
   InvoiceType          int                  null,
   InvoiceBala          numeric(18,4)        null,
   CurrencyId           int                  null,
   InvoiceDirection     int                  null,
   OutBlocId            int                  null,
   OutCorpId            int                  null,
   OutCorpName          varchar(200)         null,
   InBlocId             int                  null,
   InCorpId             int                  null,
   InCorpName           varchar(200)         null,
   InvoiceStatus        int                  null,
   Memo                 varchar(800)         null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '发票',
   'user', 'dbo', 'table', 'Invoice'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票序号',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '开票日期',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票编号',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '实际票据号',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceName'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票类型',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceType'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票金额',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票币种',
   'user', 'dbo', 'table', 'Invoice', 'column', 'CurrencyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票方向(开具/收取)',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceDirection'
go

execute sp_addextendedproperty 'MS_Description', 
   '开票集团',
   'user', 'dbo', 'table', 'Invoice', 'column', 'OutBlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '开票公司',
   'user', 'dbo', 'table', 'Invoice', 'column', 'OutCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '开票公司名称',
   'user', 'dbo', 'table', 'Invoice', 'column', 'OutCorpName'
go

execute sp_addextendedproperty 'MS_Description', 
   '收票集团',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InBlocId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收票公司',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InCorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '收票公司名称',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InCorpName'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票状态',
   'user', 'dbo', 'table', 'Invoice', 'column', 'InvoiceStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Invoice', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'Invoice', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Invoice', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Invoice', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Invoice', 'column', 'LastModifyTime'
go

alter table dbo.Invoice
   add constraint PK_INVOICE primary key (InvoiceId)
go


/****** Object:  Stored Procedure [dbo].InvoiceGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceGet]
GO

/****** Object:  Stored Procedure [dbo].InvoiceLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceLoad]
GO

/****** Object:  Stored Procedure [dbo].InvoiceInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceInsert]
GO

/****** Object:  Stored Procedure [dbo].InvoiceUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceUpdate]
GO

/****** Object:  Stored Procedure [dbo].InvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].InvoiceUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InvoiceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InvoiceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].InvoiceUpdateStatus
// 存储过程功能描述：更新Invoice中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Invoice'

set @str = 'update [dbo].[Invoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where InvoiceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].InvoiceGoBack
// 存储过程功能描述：撤返Invoice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Invoice'

set @str = 'update [dbo].[Invoice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where InvoiceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].InvoiceGet
// 存储过程功能描述：查询指定Invoice的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceGet
    /*
	@InvoiceId int
    */
    @id int
AS

SELECT
	[InvoiceId],
	[InvoiceDate],
	[InvoiceNo],
	[InvoiceName],
	[InvoiceType],
	[InvoiceBala],
	[CurrencyId],
	[InvoiceDirection],
	[OutBlocId],
	[OutCorpId],
	[OutCorpName],
	[InBlocId],
	[InCorpId],
	[InCorpName],
	[InvoiceStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Invoice]
WHERE
	[InvoiceId] = @id

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
// 存储过程名：[dbo].InvoiceLoad
// 存储过程功能描述：查询所有Invoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceLoad
AS

SELECT
	[InvoiceId],
	[InvoiceDate],
	[InvoiceNo],
	[InvoiceName],
	[InvoiceType],
	[InvoiceBala],
	[CurrencyId],
	[InvoiceDirection],
	[OutBlocId],
	[OutCorpId],
	[OutCorpName],
	[InBlocId],
	[InCorpId],
	[InCorpName],
	[InvoiceStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Invoice]

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
// 存储过程名：[dbo].InvoiceInsert
// 存储过程功能描述：新增一条Invoice记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceInsert
	@InvoiceDate datetime =NULL ,
	@InvoiceNo varchar(200) =NULL ,
	@InvoiceName varchar(80) =NULL ,
	@InvoiceType int =NULL ,
	@InvoiceBala numeric(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@InvoiceDirection int =NULL ,
	@OutBlocId int =NULL ,
	@OutCorpId int =NULL ,
	@OutCorpName varchar(200) =NULL ,
	@InBlocId int =NULL ,
	@InCorpId int =NULL ,
	@InCorpName varchar(200) =NULL ,
	@InvoiceStatus int =NULL ,
	@Memo varchar(800) =NULL ,
	@CreatorId int =NULL ,
	@InvoiceId int OUTPUT
AS

INSERT INTO [dbo].[Invoice] (
	[InvoiceDate],
	[InvoiceNo],
	[InvoiceName],
	[InvoiceType],
	[InvoiceBala],
	[CurrencyId],
	[InvoiceDirection],
	[OutBlocId],
	[OutCorpId],
	[OutCorpName],
	[InBlocId],
	[InCorpId],
	[InCorpName],
	[InvoiceStatus],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@InvoiceDate,
	@InvoiceNo,
	@InvoiceName,
	@InvoiceType,
	@InvoiceBala,
	@CurrencyId,
	@InvoiceDirection,
	@OutBlocId,
	@OutCorpId,
	@OutCorpName,
	@InBlocId,
	@InCorpId,
	@InCorpName,
	@InvoiceStatus,
	@Memo,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @InvoiceId = @@IDENTITY
exec CreateInvoiceNo @identity = @InvoiceId
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
// 存储过程名：[dbo].InvoiceUpdate
// 存储过程功能描述：更新Invoice
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].InvoiceUpdate
    @InvoiceId int,
@InvoiceDate datetime = NULL,
@InvoiceNo varchar(200) = NULL,
@InvoiceName varchar(80) = NULL,
@InvoiceType int = NULL,
@InvoiceBala numeric(18, 4) = NULL,
@CurrencyId int = NULL,
@InvoiceDirection int = NULL,
@OutBlocId int = NULL,
@OutCorpId int = NULL,
@OutCorpName varchar(200) = NULL,
@InBlocId int = NULL,
@InCorpId int = NULL,
@InCorpName varchar(200) = NULL,
@InvoiceStatus int = NULL,
@Memo varchar(800) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Invoice] SET
	[InvoiceDate] = @InvoiceDate,
	[InvoiceNo] = @InvoiceNo,
	[InvoiceName] = @InvoiceName,
	[InvoiceType] = @InvoiceType,
	[InvoiceBala] = @InvoiceBala,
	[CurrencyId] = @CurrencyId,
	[InvoiceDirection] = @InvoiceDirection,
	[OutBlocId] = @OutBlocId,
	[OutCorpId] = @OutCorpId,
	[OutCorpName] = @OutCorpName,
	[InBlocId] = @InBlocId,
	[InCorpId] = @InCorpId,
	[InCorpName] = @InCorpName,
	[InvoiceStatus] = @InvoiceStatus,
	[Memo] = @Memo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[InvoiceId] = @InvoiceId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



