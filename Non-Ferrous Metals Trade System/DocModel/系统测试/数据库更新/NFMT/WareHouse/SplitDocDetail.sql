alter table St_SplitDocDetail
   drop constraint PK_ST_SPLITDOCDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_SplitDocDetail')
            and   type = 'U')
   drop table St_SplitDocDetail
go

/*==============================================================*/
/* Table: St_SplitDocDetail                                     */
/*==============================================================*/
create table St_SplitDocDetail (
   DetailId             int                  identity,
   SplitDocId           int                  null,
   DetailStatus         int                  null,
   NewRefNo             varchar(50)          null,
   OldRefNoId           int                  null,
   OldStockId           int                  null,
   GrossAmount          decimal(18,4)        null,
   NetAmount            decimal(18,4)        null,
   UnitId               int                  null,
   AssetId              int                  null,
   Bundles              int                  null,
   BrandId              int                  null,
   PaperNo              varchar(80)          null,
   PaperHolder          int                  null,
   CardNo               varchar(200)         null,
   StockLogId           int                  null,
   Memo                 varchar(4000)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '拆单明细',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单序号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'SplitDocId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '新业务单号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'NewRefNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '原业务单序号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'OldRefNoId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '原库存号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'OldStockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '新单毛重',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'GrossAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '新单净重',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'NetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'UnitId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'AssetId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '捆数',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'Bundles'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '品牌',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'BrandId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权证编号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'PaperNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单据保管人',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'PaperHolder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '卡号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'CardNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单流水序号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'St_SplitDocDetail', 'column', 'LastModifyTime'
go

alter table St_SplitDocDetail
   add constraint PK_ST_SPLITDOCDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_SplitDocDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_SplitDocDetailUpdateStatus
// 存储过程功能描述：更新St_SplitDocDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDocDetail'

set @str = 'update [dbo].[St_SplitDocDetail] '+
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
// 存储过程名：[dbo].St_SplitDocDetailGoBack
// 存储过程功能描述：撤返St_SplitDocDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDocDetail'

set @str = 'update [dbo].[St_SplitDocDetail] '+
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
// 存储过程名：[dbo].St_SplitDocDetailGet
// 存储过程功能描述：查询指定St_SplitDocDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[SplitDocId],
	[DetailStatus],
	[NewRefNo],
	[OldRefNoId],
	[OldStockId],
	[GrossAmount],
	[NetAmount],
	[UnitId],
	[AssetId],
	[Bundles],
	[BrandId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[StockLogId],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_SplitDocDetail]
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
// 存储过程名：[dbo].St_SplitDocDetailLoad
// 存储过程功能描述：查询所有St_SplitDocDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocDetailLoad
AS

SELECT
	[DetailId],
	[SplitDocId],
	[DetailStatus],
	[NewRefNo],
	[OldRefNoId],
	[OldStockId],
	[GrossAmount],
	[NetAmount],
	[UnitId],
	[AssetId],
	[Bundles],
	[BrandId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[StockLogId],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_SplitDocDetail]

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
// 存储过程名：[dbo].St_SplitDocDetailInsert
// 存储过程功能描述：新增一条St_SplitDocDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocDetailInsert
	@SplitDocId int =NULL ,
	@DetailStatus int =NULL ,
	@NewRefNo varchar(50) =NULL ,
	@OldRefNoId int =NULL ,
	@OldStockId int =NULL ,
	@GrossAmount decimal(18, 4) =NULL ,
	@NetAmount decimal(18, 4) =NULL ,
	@UnitId int =NULL ,
	@AssetId int =NULL ,
	@Bundles int =NULL ,
	@BrandId int =NULL ,
	@PaperNo varchar(80) =NULL ,
	@PaperHolder int =NULL ,
	@CardNo varchar(200) =NULL ,
	@StockLogId int =NULL ,
	@Memo varchar(4000) =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_SplitDocDetail] (
	[SplitDocId],
	[DetailStatus],
	[NewRefNo],
	[OldRefNoId],
	[OldStockId],
	[GrossAmount],
	[NetAmount],
	[UnitId],
	[AssetId],
	[Bundles],
	[BrandId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[StockLogId],
	[Memo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@SplitDocId,
	@DetailStatus,
	@NewRefNo,
	@OldRefNoId,
	@OldStockId,
	@GrossAmount,
	@NetAmount,
	@UnitId,
	@AssetId,
	@Bundles,
	@BrandId,
	@PaperNo,
	@PaperHolder,
	@CardNo,
	@StockLogId,
	@Memo,
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
// 存储过程名：[dbo].St_SplitDocDetailUpdate
// 存储过程功能描述：更新St_SplitDocDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocDetailUpdate
    @DetailId int,
@SplitDocId int = NULL,
@DetailStatus int = NULL,
@NewRefNo varchar(50) = NULL,
@OldRefNoId int = NULL,
@OldStockId int = NULL,
@GrossAmount decimal(18, 4) = NULL,
@NetAmount decimal(18, 4) = NULL,
@UnitId int = NULL,
@AssetId int = NULL,
@Bundles int = NULL,
@BrandId int = NULL,
@PaperNo varchar(80) = NULL,
@PaperHolder int = NULL,
@CardNo varchar(200) = NULL,
@StockLogId int = NULL,
@Memo varchar(4000) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_SplitDocDetail] SET
	[SplitDocId] = @SplitDocId,
	[DetailStatus] = @DetailStatus,
	[NewRefNo] = @NewRefNo,
	[OldRefNoId] = @OldRefNoId,
	[OldStockId] = @OldStockId,
	[GrossAmount] = @GrossAmount,
	[NetAmount] = @NetAmount,
	[UnitId] = @UnitId,
	[AssetId] = @AssetId,
	[Bundles] = @Bundles,
	[BrandId] = @BrandId,
	[PaperNo] = @PaperNo,
	[PaperHolder] = @PaperHolder,
	[CardNo] = @CardNo,
	[StockLogId] = @StockLogId,
	[Memo] = @Memo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



