alter table dbo.St_StockIn
   drop constraint PK_ST_STOCKIN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockIn')
            and   type = 'U')
   drop table dbo.St_StockIn
go

/*==============================================================*/
/* Table: St_StockIn                                            */
/*==============================================================*/
create table dbo.St_StockIn (
   StockInId            int                  identity,
   GroupId              int                  null,
   CorpId               int                  null,
   DeptId               int                  null,
   StockInDate          datetime             null,
   CustomType           int                  null,
   GrossAmount          numeric(18,4)        null,
   NetAmount            numeric(18,4)        null,
   UintId               int                  null,
   AssetId              int                  null,
   Bundles              int                  null,
   BrandId              int                  null,
   DeliverPlaceId       int                  null,
   ProducerId           int                  null,
   StockType            int                  null,
   StockOperateType     int                  null,
   PaperNo              varchar(80)          null,
   PaperHolder          int                  null,
   CardNo               varchar(200)         null,
   Format               varchar(200)         null,
   OriginPlaceId        int                  null,
   OriginPlace          varchar(200)         null,
   StockInStatus        int                  null,
   RefNo                varchar(50)          null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '入库登记',
   'user', 'dbo', 'table', 'St_StockIn'
go

execute sp_addextendedproperty 'MS_Description', 
   '登记序号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'StockInId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属集团',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'GroupId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属公司',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属部门',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '入库日期',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'StockInDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '报关状态',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'CustomType'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存毛量',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'GrossAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存净量',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'UintId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种序号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '捆数',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'Bundles'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'BrandId'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'DeliverPlaceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商序号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'ProducerId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存类型(提报货)',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'StockType'
go

execute sp_addextendedproperty 'MS_Description', 
   '出入库类型',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'StockOperateType'
go

execute sp_addextendedproperty 'MS_Description', 
   '权证编号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'PaperNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '单据保管人',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'PaperHolder'
go

execute sp_addextendedproperty 'MS_Description', 
   '卡号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'CardNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '规格',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'Format'
go

execute sp_addextendedproperty 'MS_Description', 
   '产地序号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'OriginPlaceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '产地',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'OriginPlace'
go

execute sp_addextendedproperty 'MS_Description', 
   '入库状态',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'StockInStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'RefNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'St_StockIn', 'column', 'LastModifyTime'
go

alter table dbo.St_StockIn
   add constraint PK_ST_STOCKIN primary key (StockInId)
go


/****** Object:  Stored Procedure [dbo].St_StockInGet    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockInLoad    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockInInsert    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockInUpdate    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockInUpdateStatus    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockInUpdateStatus    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockInGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockInGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockInUpdateStatus
// 存储过程功能描述：更新St_StockIn中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockIn'

set @str = 'update [dbo].[St_StockIn] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockInId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockInGoBack
// 存储过程功能描述：撤返St_StockIn，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockIn'

set @str = 'update [dbo].[St_StockIn] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockInId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockInGet
// 存储过程功能描述：查询指定St_StockIn的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInGet
    /*
	@StockInId int
    */
    @id int
AS

SELECT
	[StockInId],
	[GroupId],
	[CorpId],
	[DeptId],
	[StockInDate],
	[CustomType],
	[GrossAmount],
	[NetAmount],
	[UintId],
	[AssetId],
	[Bundles],
	[BrandId],
	[DeliverPlaceId],
	[ProducerId],
	[StockType],
	[StockOperateType],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[StockInStatus],
	[RefNo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockIn]
WHERE
	[StockInId] = @id

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
// 存储过程名：[dbo].St_StockInLoad
// 存储过程功能描述：查询所有St_StockIn记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInLoad
AS

SELECT
	[StockInId],
	[GroupId],
	[CorpId],
	[DeptId],
	[StockInDate],
	[CustomType],
	[GrossAmount],
	[NetAmount],
	[UintId],
	[AssetId],
	[Bundles],
	[BrandId],
	[DeliverPlaceId],
	[ProducerId],
	[StockType],
	[StockOperateType],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[StockInStatus],
	[RefNo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockIn]

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
// 存储过程名：[dbo].St_StockInInsert
// 存储过程功能描述：新增一条St_StockIn记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInInsert
	@GroupId int =NULL ,
	@CorpId int =NULL ,
	@DeptId int =NULL ,
	@StockInDate datetime =NULL ,
	@CustomType int =NULL ,
	@GrossAmount numeric(18, 4) =NULL ,
	@NetAmount numeric(18, 4) =NULL ,
	@UintId int =NULL ,
	@AssetId int =NULL ,
	@Bundles int =NULL ,
	@BrandId int =NULL ,
	@DeliverPlaceId int =NULL ,
	@ProducerId int =NULL ,
	@StockType int =NULL ,
	@StockOperateType int =NULL ,
	@PaperNo varchar(80) =NULL ,
	@PaperHolder int =NULL ,
	@CardNo varchar(200) =NULL ,
	@Format varchar(200) =NULL ,
	@OriginPlaceId int =NULL ,
	@OriginPlace varchar(200) =NULL ,
	@StockInStatus int =NULL ,
	@RefNo varchar(50) =NULL ,
	@CreatorId int =NULL ,
	@StockInId int OUTPUT
AS

INSERT INTO [dbo].[St_StockIn] (
	[GroupId],
	[CorpId],
	[DeptId],
	[StockInDate],
	[CustomType],
	[GrossAmount],
	[NetAmount],
	[UintId],
	[AssetId],
	[Bundles],
	[BrandId],
	[DeliverPlaceId],
	[ProducerId],
	[StockType],
	[StockOperateType],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[StockInStatus],
	[RefNo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@GroupId,
	@CorpId,
	@DeptId,
	@StockInDate,
	@CustomType,
	@GrossAmount,
	@NetAmount,
	@UintId,
	@AssetId,
	@Bundles,
	@BrandId,
	@DeliverPlaceId,
	@ProducerId,
	@StockType,
	@StockOperateType,
	@PaperNo,
	@PaperHolder,
	@CardNo,
	@Format,
	@OriginPlaceId,
	@OriginPlace,
	@StockInStatus,
	@RefNo,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StockInId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockInUpdate
// 存储过程功能描述：更新St_StockIn
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockInUpdate
    @StockInId int,
@GroupId int = NULL,
@CorpId int = NULL,
@DeptId int = NULL,
@StockInDate datetime = NULL,
@CustomType int = NULL,
@GrossAmount numeric(18, 4) = NULL,
@NetAmount numeric(18, 4) = NULL,
@UintId int = NULL,
@AssetId int = NULL,
@Bundles int = NULL,
@BrandId int = NULL,
@DeliverPlaceId int = NULL,
@ProducerId int = NULL,
@StockType int = NULL,
@StockOperateType int = NULL,
@PaperNo varchar(80) = NULL,
@PaperHolder int = NULL,
@CardNo varchar(200) = NULL,
@Format varchar(200) = NULL,
@OriginPlaceId int = NULL,
@OriginPlace varchar(200) = NULL,
@StockInStatus int = NULL,
@RefNo varchar(50) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_StockIn] SET
	[GroupId] = @GroupId,
	[CorpId] = @CorpId,
	[DeptId] = @DeptId,
	[StockInDate] = @StockInDate,
	[CustomType] = @CustomType,
	[GrossAmount] = @GrossAmount,
	[NetAmount] = @NetAmount,
	[UintId] = @UintId,
	[AssetId] = @AssetId,
	[Bundles] = @Bundles,
	[BrandId] = @BrandId,
	[DeliverPlaceId] = @DeliverPlaceId,
	[ProducerId] = @ProducerId,
	[StockType] = @StockType,
	[StockOperateType] = @StockOperateType,
	[PaperNo] = @PaperNo,
	[PaperHolder] = @PaperHolder,
	[CardNo] = @CardNo,
	[Format] = @Format,
	[OriginPlaceId] = @OriginPlaceId,
	[OriginPlace] = @OriginPlace,
	[StockInStatus] = @StockInStatus,
	[RefNo] = @RefNo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StockInId] = @StockInId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



