
alter table St_Stock
   drop constraint PK_ST_STOCK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_Stock')
            and   type = 'U')
   drop table St_Stock
go

/*==============================================================*/
/* Table: St_Stock                                              */
/*==============================================================*/
create table St_Stock (
   StockId              int                  identity,
   StockNameId          int                  null,
   StockNo              varchar(80)          null,
   StockDate            datetime             null,
   AssetId              int                  null,
   Bundles              int                  null,
   GrossAmount          numeric(18,4)        null,
   NetAmount            numeric(18,4)        null,
   ReceiptInGap         numeric(18,4)        null,
   ReceiptOutGap        numeric(18,4)        null,
   CurGrossAmount       numeric(18,4)        null,
   CurNetAmount         numeric(18,4)        null,
   UintId               int                  null,
   DeliverPlaceId       int                  null,
   BrandId              int                  null,
   CustomsType          int                  null,
   GroupId              int                  null,
   CorpId               int                  null,
   DeptId               int                  null,
   ProducerId           int                  null,
   PaperNo              varchar(80)          null,
   PaperHolder          int                  null,
   Format               varchar(200)         null,
   OriginPlaceId        int                  null,
   OriginPlace          varchar(200)         null,
   PreStatus            int                  null,
   StockStatus          int                  null,
   CardNo               varchar(200)         null,
   Memo                 varchar(4000)        null,
   StockType            int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '库存',
   'user', @CurrentUser, 'table', 'St_Stock'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存名称序号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'StockNameId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存编号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'StockNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '入库时间',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'StockDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '品种序号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'AssetId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '捆数',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'Bundles'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '入库毛量，单据毛重',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'GrossAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '入库净量，单据净重',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'NetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '入库回执磅差',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'ReceiptInGap'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出库回执磅差',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'ReceiptOutGap'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '当前毛吨',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'CurGrossAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'UintId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '交货地',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'DeliverPlaceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '品牌',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'BrandId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关状态',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'CustomsType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所属集团',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'GroupId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所属公司',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'CorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所属部门',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'DeptId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生产商序号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'ProducerId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权证编号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'PaperNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单据保管人',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'PaperHolder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '规格',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'Format'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产地序号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'OriginPlaceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产地',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'OriginPlace'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '前一状态',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'PreStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存状态',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'StockStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '卡号',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'CardNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存类型(提报货)',
   'user', @CurrentUser, 'table', 'St_Stock', 'column', 'StockType'
go

alter table St_Stock
   add constraint PK_ST_STOCK primary key (StockId)
go


/****** Object:  Stored Procedure [dbo].St_StockGet    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockLoad    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockInsert    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockUpdate    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockUpdateStatus    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockUpdateStatus    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockUpdateStatus
// 存储过程功能描述：更新St_Stock中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_Stock'

set @str = 'update [dbo].[St_Stock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockGoBack
// 存储过程功能描述：撤返St_Stock，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_Stock'

set @str = 'update [dbo].[St_Stock] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockGet
// 存储过程功能描述：查询指定St_Stock的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockGet
    /*
	@StockId int
    */
    @id int
AS

SELECT
	[StockId],
	[StockNameId],
	[StockNo],
	[StockDate],
	[AssetId],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[ReceiptInGap],
	[ReceiptOutGap],
	[CurGrossAmount],
	[CurNetAmount],
	[UintId],
	[DeliverPlaceId],
	[BrandId],
	[CustomsType],
	[GroupId],
	[CorpId],
	[DeptId],
	[ProducerId],
	[PaperNo],
	[PaperHolder],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[PreStatus],
	[StockStatus],
	[CardNo],
	[Memo],
	[StockType]
FROM
	[dbo].[St_Stock]
WHERE
	[StockId] = @id

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
// 存储过程名：[dbo].St_StockLoad
// 存储过程功能描述：查询所有St_Stock记录
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLoad
AS

SELECT
	[StockId],
	[StockNameId],
	[StockNo],
	[StockDate],
	[AssetId],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[ReceiptInGap],
	[ReceiptOutGap],
	[CurGrossAmount],
	[CurNetAmount],
	[UintId],
	[DeliverPlaceId],
	[BrandId],
	[CustomsType],
	[GroupId],
	[CorpId],
	[DeptId],
	[ProducerId],
	[PaperNo],
	[PaperHolder],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[PreStatus],
	[StockStatus],
	[CardNo],
	[Memo],
	[StockType]
FROM
	[dbo].[St_Stock]

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
// 存储过程名：[dbo].St_StockInsert
// 存储过程功能描述：新增一条St_Stock记录
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInsert
	@StockNameId int =NULL ,
	@StockNo varchar(80) =NULL ,
	@StockDate datetime =NULL ,
	@AssetId int =NULL ,
	@Bundles int =NULL ,
	@GrossAmount numeric(18, 4) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@ReceiptInGap numeric(18, 4) =NULL ,
	@ReceiptOutGap numeric(18, 4) =NULL ,
	@CurGrossAmount numeric(18, 4) =NULL ,
	@CurNetAmount numeric(18, 4) =NULL ,
	@UintId int =NULL ,
	@DeliverPlaceId int =NULL ,
	@BrandId int =NULL ,
	@CustomsType int =NULL ,
	@GroupId int =NULL ,
	@CorpId int =NULL ,
	@DeptId int =NULL ,
	@ProducerId int =NULL ,
	@PaperNo varchar(80) =NULL ,
	@PaperHolder int =NULL ,
	@Format varchar(200) =NULL ,
	@OriginPlaceId int =NULL ,
	@OriginPlace varchar(200) =NULL ,
	@PreStatus int =NULL ,
	@StockStatus int =NULL ,
	@CardNo varchar(200) =NULL ,
	@Memo varchar(4000) =NULL ,
	@StockType int =NULL ,
	@StockId int OUTPUT
AS

INSERT INTO [dbo].[St_Stock] (
	[StockNameId],
	[StockNo],
	[StockDate],
	[AssetId],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[ReceiptInGap],
	[ReceiptOutGap],
	[CurGrossAmount],
	[CurNetAmount],
	[UintId],
	[DeliverPlaceId],
	[BrandId],
	[CustomsType],
	[GroupId],
	[CorpId],
	[DeptId],
	[ProducerId],
	[PaperNo],
	[PaperHolder],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[PreStatus],
	[StockStatus],
	[CardNo],
	[Memo],
	[StockType]
) VALUES (
	@StockNameId,
	@StockNo,
	@StockDate,
	@AssetId,
	@Bundles,
	@GrossAmount,
	@NetAmount,
	@ReceiptInGap,
	@ReceiptOutGap,
	@CurGrossAmount,
	@CurNetAmount,
	@UintId,
	@DeliverPlaceId,
	@BrandId,
	@CustomsType,
	@GroupId,
	@CorpId,
	@DeptId,
	@ProducerId,
	@PaperNo,
	@PaperHolder,
	@Format,
	@OriginPlaceId,
	@OriginPlace,
	@PreStatus,
	@StockStatus,
	@CardNo,
	@Memo,
	@StockType
)


SET @StockId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockUpdate
// 存储过程功能描述：更新St_Stock
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockUpdate
    @StockId int,
@StockNameId int = NULL,
@StockNo varchar(80) = NULL,
@StockDate datetime = NULL,
@AssetId int = NULL,
@Bundles int = NULL,
@GrossAmount numeric(18, 4) = NULL,
@NetAmount numeric(18, 4) = NULL,
@ReceiptInGap numeric(18, 4) = NULL,
@ReceiptOutGap numeric(18, 4) = NULL,
@CurGrossAmount numeric(18, 4) = NULL,
@CurNetAmount numeric(18, 4) = NULL,
@UintId int = NULL,
@DeliverPlaceId int = NULL,
@BrandId int = NULL,
@CustomsType int = NULL,
@GroupId int = NULL,
@CorpId int = NULL,
@DeptId int = NULL,
@ProducerId int = NULL,
@PaperNo varchar(80) = NULL,
@PaperHolder int = NULL,
@Format varchar(200) = NULL,
@OriginPlaceId int = NULL,
@OriginPlace varchar(200) = NULL,
@PreStatus int = NULL,
@StockStatus int = NULL,
@CardNo varchar(200) = NULL,
@Memo varchar(4000) = NULL,
@StockType int = NULL
AS

UPDATE [dbo].[St_Stock] SET
	[StockNameId] = @StockNameId,
	[StockNo] = @StockNo,
	[StockDate] = @StockDate,
	[AssetId] = @AssetId,
	[Bundles] = @Bundles,
	[GrossAmount] = @GrossAmount,
	[NetAmount] = @NetAmount,
	[ReceiptInGap] = @ReceiptInGap,
	[ReceiptOutGap] = @ReceiptOutGap,
	[CurGrossAmount] = @CurGrossAmount,
	[CurNetAmount] = @CurNetAmount,
	[UintId] = @UintId,
	[DeliverPlaceId] = @DeliverPlaceId,
	[BrandId] = @BrandId,
	[CustomsType] = @CustomsType,
	[GroupId] = @GroupId,
	[CorpId] = @CorpId,
	[DeptId] = @DeptId,
	[ProducerId] = @ProducerId,
	[PaperNo] = @PaperNo,
	[PaperHolder] = @PaperHolder,
	[Format] = @Format,
	[OriginPlaceId] = @OriginPlaceId,
	[OriginPlace] = @OriginPlace,
	[PreStatus] = @PreStatus,
	[StockStatus] = @StockStatus,
	[CardNo] = @CardNo,
	[Memo] = @Memo,
	[StockType] = @StockType
WHERE
	[StockId] = @StockId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



