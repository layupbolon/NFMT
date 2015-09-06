alter table dbo.Con_Contract
   drop constraint PK_CON_CONTRACT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_Contract')
            and   type = 'U')
   drop table dbo.Con_Contract
go

/*==============================================================*/
/* Table: Con_Contract                                          */
/*==============================================================*/
create table dbo.Con_Contract (
   ContractId           int                  identity,
   ContractNo           varchar(20)          null,
   OutContractNo        varchar(200)         null,
   ContractDate         datetime             null,
   ContractName         varchar(80)          null,
   TradeBorder          int                  null,
   ContractLimit        int                  null,
   ContractSide         int                  null,
   TradeDirection       int                  null,
   HaveGoodsFlow        int                  null,
   ContractPeriodS      datetime             null,
   ContractPeriodE      datetime             null,
   Premium              numeric(18,4)        null,
   InitQP               datetime             null,
   AssetId              int                  null,
   SettleCurrency       int                  null,
   SignAmount           numeric(18,4)        null,
   ExeAmount            numeric(18,4)        null,
   UnitId               int                  null,
   PriceMode            int                  null,
   Memo                 varchar(4000)        null,
   DeliveryStyle        int                  null,
   DeliveryDate         datetime             null,
   CreateFrom           int                  null,
   ContractStatus       int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约',
   'user', 'dbo', 'table', 'Con_Contract'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '内部合约号',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '外部合约号',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'OutContractNo'
go

execute sp_addextendedproperty 'MS_Description', 
   '签订日期',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约名称',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractName'
go

execute sp_addextendedproperty 'MS_Description', 
   '贸易境区(内外贸)',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'TradeBorder'
go

execute sp_addextendedproperty 'MS_Description', 
   '合同时限(长零)',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractLimit'
go

execute sp_addextendedproperty 'MS_Description', 
   '合同对方(内外部合同)',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractSide'
go

execute sp_addextendedproperty 'MS_Description', 
   '贸易方向(购销)',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'TradeDirection'
go

execute sp_addextendedproperty 'MS_Description', 
   '贸易融资',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'HaveGoodsFlow'
go

execute sp_addextendedproperty 'MS_Description', 
   '开始执行日',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractPeriodS'
go

execute sp_addextendedproperty 'MS_Description', 
   '结束执行日',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractPeriodE'
go

execute sp_addextendedproperty 'MS_Description', 
   '升贴水',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'Premium'
go

execute sp_addextendedproperty 'MS_Description', 
   '初始QP',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'InitQP'
go

execute sp_addextendedproperty 'MS_Description', 
   '品种',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'AssetId'
go

execute sp_addextendedproperty 'MS_Description', 
   '结算币种',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'SettleCurrency'
go

execute sp_addextendedproperty 'MS_Description', 
   '签订数量',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'SignAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '执行数量',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ExeAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'UnitId'
go

execute sp_addextendedproperty 'MS_Description', 
   '定价方式',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'PriceMode'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货方式',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'DeliveryStyle'
go

execute sp_addextendedproperty 'MS_Description', 
   '交货日期',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'DeliveryDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建来源',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'CreateFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '合同状态',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'ContractStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_Contract', 'column', 'LastModifyTime'
go

alter table dbo.Con_Contract
   add constraint PK_CON_CONTRACT primary key (ContractId)
go

/****** Object:  StoredProcedure [dbo].[CreateContractNo]    Script Date: 01/20/2015 10:50:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreateContractNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateContractNo]
GO

/****** Object:  StoredProcedure [dbo].[CreateContractNo]    Script Date: 01/20/2015 10:50:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[CreateContractNo]
(
@identity int
)
as
begin

declare @typeName varchar(20)
declare @contractNo varchar(20)

set @typeName = 'Contract'
set @contractNo = @typeName + CAST(@identity as varchar)

update dbo.Con_Contract set ContractNo=@contractNo where ContractId = @identity

end



GO


/****** Object:  Stored Procedure [dbo].Con_ContractGet    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractLoad    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractInsert    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractUpdate    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractUpdateStatus    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractUpdateStatus    Script Date: 2015年1月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractUpdateStatus
// 存储过程功能描述：更新Con_Contract中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_Contract'

set @str = 'update [dbo].[Con_Contract] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContractId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractGoBack
// 存储过程功能描述：撤返Con_Contract，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_Contract'

set @str = 'update [dbo].[Con_Contract] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContractId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractGet
// 存储过程功能描述：查询指定Con_Contract的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractGet
    /*
	@ContractId int
    */
    @id int
AS

SELECT
	[ContractId],
	[ContractNo],
	[OutContractNo],
	[ContractDate],
	[ContractName],
	[TradeBorder],
	[ContractLimit],
	[ContractSide],
	[TradeDirection],
	[HaveGoodsFlow],
	[ContractPeriodS],
	[ContractPeriodE],
	[Premium],
	[InitQP],
	[AssetId],
	[SettleCurrency],
	[SignAmount],
	[ExeAmount],
	[UnitId],
	[PriceMode],
	[Memo],
	[DeliveryStyle],
	[DeliveryDate],
	[CreateFrom],
	[ContractStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_Contract]
WHERE
	[ContractId] = @id

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
// 存储过程名：[dbo].Con_ContractLoad
// 存储过程功能描述：查询所有Con_Contract记录
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractLoad
AS

SELECT
	[ContractId],
	[ContractNo],
	[OutContractNo],
	[ContractDate],
	[ContractName],
	[TradeBorder],
	[ContractLimit],
	[ContractSide],
	[TradeDirection],
	[HaveGoodsFlow],
	[ContractPeriodS],
	[ContractPeriodE],
	[Premium],
	[InitQP],
	[AssetId],
	[SettleCurrency],
	[SignAmount],
	[ExeAmount],
	[UnitId],
	[PriceMode],
	[Memo],
	[DeliveryStyle],
	[DeliveryDate],
	[CreateFrom],
	[ContractStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_Contract]

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
// 存储过程名：[dbo].Con_ContractInsert
// 存储过程功能描述：新增一条Con_Contract记录
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractInsert
	@ContractNo varchar(20) =NULL ,
	@OutContractNo varchar(200) =NULL ,
	@ContractDate datetime =NULL ,
	@ContractName varchar(80) =NULL ,
	@TradeBorder int =NULL ,
	@ContractLimit int =NULL ,
	@ContractSide int =NULL ,
	@TradeDirection int =NULL ,
	@HaveGoodsFlow int =NULL ,
	@ContractPeriodS datetime =NULL ,
	@ContractPeriodE datetime =NULL ,
	@Premium numeric(18, 4) =NULL ,
	@InitQP datetime =NULL ,
	@AssetId int =NULL ,
	@SettleCurrency int =NULL ,
	@SignAmount numeric(18, 4) =NULL ,
	@ExeAmount numeric(18, 4) =NULL ,
	@UnitId int =NULL ,
	@PriceMode int =NULL ,
	@Memo varchar(4000) =NULL ,
	@DeliveryStyle int =NULL ,
	@DeliveryDate datetime =NULL ,
	@CreateFrom int =NULL ,
	@ContractStatus int =NULL ,
	@CreatorId int =NULL ,
	@ContractId int OUTPUT
AS

INSERT INTO [dbo].[Con_Contract] (
	[ContractNo],
	[OutContractNo],
	[ContractDate],
	[ContractName],
	[TradeBorder],
	[ContractLimit],
	[ContractSide],
	[TradeDirection],
	[HaveGoodsFlow],
	[ContractPeriodS],
	[ContractPeriodE],
	[Premium],
	[InitQP],
	[AssetId],
	[SettleCurrency],
	[SignAmount],
	[ExeAmount],
	[UnitId],
	[PriceMode],
	[Memo],
	[DeliveryStyle],
	[DeliveryDate],
	[CreateFrom],
	[ContractStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractNo,
	@OutContractNo,
	@ContractDate,
	@ContractName,
	@TradeBorder,
	@ContractLimit,
	@ContractSide,
	@TradeDirection,
	@HaveGoodsFlow,
	@ContractPeriodS,
	@ContractPeriodE,
	@Premium,
	@InitQP,
	@AssetId,
	@SettleCurrency,
	@SignAmount,
	@ExeAmount,
	@UnitId,
	@PriceMode,
	@Memo,
	@DeliveryStyle,
	@DeliveryDate,
	@CreateFrom,
	@ContractStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ContractId = @@IDENTITY

exec CreateContractNo @identity =@ContractId

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
// 存储过程名：[dbo].Con_ContractUpdate
// 存储过程功能描述：更新Con_Contract
// 创建人：CodeSmith
// 创建时间： 2015年1月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractUpdate
    @ContractId int,
@ContractNo varchar(20) = NULL,
@OutContractNo varchar(200) = NULL,
@ContractDate datetime = NULL,
@ContractName varchar(80) = NULL,
@TradeBorder int = NULL,
@ContractLimit int = NULL,
@ContractSide int = NULL,
@TradeDirection int = NULL,
@HaveGoodsFlow int = NULL,
@ContractPeriodS datetime = NULL,
@ContractPeriodE datetime = NULL,
@Premium numeric(18, 4) = NULL,
@InitQP datetime = NULL,
@AssetId int = NULL,
@SettleCurrency int = NULL,
@SignAmount numeric(18, 4) = NULL,
@ExeAmount numeric(18, 4) = NULL,
@UnitId int = NULL,
@PriceMode int = NULL,
@Memo varchar(4000) = NULL,
@DeliveryStyle int = NULL,
@DeliveryDate datetime = NULL,
@CreateFrom int = NULL,
@ContractStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_Contract] SET
	[ContractNo] = @ContractNo,
	[OutContractNo] = @OutContractNo,
	[ContractDate] = @ContractDate,
	[ContractName] = @ContractName,
	[TradeBorder] = @TradeBorder,
	[ContractLimit] = @ContractLimit,
	[ContractSide] = @ContractSide,
	[TradeDirection] = @TradeDirection,
	[HaveGoodsFlow] = @HaveGoodsFlow,
	[ContractPeriodS] = @ContractPeriodS,
	[ContractPeriodE] = @ContractPeriodE,
	[Premium] = @Premium,
	[InitQP] = @InitQP,
	[AssetId] = @AssetId,
	[SettleCurrency] = @SettleCurrency,
	[SignAmount] = @SignAmount,
	[ExeAmount] = @ExeAmount,
	[UnitId] = @UnitId,
	[PriceMode] = @PriceMode,
	[Memo] = @Memo,
	[DeliveryStyle] = @DeliveryStyle,
	[DeliveryDate] = @DeliveryDate,
	[CreateFrom] = @CreateFrom,
	[ContractStatus] = @ContractStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ContractId] = @ContractId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



