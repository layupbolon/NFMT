alter table Pri_Interest
   drop constraint PK_PRI_INTEREST
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_Interest')
            and   type = 'U')
   drop table Pri_Interest
go

/*==============================================================*/
/* Table: Pri_Interest                                          */
/*==============================================================*/
create table Pri_Interest (
   InterestId           int                  identity,
   SubContractId        int                  null,
   ContractId           int                  null,
   CurrencyId           int                  null,
   PricingUnit          decimal(18,4)        null,
   Premium              decimal(18,4)        null,
   OtherPrice           decimal(18,4)        null,
   InterestPrice        decimal(18,4)        null,
   PayCapital           decimal(18,6)        null,
   CurCapital           decimal(18,6)        null,
   InterestRate         decimal(18,8)        null,
   InterestBala         decimal(18,4)        null,
   InterestAmountDay    decimal(18,4)        null,
   InterestAmount       decimal(18,4)        null,
   Unit                 int                  null,
   InterestStyle        int                  null,
   Memo                 varchar(4000)        null,
   InterestDate         datetime             null,
   InterestStatus       int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '利息表',
   'user', @CurrentUser, 'table', 'Pri_Interest'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'SubContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '币种',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'CurrencyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '期货点价价格',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'PricingUnit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '升贴水',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'Premium'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他费用',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'OtherPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计息价格',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预付本金',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'PayCapital'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '当前计息本金',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'CurCapital'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '税率',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestRate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息总额',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'v',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestAmountDay'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结息重量',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '重量单位',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'Unit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计息方式',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestStyle'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结息日期',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息结算状态',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'InterestStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_Interest', 'column', 'LastModifyTime'
go

alter table Pri_Interest
   add constraint PK_PRI_INTEREST primary key (InterestId)
go

/****** Object:  Stored Procedure [dbo].Pri_InterestGet    Script Date: 2015年3月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestLoad    Script Date: 2015年3月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestInsert    Script Date: 2015年3月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestUpdate    Script Date: 2015年3月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestUpdateStatus    Script Date: 2015年3月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestUpdateStatus    Script Date: 2015年3月17日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_InterestUpdateStatus
// 存储过程功能描述：更新Pri_Interest中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_Interest'

set @str = 'update [dbo].[Pri_Interest] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where InterestId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_InterestGoBack
// 存储过程功能描述：撤返Pri_Interest，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_Interest'

set @str = 'update [dbo].[Pri_Interest] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where InterestId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_InterestGet
// 存储过程功能描述：查询指定Pri_Interest的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestGet
    /*
	@InterestId int
    */
    @id int
AS

SELECT
	[InterestId],
	[SubContractId],
	[ContractId],
	[CurrencyId],
	[PricingUnit],
	[Premium],
	[OtherPrice],
	[InterestPrice],
	[PayCapital],
	[CurCapital],
	[InterestRate],
	[InterestBala],
	[InterestAmountDay],
	[InterestAmount],
	[Unit],
	[InterestStyle],
	[Memo],
	[InterestDate],
	[InterestStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_Interest]
WHERE
	[InterestId] = @id

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
// 存储过程名：[dbo].Pri_InterestLoad
// 存储过程功能描述：查询所有Pri_Interest记录
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestLoad
AS

SELECT
	[InterestId],
	[SubContractId],
	[ContractId],
	[CurrencyId],
	[PricingUnit],
	[Premium],
	[OtherPrice],
	[InterestPrice],
	[PayCapital],
	[CurCapital],
	[InterestRate],
	[InterestBala],
	[InterestAmountDay],
	[InterestAmount],
	[Unit],
	[InterestStyle],
	[Memo],
	[InterestDate],
	[InterestStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_Interest]

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
// 存储过程名：[dbo].Pri_InterestInsert
// 存储过程功能描述：新增一条Pri_Interest记录
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestInsert
	@SubContractId int =NULL ,
	@ContractId int =NULL ,
	@CurrencyId int =NULL ,
	@PricingUnit decimal(18, 4) =NULL ,
	@Premium decimal(18, 4) =NULL ,
	@OtherPrice decimal(18, 4) =NULL ,
	@InterestPrice decimal(18, 4) =NULL ,
	@PayCapital decimal(18, 6) =NULL ,
	@CurCapital decimal(18, 6) =NULL ,
	@InterestRate decimal(18, 8) =NULL ,
	@InterestBala decimal(18, 4) =NULL ,
	@InterestAmountDay decimal(18, 4) =NULL ,
	@InterestAmount decimal(18, 4) =NULL ,
	@Unit int =NULL ,
	@InterestStyle int =NULL ,
	@Memo varchar(4000) =NULL ,
	@InterestDate datetime =NULL ,
	@InterestStatus int =NULL ,
	@CreatorId int =NULL ,
	@InterestId int OUTPUT
AS

INSERT INTO [dbo].[Pri_Interest] (
	[SubContractId],
	[ContractId],
	[CurrencyId],
	[PricingUnit],
	[Premium],
	[OtherPrice],
	[InterestPrice],
	[PayCapital],
	[CurCapital],
	[InterestRate],
	[InterestBala],
	[InterestAmountDay],
	[InterestAmount],
	[Unit],
	[InterestStyle],
	[Memo],
	[InterestDate],
	[InterestStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@SubContractId,
	@ContractId,
	@CurrencyId,
	@PricingUnit,
	@Premium,
	@OtherPrice,
	@InterestPrice,
	@PayCapital,
	@CurCapital,
	@InterestRate,
	@InterestBala,
	@InterestAmountDay,
	@InterestAmount,
	@Unit,
	@InterestStyle,
	@Memo,
	@InterestDate,
	@InterestStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @InterestId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_InterestUpdate
// 存储过程功能描述：更新Pri_Interest
// 创建人：CodeSmith
// 创建时间： 2015年3月17日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestUpdate
    @InterestId int,
@SubContractId int = NULL,
@ContractId int = NULL,
@CurrencyId int = NULL,
@PricingUnit decimal(18, 4) = NULL,
@Premium decimal(18, 4) = NULL,
@OtherPrice decimal(18, 4) = NULL,
@InterestPrice decimal(18, 4) = NULL,
@PayCapital decimal(18, 6) = NULL,
@CurCapital decimal(18, 6) = NULL,
@InterestRate decimal(18, 8) = NULL,
@InterestBala decimal(18, 4) = NULL,
@InterestAmountDay decimal(18, 4) = NULL,
@InterestAmount decimal(18, 4) = NULL,
@Unit int = NULL,
@InterestStyle int = NULL,
@Memo varchar(4000) = NULL,
@InterestDate datetime = NULL,
@InterestStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_Interest] SET
	[SubContractId] = @SubContractId,
	[ContractId] = @ContractId,
	[CurrencyId] = @CurrencyId,
	[PricingUnit] = @PricingUnit,
	[Premium] = @Premium,
	[OtherPrice] = @OtherPrice,
	[InterestPrice] = @InterestPrice,
	[PayCapital] = @PayCapital,
	[CurCapital] = @CurCapital,
	[InterestRate] = @InterestRate,
	[InterestBala] = @InterestBala,
	[InterestAmountDay] = @InterestAmountDay,
	[InterestAmount] = @InterestAmount,
	[Unit] = @Unit,
	[InterestStyle] = @InterestStyle,
	[Memo] = @Memo,
	[InterestDate] = @InterestDate,
	[InterestStatus] = @InterestStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[InterestId] = @InterestId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



