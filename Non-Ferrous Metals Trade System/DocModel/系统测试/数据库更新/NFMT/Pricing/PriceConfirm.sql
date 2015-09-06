alter table Pri_PriceConfirm
   drop constraint PK_PRI_PRICECONFIRM
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_PriceConfirm')
            and   type = 'U')
   drop table Pri_PriceConfirm
go

/*==============================================================*/
/* Table: Pri_PriceConfirm                                      */
/*==============================================================*/
create table Pri_PriceConfirm (
   PriceConfirmId       int                  identity,
   OutCorpId            int                  null,
   InCorpId             int                  null,
   ContractId           int                  null,
   SubId                int                  null,
   ContractAmount       decimal(18,4)        null,
   SubAmount            decimal(18,4)        null,
   RealityAmount        decimal(18,4)        null,
   PricingAvg           decimal(18,4)        null,
   PremiumAvg           decimal(18,4)        null,
   InterestAvg          decimal(18,4)        null,
   OtherAvg             decimal(18,4)        null,
   InterestBala         decimal(18,4)        null,
   SettlePrice          decimal(18,4)        null,
   SettleBala           decimal(18,4)        null,
   PricingDate          datetime             null,
   CurrencyId           int                  null,
   UnitId               int                  null,
   TakeCorpId           int                  null,
   ContactPerson        varchar(200)         null,
   Memo                 varchar(4000)        null,
   PriceConfirmStatus   int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '价格确认表',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格确认序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'PriceConfirmId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '对方公司',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'OutCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '我方公司',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'InCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'SubId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约签订数量',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'ContractAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约签订数量',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'SubAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实际发货数量',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'RealityAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '期货点价均价',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'PricingAvg'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '升贴水均价',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'PremiumAvg'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息均价',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'InterestAvg'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他均价',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'OtherAvg'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息总额',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'InterestBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结算单价',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'SettlePrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结算总额',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'SettleBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '选价日期',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'PricingDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '重量单位',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'UnitId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '提货单位',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'TakeCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '供方委托提货单位联系人',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'ContactPerson'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格确认状态',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'PriceConfirmStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_PriceConfirm', 'column', 'LastModifyTime'
go

alter table Pri_PriceConfirm
   add constraint PK_PRI_PRICECONFIRM primary key (PriceConfirmId)
go


/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmGet    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmLoad    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmInsert    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmUpdate    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmUpdateStatus    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PriceConfirmUpdateStatus    Script Date: 2015年3月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PriceConfirmGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PriceConfirmGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PriceConfirmUpdateStatus
// 存储过程功能描述：更新Pri_PriceConfirm中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PriceConfirm'

set @str = 'update [dbo].[Pri_PriceConfirm] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PriceConfirmId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PriceConfirmGoBack
// 存储过程功能描述：撤返Pri_PriceConfirm，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PriceConfirm'

set @str = 'update [dbo].[Pri_PriceConfirm] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PriceConfirmId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PriceConfirmGet
// 存储过程功能描述：查询指定Pri_PriceConfirm的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmGet
    /*
	@PriceConfirmId int
    */
    @id int
AS

SELECT
	[PriceConfirmId],
	[OutCorpId],
	[InCorpId],
	[ContractId],
	[SubId],
	[ContractAmount],
	[SubAmount],
	[RealityAmount],
	[PricingAvg],
	[PremiumAvg],
	[InterestAvg],
	[OtherAvg],
	[InterestBala],
	[SettlePrice],
	[SettleBala],
	[PricingDate],
	[CurrencyId],
	[UnitId],
	[TakeCorpId],
	[ContactPerson],
	[Memo],
	[PriceConfirmStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PriceConfirm]
WHERE
	[PriceConfirmId] = @id

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
// 存储过程名：[dbo].Pri_PriceConfirmLoad
// 存储过程功能描述：查询所有Pri_PriceConfirm记录
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmLoad
AS

SELECT
	[PriceConfirmId],
	[OutCorpId],
	[InCorpId],
	[ContractId],
	[SubId],
	[ContractAmount],
	[SubAmount],
	[RealityAmount],
	[PricingAvg],
	[PremiumAvg],
	[InterestAvg],
	[OtherAvg],
	[InterestBala],
	[SettlePrice],
	[SettleBala],
	[PricingDate],
	[CurrencyId],
	[UnitId],
	[TakeCorpId],
	[ContactPerson],
	[Memo],
	[PriceConfirmStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PriceConfirm]

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
// 存储过程名：[dbo].Pri_PriceConfirmInsert
// 存储过程功能描述：新增一条Pri_PriceConfirm记录
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmInsert
	@OutCorpId int =NULL ,
	@InCorpId int =NULL ,
	@ContractId int =NULL ,
	@SubId int =NULL ,
	@ContractAmount decimal(18, 4) =NULL ,
	@SubAmount decimal(18, 4) =NULL ,
	@RealityAmount decimal(18, 4) =NULL ,
	@PricingAvg decimal(18, 4) =NULL ,
	@PremiumAvg decimal(18, 4) =NULL ,
	@InterestAvg decimal(18, 4) =NULL ,
	@OtherAvg decimal(18, 4) =NULL ,
	@InterestBala decimal(18, 4) =NULL ,
	@SettlePrice decimal(18, 4) =NULL ,
	@SettleBala decimal(18, 4) =NULL ,
	@PricingDate datetime =NULL ,
	@CurrencyId int =NULL ,
	@UnitId int =NULL ,
	@TakeCorpId int =NULL ,
	@ContactPerson varchar(200) =NULL ,
	@Memo varchar(4000) =NULL ,
	@PriceConfirmStatus int =NULL ,
	@CreatorId int =NULL ,
	@PriceConfirmId int OUTPUT
AS

INSERT INTO [dbo].[Pri_PriceConfirm] (
	[OutCorpId],
	[InCorpId],
	[ContractId],
	[SubId],
	[ContractAmount],
	[SubAmount],
	[RealityAmount],
	[PricingAvg],
	[PremiumAvg],
	[InterestAvg],
	[OtherAvg],
	[InterestBala],
	[SettlePrice],
	[SettleBala],
	[PricingDate],
	[CurrencyId],
	[UnitId],
	[TakeCorpId],
	[ContactPerson],
	[Memo],
	[PriceConfirmStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@OutCorpId,
	@InCorpId,
	@ContractId,
	@SubId,
	@ContractAmount,
	@SubAmount,
	@RealityAmount,
	@PricingAvg,
	@PremiumAvg,
	@InterestAvg,
	@OtherAvg,
	@InterestBala,
	@SettlePrice,
	@SettleBala,
	@PricingDate,
	@CurrencyId,
	@UnitId,
	@TakeCorpId,
	@ContactPerson,
	@Memo,
	@PriceConfirmStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PriceConfirmId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_PriceConfirmUpdate
// 存储过程功能描述：更新Pri_PriceConfirm
// 创建人：CodeSmith
// 创建时间： 2015年3月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PriceConfirmUpdate
    @PriceConfirmId int,
@OutCorpId int = NULL,
@InCorpId int = NULL,
@ContractId int = NULL,
@SubId int = NULL,
@ContractAmount decimal(18, 4) = NULL,
@SubAmount decimal(18, 4) = NULL,
@RealityAmount decimal(18, 4) = NULL,
@PricingAvg decimal(18, 4) = NULL,
@PremiumAvg decimal(18, 4) = NULL,
@InterestAvg decimal(18, 4) = NULL,
@OtherAvg decimal(18, 4) = NULL,
@InterestBala decimal(18, 4) = NULL,
@SettlePrice decimal(18, 4) = NULL,
@SettleBala decimal(18, 4) = NULL,
@PricingDate datetime = NULL,
@CurrencyId int = NULL,
@UnitId int = NULL,
@TakeCorpId int = NULL,
@ContactPerson varchar(200) = NULL,
@Memo varchar(4000) = NULL,
@PriceConfirmStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_PriceConfirm] SET
	[OutCorpId] = @OutCorpId,
	[InCorpId] = @InCorpId,
	[ContractId] = @ContractId,
	[SubId] = @SubId,
	[ContractAmount] = @ContractAmount,
	[SubAmount] = @SubAmount,
	[RealityAmount] = @RealityAmount,
	[PricingAvg] = @PricingAvg,
	[PremiumAvg] = @PremiumAvg,
	[InterestAvg] = @InterestAvg,
	[OtherAvg] = @OtherAvg,
	[InterestBala] = @InterestBala,
	[SettlePrice] = @SettlePrice,
	[SettleBala] = @SettleBala,
	[PricingDate] = @PricingDate,
	[CurrencyId] = @CurrencyId,
	[UnitId] = @UnitId,
	[TakeCorpId] = @TakeCorpId,
	[ContactPerson] = @ContactPerson,
	[Memo] = @Memo,
	[PriceConfirmStatus] = @PriceConfirmStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PriceConfirmId] = @PriceConfirmId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



