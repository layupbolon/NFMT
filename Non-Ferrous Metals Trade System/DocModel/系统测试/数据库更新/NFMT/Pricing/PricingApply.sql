alter table Pri_PricingApply
   drop constraint PK_PRI_PRICINGAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_PricingApply')
            and   type = 'U')
   drop table Pri_PricingApply
go

/*==============================================================*/
/* Table: Pri_PricingApply                                      */
/*==============================================================*/
create table Pri_PricingApply (
   PricingApplyId       int                  identity,
   ApplyId              int                  null,
   SubContractId        int                  null,
   ContractId           int                  null,
   PricingDirection     int                  null,
   QPDate               datetime             null,
   DelayAmount          decimal(18,4)        null,
   DelayFee             decimal(18,4)        null default 0,
   DelayQPDate          datetime             null,
   OtherFee             decimal(18,4)        null,
   OtherDesc            varchar(800)         null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   MinPrice             decimal(18,4)        null,
   MaxPrice             decimal(18,4)        null,
   CurrencyId           int                  null,
   PricingBlocId        int                  null,
   PricingCorpId        int                  null,
   PricingWeight        decimal(18,4)        null,
   MUId                 int                  null,
   AssertId             int                  null,
   PricingPersoinId     int                  null,
   PricingStyle         int                  null,
   declareDate          datetime             null,
   AvgPriceStart        datetime             null,
   AvgPriceEnd          datetime             null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '点价申请',
   'user', @CurrentUser, 'table', 'Pri_PricingApply'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'PricingApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请主表序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'ApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'SubContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价方向',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'PricingDirection'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'QP日期',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'QPDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '延期总量',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'DelayAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '延期总费',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'DelayFee'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '延期QP',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'DelayQPDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他费',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'OtherFee'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他费描述',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'OtherDesc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价起始时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'StartTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价最终时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'EndTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价最低均价',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'MinPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价最高均价',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'MaxPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格币种',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价集团',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'PricingBlocId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价公司',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'PricingCorpId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价重量',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'PricingWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '重量单位',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'MUId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价品种',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'AssertId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价权限人',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'PricingPersoinId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价方式',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'PricingStyle'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '宣布日',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'declareDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '均价起始计价日',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'AvgPriceStart'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '均价终止计价日',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'AvgPriceEnd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请创建人',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApply', 'column', 'LastModifyTime'
go

alter table Pri_PricingApply
   add constraint PK_PRI_PRICINGAPPLY primary key (PricingApplyId)
go

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingApplyUpdateStatus
// 存储过程功能描述：更新Pri_PricingApply中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingApply'

set @str = 'update [dbo].[Pri_PricingApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PricingApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PricingApplyGoBack
// 存储过程功能描述：撤返Pri_PricingApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingApply'

set @str = 'update [dbo].[Pri_PricingApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PricingApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PricingApplyGet
// 存储过程功能描述：查询指定Pri_PricingApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyGet
    /*
	@PricingApplyId int
    */
    @id int
AS

SELECT
	[PricingApplyId],
	[ApplyId],
	[SubContractId],
	[ContractId],
	[PricingDirection],
	[QPDate],
	[DelayAmount],
	[DelayFee],
	[DelayQPDate],
	[OtherFee],
	[OtherDesc],
	[StartTime],
	[EndTime],
	[MinPrice],
	[MaxPrice],
	[CurrencyId],
	[PricingBlocId],
	[PricingCorpId],
	[PricingWeight],
	[MUId],
	[AssertId],
	[PricingPersoinId],
	[PricingStyle],
	[DeclareDate],
	[AvgPriceStart],
	[AvgPriceEnd],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingApply]
WHERE
	[PricingApplyId] = @id

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
// 存储过程名：[dbo].Pri_PricingApplyLoad
// 存储过程功能描述：查询所有Pri_PricingApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyLoad
AS

SELECT
	[PricingApplyId],
	[ApplyId],
	[SubContractId],
	[ContractId],
	[PricingDirection],
	[QPDate],
	[DelayAmount],
	[DelayFee],
	[DelayQPDate],
	[OtherFee],
	[OtherDesc],
	[StartTime],
	[EndTime],
	[MinPrice],
	[MaxPrice],
	[CurrencyId],
	[PricingBlocId],
	[PricingCorpId],
	[PricingWeight],
	[MUId],
	[AssertId],
	[PricingPersoinId],
	[PricingStyle],
	[DeclareDate],
	[AvgPriceStart],
	[AvgPriceEnd],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingApply]

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
// 存储过程名：[dbo].Pri_PricingApplyInsert
// 存储过程功能描述：新增一条Pri_PricingApply记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyInsert
	@ApplyId int =NULL ,
	@SubContractId int =NULL ,
	@ContractId int =NULL ,
	@PricingDirection int =NULL ,
	@QPDate datetime =NULL ,
	@DelayAmount decimal(18, 4) =NULL ,
	@DelayFee decimal(18, 4) =NULL ,
	@DelayQPDate datetime =NULL ,
	@OtherFee decimal(18, 4) =NULL ,
	@OtherDesc varchar(800) =NULL ,
	@StartTime datetime =NULL ,
	@EndTime datetime =NULL ,
	@MinPrice decimal(18, 4) =NULL ,
	@MaxPrice decimal(18, 4) =NULL ,
	@CurrencyId int =NULL ,
	@PricingBlocId int =NULL ,
	@PricingCorpId int =NULL ,
	@PricingWeight decimal(18, 4) =NULL ,
	@MUId int =NULL ,
	@AssertId int =NULL ,
	@PricingPersoinId int =NULL ,
	@PricingStyle int =NULL ,
	@DeclareDate datetime =NULL ,
	@AvgPriceStart datetime =NULL ,
	@AvgPriceEnd datetime =NULL ,
	@CreatorId int =NULL ,
	@PricingApplyId int OUTPUT
AS

INSERT INTO [dbo].[Pri_PricingApply] (
	[ApplyId],
	[SubContractId],
	[ContractId],
	[PricingDirection],
	[QPDate],
	[DelayAmount],
	[DelayFee],
	[DelayQPDate],
	[OtherFee],
	[OtherDesc],
	[StartTime],
	[EndTime],
	[MinPrice],
	[MaxPrice],
	[CurrencyId],
	[PricingBlocId],
	[PricingCorpId],
	[PricingWeight],
	[MUId],
	[AssertId],
	[PricingPersoinId],
	[PricingStyle],
	[DeclareDate],
	[AvgPriceStart],
	[AvgPriceEnd],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ApplyId,
	@SubContractId,
	@ContractId,
	@PricingDirection,
	@QPDate,
	@DelayAmount,
	@DelayFee,
	@DelayQPDate,
	@OtherFee,
	@OtherDesc,
	@StartTime,
	@EndTime,
	@MinPrice,
	@MaxPrice,
	@CurrencyId,
	@PricingBlocId,
	@PricingCorpId,
	@PricingWeight,
	@MUId,
	@AssertId,
	@PricingPersoinId,
	@PricingStyle,
	@DeclareDate,
	@AvgPriceStart,
	@AvgPriceEnd,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PricingApplyId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_PricingApplyUpdate
// 存储过程功能描述：更新Pri_PricingApply
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyUpdate
    @PricingApplyId int,
@ApplyId int = NULL,
@SubContractId int = NULL,
@ContractId int = NULL,
@PricingDirection int = NULL,
@QPDate datetime = NULL,
@DelayAmount decimal(18, 4) = NULL,
@DelayFee decimal(18, 4) = NULL,
@DelayQPDate datetime = NULL,
@OtherFee decimal(18, 4) = NULL,
@OtherDesc varchar(800) = NULL,
@StartTime datetime = NULL,
@EndTime datetime = NULL,
@MinPrice decimal(18, 4) = NULL,
@MaxPrice decimal(18, 4) = NULL,
@CurrencyId int = NULL,
@PricingBlocId int = NULL,
@PricingCorpId int = NULL,
@PricingWeight decimal(18, 4) = NULL,
@MUId int = NULL,
@AssertId int = NULL,
@PricingPersoinId int = NULL,
@PricingStyle int = NULL,
@DeclareDate datetime = NULL,
@AvgPriceStart datetime = NULL,
@AvgPriceEnd datetime = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_PricingApply] SET
	[ApplyId] = @ApplyId,
	[SubContractId] = @SubContractId,
	[ContractId] = @ContractId,
	[PricingDirection] = @PricingDirection,
	[QPDate] = @QPDate,
	[DelayAmount] = @DelayAmount,
	[DelayFee] = @DelayFee,
	[DelayQPDate] = @DelayQPDate,
	[OtherFee] = @OtherFee,
	[OtherDesc] = @OtherDesc,
	[StartTime] = @StartTime,
	[EndTime] = @EndTime,
	[MinPrice] = @MinPrice,
	[MaxPrice] = @MaxPrice,
	[CurrencyId] = @CurrencyId,
	[PricingBlocId] = @PricingBlocId,
	[PricingCorpId] = @PricingCorpId,
	[PricingWeight] = @PricingWeight,
	[MUId] = @MUId,
	[AssertId] = @AssertId,
	[PricingPersoinId] = @PricingPersoinId,
	[PricingStyle] = @PricingStyle,
	[DeclareDate] = @DeclareDate,
	[AvgPriceStart] = @AvgPriceStart,
	[AvgPriceEnd] = @AvgPriceEnd,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PricingApplyId] = @PricingApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



