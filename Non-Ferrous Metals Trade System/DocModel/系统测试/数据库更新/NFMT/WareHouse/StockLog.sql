alter table dbo.St_StockLog
   drop constraint PK_ST_STOCKLOG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockLog')
            and   type = 'U')
   drop table dbo.St_StockLog
go

/*==============================================================*/
/* Table: St_StockLog                                           */
/*==============================================================*/
create table dbo.St_StockLog (
   StockLogId           int                  identity,
   StockId              int                  null,
   StockNameId          int                  null,
   RefNo                varchar(50)          null,
   LogDirection         int                  null,
   LogType              int                  not null default -1,
   ContractId           int                  null,
   SubContractId        int                  null,
   LogDate              datetime             not null default convert(int,convert(varchar(8),getdate(),112)),
   OpPerson             int                  null,
   AssetId              int                  null,
   Bundles              int                  not null default -1,
   GrossAmount          numeric(18,4)        not null default 0,
   NetAmount            numeric(18,4)        not null default 0,
   GapAmount            decimal(18,4)        null,
   MUId                 int                  null,
   BrandId              int                  null,
   GroupId              int                  null,
   CorpId               int                  null,
   DeptId               int                  null,
   CustomsType          int                  null,
   DeliverPlaceId       int                  null,
   ProducerId           int                  null,
   PaperNo              varchar(80)          null,
   PaperHolder          int                  null,
   CardNo               varchar(200)         null,
   StockType            int                  null,
   Format               varchar(200)         null,
   OriginPlaceId        int                  null,
   OriginPlace          varchar(200)         null,
   Memo                 varchar(4000)        not null,
   LogStatus            int                  null,
   LogSourceBase        varchar(50)          null,
   LogSource            varchar(200)         null,
   SourceId             int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '出入库流水',
   'user', 'dbo', 'table', 'St_StockLog'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'StockLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'StockId'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务单序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'StockNameId'
go

execute sp_addextendedproperty 'MS_Description', 
   '业务单号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'RefNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水方向/进或出',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LogDirection'
go

execute sp_addextendedproperty 'MS_Description', 
   '操作类型/出库，出库冲销，入库，入库冲销，移库，移库冲销，质押，质押冲销，回购，回购冲销，收款，收款冲销，付款，付款冲销，开票，开票冲销，收票，收票冲销。',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LogType'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'SubContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '操作日期',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LogDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '操作人',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'OpPerson'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '捆数',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'Bundles'
go

execute sp_addextendedproperty 'MS_Description', 
   '毛量',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'GrossAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '净量',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '磅差',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'GapAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '计量单位',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'MUId'
go

execute sp_addextendedproperty 'MS_Description', 
   '品牌',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'BrandId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属集团',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'GroupId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属公司',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'CorpId'
go

execute sp_addextendedproperty 'MS_Description', 
   '所属部门',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'DeptId'
go

execute sp_addextendedproperty 'MS_Description', 
   '报关状态',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'CustomsType'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货地',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'DeliverPlaceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产商序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'ProducerId'
go

execute sp_addextendedproperty 'MS_Description', 
   '权证编号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'PaperNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '单据保管人',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'PaperHolder'
go

execute sp_addextendedproperty 'MS_Description', 
   '卡号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'CardNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存类型(提报货)',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'StockType'
go

execute sp_addextendedproperty 'MS_Description', 
   '规格',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'Format'
go

execute sp_addextendedproperty 'MS_Description', 
   '产地序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'OriginPlaceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '产地',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'OriginPlace'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水状态',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LogStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水来源库名',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LogSourceBase'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水来源/表名记录',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LogSource'
go

execute sp_addextendedproperty 'MS_Description', 
   '来源编号/表序号记录',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'SourceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'St_StockLog', 'column', 'LastModifyTime'
go

alter table dbo.St_StockLog
   add constraint PK_ST_STOCKLOG primary key (StockLogId)
go


/****** Object:  Stored Procedure [dbo].St_StockLogGet    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogLoad    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogInsert    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogUpdate    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogUpdateStatus    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogUpdateStatus    Script Date: 2015年1月19日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogUpdateStatus
// 存储过程功能描述：更新St_StockLog中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockLog'

set @str = 'update [dbo].[St_StockLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockLogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockLogGoBack
// 存储过程功能描述：撤返St_StockLog，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockLog'

set @str = 'update [dbo].[St_StockLog] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockLogId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockLogGet
// 存储过程功能描述：查询指定St_StockLog的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogGet
    /*
	@StockLogId int
    */
    @id int
AS

SELECT
	[StockLogId],
	[StockId],
	[StockNameId],
	[RefNo],
	[LogDirection],
	[LogType],
	[ContractId],
	[SubContractId],
	[LogDate],
	[OpPerson],
	[AssetId],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[GapAmount],
	[MUId],
	[BrandId],
	[GroupId],
	[CorpId],
	[DeptId],
	[CustomsType],
	[DeliverPlaceId],
	[ProducerId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[StockType],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[Memo],
	[LogStatus],
	[LogSourceBase],
	[LogSource],
	[SourceId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockLog]
WHERE
	[StockLogId] = @id

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
// 存储过程名：[dbo].St_StockLogLoad
// 存储过程功能描述：查询所有St_StockLog记录
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogLoad
AS

SELECT
	[StockLogId],
	[StockId],
	[StockNameId],
	[RefNo],
	[LogDirection],
	[LogType],
	[ContractId],
	[SubContractId],
	[LogDate],
	[OpPerson],
	[AssetId],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[GapAmount],
	[MUId],
	[BrandId],
	[GroupId],
	[CorpId],
	[DeptId],
	[CustomsType],
	[DeliverPlaceId],
	[ProducerId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[StockType],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[Memo],
	[LogStatus],
	[LogSourceBase],
	[LogSource],
	[SourceId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockLog]

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
// 存储过程名：[dbo].St_StockLogInsert
// 存储过程功能描述：新增一条St_StockLog记录
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogInsert
	@StockId int =NULL ,
	@StockNameId int =NULL ,
	@RefNo varchar(50) =NULL ,
	@LogDirection int =NULL ,
	@LogType int ,
	@ContractId int =NULL ,
	@SubContractId int =NULL ,
	@LogDate datetime ,
	@OpPerson int =NULL ,
	@AssetId int =NULL ,
	@Bundles int ,
	@GrossAmount numeric(18, 4) ,
	@NetAmount numeric(18, 4) ,
	@GapAmount decimal(18, 4) =NULL ,
	@MUId int =NULL ,
	@BrandId int =NULL ,
	@GroupId int =NULL ,
	@CorpId int =NULL ,
	@DeptId int =NULL ,
	@CustomsType int =NULL ,
	@DeliverPlaceId int =NULL ,
	@ProducerId int =NULL ,
	@PaperNo varchar(80) =NULL ,
	@PaperHolder int =NULL ,
	@CardNo varchar(200) =NULL ,
	@StockType int =NULL ,
	@Format varchar(200) =NULL ,
	@OriginPlaceId int =NULL ,
	@OriginPlace varchar(200) =NULL ,
	@Memo varchar(4000) ,
	@LogStatus int =NULL ,
	@LogSourceBase varchar(50) =NULL ,
	@LogSource varchar(200) =NULL ,
	@SourceId int =NULL ,
	@CreatorId int =NULL ,
	@StockLogId int OUTPUT
AS

INSERT INTO [dbo].[St_StockLog] (
	[StockId],
	[StockNameId],
	[RefNo],
	[LogDirection],
	[LogType],
	[ContractId],
	[SubContractId],
	[LogDate],
	[OpPerson],
	[AssetId],
	[Bundles],
	[GrossAmount],
	[NetAmount],
	[GapAmount],
	[MUId],
	[BrandId],
	[GroupId],
	[CorpId],
	[DeptId],
	[CustomsType],
	[DeliverPlaceId],
	[ProducerId],
	[PaperNo],
	[PaperHolder],
	[CardNo],
	[StockType],
	[Format],
	[OriginPlaceId],
	[OriginPlace],
	[Memo],
	[LogStatus],
	[LogSourceBase],
	[LogSource],
	[SourceId],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StockId,
	@StockNameId,
	@RefNo,
	@LogDirection,
	@LogType,
	@ContractId,
	@SubContractId,
	@LogDate,
	@OpPerson,
	@AssetId,
	@Bundles,
	@GrossAmount,
	@NetAmount,
	@GapAmount,
	@MUId,
	@BrandId,
	@GroupId,
	@CorpId,
	@DeptId,
	@CustomsType,
	@DeliverPlaceId,
	@ProducerId,
	@PaperNo,
	@PaperHolder,
	@CardNo,
	@StockType,
	@Format,
	@OriginPlaceId,
	@OriginPlace,
	@Memo,
	@LogStatus,
	@LogSourceBase,
	@LogSource,
	@SourceId,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StockLogId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockLogUpdate
// 存储过程功能描述：更新St_StockLog
// 创建人：CodeSmith
// 创建时间： 2015年1月19日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogUpdate
    @StockLogId int,
@StockId int = NULL,
@StockNameId int = NULL,
@RefNo varchar(50) = NULL,
@LogDirection int = NULL,
@LogType int,
@ContractId int = NULL,
@SubContractId int = NULL,
@LogDate datetime,
@OpPerson int = NULL,
@AssetId int = NULL,
@Bundles int,
@GrossAmount numeric(18, 4),
@NetAmount numeric(18, 4),
@GapAmount decimal(18, 4) = NULL,
@MUId int = NULL,
@BrandId int = NULL,
@GroupId int = NULL,
@CorpId int = NULL,
@DeptId int = NULL,
@CustomsType int = NULL,
@DeliverPlaceId int = NULL,
@ProducerId int = NULL,
@PaperNo varchar(80) = NULL,
@PaperHolder int = NULL,
@CardNo varchar(200) = NULL,
@StockType int = NULL,
@Format varchar(200) = NULL,
@OriginPlaceId int = NULL,
@OriginPlace varchar(200) = NULL,
@Memo varchar(4000),
@LogStatus int = NULL,
@LogSourceBase varchar(50) = NULL,
@LogSource varchar(200) = NULL,
@SourceId int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_StockLog] SET
	[StockId] = @StockId,
	[StockNameId] = @StockNameId,
	[RefNo] = @RefNo,
	[LogDirection] = @LogDirection,
	[LogType] = @LogType,
	[ContractId] = @ContractId,
	[SubContractId] = @SubContractId,
	[LogDate] = @LogDate,
	[OpPerson] = @OpPerson,
	[AssetId] = @AssetId,
	[Bundles] = @Bundles,
	[GrossAmount] = @GrossAmount,
	[NetAmount] = @NetAmount,
	[GapAmount] = @GapAmount,
	[MUId] = @MUId,
	[BrandId] = @BrandId,
	[GroupId] = @GroupId,
	[CorpId] = @CorpId,
	[DeptId] = @DeptId,
	[CustomsType] = @CustomsType,
	[DeliverPlaceId] = @DeliverPlaceId,
	[ProducerId] = @ProducerId,
	[PaperNo] = @PaperNo,
	[PaperHolder] = @PaperHolder,
	[CardNo] = @CardNo,
	[StockType] = @StockType,
	[Format] = @Format,
	[OriginPlaceId] = @OriginPlaceId,
	[OriginPlace] = @OriginPlace,
	[Memo] = @Memo,
	[LogStatus] = @LogStatus,
	[LogSourceBase] = @LogSourceBase,
	[LogSource] = @LogSource,
	[SourceId] = @SourceId,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StockLogId] = @StockLogId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



