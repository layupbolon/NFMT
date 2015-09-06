alter table dbo.Con_ContractSub
   drop constraint PK_CON_CONTRACTSUB
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractSub')
            and   type = 'U')
   drop table dbo.Con_ContractSub
go

/*==============================================================*/
/* Table: Con_ContractSub                                       */
/*==============================================================*/
create table dbo.Con_ContractSub (
   SubId                int                  identity,
   ContractId           int                  null,
   ContractDate         datetime             null,
   TradeBorder          int                  null,
   ContractLimit        int                  null,
   ContractSide         int                  null,
   TradeDirection       int                  null,
   ContractPeriodS      datetime             null,
   ContractPeriodE      datetime             null,
   Premium              numeric(18,4)        null,
   SubNo                varchar(80)          null,
   OutContractNo        varchar(200)         null,
   SubName              varchar(200)         null,
   SettleCurrency       int                  null,
   SignAmount           numeric(18,4)        null,
   ExeAmount            numeric(18,4)        null,
   UnitId               int                  null,
   PriceMode            int                  null,
   ShipTime             datetime             null,
   ArriveTime           datetime             null,
   InitQP               datetime             null,
   Memo                 varchar(4000)        null,
   DeliveryStyle        int                  null,
   DeliveryDate         datetime             null,
   CreateFrom           int                  null,
   SubStatus            int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null,
   AssetId              int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '子合约',
   'user', 'dbo', 'table', 'Con_ContractSub'
go

execute sp_addextendedproperty 'MS_Description', 
   '信用证序号',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'SubId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '签订日期',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ContractDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '贸易境区(内外贸)',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'TradeBorder'
go

execute sp_addextendedproperty 'MS_Description', 
   '合同时限(长零)',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ContractLimit'
go

execute sp_addextendedproperty 'MS_Description', 
   '合同对方(内外部合同)',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ContractSide'
go

execute sp_addextendedproperty 'MS_Description', 
   '贸易方向(购销)',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'TradeDirection'
go

execute sp_addextendedproperty 'MS_Description', 
   '开始执行日',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ContractPeriodS'
go

execute sp_addextendedproperty 'MS_Description', 
   '结束执行日',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ContractPeriodE'
go

execute sp_addextendedproperty 'MS_Description', 
   '升贴水',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'Premium'
go

execute sp_addextendedproperty 'MS_Description', 
   '内部子合约号',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'SubNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '外部合约号',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'OutContractNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约名称',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'SubName'
go

execute sp_addextendedproperty 'MS_Description', 
   '结算币种',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'SettleCurrency'
go

execute sp_addextendedproperty 'MS_Description', 
   '签订数量',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'SignAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '执行数量',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ExeAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'UnitId'
go

execute sp_addextendedproperty 'MS_Description', 
   '定价方式',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'PriceMode'
go

execute sp_addextendedproperty 'MS_Description', 
   '装船月',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ShipTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '到货月',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'ArriveTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '初始QP',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'InitQP'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货方式',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'DeliveryStyle'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货日期',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'DeliveryDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建来源',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'CreateFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '子合约状态',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'SubStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'LastModifyTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'Con_ContractSub', 'column', 'AssetId'
go

alter table dbo.Con_ContractSub
   add constraint PK_CON_CONTRACTSUB primary key (SubId)
go


/****** Object:  StoredProcedure [dbo].[CreateContractSubNo]    Script Date: 01/20/2015 10:51:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateContractSubNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateContractSubNo]
GO

/****** Object:  StoredProcedure [dbo].[CreateContractSubNo]    Script Date: 01/20/2015 10:51:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[CreateContractSubNo]
(
@identity int
)
as
begin

declare @typeName varchar(20)
declare @contractSubNo varchar(20)

set @typeName = 'ContractSub'
set @contractSubNo = @typeName + CAST(@identity as varchar)

update dbo.Con_ContractSub set SubNo=@contractSubNo where SubId = @identity

end


GO
/****** Object:  Stored Procedure [dbo].Con_ContractSubGet    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubLoad    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubInsert    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubUpdate    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubUpdateStatus    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractSubUpdateStatus    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractSubGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractSubGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractSubUpdateStatus
// 存储过程功能描述：更新Con_ContractSub中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractSub'

set @str = 'update [dbo].[Con_ContractSub] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SubId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractSubGoBack
// 存储过程功能描述：撤返Con_ContractSub，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractSub'

set @str = 'update [dbo].[Con_ContractSub] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where SubId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractSubGet
// 存储过程功能描述：查询指定Con_ContractSub的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubGet
    /*
	@SubId int
    */
    @id int
AS

SELECT
	[SubId],
	[ContractId],
	[ContractDate],
	[TradeBorder],
	[ContractLimit],
	[ContractSide],
	[TradeDirection],
	[ContractPeriodS],
	[ContractPeriodE],
	[Premium],
	[SubNo],
	[OutContractNo],
	[SubName],
	[SettleCurrency],
	[SignAmount],
	[ExeAmount],
	[UnitId],
	[PriceMode],
	[ShipTime],
	[ArriveTime],
	[InitQP],
	[Memo],
	[DeliveryStyle],
	[DeliveryDate],
	[CreateFrom],
	[SubStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime],
	[AssetId]
FROM
	[dbo].[Con_ContractSub]
WHERE
	[SubId] = @id

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
// 存储过程名：[dbo].Con_ContractSubLoad
// 存储过程功能描述：查询所有Con_ContractSub记录
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubLoad
AS

SELECT
	[SubId],
	[ContractId],
	[ContractDate],
	[TradeBorder],
	[ContractLimit],
	[ContractSide],
	[TradeDirection],
	[ContractPeriodS],
	[ContractPeriodE],
	[Premium],
	[SubNo],
	[OutContractNo],
	[SubName],
	[SettleCurrency],
	[SignAmount],
	[ExeAmount],
	[UnitId],
	[PriceMode],
	[ShipTime],
	[ArriveTime],
	[InitQP],
	[Memo],
	[DeliveryStyle],
	[DeliveryDate],
	[CreateFrom],
	[SubStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime],
	[AssetId]
FROM
	[dbo].[Con_ContractSub]

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
// 存储过程名：[dbo].Con_ContractSubInsert
// 存储过程功能描述：新增一条Con_ContractSub记录
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubInsert
	@ContractId int =NULL ,
	@ContractDate datetime =NULL ,
	@TradeBorder int =NULL ,
	@ContractLimit int =NULL ,
	@ContractSide int =NULL ,
	@TradeDirection int =NULL ,
	@ContractPeriodS datetime =NULL ,
	@ContractPeriodE datetime =NULL ,
	@Premium numeric(18, 4) =NULL ,
	@SubNo varchar(80) =NULL ,
	@OutContractNo varchar(200) =NULL ,
	@SubName varchar(200) =NULL ,
	@SettleCurrency int =NULL ,
	@SignAmount numeric(18, 4) =NULL ,
	@ExeAmount numeric(18, 4) =NULL ,
	@UnitId int =NULL ,
	@PriceMode int =NULL ,
	@ShipTime datetime =NULL ,
	@ArriveTime datetime =NULL ,
	@InitQP datetime =NULL ,
	@Memo varchar(4000) =NULL ,
	@DeliveryStyle int =NULL ,
	@DeliveryDate datetime =NULL ,
	@CreateFrom int =NULL ,
	@SubStatus int =NULL ,
	@CreatorId int =NULL ,
	@AssetId int =NULL ,
	@SubId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractSub] (
	[ContractId],
	[ContractDate],
	[TradeBorder],
	[ContractLimit],
	[ContractSide],
	[TradeDirection],
	[ContractPeriodS],
	[ContractPeriodE],
	[Premium],
	[SubNo],
	[OutContractNo],
	[SubName],
	[SettleCurrency],
	[SignAmount],
	[ExeAmount],
	[UnitId],
	[PriceMode],
	[ShipTime],
	[ArriveTime],
	[InitQP],
	[Memo],
	[DeliveryStyle],
	[DeliveryDate],
	[CreateFrom],
	[SubStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime],
	[AssetId]
) VALUES (
	@ContractId,
	@ContractDate,
	@TradeBorder,
	@ContractLimit,
	@ContractSide,
	@TradeDirection,
	@ContractPeriodS,
	@ContractPeriodE,
	@Premium,
	@SubNo,
	@OutContractNo,
	@SubName,
	@SettleCurrency,
	@SignAmount,
	@ExeAmount,
	@UnitId,
	@PriceMode,
	@ShipTime,
	@ArriveTime,
	@InitQP,
	@Memo,
	@DeliveryStyle,
	@DeliveryDate,
	@CreateFrom,
	@SubStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()
,
	@AssetId
)


SET @SubId = @@IDENTITY

exec CreateContractSubNo @identity =@SubId

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
// 存储过程名：[dbo].Con_ContractSubUpdate
// 存储过程功能描述：更新Con_ContractSub
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractSubUpdate
    @SubId int,
@ContractId int = NULL,
@ContractDate datetime = NULL,
@TradeBorder int = NULL,
@ContractLimit int = NULL,
@ContractSide int = NULL,
@TradeDirection int = NULL,
@ContractPeriodS datetime = NULL,
@ContractPeriodE datetime = NULL,
@Premium numeric(18, 4) = NULL,
@SubNo varchar(80) = NULL,
@OutContractNo varchar(200) = NULL,
@SubName varchar(200) = NULL,
@SettleCurrency int = NULL,
@SignAmount numeric(18, 4) = NULL,
@ExeAmount numeric(18, 4) = NULL,
@UnitId int = NULL,
@PriceMode int = NULL,
@ShipTime datetime = NULL,
@ArriveTime datetime = NULL,
@InitQP datetime = NULL,
@Memo varchar(4000) = NULL,
@DeliveryStyle int = NULL,
@DeliveryDate datetime = NULL,
@CreateFrom int = NULL,
@SubStatus int = NULL,
@LastModifyId int = NULL,
@AssetId int = NULL
AS

UPDATE [dbo].[Con_ContractSub] SET
	[ContractId] = @ContractId,
	[ContractDate] = @ContractDate,
	[TradeBorder] = @TradeBorder,
	[ContractLimit] = @ContractLimit,
	[ContractSide] = @ContractSide,
	[TradeDirection] = @TradeDirection,
	[ContractPeriodS] = @ContractPeriodS,
	[ContractPeriodE] = @ContractPeriodE,
	[Premium] = @Premium,
	[SubNo] = @SubNo,
	[OutContractNo] = @OutContractNo,
	[SubName] = @SubName,
	[SettleCurrency] = @SettleCurrency,
	[SignAmount] = @SignAmount,
	[ExeAmount] = @ExeAmount,
	[UnitId] = @UnitId,
	[PriceMode] = @PriceMode,
	[ShipTime] = @ShipTime,
	[ArriveTime] = @ArriveTime,
	[InitQP] = @InitQP,
	[Memo] = @Memo,
	[DeliveryStyle] = @DeliveryStyle,
	[DeliveryDate] = @DeliveryDate,
	[CreateFrom] = @CreateFrom,
	[SubStatus] = @SubStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()
,
	[AssetId] = @AssetId
WHERE
	[SubId] = @SubId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



